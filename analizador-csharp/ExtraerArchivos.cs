using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace analizador_csharp
{
    class ExtraerArchivos
    {
        public HashSet<string> ListaArchivos(int Analizador, string Ruta)
        {
            var archivos = new List<string>();

            HashSet<string> Archivos = new HashSet<string>();
            var datos = new Datos();
            datos.UsuariosValida(Analizador);

            var xml = datos.ResultadoXML;
            string[] extArray;

            //Por cada sub analizador
            foreach (XmlNode xmlNode1 in xml.DocumentElement.SelectSingleNode("Datos").SelectNodes("row"))
            {
                extArray = xmlNode1.Attributes["Extensiones"].Value.Split('|');

                

                foreach (string Extension in extArray)
                {
                    string tipo = "*." + Extension;
                    string[] ArchivosArray = Directory.GetFiles(Ruta, tipo, SearchOption.AllDirectories);
                    for (int i = 0; i <= ArchivosArray.Count() - 1; i++)
                    {
                        Archivos.Add(ArchivosArray[i].ToString());
                    }
                }
            }

                      
            


            return Archivos;
        }
    }
}
