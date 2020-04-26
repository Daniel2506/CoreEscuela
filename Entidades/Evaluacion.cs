using System;

namespace CoreEscuela.Entidades
{
    public class Evaluacion : Base
    {
        public Asignatura Asignatura { get; set; }
        public Alumno Alumno { get; set; }
        public float Nota { get; set; }

        public override string ToString() 
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}