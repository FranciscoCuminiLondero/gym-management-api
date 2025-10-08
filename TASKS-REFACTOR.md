Checklist refactor Usuario (sin migraciones)

- [x] Introducir entidad `Usuario` base y hacer que `Alumno` y `Profesor` hereden de ella
- [x] Añadir `Contract/Responses/UsuarioResponse.cs` (DTO común)
- [x] Crear `Application/Abstractions/IUsuarioRepository.cs` con métodos básicos (GetByEmail, GetById, ExistsByEmail, GetDtoByEmail/GetDtoById, GetWithPasswordByEmail)
- [x] Implementar `Infrastructure/Persistence/Repositories/UsuarioRepository.cs` (devuelve Alumno/Profesor según email/id)
- [x] Crear `Application/Services/IUsuarioService.cs` y `UsuarioService.cs` (delegación al repo)
- [x] Añadir `DbSet<Usuario>` y mapping TPH en `Infrastructure/Persistence/GymDbContext.cs` (preparado, migración pendiente)
- [x] Añadir seed admin en `GymDbContext` (seed agregado en código)
- [x] Añadir `GetWithPasswordByEmail` para autenticación (ya implementado)
- [x] Añadir `IsActivo(int id)` a `IUsuarioRepository` e implementar en `UsuarioRepository`
- [x] Reemplazar llamadas a actividad de alumno por `IUsuarioRepository.IsActivo` en los servicios:
  - [x] `ReservaService` (reemplazado)
  - [x] `MembresiaService` (reemplazado)
 - [x] Mover `HasMembresiaActiva` a `IUsuarioRepository` y usarla desde `ReservaService` (implementado). La firma y la implementación en `AlumnoRepository` fueron eliminadas; ahora `HasMembresiaActiva` existe sólo en `IUsuarioRepository`.

Tareas pendientes / recomendadas (a evaluar y completar):

- [ ] Revisar y normalizar inyecciones y nombres de campos en `AuthService` y otros servicios (correcciones de nombres realizadas: `_profesorRepository`, `_planRepository`)
- [ ] Decidir qué métodos deben ser exclusivos de `IAlumnoRepository` / `IProfesorRepository` y cuáles deben moverse a `IUsuarioRepository` (recomendado: ExistsByEmail, IsActivo, GetDtoById/GetDtoByEmail)
- [ ] Pasada global para reemplazar todos los call-sites que deberían usar `IUsuarioRepository` (por ejemplo si quieres consolidar comprobación de actividad y existencia por email)
- [ ] Ejecutar `dotnet build` y corregir errores de compilación en tu entorno local (he verificado estático: no se encontraron errores en el workspace después de los cambios)
- [ ] Generar migración EF Core localmente y revisar el script antes de aplicar (backup de la DB recomendado)
- [ ] Revisar controladores/serialización que devuelven `AlumnoResponse` / `ProfesorResponse` y decidir si algunos endpoints deberían devolver `UsuarioResponse` en su lugar
- [ ] Pasada de limpieza: eliminar interfaces/implementaciones que queden sin referencias (hacerlo solo cuando no existan más referencias)

Notas:
- No borré `IAlumnoRepository`, `AlumnoRepository`, `IProfesorRepository`, `ProfesorRepository` ni migraciones existentes. Mantener esas piezas permite un refactor seguro y gradual.
- Cambios aplicados localmente en esta rama (`refactor/new-entity-usuario`): añadidos/actualizados de archivos mencionados arriba. Revisa y confirma antes de generar/aplicar migraciones.
- Recomendación de backup DB antes de aplicar migraciones: copia `Api/gym.db` a `Api/gym.db.bak`.

Comandos sugeridos (ejecutar localmente):

```powershell
# build
dotnet build .\GymManagement.sln

# crear migración (desde la raíz del repo)
dotnet ef migrations add AddUsuarioTphAndSeedAdmin -p .\Infrastructure\Infrastructure.csproj -s .\Api\Presentation.csproj --context GymDbContext

# revisar los archivos generados en Infrastructure/Migrations

# aplicar migración
dotnet ef database update -p .\Infrastructure\Infrastructure.csproj -s .\Api\Presentation.csproj --context GymDbContext
```

Si quieres, puedo:
- mover más métodos a `IUsuarioRepository` y reemplazar usos en más servicios (por ejemplo `AuthService` creación/registro),
- o dejar la mezcla actual (repositorios por entidad + repo usuario para operaciones transversales) y preparar una segunda pasada para eliminación segura.

Fin del checklist.
