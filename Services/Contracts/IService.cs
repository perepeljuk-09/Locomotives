namespace Locomotives.API.Services.Contracts
{
    public interface IService<T> where T : class
    {
        Task<bool> DeleteAsync(int id);
    }
}
