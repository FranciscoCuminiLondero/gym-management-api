Checklist refactor Usuario (sin migraciones)

- [x] Introducir entidad `Usuario` y hacer que `Alumno` y `Profesor` hereden de ella
- [x] Añadir `Contract/Responses/UsuarioResponse.cs` (DTO común)
- [x] Crear `Application/Abstractions/IUsuarioRepository.cs` con métodos básicos (GetByEmail, GetById, ExistsByEmail, GetDtoByEmail/GetDtoById, GetWithPasswordByEmail)
- [x] Implementar `Infrastructure/Persistence/Repositories/UsuarioRepository.cs`
- [x] Crear `Application/Services/IUsuarioService.cs` y `UsuarioService.cs`
- [x] Añadir `DbSet<Usuario>` y mapping TPH en `Infrastructure/Persistence/GymDbContext.cs` (código preparado)
- [x] Añadir seed admin en `GymDbContext`
- [x] Añadir `GetWithPasswordByEmail` para autenticación
- [x] Añadir `IsActivo(int id)` a `IUsuarioRepository` e implementar en `UsuarioRepository`
- [x] Mover `HasMembresiaActiva` a `IUsuarioRepository` y usarla desde `ReservaService`
- [x] Reemplazar llamadas relevantes en servicios para usar `IUsuarioRepository` (pasada global realizada)
- [x] Añadir `UsuariosController` y centralizar endpoints resumen; eliminar duplicados en `AlumnosController`/`ProfesoresController`
- [x] Añadir paginación y filtrado a `GET /api/usuarios`

Pendientes:

- [ ] Ejecutar `dotnet build` y corregir errores de compilación en entorno local
- [ ] Generar migración EF Core `AddUsuarioTphAndSeedAdmin` y revisar Up/Down
- [ ] Aplicar migración en entornos con backup de la BD
- [ ] Revisar y normalizar inyecciones y nombres de campos en servicios (p.ej. `AuthService`)
- [ ] Revisar controladores/serialización que devuelven `AlumnoResponse` / `ProfesorResponse` y decidir cambios
- [ ] Pasada de limpieza final: eliminar interfaces/implementaciones no referenciadas
- [ ] Añadir pruebas unitarias relevantes (repositorio y paginación)
- [ ] Añadir CI básico (build en PRs)
- [ ] Añadir `Api/smoke-tests.http` con requests críticos para pruebas manuales

