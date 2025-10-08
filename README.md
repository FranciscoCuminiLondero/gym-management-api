# Sistema de Gestión de Gimnasios

> **Proyecto Final – Programación IV**  
> Una API RESTful para la gestión de gimnasios con **jerarquía de usuarios**, **autenticación JWT** y **Clean Architecture**. Desarrollada con **ASP.NET Core 8**, **C#** y **SQLite**.
---

## 🖋️ Minuta

### Contexto

Los gimnasios necesitan un sistema que centralice la gestión de alumnos, profesores, clases, sucursales, reservas, pagos y membresías. Se requiere una plataforma que permita registrar y controlar de manera confiable la información de cada usuario y sus interacciones con el gimnasio, garantizando que las reservas de clases, la asignación de salas y el seguimiento de pagos se realicen de forma organizada y eficiente. Además, es necesario que el sistema facilite la comunicación entre alumnos y profesores y proporcione herramientas de auditoría y notificación para optimizar la administración diaria y asegurar la trazabilidad de todas las acciones dentro del gimnasio.

### Proceso actual

En el escenario actual, los gimnasios registran alumnos, profesores y clases principalmente en hojas de cálculo. Cada alumno se asocia a un plan y realiza pagos que se registran manualmente en un Excel. No existe la reserva de clases, sino que se asignan por orden de llegada y sin cupos, lo que hace que las clases puedan quedar sobrepasadas de gente o con cupos desaprovechados.  

Los profesores, en caso de ausencia o enfermedad, no tienen forma de notificar al usuario, lo que genera muchas veces la asistencia de alumnos sin la posibilidad de recibir la clase. La información de pagos, reservas y membresías se dispersa entre distintos archivos y documentos digitales como en papel, dificultando el seguimiento de la actividad de los alumnos y generando problemas para auditar las acciones realizadas.  

Este método es lento, propenso a errores y limita la capacidad del gimnasio de ofrecer una experiencia organizada y profesional a sus clientes. Además, los gimnasios que cuentan con múltiples sucursales llevan cada uno su control de forma aislada, y muy pocas veces se centraliza la información, perdiendo la posibilidad de tener una visión general del funcionamiento del conjunto.

### Proceso con el sistema de información deseado

Con la implementación del sistema de gestión, los alumnos podrán registrarse y mantener un perfil actualizado en el sistema, asociando su membresía al plan que hayan contratado y permitiendo la administración automática de pagos y cambios de planes. Cada alumno tendrá la posibilidad de reservar clases directamente desde su perfil, visualizando la disponibilidad de las salas y los cupos de cada clase, mientras que el sistema notificará automáticamente cualquier cambio o confirmación de reserva.

Los profesores, por su parte, podrán ver sus clases asignadas a las salas de las sucursales correspondientes, gestionando su calendario de manera centralizada y recibiendo notificaciones sobre las reservas y asistencia de los alumnos, mejorando tanto el orden del establecimiento como su propia planificación.

La relación entre clases, alumnos y profesores será monitoreada, permitiendo que los alumnos asistan únicamente a las clases correspondientes a su membresía y que los profesores tengan visibilidad clara de sus alumnos y reservas.

El sistema también permitirá llevar un historial completo de pagos, membresías, notificaciones y auditorías de todas las acciones realizadas, garantizando control, trazabilidad y facilitando la toma de decisiones para la administración del gimnasio. De esta manera, se mejora la gestión general del gimnasio, se optimiza la utilización de recursos, se reducen los errores administrativos y se asegura una experiencia más fluida y profesional tanto para los alumnos como para los profesores.

### Diagrama de Clases

![Diagrama de clases](https://github.com/user-attachments/assets/c1a9894a-a650-4722-9967-ae7abfbc97c8)

---

## 🎯 Objetivo

Centralizar la gestión de **alumnos**, **profesores**, **clases**, **sucursales**, **reservas**, **pagos** y **membresías**.  
El sistema permite:
- Registro y administración de usuarios (alumnos y profesores).
- Reserva de clases con control de cupos.
- Gestión de membresías y pagos automáticos.
- Notificaciones y auditoría de acciones.
- Visibilidad unificada en gimnasios con múltiples sucursales.

---

## 🏗️ Arquitectura

Este proyecto sigue el patrón de **Clean Architecture**, separando responsabilidades en capas bien definidas:

``` bash
GymManagement (solución)
├── Api (Presentation) → Web API (controladores, JWT, CORS, Swagger)
├── Application → Servicios de negocio, interfaces de repositorio
├── Domain → Entidades con jerarquía TPH (Usuario → Alumno/Profesor)
├── Contract → DTOs (requests/responses) y contratos públicos
└── Infrastructure → EF Core, repositorios concretos, migraciones
```

## 🛠️ Tecnologías

- **Lenguaje**: C# 12
- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Base de datos**: SQLite (desarrollo) con Entity Framework Core
- **Arquitectura**: Clean Architecture + Repository Pattern
- **Autenticación**: JWT Bearer Tokens
- **Documentación**: Swagger/OpenAPI con autenticación
- **ORM**: Entity Framework Core con Table Per Hierarchy (TPH)

## 🔐 Autenticación y Autorización

### Sistema de Roles
- **SuperAdmin**: Acceso total al sistema
- **Administrador**: Gestión general del gimnasio  
- **Profesor**: Gestión de sus clases y alumnos
- **Alumno**: Acceso a su perfil y reservas

### Endpoints de Autenticación
- `POST /api/auth/login` - Inicio de sesión con email/password
- `POST /api/auth/register` - Registro de nuevos usuarios

### Políticas de Autorización
- **AdminPolicy**: Requiere rol SuperAdmin o Administrador
- **ProfesorPolicy**: Requiere rol Profesor
- **AlumnoPolicy**: Requiere rol Alumno
- **AlumnoOrProfesorPolicy**: Requiere rol Alumno o Profesor

---

## 📁 Estructura del Dominio

### Jerarquía de Usuarios (TPH - Table Per Hierarchy)
```csharp
Usuario (abstracta)
├── Alumno (con Plan y Membresías)
└── Profesor (con Clases asignadas)
```

### Entidades Principales
- **`Usuario`** - Clase base abstracta con datos comunes
- **`Alumno`** - Hereda de Usuario, tiene Plan y Membresías  
- **`Profesor`** - Hereda de Usuario, puede impartir Clases
- **`Rol`** - Define permisos (SuperAdmin, Administrador, Profesor, Alumno)
- **`Plan`** - Tipos de membresía disponibles
- **`Membresia`** - Relación Alumno-Plan con fechas y pagos
- **`Sucursal`** - Ubicaciones del gimnasio
- **`Sala`** - Espacios dentro de las sucursales
- **`Clase`** - Actividades impartidas por profesores
- **`Reserva`** - Reservas de alumnos para clases específicas
- **`Pago`** - Historial de pagos de membresías
- **`Notificacion`** - Sistema de mensajería interna
- **`Auditoria`** - Log de acciones del sistema

---

## 🚀 API Endpoints Implementados

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar nuevo usuario

### Gestión de Alumnos 
- `GET /api/alumnos` - Listar alumnos (SuperAdmin)
- `GET /api/alumnos/{id}` - Obtener alumno específico (SuperAdmin/Alumno propio)
- `PUT /api/alumnos/{id}` - Actualizar alumno (SuperAdmin/Alumno propio)  
- `DELETE /api/alumnos/{id}` - Eliminar alumno (SuperAdmin)

### Gestión de Profesores
- `GET /api/profesores` - Listar profesores (SuperAdmin)
- `GET /api/profesores/{id}` - Obtener profesor (SuperAdmin/Profesor propio)
- `PUT /api/profesores/{id}` - Actualizar profesor (SuperAdmin/Profesor propio)
- `DELETE /api/profesores/{id}` - Eliminar profesor (SuperAdmin)

### Gestión de Clases
- `GET /api/clases` - Listar clases disponibles
- `GET /api/clases/{id}` - Obtener clase específica
- `POST /api/clases` - Crear nueva clase (Profesor/Admin)
- `PUT /api/clases/{id}` - Actualizar clase (Profesor/Admin)
- `DELETE /api/clases/{id}` - Eliminar clase (Admin)

### Gestión de Planes
- `GET /api/planes` - Listar planes disponibles
- `GET /api/planes/{id}` - Obtener plan específico
- `POST /api/planes` - Crear nuevo plan (Admin)
- `PUT /api/planes/{id}` - Actualizar plan (Admin)
- `DELETE /api/planes/{id}` - Eliminar plan (Admin)

### Gestión de Reservas
- `GET /api/reservas` - Listar reservas (filtradas por usuario)
- `GET /api/reservas/{id}` - Obtener reserva específica
- `POST /api/reservas` - Crear nueva reserva (Alumno)
- `DELETE /api/reservas/{id}` - Cancelar reserva

## 📋 Contratos (DTOs)

### Requests
- `LoginRequest` - Email y contraseña
- `RegisterRequest` - Datos completos para registro
- `CreateAlumnoRequest` - Datos específicos de alumno
- `CreateProfesorRequest` - Datos específicos de profesor
- `CreateReservaRequest` - Datos para nueva reserva
- `CreatePlanRequest` - Datos para nuevo plan
- `CreateMembresiaRequest` - Datos para nueva membresía

### Responses  
- `AuthResponse` - Token JWT y datos del usuario
- `AlumnoResponse` - Datos públicos del alumno
- `ProfesorResponse` - Datos públicos del profesor
- `ClaseResponse` - Información de la clase
- `PlanResponse` - Detalles del plan
- `ReservaResponse` - Confirmación de reserva
- `MembresiaResponse` - Estado de membresía

---

## ⚠️ Estado Actual del Proyecto

### ✅ Implementado
- [x] Jerarquía de usuarios con TPH (Usuario → Alumno/Profesor)
- [x] Autenticación JWT completa con roles
- [x] Repositorios concretos con lógica específica
- [x] Servicios de negocio configurados
- [x] Controladores con autorización por roles
- [x] Migraciones de Entity Framework
- [x] Swagger con autenticación JWT
- [x] CORS configurado para frontend
- [x] Middleware global de manejo de excepciones

### 🔄 En Desarrollo
- [ ] **USUARIOS HARDCODEADOS**: No hay SuperAdmin por defecto
- [ ] Aplicación de migraciones a la base de datos
- [ ] Servicios de Membresía y Pago completos
- [ ] Sistema de notificaciones
- [ ] Auditoría de acciones
- [ ] Frontend para pruebas

### 🚨 Funcionalidades Faltantes
- [ ] Seeding de datos iniciales (SuperAdmin por defecto)
- [ ] Validaciones de negocio avanzadas
- [ ] Sistema de caché
- [ ] Logging estructurado
- [ ] Tests unitarios e integración
- [ ] Documentación de API completa
- [ ] Deployment y CI/CD

## ⚠️ Importante - No hay SuperAdmin hardcodeado

**El sistema NO incluye usuarios por defecto.** Necesitas:

1. **Crear el primer SuperAdmin manualmente** en la base de datos, o
2. **Implementar un endpoint de inicialización**, o  
3. **Usar el endpoint de registro** y luego cambiar el rol en la BD

Para crear un SuperAdmin temporal, puedes:
```sql
-- Después de aplicar migraciones
INSERT INTO Usuarios (Nombre, Apellido, Email, PasswordHash, RolId, Discriminator)
VALUES ('Admin', 'Sistema', 'admin@gym.com', 'hash_password', 1, 'Alumno');
```

## ▶️ Cómo ejecutar

### Prerrequisitos
- .NET 8 SDK
- SQLite (incluido en .NET)

### Pasos
```bash
# Clonar repositorio
git clone https://github.com/FranciscoCuminiLondero/gym-management-api.git
cd gym-management-api

# Restaurar dependencias
dotnet restore

# Aplicar migraciones (crear BD)
dotnet ef database update --project Infrastructure --startup-project Api

# Ejecutar aplicación
dotnet run --project Api
```

### Acceso
- **API**: http://localhost:5262
- **Swagger**: http://localhost:5262/swagger  
- **HTTPS**: https://localhost:7253 (requiere certificado dev)
