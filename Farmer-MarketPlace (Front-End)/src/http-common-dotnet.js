import axios from 'axios';

export default axios.create({
  baseURL: 'https://localhost:7213',
  // baseURL: 'https://farmermarketplaceaspnetcore-production.up.railway.app/',
  headers: {
    'Content-Type': 'application/json',
  },
});
