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

## 📁 Estructura del Dominio

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

---

## Entidades y Contratos

El sistema sigue una arquitectura limpia con separación clara de capas:

- **Domain**: Contiene las entidades del negocio (`Alumno`, `Profesor`, `Clase`, `Membresia`, etc.).
- **Contract**: Define los DTOs (Data Transfer Objects) que la API expone:
  - **Alumno**:  
    - `CreateAlumnoRequest`: datos necesarios para registrar un nuevo alumno.  
    - `AlumnoResponse`: información devuelta tras la creación.

  - **Profesor**:  
    - `CreateProfesorRequest`: datos básicos para dar de alta un profesor.  
    - `ProfesorResponse`: perfil público del profesor.

  - **Reserva**:  
    - `CreateReservaRequest`: vincula un alumno a una clase específica.  
    - `ReservaResponse`: confirma la reserva con estado y fecha.

---

## ▶️ Cómo ejecutar

```bash
git clone https://github.com/tu-usuario/gym-management-api.git
cd gym-management-api
dotnet run --project GymManagement.Presentation
