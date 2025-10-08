using Application.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IAlumnoRepository _alumnoRepo;
        private readonly IProfesorRepository _profesorRepo;

        public UsuarioService(IAlumnoRepository alumnoRepo, IProfesorRepository profesorRepo)
        {
            _alumnoRepo = alumnoRepo;
            _profesorRepo = profesorRepo;
        }

        public Usuario? GetByEmail(string email)
        {
            var alumno = _alumnoRepo.GetByCriterial(a => a.Email == email).FirstOrDefault();
            if (alumno != null) return alumno;

            var profesor = _profesorRepo.GetByCriterial(p => p.Email == email).FirstOrDefault();
            if (profesor != null) return profesor;

            return null;
        }

        public Usuario? GetById(int id)
        {
            var alumno = _alumnoRepo.GetById(id);
            if (alumno != null) return alumno;

            var profesor = _profesorRepo.GetById(id);
            if (profesor != null) return profesor;

            return null;
        }

        public bool ExistsByEmail(string email)
        {
            return _alumnoRepo.ExistsByEmail(email) || _profesorRepo.ExistsByEmail(email);
        }
    }
}
