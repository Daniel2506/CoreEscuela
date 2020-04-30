using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreEscuela.Entidades
{
    public class Reporteador
    {
        private Dictionary<LlaveDiccionario, IEnumerable<Base>> _dictionary;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<Base>> dictionary)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            this._dictionary = dictionary;
        }


        public IEnumerable<Evaluacion> getEvaluaciones()
        {
            var rta = _dictionary.TryGetValue(LlaveDiccionario.Evaluaciones, out IEnumerable<Base> lista);

            if(rta)
                return lista.Cast<Evaluacion>();
            else
                return new List<Evaluacion>();           
        }

        public IEnumerable<string> GetAsignaturas()
        {
            var listaEvaluaciones =  getEvaluaciones();

            return (from ev in listaEvaluaciones
                   select ev.Asignatura.Nombre).Distinct(); 
        }

        public IEnumerable<string> GetAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones =  getEvaluaciones();

            return (from ev in listaEvaluaciones
                   select ev.Asignatura.Nombre).Distinct(); 
        }

        public Dictionary<string, IEnumerable<Evaluacion> > GetEvaluxAsig()
        {
            var dictionary = new Dictionary<string, IEnumerable<Evaluacion>>();

            var asignaturas = GetAsignaturas(out var listaEvaluaciones);


            foreach (var asignatura in asignaturas)
            {
                var evaluacion =  from ev in listaEvaluaciones
                                  where ev.Asignatura.Nombre ==  asignatura
                                  select ev;

                dictionary.Add(asignatura, evaluacion);
            }
            return dictionary;
        }
        public Dictionary<string, IEnumerable<AlumnoPromedio> > GetPromedioAlumXAsg()
        {
            var dictionary = new Dictionary<string, IEnumerable<AlumnoPromedio>>();

            var dicEvalXAsig =  GetEvaluxAsig();


            foreach (var item in dicEvalXAsig)
            {
                var promediosAlumnos = from ev in item.Value
                                        group ev by new
                                        { 
                                            ev.Alumno.UniqueId,
                                            ev.Alumno.Nombre
                                        }
                                        into  grupoEvalAlum
                                        select new AlumnoPromedio
                                        {
                                            AlumnoNombre = grupoEvalAlum.Key.Nombre,
                                            AlumnoId = grupoEvalAlum.Key.UniqueId,
                                            Promedio = grupoEvalAlum.Average( evalucion => evalucion.Nota )
                                        };
                dictionary.Add(item.Key, promediosAlumnos);
            }


            return dictionary;
        }

        public IEnumerable<AlumnoPromedio> GetTopPromedio(int tpo, string materia)
        {
            var list = new List<AlumnoPromedio>();

            var listPromedios = GetPromedioAlumXAsg();

            foreach (var item in listPromedios.Where(key => key.Key == materia))
            {
                list.AddRange( (from t in item.Value
                        where item.Key == materia
                        orderby t.Promedio descending
                        select t).Take(tpo).ToList()) ;  
                        Console.WriteLine("hey");
            }

            return list;
        }
    }
}