using System;

namespace CoreEscuela.Entidades
{
    public abstract class  Base
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public Base()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre}, {UniqueId}";
        }
    }
}