using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Data;
using System.Data.Common;
using Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;
using Utility;


namespace DataLayer
{
    public class PuestoDA
    {
        private static PuestoDA _PuestoDA = null;

        private PuestoDA() { }

        public static PuestoDA Instanse 
	    {
		    get {
                if (_PuestoDA != null) return _PuestoDA;
                _PuestoDA = new PuestoDA();
                return _PuestoDA;
		    }
	    }

        public List<PuestoBE> GetByUser(DBHelper pDBHelper,string CuentaUsuario)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetByUser"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataReader["IdEmpresa"].ToString()))
                            oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdPais"].ToString()))
                            oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdDepartamento"].ToString()))
                            oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ? 
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString():
                                                        dataReader["UsuarioModificacion"].ToString(); 
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ? 
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString(): 
                                                        dataReader["UsuarioCreacion"].ToString() ; 
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ? 
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString():
                                                        dataReader["UsuarioEliminacion"].ToString(); 
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();
                        
                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<PuestoBE> GetByUserAdministrador(DBHelper pDBHelper, string CuentaUsuario, int IdEmpresa)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
                new DBHelper.Parameters("@IdEmpresa", (IdEmpresa == Constantes.INT_NULO) ? (object)DBNull.Value : IdEmpresa),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetByUserAdministrador"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataReader["IdEmpresa"].ToString()))
                            oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdPais"].ToString()))
                            oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdDepartamento"].ToString()))
                            oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString();
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<PuestoBE> GetByUserAdministradorFiltros(DBHelper pDBHelper, string CuentaUsuario, int IdEmpresa, int IdPais, int IdDepartamento,
                                                        int IdArea, int IdSubArea, string TituloPuesto, string NombreOcupante, string CodigoValua)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
                new DBHelper.Parameters("@IdEmpresa", (IdEmpresa == Constantes.INT_NULO) ? (object)DBNull.Value : IdEmpresa),
                new DBHelper.Parameters("@IdPais", (IdPais == Constantes.INT_NULO) ? (object)DBNull.Value : IdPais),
                new DBHelper.Parameters("@IdDepartamento", (IdDepartamento == Constantes.INT_NULO) ? (object)DBNull.Value : IdDepartamento),
                new DBHelper.Parameters("@IdArea", (IdArea == Constantes.INT_NULO) ? (object)DBNull.Value : IdArea),
                new DBHelper.Parameters("@IdSubArea", (IdSubArea == Constantes.INT_NULO) ? (object)DBNull.Value : IdSubArea),
                new DBHelper.Parameters("@TituloPuesto", string.IsNullOrEmpty(TituloPuesto) ? (object)DBNull.Value : TituloPuesto),
                new DBHelper.Parameters("@NombreOcupante", string.IsNullOrEmpty(NombreOcupante) ? (object)DBNull.Value : NombreOcupante),
                new DBHelper.Parameters("@CodigoValua", string.IsNullOrEmpty(CodigoValua) ? (object)DBNull.Value : CodigoValua),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetByUserAdministradorFiltros"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataReader["IdEmpresa"].ToString()))
                            oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdPais"].ToString()))
                            oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdDepartamento"].ToString()))
                            oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString();
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<PuestoBE> GetEliminadosByUser(DBHelper pDBHelper, string CuentaUsuario)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetEliminadosByUser"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataReader["IdEmpresa"].ToString()))
                            oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdPais"].ToString()))
                            oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdDepartamento"].ToString()))
                            oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString(); 
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<PuestoBE> ExportarByUser(DBHelper pDBHelper, string CuentaUsuario)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosExportarByUser"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        oObject.Area = dataReader["Area"].ToString();
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString(); 
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<PuestoBE> ExportarEliminadosByUser(DBHelper pDBHelper, string CuentaUsuario)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Usuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosExportarEliminadosByUser"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        oObject.Area = dataReader["Area"].ToString();
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString();  
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public bool Insert(DBHelper pDBHelper, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@TituloPuesto", string.IsNullOrEmpty(pPuesto.TituloPuesto) ? (object)DBNull.Value : pPuesto.TituloPuesto),
                new DBHelper.Parameters("@IdEstado", pPuesto.IdEstado == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdEstado),
                new DBHelper.Parameters("@IdAdjunto", pPuesto.IdAdjunto == 0 ? (object)DBNull.Value : pPuesto.IdAdjunto),
                new DBHelper.Parameters("@IdPais", pPuesto.IdPais == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdPais),
                new DBHelper.Parameters("@IdEmpresa", pPuesto.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdEmpresa),
                new DBHelper.Parameters("@IdDepartamento", pPuesto.IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdDepartamento),
                new DBHelper.Parameters("@IdArea", pPuesto.IdArea == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdArea),
                new DBHelper.Parameters("@IdSubArea", pPuesto.IdSubArea == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSubArea),
                new DBHelper.Parameters("@NombreOcupante", string.IsNullOrEmpty(pPuesto.NombreOcupante) ? (object)DBNull.Value : pPuesto.NombreOcupante),
                new DBHelper.Parameters("@Grado", string.IsNullOrEmpty(pPuesto.Grado) ? (object)DBNull.Value : pPuesto.Grado),
                new DBHelper.Parameters("@IdCompetenciaT", pPuesto.IdCompetenciaT == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaT),
                new DBHelper.Parameters("@IdCompetenciaG", pPuesto.IdCompetenciaG == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaG),
                new DBHelper.Parameters("@IdCompetenciaRH", pPuesto.IdCompetenciaRH == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaRH),
                new DBHelper.Parameters("@CompetenciaPTS", string.IsNullOrEmpty(pPuesto.CompetenciaPTS) ? (object)DBNull.Value : pPuesto.CompetenciaPTS),
                new DBHelper.Parameters("@IdSolucionA", pPuesto.IdSolucionA == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSolucionA),
                new DBHelper.Parameters("@IdSolucionD", pPuesto.IdSolucionD == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSolucionD),
                new DBHelper.Parameters("@SolucionPorc", string.IsNullOrEmpty(pPuesto.SolucionPorc) ? (object)DBNull.Value : pPuesto.SolucionPorc),
                new DBHelper.Parameters("@SolucionPTS", string.IsNullOrEmpty(pPuesto.SolucionPTS) ? (object)DBNull.Value : pPuesto.SolucionPTS),
                new DBHelper.Parameters("@IdResponsabilidadA", pPuesto.IdResponsabilidadA == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadA),
                new DBHelper.Parameters("@IdResponsabilidadM", pPuesto.IdResponsabilidadM == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadM),
                new DBHelper.Parameters("@IdResponsabilidadI", pPuesto.IdResponsabilidadI == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadI),
                new DBHelper.Parameters("@ResponsabilidadPTS", string.IsNullOrEmpty(pPuesto.ResponsabilidadPTS) ? (object)DBNull.Value : pPuesto.ResponsabilidadPTS),
                new DBHelper.Parameters("@Total", string.IsNullOrEmpty(pPuesto.Total) ? (object)DBNull.Value : pPuesto.Total),
                new DBHelper.Parameters("@Perfil", string.IsNullOrEmpty(pPuesto.Perfil) ? (object)DBNull.Value : pPuesto.Perfil),
                new DBHelper.Parameters("@PuntoMedio", string.IsNullOrEmpty(pPuesto.PuntoMedio) ? (object)DBNull.Value : pPuesto.PuntoMedio),
                new DBHelper.Parameters("@Magnitud", string.IsNullOrEmpty(pPuesto.Magnitud) ? (object)DBNull.Value : pPuesto.Magnitud),
                new DBHelper.Parameters("@Comentario", string.IsNullOrEmpty(pPuesto.Comentario) ? (object)DBNull.Value : pPuesto.Comentario),
                new DBHelper.Parameters("@UsuarioCreacion", string.IsNullOrEmpty(pPuesto.UsuarioCreador) ? (object)DBNull.Value : pPuesto.UsuarioCreador),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(pPuesto.UsuarioModificacion) ? (object)DBNull.Value : pPuesto.UsuarioModificacion),
                new DBHelper.Parameters("@UsuarioEliminacion", string.IsNullOrEmpty(pPuesto.UsuarioElimino) ? (object)DBNull.Value : pPuesto.UsuarioElimino),
                new DBHelper.Parameters("@CodigoFuncion", string.IsNullOrEmpty(pPuesto.CodigoFuncion) ? (object)DBNull.Value : pPuesto.CodigoFuncion),
                new DBHelper.Parameters("@CodigoOcupante", string.IsNullOrEmpty(pPuesto.CodigoOcupante) ? (object)DBNull.Value : pPuesto.CodigoOcupante),
                new DBHelper.Parameters("@CodigoValua", string.IsNullOrEmpty(pPuesto.CodigoValua) ? (object)DBNull.Value : pPuesto.CodigoValua),
                
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestosInsert"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public bool Update(DBHelper pDBHelper, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", pPuesto.Id == 0 ? (object)DBNull.Value : pPuesto.Id),
                new DBHelper.Parameters("@TituloPuesto", string.IsNullOrEmpty(pPuesto.TituloPuesto) ? (object)DBNull.Value : pPuesto.TituloPuesto),
                new DBHelper.Parameters("@IdEstado", pPuesto.IdEstado == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdEstado),
                new DBHelper.Parameters("@IdAdjunto", pPuesto.IdAdjunto == 0 ? (object)DBNull.Value : pPuesto.IdAdjunto),
                new DBHelper.Parameters("@IdPais", pPuesto.IdPais == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdPais),
                new DBHelper.Parameters("@IdEmpresa", pPuesto.IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdEmpresa),
                new DBHelper.Parameters("@IdDepartamento", pPuesto.IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdDepartamento),
                new DBHelper.Parameters("@IdArea", pPuesto.IdArea == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdArea),
                new DBHelper.Parameters("@IdSubArea", pPuesto.IdSubArea == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSubArea),
                new DBHelper.Parameters("@NombreOcupante", string.IsNullOrEmpty(pPuesto.NombreOcupante) ? (object)DBNull.Value : pPuesto.NombreOcupante),
                new DBHelper.Parameters("@Grado", string.IsNullOrEmpty(pPuesto.Grado) ? (object)DBNull.Value : pPuesto.Grado),
                new DBHelper.Parameters("@IdCompetenciaT", pPuesto.IdCompetenciaT == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaT),
                new DBHelper.Parameters("@IdCompetenciaG", pPuesto.IdCompetenciaG == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaG),
                new DBHelper.Parameters("@IdCompetenciaRH", pPuesto.IdCompetenciaRH == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdCompetenciaRH),
                new DBHelper.Parameters("@CompetenciaPTS", string.IsNullOrEmpty(pPuesto.CompetenciaPTS) ? (object)DBNull.Value : pPuesto.CompetenciaPTS),
                new DBHelper.Parameters("@IdSolucionA", pPuesto.IdSolucionA == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSolucionA),
                new DBHelper.Parameters("@IdSolucionD", pPuesto.IdSolucionD == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdSolucionD),
                new DBHelper.Parameters("@SolucionPorc", string.IsNullOrEmpty(pPuesto.SolucionPorc) ? (object)DBNull.Value : pPuesto.SolucionPorc),
                new DBHelper.Parameters("@SolucionPTS", string.IsNullOrEmpty(pPuesto.SolucionPTS) ? (object)DBNull.Value : pPuesto.SolucionPTS),
                new DBHelper.Parameters("@IdResponsabilidadA", pPuesto.IdResponsabilidadA == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadA),
                new DBHelper.Parameters("@IdResponsabilidadM", pPuesto.IdResponsabilidadM == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadM),
                new DBHelper.Parameters("@IdResponsabilidadI", pPuesto.IdResponsabilidadI == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.IdResponsabilidadI),
                new DBHelper.Parameters("@ResponsabilidadPTS", string.IsNullOrEmpty(pPuesto.ResponsabilidadPTS) ? (object)DBNull.Value : pPuesto.ResponsabilidadPTS),
                new DBHelper.Parameters("@Total", string.IsNullOrEmpty(pPuesto.Total) ? (object)DBNull.Value : pPuesto.Total),
                new DBHelper.Parameters("@Perfil", string.IsNullOrEmpty(pPuesto.Perfil) ? (object)DBNull.Value : pPuesto.Perfil),
                new DBHelper.Parameters("@PuntoMedio", string.IsNullOrEmpty(pPuesto.PuntoMedio) ? (object)DBNull.Value : pPuesto.PuntoMedio),
                new DBHelper.Parameters("@Magnitud", string.IsNullOrEmpty(pPuesto.Magnitud) ? (object)DBNull.Value : pPuesto.Magnitud),
                new DBHelper.Parameters("@Comentario", string.IsNullOrEmpty(pPuesto.Comentario) ? (object)DBNull.Value : pPuesto.Comentario),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(pPuesto.UsuarioModificacion) ? (object)DBNull.Value : pPuesto.UsuarioModificacion),
                new DBHelper.Parameters("@CodigoFuncion", string.IsNullOrEmpty(pPuesto.CodigoFuncion) ? (object)DBNull.Value : pPuesto.CodigoFuncion),
                new DBHelper.Parameters("@CodigoOcupante", string.IsNullOrEmpty(pPuesto.CodigoOcupante) ? (object)DBNull.Value : pPuesto.CodigoOcupante),
                new DBHelper.Parameters("@CodigoValua", string.IsNullOrEmpty(pPuesto.CodigoValua) ? (object)DBNull.Value : pPuesto.CodigoValua),
                
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestosUpdate"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public bool Delete(DBHelper pDBHelper, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", pPuesto.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.Id),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(pPuesto.UsuarioModificacion) ? (object)DBNull.Value : pPuesto.UsuarioModificacion),
                new DBHelper.Parameters("@UsuarioEliminacion", string.IsNullOrEmpty(pPuesto.UsuarioElimino) ? (object)DBNull.Value : pPuesto.UsuarioElimino)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestosDelete"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public bool SetInactive(DBHelper pDBHelper, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", pPuesto.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.Id),
                new DBHelper.Parameters("@UsuarioModificacion", string.IsNullOrEmpty(pPuesto.UsuarioModificacion) ? (object)DBNull.Value : pPuesto.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestosSetInactive"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public string GenerarCorrelativo(DBHelper pDBHelper, string cadena)
        {
            string numero = "";
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@Cadena", string.IsNullOrEmpty(cadena) ? (object)DBNull.Value : cadena),
            };
                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGenerarCorrelativo"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    if (dataReader.Read())
                    {
                        numero = dataReader["Numero"].ToString();
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return numero;
        }
        public PuestoBE GetById(DBHelper pDBHelper, int IdPuesto)
        {
            PuestoBE oObject = new PuestoBE();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", IdPuesto == Constantes.INT_NULO ? (object)DBNull.Value : IdPuesto),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetById"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    if (dataReader.Read())
                    {
                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                            oObject.AdjuntoFisico = (byte[])dataReader["Fisico"];
                        }
                        oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacion"].ToString();
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oObject;
        }
        public bool InsertAdjuntoTemporalInPuesto(DBHelper pDBHelper, AdjuntoBE AdjuntoBE, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdAdjuntoTemporal", AdjuntoBE.IdAdjunto == Constantes.INT_NULO ? (object)DBNull.Value : AdjuntoBE.IdAdjunto),
                new DBHelper.Parameters("@UsuarioCreacion", string.IsNullOrEmpty(pPuesto.UsuarioCreador) ? (object)DBNull.Value : pPuesto.UsuarioCreador),
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestoInsertAdjuntoInPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<PuestoBE> GetHistoriaById(DBHelper pDBHelper,PuestoBE PuestoBE)
        {
            List<PuestoBE> oLista = new List<PuestoBE>();
            DBHelper.Parameters[] colParameters = null;
            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", PuestoBE.Id == Constantes.INT_NULO ? (object)DBNull.Value : PuestoBE.Id),
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dataReader = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestosGetHistoriaById"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dataReader.Read())
                    {
                        PuestoBE oObject = new PuestoBE();

                        oObject.Id = Convert.ToInt32(dataReader["IdPuesto"].ToString());
                        oObject.IdEstado = Convert.ToInt32(dataReader["IdEstado"].ToString());
                        oObject.Estado = dataReader["Estado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdAdjunto"].ToString()))
                        {
                            oObject.IdAdjunto = Convert.ToInt32(dataReader["IdAdjunto"]);
                            oObject.NombreAdjunto = dataReader["NombreAdjunto"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataReader["IdEmpresa"].ToString()))
                            oObject.IdEmpresa = Convert.ToInt32(dataReader["IdEmpresa"].ToString());
                        oObject.Empresa = dataReader["Empresa"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdPais"].ToString()))
                            oObject.IdPais = Convert.ToInt32(dataReader["IdPais"].ToString());
                        oObject.Pais = dataReader["Pais"].ToString();
                        oObject.TituloPuesto = dataReader["TituloPuesto"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdDepartamento"].ToString()))
                            oObject.IdDepartamento = Convert.ToInt32(dataReader["IdDepartamento"].ToString());
                        oObject.Departamento = dataReader["Departamento"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdArea"].ToString()))
                            oObject.IdArea = Convert.ToInt32(dataReader["IdArea"].ToString());
                        oObject.Area = dataReader["Area"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSubArea"].ToString()))
                            oObject.IdSubArea = Convert.ToInt32(dataReader["IdSubArea"].ToString());
                        oObject.SubArea = dataReader["SubArea"].ToString();
                        oObject.NombreOcupante = dataReader["NombreOcupante"].ToString();
                        oObject.Grado = dataReader["Grado"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaT"].ToString()))
                            oObject.IdCompetenciaT = Convert.ToInt32(dataReader["IdCompetenciaT"].ToString());
                        oObject.CompetenciaT = dataReader["CompetenciaT"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaG"].ToString()))
                            oObject.IdCompetenciaG = Convert.ToInt32(dataReader["IdCompetenciaG"].ToString());
                        oObject.CompetenciaG = dataReader["CompetenciaG"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdCompetenciaRH"].ToString()))
                            oObject.IdCompetenciaRH = Convert.ToInt32(dataReader["IdCompetenciaRH"].ToString());
                        oObject.CompetenciaRH = dataReader["CompetenciaRH"].ToString();
                        oObject.CompetenciaPTS = dataReader["CompetenciaPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionA"].ToString()))
                            oObject.IdSolucionA = Convert.ToInt32(dataReader["IdSolucionA"].ToString());
                        oObject.SolucionA = dataReader["SolucionA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdSolucionD"].ToString()))
                            oObject.IdSolucionD = Convert.ToInt32(dataReader["IdSolucionD"].ToString());
                        oObject.SolucionD = dataReader["SolucionD"].ToString();
                        oObject.SolucionPorc = dataReader["SolucionPorc"].ToString();
                        oObject.SolucionPTS = dataReader["SolucionPTS"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadA"].ToString()))
                            oObject.IdResponsabilidadA = Convert.ToInt32(dataReader["IdResponsabilidadA"].ToString());
                        oObject.ResponsabilidadA = dataReader["ResponsabilidadA"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadM"].ToString()))
                            oObject.IdResponsabilidadM = Convert.ToInt32(dataReader["IdResponsabilidadM"].ToString());
                        oObject.ResponsabilidadM = dataReader["ResponsabilidadM"].ToString();
                        if (!string.IsNullOrEmpty(dataReader["IdResponsabilidadI"].ToString()))
                            oObject.IdResponsabilidadI = Convert.ToInt32(dataReader["IdResponsabilidadI"].ToString());
                        oObject.ResponsabilidadI = dataReader["ResponsabilidadI"].ToString();
                        oObject.ResponsabilidadPTS = dataReader["ResponsabilidadPTS"].ToString();
                        oObject.Total = dataReader["Total"].ToString();
                        oObject.Perfil = dataReader["Perfil"].ToString();
                        oObject.PuntoMedio = dataReader["PuntoMedio"].ToString();
                        oObject.Magnitud = dataReader["Magnitud"].ToString();
                        oObject.Comentario = dataReader["Comentario"].ToString();
                        oObject.UsuarioModificacion = dataReader["UsuarioModificacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioModificacion"].ToString();
                        oObject.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oObject.UsuarioCreador = dataReader["UsuarioCreacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioCreacion"].ToString();
                        oObject.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oObject.UsuarioElimino = dataReader["UsuarioEliminacionConfidencial"].ToString() == "1" ?
                                                        ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString() :
                                                        dataReader["UsuarioEliminacion"].ToString();
                        if (dataReader["FechaEliminacion"].ToString() != "")
                        {
                            oObject.FechaEliminacion = Convert.ToDateTime(dataReader["FechaEliminacion"]);
                        }
                        oObject.CodigoFuncion = dataReader["CodigoFuncion"].ToString();
                        oObject.CodigoOcupante = dataReader["CodigoOcupante"].ToString();
                        oObject.CodigoValua = dataReader["CodigoValua"].ToString();

                        oLista.Add(oObject);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }

            return oLista;
        }
        public List<EmpresaBE> MapaPuestosGetEmpresaInPuesto(DBHelper pDBHelper, string CuentaUsuario)
        {
            List<EmpresaBE> lst = null;
            EmpresaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<EmpresaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@CuentaUsuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetEmpresaInPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new EmpresaBE();

                        obj.Id = int.Parse(dr["IdEmpresa"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<PaisBE> MapaPuestosGetPaisInPuestoByEmpresa(DBHelper pDBHelper, int IdEmpresa)
        {
            List<PaisBE> lst = null;
            PaisBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<PaisBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetPaisInPuestoByEmpresa"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new PaisBE();

                        obj.Id = int.Parse(dr["IdPais"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<DepartamentoBE> MapaPuestosGetDepartamentoInPuestoByEmpresaPais(DBHelper pDBHelper, int IdEmpresa, int IdPais)
        {
            List<DepartamentoBE> lst = null;
            DepartamentoBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<DepartamentoBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa),
                    new DBHelper.Parameters("@IdPais", IdPais == Constantes.INT_NULO ? (object)DBNull.Value : IdPais)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetDepartamentoInPuestoByEmpresaPais"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new DepartamentoBE();

                        obj.Id = int.Parse(dr["IdDepartamento"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<GradeStructureBE> MapaPuestosGetGradoInPuesto(DBHelper pDBHelper,string CuentaUsuario)
        {
            List<GradeStructureBE> lst = null;
            GradeStructureBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<GradeStructureBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@CuentaUsuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetGradoInPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new GradeStructureBE();

                        obj.Gs = dr["Grado"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<AreaBE> MapaPuestosGetAreaInPuestoByEmpresaPaisDepartamento(DBHelper pDBHelper, int IdEmpresa, int IdPais, int IdDepartamento)
        {
            List<AreaBE> lst = null;
            AreaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<AreaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa),
                    new DBHelper.Parameters("@IdPais", IdPais == Constantes.INT_NULO ? (object)DBNull.Value : IdPais),
                    new DBHelper.Parameters("@IdDepartamento", IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : IdDepartamento)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetAreaInPuestoByEmpresaPaisDepartamento"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new AreaBE();

                        obj.Id = int.Parse(dr["IdArea"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public List<SubAreaBE> MapaPuestosGetSubAreaInPuestoByEmpresaPaisDepartamentoArea(DBHelper pDBHelper, int IdEmpresa, int IdPais, int IdDepartamento, int IdArea)
        {
            List<SubAreaBE> lst = null;
            SubAreaBE obj = null;
            DBHelper.Parameters[] colParameters = null;
            lst = new List<SubAreaBE>();
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa),
                    new DBHelper.Parameters("@IdPais", IdPais == Constantes.INT_NULO ? (object)DBNull.Value : IdPais),
                    new DBHelper.Parameters("@IdDepartamento", IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : IdDepartamento),
                    new DBHelper.Parameters("@IdArea", IdArea == Constantes.INT_NULO ? (object)DBNull.Value : IdArea)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestosGetSubAreaInPuestoByEmpresaPaisDepartamentoArea"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        obj = new SubAreaBE();

                        obj.Id = int.Parse(dr["IdSubArea"].ToString());
                        obj.Descripcion = dr["Descripcion"].ToString();

                        lst.Add(obj);
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int MapaPuestoGetColumnas(DBHelper pDBHelper,string CuentaUsuario)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@CuentaUsuario", string.IsNullOrEmpty(CuentaUsuario) ? (object)DBNull.Value : CuentaUsuario)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestoGetColumnas"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public string MapaPuestoGetPuestos(DBHelper pDBHelper, int IdEmpresa,int IdPais,int IdDepartamento,int IdArea, int IdSubArea,string Grado)
        {
            string Resultado = "";

            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa == Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa),
                    new DBHelper.Parameters("@IdPais", IdPais == Constantes.INT_NULO ? (object)DBNull.Value : IdPais),
                    new DBHelper.Parameters("@IdDepartamento", IdDepartamento == Constantes.INT_NULO ? (object)DBNull.Value : IdDepartamento),
                    new DBHelper.Parameters("@IdArea", IdArea == Constantes.INT_NULO ? (object)DBNull.Value : IdArea),
                    new DBHelper.Parameters("@IdSubArea", IdSubArea == Constantes.INT_NULO ? (object)DBNull.Value : IdSubArea),
                    new DBHelper.Parameters("@Grado", string.IsNullOrEmpty(Grado) ? (object)DBNull.Value : Grado)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("MapaPuestoGetPuestos"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        if(!string.IsNullOrEmpty(Resultado))
                        { Resultado += "; "; }
                        Resultado += dr["TituloPuesto"];
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public bool PuestoInsertAdjuntoInPuestoByIdPuesto(DBHelper pDBHelper, AdjuntoBE AdjuntoBE, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", pPuesto.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.Id),
                new DBHelper.Parameters("@IdAdjuntoTemporal", AdjuntoBE.IdAdjunto == Constantes.INT_NULO ? (object)DBNull.Value : AdjuntoBE.IdAdjunto),
                new DBHelper.Parameters("@UsuarioCreacion", string.IsNullOrEmpty(pPuesto.UsuarioCreador) ? (object)DBNull.Value : pPuesto.UsuarioCreador),
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestoInsertAdjuntoInPuestoByIdPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public bool DeleteAdjuntoByIdPuesto(DBHelper pDBHelper, PuestoBE pPuesto)
        {
            DBHelper.Parameters[] colParameters = null;

            try
            {
                colParameters = new DBHelper.Parameters[] {
                new DBHelper.Parameters("@IdPuesto", pPuesto.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.Id),
                new DBHelper.Parameters("@Usuario", pPuesto.Id == Constantes.INT_NULO ? (object)DBNull.Value : pPuesto.UsuarioModificacion)
            };

                pDBHelper.CreateDBParameters(colParameters);

                int lfilasAfectadas = pDBHelper.ExecuteNonQuery(pDBHelper.concatOwner("PuestoDeleteAdjuntoByIdPuesto"), CommandType.StoredProcedure, Utility.ConnectionState.KeepOpen, ParameterError.notInclude, CleanParameters.preserve);

                return (lfilasAfectadas > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int GetCountByIdPais(DBHelper pDBHelper,int IdPais)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdPais", IdPais ==Constantes.INT_NULO ? (object)DBNull.Value : IdPais)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestoGetCountByIdPais"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int GetCountByIdEmpresa(DBHelper pDBHelper, int IdEmpresa)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdEmpresa", IdEmpresa ==Constantes.INT_NULO ? (object)DBNull.Value : IdEmpresa)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestoGetCountByIdEmpresa"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int GetByIdDepartamento(DBHelper pDBHelper, int IdDepartamento)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdDepartamento", IdDepartamento ==Constantes.INT_NULO ? (object)DBNull.Value : IdDepartamento)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestoGetByIdDepartamento"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int GetByIdArea(DBHelper pDBHelper, int IdArea)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdArea", IdArea ==Constantes.INT_NULO ? (object)DBNull.Value : IdArea)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestoGetByIdArea"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }
        public int GetByIdSubArea(DBHelper pDBHelper, int IdSubArea)
        {
            int Resultado = 0;
            DBHelper.Parameters[] colParameters = null;
            try
            {
                // Parámetros
                colParameters = new DBHelper.Parameters[] {
                    new DBHelper.Parameters("@IdSubArea", IdSubArea ==Constantes.INT_NULO ? (object)DBNull.Value : IdSubArea)
            };

                pDBHelper.ClearParameter();
                pDBHelper.CreateDBParameters(colParameters);

                using (IDataReader dr = pDBHelper.ExecuteReader(pDBHelper.concatOwner("PuestoGetByIdSubArea"), CommandType.StoredProcedure, Utility.ConnectionState.CloseOnExit))
                {
                    // Leyendo reader 
                    while (dr.Read())
                    {
                        Resultado++;
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                colParameters = null;
                if (pDBHelper != null && pDBHelper.Command.Transaction == null) pDBHelper.Dispose();
            }
        }

    }

    #region XmlClass

    [XmlRoot("Puesto")]
    public class ListPuesto
    {
        [XmlElement("Puesto_Parts")]
        public List<Puesto_Parts> Puesto_Parts { get; set; }
    }
    public class Puesto_Parts
    {
        [XmlElement("estado")]
        public string IdEstado { get; set; }
        [XmlElement("empresa")]
        public string IdEmpresa { get; set; }
        [XmlElement("pais")]
        public string IdPais { get; set; }
        [XmlElement("titulo_puesto")]
        public string Titulo_Puesto { get; set; }
        [XmlElement("departamento")]
        public string IdDepartamento { get; set; }
        [XmlElement("area")]
        public string IdArea { get; set; }
        [XmlElement("subarea")]
        public string IdSubArea { get; set; }
        [XmlElement("nombre_ocupante")]
        public string NombreOcupante { get; set; }
        [XmlElement("grado")]
        public string Grado { get; set; }
        [XmlElement("competencia_t")]
        public string IdCompetencia_T { get; set; }
        [XmlElement("competencia_g")]
        public string IdCompetencia_G { get; set; }
        [XmlElement("competencia_rh")]
        public string IdCompetencia_RH { get; set; }
        [XmlElement("competencia_pts")]
        public string Competencia_PTS { get; set; }
        [XmlElement("solucion_a")]
        public string IdSolucion_A { get; set; }
        [XmlElement("solucion_d")]
        public string IdSolucion_D { get; set; }
        [XmlElement("solucion_porc")]
        public string Solucion_PORC { get; set; }
        [XmlElement("solucion_pts")]
        public string Solucion_PTS { get; set; }
        [XmlElement("responsabilidad_a")]
        public string IdResponsabilidad_A { get; set; }
        [XmlElement("responsabilidad_m")]
        public string IdResponsabilidad_M { get; set; }
        [XmlElement("responsabilidad_i")]
        public string IdResponsabilidad_I { get; set; }
        [XmlElement("responsabilidad_pts")]
        public string Responsabilidad_PTS { get; set; }
        [XmlElement("total")]
        public string Total { get; set; }
        [XmlElement("perfil")]
        public string Perfil { get; set; }
        [XmlElement("punto_medio")]
        public string Punto_Medio { get; set; }
        [XmlElement("magnitud")]
        public string Magnitud { get; set; }
        [XmlElement("comentario")]
        public string Comentario { get; set; }
        [XmlElement("usuario_modificador")]
        public string Usuario_Modificador { get; set; }
        [XmlElement("fecha_modificacion")]
        public string Fecha_Modificacion { get; set; }
        [XmlElement("usuario_creador")]
        public string Usuario_Creador { get; set; }
        [XmlElement("fecha_creacion")]
        public string Fecha_Creacion { get; set; }
        [XmlElement("usuario_elimino")]
        public string Usuario_Elimino { get; set; }
        [XmlElement("fecha_eliminacion")]
        public string Fecha_Eliminacion { get; set; }
        [XmlElement("codigo_funcion")]
        public string Codigo_Funcion { get; set; }
        [XmlElement("codigo_ocupante")]
        public string Codigo_Ocupante { get; set; }
        [XmlElement("codigo_valua")]
        public string Codigo_Valua { get; set; }
        [XmlElement("idadjunto")]
        public string IdAdjunto { get; set; }
    }
#endregion
}
