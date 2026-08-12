import axios from 'axios';

const API_URL = 'https://localhost:5000/api/products';

export const productService = {
    async obtenerTodos() {
        const response = await axios.get(API_URL);
        return response.data;
    },

    async crear(product) {
        const response = await axios.post(API_URL, product);
        return response.data;
    },

    async actualizar(id, product) {
        const response = await axios.put(`${API_URL}/${id}`, product);
        return response.data;
    },
}