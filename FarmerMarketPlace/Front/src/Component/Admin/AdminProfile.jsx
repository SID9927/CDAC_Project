import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import AdminNavBar from "./AdminNavBar";
import toast from "react-hot-toast";
import adminServices from "../../Services/admin.services";
import Footer from "../Footer";
import '../../App.css';

function AdminProfile() {
  const [isDisabled, setIsDisabled] = useState(true);
  const [userId, setUserId] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [phoneNo, setPhoneNo] = useState("");
  const [address, setAddress] = useState("");
  const [isadmin, setIsAdmin] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    init();
  }, []);

  const init = () => {
    var val = localStorage.getItem("user-token");
    var object = JSON.parse(val);
    setUserId(object.userId);
    adminServices
      .getSpecificUserDetails(object.userId)
      .then((response) => {
        console.log(response.data);
        setAddress(response.data.address);
        setPassword(response.data.password);
        setFirstname(response.data.firstname);
        setLastname(response.data.lastname);
        setPhoneNo(response.data.phoneNo);
        setEmail(response.data.email);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const updateProfile = () => {
    setIsDisabled(false);
  };

  const updateUser = () => {
    if (
      userId === "" ||
      email === "" ||
      password === "" ||
      firstname === "" ||
      lastname === "" ||
      phoneNo === "" ||
      address === "" ||
      isadmin === ""
    ) {
      console.log("Empty");
      toast.error("Something went wrong!", {
        style: {
          borderRadius: "10px",
          background: "#333",
          color: "#fff",
        },
      });
      return;
    }

    const data = {
      userId,
      email,
      password,
      firstname,
      lastname,
      phoneNo,
      address,
      isadmin,
    };
    adminServices
      .updateUser(userId, data)
      .then((response) => {
        console.log("User Updated", response.data);
        toast.success("Profile Updated! Log-In Again...", {
          style: {
            borderRadius: "10px",
            background: "#333",
            color: "#fff",
          },
        });
        setTimeout(() => {
          navigate("/");
        }, 2500);
      })
      .catch((error) => {
        console.log("Something went wrong", error);
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
    <div>
      <AdminNavBar />
      <div className="container h-100">
        <div className="row justify-content-sm-center h-100">
          <div className="col-xl-8 col-md-8 col-12 col-sm-12">
            <div className="card shadow-lg mt-5 custom-card">
              <div className="card-body px-5 pt-5">
                <h1 className="fs-4 card-title fw-bold mb-4 text-center text-primary">
                  ADMIN PROFILE
                </h1>
                <hr />

                <div className="row g-3">
                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="firstname">
                      First Name
                    </label>
                    <input
                      id="firstname"
                      type="text"
                      className="form-control custom-input"
                      name="firstname"
                      required
                      value={firstname}
                      disabled={isDisabled}
                      onChange={(e) => setFirstname(e.target.value)}
                    />
                  </div>

                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="lastname">
                      Last Name
                    </label>
                    <input
                      id="lastname"
                      type="text"
                      className="form-control custom-input"
                      name="lastname"
                      required
                      value={lastname}
                      disabled={isDisabled}
                      onChange={(e) => setLastname(e.target.value)}
                    />
                  </div>
                </div>

                <div className="row g-3">
                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="email">
                      E-Mail Address
                    </label>
                    <input
                      id="email"
                      type="email"
                      className="form-control custom-input"
                      name="email"
                      required
                      pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"
                      value={email}
                      disabled={isDisabled}
                      onChange={(e) => setEmail(e.target.value)}
                    />
                  </div>

                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="password">
                      Password
                    </label>
                    <input
                      id="password"
                      type="text"
                      className="form-control custom-input"
                      name="password"
                      required
                      value={password}
                      disabled={isDisabled}
                      onChange={(e) => setPassword(e.target.value)}
                    />
                  </div>
                </div>

                <div className="row g-3">
                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="phone">
                      Phone No.
                    </label>
                    <input
                      id="phone"
                      type="tel"
                      className="form-control custom-input"
                      name="phone"
                      required
                      pattern="[0-9]{10}"
                      value={phoneNo}
                      disabled={isDisabled}
                      onChange={(e) => setPhoneNo(e.target.value)}
                    />
                  </div>
                  <div className="col mb-3">
                    <label className="mb-2 text-muted" htmlFor="Address">
                      Address
                    </label>
                    <input
                      id="Address"
                      type="text"
                      className="form-control custom-input"
                      name="Address"
                      required
                      value={address}
                      disabled={isDisabled}
                      onChange={(e) => setAddress(e.target.value)}
                    />
                  </div>
                </div>
                <hr />

                <div className="align-items-center d-flex">
                  <button
                    type="submit"
                    className="btn btn-primary custom-btn"
                    onClick={() => navigate("/admin/")}
                  >
                    Go Back
                  </button>
                  {isDisabled ? (
                    <button
                      type="submit"
                      className="btn btn-success ms-auto custom-btn"
                      onClick={() => updateProfile()}
                    >
                      Update Profile
                    </button>
                  ) : (
                    <button
                      type="submit"
                      className="btn btn-success ms-auto custom-btn"
                      onClick={() => updateUser()}
                    >
                      Save Data
                    </button>
                  )}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default AdminProfile;
