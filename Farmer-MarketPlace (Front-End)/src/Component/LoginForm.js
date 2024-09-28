import { useEffect, useState } from "react";
import userService from "../Services/user.services";
import { useNavigate } from "react-router-dom";
import toast, { Toaster } from "react-hot-toast";
import logo from "../imagess/logo.png";
import video1 from "../Video/video1.mp4";
import Footer from "./Footer";
import '../App.css';
import LogoBrand from "./LogoBrand";


function LoginForm() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  useEffect(() => {
    localStorage.clear();
  });
  const navigate = useNavigate();
  const loginUser = (e) => {
    e.preventDefault();

    var email = username;

    const user = { email, password };
    // console.log(user);
    userService
      .login(user)
      .then((respose) => {
        // console.log("User Login Credentials Send", respose.data);

        if (respose.data.length === 0) {
          toast.error("Invalid Credentials", {
            style: {
              borderRadius: "10px",
              background: "#333",
              color: "#fff",
            },
          });
        }

        localStorage.setItem("user-token", JSON.stringify(respose.data));

        var data = JSON.parse(localStorage.getItem("user-token"));
        if (data?.isAdmin === true || data?.isAdmin === 1) {
          console.log("admin");
          navigate("/admin");
        } else if (data?.isAdmin === false || data?.isAdmin === 0) {
          console.log("user");
          navigate("/user");
        }
      })
      .catch((error) => {
        console.log("Something Went Wrong", error);
      });
  };

  return (
    <div style={{
      height: "88vh",
      display: "flex",
      flexDirection: "column",
    }}>
      <LogoBrand/>
    <div className="login-container">
      <video autoPlay loop muted className="background-video">
        <source src={video1} type="video/mp4" />
        Your browser does not support the video tag.
      </video>
      <div className="login-box">
        <div className="login-content">
          <h1 className="fs-4 card-title fw-bold mb-4 text-center text-primary">
            Login
          </h1>
          <form onSubmit={(e) => loginUser(e)}>
            <div className="mb-3">
              <input
                id="email"
                type="email"
                className="form-control"
                name="email"
                required
                autoFocus
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="E-Mail Address"
              />
            </div>
            <div className="mb-3">
              <input
                id="password"
                type="password"
                className="form-control"
                name="password"
                required
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
              />
            </div>
            <div className="d-grid">
              <button type="submit" className="btn btn-primary">
                Login
              </button>
            </div>
          </form>
          <div className="text-center mt-3">
            Don't have an account?{" "}
            <a href="/register" className="text-primary">
              Create an Account
            </a>
          </div>
        </div>
      </div>

      <Footer />
    </div>
    </div>
  );
}

export default LoginForm;
