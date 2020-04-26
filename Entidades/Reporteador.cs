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
        public Dictionary<string, IEnumerable<object> > GetPromedioAlumXAsg()
        {
            var dictionary = new Dictionary<string, IEnumerable<object>>();

            var dicEvalXAsig =  GetEvaluxAsig();


            foreach (var item in dicEvalXAsig)
            {
                var a = from ev in item.Value
                        group ev by new
                        { 
                            ev.Alumno.UniqueId,
                            ev.Nota
                        }
                        into  grupoEvalAlum
                        select new 
                        {
                            AlumnoId = grupoEvalAlum.Key,
                            Promedio = grupoEvalAlum.Average( evalucion => evalucion.Nota )
                        };
            }


            return dictionary;
        }
    }
}