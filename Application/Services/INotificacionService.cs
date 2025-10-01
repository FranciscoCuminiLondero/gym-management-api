namespace Application.Services
{
    public interface INotificacionService
    {
        void NotificarReservaConfirmada(int alumnoId, int claseId);
        void NotificarNuevaReservaAlProfesor(int profesorId, int alumnoId, int claseId);
    }
}
