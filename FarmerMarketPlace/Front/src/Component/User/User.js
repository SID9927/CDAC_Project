import { useEffect, useState } from "react";
import farmerServices from "../../Services/farmer.services";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import AddToCart from "./AddToCart";
import UserNavBar from "./UserNavBar";
import Footer from "../Footer";

function User() {
  const [products, setProducts] = useState([]);
  const [isReRender, setIsReRender] = useState(true);

  const init = () => {
    farmerServices
      .getProductList()
      .then((response) => {
        console.log("Printing Products data", response.data);
        setProducts(response.data);
      })
      .catch((error) => {
        console.log("Something went wrong", error);
      });
  };

  useEffect(() => {
    init();
  }, []);

  const priceAscending = () => {
    var data = products.sort((a, b) => a.pricePerUnit - b.pricePerUnit);
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  const quantityAscending = () => {
    var data = products.sort((a, b) => a.quantity - b.quantity);
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  const quantityDescending = () => {
    var data = products.sort((a, b) => b.quantity - a.quantity);
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  const priceDescending = () => {
    var data = products.sort((a, b) => b.pricePerUnit - a.pricePerUnit);
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  const nameAscending = () => {
    var data = products.sort((a, b) => a.stockItem.localeCompare(b.stockItem));
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  const nameDescending = () => {
    var data = products.sort((a, b) => b.stockItem.localeCompare(a.stockItem));
    setProducts(data);
    setIsReRender(false);
    setTimeout(() => {
      setIsReRender(true);
    }, 1);
  };

  return (
    <div>
      <UserNavBar />

      <div className="container my-3">
        <div className="row">
          <div className="col-xl-3">
            <div className="card p-3 shadow-sm">
              <h5 className="text-center mb-3">Sort Products</h5>
              <div className="btn-group-vertical w-100">
                <button
                  className="btn btn-outline-secondary dropdown-toggle"
                  type="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Sort By Descending
                </button>
                <ul className="dropdown-menu w-100">
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => priceDescending()}
                    >
                      Price Descending
                    </a>
                  </li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => quantityDescending()}
                    >
                      Quantity Descending
                    </a>
                  </li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => nameDescending()}
                    >
                      Name Descending
                    </a>
                  </li>
                </ul>

                <button
                  className="btn btn-outline-secondary dropdown-toggle mt-2"
                  type="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Sort By Ascending
                </button>
                <ul className="dropdown-menu w-100">
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => priceAscending()}
                    >
                      Price Ascending
                    </a>
                  </li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => quantityAscending()}
                    >
                      Quantity Ascending
                    </a>
                  </li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="#"
                      onClick={() => nameAscending()}
                    >
                      Name Ascending
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>

          <div className="col-xl-9 mt-3 mt-xl-0">
            <div className="container mt-2">
              {isReRender ? (
                <Row xs={1} sm={2} md={3} className="g-4">
                  {products.map((p) => (
                    <Col key={p.id}>
                      <Card className="shadow-sm">
                        <Card.Img
                          variant="top"
                          src={`http://localhost:9090/FarmersMarketplace/admin/${p.id}`}
                        />
                        <Card.Body className="text-center">
                          <Card.Title className="fw-bold">
                            {p.stockItem}
                          </Card.Title>
                          <Card.Text>
                            Amount:{" "}
                            <span className="fw-bold">
                              INR {p.pricePerUnit}
                            </span>
                          </Card.Text>
                          <AddToCart productId={p.id} />
                        </Card.Body>
                      </Card>
                    </Col>
                  ))}
                </Row>
              ) : (
                <div className="text-center">No products available</div>
              )}
            </div>
          </div>
        </div>
      </div>

      <Footer />
    </div>
  );
}

export default User;
