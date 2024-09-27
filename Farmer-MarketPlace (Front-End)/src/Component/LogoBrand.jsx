import React from "react";
import logo from '../imagess/logo.png';
import '../App.css';

function LogoBrand() {
  return (
    <nav className="Login-navbar navbar-expand-lg py-2 fixed-top">
      <div className="container">
        <div className="Login-navbar-brand mx-auto">
          <img
            src={logo}
            style={{ width: "70px", height: "auto" }}
            alt="Logo"
          />
        </div>
      </div>
    </nav>
  );
}

export default LogoBrand;
