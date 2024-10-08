import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import farmerServices from "../../Services/farmer.services";
import toast, { Toaster } from "react-hot-toast";
import AdminNavBar from "./AdminNavBar";
import Footer from "../Footer";
import '../../App.css';

function AddNewFarmer() {
  const [email, setEmail] = useState("");
  const [firstname, setFirstname] = useState("");
  const [lastname, setLastname] = useState("");
  const [phoneNo, setPhoneNo] = useState("");
  const [address, setAddress] = useState("");
  const { farmerId } = useParams();
  const navigate = useNavigate();

  const addFarmer = (e) => {
    e.preventDefault();
    const farmer = { email, firstname, lastname, phoneNo, address };
    console.log(farmer);

    if (farmerId) {
      // update
      farmerServices
        .updateFarmer(farmer, farmerId)
        .then((response) => {
          console.log("Farmer data updated successfully", response.data);
          toast.success("Farmer Updated. Auto-Redirecting....", {
            style: {
              borderRadius: "10px",
              background: "#333",
              color: "#fff",
            },
          });
          setTimeout(() => {
            navigate("/admin/farmer");
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
    } else {
      // create
      farmerServices
        .addFarmer(farmer)
        .then((respose) => {
          console.log("Farmer Registered.", respose.data);
          toast.success("Farmer Added. Auto-Redirecting....", {
            style: {
              borderRadius: "10px",
              background: "#333",
              color: "#fff",
            },
          });
          setTimeout(() => {
            navigate("/admin/farmer");
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
    }
  };

  useEffect(() => {
    if (farmerId) {
      farmerServices
        .getFarmerDetails(farmerId)
        .then((respose) => {
          console.log(respose.data);
          setFirstname(respose.data.firstname);
          setLastname(respose.data.lastname);
          setPhoneNo(respose.data.phoneNo);
          setAddress(respose.data.address);
          setEmail(respose.data.email);
        })
        .catch((error) => {
          console.log("Something went wrong", error);
        });
    }
  }, []);

  return (
    <div>
      <AdminNavBar />
      <div className="container h-100">
        <div className="row justify-content-sm-center h-100">
          <div className="col-xl-2"></div>

          <div className="col-xl-8 col-md-8 col-sm-12 col-12 pt-5">
            <div className="card shadow-lg custom-card">
              <div className="card-body px-5 pt-5">
                <h1 className="fs-4 card-title fw-bold mb-4 text-center text-primary">
                  {farmerId ? "UPDATE" : "ADD NEW"} FARMER
                </h1>
                <form onSubmit={(e) => addFarmer(e)}>
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
                        autoFocus
                        value={firstname}
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
                        onChange={(e) => setEmail(e.target.value)}
                      />
                    </div>

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
                        onChange={(e) => setPhoneNo(e.target.value)}
                      />
                    </div>
                  </div>

                  <div className="row g-3">
                    <div className="col mb-3">
                      <label className="mb-2 text-muted" htmlFor="Address">
                        Address
                      </label>
                      <textarea
                        id="Address"
                        className="form-control custom-input"
                        name="Address"
                        required
                        rows="2"
                        cols="50"
                        maxLength="50"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                      />
                    </div>
                  </div>

                  <div className="align-items-center d-flex">
                    <button
                      type="submit"
                      className="btn btn-primary form-control custom-btn"
                    >
                      {farmerId ? "Update" : "Add New"} Farmer
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
          <div className="col-xl-2"></div>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default AddNewFarmer;
