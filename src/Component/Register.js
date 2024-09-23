import { useState } from "react";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import logo from "../imagess/logo.png";
import Footer from "./Footer";
import "../App.css";
import userServices from "../Services/user.services";
import LogoBrand from "./LogoBrand";

function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [phoneNo, setPhoneNo] = useState("");
  const [address, setAddress] = useState("");
  const [isAdmin, setIsAdmin] = useState(Boolean);
  const [agreeTerms, setAgreeTerms] = useState(false);
  const navigate = useNavigate();

  const registerUser = (e) => {
    e.preventDefault();
    if (!agreeTerms) {
      toast.error("Please agree to the terms and conditions");
      return;
    }
    setIsAdmin("false");
    const user = {
      email,
      password,
      firstname,
      lastname,
      phoneNo,
      address,
      isAdmin,
    };
    console.log(user);

    userServices
      .register(user)
      .then((respose) => {
        console.log("User Registered", respose.data);
        toast.success("User Registered! Auto-Redirecting....", {
          style: {
            borderRadius: "10px",
            background: "#333",
            color: "#fff",
          },
        });
        setTimeout(() => {
          console.log("User Registered");
          navigate("/");
        }, 2500);
      })
      .catch((error) => {
        console.log("Something Went Wrong", error);
        toast.error("Something went wrong!", {
          style: {
            borderRadius: "10px",
            background: "#333",
            color: "#fff",
          },
        });
      });
  };

  return (
    <div
      style={{
        height: "88vh",
        display: "flex",
        flexDirection: "column",
      }}
    >
      <LogoBrand />
      <div
        style={{
          backgroundImage:
            "url('https://images.pexels.com/photos/2152958/pexels-photo-2152958.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1')",
          backgroundSize: "cover",
          backgroundPosition: "center",
          display: "flex",
          flex: 1,
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <div className="container">
          <div className="row">
            <div className="col-xl-2"></div>
            <div className="col-xl-8">
              <div
                className="card shadow-lg border "
                style={{
                  backgroundColor: "rgba(255, 255, 255, 0.8)",
                  backdropFilter: "blur(1px)",
                }}
              >
                <div className="card-body px-5 pt-5">
                  <h1 className="fs-4 card-title fw-bold mb-1 text-center text-primary">
                    Sign Up
                  </h1>
                  <hr />
                  <form onSubmit={(e) => registerUser(e)}>
                    <div className="row g-3 mt-0">
                      <div className="col-xl-6 col-md-6 col-12 col-sm-12 mb-1">
                        <input
                          id="firstname"
                          type="text"
                          className="form-control"
                          name="firstname"
                          required
                          autoFocus
                          value={firstname}
                          onChange={(e) => setFirstname(e.target.value)}
                          placeholder="First Name"
                        />
                      </div>

                      <div className="col-xl-6 col-md-6 col-12 col-sm-12 mb-1">
                        <input
                          id="lastname"
                          type="text"
                          className="form-control"
                          name="lastname"
                          required
                          value={lastname}
                          onChange={(e) => setLastname(e.target.value)}
                          placeholder="Last Name"
                        />
                      </div>
                    </div>

                    <div className="row g-3 mt-0">
                      <div className="col-xl-6 col-12 col-md-6 col-sm-12 mb-1">
                        <input
                          id="email"
                          type="email"
                          className="form-control"
                          name="email"
                          required
                          pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"
                          value={email}
                          onChange={(e) => setEmail(e.target.value)}
                          placeholder="E-Mail Address"
                        />
                      </div>

                      <div className="col-xl-6 col-12 col-md-6 col-sm-12 mb-1">
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
                    </div>

                    <div className="row g-3 mt-0">
                      <div className="col-xl-6 col-12 col-md-6 col-sm-12 mb-2">
                        <input
                          id="phone"
                          type="tel"
                          className="form-control"
                          name="phone"
                          required
                          pattern="[0-9]{10}"
                          value={phoneNo}
                          onChange={(e) => setPhoneNo(e.target.value)}
                          placeholder="Phone No"
                        />
                      </div>
                      <div className="col-xl-6 col-12 col-md-6 col-sm-12 mb-1">
                        <input
                          id="Address"
                          type="test"
                          className="form-control"
                          name="Address"
                          required
                          value={address}
                          onChange={(e) => setAddress(e.target.value)}
                          placeholder="Address"
                        />
                      </div>
                    </div>

                    <div className="form-check mb-3">
                      <input
                        type="checkbox"
                        className="form-check-input"
                        id="agreeTerms"
                        checked={agreeTerms}
                        onChange={(e) => setAgreeTerms(e.target.checked)}
                        required
                      />
                      <label className="form-check-label" htmlFor="agreeTerms">
                        Agreed with our terms and condition.
                      </label>
                    </div>

                    <div className="align-items-center d-flex">
                      <button
                        type="submit"
                        className="btn btn-primary form-control"
                      >
                        Sign Up
                      </button>
                    </div>
                  </form>
                </div>
                <div className="card-footer py-1 border-0">
                  <div className="text-center mb-4">
                    Already have an account?{" "}
                    <a href="/" className="text-primary">
                      Login
                    </a>
                  </div>
                </div>
              </div>
            </div>
            <div className="col-xl-2"></div>
          </div>
        </div>
        
      </div>
      <Footer />
    </div>
  );
}

export default Register;
