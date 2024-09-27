import httpClient from "../http-common-spring";

const login = (data) => {
  return httpClient.post("/user/login", data);
};

const register = (data) => {
  return httpClient.post("/user/register", data);
};

const addToCart = (id, form) => {
  return httpClient.post(`/user/addtocart/${id}`, form);
};

const checkOut = () => {
  return httpClient.get("/user/checkout");
};

const removeItem = (id) => {
  return httpClient.post(`/user/removefromcart/${id}`);
};

const placeOrder = () => {
  return httpClient.post(`/user/placeorder`);
};

const orders = (form) => {
  return httpClient.post(`/user/orders`, form);
};

export default {
  login,
  register,
  addToCart,
  checkOut,
  removeItem,
  placeOrder,
  orders,
};
