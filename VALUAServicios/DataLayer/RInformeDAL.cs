using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace DataLayer
{
    public class RInformeDAL
    {
        public bool EliminarInforme(int IdInforme)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_INFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdInforme);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public int InsertarInforme(RInformeBE InformeBE)
        {
            int res = 0;
            int idInformeGenerado = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_INFORME"))
            {
                objDB.AddInParameter(objCMD, "@Titulo", DbType.String, InformeBE.Titulo);
                objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, InformeBE.NumInforme);
                objDB.AddInParameter(objCMD, "@FechaVisitaAl", DbType.DateTime, InformeBE.FechaVisitaAl);
                objDB.AddInParameter(objCMD, "@FechaVisitaDel", DbType.DateTime, InformeBE.FechaVisitaDel);
                objDB.AddInParameter(objCMD, "@FechaCreacion", DbType.DateTime, InformeBE.FechaCreacion);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, InformeBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@Alcance", DbType.String, InformeBE.Alcance);
                objDB.AddInParameter(objCMD, "@Objetivo", DbType.String, InformeBE.Objetivo);
                objDB.AddInParameter(objCMD, "@Limitaciones", DbType.String, InformeBE.Limitaciones);
                objDB.AddInParameter(objCMD, "@UsuarioCreador", DbType.String, InformeBE.UsuarioCreador);
                objDB.AddInParameter(objCMD, "@UsuarioModificacion", DbType.String, InformeBE.UsuarioModificacion);
                objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, InformeBE.IdZona);
                objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, InformeBE.IdSucursal);
                objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, InformeBE.IdSociedad);
                objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, InformeBE.IdLocal);
                objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, InformeBE.IdAlmacen);
                objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, InformeBE.IdAreaCorporativa);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, InformeBE.IdPais);
                objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, InformeBE.IdCiudad);
                objDB.AddInParameter(objCMD, "@IdTipoInforme", DbType.Int32, InformeBE.IdTipoInforme);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, InformeBE.EstaActivo);
                objDB.AddInParameter(objCMD, "@Correlativo", DbType.Int32, InformeBE.Correlativo);
                objDB.AddInParameter(objCMD, "@PersonalRealizoVisita", DbType.String, InformeBE.PersonalRealizoVisita);
                objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, InformeBE.IdEstadoInforme);
                objDB.AddInParameter(objCMD, "@NombreUsuarioCreador", DbType.String, InformeBE.NombreUsuarioCreador);
                objDB.AddInParameter(objCMD, "@NombrePersonalRealizoVisita", DbType.String, InformeBE.NombrePersonalRealizoVisita);
                objDB.AddOutParameter(objCMD, "@Id", DbType.Int64, 0);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                    idInformeGenerado = int.Parse(objDB.GetParameterValue(objCMD, "@Id").ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return idInformeGenerado;
        }

        public bool ActualizarInforme(RInformeBE InformeBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_INFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.String, InformeBE.Id);
             //   objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, InformeBE.NumInforme);
                objDB.AddInParameter(objCMD, "@FechaVisitaAl", DbType.DateTime, InformeBE.FechaVisitaAl);
                objDB.AddInParameter(objCMD, "@FechaVisitaDel", DbType.DateTime, InformeBE.FechaVisitaDel);
                //objDB.AddInParameter(objCMD, "@FechaCreacion", DbType.DateTime, InformeBE.FechaCreacion);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, InformeBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@Alcance", DbType.String, InformeBE.Alcance);
                objDB.AddInParameter(objCMD, "@Objetivo", DbType.String, InformeBE.Objetivo);
                objDB.AddInParameter(objCMD, "@Limitaciones", DbType.String, InformeBE.Limitaciones);
                //objDB.AddInParameter(objCMD, "@UsuarioCreador", DbType.String, InformeBE.UsuarioCreador);
                objDB.AddInParameter(objCMD, "@UsuarioModificacion", DbType.String, InformeBE.UsuarioModificacion);
                objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, InformeBE.IdZona);
                objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, InformeBE.IdSucursal);
                objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, InformeBE.IdSociedad);
                objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, InformeBE.IdLocal);
                objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, InformeBE.IdAlmacen);
                objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, InformeBE.IdAreaCorporativa);
                objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, InformeBE.IdPais);
                objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, InformeBE.IdCiudad);
                objDB.AddInParameter(objCMD, "@IdTipoInforme", DbType.Int32, InformeBE.IdTipoInforme);
                objDB.AddInParameter(objCMD, "@EstaActivo", DbType.Boolean, InformeBE.EstaActivo);
                objDB.AddInParameter(objCMD, "@Titulo", DbType.String, InformeBE.Titulo);
                objDB.AddInParameter(objCMD, "@PersonalRealizoVisita", DbType.String, InformeBE.PersonalRealizoVisita);
                objDB.AddInParameter(objCMD, "@NombrePersonalRealizoVisita", DbType.String, InformeBE.NombrePersonalRealizoVisita);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public bool ActualizarInformeEstado(RInformeBE InformeBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_ESTADOINFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, InformeBE.Id);
                objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, InformeBE.IdEstadoInforme);
                objDB.AddInParameter(objCMD, "@IdInstanciaWorkFlow", DbType.Int32, InformeBE.IdInstanciaWorkFlow);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public bool ActualizarVistoBueno(bool VBRevisor, bool VBAprobador, bool VBResponsable,int idInforme)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_VISTOBUENOINFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idInforme);
                objDB.AddInParameter(objCMD, "@VistoBuenoRevisor", DbType.Boolean, VBRevisor);
                objDB.AddInParameter(objCMD, "@VistoBuenoAprobador", DbType.Boolean, VBAprobador);
                objDB.AddInParameter(objCMD, "@VistoBuenoResponsable", DbType.Boolean, VBResponsable);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public bool ActualizarFechaEnvioAprobacion(RInformeBE InformeBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_FECHA_APROBACION_INFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, InformeBE.Id);
                objDB.AddInParameter(objCMD, "@FechaEnvioAprobacion", DbType.DateTime, InformeBE.FechaEnvioAprobacion);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public bool ActualizarFechaAprobacion(RInformeBE InformeBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_FECHA_APROBACION_FINAL_INFORME"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, InformeBE.Id);
                objDB.AddInParameter(objCMD, "@FechaAprobacion", DbType.DateTime, InformeBE.FechaAprobacion);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return res > 0;
        }
        public int ObtenerCorrelativo()
        {

            int correlativo = 0;
           
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERCORRELATIVOINFORME"))
            {
                try
                {
                    correlativo = Convert.ToInt32(objDB.ExecuteScalar(objCMD));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return correlativo;
        }

        public List<RInformeBE> ObtenerInformes(int IdEstadoInforme, string anio, string informe,string usuario)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERINFORMES"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, IdEstadoInforme);
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, informe);
                    objDB.AddInParameter(objCMD, "@UsuarioCreador", DbType.String, usuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                            oInformedBE.TipoInforme = (string)oDataReader["TipoInforme"];
                            oInformedBE.Sociedad = (string)oDataReader["Sociedad"];
                            oInformedBE.Area = (string)oDataReader["Area"];
                            oInformedBE.UsuarioCreador = (string)oDataReader["UsuarioCreador"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.Estado = (string)oDataReader["Estado"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oInformedBE.NumeroEventos = (int)oDataReader["NumeroEventos"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesTareaUsuario(int IdEstadoInforme, string usuario, string estadoTarea,string anio, string informe, int idInstancia)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_INFORMES_POR_INSTANCIA"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, IdEstadoInforme);
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, usuario);
                    objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estadoTarea);
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, informe);
                    objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, idInstancia);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                            oInformedBE.TipoInforme = (string)oDataReader["TipoInforme"];
                            oInformedBE.Sociedad = (string)oDataReader["Sociedad"];
                            oInformedBE.Area = (string)oDataReader["Area"];
                            oInformedBE.UsuarioCreador = (string)oDataReader["UsuarioCreador"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.Estado = (string)oDataReader["Estado"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesAdministradores(RInformeBE InformeBE, int IdGerenciaCentral,int IdGerenciaDivision,int IdArea)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ADMINISTRACIONINFORMES"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, InformeBE.NumInforme);
                    objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, InformeBE.IdSociedad);
                    objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, InformeBE.IdZona);
                    objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, InformeBE.IdSucursal);
                    objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, InformeBE.IdLocal);
                    objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, InformeBE.IdAlmacen);
                    objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, InformeBE.IdAreaCorporativa);
                    objDB.AddInParameter(objCMD, "@IdTipoInforme", DbType.Int32, InformeBE.IdTipoInforme);
                    objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, InformeBE.IdPais);
                    objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, InformeBE.IdEstadoInforme);
                    objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, InformeBE.IdCiudad);
                    objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                    objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, IdGerenciaDivision);
                    objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
                    objDB.AddInParameter(objCMD, "@FechaDel", DbType.DateTime, InformeBE.FechaVisitaDel == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaDel);
                    objDB.AddInParameter(objCMD, "@FechaAl", DbType.DateTime, InformeBE.FechaVisitaAl == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaAl);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                           // oInformedBE.TipoInforme = (string)oDataReader["TipoInforme"];
                            oInformedBE.Sociedad = (string)oDataReader["Sociedad"];
                            oInformedBE.Zona = (string)oDataReader["Zona"];
                            oInformedBE.Sucursal = (string)oDataReader["Sucursal"];
                            oInformedBE.Local = (string)oDataReader["Local"];
                            oInformedBE.Almacen = (string)oDataReader["Almacen"];
                            oInformedBE.Area = (string)oDataReader["Area"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.FechaCreacion2 = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.Estado = (string)oDataReader["Estado"];
                            oInformedBE.UsuarioCreador = (string)oDataReader["UsuarioCreador"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesAdministradoresEventos(RInformeBE InformeBE, int IdGerenciaCentral, int IdGerenciaDivision, int IdArea)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ADMINISTRACIONEVENTOS"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, InformeBE.NumInforme);
                    objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, InformeBE.IdSociedad);
                    objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, InformeBE.IdZona);
                    objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, InformeBE.IdSucursal);
                    objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, InformeBE.IdLocal);
                    objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, InformeBE.IdAlmacen);
                    objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, InformeBE.IdAreaCorporativa);
                    objDB.AddInParameter(objCMD, "@IdTipoInforme", DbType.Int32, InformeBE.IdTipoInforme);
                    objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, InformeBE.IdPais);
                    objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, InformeBE.IdEstadoInforme);
                    objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, InformeBE.IdCiudad);
                    objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                    objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, IdGerenciaDivision);
                    objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
                    objDB.AddInParameter(objCMD, "@FechaDel", DbType.DateTime, InformeBE.FechaVisitaDel == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaDel);
                    objDB.AddInParameter(objCMD, "@FechaAl", DbType.DateTime, InformeBE.FechaVisitaAl == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaAl);

                    //Datos del Evento
                    objDB.AddInParameter(objCMD, "@IdGradoCriticidad", DbType.Int32, InformeBE.IdGradoCriticidad);
                    objDB.AddInParameter(objCMD, "@NumEventoRiesgo", DbType.String, InformeBE.NumEventoRiesgo);
                    objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, InformeBE.ResponsableArea);
                    objDB.AddInParameter(objCMD, "@FechaCreacionEventoDel", DbType.DateTime, InformeBE.FechaCreacionEventoDel == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaCreacionEventoDel);
                    objDB.AddInParameter(objCMD, "@FechaCreacionEventoAl", DbType.DateTime, InformeBE.FechaCreacionEventoAl == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaCreacionEventoAl);
                    objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, InformeBE.IdEstadoEventoRiesgo);

                    //Fin de datos del Evento
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.IdEvento = (int)oDataReader["IdEvento"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oInformedBE.TituloEvento = (string)oDataReader["TituloEvento"];
                            oInformedBE.GradoCriticidad = (string)oDataReader["GradoCriticidad"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oInformedBE.IdEstadoEventoRiesgo = (int)oDataReader["IdEstadoEventoRiesgo"];
                            oInformedBE.Area = (string)oDataReader["Area"];
                            oInformedBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oInformedBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            oInformedBE.Estado = (string)oDataReader["EstadoEventoRiesgo"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesAdministradoresExportar(RInformeBE InformeBE, int IdGerenciaCentral, int IdGerenciaDivision, int IdArea)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ADMINISTRACIONINFORMESEXPORTAR"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, InformeBE.NumInforme);
                    objDB.AddInParameter(objCMD, "@IdSociedad", DbType.Int32, InformeBE.IdSociedad);
                    objDB.AddInParameter(objCMD, "@IdZona", DbType.Int32, InformeBE.IdZona);
                    objDB.AddInParameter(objCMD, "@IdSucursal", DbType.Int32, InformeBE.IdSucursal);
                    objDB.AddInParameter(objCMD, "@IdLocal", DbType.Int32, InformeBE.IdLocal);
                    objDB.AddInParameter(objCMD, "@IdAlmacen", DbType.Int32, InformeBE.IdAlmacen);
                    objDB.AddInParameter(objCMD, "@IdAreaCorporativa", DbType.Int32, InformeBE.IdAreaCorporativa);
                    objDB.AddInParameter(objCMD, "@IdTipoInforme", DbType.Int32, InformeBE.IdTipoInforme);
                    objDB.AddInParameter(objCMD, "@IdPais", DbType.Int32, InformeBE.IdPais);
                    objDB.AddInParameter(objCMD, "@IdEstadoInforme", DbType.Int32, InformeBE.IdEstadoInforme);
                    objDB.AddInParameter(objCMD, "@IdCiudad", DbType.Int32, InformeBE.IdCiudad);
                    objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, IdGerenciaCentral);
                    objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, IdGerenciaDivision);
                    objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, IdArea);
                    objDB.AddInParameter(objCMD, "@FechaDel", DbType.DateTime, InformeBE.FechaVisitaDel == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaDel);
                    objDB.AddInParameter(objCMD, "@FechaAl", DbType.DateTime, InformeBE.FechaVisitaAl == DateTime.MinValue ? (object)DBNull.Value : InformeBE.FechaVisitaAl);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.Estado = oDataReader["Estado"] == DBNull.Value ? "" : (string)oDataReader["Estado"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.FechaEnvioAprobacion = oDataReader["FechaEnvioAprobacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEnvioAprobacion"]; ;
                            oInformedBE.UsuarioCreador = (string)oDataReader["NombreUsuarioCreador"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                            oInformedBE.AreaCorporativa = (string)oDataReader["AreaCorporativa"];
                           // oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.IdEstadoEventoRiesgo = oDataReader["IdEstadoEventoRiesgo"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoEventoRiesgo"];
                            oInformedBE.Sociedad = (string)oDataReader["Sociedad"];
                            oInformedBE.Zona = (string)oDataReader["Zona"];
                            oInformedBE.Sucursal = (string)oDataReader["Sucursal"];
                            oInformedBE.Local = (string)oDataReader["Local"];
                            oInformedBE.Almacen = (string)oDataReader["Almacen"];
                            oInformedBE.Pais = (string)oDataReader["Pais"];
                            oInformedBE.Ciudad = (string)oDataReader["Ciudad"];
                            oInformedBE.FechaVisitaDel = (DateTime)oDataReader["FechaVisitaDel"];
                            oInformedBE.FechaVisitaAl = (DateTime)oDataReader["FechaVisitaAl"];
                            oInformedBE.PersonalRealizoVisita = (string)oDataReader["NombrePersonalRealizoVisita"];
                            oInformedBE.TipoInforme = (string)oDataReader["TipoInforme"];
                            oInformedBE.RiesgoEventoDetectado = oDataReader["RiesgoEventoDetectado"] == DBNull.Value ? "" : (string)oDataReader["RiesgoEventoDetectado"];
                            oInformedBE.Objetivo = (string)oDataReader["Objetivo"];
                            oInformedBE.Limitaciones = (string)oDataReader["Limitaciones"];
                            oInformedBE.Alcance = (string)oDataReader["Alcance"];
                            oInformedBE.NumEventoRiesgo = oDataReader["NumEventoRiesgo"] == DBNull.Value ? "" : (string)oDataReader["NumEventoRiesgo"];
                            oInformedBE.EstadoEventoRiesgo = oDataReader["EstadoEventoRiesgo"] == DBNull.Value ? "" : (string)oDataReader["EstadoEventoRiesgo"];
                            oInformedBE.FechaCreacionEvento = oDataReader["FechaCreacionEvento"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaCreacionEvento"];
                            oInformedBE.CreadorEvento = oDataReader["CreadorEvento"] == DBNull.Value ? "" : (string)oDataReader["CreadorEvento"];
                            oInformedBE.TituloEvento = oDataReader["TituloEvento"] == DBNull.Value ? "" : (string)oDataReader["TituloEvento"];
                            oInformedBE.Impacto = oDataReader["Impacto"] == DBNull.Value ? "" : (string)oDataReader["Impacto"];
                            oInformedBE.Probabilidad = oDataReader["Probabilidad"] == DBNull.Value ? "" : (string)oDataReader["Probabilidad"];
                            oInformedBE.GradoCriticidad = oDataReader["GradoCriticidad"] == DBNull.Value ? "" : (string)oDataReader["GradoCriticidad"];
                            oInformedBE.TipoRiesgo = oDataReader["TipoRiesgo"] == DBNull.Value ? "" : (string)oDataReader["TipoRiesgo"];
                            oInformedBE.FechaEventoRiesgo = oDataReader["FechaEventoRiesgo"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEventoRiesgo"];
                            oInformedBE.DetalleEvento = oDataReader["DetalleEvento"] == DBNull.Value ? "" : (string)oDataReader["DetalleEvento"];
                            oInformedBE.GerenciaCentral = oDataReader["GerenciaCentral"] == DBNull.Value ? "" : (string)oDataReader["GerenciaCentral"];
                            oInformedBE.GerenciaDivision = oDataReader["GerenciaDivision"] == DBNull.Value ? "" : (string)oDataReader["GerenciaDivision"];
                            oInformedBE.Area = oDataReader["Area"] == DBNull.Value ? "" : (string)oDataReader["Area"];
                            oInformedBE.ResponsableArea = oDataReader["ResponsableArea"] == DBNull.Value ? "" : (string)oDataReader["ResponsableArea"];
                            oInformedBE.MontoProductoPerdida = oDataReader["MontoProductoPerdida"] == DBNull.Value ? 0 : (decimal)oDataReader["MontoProductoPerdida"];
                            oInformedBE.MontoPosiblePerdida = oDataReader["MontoPosiblePerdida"] == DBNull.Value ? 0 : (decimal)oDataReader["MontoPosiblePerdida"];
                            oInformedBE.MontoTotal = oDataReader["MontoTotal"] == DBNull.Value ? 0 : (decimal)oDataReader["MontoTotal"];
                            oInformedBE.Aplica = oDataReader["Aplica"] == DBNull.Value ? oInformedBE.Aplica.HasValue: (bool)oDataReader["Aplica"]; 
                            oInformedBE.ComentarioGerencia = oDataReader["ComentarioGerencia"] == DBNull.Value ? "" : (string)oDataReader["ComentarioGerencia"];
                            oInformedBE.Recomendaciones = oDataReader["Recomendaciones"] == DBNull.Value ? "" : (string)oDataReader["Recomendaciones"];
                            oInformedBE.ConCopiaA = oDataReader["NombreConCopiaA"] == DBNull.Value ? "" : (string)oDataReader["NombreConCopiaA"];
                            oInformedBE.ResponsableSeguimiento = oDataReader["ResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["ResponsableSeguimiento"];
                            oInformedBE.Dias = oDataReader["Dias"] == DBNull.Value ? 0 : (int)oDataReader["Dias"];
                            oInformedBE.NotificarGerencia = oDataReader["NotificarGerencia"] == DBNull.Value ? false : (bool)oDataReader["NotificarGerencia"];
                            oInformedBE.ComentarioResponsable = oDataReader["ComentarioResponsable"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsable"];
                            oInformedBE.EstadoResponsableSeguimiento = oDataReader["EstadoResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["EstadoResponsableSeguimiento"];
                            oInformedBE.MotivoRechazoResponsableSeguimiento = oDataReader["MotivoRechazoResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoResponsableSeguimiento"];
                            oInformedBE.PlanAccion = oDataReader["PlanAccion"] == DBNull.Value ? "" : (string)oDataReader["PlanAccion"];
                            oInformedBE.ComentarioResponsable = oDataReader["ComentarioResponsable"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsable"];
                            oInformedBE.FechaProgramadaRA = oDataReader["FechaProgramadaRA"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRA"];
                            oInformedBE.FechaImplementacion = oDataReader["FechaImplementacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaImplementacion"];
                            oInformedBE.EstadoResponsableArea = oDataReader["EstadoResponsableArea"] == DBNull.Value ? "" : (string)oDataReader["EstadoResponsableArea"];
                            oInformedBE.MotivoRechazoReponsableArea = oDataReader["MotivoRechazoReponsableArea"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoReponsableArea"];
                            oInformedBE.ComentarioResponsableObservacion = oDataReader["ComentarioResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsableObservacion"];
                            oInformedBE.FechaProgramadaRO = oDataReader["FechaProgramadaRO"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRO"];
                            oInformedBE.FechaEjecucion = oDataReader["FechaEjecucion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEjecucion"];
                            oInformedBE.EstadoResponsableObservacion = oDataReader["EstadoResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["EstadoResponsableObservacion"];
                            oInformedBE.FechaCierreRS = oDataReader["FechaCierreRS"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaCierreRS"];
                            oInformedBE.FechaCierreRA = oDataReader["FechaCierreRA"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaCierreRA"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesPorAnio(string anio,int idEstado,string usuario)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_INFORMEPORANIO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, idEstado);
                    objDB.AddInParameter(objCMD, "@UsuarioCreador", DbType.String, usuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RInformeBE> ObtenerInformesPorAnioPendiente(string anio, int idEstado,string cuenta, string estado)
        {
            List<RInformeBE> oListaInformes = new List<RInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_INFORMEPORANIO_PENDIENTE"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, idEstado);
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, cuenta);
                    objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estado);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RInformeBE oInformedBE = new RInformeBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaInformes.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaInformes;
        }
        public List<RAnioBE> ObtenerAnios()
        {
            List<RAnioBE> oListaAnios = new List<RAnioBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ANIOS_CREACION_INFORME"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RAnioBE oAnioBE = new RAnioBE();
                            oAnioBE.Id = int.Parse(oDataReader["Id"].ToString());
                            oAnioBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaAnios.Add(oAnioBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaAnios;
        }
        public List<REstadoInformeBE> ObtenerEstados()
        {
            List<REstadoInformeBE> oListaEstados = new List<REstadoInformeBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERESTADO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REstadoInformeBE oEstadoBE = new REstadoInformeBE();
                            oEstadoBE.Id = (int)oDataReader["Id"];
                            oEstadoBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaEstados.Add(oEstadoBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaEstados;
        }
        public RInformeBE ObtenerInformePorId(Int64 piIdInforme)
        {
            RInformeBE oInformedBE = new RInformeBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERINFORMEPORID"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdInforme);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.FechaVisitaDel = (DateTime)oDataReader["FechaVisitaDel"];
                            oInformedBE.FechaVisitaAl = (DateTime)oDataReader["FechaVisitaAl"];
                            oInformedBE.Objetivo = oDataReader["Objetivo"] == (object)DBNull.Value ? "" : (string)oDataReader["Objetivo"];
                            oInformedBE.Alcance = oDataReader["Alcance"] == (object)DBNull.Value ? "" : (string)oDataReader["Alcance"];
                            oInformedBE.Limitaciones = oDataReader["Limitaciones"] == (object)DBNull.Value ? "" : (string)oDataReader["Limitaciones"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oInformedBE.UsuarioCreador = (string)oDataReader["UsuarioCreador"];
                            oInformedBE.UsuarioModificacion = (string)oDataReader["UsuarioModificacion"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oInformedBE.IdInstanciaWorkFlow = oDataReader["IdInstanciaWorkFlow"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdInstanciaWorkFlow"];
                            oInformedBE.IdZona = oDataReader["IdZona"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdZona"];
                            oInformedBE.IdSociedad = oDataReader["IdSociedad"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdSociedad"];
                            oInformedBE.IdSucursal = oDataReader["IdSucursal"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdSucursal"];
                            oInformedBE.IdLocal = oDataReader["IdLocal"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdLocal"];
                            oInformedBE.IdAlmacen = oDataReader["IdAlmacen"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdAlmacen"];
                            oInformedBE.IdCiudad = oDataReader["IdCiudad"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdCiudad"];
                            oInformedBE.IdPais = oDataReader["IdPais"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdPais"];
                            oInformedBE.IdAreaCorporativa = (int)oDataReader["IdAreaCorporativa"];
                            oInformedBE.IdTipoInforme = (int)oDataReader["IdTipoInforme"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                            oInformedBE.Sociedad = oDataReader["Sociedad"] == (object)DBNull.Value ? "" : (string)oDataReader["Sociedad"];
                            oInformedBE.Zona = oDataReader["Zona"] == (object)DBNull.Value ? "" : (string)oDataReader["Zona"];
                            oInformedBE.Sucursal = oDataReader["Sucursal"] == (object)DBNull.Value ? "" : (string)oDataReader["Sucursal"];
                            oInformedBE.Local = oDataReader["Local"] == (object)DBNull.Value ? "" : (string)oDataReader["Local"];
                            oInformedBE.Almacen = oDataReader["Almacen"] == (object)DBNull.Value ? "" : (string)oDataReader["Almacen"];
                            oInformedBE.Pais = oDataReader["Pais"] == (object)DBNull.Value ? "" : (string)oDataReader["Pais"];
                            oInformedBE.Ciudad = oDataReader["Ciudad"] == (object)DBNull.Value ? "" : (string)oDataReader["Ciudad"];
                            oInformedBE.Estado = oDataReader["Estado"] == (object)DBNull.Value ? "" : (string)oDataReader["Estado"];
                            oInformedBE.AreaCorporativa = (string)oDataReader["AreaCorporativa"];
                            oInformedBE.TipoInforme = oDataReader["TipoInforme"] == (object)DBNull.Value ? "" : (string)oDataReader["TipoInforme"];
                            oInformedBE.PersonalRealizoVisita = oDataReader["PersonalRealizoVisita"] == (object)DBNull.Value ? "" :(string) oDataReader["PersonalRealizoVisita"];
                            oInformedBE.NombrePersonalRealizoVisita = oDataReader["NombrePersonalRealizoVisita"] == (object)DBNull.Value ? "" : (string)oDataReader["NombrePersonalRealizoVisita"];
                            oInformedBE.FechaEnvioAprobacion = oDataReader["FechaEnvioAprobacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEnvioAprobacion"];
                            oInformedBE.FechaAprobacion = oDataReader["FechaAprobacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaAprobacion"];

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oInformedBE;
        }
        public RInformeBE ObtenerInformePorIdInstancia(int IdInstancia)
        {
            RInformeBE oInformedBE = new RInformeBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERINFORME_PORID_INSTANCIA"))
            {

                try
                {
                    objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, IdInstancia);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.FechaVisitaDel = (DateTime)oDataReader["FechaVisitaDel"];
                            oInformedBE.FechaVisitaAl = (DateTime)oDataReader["FechaVisitaAl"];
                            oInformedBE.Objetivo = oDataReader["Objetivo"] == (object)DBNull.Value ? "" : (string)oDataReader["Objetivo"];
                            oInformedBE.Alcance = oDataReader["Alcance"] == (object)DBNull.Value ? "" : (string)oDataReader["Alcance"];
                            oInformedBE.Limitaciones = oDataReader["Limitaciones"] == (object)DBNull.Value ? "" : (string)oDataReader["Limitaciones"];
                            oInformedBE.FechaCreacion = (DateTime)oDataReader["FechaCreacion"];
                            oInformedBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oInformedBE.UsuarioCreador = (string)oDataReader["UsuarioCreador"];
                            oInformedBE.UsuarioModificacion = (string)oDataReader["UsuarioModificacion"];
                            oInformedBE.NumInforme = (string)oDataReader["NumInforme"];
                            oInformedBE.IdEstadoInforme = (int)oDataReader["IdEstadoInforme"];
                            oInformedBE.IdInstanciaWorkFlow = oDataReader["IdInstanciaWorkFlow"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdInstanciaWorkFlow"];
                            oInformedBE.IdZona = oDataReader["IdZona"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdZona"];
                            oInformedBE.IdSociedad = oDataReader["IdSociedad"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdSociedad"];
                            oInformedBE.IdSucursal = oDataReader["IdSucursal"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdSucursal"];
                            oInformedBE.IdLocal = oDataReader["IdLocal"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdLocal"];
                            oInformedBE.IdAlmacen = oDataReader["IdAlmacen"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdAlmacen"];
                            oInformedBE.IdCiudad = oDataReader["IdCiudad"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdCiudad"];
                            oInformedBE.IdPais = oDataReader["IdPais"] == (object)DBNull.Value ? 0 : (int)oDataReader["IdPais"];
                            oInformedBE.IdAreaCorporativa = (int)oDataReader["IdAreaCorporativa"];
                            oInformedBE.IdTipoInforme = (int)oDataReader["IdTipoInforme"];
                            oInformedBE.Titulo = (string)oDataReader["Titulo"];
                            oInformedBE.Sociedad = oDataReader["Sociedad"] == (object)DBNull.Value ? "" : (string)oDataReader["Sociedad"];
                            oInformedBE.Zona = oDataReader["Zona"] == (object)DBNull.Value ? "" : (string)oDataReader["Zona"];
                            oInformedBE.Sucursal = oDataReader["Sucursal"] == (object)DBNull.Value ? "" : (string)oDataReader["Sucursal"];
                            oInformedBE.Local = oDataReader["Local"] == (object)DBNull.Value ? "" : (string)oDataReader["Local"];
                            oInformedBE.Almacen = oDataReader["Almacen"] == (object)DBNull.Value ? "" : (string)oDataReader["Almacen"];
                            oInformedBE.Pais = oDataReader["Pais"] == (object)DBNull.Value ? "" : (string)oDataReader["Pais"];
                            oInformedBE.Ciudad = oDataReader["Ciudad"] == (object)DBNull.Value ? "" : (string)oDataReader["Ciudad"];
                            oInformedBE.Estado = oDataReader["Estado"] == (object)DBNull.Value ? "" : (string)oDataReader["Estado"];
                            oInformedBE.AreaCorporativa = (string)oDataReader["AreaCorporativa"];
                            oInformedBE.TipoInforme = oDataReader["TipoInforme"] == (object)DBNull.Value ? "" : (string)oDataReader["TipoInforme"];
                            oInformedBE.PersonalRealizoVisita = oDataReader["PersonalRealizoVisita"] == (object)DBNull.Value ? "" : (string)oDataReader["PersonalRealizoVisita"];
                            oInformedBE.NombrePersonalRealizoVisita = oDataReader["NombrePersonalRealizoVisita"] == (object)DBNull.Value ? "" : (string)oDataReader["NombrePersonalRealizoVisita"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oInformedBE;
        }
    }
}
