﻿namespace BibliotecaAPI.Entidades
{
    public class Autor : IHaveId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
    }
}
