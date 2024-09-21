import React, { useEffect, useState } from "react";
import farmerServices from "../../Services/farmer.services";
import Card from "react-bootstrap/Card";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";
import AddToCart from "./AddToCart";
import UserNavBar from "./UserNavBar";
import Footer from "../Footer";
import SortProducts from "./SortProducts";

function User() {
  const [allProducts, setAllProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState([]);

  useEffect(() => {
    farmerServices
      .getProductList()
      .then((response) => {
        setAllProducts(response.data);
        setFilteredProducts(response.data);
      })
      .catch((error) => {
        console.log("Something went wrong", error);
      });
  }, []);

  const applyFilters = (sortType, priceRange, selectedCategory) => {
    let result = [...allProducts];

    // Apply price range filter
    result = result.filter(p => p.pricePerUnit >= priceRange[0] && p.pricePerUnit <= priceRange[1]);

    // Apply category filter
    if (selectedCategory) {
      result = result.filter(p => p.category.categoryName === selectedCategory);
    }

    // Apply sorting
    switch (sortType) {
      case 'priceLowToHigh':
        result.sort((a, b) => a.pricePerUnit - b.pricePerUnit);
        break;
      case 'priceHighToLow':
        result.sort((a, b) => b.pricePerUnit - a.pricePerUnit);
        break;
      case 'inStock':
        result = result.filter(p => p.quantity > 0);
        break;
      case 'outOfStock':
        result = result.filter(p => p.quantity <= 0);
        break;
      default:
        break;
    }

    setFilteredProducts(result);
  };
  return (
    <div>
      <UserNavBar />
      <div className="container my-3">
        <div className="row">
          <div className="col-xl-3">
            <SortProducts onApplyFilters={applyFilters} />
          </div>
          <div className="col-xl-9 mt-3 mt-xl-0">
            <div className="container mt-2">
              {filteredProducts.length > 0 ? (
                <Row xs={1} sm={2} md={2} lg={3} xl={4} className="g-4">
                  {filteredProducts.map((p) => (
                    <Col key={p.id}>
                      <Card className="shadow-sm">
                        <Card.Img
                          variant="top"
                          src={`http://localhost:9090/FarmersMarketplace/farmer/${p.id}/image`}
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
              ) : (                <div className="text-center">No products available</div>
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
