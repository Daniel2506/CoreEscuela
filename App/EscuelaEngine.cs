using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }
        public EscuelaEngine()
        {
            Inicializar();
        }
 
        private void Inicializar ()
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

        private void LoadEvaluaciones(Curso curso)
        {
            foreach (var asignatura in curso.Asignaturas)
            {
                foreach (var alumno in curso.Alumnos)
                {
                    var rnd = new Random(System.Environment.TickCount);
                    for (int i = 0; i < 5; i++)
                    {
                        var ev = new Evaluacion
                        {
                            Asignatura = asignatura,
                            Alumno = alumno,
                            Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                            Nota = (float)(5 * rnd.NextDouble())
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
            string[] FirstName = {"Laura", "Marcela", "Daniel", "Manuela", "Santiago", "Rosa", "Stella", "Juan", "Felipe"};
            string[] LastName = {"Trump", "Perez", "Guzman", "Fonseca", "Ruiz", "Roa", "Suarez", "Tovar", "Rodriguez"};
            string[] SecondName = {"Maria", "Nicolas", "Luis", "Carlos", "Ana", "Viviana", "Jacobo", "Andres", "Consuelo"};
            
            var listaAlumnos = from n1 in FirstName
                               from n2 in SecondName         
                               from a1 in LastName
                               select new Alumno {Nombre = $"{n1} {n2} {a1}"};

            return listaAlumnos.OrderBy( (a) => a.UniqueId ).Take(cantAlumnos).ToList();                               
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
    }
}