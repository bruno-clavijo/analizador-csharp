using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace analizador_csharp
{
    class Comentarios
    {
        public bool EsComentario(string lineaCodigo)
        {
            bool resultado = false;

            if (lineaCodigo.StartsWith("//") || lineaCodigo.StartsWith("/*") || lineaCodigo.StartsWith("*/") ||
                lineaCodigo.StartsWith("<!--") || lineaCodigo.StartsWith("'"))
            {
                resultado = true;
            }
            return resultado;
        }
    }
}
