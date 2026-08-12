# .NET + Vue — API REST con frontend en Vue 3

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Vue](https://img.shields.io/badge/Vue.js%203-4FC08D?style=for-the-badge&logo=vuedotjs&logoColor=white)
![Vite](https://img.shields.io/badge/Vite-646CFF?style=for-the-badge&logo=vite&logoColor=white)
![Axios](https://img.shields.io/badge/Axios-5A29E4?style=for-the-badge&logo=axios&logoColor=white)
![Tailwind](https://img.shields.io/badge/Tailwind%20CSS-06B6D4?style=for-the-badge&logo=tailwindcss&logoColor=white)
![License](https://img.shields.io/badge/Licencia-MIT-green?style=for-the-badge)

> **Full-stack moderno: API REST en ASP.NET Core consumida por un frontend Vue 3 con Vite. CRUD completo y documentado.**

Ejemplo completo de integración **.NET + Vue**: un backend `ASP.NET Core` (C#) que expone una API REST de productos, y un frontend `Vue 3` (Composition API) que la consume con Axios. Arquitectura cliente-servidor limpia, ideal como base para proyectos reales.

## ✨ Características

- **API REST en C#** — endpoints CRUD completos sobre una colección de productos (`ProductsController`)
- **Arquitectura en capas** — Controllers → Services → Models, separación de responsabilidades clara
- **Frontend Vue 3** — Composition API con `<script setup>`, consumo HTTP con Axios
- **Estilado con Tailwind CSS** vía plugin Vite
- **Configurado para desarrollo local** — backend y frontend arrancan por separado con live reload

## 🛠 Stack

| Capa | Tecnología |
|---|---|
| Backend | .NET 10 (ASP.NET Core, C#) |
| Frontend | Vue 3 + Vite |
| HTTP Client | Axios |
| Estilos | Tailwind CSS 4 |

## 🚀 Inicio rápido

**Backend:**

```bash
cd Backend.Api
dotnet run
# API en http://localhost:5xxx (revisa Properties/launchSettings.json)
```

**Frontend:**

```bash
cd Frontend.UI
npm install
npm run dev
# App en http://localhost:5173
```

## 📁 Estructura

```
net-vue-examples/
├── Backend.Api/
│   ├── Controllers/       # ProductsController (CRUD)
│   ├── Services/          # Lógica de negocio
│   ├── Models/            # Entidades
│   ├── Program.cs         # Configuración y DI
│   └── products.json      # Datos de ejemplo
└── Frontend.UI/
    ├── src/               # Componentes Vue 3
    ├── services/          # Cliente Axios
    ├── vite.config.js
    └── package.json
```

## 🏗 Arquitectura

```
Vue 3 (Frontend)  --Axios/HTTP-->  ASP.NET Core (API REST)  -->  Datos
     :5173                             :5xxx
```

El frontend nunca toca los datos directamente: toda la lógica pasa por la API REST, que valida y sirve los productos desde `products.json`.

<!-- Agrega capturas en docs/screenshots/ -->

---

## Desarrollado por Francisco Javier Laguna

Full-stack developer · React · Vue · .NET · PHP

[GitHub](https://github.com/jlaguna553) · [LinkedIn](https://www.linkedin.com/in/francisco-javier-laguna-mondrag%C3%B3n-80a798154/) · [CV Online](https://cv-online.jlaguna553.workers.dev/v/xrdcnyej)
