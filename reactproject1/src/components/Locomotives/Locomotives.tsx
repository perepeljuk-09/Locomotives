import React, { useEffect, useState } from 'react';
import { ApiUrl } from '../../utils/ApiUrl';
import { LocomotiveItem, LocomotiveType } from './LocomotiveItem';
import { Button } from '../../utils/Button';
import { useNavigate } from 'react-router-dom';



function Locomotives() {

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [locomotives, setlocomotives] = useState<LocomotiveType[] | null>(null);

    const navigate = useNavigate();
    useEffect(() => {
        Getlocomotives()
    }, [])

    const Getlocomotives = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}`)

            if (!response.ok) {
                setError("Has been error")
            }
            const data: LocomotiveType[] = await response.json()
            setlocomotives(data)
        } catch (error) {
            setError(error.message)
        } finally {
            setIsLoading(false)
        }

    }

    const DeleteLocomotive = async (id: number) => {
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.Locomotives}/${id}`, {
                method: "DELETE"
            })

            if (!response.ok) {
                console.error("error")
            }

            const data: boolean = await response.json();

            if (data) {
                setlocomotives(prev => prev!.filter(locomotive => locomotive.id !== id))
            }

        } catch (error) {
            console.error("error for delete")
        } finally {

        }
    }

    const AddNewLocomotive = () => navigate('/locomotives/create')
    return (
        <div>
            <div style={{ display: 'flex', gap: '35px' }}>
                <h2>Locomotives page</h2>
                <Button text="Add new +" onClick={AddNewLocomotive} />
            </div>
            {
                isLoading ? <p>Loading...</p>
                    : error ? <p>{error}</p>
                        : !locomotives?.length ? <p>Hasn't data about Locomotives</p>
                            :
                            locomotives.map(locomotive => {
                                return <LocomotiveItem key={locomotive.id} {...locomotive} deleteLocomotive={DeleteLocomotive} />
                            })}
        </div>
    );
}

export { Locomotives };