import { useEffect, useState } from "react";
import userServices from "../../Services/user.services";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import EmptyCart from "./EmptyCart";
import UserNavBar from "./UserNavBar";
import { useNavigate } from "react-router-dom";
import Footer from "../Footer";

function CheckOut() {
  const [cart, setCart] = useState([]);
  const [totalAmount, setTotalAmount] = useState(0);
  const [user, setUser] = useState([]);
  const [empty, setEmpty] = useState(false);

  const navigate = useNavigate();
  const init = () => {
    var val = localStorage.getItem("user-token");
    var object = JSON.parse(val);
    setUser(object);

    userServices
      .checkOut()
      .then((response) => {
        console.log("Got response from checkout", response.data);
        setCart(response.data);

        calculateTotalAmount(response.data);

        if (response.data.length === 0) {
          setEmpty(true);
        }
      })
      .catch((error) => {
        console.log("Something went wrong", error);
        setEmpty(true);
      });
  };

  const calculateTotalAmount = (cartData) => {
    const sum = cartData.reduce(
      (total, item) => total + item.price * item.qty,
      0
    );
    setTotalAmount(sum);
  };

  const removeItem = (id) => {
    console.log(id);
    userServices
      .removeItem(id)
      .then((response) => {
        console.log("Item Removed", response.data);
        init();
      })
      .catch((error) => {
        console.log("Something went wrong", error);
      });
  };

  const updateQuantity = (index, newQty) => {
    if (newQty < 1) return;
    const updatedCart = [...cart];
    updatedCart[index].qty = newQty;
    updatedCart[index].amount = updatedCart[index].price * newQty;
    setCart(updatedCart);
    calculateTotalAmount(updatedCart);
  };

  useEffect(() => {
    init();
  }, []);

  const buyItems = () => {
    console.log("hi");
    userServices
      .placeOrder()
      .then((response) => {
        console.log("Order Placed", response.data);
      })
      .catch((error) => {
        console.log("Something went wrong", error);
      });
  };

  return (
    <div>
      <UserNavBar />
      <div className="container">
        <h3 className="my-1 mt-5 text-center text-primary fw-bold">Basket</h3>
        <hr />
        {empty ? (
          <EmptyCart />
        ) : (
          <div className="row">
            <div className="col-md-8">
              {cart.map((c, index) => (
                <div key={index} className="card mb-3">
                  <div className="row g-0">
                    <div className="col-md-3">
                      <img
                        src={`http://localhost:9090/FarmersMarketplace/farmer/${c.id}/image`}
                        alt="productImage"
                        className="img-fluid rounded-start"
                        style={{ maxHeight: "150px", objectFit: "cover" }}
                      />
                      {/* <img
                        src={`https://farmermarketplaceaspnetcore-production.up.railway.app/farmer/${c.id}/image`}
                        alt="productImage"
                        className="img-fluid rounded-start"
                        style={{ maxHeight: "150px", objectFit: "cover" }}
                      /> */}
                    </div>
                    <div className="col-md-9">
                      <div className="card-body">
                        <h5 className="card-title">{c.item}</h5>
                        <p className="card-text">Price: ₹{c.price}</p>
                        <div className="d-flex justify-content-between align-items-center">
                          <div>
                            <button
                              className="btn btn-sm btn-outline-secondary me-2"
                              onClick={() => updateQuantity(index, c.qty - 1)}
                            >
                              -
                            </button>
                            {c.qty}
                            <button
                              className="btn btn-sm btn-outline-secondary ms-2"
                              onClick={() => updateQuantity(index, c.qty + 1)}
                            >
                              +
                            </button>
                          </div>
                          <button
                            className="btn btn-sm btn-danger"
                            onClick={() => {
                              removeItem(index);
                            }}
                          >
                            Remove
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            <div className="col-md-4">
              <div className="card">
                <div className="card-body">
                  <h5 className="card-title">Price Details</h5>
                  <hr />
                  <p className="d-flex justify-content-between">
                    <span>Price ({cart.length} items)</span>
                    <span>₹{totalAmount}</span>
                  </p>
                  <p className="d-flex justify-content-between">
                    <span>Delivery Charges</span>
                    <span className="text-success">FREE</span>
                  </p>
                  <hr />
                  <p className="d-flex justify-content-between fw-bold">
                    <span>Total Amount</span>
                    <span>₹{totalAmount}</span>
                  </p>
                  <button
                    className="btn btn-primary w-100"
                    onClick={() =>
                      navigate("/user/payment", { state: { totalAmount } })
                    }
                  >
                    Place Order
                  </button>
                </div>
              </div>
            </div>
          </div>
        )}

        <div className="mt-4">
          <Card>
            <Card.Body className="ms-4">
              <Card.Title className="text-success">
                Delivery Address:{" "}
              </Card.Title>
              <hr />
              <Card.Text>
                <b>Name:</b> {user.firstname + " " + user.lastname}
                <br />
                <b>Address:</b> {user.address}
                <br />
                <b>Email:</b> {user.email}
                <br />
                <b>Phone No.:</b> {user.phoneNo}
              </Card.Text>
            </Card.Body>
          </Card>
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default CheckOut;
