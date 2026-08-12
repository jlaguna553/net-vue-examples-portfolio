<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';

// URL base de la API corriendo en .NET (WSL / Localhost)
const API_URL = 'http://localhost:5000/api/productos';

// Estados reactivos (variables que al cambiar actualizan el HTML automáticamente)
const productos = ref([]);
const cargando = ref(false);
const editando = ref(false);

// Objeto reactivo para vincular con el formulario
const formulario = ref({
  id: null,
  nombre: '',
  precio: 0,
  stock: 0
});

// 1. GET: Leer la lista de productos desde la API
const cargarProductos = async () => {
  cargando.value = true;
  try {
    const respuesta = await axios.get(API_URL);
    productos.value = respuesta.data;
  } catch (error) {
    console.error('Error al obtener productos desde la API:', error);
  } finally {
    cargando.value = false;
  }
};

// 2. POST / PUT: Crear o actualizar según el estado de 'editando'
const guardarProducto = async () => {
  try {
    const payload = {
      nombre: formulario.value.nombre,
      precio: parseFloat(formulario.value.precio),
      stock: parseInt(formulario.value.stock)
    };

    if (editando.value) {
      // PUT: Actualizar registro existente enviando el ID en la URL
      await axios.put(`${API_URL}/${formulario.value.id}`, payload);
    } else {
      // POST: Crear nuevo registro
      await axios.post(API_URL, payload);
    }

    limpiarFormulario();
    await cargarProductos(); // Recargar la lista para mostrar los cambios
  } catch (error) {
    console.error('Error al guardar el producto:', error);
  }
};

// Cargar un producto seleccionado en el formulario para editar
const seleccionarParaEditar = (prod) => {
  editando.value = true;
  formulario.value = { ...prod }; // Clonamos el objeto para no modificar la lista en vivo
};

// Cancelar edición o limpiar campos
const limpiarFormulario = () => {
  editando.value = false;
  formulario.value = { id: null, nombre: '', precio: 0, stock: 0 };
};

// Hook del ciclo de vida: Ejecutar al montar el componente en el navegador
onMounted(() => {
  cargarProductos();
});
</script>

<template>
  <div class="contenedor">
    <h1>Gestión de Productos</h1>
    
    <!-- FORMULARIO DE CREACIÓN / EDICIÓN -->
    <div class="card">
      <h2>{{ editando ? 'Editar Producto (PUT)' : 'Nuevo Producto (POST)' }}</h2>
      <form @submit.prevent="guardarProducto">
        <div class="campo">
          <label>Nombre del Producto:</label>
          <input v-model="formulario.nombre" type="text" required placeholder="Ej. Laptop" />
        </div>

        <div class="campo">
          <label>Precio ($):</label>
          <input v-model="formulario.precio" type="number" step="0.01" required />
        </div>

        <div class="campo">
          <label>Stock (Piezas):</label>
          <input v-model="formulario.stock" type="number" required />
        </div>

        <div class="acciones">
          <button type="submit" class="btn btn-primario">
            {{ editando ? 'Guardar Cambios' : 'Crear Producto' }}
          </button>
          <button v-if="editando" type="button" @click="limpiarFormulario" class="btn btn-secundario">
            Cancelar
          </button>
        </div>
      </form>
    </div>

    <!-- LISTADO DE PRODUCTOS (GET) -->
    <div class="card">
      <h2>Catálogo de Productos</h2>
      
      <p v-if="cargando">Cargando datos desde la API...</p>
      <p v-else-if="productos.length === 0">No hay productos en el catálogo.</p>

      <table v-else class="tabla">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nombre</th>
            <th>Precio</th>
            <th>Stock</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="prod in productos" :key="prod.id">
            <td>{{ prod.id }}</td>
            <td>{{ prod.nombre }}</td>
            <td>${{ prod.precio.toFixed(2) }}</td>
            <td>{{ prod.stock }}</td>
            <td>
              <button @click="seleccionarParaEditar(prod)" class="btn btn-editar">
                Editar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<style scoped>
.contenedor {
  max-width: 800px;
  margin: 2rem auto;
  padding: 0 1rem;
  font-family: system-ui, -apple-system, sans-serif;
  color: #333;
}

.card {
  background: #f9f9f9;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 1.5rem;
  margin-bottom: 2rem;
}

.campo {
  margin-bottom: 1rem;
}

.campo label {
  display: block;
  font-weight: bold;
  margin-bottom: 0.3rem;
}

.campo input {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  box-sizing: border-box;
}

.acciones {
  display: flex;
  gap: 0.5rem;
  margin-top: 1rem;
}

.tabla {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

.tabla th, .tabla td {
  border: 1px solid #ddd;
  padding: 0.6rem;
  text-align: left;
}

.tabla th {
  background-color: #eee;
}

.btn {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-weight: bold;
}

.btn-primario { background-color: #2b8a3e; color: white; }
.btn-secundario { background-color: #868e96; color: white; }
.btn-editar { background-color: #1c7ed6; color: white; }
</style>