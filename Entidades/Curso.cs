using System;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Curso : Base
    {
        public TipoCurso TipoCurso { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }
    }
}