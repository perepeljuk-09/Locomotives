import React, { useEffect, useState } from 'react';
import DriverItem, { DriverType } from './DriverItem';
import { ApiUrl } from '../../utils/ApiUrl';
import { Button } from '../../utils/Button';
import { useNavigate } from 'react-router-dom';



function Drivers() {

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [drivers, setDrivers] = useState<DriverType[] | null>(null);

    const navigate = useNavigate();
    useEffect(() => {
        GetDrivers()
    }, [])

    const GetDrivers = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Drivers}`)

            if (!response.ok) {
                setError("Has been error")
            }
            const data: DriverType[] = await response.json()
            setDrivers(data)
        } catch (error) {
            setError(error.message)
        } finally {
            setIsLoading(false)
        }

    }

    const DeleteDriver = async (id: number) => {
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Drivers}/${id}`, {
                method: "DELETE"
            })

            if (!response.ok) {
                console.error("error")
            }

            const data: boolean = await response.json();

            if (data) {
                setDrivers(prev => prev!.filter(driver => driver.id !== id))
            }

        } catch (error) {
            console.error("error for delete")
        } finally {

        }
    }

    const AddNewDriver = () => navigate('/drivers/create')
    return (
        <div>
            <div style={{ display: 'flex', gap: '35px' }}>
                <h2>Drivers page</h2>
                <Button text="Add new +" onClick={AddNewDriver} />
            </div>
            {
                isLoading ? <p>Loading...</p>
                    : error ? <p>{error}</p>
                        : !drivers?.length ? <p>Hasn't data about drivers</p>
                            :
                            drivers.map(driver => {
                                return <DriverItem key={driver.id} {...driver} deleteDriver={DeleteDriver} />
                            })}
        </div>
    );
}

export default Drivers;