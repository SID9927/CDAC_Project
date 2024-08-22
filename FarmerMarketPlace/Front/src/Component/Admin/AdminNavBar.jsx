import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import logo from '../../imagess/logo.png'
import "../../App.css"

function AdminNavBar() {
    const [loading, setLoading] = useState(false)

    const [object, setObject] = useState('')
    const navigate = useNavigate()
    useEffect(() => {
        init();
    }, []);

    const init = () => {
        var val = localStorage.getItem('user-token');
        var object = JSON.parse(val);
        setObject(object)
    }

    const logout = () => {
        setLoading(true)

        setTimeout(() => {
            setLoading(false)
            localStorage.removeItem('user-token');
            console.log('Logged Out')
            navigate('/')
        }, 3500);

    }
    return (
        <div>
            {loading ? "" :
                <nav className="navbar navbar-expand-lg py-0" style={{ backgroundColor: '#52b788' }}>
                    <div className="container">
                        <a href="/admin" className="navbar-brand">
                            <img src={logo} style={{ width: '70px', height: 'auto' }} alt="Logo" />
                        </a>
                        <button type="button" className="navbar-toggler" data-bs-toggle="collapse" data-bs-target="#navbarCollapse">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarCollapse">

                            <div className="navbar-nav ms-auto">
                                    <a className="nav-item nav-link active fw-bolder myHover ms-2" href="/admin/profile">My Profile </a>
                            </div>
                            <div className="navbar-nav ">
                                <a className="nav-item nav-link active fw-bolder myHover ms-2" href="#" onClick={logout}>Logout</a>
                            </div>
                        </div>
                    </div>
                </nav>
            }
            {loading ? navigate('/pageload') : ""}
        </div>
    );
}

export default AdminNavBar;