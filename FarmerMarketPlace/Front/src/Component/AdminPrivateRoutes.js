import { useState } from 'react';
import { Navigate, Outlet } from 'react-router-dom'

const useAuth = () => {
    const user = JSON.parse(localStorage.getItem('user-token'))
    
    if (user.isadmin == true || user?.isAdmin == 1) {
        return true
    } else {
        return false
    }
}

const AdminProtectedRoutes = () => {


    const auth = useAuth()
// debugger;

    return auth ? <Outlet/>: <Navigate to="/"/>
}


export default AdminProtectedRoutes;;