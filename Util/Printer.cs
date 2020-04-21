using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }
        public static void WriteTitle(string title)
        {
            var tam = title.Length + 4;

            DrawLine(tam);
            WriteLine($"|  {title}  |");
            DrawLine(tam);
        }

        public static void Ring(int hz = 2000, int time = 500, int cant = 1)
        {
            while (cant > 0)
            {
                Beep(hz, time); 
                cant --; 
            }
        }
    }
}