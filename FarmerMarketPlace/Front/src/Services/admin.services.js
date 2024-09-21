import httpClient from "../http-common-dotnet";
import httpClient2 from "../http-common-spring";

const getCategory = () => {
  return httpClient.get("/admin/categorylist");
};

const removeCategory = (categoryid) => {
  return httpClient2.delete(`/user/removecategory/${categoryid}`);
};

const addCategory = (categoryName) => {
  return httpClient.get(`/admin/addcategory/${categoryName}`);
};

const addProduct = (data, farmerId) => {
  return httpClient.post(`/admin/newproduct/${farmerId}`, data);
};

const addProductImage = (file, id) => {
  return httpClient2.post(`/farmer/${id}/image`, file);
};

const updateProduct = (id, form) => {
  return httpClient.put(`/admin/updateproduct/${id}`, form);
};

const orderDetails = () => {
  return httpClient.get("/admin/allorders");
};

const getUsersList = () => {
  return httpClient.get("/admin/allusers");
};

const getSpecificUserDetails = (id) => {
  return httpClient2.get(`/user/userdetails/${id}`);
};

const updateUser = (id, data) => {
  return httpClient2.post(`/user/updateuser/${id}`, data);
};

const getFarmerCount = () => {
  return httpClient.get("/admin/farmercount");
};

const getOrderCount = () => {
  return httpClient.get("/admin/ordercount");
};

const getProductCount = () => {
  return httpClient.get("/admin/productcount");
};

const getUserCount = () => {
  return httpClient.get("/admin/usercount");
};

const getCategoryCount = () => {
  return httpClient.get("/admin/categorycount");
};

export default {
  getCategory,
  removeCategory,
  addCategory,
  addProduct,
  addProductImage,
  updateProduct,
  orderDetails,
  getUsersList,
  getSpecificUserDetails,
  updateUser,
  getFarmerCount,
  getOrderCount,
  getProductCount,
  getUserCount,
  getCategoryCount,
};
