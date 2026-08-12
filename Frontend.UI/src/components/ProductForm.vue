<script setup>
import { ref, watch } from 'vue';

// 1. PROPS: Recibe un producto si el usuario dio clic en "Editar"
const props = defineProps({
  productoAEditar: {
    type: Object,
    default: null
  }
});

// 2. EMITS: Eventos que este componente puede disparar hacia el padre
const emit = defineEmits(['guardar', 'cancelar']);

// Estado local del formulario
const formulario = ref({
  id: null,
  nombre: '',
  precio: 0,
  stock: 0
});

// Escucha cambios en 'productoAEditar'. Si cambia, rellena el formulario.
watch(
  () => props.productoAEditar,
  (nuevoValor) => {
    if (nuevoValor) {
      formulario.value = { ...nuevoValor };
    } else {
      limpiar();
    }
  },
  { immediate: true }
);

const submit = () => {
  emit('guardar', { ...formulario.value });
};

const limpiar = () => {
  formulario.value = { id: null, nombre: '', precio: 0, stock: 0 };
  emit('cancelar');
};
</script>

<template>
  <div class="bg-white shadow-md rounded-lg p-6 mb-8 border border-gray-200">
    <h2 class="text-xl font-bold text-gray-800 mb-4">
      {{ formulario.id ? '✏️ Editar Producto (PUT)' : '➕ Nuevo Producto (POST)' }}
    </h2>

    <form @submit.prevent="submit" class="space-y-4">
      <div>
        <label class="block text-sm font-medium text-gray-700 mb-1">Nombre del Producto</label>
        <input 
          v-model="formulario.nombre" 
          type="text" 
          required 
          placeholder="Ej. Laptop Gaming" 
          class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
      </div>

      <div class="grid grid-cols-1 sm:grid-cols-2 gap-4">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Precio ($)</label>
          <input 
            v-model.number="formulario.precio" 
            type="number" 
            step="0.01" 
            required 
            class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Stock (Piezas)</label>
          <input 
            v-model.number="formulario.stock" 
            type="number" 
            required 
            class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
      </div>

      <div class="flex gap-3 pt-2">
        <button 
          type="submit" 
          class="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded-md transition-colors"
        >
          {{ formulario.id ? 'Guardar Cambios' : 'Crear Producto' }}
        </button>

        <button 
          v-if="formulario.id" 
          type="button" 
          @click="limpiar" 
          class="bg-gray-500 hover:bg-gray-600 text-white font-medium py-2 px-4 rounded-md transition-colors"
        >
          Cancelar
        </button>
      </div>
    </form>
  </div>
</template>