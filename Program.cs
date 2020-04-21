using System;
using System.Collections.Generic;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var engine = new EscuelaEngine();
            //engine.Inicializar();
            
            //Printer.Ring();
            ImprimirCursos(engine.Escuela);
        }

        private static void ImprimirCursos(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de escuela");
        
            if (escuela?.Cursos != null)
            {
                foreach (var item in escuela.Cursos)
                {
                    Console.WriteLine($"Nombre: {item.Nombre}, Id: {item.UniqueId}, Jornada: {item.TipoCurso}");        
                }
            }
        }
    } 
}