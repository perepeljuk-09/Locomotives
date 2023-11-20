import React, { useEffect, useState } from 'react';
import DepotItem, { DepotType } from './DepotItem';
import { ApiUrl } from '../../utils/ApiUrl';
import { Button } from '../../utils/Button';
import { useNavigate } from 'react-router-dom';

function Depots() {

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [depots, setDepots] = useState<DepotType[] | null>(null);

    const navigate = useNavigate();
    useEffect(() => {
        GetDepots()
    }, [])

    const GetDepots = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Depots}`)

            if (!response.ok) {
                setError("Has been error")
            }
            const data: DepotType[] = await response.json()
            setDepots(data)
        } catch (error) {
            setError(error.message)
        } finally {
            setIsLoading(false)
        }

    }

    const DeleteDepot = async (id: number) => {
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Depots}/${id}`, {
                method: "DELETE"
            })

            if (!response.ok) {
                console.error("error")
            }

            const data: boolean = await response.json();

            if (data) {
                setDepots(prev => prev!.filter(depot => depot.id !== id))
            }

        } catch (error) {
            console.error("error for delete")
        } finally {

        }
    }

    const AddNewDepot = () => navigate('/depots/create')
    return (
        <div>
            <div style={{ display: 'flex', gap: '35px' }}>
                <h2>Depots page</h2>
                <Button text="Add new +" onClick={AddNewDepot} />
            </div>
            {
                isLoading ? <p>Loading...</p>
                    : error ? <p>{error}</p>
                        : !depots?.length ? <p>Hasn't data about depots</p>
                            :
                            depots.map(depot => {
                                return <DepotItem key={depot.id} {...depot} deleteDepot={DeleteDepot} />
                            })
            }
        </div>
    );
}

export default Depots;