import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';



type UpdateLocoDto = {
    categoryName: string;
}

type LocoDto = {
    id: number;

} & UpdateLocoDto;
function LocoCategoryPage() {

    const id = useParams().id;
    const navigate = useNavigate();

    const [name, setName] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [isLoading, setIsloading] = useState<boolean>(false);



    const UpdateCategory = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")

            const dto: UpdateLocoDto = {
                categoryName: name
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}/${id}`, {
                method: "PUT",
                body: JSON.stringify(dto),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            })

            if (!response.ok) {
                setError("was been error for request to create")
                const data = await response.json();
                console.log("data >>>", data)
                return;
            }
            navigate('/lococategories')
        } catch (error) {
            setError(error.message)
        } finally {

        }
    }

    const GetLocoCategoryById = async () => {
        try {
            setIsloading(true)

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}/${id}`)

            if (!response.ok) {
                setError(`was error with request status code ${response.status}`)
                return;
            }

            const data: LocoDto = await response.json()

            setName(data.categoryName)

        } catch (error) {
            setError(error.message)
        } finally {
            setIsloading(false)
        }
    }
    useEffect(() => {
        GetLocoCategoryById()
    }, [])
    return (
        <>
            {
                isLoading ? (
                    <div>Loading...</div>
                ) : (
                    <div style={{ padding: '5px 10px' }}>
                        <Button text='back' onClick={() => navigate('/lococategories')} />
                        <h2 style={{ textAlign: "center", margin: '10px 0' }}>Update locomotive category</h2>
                        <div>
                            <label htmlFor={'#name'}>Name:</label>
                            <input id='name' placeholder={'enter here...'} value={name} onChange={(e) => setName(e.target.value)} />
                        </div>
                        <Button text='Update' onClick={UpdateCategory} />

                        <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                            {error && error}
                        </h3>
                    </div >
                )
            }
        </>
    );
}

export { LocoCategoryPage };