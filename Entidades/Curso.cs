using System;
using System.Collections.Generic;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Curso : Base, ILugar
    {
        public TipoCurso TipoCurso { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }

        public string Direccion { get; set; }

        public void LimpiarLugar()
        {
            Printer.WriteTitle("CURSO: LIMPIANDO ESTABLECIMIENTO");
        }
    }
}