using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace analizador_csharp
{
    class Program 
    {
        static void Main(string[] args)
        {
            var ini = new Inicio();
            int[] arr = { 5 };

            bool re = ini.IniciarAnalisis(2, arr, "D:\\Coppel\\CosultaeImpresiones");

        }
    }
}