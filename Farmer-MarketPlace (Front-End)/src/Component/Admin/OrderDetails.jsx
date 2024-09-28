import { useEffect, useState } from "react";
import adminServices from "../../Services/admin.services";
import AdminNavBar from "./AdminNavBar";
import { useNavigate } from "react-router-dom";
import Footer from "../Footer";
import '../../App.css';

function OrderDetails() {
  const navigate = useNavigate();
  const [order, setOrder] = useState([]);

  const init = () => {
    adminServices
      .orderDetails()
      .then((response) => {
        console.log("Order Details", response.data);
        setOrder(response.data);
      })
      .catch((error) => {
        console.log("Something Went Wrong", error);
      });
  };

  useEffect(() => {
    init();
  }, []);

  return (
    <div>
      <AdminNavBar />
      <div className="container-fluid">
        <h3 className="my-1 mt-5 text-center text-primary fw-bold">
          Order History
        </h3>
        <hr />
        <div>
          <button
            type="button"
            className="btn btn-info mb-3 custom-btn"
            onClick={() => navigate("/admin")}
          >
            <i className="fa fa-home" aria-hidden="true"></i> Go Back
          </button>
          <table className="table table-bordered table-striped custom-table text-center">
            <thead className="thead-dark">
              <tr className="table-primary">
                <th>Product Name</th>
                <th>Quantity</th>
                <th>Amount</th>
                <th>Delivery Date</th>
                <th>Payment Status</th>
                <th>Order Place Date</th>
                <th>Image</th>
              </tr>
            </thead>
            <tbody>
              {order.map((o) => (
                <tr key={order.indexOf(o)} className="custom-row">
                  <td>{o.orderItem}</td>
                  <td>{o.quantity}</td>
                  <td>{o.amount}</td>
                  <td>{o.orders?.deliveryDate || 'N/A'}</td>
                  <td>
                    <span
                      className={`badge ${
                        o.orders?.paymentStatus ? "bg-success" : "bg-warning"
                      }`}
                    >
                      {o.orders?.paymentStatus ? "Done" : "Pending"}
                    </span>
                  </td>
                  <td>{o.orders?.placeOrderDate || 'N/A'}</td>
                  <td className="text-center">
                    <img
                      src={`http://localhost:9090/FarmersMarketplace/farmer/${o.orderItem}/image`}
                      // src={`https://farmermarketplaceaspnetcore-production.up.railway.app/farmer/${o.orderItem}/image`}
                      alt="productImage"
                      width={75}
                      className="img-thumbnail"
                    />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default OrderDetails;
