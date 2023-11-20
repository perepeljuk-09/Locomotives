import React, { useEffect, useState } from 'react';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';
import { useNavigate } from 'react-router-dom';
import { LocoCategoryType } from '../LocoCategories/LocoCategoryItem';
import { DepotType } from '../Depot/DepotItem';

type CreateLocomotiveDto = {
    name: string;
    releaseDate: string;
    depotId: number | null;
    categoryId: number;
}

function CreateLocomotive() {

    const [name, setName] = useState<string>('');
    const [releaseDate, setReleaseDate] = useState<string>(new Date().toISOString().split('T')[0]);
    const [depotId, setDepotId] = useState<number | null>(null);
    const [categoryId, setCategoryId] = useState<number | null>(null);

    const [depots, setDepots] = useState<DepotType[] | null>(null);
    const [locoCategories, setLocoCategories] = useState<LocoCategoryType[] | null>(null);

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);



    const GetLocoCategories = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`)
        return response;
    }
    const GetDepots = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.Depots}`)
        return response;
    }

    const navigate = useNavigate();
    const CreateNewLocomotive = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")
            if (!releaseDate) throw new Error("releaseDate can't be empty, enter releaseDate")
            if (!depotId) throw new Error("Locomotive must be have anyone depot")
            if (!categoryId) throw new Error("Locomotive must be have anyone category")

            const dto: CreateLocomotiveDto = {
                name,
                depotId,
                categoryId,
                releaseDate
            }
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}`, {
                method: "POST",
                body: JSON.stringify(dto),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })

            const data = await response.json()
            if (!response.ok) {
                setError(`was been error for request to create status code ${response.status}, ${data}`)
                return;
            }
            navigate('/locomotives')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    useEffect(() => {
        async function GetData() {
            setIsLoading(true)
            try {
                const response = await Promise.all([GetLocoCategories(), GetDepots()])

                if (!response[0].ok) {
                    setError(`error with request for locoCategories status code ${response[0].status}`)
                }
                if (!response[1].ok) {
                    setError(`error with request for Depots status code ${response[1].status}`)
                }

                const locoCategoriesData: LocoCategoryType[] = await response[0].json()

                if (locoCategoriesData.length) {
                    setLocoCategories(locoCategoriesData)
                    setCategoryId(locoCategoriesData[0].id)
                }

                const depotsData: DepotType[] = await response[1].json()

                if (depotsData.length) {
                    setDepots(depotsData)
                    setDepotId(depotsData[0].id)
                }

            } catch (error) {
                setError(error.message)
            } finally {
                setIsLoading(false);
            }
        }

        GetData()
    }, [])

    return (
        <div style={{ padding: '5px 10px' }}>
            <h2 style={{ textAlign: "center", margin: '10px 0' }}>Create new Locomotive</h2>
            {isLoading ? (
                <h2>Loading...</h2>
            ) : (
                <>
                    <div>
                            <label htmlFor={'#name'}>Name:</label>
                            <input id='name' placeholder={'enter here...'} value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor={'#releasedate'}>Release date:</label>
                        <input id='releasedate' placeholder={'enter here...'} type='date' value={releaseDate} onChange={(e) => setReleaseDate(e.target.value)} />
                    </div>
                    <div>
                        <label htmlFor={'#depots'}>Depots:</label>
                        <select id='depots' name='depots' value={depotId ?? "-----"} onChange={(e) => setDepotId(+e.target.value)}>
                            {!depots?.length ? (
                                <option value="-----" />
                            ) : (
                                depots.map((depot) => <option
                                    key={depot.id}
                                    value={depot.id}
                                >
                                    {depot.name}
                                </option>)
                            )}
                        </select>
                    </div>
                    <div style={{ margin: '10px 0' }}>
                        <label htmlFor={'#locomotivescategories'}>Locomotives categories:</label>
                        <select id='locomotivescategories' name='locomotivescategories' value={categoryId ?? "-----"} onChange={(e) => setCategoryId(+e.target.value)}>
                            {!locoCategories?.length ? (
                                <option value="-----" />
                            ) : (
                                locoCategories.map((locaCategory) => <option
                                    key={locaCategory.id}
                                    value={locaCategory.id}
                                >
                                    {locaCategory.categoryName}
                                </option>
                                ))}
                        </select>
                    </div>
                    <Button text='Create' onClick={CreateNewLocomotive} />
                </>
            )}

            <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                {error && error}
            </h3>
        </div>
    );
}

export { CreateLocomotive };