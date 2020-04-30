using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            //EVENTS
            AppDomain.CurrentDomain.ProcessExit += AccionEvent;
            AppDomain.CurrentDomain.ProcessExit += (ob, e) => Printer.WriteTitle("EXPRESION LAMBDA");


            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var engine = new EscuelaEngine();


            /// Reporteador
            var reporteador = new Reporteador(engine.GetDictionary());
            var evalList = reporteador.getEvaluaciones();
            var asgList = reporteador.GetAsignaturas();
            var evaXAsgList = reporteador.GetEvaluxAsig();
            var promEvaXAsgList = reporteador.GetPromedioAlumXAsg();
            var topList = reporteador.GetTopPromedio(5, "Math");
        
            //ImprimirCursos(engine.Escuela);
            
            var listaObjetos = engine.GetObjectsBase();

            /*var listaILugar = from obj  in listaObjetos
                              where  obj is ILugar
                              select (ILugar) obj;*/

            //engine.Escuela.LimpiarLugar();

            var dictionary = engine.GetDictionary();
            //engine.ImprimirDiccionario(dictionary, true);
        }

        private static void AccionEvent(object sender, EventArgs e)
        {
            Printer.WriteTitle("SALIENDO");
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