import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';
import { MultiSelect } from 'react-multi-select-component';
import { OptionType } from './CreateDriver';
import { LocomotiveType } from '../Locomotives/LocomotiveItem';
import { LocoCategoryType } from '../LocoCategories/LocoCategoryItem';



type UpdateDriverDto = {
    firstName: string;
    isVacation: boolean;
    locomotiveId: number;
    categoriesIds: number[];
}

type DriverDto = {
    id: number;
    locoCategories: Record<string, string>;
} & Omit<UpdateDriverDto, 'categoriesIds'>;
function DriverPage() {

    const id = useParams().id;
    const navigate = useNavigate();

    const [name, setName] = useState<string>('');
    const [isVacation, setIsVacation] = useState<boolean>(false);
    const [locoId, setLocoId] = useState<number | null>(null);

    const [locomotives, setLocomotives] = useState<LocomotiveType[] | null>(null);

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsloading] = useState<boolean>(false);

    const [optionsCategories, setOptionsCategories] = useState<OptionType[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<OptionType[]>([]);


    const UpdateDriver = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")
            if (!locoId) throw new Error("Field locomotive can't be empty")
            if (!selectedCategories.length) throw new Error("Driver can't hasn't locomotive categories ")

            const dto: UpdateDriverDto = {
                firstName: name,
                isVacation,
                locomotiveId: locoId,
                categoriesIds: selectedCategories.map(category => +category.value)
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Drivers}/${id}`, {
                method: "PUT",
                body: JSON.stringify(dto),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })

            if (!response.ok) {
                const data = await response.json();
                setError(data)
                return;
            }
            navigate('/drivers')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    const GetLocoCategories = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`)
        return response;
    }
    const GetLocomotives = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}`)
        return response;
    }

    const GetDriverById = async () => {
        const response = await fetch(`${ApiUrl.Main}${ApiUrl.Drivers}/${id}`)
        return response;
    }


    useEffect(() => {
        async function GetData() {
            setIsloading(true)
            try {
                const response = await Promise.all([GetLocoCategories(), GetLocomotives(), GetDriverById()])

                if (!response[0].ok) {
                    setError(`error with request for locoCategories status code ${response[0].status}`)
                }
                if (!response[1].ok) {
                    setError(prev => prev + `error with request for locomotives status code ${response[1].status}`)
                }
                if (!response[2].ok) {
                    setError(prev => prev + `error with request for driver status code ${response[2].status}`)
                    return;
                }

                const locoCategoriesData: LocoCategoryType[] = await response[0].json()
                const locomotivesData: LocomotiveType[] = await response[1].json()
                const driverData: DriverDto = await response[2].json()


                if (locomotivesData.length) {
                    setLocomotives(locomotivesData)
                }
                if (driverData) {
                    setName(driverData.firstName)
                    setIsVacation(driverData.isVacation)
                    setLocoId(driverData.locomotiveId)
                }

                if (locoCategoriesData.length) {
                    const options: OptionType[] = []
                    const selectedOptions: OptionType[] = []

                    locoCategoriesData.forEach(category => options.push({ label: category.categoryName, value: String(category.id) }))
                    setOptionsCategories(options)

                    const keys = Object.keys(driverData.locoCategories)
                    const values = Object.values(driverData.locoCategories)

                    keys.forEach((key, index) => {
                        const opt = options.find(opt => opt.label === values[index])
                        if (opt) selectedOptions.push(opt)
                    })
                    setSelectedCategories(selectedOptions)
                }

            } catch (error) {
                setError(error.message)
            } finally {
                setIsloading(false);
            }
        }

        GetData()
    }, [])
    return (
        <div style={{ padding: '5px 10px' }}>
            <h2 style={{ textAlign: "center", margin: '10px 0' }}>Update Driver</h2>
            {isLoading ? (
                <h2>Loading...</h2>
            ) : (
                <>
                    <div>
                        <label htmlFor={'#name'}>Name:</label>
                        <input id='name' placeholder={'enter here...'} value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor={'#vacation'}>Is vacation:</label>
                        <input id='vacation' placeholder={'enter here...'} type={"checkbox"} checked={isVacation} onChange={() => setIsVacation(prev => !prev)} />
                    </div>
                    <div>
                        <label htmlFor={'#locomotives'}>Locomotives:</label>
                        <select id='locomotives' name='locomotives' value={locoId ?? "-----"} onChange={(e) => setLocoId(+e.target.value)}>
                            {!locomotives?.length ? (
                                <option value="-----" />
                            ) : (
                                locomotives.map((locomotive) => <option
                                    key={locomotive.id}
                                    value={locomotive.id}
                                >
                                    {locomotive.name}
                                </option>)
                            )}
                        </select>
                    </div>
                    <div style={{ margin: '10px 0' }}>

                        <label htmlFor={'#locomotives'}>Locomotive categories:</label>
                        <MultiSelect
                            options={optionsCategories}
                            value={selectedCategories}
                            onChange={setSelectedCategories}
                            labelledBy="Locomotive categories"
                        />
                    </div>
                    <Button text='Update' onClick={UpdateDriver} />
                </>
            )}

            <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                {error && error}
            </h3>
        </div>
    );
}

export { DriverPage };