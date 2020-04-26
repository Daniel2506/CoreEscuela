using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }
        public EscuelaEngine()
        {
            Inicializar();
        }

        private void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", "Colombia", "Bogota", 2012, TipoEscuela.Primaria);
            LoadCorses();

            foreach (var curso in Escuela.Cursos)
            {
                Random cantRandom = new Random();
                curso.Asignaturas = LoadAsignaturas();
                curso.Alumnos = GenerateAlumnosRandom(cantRandom.Next(5, 20));
                LoadEvaluaciones(curso);
            }
        }
        public IReadOnlyList<Base> GetObjectsBase(bool traeEvaluaciones = true, bool traeAlumnos = true, bool traeAsignaturas = true, bool traeCursos = true)
        {
            var listObj = new List<Base>();
            var conteoEvaluaciones = 0;
            listObj.Add(Escuela);

            if (traeCursos)
                listObj.AddRange(Escuela.Cursos);

            foreach (var curso in Escuela.Cursos)
            {
                if (traeAsignaturas)
                    listObj.AddRange(curso.Asignaturas);

                if (traeAlumnos)
                    listObj.AddRange(curso.Alumnos);


                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += conteoEvaluaciones;
                    }
                }
            }

            return listObj.AsReadOnly();
        }

        public Dictionary<LlaveDiccionario, IEnumerable<Base>> GetDictionary()
        {
            var dictionary = new Dictionary<LlaveDiccionario, IEnumerable<Base>>();

            dictionary.Add(LlaveDiccionario.Escuela, new[] { Escuela });
            dictionary.Add(LlaveDiccionario.Cursos, Escuela.Cursos.Cast<Base>());

            var tempListAsi = new List<Asignatura>();
            var tempListAl = new List<Alumno>();
            var tempListEv = new List<Evaluacion>();

            foreach (var curso in Escuela.Cursos)
            {
                tempListAsi.AddRange(curso.Asignaturas);
                tempListAl.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    tempListEv.AddRange(alumno.Evaluaciones);
                }

            }
            dictionary.Add(LlaveDiccionario.Asignaturas, tempListAsi.Cast<Base>());
            dictionary.Add(LlaveDiccionario.Alumnos, tempListAl.Cast<Base>());
            dictionary.Add(LlaveDiccionario.Evaluaciones, tempListEv.Cast<Base>());

            return dictionary;
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<Base>> dic , bool impEval = false)
        {
            foreach (var key in dic)
            {
                Printer.WriteTitle(key.Key.ToString());

                foreach (var valor in key.Value)
                {   
                    switch (key.Key)
                    {
                        case LlaveDiccionario.Evaluaciones :

                            if (impEval)
                                Console.WriteLine(valor);
                        break;
                        
                        case LlaveDiccionario.Alumnos:
                                Console.WriteLine(valor.Nombre);
                        break;

                        case LlaveDiccionario.Cursos:
                                var curtemp = valor as Curso;
                                if(curtemp != null)
                                {
                                    Console.WriteLine($"NOMBRE: {curtemp.Nombre}, # numero alumnos: {curtemp.Alumnos.Count()}");

                                }
                        break;

                        default: 
                            Console.WriteLine(valor);
                        break;
                    }   
                }
            }
        }



        #region  Metodos de carga
        private void LoadEvaluaciones(Curso curso)
        {
            var rnd = new Random(System.Environment.TickCount);
            foreach (var asignatura in curso.Asignaturas)
            {
                foreach (var alumno in curso.Alumnos)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var ev = new Evaluacion
                        {
                            Asignatura = asignatura,
                            Alumno = alumno,
                            Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                            Nota = (float) Math.Round((5 * rnd.NextDouble()), 2)
                        };
                        alumno.Evaluaciones.Add(ev);
                    }
                }
            }
        }

        private List<Asignatura> LoadAsignaturas()
        {
            List<Asignatura> listaAsignaturas = new List<Asignatura>(){
                new Asignatura{ Nombre = "Math" },
                new Asignatura{ Nombre = "Spanish" },
                new Asignatura{ Nombre = "Bioligy" },
                new Asignatura{ Nombre = "Geography" }
            };
            return listaAsignaturas;
        }

        private List<Alumno> GenerateAlumnosRandom(int cantAlumnos)
        {
            string[] FirstName = { "Laura", "Marcela", "Daniel", "Manuela", "Santiago", "Rosa", "Stella", "Juan", "Felipe" };
            string[] LastName = { "Trump", "Perez", "Guzman", "Fonseca", "Ruiz", "Roa", "Suarez", "Tovar", "Rodriguez" };
            string[] SecondName = { "Maria", "Nicolas", "Luis", "Carlos", "Ana", "Viviana", "Jacobo", "Andres", "Consuelo" };

            var listaAlumnos = from n1 in FirstName
                               from n2 in SecondName
                               from a1 in LastName
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((a) => a.UniqueId).Take(cantAlumnos).ToList();
        }

        private void LoadCorses()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso(){ Nombre = "101", TipoCurso = TipoCurso.Mañana},
                new Curso(){ Nombre = "201", TipoCurso = TipoCurso.Mañana},
                new Curso(){ Nombre = "301", TipoCurso = TipoCurso.Mañana},
                new Curso(){ Nombre = "401", TipoCurso = TipoCurso.Tarde},
                new Curso(){ Nombre = "501", TipoCurso = TipoCurso.Tarde}
            };
        }
        #endregion
    }
}