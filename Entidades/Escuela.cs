using System;
using System.Collections.Generic;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Escuela : Base, ILugar
    {
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public int AnoCreacion { get; set; }
        public TipoEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set;}

        public Escuela(string nombre, string pais, string ciudad, int ano, TipoEscuela tipo)
        {
            this.Nombre = nombre;
            this.Pais = pais;
            this.Ciudad = ciudad;
            this.AnoCreacion = ano;
        }

        public override string ToString()
        {
            return $"Nombre: {this.Nombre} \nTipo Escuela: {this.TipoEscuela} \nPais: {this.AnoCreacion} \nCiudad: {this.Ciudad}";
        }

        public void LimpiarLugar()
        {
            //
            Printer.WriteTitle("LIMPIANDO ESTABLECIMIENTO");
        }
    }
}