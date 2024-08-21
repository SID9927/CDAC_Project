import axios from 'axios';

export default axios.create({
  baseURL: 'http://localhost:5194/',
  headers: {
    'Content-Type': 'application/json',
  },
});
