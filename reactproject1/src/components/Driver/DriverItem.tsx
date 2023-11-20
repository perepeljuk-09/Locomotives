import React from 'react';
import { useNavigate } from 'react-router-dom';

export type DriverType = {
    id: number,
    firstName: string,
    isVacation: boolean,
    locomotiveId: number,
    locoCategories: Record<string, string>
}

type DriverPropsType = {
    deleteDriver: (id: number) => void;
} & DriverType

function DriverItem({ id, firstName, isVacation, locoCategories, locomotiveId, deleteDriver }: DriverPropsType) {

    const ctgid_1 = locoCategories["1"] ?? ""
    const ctgid_2 = locoCategories["2"] ?? ""
    const ctgid_3 = locoCategories["3"] ?? ""

    const navigate = useNavigate()
    return (
        <li
            onClick={() => navigate(`/drivers/${id}`)}
            style={{ cursor: 'pointer' }}
        >First name - {firstName}; Is vacation - {String(isVacation)}; Category for locomotive - {ctgid_1}{ctgid_2}{ctgid_3};LocomotiveId - {locomotiveId} === <span onClick={(e) => {
            e.stopPropagation()
            deleteDriver(id)
        }} style={{ color: 'red', fontSize: '25px' }}>&times;</span></li>
    );
}

export default DriverItem;