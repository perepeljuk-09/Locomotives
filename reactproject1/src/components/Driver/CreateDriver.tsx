import React, { useEffect, useState } from 'react';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';
import { useNavigate } from 'react-router-dom';
import { LocoCategoryType } from '../LocoCategories/LocoCategoryItem';
import { LocomotiveType } from '../Locomotives/LocomotiveItem';
import { MultiSelect } from 'react-multi-select-component';

type CreateDriverDto = {
    firstName: string;
    locomotiveId: number;
    categoriesIds: number[];
}

export type OptionType = { label: string, value: string }
function CreateDriver() {

    const [name, setName] = useState<string>('');
    const [locoId, setLocoId] = useState<number | null>(null);


    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const [locomotives, setLocomotives] = useState<LocomotiveType[] | null>(null);

    const [optionsCategories, setOptionsCategories] = useState<OptionType[]>([]);
    const [selectedCategories, setSelectedCategories] = useState<OptionType[]>([]);



    const GetLocoCategories = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`)
        return response;
    }
    const GetLocomotives = () => {
        const response = fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}`)
        return response;
    }

    const navigate = useNavigate();
    const CreateNewDriver = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")
            if (!locoId) throw new Error("locomotive can't be empty, choose locomotive")
            if (!selectedCategories.length) throw new Error("driver must be have anyone category")

            const dto: CreateDriverDto = {
                firstName: name,
                locomotiveId: locoId,
                categoriesIds: selectedCategories.map(category => +category.value)
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Drivers}`, {
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
            navigate('/drivers')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    useEffect(() => {
        async function GetData() {
            setIsLoading(true)
            try {
                const response = await Promise.all([GetLocoCategories(), GetLocomotives()])

                if (!response[0].ok) {
                    setError(`error with request for locoCategories status code ${response[0].status}`)
                }
                if (!response[1].ok) {
                    setError(`error with request for locomotives status code ${response[1].status}`)
                }

                const locoCategoriesData: LocoCategoryType[] = await response[0].json()
                const locomotivesData: LocomotiveType[] = await response[1].json()
                setLocomotives(locomotivesData)
                setLocoId(locomotivesData[1].id)

                const options: OptionType[] = []

                locoCategoriesData.forEach(category => options.push({ label: category.categoryName, value: String(category.id) }))
                setOptionsCategories(options)
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
            <h2 style={{ textAlign: "center", margin: '10px 0' }}>Create new Driver</h2>
            {isLoading ? (
                <h2>Loading...</h2>
            ) : (
                <>
                    <div>
                            <label htmlFor={'#name'}>Name:</label>
                            <input id='name' placeholder={'enter here...'} value={name} onChange={(e) => setName(e.target.value)} />
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
                        <MultiSelect
                            options={optionsCategories}
                            value={selectedCategories}
                            onChange={setSelectedCategories}
                            labelledBy="Locomotive categories"
                        />
                    </div>
                    <Button text='Create' onClick={CreateNewDriver} />
                </>
            )}

            <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                {error && error}
            </h3>
        </div>
    );
}

export { CreateDriver };