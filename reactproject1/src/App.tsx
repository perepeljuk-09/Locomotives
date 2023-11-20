import { Route, Routes } from 'react-router-dom';
import './App.css'
import Layout from './components/Layout';
import Main from './components/Main';
import Drivers from './components/Driver/Drivers';
import Depots from './components/Depot/Depots';
import { Locomotives } from './components/Locomotives/Locomotives';
import { LocoCategories } from './components/LocoCategories/LocoCategories';
import { CreateLocoCategory } from './components/LocoCategories/CreateLocoCategory';
import { CreateDepot } from './components/Depot/CreateDepot';
import { CreateDriver } from './components/Driver/CreateDriver';
import { CreateLocomotive } from './components/Locomotives/CreateLocomotive';
import { LocoCategoryPage } from './components/LocoCategories/LocoCategoryPage';
import { DepotPage } from './components/Depot/DepotPage';
import { DriverPage } from './components/Driver/DriverPage';
import { LocomotivePage } from './components/Locomotives/LocomotivePage';

function App() {


    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Main />} />
                <Route path="/drivers" element={<Drivers />} />
                <Route path="/drivers/create" element={<CreateDriver />} />
                <Route path="/drivers/:id" element={<DriverPage />} />
                <Route path="/locomotives" element={<Locomotives />} />
                <Route path="/locomotives/create" element={<CreateLocomotive />} />
                <Route path="/locomotives/:id" element={<LocomotivePage />} />
                <Route path="/depots" element={<Depots />} />
                <Route path="/depots/create" element={<CreateDepot />} />
                <Route path="/depots/:id" element={<DepotPage />} />
                <Route path="/lococategories" element={<LocoCategories />} /> 
                <Route path="/lococategories/create" element={<CreateLocoCategory />} /> 
                <Route path="/lococategories/:id" element={<LocoCategoryPage />} /> 
            </Routes>
        </Layout>
    )
}

export default App
