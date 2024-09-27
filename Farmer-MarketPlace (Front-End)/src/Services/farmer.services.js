import httpClient from "../http-common-spring";
import httpClient2 from "../http-common-dotnet";

const farmersList = () => {
  return httpClient.get("/farmer/list");
};

const removeFarmer = (id) => {
  return httpClient2.get(`/admin/removefarmer/${id}`);
};

const addFarmer = (data) => {
  return httpClient2.post("/admin/newfarmer", data);
};

const updateFarmer = (data, farmerid) => {
  return httpClient2.put(`/admin/updatefarmer/${farmerid}`, data);
};

const getFarmerDetails = (farmerid) => {
  return httpClient.get(`/farmer/farmerdetails/${farmerid}`);
};

const getProductList = () => {
  return httpClient.get(`/farmer/allproducts`);
};

const removeProduct = (id) => {
  return httpClient2.get(`/admin/removeproduct/${id}`);
};

const getSpecificProductDetails = (farmerid, productid) => {
  return httpClient.get(`/farmer/products/${farmerid}/${productid}`);
};

export default {
  farmersList,
  removeFarmer,
  addFarmer,
  updateFarmer,
  getFarmerDetails,
  getProductList,
  removeProduct,
  getSpecificProductDetails,
};
