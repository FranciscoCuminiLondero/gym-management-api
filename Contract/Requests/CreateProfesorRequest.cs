﻿namespace Contract.Requests
{
    public class CreateProfesorRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; } 
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
