import { BrowserRouter as Router, Route, Routes as Rutas, Navigate } from 'react-router-dom';
import Login from '../Components/Login';
import { Home } from '../Components/Home';
//import { Contexto } from '../App'
//import { Usuario } from '../Interfaces/Interfaces';


export default function Routes() {

    const id= sessionStorage.getItem("Id")
    //console.log(id)

    return ( 
        <div >

            <Router>
                <Rutas>
                {!id ? <Route path="*" element={<Navigate to="/" />} /> : null}

                    <Route path="/home" element={!id ? <Navigate to="/" /> : <Home />} />
                    <Route path="/" element={id ? <Navigate to="/home" /> : <Login />} />
 
                </Rutas>
            </Router>

        </div>
    );
}


