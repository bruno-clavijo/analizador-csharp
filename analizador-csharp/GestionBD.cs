using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace analizador_csharp
{
    public class GestionBD
    {
        private SqlConnection CadenaDeConexion;
        private SqlCommand Comando;
        private SqlTransaction TransaccionSQL;

        public GestionBD()
        {
            CadenaDeConexion = new SqlConnection("Data Source= DESKTOP-F8M6H30\\SQLEXPRESS; Initial Catalog= Analizador; uid=sa; Password=sa; ");
        }

        public void CerrarConexion()
        {
            try
            {
                if (CadenaDeConexion != null)
                {
                    if (CadenaDeConexion.State != ConnectionState.Closed)
                        CadenaDeConexion.Close();
                }
            }
            catch (SqlException Err)
            {
                //EscribeLog("BaseDeDatos.CerrarConexion " + Err.Message.ToString() + " "
                //                                              + Err.Number.ToString() + " "
                //                                              + Err.Procedure.ToString() + " "
                //                                              + Err.LineNumber.ToString() + " "
                //                                              + Err.State.ToString());
            }
        }

        // --- Transacciones SQL ---

        public bool InicializaTransaccion()
        {
            bool respuesta = false;
            try
            {
                TransaccionSQL = CadenaDeConexion.BeginTransaction("TransaccionSQL");
            }
            catch (Exception Err)
            {
                //EscribeLog("BaseDeDatos.InicializaTransaccion " + Err.Message.ToString());
            }
            return respuesta;
        }

        public bool CommitTransaccion()
        {
            bool respuesta = false;
            try
            {
                TransaccionSQL.Commit();
            }
            catch (Exception Err)
            {
                //EscribeLog("BaseDeDatos.CommitTransaccion " + Err.Message.ToString());
            }
            return respuesta;
        }

        public bool RollBackTransaccion()
        {
            bool respuesta = false;
            try
            {
                TransaccionSQL.Rollback();
            }
            catch (Exception Err)
            {
                //EscribeLog("BaseDeDatos.RollBackTransaccion " + Err.Message.ToString());
            }
            return respuesta;
        }

        public bool AsignaTransaccion()
        {
            bool respuesta = false;
            try
            {
                Comando.Transaction = TransaccionSQL;
            }
            catch (Exception Err)
            {
                //EscribeLog("BaseDeDatos.AsignaTransaccion " + Err.Message.ToString());
            }
            return respuesta;
        }

        // ---

        // --- Procedimientos Almacenados ---

        public void PreparaStoredProcedure(string SP)
        {
            try
            {
                CadenaDeConexion.Open();
                Comando = new SqlCommand();
                Comando.Connection = CadenaDeConexion;
                Comando.CommandText = SP;
                Comando.CommandType = CommandType.StoredProcedure;
            }
            catch (SqlException Err)
            {
                CerrarConexion();
                //EscribeLog("BaseDeDatos.EjecutaStoreProcedure " + SP + " " + Err.Message.ToString());
            }
        }

        public void EjecutaStoredProcedureNonQuery()
        {
            try
            {
                Comando.ExecuteNonQuery();
            }
            catch (SqlException Err)
            {
                CerrarConexion();
                //EscribeLog("BaseDeDatos.EjecutaStoreProcedureNonQuery " + Err.Message.ToString()
                //                                                          + Err.Number.ToString() + " "
                //                                                          + Err.Procedure.ToString() + " "
                //                                                          + Err.LineNumber.ToString() + " "
                //                                                          + Err.State.ToString());
            }
        }

        public SqlDataReader AlmacenarStoredProcedureDataReader()
        {
            SqlDataReader sqlDataReader = null;
            try
            {
                sqlDataReader = Comando.ExecuteReader();
            }
            catch (SqlException Err)
            {
                CerrarConexion();
                //EscribeLog("BaseDeDatos.AlmacenarStoredProcedureDataReader " + Err.Message.ToString() + " "
                //                                                               + Err.Number.ToString() + " "
                //                                                               + Err.Procedure.ToString() + " "
                //                                                               + Err.LineNumber.ToString() + " "
                //                                                               + Err.State.ToString());
            }
            return sqlDataReader;
        }

        public void CargaParametro(string Parametro, SqlDbType Tipo, int Tamano, ParameterDirection Direccion, object Valor)
        {
            try
            {
                Comando.Parameters.Add(new SqlParameter(Parametro, Tipo, Tamano, Direccion, true, 0, 0, "", DataRowVersion.Current, Valor));
            }
            catch (SqlException Err)
            {
                CerrarConexion();
                //EscribeLog("BaseDeDatos.CargaParametro " + Err.Message.ToString() + " "
                //                                              + Err.Number.ToString() + " "
                //                                              + Err.Procedure.ToString() + " "
                //                                              + Err.LineNumber.ToString() + " "
                //                                              + Err.State.ToString());
            }

        }

        public object RegresaValorParam(string name)
        {
            try
            {
                return Comando.Parameters[name].Value;
            }
            catch (Exception Err)
            {
                //EscribeLog("BaseDatos.RegresaValorParam " + name + " " + Err.Message.ToString());
                return 0;
            }
        }


        // ---
    }
}
