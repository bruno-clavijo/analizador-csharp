using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace analizador_csharp
{
    public class Inicio
    {
        public Inicio()
        {

        }

        public bool IniciarAnalisis(int AplicacionID, int[] Analizadores, string Ruta)
        {
            var extra = new ExtraerArchivos();
            var comentarios = new Comentarios();
            var Archivos = new HashSet<string>();

            // Extraer archivos a analizar
            foreach (var analizador in Analizadores)
            {
                Archivos = extra.ListaArchivos(analizador, Ruta);


            // Recorrer cada linea de archivo
                foreach (string archivo in Archivos)
                {

                    var fileStream = new FileStream(archivo, FileMode.Open, FileAccess.Read);
                    
                    string nombreArchivo = Path.GetFileName(Ruta);
                    nombreArchivo = Regex.Replace(nombreArchivo, @"\.\w+", string.Empty).Trim();
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            string lineaCodigo = streamReader.ReadLine().Trim();

                            //Contar No. Linea
                            ++NoLinea;

                            if (!string.IsNullOrEmpty(lineaCodigo))
                            {
                                //Actualizar el nuevo VerificaComentarios
                                lineaCodigo = comentarios.EsComentario(lineaCodigo);
                                if (!String.IsNullOrEmpty(lineaCodigo))
                                {
                                    datosLinea.ObtenerSalida(lineaCodigo, Archivo, InventarioCM, Resultado, NoLinea, Ruta, Librerias);
                                }
                            }
                        }
                    }
                     
                }

            }


            return true;
        }
    }
}
