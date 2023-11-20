//import React from 'react';
import s from '../styles/Navbar.module.css';
import { Link, useLocation } from "react-router-dom";


function Navbar() {

    const path = useLocation().pathname;

    const GetClassName = (chekedPath: string) => {
        return path === chekedPath ? s.navbar__link_active : s.navbar__link;
    }

    return (
        <nav className={s.navbar}>
            <ul className={s.navbar__menu}>
                <li><Link className={GetClassName('/')} to={'/'}>Main</Link></li>
                <li><Link className={GetClassName('/drivers')} to={'/drivers'}>Drivers</Link></li>
                <li><Link className={GetClassName('/locomotives')} to={'/locomotives'}>Locomotives</Link></li>
                <li><Link className={GetClassName('/depots')} to={'/depots'}>Depots</Link></li>
                <li><Link className={GetClassName('/lococategories')} to={'/lococategories'}>LocoCategories</Link></li>
          </ul>
      </nav>
  );
}

export default Navbar;