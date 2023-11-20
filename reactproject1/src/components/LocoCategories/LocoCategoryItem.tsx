import React from 'react';
import { useNavigate } from 'react-router-dom';

export type LocoCategoryType = {
    id: number,
    categoryName: string,
}

type LocoCategoryPropsType = {
    deleteCategory: (id: number) => void;
} & LocoCategoryType

function LocoCategoryItem({ id, categoryName, deleteCategory }: LocoCategoryPropsType) {

    const navigate = useNavigate()
    return (
        <li
            onClick={() => navigate(`/lococategories/${id}`)}
            style={{ cursor: 'pointer' }}
        >ID - {id}; Category - {categoryName} === <span onClick={(e) => {
            e.stopPropagation()
            deleteCategory(id)
        }} style={{ color: 'red', fontSize: '25px' }}>&times;</span></li>
    );
}

export { LocoCategoryItem };