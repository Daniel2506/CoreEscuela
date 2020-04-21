using System;

namespace CoreEscuela.Entidades
{
    public class Evaluacion : Base
    {
        public Asignatura Asignatura { get; set; }
        public Alumno Alumno { get; set; }
        public float Nota { get; set; }
    }
}