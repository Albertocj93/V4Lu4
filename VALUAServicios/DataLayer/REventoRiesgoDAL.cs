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
    public class REventoRiesgoDAL
    {

        public bool EliminarEventoRiesgo(int IdEventoRiesgo)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_DEL_EVENTORIESGO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdEventoRiesgo);
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
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERCORRELATIVOEVENTORIESGO"))
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
        public Int64 InsertarEventoRiesgo(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Int64 idEventoGenerado = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_INS_EVENTORIESGO"))
            {
                objDB.AddInParameter(objCMD, "@TituloEvento", DbType.String, EventoBE.Titulo);
                objDB.AddInParameter(objCMD, "@IdTipoRiesgo", DbType.Int32, EventoBE.IdTipoRiesgo);
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, EventoBE.IdArea);
                objDB.AddInParameter(objCMD, "@IdEventoRiesgoDetectado", DbType.Int32, EventoBE.IdEventoRiesgoDetectado);
                objDB.AddInParameter(objCMD, "@IdGradoCriticidad", DbType.Int32, EventoBE.IdGradoCriticidad);
                objDB.AddInParameter(objCMD, "@IdProbabilidad", DbType.Int32, EventoBE.IdProbabilidad);
                objDB.AddInParameter(objCMD, "@FechaEvento", DbType.DateTime, EventoBE.FechaEvento == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaEvento);
                objDB.AddInParameter(objCMD, "@DetalleEvento", DbType.String, EventoBE.DetalleEvento);
                objDB.AddInParameter(objCMD, "@MontoProductoPerdida", DbType.Decimal, EventoBE.MontoProductoPerdida);
                objDB.AddInParameter(objCMD, "@MontoPosiblePerdida", DbType.Decimal, EventoBE.MontoPosiblePerdida);
                objDB.AddInParameter(objCMD, "@MontoTotal", DbType.Decimal, EventoBE.MontoTotal);
                objDB.AddInParameter(objCMD, "@ComentarioGerencia", DbType.String, EventoBE.ComentarioGerencia);
                objDB.AddInParameter(objCMD, "@Recomendaciones", DbType.String, EventoBE.Recomendaciones);
                objDB.AddInParameter(objCMD, "@ConCopiaA", DbType.String, EventoBE.ConCopiaA);
                objDB.AddInParameter(objCMD, "@Aplica", DbType.Boolean, EventoBE.Aplica);
                objDB.AddInParameter(objCMD, "@FechaCreacion", DbType.DateTime, EventoBE.FechaCreacion == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaCreacion);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, EventoBE.FechaModificacion == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@UsuarioCreador", DbType.String, EventoBE.UsuarioCreador);
                objDB.AddInParameter(objCMD, "@UsuarioModificacion", DbType.String, EventoBE.UsuarioModificacion);
                objDB.AddInParameter(objCMD, "@IdEstadoRO", DbType.Int32, EventoBE.IdEstadoRO == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRO);
                objDB.AddInParameter(objCMD, "@IdEstadoRS", DbType.Int32, EventoBE.IdEstadoRS == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRS);
                objDB.AddInParameter(objCMD, "@IdEstadoRA", DbType.Int32, EventoBE.IdEstadoRA == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRA);
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, EventoBE.IdGerenciaDivision);
                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, EventoBE.IdGerenciaCentral);
                objDB.AddInParameter(objCMD, "@NumEventoRiesgo", DbType.String, EventoBE.NumEventoRiesgo);
                objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, EventoBE.NumInforme);
                objDB.AddInParameter(objCMD, "@FlagSinGestion", DbType.Boolean, EventoBE.FlagSinGestion);
                objDB.AddInParameter(objCMD, "@PlazoDias", DbType.String, EventoBE.PlazoDias);
                objDB.AddInParameter(objCMD, "@FechaImplementacion", DbType.DateTime, EventoBE.FechaImplementacion == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaImplementacion);
                objDB.AddInParameter(objCMD, "@NotificarGerenciaGeneral", DbType.Boolean, EventoBE.NotificarGerenciaGeneral);
                objDB.AddInParameter(objCMD, "@ComentarioResponsableSeguimiento", DbType.String, EventoBE.ComentarioResponsableSeguimiento);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRA", DbType.DateTime,  EventoBE.FechaProgramadaRA == DateTime.MinValue ? (object)DBNull.Value:EventoBE.FechaProgramadaRA);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRO", DbType.DateTime, EventoBE.FechaProgramadaRO == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaProgramadaRO);
                objDB.AddInParameter(objCMD, "@PlanAccionRA", DbType.String, EventoBE.PlanAccionRA);
                objDB.AddInParameter(objCMD, "@FechaEjecucionRO", DbType.DateTime, EventoBE.FechaEjecucioRO == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaEjecucioRO);
                objDB.AddInParameter(objCMD, "@IdInforme", DbType.Int32, EventoBE.IdInforme);
                objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, EventoBE.IdEstadoEventoRiesgo);
                objDB.AddInParameter(objCMD, "@Correlativo", DbType.Int32, EventoBE.Correlativo);
                objDB.AddInParameter(objCMD, "@IdImpacto", DbType.Int32, EventoBE.IdImpacto);
                objDB.AddInParameter(objCMD, "@NombreUsuarioCreador", DbType.String, EventoBE.NombreUsuarioCreador);
                objDB.AddInParameter(objCMD, "@NombreConCopiaA", DbType.String, EventoBE.NombreConCopiaA);
                objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, EventoBE.ResponsableArea);
                objDB.AddOutParameter(objCMD, "@Id", DbType.Int64, 0);
                try
                {
                    res = objDB.ExecuteNonQuery(objCMD);
                    idEventoGenerado = Convert.ToInt64(objDB.GetParameterValue(objCMD, "@Id"));
                }
                    
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return idEventoGenerado;
        }

        public bool ActualizarEvento(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@TituloEvento", DbType.String, EventoBE.Titulo);
                objDB.AddInParameter(objCMD, "@IdTipoRiesgo", DbType.Int32, EventoBE.IdTipoRiesgo);
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, EventoBE.IdArea);
                objDB.AddInParameter(objCMD, "@IdEventoRiesgoDetectado", DbType.Int32, EventoBE.IdEventoRiesgoDetectado);
                objDB.AddInParameter(objCMD, "@IdGradoCriticidad", DbType.Int32, EventoBE.IdGradoCriticidad);
                objDB.AddInParameter(objCMD, "@FechaEvento", DbType.DateTime, EventoBE.FechaEvento == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaEvento);
                objDB.AddInParameter(objCMD, "@DetalleEvento", DbType.String, EventoBE.DetalleEvento);
                objDB.AddInParameter(objCMD, "@MontoProductoPerdida", DbType.Decimal, EventoBE.MontoProductoPerdida);
                objDB.AddInParameter(objCMD, "@MontoPosiblePerdida", DbType.Decimal, EventoBE.MontoPosiblePerdida);
                objDB.AddInParameter(objCMD, "@MontoTotal", DbType.Decimal, EventoBE.MontoTotal);
                objDB.AddInParameter(objCMD, "@ComentarioGerencia", DbType.String, EventoBE.ComentarioGerencia);
                objDB.AddInParameter(objCMD, "@Recomendaciones", DbType.String, EventoBE.Recomendaciones);
                objDB.AddInParameter(objCMD, "@ConCopiaA", DbType.String, EventoBE.ConCopiaA);
                objDB.AddInParameter(objCMD, "@NombreConCopiaA", DbType.String, EventoBE.NombreConCopiaA);
                objDB.AddInParameter(objCMD, "@Aplica", DbType.Boolean, EventoBE.Aplica);
                objDB.AddInParameter(objCMD, "@FechaModificacion", DbType.DateTime, EventoBE.FechaModificacion == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaModificacion);
                objDB.AddInParameter(objCMD, "@UsuarioModificacion", DbType.String, EventoBE.UsuarioModificacion);
                objDB.AddInParameter(objCMD, "@IdEstadoRO", DbType.Int32, EventoBE.IdEstadoRO == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRO);
                objDB.AddInParameter(objCMD, "@IdEstadoRS", DbType.Int32, EventoBE.IdEstadoRS == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRS);
                objDB.AddInParameter(objCMD, "@IdEstadoRA", DbType.Int32, EventoBE.IdEstadoRA == 0 ? (object)DBNull.Value : EventoBE.IdEstadoRA);
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, EventoBE.IdGerenciaDivision);
                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, EventoBE.IdGerenciaCentral);
                objDB.AddInParameter(objCMD, "@NumEventoRiesgo", DbType.String, EventoBE.NumEventoRiesgo);
                objDB.AddInParameter(objCMD, "@NumInforme", DbType.String, EventoBE.NumInforme);
                objDB.AddInParameter(objCMD, "@FlagSinGestion", DbType.Boolean, EventoBE.FlagSinGestion);
                objDB.AddInParameter(objCMD, "@PlazoDias", DbType.String, EventoBE.PlazoDias);
                objDB.AddInParameter(objCMD, "@FechaImplementacion", DbType.DateTime, EventoBE.FechaImplementacion == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaImplementacion);
                objDB.AddInParameter(objCMD, "@NotificarGerenciaGeneral", DbType.Boolean, EventoBE.NotificarGerenciaGeneral);
                objDB.AddInParameter(objCMD, "@ComentarioResponsableSeguimiento", DbType.String, EventoBE.ComentarioResponsableSeguimiento);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRA", DbType.DateTime, EventoBE.FechaProgramadaRA == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaProgramadaRA);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRO", DbType.DateTime, EventoBE.FechaProgramadaRO == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaProgramadaRO);
                objDB.AddInParameter(objCMD, "@PlanAccionRA", DbType.String, EventoBE.PlanAccionRA);
                objDB.AddInParameter(objCMD, "@FechaEjecucionRO", DbType.DateTime, EventoBE.FechaEjecucioRO == DateTime.MinValue ? (object)DBNull.Value : EventoBE.FechaEjecucioRO);
                objDB.AddInParameter(objCMD, "@IdInforme", DbType.Int32, EventoBE.IdInforme);
                objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, EventoBE.IdEstadoEventoRiesgo);
                objDB.AddInParameter(objCMD, "@IdImpacto", DbType.Int32, EventoBE.IdImpacto);
                objDB.AddInParameter(objCMD, "@IdProbabilidad", DbType.Int32, EventoBE.IdProbabilidad);
                objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, EventoBE.ResponsableArea);
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
        public bool ActualizarEventoEstadoRS(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RS_DATA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@PlazoDias", DbType.Int32, EventoBE.PlazoDias);
                objDB.AddInParameter(objCMD, "@NotificarGerenciaGeneral", DbType.Boolean, EventoBE.NotificarGerenciaGeneral);
                objDB.AddInParameter(objCMD, "@ComentarioResponsableSeguimiento", DbType.String, EventoBE.ComentarioResponsableSeguimiento);
                objDB.AddInParameter(objCMD, "@IdEstadoRS", DbType.Int32, EventoBE.IdEstadoRS);
                objDB.AddInParameter(objCMD, "@MotivoRechazoSeguimiento", DbType.String, EventoBE.MotivoRechazoSeguimiento);
               // objDB.AddInParameter(objCMD, "@ResponsableSeguimiento", DbType.String, EventoBE.ResponsableSeguimiento);
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
        public bool ActualizarEnviadoRA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ENVIADO_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@EnviadoRA", DbType.Int32, EventoBE.EnviadoRA);
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

        public bool ActualizarEstadoRA(int idEvento, int idEstadoRa)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@IdEstadoRA", DbType.Int32, idEstadoRa);
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
        public bool ActualizarEstadoRO(int idEvento, int idEstadoRO)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@IdEstadoRO", DbType.Int32, idEstadoRO);
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
        public bool ActualizarEstadoRS(int idEvento, int idEstadoRS)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RS"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@IdEstadoRS", DbType.Int32, idEstadoRS);
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
        public bool ActualizarAsignarRO_RA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ASIGNARRO_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@AsignarRO_RA", DbType.Int32, EventoBE.AsignarRO_RA);
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
        public bool ActualizarIniciarSeguimiento_RA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_INICIARSEGUIMIENTO_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@IniciarSeguimiento_RA", DbType.Int32, EventoBE.IniciarSeguimiento_RA);
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
        public bool ActualizarObviadoRO_RA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_OBVIADORO_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@ObviadoRO_RA", DbType.Int32, EventoBE.ObviadoRO_RA);
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
        public bool ActualizarEnviadoRS(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RS"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@EnviadoRS", DbType.Int32, EventoBE.EnviadoRS);
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
        public bool ActualizarEventoEstadoRA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RA_DATA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@PlanAccionRA", DbType.String, EventoBE.PlanAccionRA);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRA", DbType.DateTime, EventoBE.FechaProgramadaRA);
                objDB.AddInParameter(objCMD, "@IdEstadoRA", DbType.Int32, EventoBE.IdEstadoRA);
                objDB.AddInParameter(objCMD, "@MotivoRechazoResponsable", DbType.String, EventoBE.MotivoRechazoRA);
                objDB.AddInParameter(objCMD, "@IdResponsableObservacion", DbType.Int32, EventoBE.IdResponsableObservacion);
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
        public bool ActualizarEventoEstadoRO(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_ESTADO_RO_DATA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@ComentarioResponsableObservacion", DbType.String, EventoBE.ComentarioResponsableObservacion);
                objDB.AddInParameter(objCMD, "@FechaProgramadaRO", DbType.DateTime, EventoBE.FechaProgramadaRO);
                objDB.AddInParameter(objCMD, "@IdEstadoRO", DbType.Int32, EventoBE.IdEstadoRO);
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
        public bool ActualizarResponsableObservacion(int IdEvento, int IdResponsableObservacion)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_RO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdEvento);
                objDB.AddInParameter(objCMD, "@IdResponsableObservacion", DbType.Int32, IdResponsableObservacion);
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
        public bool ActualizarCamposRA(int IdEvento, int idGerenciaCentral,int idGerenciaDivision, int idArea, string ResponsableArea,int idResponsableObservacion)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_EVENTORIESGO_DATOS_RA_REASIGNACION"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, IdEvento);
                objDB.AddInParameter(objCMD, "@IdGerenciaCentral", DbType.Int32, idGerenciaCentral);
                objDB.AddInParameter(objCMD, "@IdGerenciaDivision", DbType.Int32, idGerenciaDivision);
                objDB.AddInParameter(objCMD, "@IdArea", DbType.Int32, idArea);
                objDB.AddInParameter(objCMD, "@ResponsableArea", DbType.String, ResponsableArea);
                objDB.AddInParameter(objCMD, "@IdResponsableObservacion", DbType.Int32, idResponsableObservacion);
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
        public bool ActualizarEventoEstado(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_ESTADOEVENTO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, EventoBE.IdEstadoEventoRiesgo);
                objDB.AddInParameter(objCMD, "@IdInstanciaWorkFlow", DbType.Int32, EventoBE.IdInstanciaWorkFlow);
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
        public bool ActualizarFechaEnvioAprobacion(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_FECHA_APROBACION_EVENTO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@FechaEnvioAprobacion", DbType.DateTime, EventoBE.FechaEnvioAprobacion);
                objDB.AddInParameter(objCMD, "@ResponsableSeguimiento", DbType.String, EventoBE.ResponsableSeguimiento);
                objDB.AddInParameter(objCMD, "@FechaImplementacion", DbType.DateTime, EventoBE.FechaImplementacion);
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
        public bool ActualizarFechaCierreRS(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_FECHA_CIERRE_RS"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@FechaCierreRS", DbType.DateTime, EventoBE.FechaCierreRS);
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
        public bool ActualizarFechaCierreRA(REventoRiesgoBE EventoBE)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_FECHA_CIERRE_RA"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, EventoBE.Id);
                objDB.AddInParameter(objCMD, "@FechaCierreRA", DbType.DateTime, EventoBE.FechaCierreRA);
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
        public bool ActualizarResponsableSeguimiento(int idEvento, string ResponsableSeguimiento)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_RS_EVENTO"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@ResponsableSeguimiento", DbType.String, ResponsableSeguimiento);
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
        public List<REventoRiesgoBE> ObtenerEventosRiesgosPorInforme(int IdInforme, string NumeroEvento)
        {
            List<REventoRiesgoBE> oListaEventos = new List<REventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_EVENTO_INFORME"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdInforme", DbType.Int64, IdInforme);
                    objDB.AddInParameter(objCMD, "@NumeroEvento", DbType.String, NumeroEvento);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REventoRiesgoBE oEventoBE = new REventoRiesgoBE();
                            oEventoBE.ResponsableGerenciaDivision = oDataReader["ResponsableGerenciaDivision"] == DBNull.Value ? "" : (string)oDataReader["ResponsableGerenciaDivision"];
                            oEventoBE.Id = (int)oDataReader["Id"];
                            oEventoBE.Titulo = (string)oDataReader["TituloEvento"];
                            oEventoBE.IdTipoRiesgo = (int)oDataReader["IdTipoRiesgo"];
                            oEventoBE.IdArea = oDataReader["IdArea"] == DBNull.Value ? 0 : (int)oDataReader["IdArea"];
                            oEventoBE.IdEventoRiesgoDetectado = (int)oDataReader["IdEventoRiesgoDetectado"];
                            oEventoBE.IdGradoCriticidad = oDataReader["IdGradoCriticidad"] == DBNull.Value ? 0 : (int)oDataReader["IdGradoCriticidad"];
                            oEventoBE.FechaEvento = (DateTime)oDataReader["FechaEvento"];
                            oEventoBE.DetalleEvento = (string)oDataReader["DetalleEvento"];
                            oEventoBE.MontoProductoPerdida = (decimal)oDataReader["MontoProductoPerdida"];
                            oEventoBE.MontoPosiblePerdida = (decimal)oDataReader["MontoPosiblePerdida"];
                            oEventoBE.MontoTotal = (decimal)oDataReader["MontoTotal"];
                            oEventoBE.ComentarioGerencia = oDataReader["ComentarioGerencia"] == DBNull.Value ? "" : (string)oDataReader["ComentarioGerencia"]; ;
                            oEventoBE.Recomendaciones = (string)oDataReader["Recomendaciones"];
                            oEventoBE.ConCopiaA = oDataReader["ConCopiaA"] == DBNull.Value ? "" : (string)oDataReader["ConCopiaA"];
                            oEventoBE.Aplica = (bool)oDataReader["Aplica"];
                            oEventoBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oEventoBE.UsuarioModificacion = (string)oDataReader["UsuarioModificacion"];
                            oEventoBE.NombreUsuarioCreador = (string)oDataReader["NombreUsuarioCreador"];
                            oEventoBE.IdEstadoRO = oDataReader["IdEstadoRO"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRO"];
                            oEventoBE.IdEstadoRS = oDataReader["IdEstadoRS"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRS"];
                            oEventoBE.IdEstadoRA = oDataReader["IdEstadoRA"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRA"];
                            oEventoBE.IdGerenciaCentral = (int)oDataReader["IdGerenciaCentral"];
                            oEventoBE.IdGerenciaDivision = oDataReader["IdGerenciaDivision"] == DBNull.Value ? 0 : (int)oDataReader["IdGerenciaDivision"]; ;
                            oEventoBE.NumInforme = (string)oDataReader["NumInforme"];
                            oEventoBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oEventoBE.FlagSinGestion = (bool)oDataReader["FlagSinGestion"];
                            oEventoBE.PlazoDias = oDataReader["PlazoDias"] == DBNull.Value ? 0 : (int)oDataReader["PlazoDias"];
                            oEventoBE.FechaImplementacion = oDataReader["FechaImplementacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaImplementacion"];
                            oEventoBE.NotificarGerenciaGeneral = (bool)oDataReader["NotificarGerenciaGeneral"];
                            oEventoBE.ComentarioResponsableSeguimiento = oDataReader["ComentarioResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsableSeguimiento"];
                            oEventoBE.ComentarioResponsableObservacion = oDataReader["ComentarioResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsableObservacion"];
                            oEventoBE.FechaProgramadaRA = oDataReader["FechaProgramadaRA"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRA"];
                            oEventoBE.FechaProgramadaRO = oDataReader["FechaProgramadaRO"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRO"];
                            oEventoBE.PlanAccionRA = oDataReader["PlanAccionRA"] == DBNull.Value ? "" : (string)oDataReader["PlanAccionRA"];
                            oEventoBE.IdInforme = (int)oDataReader["IdInforme"];
                            oEventoBE.IdEstadoEventoRiesgo = (int)oDataReader["IdEstadoEventoRiesgo"];
                            oEventoBE.DescripcionCriticidad = oDataReader["GradoCriticidad"] == DBNull.Value ? "" : (string)oDataReader["GradoCriticidad"];
                            oEventoBE.ColorCriticidad = oDataReader["Color"] == DBNull.Value ? "" : (string)oDataReader["Color"];
                            oEventoBE.TipoRiesgo = (string)oDataReader["TipoRiesgo"];
                            oEventoBE.Area = oDataReader["Area"] == DBNull.Value ? "" : (string)oDataReader["Area"];
                            oEventoBE.RiesgoEventoDetectado = (string)oDataReader["RiesgoEventoDetectado"];
                            oEventoBE.GerenciaDivision = oDataReader["GerenciaDivision"] == DBNull.Value ? "" : (string)oDataReader["GerenciaDivision"];
                            oEventoBE.MotivoRechazoSeguimiento = oDataReader["MotivoRechazoSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoSeguimiento"];
                            oEventoBE.MotivoRechazoRA = oDataReader["MotivoRechazoResponsable"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoResponsable"];
                            oEventoBE.GerenciaCentral = (string)oDataReader["GerenciaCentral"];
                            oEventoBE.Estado = (string)oDataReader["Estado"];
                            oEventoBE.Impacto = oDataReader["Impacto"] == DBNull.Value ? "" : (string)oDataReader["Impacto"];
                            oEventoBE.Probabilidad = oDataReader["Probabilidad"] == DBNull.Value ? "" : (string)oDataReader["Probabilidad"];
                            oEventoBE.IdImpacto = oDataReader["IdImpacto"] == DBNull.Value ? 0 : (int)oDataReader["IdImpacto"];
                            oEventoBE.IdProbabilidad = oDataReader["IdProbabilidad"] == DBNull.Value ? 0 : (int)oDataReader["IdProbabilidad"];
                            oEventoBE.GradoCriticidad = oDataReader["GradoCriticidad"] == DBNull.Value ? "" : (string)oDataReader["GradoCriticidad"];
                            oEventoBE.IdInstanciaWorkFlow = oDataReader["IdInstanciaWorkFlow"] == DBNull.Value ? 0 : (int)oDataReader["IdInstanciaWorkFlow"];
                            oEventoBE.ResponsableSeguimiento = oDataReader["ResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["ResponsableSeguimiento"];
                            oEventoBE.IdResponsableObservacion = oDataReader["IdResponsableObservacion"] == DBNull.Value ? 0 : (int)oDataReader["IdResponsableObservacion"];
                            oEventoBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            oEventoBE.VistoBuenoRevisor = (bool)oDataReader["VistoBuenoRevisor"];
                            oEventoBE.VistoBuenoAprobador = (bool)oDataReader["VistoBuenoAprobador"];
                            oEventoBE.VistoBuenoResponsable = (bool)oDataReader["VistoBuenoResponsable"];
                            oEventoBE.DescripcionEstadoRA = oDataReader["DescripcionEstadoRA"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRA"];
                            oEventoBE.DescripcionEstadoRO = oDataReader["DescripcionEstadoRO"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRO"];
                            oEventoBE.DescripcionEstadoRS = oDataReader["DescripcionEstadoRS"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRS"];
                            oEventoBE.FechaEnvioAprobacion = oDataReader["FechaEnvioAprobacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEnvioAprobacion"];
                            oEventoBE.AsignarRO_RA = oDataReader["AsignarRO_RA"] == DBNull.Value ? false : (bool)oDataReader["AsignarRO_RA"];
                            oEventoBE.IniciarSeguimiento_RA = oDataReader["IniciarSeguimiento_RA"] == DBNull.Value ? false : (bool)oDataReader["IniciarSeguimiento_RA"];
                            oEventoBE.ObviadoRO_RA = oDataReader["ObviadoRO_RA"] == DBNull.Value ? false : (bool)oDataReader["ObviadoRO_RA"];
                            oEventoBE.ResponsableArea = oDataReader["ResponsableArea"] == DBNull.Value ? "" : (string)oDataReader["ResponsableArea"];
                            oEventoBE.FechaCreacion = oDataReader["FechaCreacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaCreacion"];
                            oListaEventos.Add(oEventoBE);
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oListaEventos;
        }

        public REventoRiesgoBE ObtenerEventoPorId(Int64 piIdEvento)
        {
            REventoRiesgoBE oEventoBE = new REventoRiesgoBE();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENEREVENTOPORID"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Id", DbType.Int32, piIdEvento);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        if (oDataReader.Read())
                        {
                            oEventoBE.ResponsableGerenciaDivision = oDataReader["ResponsableGerenciaDivision"] == DBNull.Value ? "" : (string)oDataReader["ResponsableGerenciaDivision"]; 
                            oEventoBE.Id = (int)oDataReader["Id"];
                            oEventoBE.Titulo = (string)oDataReader["TituloEvento"];
                            oEventoBE.IdTipoRiesgo = (int)oDataReader["IdTipoRiesgo"];
                            oEventoBE.IdArea = oDataReader["IdArea"] == DBNull.Value ? 0 : (int)oDataReader["IdArea"];
                            oEventoBE.IdEventoRiesgoDetectado = (int)oDataReader["IdEventoRiesgoDetectado"];
                            oEventoBE.IdGradoCriticidad = oDataReader["IdGradoCriticidad"] == DBNull.Value ? 0 : (int)oDataReader["IdGradoCriticidad"];
                            oEventoBE.FechaEvento = (DateTime)oDataReader["FechaEvento"];
                            oEventoBE.DetalleEvento = (string)oDataReader["DetalleEvento"];
                            oEventoBE.MontoProductoPerdida = (decimal)oDataReader["MontoProductoPerdida"];
                            oEventoBE.MontoPosiblePerdida = (decimal)oDataReader["MontoPosiblePerdida"];
                            oEventoBE.MontoTotal = (decimal)oDataReader["MontoTotal"];
                            oEventoBE.ComentarioGerencia = oDataReader["ComentarioGerencia"] == DBNull.Value ? "" : (string)oDataReader["ComentarioGerencia"]; ;
                            oEventoBE.Recomendaciones = (string)oDataReader["Recomendaciones"];
                            oEventoBE.ConCopiaA = oDataReader["ConCopiaA"]==DBNull.Value ? "": (string) oDataReader["ConCopiaA"] ;
                            oEventoBE.Aplica = (bool)oDataReader["Aplica"];
                            oEventoBE.FechaModificacion = (DateTime)oDataReader["FechaModificacion"];
                            oEventoBE.UsuarioModificacion = (string)oDataReader["UsuarioModificacion"];
                            oEventoBE.NombreUsuarioCreador = (string)oDataReader["NombreUsuarioCreador"];
                            oEventoBE.IdEstadoRO = oDataReader["IdEstadoRO"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRO"];
                            oEventoBE.IdEstadoRS = oDataReader["IdEstadoRS"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRS"];
                            oEventoBE.IdEstadoRA = oDataReader["IdEstadoRA"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoRA"];
                            oEventoBE.IdGerenciaCentral = (int)oDataReader["IdGerenciaCentral"];
                            oEventoBE.IdGerenciaDivision = oDataReader["IdGerenciaDivision"] == DBNull.Value ? 0 : (int)oDataReader["IdGerenciaDivision"]; ;
                            oEventoBE.NumInforme = (string)oDataReader["NumInforme"];
                            oEventoBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oEventoBE.FlagSinGestion = (bool)oDataReader["FlagSinGestion"];
                            oEventoBE.PlazoDias = oDataReader["PlazoDias"] == DBNull.Value ? 0: (int)oDataReader["PlazoDias"];
                            oEventoBE.FechaImplementacion = oDataReader["FechaImplementacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaImplementacion"];
                            oEventoBE.NotificarGerenciaGeneral = (bool)oDataReader["NotificarGerenciaGeneral"];
                            oEventoBE.ComentarioResponsableSeguimiento = oDataReader["ComentarioResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsableSeguimiento"];
                            oEventoBE.ComentarioResponsableObservacion = oDataReader["ComentarioResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ComentarioResponsableObservacion"];
                            oEventoBE.FechaProgramadaRA = oDataReader["FechaProgramadaRA"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRA"];
                            oEventoBE.FechaProgramadaRO = oDataReader["FechaProgramadaRO"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaProgramadaRO"];
                            oEventoBE.PlanAccionRA = oDataReader["PlanAccionRA"] == DBNull.Value ? "" : (string)oDataReader["PlanAccionRA"];
                            oEventoBE.IdInforme = (int)oDataReader["IdInforme"];
                            oEventoBE.IdEstadoEventoRiesgo = (int)oDataReader["IdEstadoEventoRiesgo"];
                            oEventoBE.DescripcionCriticidad = oDataReader["GradoCriticidad"] == DBNull.Value ? "" : (string)oDataReader["GradoCriticidad"];
                            oEventoBE.ColorCriticidad = oDataReader["Color"] == DBNull.Value ? "" : (string)oDataReader["Color"];
                            oEventoBE.TipoRiesgo = (string)oDataReader["TipoRiesgo"];
                            oEventoBE.Area = oDataReader["Area"] == DBNull.Value ? "" : (string)oDataReader["Area"];
                            oEventoBE.RiesgoEventoDetectado = (string)oDataReader["RiesgoEventoDetectado"];
                            oEventoBE.GerenciaDivision = oDataReader["GerenciaDivision"] == DBNull.Value ? "" : (string)oDataReader["GerenciaDivision"];
                            oEventoBE.MotivoRechazoSeguimiento = oDataReader["MotivoRechazoSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoSeguimiento"];
                            oEventoBE.MotivoRechazoRA = oDataReader["MotivoRechazoResponsable"] == DBNull.Value ? "" : (string)oDataReader["MotivoRechazoResponsable"]; 
                            oEventoBE.GerenciaCentral = (string)oDataReader["GerenciaCentral"];
                            oEventoBE.Estado = (string)oDataReader["Estado"];
                            oEventoBE.Impacto = oDataReader["Impacto"] == DBNull.Value ? "" : (string)oDataReader["Impacto"]; 
                            oEventoBE.Probabilidad = oDataReader["Probabilidad"] == DBNull.Value ? "" : (string)oDataReader["Probabilidad"]; 
                            oEventoBE.IdImpacto = oDataReader["IdImpacto"] == DBNull.Value ? 0 : (int)oDataReader["IdImpacto"];
                            oEventoBE.IdProbabilidad = oDataReader["IdProbabilidad"] == DBNull.Value ? 0 : (int)oDataReader["IdProbabilidad"];
                            oEventoBE.GradoCriticidad = oDataReader["GradoCriticidad"] == DBNull.Value ? "" : (string)oDataReader["GradoCriticidad"];
                            oEventoBE.IdInstanciaWorkFlow = oDataReader["IdInstanciaWorkFlow"] == DBNull.Value ? 0 : (int)oDataReader["IdInstanciaWorkFlow"];
                            oEventoBE.ResponsableSeguimiento = oDataReader["ResponsableSeguimiento"] == DBNull.Value ? "" : (string)oDataReader["ResponsableSeguimiento"];
                            oEventoBE.IdResponsableObservacion = oDataReader["IdResponsableObservacion"] == DBNull.Value ? 0 : (int)oDataReader["IdResponsableObservacion"];
                            oEventoBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            oEventoBE.VistoBuenoRevisor = (bool)oDataReader["VistoBuenoRevisor"];
                            oEventoBE.VistoBuenoAprobador = (bool)oDataReader["VistoBuenoAprobador"];
                            oEventoBE.VistoBuenoResponsable = (bool)oDataReader["VistoBuenoResponsable"];
                            oEventoBE.DescripcionEstadoRA = oDataReader["DescripcionEstadoRA"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRA"];
                            oEventoBE.DescripcionEstadoRO = oDataReader["DescripcionEstadoRO"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRO"];
                            oEventoBE.DescripcionEstadoRS = oDataReader["DescripcionEstadoRS"] == DBNull.Value ? "" : (string)oDataReader["DescripcionEstadoRS"];
                            oEventoBE.FechaEnvioAprobacion = oDataReader["FechaEnvioAprobacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaEnvioAprobacion"];
                            oEventoBE.AsignarRO_RA = oDataReader["AsignarRO_RA"] == DBNull.Value ? false : (bool)oDataReader["AsignarRO_RA"];
                            oEventoBE.IniciarSeguimiento_RA = oDataReader["IniciarSeguimiento_RA"] == DBNull.Value ? false : (bool)oDataReader["IniciarSeguimiento_RA"];
                            oEventoBE.ObviadoRO_RA = oDataReader["ObviadoRO_RA"] == DBNull.Value ? false : (bool)oDataReader["ObviadoRO_RA"];
                            oEventoBE.ResponsableArea = oDataReader["ResponsableArea"] == DBNull.Value ? "" : (string)oDataReader["ResponsableArea"];
                            oEventoBE.FechaCreacion = oDataReader["FechaCreacion"] == DBNull.Value ? new DateTime() : (DateTime)oDataReader["FechaCreacion"];
                            oEventoBE.Sociedad = oDataReader["Sociedad"] == DBNull.Value ? "" : (string)oDataReader["Sociedad"];
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            return oEventoBE;
        }
        public bool ActualizarVistoBuenoEventoRevisor(bool VBRevisor, int idEvento)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_VISTOBUENOEVENTOREVISOR"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@VistoBuenoRevisor", DbType.Boolean, VBRevisor);
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
        public bool ActualizarVistoBuenoEventoAprobador(bool VBAprobador, int idEvento)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_VISTOBUENOEVENTOAPROBADOR"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
                objDB.AddInParameter(objCMD, "@VistoBuenoAprobador", DbType.Boolean, VBAprobador);
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
        public bool ActualizarVistoBuenoEventoResponsable(bool VBResponsable, int idEvento)
        {
            int res = 0;
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_UPD_VISTOBUENOEVENTORESPONSABLE"))
            {
                objDB.AddInParameter(objCMD, "@Id", DbType.Int32, idEvento);
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
        public List<REstadoRSBE> ObtenerEstadosRS()
        {
            List<REstadoRSBE> oListaEstados = new List<REstadoRSBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERESTADO_RS"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REstadoRSBE oEstadoBE = new REstadoRSBE();
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
        public List<RGradoCriticidadBE> ObtenerGradoCriticidad()
        {
            List<RGradoCriticidadBE> oListaGradoCriticidad = new List<RGradoCriticidadBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_GRADO_CRITICIDAD"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            RGradoCriticidadBE oGradoCriticidadBE = new RGradoCriticidadBE();
                            oGradoCriticidadBE.Id = (int)oDataReader["Id"];
                            oGradoCriticidadBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaGradoCriticidad.Add(oGradoCriticidadBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaGradoCriticidad;
        }
        public List<REstadoRABE> ObtenerEstadosRA()
        {
            List<REstadoRABE> oListaEstados = new List<REstadoRABE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERESTADO_RA"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REstadoRABE oEstadoBE = new REstadoRABE();
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
        public List<REstadoRABE> ObtenerEstadosRO()
        {
            List<REstadoRABE> oListaEstados = new List<REstadoRABE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERESTADO_RO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REstadoRABE oEstadoBE = new REstadoRABE();
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
        public List<REstadoEventoRiesgoBE> ObtenerEstados()
        {
            List<REstadoEventoRiesgoBE> oListaEstados = new List<REstadoEventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENERESTADO_EVENTORIESGO"))
            {
                try
                {
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REstadoEventoRiesgoBE oEstadoBE = new REstadoEventoRiesgoBE();
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
        public List<RAnioBE> ObtenerAnios()
        {
            List<RAnioBE> oListaAnios = new List<RAnioBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_ANIOS_CREACION_EVENTO"))
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

        public List<REventoRiesgoBE> ObtenerEventosTareaUsuario(int IdEstadoEvento, string usuario, string estadoTarea, string anio, string evento, int idInstancia)
        {
            List<REventoRiesgoBE> oListaEventos = new List<REventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_EVENTO_POR_INSTANCIA"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, IdEstadoEvento);
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, usuario);
                    objDB.AddInParameter(objCMD, "@EstadoTarea", DbType.String, estadoTarea);
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@NumEventoRiesgo", DbType.String, evento);
                    objDB.AddInParameter(objCMD, "@IdInstancia", DbType.Int32, idInstancia);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REventoRiesgoBE oEventoBE = new REventoRiesgoBE();
                            oEventoBE.Id = (int)oDataReader["Id"];
                            oEventoBE.IdInforme = (int)oDataReader["IdInforme"];
                            oEventoBE.NumInforme = (string)oDataReader["NumInforme"];
                            oEventoBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oEventoBE.Titulo = (string)oDataReader["TituloEvento"];
                            oEventoBE.GradoCriticidad = (string)oDataReader["GradoCriticidad"];
                            oEventoBE.Estado = (string)oDataReader["Estado"];
                            oEventoBE.Area = (string)oDataReader["Area"];
                            oEventoBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oEventoBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            oEventoBE.IdResponsableObservacion = oDataReader["IdResponsableObservacion"] == DBNull.Value ? 0: (int)oDataReader["IdResponsableObservacion"];
                            oEventoBE.IdEstadoTransaccional = oDataReader["IdEstadoTransaccional"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoTransaccional"];
                            oEventoBE.IdEstadoEventoRiesgo = oDataReader["IdEstadoEventoRiesgo"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoEventoRiesgo"]; 
                            oListaEventos.Add(oEventoBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaEventos;
        }
        public List<REventoRiesgoBE> ObtenerEventosTareaUsuarioParticipa(int IdEstadoEvento, string usuario, string anio, string evento)
        {
            List<REventoRiesgoBE> oListaEventos = new List<REventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_EVENTO_POR_USUARIO"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@IdEstadoEventoRiesgo", DbType.Int32, IdEstadoEvento);
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, usuario);
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@NumEventoRiesgo", DbType.String, evento);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REventoRiesgoBE oEventoBE = new REventoRiesgoBE();
                            oEventoBE.Id = (int)oDataReader["Id"];
                            oEventoBE.IdEstadoEventoRiesgo = (int)oDataReader["IdEstadoEventoRiesgo"];
                            oEventoBE.IdInforme = (int)oDataReader["IdInforme"];
                            oEventoBE.NumInforme = (string)oDataReader["NumInforme"];
                            oEventoBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oEventoBE.Titulo = (string)oDataReader["TituloEvento"];
                            oEventoBE.GradoCriticidad = (string)oDataReader["GradoCriticidad"];
                            oEventoBE.Estado = (string)oDataReader["Estado"];
                            oEventoBE.Area = (string)oDataReader["Area"];
                            oEventoBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oEventoBE.Aesperade = (string)oDataReader["Aesperade"];
                            //oEventoBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            //oEventoBE.IdResponsableObservacion = oDataReader["IdResponsableObservacion"] == DBNull.Value ? 0 : (int)oDataReader["IdResponsableObservacion"];
                            //oEventoBE.IdEstadoTransaccional = oDataReader["IdEstadoTransaccional"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoTransaccional"];
                            //oEventoBE.IdEstadoEventoRiesgo = oDataReader["IdEstadoEventoRiesgo"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoEventoRiesgo"];
                            oListaEventos.Add(oEventoBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaEventos;
        }
        public List<REventoRiesgoBE> ObtenerEventosPorAnioPendiente(string anio, int idEstado, string cuenta)
        {
            List<REventoRiesgoBE> oListaEventos = new List<REventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_EVENTOPORANIO_PENDIENTE"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@Anio", DbType.String, anio);
                    objDB.AddInParameter(objCMD, "@IdEstado", DbType.String, idEstado);
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, cuenta);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REventoRiesgoBE oInformedBE = new REventoRiesgoBE();
                            oInformedBE.Id = (int)oDataReader["Id"];
                            oInformedBE.Descripcion = (string)oDataReader["Descripcion"];
                            oListaEventos.Add(oInformedBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaEventos;
        }
        public List<REventoRiesgoBE> ObtenerEventosPorParticipate(string usuario)
        {
            List<REventoRiesgoBE> oListaEventos = new List<REventoRiesgoBE>();
            Database objDB = Util.CrearBaseDatos();
            using (DbCommand objCMD = objDB.GetStoredProcCommand("USPR_GET_OBTENER_EVENTO_POR_ROLPARTICIPANTE"))
            {
                try
                {
                    objDB.AddInParameter(objCMD, "@CuentaUsuario", DbType.String, usuario);
                    using (IDataReader oDataReader = objDB.ExecuteReader(objCMD))
                    {
                        while (oDataReader.Read())
                        {
                            REventoRiesgoBE oEventoBE = new REventoRiesgoBE();
                            oEventoBE.Id = (int)oDataReader["Id"];
                            oEventoBE.IdInforme = (int)oDataReader["IdInforme"];
                            oEventoBE.NumInforme = (string)oDataReader["NumInforme"];
                            oEventoBE.NumEventoRiesgo = (string)oDataReader["NumEventoRiesgo"];
                            oEventoBE.Titulo = (string)oDataReader["TituloEvento"];
                            oEventoBE.GradoCriticidad = (string)oDataReader["GradoCriticidad"];
                            oEventoBE.Estado = (string)oDataReader["Estado"];
                            oEventoBE.Area = (string)oDataReader["Area"];
                            oEventoBE.ResponsableArea = (string)oDataReader["ResponsableArea"];
                            oEventoBE.ResponsableObservacion = oDataReader["ResponsableObservacion"] == DBNull.Value ? "" : (string)oDataReader["ResponsableObservacion"];
                            oEventoBE.IdResponsableObservacion = oDataReader["IdResponsableObservacion"] == DBNull.Value ? 0 : (int)oDataReader["IdResponsableObservacion"];
                            oEventoBE.IdEstadoEventoRiesgo = oDataReader["IdEstadoEventoRiesgo"] == DBNull.Value ? 0 : (int)oDataReader["IdEstadoEventoRiesgo"]; 
                            oListaEventos.Add(oEventoBE);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return oListaEventos;
        }
    }
}
