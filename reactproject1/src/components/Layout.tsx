import { ReactNode } from 'react';
import s from '../styles/Layout.module.css';
import Navbar from './Navbar';

type LayoutProps = {
    children: ReactNode;
}
function Layout({ children }: LayoutProps) {
    return (
        <div className={s.layout}>
            <Navbar/>
            {children}
      </div>
  );
}

export default Layout;