# Sistema de Gestión de Gimnasios

> **Proyecto Final – Programación IV**  
> Una API RESTful para la gestión de gimnasios, desarrollada con **Clean Architecture**, **ASP.NET Core**, **C#** y **SQLite**.

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
├── GymManagement.Presentation → Web API (controladores, configuración, DI)
├── GymManagement.Application → Casos de uso, servicios, mapeo, interfaces de repositorio
├── GymManagement.Domain → Entidades del negocio y lógica central
├── GymManagement.Contract → DTOs (requests/responses) y contratos públicos
└── GymManagement.Infrastructure → Implementaciones (EF Core, repositorios, servicios externos)
```

## 🛠️ Tecnologías

- **Lenguaje**: C# 12
- **Framework**: .NET 8
- **Base de datos**: SQLite (para desarrollo)
- **ORM**: Entity Framework Core
- **Arquitectura**: Clean Architecture

---

## 📁 Estructura del Dominio (en construcción)

Las entidades principales del sistema son:

- `Alumno`
- `Profesor`
- `Sucursal`
- `Sala`
- `Clase`
- `Reserva`
- `Membresia`
- `Pago`
- `Notificacion`
- `Auditoria`

> ✅ **Estado actual**: Se está implementando la entidad `Alumno` y su persistencia básica.

---

## ▶️ Cómo ejecutar

```bash
git clone https://github.com/tu-usuario/gym-management-api.git
cd gym-management-api
dotnet run --project GymManagement.Presentation
