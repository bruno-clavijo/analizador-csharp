using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

namespace analizador_csharp
{
    class Datos : GestionBD
    {
        private const int Configuracion = 1;
        private const string ObtenerDatos = "Sp_Datos";

        private XmlDocument resultadoXML;
        public XmlDocument ResultadoXML
        {
            get { return resultadoXML; }
        }

        public bool UsuariosValida(int Analizador)
        {
            bool respuesta = false;
            try
            {
                PreparaStoredProcedure(ObtenerDatos);
                CargaParametro("tipo", SqlDbType.Int, 8, ParameterDirection.Input, Configuracion);
                CargaParametro("Tipoanalizador", SqlDbType.Int, 8, ParameterDirection.Input, Analizador);
                SqlDataReader Lector = AlmacenarStoredProcedureDataReader();
                if (Lector.Read())
                {
                    resultadoXML = new XmlDocument();
                    string Document = "<xml>" + Lector[0].ToString() + "</xml>";
                    resultadoXML.LoadXml(Document);
                    XmlNode xmlNode = resultadoXML.DocumentElement.SelectSingleNode("Datos");
                    respuesta = xmlNode.HasChildNodes;
                }
                CerrarConexion();
                if (respuesta)
                    Console.WriteLine("Correcto: Usuario: " + Configuracion + " Intento de Ingreso");
                    //EscribeLog("Correcto: Usuario: " + Configuracion + " Intento de Ingreso");
                else
                    Console.WriteLine("Error: Usuario: " + Configuracion + " No Intento de Ingreso");
                    //EscribeLog("Error: Usuario: " + Usuario + " No Intento de Ingreso");
            }
            catch (Exception Err)
            {
                Console.WriteLine("Excepcion: Usuarios.UsuariosValida " + Err.Message.ToString());
                //EscribeLog("Excepcion: Usuarios.UsuariosValida " + Err.Message.ToString());
            }
            return respuesta;
        }
    }
}
