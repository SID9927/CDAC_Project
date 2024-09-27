import axios from 'axios';

export default axios.create({
  baseURL: 'http://localhost:9090/FarmersMarketplace/',
  // baseURL: 'https://farmermarketplacespringboot-production.up.railway.app/FarmersMarketplace/',
  headers: {
    'Content-Type': 'application/json',
  },
});
