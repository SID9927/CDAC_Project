import axios from 'axios';

export default axios.create({
  // baseURL: 'https://localhost:7213',
  baseURL: 'https://farmermarketplaceasp-netcore.onrender.com/',
  headers: {
    'Content-Type': 'application/json',
  },
});
