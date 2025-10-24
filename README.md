# Sistema de Gestión de Gimnasios

> **Proyecto Final – Programación IV**  
> Una API RESTful para la gestión de gimnasios, desarrollada con **Clean Architecture**, **ASP.NET Core**, **C#** y **SQLite**.

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
- **Autenticación y autorización basada en roles (JWT)**.
- Reserva de clases con control de cupos.
- Gestión de membresías y pagos automáticos.
- Notificaciones y auditoría de acciones.
- Visibilidad unificada en gimnasios con múltiples sucursales.

---

## 🏗️ Arquitectura

Este proyecto sigue el patrón de **Clean Architecture**, separando responsabilidades en capas bien definidas:

```bash
GymManagement (solución)
├── Api (Presentation) → Web API (controladores, configuración, DI)
├── Application → Casos de uso, servicios, interfaces de repositorio
├── Domain → Entidades del negocio y lógica central
├── Contract → DTOs (requests/responses) y contratos públicos
└── Infrastructure → Implementaciones (EF Core, repositorios, migraciones)
```

### Capas del Proyecto

- **Presentation (Api)**: Controladores REST, configuración de autenticación/autorización, Swagger
- **Application**: Servicios de negocio, interfaces de repositorio, lógica de aplicación
- **Domain**: Entidades del dominio (`Usuario`, `Alumno`, `Profesor`, `Clase`, etc.)
- **Contract**: DTOs de Request/Response para comunicación con clientes
- **Infrastructure**: Implementación de repositorios, DbContext, migraciones

---

## 🛠️ Tecnologías

- **Lenguaje**: C# 12
- **Framework**: .NET 8
- **Base de datos**: SQLite (para desarrollo)
- **ORM**: Entity Framework Core 8.0
- **Autenticación**: JWT (JSON Web Tokens)
- **Documentación**: Swagger/OpenAPI
- **Arquitectura**: Clean Architecture

---

## 📁 Entidades del Dominio

### Entidades Principales

- **`Usuario`** (clase base): Información común de usuarios del sistema
  - `Alumno`: Hereda de Usuario, contiene membresías
  - `Profesor`: Hereda de Usuario, tiene clases asignadas
- **`Sucursal`**: Ubicaciones del gimnasio
- **`Sala`**: Espacios físicos dentro de sucursales (incluye tipo: Yoga, Spinning, Funcional, Pesas, Multiuso)
- **`Clase`**: Sesiones de entrenamiento dictadas por profesores
- **`Reserva`**: Reserva de un alumno en una clase
- **`Membresia`**: Suscripción de alumno a un plan
- **`Plan`**: Tipos de suscripción disponibles
- **`Pago`**: Registro de pagos de membresías
- **`Notificacion`**: Sistema de notificaciones
- **`Auditoria`**: Registro de acciones del sistema

### Relaciones Clave

- `Usuario` → `Alumno` | `Profesor` (herencia TPH - Table Per Hierarchy)
- `Alumno` ↔ `Membresia` (1 a N)
- `Alumno` ↔ `Reserva` (1 a N)
- `Profesor` ↔ `Clase` (1 a N)
- `Clase` ↔ `Reserva` (1 a N)
- `Clase` → `Sala` → `Sucursal`
- `Membresia` → `Plan`
- `Membresia` ↔ `Pago` (1 a N)

---

## 🔐 Autenticación y Autorización

### Sistema de Roles

El sistema implementa **autenticación JWT** con los siguientes roles:

- **Administrador**: Acceso total al sistema
- **Profesor**: Gestión de clases propias
- **Alumno**: Reserva de clases, gestión de perfil propio

### Políticas de Autorización

```csharp
- AdminPolicy: Solo administradores
- ProfesorPolicy: Solo profesores
- AlumnoPolicy: Solo alumnos
- AdminOrSuperAdminPolicy: Administradores o SuperAdministradores
```

### Credenciales por Defecto

```json
{
  "email": "admin@gym.com",
  "password": "Admin123!"
}
```

### Flujo de Autenticación

1. **Registro** (`POST /api/auth/register`):
   - Alumnos pueden autoregistrarse
   - Solo administradores pueden crear profesores/administradores

2. **Login** (`POST /api/auth/login`):
   - Retorna token JWT válido por 1 hora
   - Incluye rol del usuario

3. **Uso del Token**:
   - Header: `Authorization: Bearer {token}`
   - Validación automática en endpoints protegidos

---

## 📡 API Endpoints

### 🔓 Autenticación (Público)

| Endpoint | Método | Descripción |
|----------|--------|-------------|
| `/api/auth/register` | POST | Registro de usuarios (alumnos público, profesores solo admin) |
| `/api/auth/login` | POST | Autenticación y obtención de token JWT |

### 👥 Usuarios

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/usuarios` | GET | 🔐 Admin | Lista todos los usuarios |
| `/api/usuarios/{id}` | GET | 🔒 Owner/Admin | Ver perfil de usuario |
| `/api/usuarios/by-email?email=` | GET | 🔐 Admin | Buscar usuario por email |
| `/api/usuarios/{id}` | DELETE | 🔐 Admin | Desactivar usuario |

### 🎓 Alumnos

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/alumnos` | GET | 🔐 Admin | Lista todos los alumnos |
| `/api/alumnos/{id}` | GET | 🔒 Owner/Admin | Ver perfil de alumno |
| `/api/alumnos/perfil?alumnoId=` | GET | 🔒 Owner/Admin | Ver perfil completo (membresía, reservas) |
| `/api/alumnos/{id}` | PUT | 🔒 Owner/Admin | Actualizar datos del alumno |

### 👨‍🏫 Profesores

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/profesores` | GET | 🌐 Público | Lista todos los profesores |
| `/api/profesores/{id}` | GET | 🌐 Público | Ver perfil de profesor |
| `/api/profesores/{id}` | PUT | 🔒 Owner/Admin | Actualizar datos del profesor |

### 🏋️ Clases

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/clases` | GET | 🔒 Autenticado | Todas las clases activas |
| `/api/clases/fecha/{fecha}` | GET | 🔒 Autenticado | Clases disponibles por fecha |
| `/api/clases/{id}` | GET | 🔒 Autenticado | Detalles de una clase |
| `/api/clases` | POST | 🔐 Profesor/Admin | Crear nueva clase |
| `/api/clases/{id}` | DELETE | 🔐 Profesor/Admin | Eliminar clase |

### 📅 Reservas

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/reservas` | POST | 🔒 Autenticado | Crear nueva reserva |
| `/api/reservas/alumno/{alumnoId}` | GET | 🔒 Autenticado | Reservas de un alumno |
| `/api/reservas/clase/{claseId}` | GET | 🔒 Autenticado | Reservas de una clase |
| `/api/reservas/{id}` | DELETE | 🔒 Autenticado | Cancelar reserva |

### 💳 Planes

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/planes` | GET | 🌐 Público | Ver planes disponibles |
| `/api/planes` | POST | 🔐 Admin | Crear nuevo plan |
| `/api/planes/{id}` | DELETE | 🔐 Admin | Eliminar plan |

### 🏢 Sucursales

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/sucursales` | GET | 🌐 Público | Ver sucursales activas |
| `/api/sucursales/all` | GET | 🔐 Admin | Ver todas (incluidas inactivas) |
| `/api/sucursales/{id}` | GET | 🌐 Público | Detalles de sucursal |
| `/api/sucursales/{id}` | DELETE | 🔐 Admin | Desactivar sucursal |

### 🏠 Salas

| Endpoint | Método | Autorización | Descripción |
|----------|--------|--------------|-------------|
| `/api/salas` | GET | 🌐 Público | Ver todas las salas |
| `/api/salas/sucursal/{id}` | GET | 🌐 Público | Salas de una sucursal |
| `/api/salas/{id}` | GET | 🌐 Público | Detalles de sala |
| `/api/salas/{id}` | DELETE | 🔐 Admin | Desactivar sala |

**Leyenda:**
- 🌐 Público: Sin autenticación requerida
- 🔒 Autenticado: Requiere token JWT válido
- 🔐 Admin: Solo administradores
- 🔒 Owner/Admin: Usuario propietario o administrador

---

## 📦 Contratos (DTOs)

### Requests

- `RegisterRequest`: Registro de nuevos usuarios
- `LoginRequest`: Autenticación
- `CreateClaseRequest`: Creación de clases
- `CreateReservaRequest`: Nueva reserva
- `CreatePlanRequest`: Nuevo plan
- `CreateMembresiaRequest`: Nueva membresía
- `CreateAlumnoRequest`: Registro de alumno
- `CreateProfesorRequest`: Registro de profesor
- `UpdateAlumnoRequest`: Actualización de datos de alumno
- `UpdateProfesorRequest`: Actualización de datos de profesor

### Responses

- `UsuarioResponse`: Datos públicos de usuario
- `AlumnoResponse`: Perfil de alumno
- `AlumnoPerfilResponse`: Perfil completo (membresía + reservas)
- `ProfesorResponse`: Perfil de profesor
- `ClaseResponse`: Información de clase con cupos
- `ReservaResponse`: Confirmación de reserva
- `PlanResponse`: Detalles de plan
- `MembresiaResponse`: Estado de membresía
- `SucursalResponse`: Información de sucursal
- `SalaResponse`: Información de sala (incluye tipo y estado activo)

---

## 🗑️ Operaciones de Eliminación

El sistema implementa diferentes estrategias de eliminación según la entidad:

### Desactivación (Soft Delete)

Estas entidades se **desactivan** en lugar de eliminarse físicamente:

- **Usuarios** (`Activo = false`):
  - Mantiene historial de auditoría
  - Preserva relaciones con reservas y membresías
  - Solo administradores pueden desactivar

- **Sucursales** (`Activa = false`):
  - Oculta de listados públicos
  - Visible en endpoint `/api/sucursales/all` (solo admin)
  - Solo administradores pueden desactivar

- **Salas** (`Activa = false`):
  - Se excluyen de nuevas reservas
  - Clases existentes no se afectan
  - Solo administradores pueden desactivar

### Eliminación Física (Hard Delete)

Estas entidades se **eliminan físicamente** de la base de datos:

- **Planes**:
  - Solo si no tienen membresías asociadas
  - Requiere permisos de administrador

- **Clases**:
  - Solo profesores y administradores
  - Elimina reservas asociadas en cascada
  - Notifica a alumnos afectados

- **Reservas**:
  - Cancelación por el alumno dueño o administrador
  - Libera cupo en la clase
  - Genera notificación de cancelación

### Matriz de Permisos de Eliminación

| Entidad | Método | Tipo | Rol Requerido | Endpoint |
|---------|--------|------|---------------|----------|
| Usuario | DELETE | Desactivación | Admin | `/api/usuarios/{id}` |
| Sucursal | DELETE | Desactivación | Admin | `/api/sucursales/{id}` |
| Sala | DELETE | Desactivación | Admin | `/api/salas/{id}` |
| Plan | DELETE | Física | Admin | `/api/planes/{id}` |
| Clase | DELETE | Física | Profesor/Admin | `/api/clases/{id}` |
| Reserva | DELETE | Física | Autenticado | `/api/reservas/{id}` |

---

## ▶️ Cómo ejecutar

### Prerrequisitos

- .NET 8 SDK
- SQLite

### Instalación


```bash
# 1. Clonar el repositorio
git clone https://github.com/FranciscoCuminiLondero/gym-management-api.git
cd gym-management-api

# 2. Restaurar dependencias
dotnet restore

# 3. Aplicar migraciones
dotnet ef database update --project Infrastructure --startup-project Api

# 4. Ejecutar la aplicación
dotnet run --project Api
```

La API estará disponible en:
- HTTPS: `https://localhost:7253`
- HTTP: `http://localhost:5253`
- Swagger: `https://localhost:7253/swagger`

---

## 🧪 Usando la API con Swagger

### 1. Accede a Swagger UI
Abre tu navegador en `https://localhost:7253/swagger`

### 2. Autenticación

#### Paso 1: Login
1. Expande `POST /api/auth/login`
2. Click en "Try it out"
3. Usa las credenciales admin:
```json
{
  "email": "admin@gym.com",
  "password": "Admin123!"
}
```
4. Ejecuta y **copia el token** de la respuesta

#### Paso 2: Autorizar en Swagger
1. Click en el botón **"Authorize" 🔓** (arriba a la derecha)
2. Pega el token (sin "Bearer", Swagger lo agrega automáticamente)
3. Click "Authorize"
4. Click "Close"

#### Paso 3: Usar Endpoints Protegidos
Ahora puedes usar todos los endpoints que requieren autenticación ✅

### 3. Ejemplos de Flujo Completo

#### Crear un Alumno Nuevo
```json
POST /api/auth/register

{
  "nombre": "Juan",
  "apellido": "Pérez",
  "email": "juan@example.com",
  "password": "Pass123!",
  "dni": "12345678",
  "telefono": "555-1234",
  "fechaNacimiento": "2000-01-15",
  "role": "Alumno",
  "planId": 1
}
```

#### Crear un Profesor (requiere ser Admin)
```json
POST /api/auth/register
Authorization: Bearer {admin-token}

{
  "nombre": "Carlos",
  "apellido": "Rodríguez",
  "email": "carlos.prof@gym.com",
  "password": "Profesor123!",
  "dni": "98765432",
  "telefono": "555-0123",
  "fechaNacimiento": "1985-03-15",
  "role": "Profesor"
}
```

#### Ver Sucursales y Salas Disponibles
```bash
GET /api/sucursales        # Sucursales activas
GET /api/salas             # Todas las salas
GET /api/salas/sucursal/1  # Salas de sucursal específica
```

#### Crear una Clase (como Profesor o Admin)
```json
POST /api/clases
Authorization: Bearer {profesor-token}

{
  "profesorId": 1,
  "salaId": 1,
  "sucursalId": 1,
  "nombre": "Yoga Matutino",
  "descripcion": "Clase de yoga para principiantes",
  "duracionMinutos": 60,
  "horaInicio": "08:00:00",
  "fecha": "2025-10-25",
  "capacidad": 20
}
```

#### Ver Todas las Clases Activas
```bash
GET /api/clases
Authorization: Bearer {token}
```

#### Reservar una Clase (como Alumno)
```json
POST /api/reservas
Authorization: Bearer {alumno-token}

{
  "alumnoId": 3,
  "claseId": 1
}
```

#### Cancelar una Reserva
```bash
DELETE /api/reservas/1
Authorization: Bearer {alumno-token}
```

#### Desactivar una Sucursal (solo Admin)
```bash
DELETE /api/sucursales/1
Authorization: Bearer {admin-token}
```

#### Eliminar una Clase (Profesor o Admin)
```bash
DELETE /api/clases/1
Authorization: Bearer {profesor-token}
```

#### Actualizar Datos de Alumno
```json
PUT /api/alumnos/3
Authorization: Bearer {alumno-token}

{
  "nombre": "Juan Carlos",
  "telefono": "555-9999",
  "email": "juancarlos@example.com"
}
```

#### Actualizar Datos de Profesor
```json
PUT /api/profesores/2
Authorization: Bearer {profesor-token}

{
  "nombre": "Carlos Eduardo",
  "apellido": "Rodríguez García",
  "telefono": "555-8888"
}
```

---

## 🗃️ Base de Datos

### Datos Iniciales (Seed Data)

El sistema incluye datos de prueba:

**Usuarios:**
- Admin: `admin@gym.com` / `Admin123!`

**Sucursales:**
1. Sucursal Centro - Av. Principal 123
2. Sucursal Norte - Calle Norte 456

**Salas:**
1. Sala A (Multiuso - Sucursal Centro) - Capacidad: 25
2. Sala B (Spinning - Sucursal Centro) - Capacidad: 20
3. Sala 1 (Funcional - Sucursal Norte) - Capacidad: 30
4. Sala 2 (Pesas - Sucursal Norte) - Capacidad: 40

### Migraciones

```bash
# Crear nueva migración
dotnet ef migrations add NombreMigracion --project Infrastructure --startup-project Api

# Aplicar migraciones
dotnet ef database update --project Infrastructure --startup-project Api

# Revertir última migración
dotnet ef database update PreviousMigration --project Infrastructure --startup-project Api

# Eliminar última migración (si no se aplicó)
dotnet ef migrations remove --project Infrastructure --startup-project Api
```

---

## 📚 Documentación Adicional

El proyecto incluye documentación detallada en archivos markdown:

- **`AUTORIZACION-GUIA.md`**: Guía completa de autorización por roles
  - Ejemplos de uso de atributos `[Authorize]`
  - Políticas personalizadas
  - Validación manual de permisos
  - Mejores prácticas de seguridad

- **`SWAGGER-JWT-GUIA.md`**: Tutorial de uso de Swagger con JWT
  - Cómo autenticarse en Swagger
  - Uso del botón "Authorize"
  - Troubleshooting común
  - Ejemplos visuales paso a paso

---

## 🔒 Seguridad

### Matriz de Permisos

| Usuario | Crear Alumno | Crear Profesor | Crear Clase | Ver Todos Usuarios | Ver Propio Perfil | Modificar Propio Perfil | Eliminar Entidades |
|---------|--------------|----------------|-------------|-------------------|-------------------|------------------------|-------------------|
| Sin autenticar | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| Alumno | ✅ | ❌ | ❌ | ❌ | ✅ | ✅ | ❌ (solo propias reservas) |
| Profesor | ✅ | ❌ | ✅ | ❌ | ✅ | ✅ | ✅ (propias clases) |
| Admin | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ (todas) |

### Tokens JWT

- **Expiración**: 1 hora
- **Algoritmo**: HS256
- **Claims incluidos**: 
  - `NameIdentifier`: ID del usuario
  - `Email`: Email del usuario
  - `Role`: Rol del usuario

### Validaciones Implementadas

- Email único por usuario
- Passwords encriptados con SHA256
- Validación de roles en endpoints sensibles
- Validación de ownership (usuarios solo ven su propia información)
- Verificación de permisos en operaciones críticas

---

## 🧩 Patrones y Principios

### Clean Architecture

El proyecto sigue estrictamente los principios de Clean Architecture:

- **Independencia de Frameworks**: La lógica de negocio no depende de EF Core
- **Testeable**: Lógica de negocio separada de infraestructura
- **Independencia de UI**: La API puede cambiar sin afectar el dominio
- **Independencia de BD**: Se puede cambiar SQLite por otra BD fácilmente

### Repository Pattern

Cada entidad tiene su repositorio con interfaz en `Application` e implementación en `Infrastructure`:

```csharp
// Interfaz (Application)
public interface IAlumnoRepository : IBaseRepository<Alumno>
{
    List<Alumno> GetActivos();
}

// Implementación (Infrastructure)
public class AlumnoRepository : BaseRepository<Alumno>, IAlumnoRepository
{
    public List<Alumno> GetActivos() => GetByCriterial(a => a.Activo);
}
```

### Dependency Injection

Todos los servicios y repositorios están registrados en `Program.cs`:

```csharp
builder.Services.AddScoped<IAlumnoService, AlumnoService>();
builder.Services.AddScoped<IAlumnoRepository, AlumnoRepository>();
// ...
```

---


## 👥 Autor

**Francisco Cumini Londero**  
Proyecto Final - Programación IV

---

## 📄 Licencia

Este proyecto fue desarrollado con fines educativos como parte del curso de Programación IV.

---

## 🤝 Contribuciones

Si deseas contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -m 'Agrega nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request
