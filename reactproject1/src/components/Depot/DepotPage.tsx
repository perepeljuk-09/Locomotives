import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';



type UpdateDepotDto = {
    name: string;
}

type DepotDto = {
    id: number;

} & UpdateDepotDto;
function DepotPage() {

    const id = useParams().id;
    const navigate = useNavigate();

    const [name, setName] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsloading] = useState<boolean>(false);



    const UpdateDepot = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")

            const dto: UpdateDepotDto = {
                name
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Depots}/${id}`, {
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
                console.log("data >>>", data)
                return;
            }
            navigate('/depots')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    const GetDepotById = async () => {
        try {
            setIsloading(true)

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Depots}/${id}`)

            if (!response.ok) {
                setError(`was error with request status code ${response.status}`)
                return;
            }

            const data: DepotDto = await response.json()

            setName(data.name)

        } catch (error) {
            setError(error.message)
        } finally {
            setIsloading(false)
        }
    }
    useEffect(() => {
        GetDepotById()
    }, [])
    return (
        <>
            {
                isLoading ? (
                    <div>Loading...</div>
                ) : (
                    <div style={{ padding: '5px 10px' }}>
                        <Button text='back' onClick={() => navigate('/depots')} />
                        <h2 style={{ textAlign: "center", margin: '10px 0' }}>Update depot</h2>
                        <div>
                            <label htmlFor={'#name'}>Depot name:</label>
                            <input id='name' placeholder={'enter here...'} value={name} onChange={(e) => setName(e.target.value)} />
                            </div>
                            <Button text='Update' onClick={UpdateDepot} />

                        <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                            {error && error}
                        </h3>
                    </div >
                )
            }
        </>
    );
}

export { DepotPage };