using Dapper;
using Locomotives.API.Mappers;
using Locomotives.API.Models.Dto.Depot;
using Locomotives.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locomotives.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeDappController : ControllerBase
	{
		private const string _sqlConnection = "Data Source=(local); Database=applicationdb; MultipleActiveResultSets=true; TrustServerCertificate=True; Trusted_Connection=True;";
		private readonly string _npsqlConnection = "Host=localhost;Port=5432;Database=locomotivesDb;Username=postgres;Password=root";
		//private readonly string _npsqlConnection;

		public EmployeeDappController(IConfiguration configuration)
		{
			//_npsqlConnection = configuration.GetConnectionString("pg") ?? throw new ApplicationException("Hasn't connection string");
		}

		// GET: api/<ValuesController>
		[HttpGet("depots/{id}")]
		public async Task<ActionResult<List<DepotDto>>> GetDepots([FromRoute] int id)
		{
			Dictionary<int, DepotDto> keyValuePairs = new Dictionary<int, DepotDto>();
			using(IDbConnection db = new NpgsqlConnection(_npsqlConnection))
			{
				var result = await db.QueryAsync<Depot, Locomotive, DepotDto>(
					$"""
					SELECT
						d.depot_id AS {nameof(Depot.Id)},
						d.depot_name AS {nameof(Depot.Name)},
						l.locomotive_id AS {nameof(Locomotive.Id)},
						l.locomotive_name AS {nameof(Locomotive.Name)},
						l.locomotive_category_id
					FROM main.depots d
					JOIN main.locomotives l ON l.depot_id = d.depot_id
					WHERE d.depot_id = @depotId
					""",
					(depot, loco) =>
					{
						DepotDto dto = new();
						if (keyValuePairs.TryGetValue(depot.Id, out var value))
						{
							dto = value;
						}
                        else
                        {
							dto = DepotMapper.ToDto(depot);
							keyValuePairs.Add(depot.Id, dto);
                        }

						dto.Locomotives.Add(loco.Id, loco.Name);


						return dto;
					},
					new { depotId = id },
					splitOn: $"{nameof(Locomotive.Id)}"
					);

				if (result == null)
					return BadRequest();

				return Ok(result.First());
			}
		}
		// GET: api/<ValuesController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Employee>>> Get()
		{

			using(IDbConnection db = new SqlConnection(_sqlConnection))
			{
				var result = await db.QueryAsync<Employee>("SELECT employee_id as employeeId," +
					"department," +
					"first_name as firstName," +
					"last_name as lastName," +
					"patronomyc, birthday," +
					"date_of_employment as dateOfEmployment," +
					"salary" +
					" FROM dbo.employees");

				if (result == null)
					return BadRequest();

				return Ok(result);
			}
		}

		// GET api/<ValuesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Employee>> Get(int id)
		{
			using(var db = new SqlConnection(_sqlConnection))
			{

				var result = await db.QueryFirstOrDefaultAsync<Employee>("SELECT employee_id as employeeId," +
					"department," +
					"first_name as firstName," +
					"last_name as lastName," +
					"patronomyc, birthday," +
					"date_of_employment as dateOfEmployment," +
					"salary " +
					"FROM dbo.employees " +
					"WHERE employee_id = @empId",
					new { empId = id });

				if (result == null)
					return BadRequest($"Employee with id equal {id}, not found");

				return Ok(result);
			}
		}

		// POST api/<ValuesController>
		[HttpPost]
		public async Task<ActionResult<int>> Post([FromBody] Employee newEmp)
		{

			using (var db = new SqlConnection(_sqlConnection))
			{

				var result = await db.ExecuteAsync("INSERT INTO dbo.employees (department, first_name, last_name, patronomyc, birthday, date_of_employment, salary)" +
					"VALUES (@Department, @FirstName, @LastName, @Patronomyc, @Birthday, @DateOfEmployment, @Salary)", newEmp);


				return Ok(result);
			}
		}

		// PUT api/<ValuesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<int>> Put(int id, [FromBody] Employee editEmp)
		{

			using (var db = new SqlConnection(_sqlConnection))
			{
				editEmp.EmployeeId = id;
				var result = await db.ExecuteAsync("UPDATE dbo.employees " +
					"SET department = @Department, " +
					"first_name = @FirstName, " +
					"last_name = @LastName, " +
					"patronomyc = @Patronomyc, " +
					"birthday = @Birthday, " +
					"date_of_employment = @DateOfEmployment, " +
					"salary = @Salary " +
					"WHERE employee_id = @EmployeeId", editEmp);


				return Ok(result);
			}
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete(int id)
		{


			using (var db = new SqlConnection(_sqlConnection))
			{
				var result = await db.ExecuteAsync("DELETE FROM dbo.employees WHERE employee_id = @EmployeeId", new { EmployeeId = id});


				return Ok(result);
			}
		}
	}
}
