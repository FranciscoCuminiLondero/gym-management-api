# JSONs de Prueba para Endpoints - GymManagement API

## 🔐 Auth Controller

### POST /api/auth/login
```json
{
  "email": "admin@gym.com",
  "password": "Admin123!"
}
```

### POST /api/auth/register
```json
{
  "nombre": "Juan",
  "apellido": "Pérez",
  "dni": "12345678",
  "email": "juan.perez@email.com",
  "password": "Pass123!",
  "telefono": "555-1234",
  "fechaNacimiento": "1995-05-15",
  "role": "Alumno",
  "planId": 1
}
```

---

## 👤 Usuarios Controller

### GET /api/usuarios
**Query params:** `?roleId=4&sucursalId=1`

### GET /api/usuarios/{id}
**Ejemplo:** `/api/usuarios/1`

### GET /api/usuarios/by-email?email=admin@gym.com

### POST /api/usuarios
```json
{
  "nombre": "María",
  "apellido": "González",
  "dni": "87654321",
  "email": "maria.gonzalez@email.com",
  "password": "Pass123!",
  "telefono": "555-5678",
  "fechaNacimiento": "1998-03-22",
  "genero": "Femenino",
  "direccion": "Calle Falsa 123",
  "role": "Alumno",
  "sucursalId": 1
}
```

### PATCH /api/usuarios/{id}
```json
{
  "nombre": "María Actualizada",
  "telefono": "555-9999",
  "genero": "Femenino",
  "direccion": "Nueva Dirección 456",
  "image": "https://ejemplo.com/imagen.jpg",
  "sucursalId": 2
}
```

### DELETE /api/usuarios/{id}
**Ejemplo:** `/api/usuarios/5`

---

## 👨‍🏫 Profesores Controller

### GET /api/profesores
**Query params:** `?sucursalId=1`

### GET /api/profesores/{id}
**Ejemplo:** `/api/profesores/1`

### GET /api/profesores/{id}/clases
**Ejemplo:** `/api/profesores/1/clases`

### POST /api/profesores
```json
{
  "nombre": "Carlos",
  "apellido": "Rodríguez",
  "dni": "11223344",
  "email": "carlos.rodriguez@gym.com",
  "password": "Prof123!",
  "telefono": "555-7777",
  "fechaNacimiento": "1985-08-10"
}
```

### PATCH /api/profesores/{id}
```json
{
  "especialidad": "Pilates y Yoga",
  "telefono": "555-8888",
  "sucursalId": 2
}
```

### PUT /api/profesores/{id}
```json
{
  "nombre": "Carlos",
  "apellido": "Rodríguez Actualizado",
  "telefono": "555-8888",
  "email": "carlos.nuevo@gym.com",
  "fechaNacimiento": "1985-08-10"
}
```

### DELETE /api/profesores/{id}
**Ejemplo:** `/api/profesores/3`

---

## 📋 Planes Controller

### GET /api/planes

### GET /api/planes/{id}
**Ejemplo:** `/api/planes/1`

### POST /api/planes
```json
{
  "nombre": "Plan Premium",
  "descripcion": "Acceso ilimitado a todas las instalaciones",
  "precio": 5999.99,
  "duracionDias": 30,
  "maxReservasPorMes": 20,
  "tiposPermitidos": ["general", "especializada"]
}
```

### PATCH /api/planes/{id}
```json
{
  "precio": 6499.99,
  "maxReservasPorMes": 25,
  "tiposPermitidos": ["general", "especializada", "premium"]
}
```

### DELETE /api/planes/{id}
**Ejemplo:** `/api/planes/2`

---

## 🏋️ Clases Controller

### GET /api/clases
**Query params:** `?sucursalId=1`

### GET /api/clases/fecha/{fecha}
**Ejemplo:** `/api/clases/fecha/2025-11-15`

### GET /api/clases/{id}
**Ejemplo:** `/api/clases/1`

### POST /api/clases
```json
{
  "profesorId": 1,
  "salaId": 1,
  "sucursalId": 1,
  "nombre": "Yoga Matutino",
  "descripcion": "Clase de yoga para principiantes",
  "duracionMinutos": 60,
  "horaInicio": "08:00:00",
  "fecha": "2025-11-20",
  "capacidad": 20
}
```

**Nota:** El ProfesorId debe existir en la base de datos. Si no tienes profesores creados, primero crea uno usando POST /api/profesores.

### PATCH /api/clases/{id}
```json
{
  "nombre": "Yoga Avanzado",
  "duracionMinutos": 90,
  "capacidad": 15,
  "dias": ["Lunes", "Jueves"],
  "mostrarEnHome": false
}
```

### DELETE /api/clases/{id}
**Ejemplo:** `/api/clases/3`

---

## 🎟️ Reservas Controller

### GET /api/reservas?alumnoId={id}
**Ejemplo:** `/api/reservas?alumnoId=5`

### GET /api/reservas?claseId={id}
**Ejemplo:** `/api/reservas?claseId=2`

### POST /api/reservas
```json
{
  "alumnoId": 1,
  "claseId": 1,
  "fechaReserva": "2025-11-20"
}
```

### PATCH /api/reservas/{id}
```json
{
  "estado": "cancelada"
}
```

### DELETE /api/reservas/{id}
**Ejemplo:** `/api/reservas/10`

---

## 🏢 Sucursales Controller

### GET /api/sucursales

### GET /api/sucursales/all

### GET /api/sucursales/{id}
**Ejemplo:** `/api/sucursales/1`

### POST /api/sucursales
```json
{
  "nombre": "Sucursal Sur",
  "direccion": "Av. Sur 789",
  "telefono": "555-0003",
  "email": "sur@gym.com"
}
```

### PUT /api/sucursales/{id}
```json
{
  "nombre": "Sucursal Sur Actualizada",
  "direccion": "Nueva Av. Sur 1000",
  "telefono": "555-0004",
  "email": "surnuevo@gym.com"
}
```

### DELETE /api/sucursales/{id}
**Ejemplo:** `/api/sucursales/3`

---

## 🏠 Salas Controller

### GET /api/salas

### GET /api/salas/sucursal/{sucursalId}
**Ejemplo:** `/api/salas/sucursal/1`

### GET /api/salas/{id}
**Ejemplo:** `/api/salas/1`

### POST /api/salas
```json
{
  "sucursalId": 1,
  "nombre": "Sala C",
  "tipo": "Cardio",
  "capacidad": 25,
  "descripcion": "Sala equipada con máquinas cardiovasculares"
}
```

### PUT /api/salas/{id}
```json
{
  "nombre": "Sala C Actualizada",
  "tipo": "Cardio y Funcional",
  "capacidad": 30,
  "descripcion": "Sala multiuso renovada"
}
```

### DELETE /api/salas/{id}
**Ejemplo:** `/api/salas/5`

---

## 💳 Membresías Controller

### GET /api/membresias?alumnoId={id}
**Ejemplo:** `/api/membresias?alumnoId=5`

### POST /api/membresias
```json
{
  "alumnoId": 5,
  "planId": 1,
  "fechaInicio": "2025-11-14",
  "fechaFin": "2025-12-14",
  "monto": 5999.99,
  "metodoPago": "Tarjeta"
}
```

### PATCH /api/membresias/{id}
```json
{
  "planId": 2,
  "fechaFin": "2025-12-31",
  "activa": true
}
```
