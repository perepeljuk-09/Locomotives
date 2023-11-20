import React from 'react';
import { useNavigate } from 'react-router-dom';

export type LocomotiveType = {
    id: number,
    name: string,
    releaseDate: string,
    depotId?: number,
    depotName?: string,
    drivers: Record<string, string>,
    locomotiveCategoryId: number,
    locomotiveCategoryName: string
}

type LocomotivePropsType = {
    deleteLocomotive: (id: number) => void;
} & LocomotiveType

function LocomotiveItem({ id, name, releaseDate, depotId, depotName, locomotiveCategoryId, locomotiveCategoryName, deleteLocomotive }: LocomotivePropsType) {

    const navigate = useNavigate()
    return (
        <li
            onClick={() => navigate(`/locomotives/${id}`)}
            style={{ cursor: 'pointer' }}
        >ID - {id}; Name - {name}; Depot name - {depotName ?? "absent"}; Category of locomotive - {locomotiveCategoryName}; === <span onClick={(e) => {
            e.stopPropagation()
            deleteLocomotive(id)
        }} style={{ color: 'red', fontSize: '25px' }}>&times;</span></li>
    );
}

export { LocomotiveItem };