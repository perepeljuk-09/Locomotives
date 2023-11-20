import React, { useEffect, useState } from 'react';
import { ApiUrl } from '../../utils/ApiUrl';
import { LocoCategoryItem, LocoCategoryType } from './LocoCategoryItem';
import { Button } from '../../utils/Button';
import { useNavigate } from 'react-router-dom';

function LocoCategories() {

    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [categories, setCategories] = useState<LocoCategoryType[] | null>(null);

    const navigate = useNavigate();
    useEffect(() => {
        GetLocoCategories()
    }, [])

    const GetLocoCategories = async () => {
        setIsLoading(true);
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`)

            if (!response.ok) {
                setError("Has been error")
            }
            const data: LocoCategoryType[] = await response.json()
            setCategories(data)
        } catch (error) {
            setError(error.message)
        } finally {
            setIsLoading(false)
        }
    }

    const DeleteCategory = async (id: number) => {
        try {
            const response = await fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}/${id}`, {
                method: "DELETE"
            })

            if (!response.ok) {
                console.error("error")
            }

            const data: boolean = await response.json();

            if (data) {
                setCategories(prev => prev!.filter(category => category.id !== id))
            }

        } catch (error) {
            console.error("error for delete")
        } finally {

        }
    }

    const AddNewCategory = () => navigate('/lococategories/create')
    return (
        <div>
            <div style={{ display: 'flex', gap: '35px' }}>
                <h2>Loco categories page</h2>
                <Button text="Add new +" onClick={AddNewCategory} />
            </div>
            {
                isLoading ? <p>Loading...</p>
                    : error ? <p>{error}</p>
                        : !categories?.length ? <p>Hasn't data about drivers</p>
                            :
                            categories.map(category => {
                                return <LocoCategoryItem key={category.id} {...category} deleteCategory={DeleteCategory} />
                            })}
        </div>
    );
}

export { LocoCategories };