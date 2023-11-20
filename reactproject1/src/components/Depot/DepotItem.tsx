import React from 'react';
import { useNavigate } from 'react-router-dom';

export type DepotType = {
    id: number,
    name: string,
    locomotives: Record<string, string>,
}

type DepotPropsType = {
    deleteDepot: (id: number) => void;
} & DepotType
function DepotItem({ id, name, locomotives, deleteDepot }: DepotPropsType) {

    const navigate = useNavigate()
    return (
        <li
            onClick={() => navigate(`/depots/${id}`)}
            style={{ cursor: 'pointer' }}
        >ID - {id}; Depot name - {name}; <span onClick={(e) => {
            e.stopPropagation()
            deleteDepot(id)
        }} style={{ color: 'red', fontSize: '25px' }}>&times;</span></li>
    );
}

export default DepotItem;