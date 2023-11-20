import React, { useState } from 'react';
import { Button } from '../../utils/Button';
import { ApiUrl } from '../../utils/ApiUrl';
import { useNavigate } from 'react-router-dom';

type CreateLocoDto = {
    categoryName: string;
}

function CreateLocoCategory() {

    const [name, setName] = useState<string>('');
    const [error, setError] = useState<string | null>(null);

    const navigate = useNavigate();
    const CreateNewCategory = async () => {
        try {
            if (!name.length) throw new Error("Name can't be empty")

            const dto: CreateLocoDto = {
                categoryName: name
            }

            const response = await fetch(`${ApiUrl.Main}${ApiUrl.LocomotiveCategories}`, {
                method: "POST",
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

    return (
        <div style={{ padding: '5px 10px' }}>
            <h2 style={{ textAlign: "center", margin: '10px 0' }}>Create new locomotive category</h2>
            <div>
                <label htmlFor={'#name'}>Name:</label>
                <input id='name' placeholder={'enter here...'} onChange={(e) => setName(e.target.value)} />
            </div>
            <Button text='Create' onClick={CreateNewCategory} />

            <h3 style={{ color: 'red', fontSize: '24px', margin: '10px 0 0 0' }}>
                {error && error}
            </h3>
        </div>
    );
}

export {CreateLocoCategory};