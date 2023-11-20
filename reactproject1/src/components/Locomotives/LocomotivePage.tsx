import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';
import { LocoCategoryType } from '../LocoCategories/LocoCategoryItem';
import { DepotType } from '../Depot/DepotItem';



type UpdateLocomotiveDto = {
    name: string;
    releaseDate: string;
    depotId: number;
    categoryId: number;
}

type LocomotiveDto = {
    id: number;
    depotName: string;
    drivers: Record<string, string>;
    locomotiveCategoryId: number;
    locomotiveCategoryName: string;
} & Omit<UpdateLocomotiveDto,'categoryId'>;
function LocomotivePage() {

    const id = useParams().id;
    const navigate = useNavigate();

    const [name, setName] = useState<string>('');
    const [releaseDate, setReleaseDate] = useState<string | null>(null);
    const [depotId, setDepotId] = useState<number | null>(null);
    const [categoryId, setCategoryId] = useState<number | null>(null);

    const [depots, setDepots] = useState<DepotType[] | null>(null);
    const [locoCategories, setLocoCategories] = useState<LocoCategoryType[] | null>(null);


    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsloading] = useState<boolean>(false);


    const GetLocoCategories = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`)
        return response;
    }
    const GetDepots = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.Depots}`)
        return response;
    }

    const UpdateLocomotive = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")
            if (!categoryId) throw new Error("choose loco category")
            if (!depotId) throw new Error("choose depot")
            if (!releaseDate) throw new Error("choose releaseDate")

            const dto: UpdateLocomotiveDto = {
                name,
                categoryId,
                depotId,
                releaseDate,
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}/${id}`, {
                method: "PUT",
                body: JSON.stringify(dto),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })

            if (!response.ok) {
                setError("was been error for request to update")
                const data = await response.json();
                return;
            }
            navigate('/locomotives')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    const GetLocomotiveById = async () => {
        const response = await fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}/${id}`)
        return response;
    }

    useEffect(() => {
        async function GetData() {
            setIsloading(true)
            try {
                const response = await Promise.all([GetLocoCategories(), GetDepots(), GetLocomotiveById()])

                if (!response[0].ok) {
                    setError(`error with request for locoCategories status code ${response[0].status}`)
                }
                if (!response[1].ok) {
                    setError(`error with request for Depots status code ${response[1].status}`)
                }

                const locoCategoriesData: LocoCategoryType[] = await response[0].json()

                if (locoCategoriesData.length) {
                    setLocoCategories(locoCategoriesData)
                }

                const depotsData: DepotType[] = await response[1].json()

                if (depotsData.length) {
                    setDepots(depotsData)
                }


                const locomotiveDate: LocomotiveDto = await response[2].json()

                if (locomotiveDate) {
                    setName(locomotiveDate.name)
                    setReleaseDate(locomotiveDate.releaseDate)
                    setDepotId(locomotiveDate.depotId)
                    setCategoryId(locomotiveDate.locomotiveCategoryId)
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
                        <input id='releasedate' placeholder={'enter here...'} type='date' value={releaseDate ?? new Date().toISOString().split('T')[0]} onChange={(e) => setReleaseDate(e.target.value)} />
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
                    <Button text='Update' onClick={UpdateLocomotive} />
                </>
            )}

            <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                {error && error}
            </h3>
        </div>
    );
}

export { LocomotivePage };