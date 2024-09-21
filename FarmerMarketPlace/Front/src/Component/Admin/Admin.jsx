import { useNavigate } from "react-router-dom";
import AdminNavBar from "./AdminNavBar";
import Footer from "../Footer";
import '../../App.css';
import React, { useState, useEffect } from "react";
import { Card, Row, Col, Button, Container } from "react-bootstrap";
import {
  FaBoxOpen,
  FaShoppingCart,
  FaTags,
  FaUserAlt,
  FaUsers,
} from "react-icons/fa";
import adminServices from "../../Services/admin.services";

const statsData = [
  {
    id: 1,
    title: "Total Farmers",
    icon: <FaUserAlt />,
    color: "primary",
    link: "/admin/farmer",
    buttonText: "View Farmers",
    buttonClass: "button-30",
    countKey: "farmers",
  },
  {
    id: 2,
    title: "Total Categories",
    icon: <FaTags />,
    color: "success",
    link: "/admin/category",
    buttonText: "View Categories",
    buttonClass: "button-30",
    countKey: "categories",
  },
  {
    id: 3,
    title: "Total Products",
    icon: <FaBoxOpen />,
    color: "danger",
    link: "/admin/productslist",
    buttonText: "View Products",
    buttonClass: "button-30",
    countKey: "products",
  },
  {
    id: 4,
    title: "Total Orders",
    icon: <FaShoppingCart />,
    color: "warning",
    link: "/admin/order",
    buttonText: "View Orders",
    buttonClass: "button-30",
    countKey: "orders",
  },
  {
    id: 5,
    title: "Total Users",
    icon: <FaUsers />,
    color: "info",
    link: "/admin/userslist",
    buttonText: "View Users",
    buttonClass: "button-30",
    countKey: "users",
  },
];

function Admin() {
  const navigate = useNavigate();

  const [counts, setCounts] = useState({
    farmers: 0,
    categories: 0,
    products: 0,
    orders: 0,
    users: 0,
  });

  const [displayCounts, setDisplayCounts] = useState({
    farmers: 0,
    categories: 0,
    products: 0,
    orders: 0,
    users: 0,
  });

  useEffect(() => {
    const fetchCounts = async () => {
      try {
        const [farmers, categories, products, orders, users] = await Promise.all([
          adminServices.getFarmerCount(),
          adminServices.getCategoryCount(),
          adminServices.getProductCount(),
          adminServices.getOrderCount(),
          adminServices.getUserCount(),
        ]);

        const newCounts = {
          farmers: farmers.data,
          categories: categories.data,
          products: products.data,
          orders: orders.data,
          users: users.data,
        };

        setCounts(newCounts);

        // Animate the count increase
        Object.keys(newCounts).forEach((key) => {
          let start = 0;
          const end = newCounts[key];
          const duration = 2000; // 2 seconds
          const increment = end / (duration / 16); // 60 FPS

          const timer = setInterval(() => {
            start += increment;
            if (start >= end) {
              clearInterval(timer);
              start = end;
            }
            setDisplayCounts((prev) => ({ ...prev, [key]: Math.floor(start) }));
          }, 16);
        });
      } catch (error) {
        console.error("Error fetching counts:", error);
      }
    };

    fetchCounts();
  }, []);
  
  return (
    <div>
      <AdminNavBar />

      <Container className="my-5">
        <Row className="g-4">
          {statsData.slice(0, 4).map((item) => (
            <Col key={item.id} xs={12} sm={6} md={6} lg={3}>
              <Card className={`text-center border-0 shadow-sm h-100 bg-white`}>
                <Card.Body>
                  <div
                    className={`text-${item.color} mb-3`}
                    style={{ fontSize: "3rem" }}
                  >
                    {item.icon}
                  </div>
                  <Card.Title className="fs-2 fw-bold">{displayCounts[item.countKey]}</Card.Title>
                  <Card.Text className="mb-4 fs-5 text-muted">
                    {item.title}
                  </Card.Text>
                  <Button
                    variant={item.color}
                    className="w-100"
                    onClick={() => navigate(item.link)}
                  >
                    {item.buttonText}
                  </Button>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>

        <Row className="g-4 mt-4 justify-content-center">
          <Col xs={12} sm={6} md={4} lg={3}>
            <Card className="text-center border-0 shadow-sm h-100 bg-white">
              <Card.Body>
                <div
                  className={`text-${statsData[4].color} mb-3`}
                  style={{ fontSize: "3rem" }}
                >
                  {statsData[4].icon}
                </div>
                <Card.Title className="fs-2 fw-bold">
                  {displayCounts[statsData[4].countKey]}
                </Card.Title>
                <Card.Text className="mb-4 fs-5 text-muted">
                  {statsData[4].title}
                </Card.Text>
                <Button
                  variant={statsData[4].color}
                  className="w-100"
                  onClick={() => navigate(statsData[4].link)}
                >
                  {statsData[4].buttonText}
                </Button>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>

      <Footer />
    </div>
  );
}


export default Admin;