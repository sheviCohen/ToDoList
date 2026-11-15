import axios from 'axios';

const apiUrl = "http://localhost:5220"
axios.defaults.baseURL = apiUrl;
axios.interceptors.response.use(
  response => response,
  error => {
    console.error("API error:", error.config?.url, error.response?.status, error.message);
    return Promise.reject(error);
  }
);


export default {
  getTasks: async () => {
    const result = await axios.get(`/items`)    
    return result.data;
  },

addTask: async (name) => {
  const result = await axios.post(`/items`,{name:name,isComplete:false});
  return result.data;
},


setCompleted: async (id, isComplete) => {
  await axios.put(`/items/${id}`, {isComplete:isComplete});
},

deleteTask: async (id) => {
  await axios.delete(`/items/${id}`);
},


};
