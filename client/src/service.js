import axios from 'axios';

const apiUrl = "http://localhost:5220"

export default {
  getTasks: async () => {
    const result = await axios.get(`${apiUrl}/items`)    
    return result.data;
  },

addTask: async (name) => {
  const result = await axios.post(`${apiUrl}/items`,{name:name,isComplete:false});
  return result.data;
},


setCompleted: async (id, isComplete) => {
  await axios.put(`${apiUrl}/items/${id}`, {isComplete:isComplete});
},

deleteTask: async (id) => {
  await axios.delete(`${apiUrl}/items/${id}`);
},


};
