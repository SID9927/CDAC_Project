import React, { useEffect, useState } from "react";
import Cards from "react-credit-cards-2";
import "react-credit-cards-2/dist/es/styles-compiled.css";
import "../../App.css";
import userServices from "../../Services/user.services";
import PaymentDoneLoading from "./PaymentDoneLoading";
import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import UserNavBar from "./UserNavBar";
import Footer from "../Footer";

const PaymentForm = (props) => {
  const [state, setState] = useState({
    number: "",
    name: "",
    expiry: "",
    cvc: "",
    focus: "",
  });
  const [user, setUser] = useState([]);
  const [orderPlaced, setOrderPlaced] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    var val = localStorage.getItem("user-token");
    var object = JSON.parse(val);
    setUser(object);
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setState(prev => ({ ...prev, [name]: value }));
  };

  const handleInputFocus = (e) => {
    setState(prev => ({ ...prev, focus: e.target.name }));
  };

  const handleExpiry = (e) => {
    const { value } = e.target;
    setState(prev => ({ ...prev, expiry: prev.expiry.concat(value) }));
  };

  const sendData = (event) => {
    event.preventDefault();
    const { number, expiry, cvc } = state;
    if (
      number === "4444444444443333" &&
      expiry === "1225" &&
      cvc === "654"
    ) {
      userServices
        .placeOrder()
        .then((response) => {
          console.log("Order Placed", response.data);
          toast.success("Transaction Done!", {
            duration: 2500,
            style: {
              borderRadius: "10px",
              background: "#333",
              color: "#fff",
            },
          });
          setOrderPlaced(true);
          setTimeout(() => {
            navigate("/user/orders");
          }, 3500);
        })
        .catch((error) => {
          console.log("Something went wrong", error);
        });
    } else {
      toast.error("Something went wrong!", {
        style: {
          borderRadius: "10px",
          background: "#333",
          color: "#fff",
        },
      });
    }
  };

  return (
    <div>
      <UserNavBar />

      {orderPlaced ? (
        <PaymentDoneLoading />
      ) : (
        <div className="text-center" style={{ marginTop: "5%" }}>
          <div className="container">
            <div className="row d-flex justify-content-center">
              <div className="col-xl-7 mb-4">
                <div id="PaymentForm" className="card shadow-sm p-4">
                  <Cards
                    number={state.number}
                    expiry={state.expiry}
                    cvc={state.cvc}
                    name={state.name}
                    focused={state.focus}
                  />
                  <p className="mt-5 text-muted">
                    <strong>Use the following details for payment:</strong>{" "}
                    <br />
                    Card Number: 4444-4444-4444-3333 <br />
                    Exp Date: 12/25 <br />
                    CVC: 654
                  </p>
                </div>
              </div>
              <div className="col-xl-5">
                <div className="card border-primary p-4 shadow-sm">
                  <h5 className="text-success fw-bold mb-3 text-center">
                    Enter Your Card Details
                  </h5>
                  <form
                    className="row g-3"
                    onSubmit={(event) => sendData(event)}
                  >
                    <div className="col-12">
                      <input
                        className="form-control mb-3"
                        type="tel"
                        name="number"
                        placeholder="Card Number"
                        value={state.number}
                        maxLength="16"
                        pattern="[0-9]+"
                        onChange={handleInputChange}
                        onFocus={handleInputFocus}
                        required
                      />
                    </div>
                    <div className="col-12">
                      <div className="row">
                        <div className="col-6 mb-3">
                          <select
                            name="expiry"
                            className="form-select"
                            onChange={handleInputChange}
                            required
                          >
                            <option value="">Month</option>
                            <option value="01">Jan</option>
                            <option value="02">Feb</option>
                            <option value="03">Mar</option>
                            <option value="04">Apr</option>
                            <option value="05">May</option>
                            <option value="06">Jun</option>
                            <option value="07">Jul</option>
                            <option value="08">Aug</option>
                            <option value="09">Sep</option>
                            <option value="10">Oct</option>
                            <option value="11">Nov</option>
                            <option value="12">Dec</option>
                          </select>
                        </div>
                        <div className="col-6 mb-3">
                          <select
                            name="expiry"
                            className="form-select"
                            onChange={handleExpiry}
                            required
                          >
                            <option value="">Year</option>
                            <option value="21">2021</option>
                            <option value="22">2022</option>
                            <option value="23">2023</option>
                            <option value="24">2024</option>
                            <option value="25">2025</option>
                            <option value="26">2026</option>
                            <option value="27">2027</option>
                            <option value="28">2028</option>
                            <option value="29">2029</option>
                            <option value="30">2030</option>
                          </select>
                        </div>
                      </div>
                    </div>
                    <div className="col-12 mb-3">
                      <input
                        className="form-control"
                        type="tel"
                        name="cvc"
                        placeholder="Card CVC"
                        maxLength="3"
                        value={state.cvc}
                        pattern="\d*"
                        onChange={handleInputChange}
                        onFocus={handleInputFocus}
                        required
                      />
                    </div>
                    <div className="col-12 mb-3">
                      <input
                        className="form-control"
                        type="text"
                        name="name"
                        placeholder="Cardholder Name"
                        value={state.name}
                        onChange={handleInputChange}
                        onFocus={handleInputFocus}
                        required
                      />
                    </div>
                    <div className="text-center">
                      <button type="submit" className="btn btn-dark btn-block">
                        Pay INR {props.amount}
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}

      <Footer />
    </div>
  );
};

export default PaymentForm;
