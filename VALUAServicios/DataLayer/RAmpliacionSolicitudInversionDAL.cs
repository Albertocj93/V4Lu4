using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using BusinessEntities;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using Common;
using System.Xml;
using BusinessEntities.DTO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace DataLayer
{
    public class RAmpliacionSolicitudInversionDAL
    {
        public RResultadoAmpliacionAPIBE InsertarAmpliacionSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {
            string track = "";
            string Correlativo = "";
            RResultadoAmpliacionAPIBE oRespuesta = new RResultadoAmpliacionAPIBE();

            ManejadorLogSimpleBL.WriteLog("ENTRO 1");
            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalSolInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RCoordinadorBE oCoordinador = new RCoordinadorBE();
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            ManejadorLogSimpleBL.WriteLog("ENTRO 2");
            if (oSolicitudInversion == null)
            {
                oRespuesta.success = false;
                return oRespuesta;
            }


            bool resultadoCabecera = false;

            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));

            ScriptorContent contenidoCabecera = canalCabecera.NewContent();


            try
            {

                #region SetearCabecera
                //Para Ampliacion API
                oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                oSolicitudInversion.IdTipoAPI.Id = TiposAPI.IdAmpliacion;
                oSolicitudInversion.FechaCreacion = DateTime.Now;
                ManejadorLogSimpleBL.WriteLog("ENTRO 3");
                if (oSolicitudInversion.IdSociedad != null)
                    Correlativo = GenerarCorrelativo(oSolicitudInversion.IdSociedad.Id.ToString(), oSolicitudInversion.IdTipoAPI.Id.ToString(), oSolicitudInversion.FechaCreacion.ToShortDateString());

                track += "genero el correlativo " + Correlativo;
                contenidoCabecera.Parts.NumSolicitud = Correlativo;
                contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.NombreCortoSolicitud = oSolicitudInversion.NombreCortoSolicitud;
                contenidoCabecera.Parts.FechaInicio = oSolicitudInversion.FechaInicio;
                contenidoCabecera.Parts.FechaCierre = oSolicitudInversion.FechaCierre;
                contenidoCabecera.Parts.Ubicacion = oSolicitudInversion.Ubicacion;
                contenidoCabecera.Parts.Marca = oSolicitudInversion.Marca;
                contenidoCabecera.Parts.Descripcion = oSolicitudInversion.Descripcion != null ? oSolicitudInversion.Descripcion : "";
                contenidoCabecera.Parts.Observaciones = oSolicitudInversion.Observaciones != null ? oSolicitudInversion.Observaciones : "";

                if (oSolicitudInversion.Responsable != null)
                    contenidoCabecera.Parts.Responsable = oSolicitudInversion.Responsable.CuentaUsuario;

                contenidoCabecera.Parts.Van = oSolicitudInversion.VAN;
                contenidoCabecera.Parts.Tir = oSolicitudInversion.TIR;
                contenidoCabecera.Parts.Pri = oSolicitudInversion.PRI;
                contenidoCabecera.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras != null ? oSolicitudInversion.ObservacionesFinancieras : "";
                contenidoCabecera.Parts.VanAmpliacion = oSolicitudInversion.VANAmpliacion;
                contenidoCabecera.Parts.TirAmpliacion = oSolicitudInversion.TIRAmpliacion;
                contenidoCabecera.Parts.PriAmpliacion = oSolicitudInversion.PRIAmpliacion;
                contenidoCabecera.Parts.ObservacionesFinancierasAmpliacion = oSolicitudInversion.ObservacionesFinancierasAmpliacion != null ? oSolicitudInversion.ObservacionesFinancierasAmpliacion : "";
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto != null ? oSolicitudInversion.NombreProyecto : "";

                if (oSolicitudInversion.ResponsableProyecto != null)
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;

                contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FlagTipoBolsa = oSolicitudInversion.FlagTipoBolsa;
                contenidoCabecera.Parts.MontoAprobadoPlanBase = oSolicitudInversion.MontoAprobadoPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioCreador = oSolicitudInversion.UsuarioCreador != null ? oSolicitudInversion.UsuarioCreador.CuentaUsuario : "";
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico != null ? oSolicitudInversion.UsuarioModifico.CuentaUsuario : "";
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
                //contenidoCabecera.Parts.IdAPIInicial = oSolicitudInversion.IdAPIInicial;
                ManejadorLogSimpleBL.WriteLog("ENTRO 4");
                //if (oSolicitudInversion.IdCoordinador != null)
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudInversion.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());
                if (oSolicitudInversion.IdCoordinador != null)
                {
                    oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorCodigoCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                    //ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", new Guid(), "<>")
                    //                                        .QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                    //contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                    contenidoCabecera.Parts.IdCoordinador = oCoordinador.Id.Value.ToString();
                }
                ManejadorLogSimpleBL.WriteLog("ENTRO 5");
                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 6");
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());

                if (oSolicitudInversion.IdSociedad != null)
                    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 7");
                if (oSolicitudInversion.IdTipoAPI != null)
                    contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 8");
                if (oSolicitudInversion.IdMacroServicio != null)
                    contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oSolicitudInversion.IdMacroServicio.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 9");
                if (oSolicitudInversion.IdSector != null)
                    contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSolicitudInversion.IdSector.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 10");
                if (oSolicitudInversion.IdCeCo != null)
                    contenidoCabecera.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.Value.ToString();
                ManejadorLogSimpleBL.WriteLog("ENTRO 11");
                if (oSolicitudInversion.IdAPIInicial != null)
                    contenidoCabecera.Parts.IdAPIInicial = oSolicitudInversion.IdAPIInicial.Id.ToString();
                if (oSolicitudInversion.IdPeriodoPRI != null)
                    contenidoCabecera.Parts.IdPeriodoPRI = oSolicitudInversion.IdPeriodoPRI.codigo;
                if (oSolicitudInversion.IdPeriodoPRIAmpliacion != null)
                    contenidoCabecera.Parts.IdPeriodoPRIAmpliacion = oSolicitudInversion.IdPeriodoPRIAmpliacion.codigo;

                #endregion SetearCabecera


                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();
                oRespuesta.NumSolicitud = Correlativo;

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera
                Guid? id = null;
                if (oRespuesta.success)
                {
                    id = contenidoCabecera.Id;
                    oRespuesta.Id = id.ToString();
                    oRespuesta.CoordinadorCuenta = oSolicitudInversion.IdCoordinador.CuentaRed;
                    oRespuesta.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                    oRespuesta.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    oRespuesta.FechaCreacion = contenidoCabecera.Parts.FechaCreacion;
                    oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();

                    track = "Obtuvo Id de cabecera guardada";

                    if (!String.IsNullOrEmpty(id.ToString()))
                    {

                        #region IncrementarCorrelativo

                        ScriptorContent ContenidoTipoAPI = canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault();
                        if (ContenidoTipoAPI.Parts.Correlativo != null)
                        {
                            ContenidoTipoAPI.Parts.Correlativo = ContenidoTipoAPI.Parts.Correlativo + 1;
                        }
                        else
                        {
                            ContenidoTipoAPI.Parts.Correlativo = 1;
                        }

                        ContenidoTipoAPI.Save();

                        #endregion

                    }
                }
                else
                {
                    ManejadorLogSimpleBL.WriteLog("Error al guardar cabecera: " + contenidoCabecera.LastError);
                }
            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog(ex.Message.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.StackTrace.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.InnerException.ToString());
                oRespuesta.success = false;
                return oRespuesta;
            }

            return oRespuesta;
            //return "cabecera =>" + resultadoCabecera + " detalle =>" + Common.WebUtil.ToJson(resultadoDetalle);


        }

        public bool InsertarDetalleAmpliacionSolicitudInversion(RDetalleSolicitudInversionBE det, string IdSolicitudInversion, string CodOI, string DescripcionOI)
        {
            bool detalle = false;
            List<string> resultadoDetalle = new List<string>();

            #region SetearDetalle
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorContent contenidoDetalle;
            ScriptorContent cotenidoCabecera = canalCabecera.QueryContents("#Id", IdSolicitudInversion, "=").ToList().FirstOrDefault();
            #region GuardarInversion

            bool resultInversion = false;
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            ScriptorContent contentInversion = canalInversion.NewContent();
            contentInversion.Parts.CodigoOI = CodOI;
            contentInversion.Parts.MontoDisponible = det.MontoAAmpliarUSD;
            contentInversion.Parts.MontoContable = det.MontoAAmpliarUSD;
            contentInversion.Parts.Descripcion = cotenidoCabecera.Parts.NumSolicitud;
            contentInversion.Parts.NombreProyecto = cotenidoCabecera.Parts.NombreProyecto;
            contentInversion.Parts.DescripcionOI = DescripcionOI;
            contentInversion.Parts.IdCeCo = cotenidoCabecera.Parts.IdCeCo.ToString();
            contentInversion.Parts.CodigoProyecto = cotenidoCabecera.Parts.CodigoProyecto;
            //contentInversion.Parts.IdEstado = EstadosMovimiento.Creado;
            contentInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault());
            if (det.IdTipoActivo != null)
                if (det.IdTipoActivo.Id != null)
                    contentInversion.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
            
            resultInversion = contentInversion.Save();


            #endregion

            if (resultInversion)
            {
                contenidoDetalle = canalDetalle.NewContent();

                contenidoDetalle.Parts.Cantidad = det.Cantidad;
                contenidoDetalle.Parts.PrecioUnitario = det.PrecioUnitario;
                contenidoDetalle.Parts.PrecioUnitarioUSD = det.PrecioUnitarioUSD;
                contenidoDetalle.Parts.PrecioMontoOrigen = det.PrecioMontoOrigen;
                contenidoDetalle.Parts.PrecioMontoUSD = det.PrecioMontoUSD;
                contenidoDetalle.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(cotenidoCabecera);
                if (det.IdMonedaCotizada != null)
                    contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());

                contenidoDetalle.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contentInversion);
                ManejadorLogSimpleBL.WriteLog("ENTRO 11");
                contenidoDetalle.Parts.VidaUtil = det.VidaUtil;
                                

                ManejadorLogSimpleBL.WriteLog("ENTRO 12");
                if (det.IdTipoActivo != null)
                    if (det.IdTipoActivo.Id != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                if (det.IdTipoCambio != null)
                {
                    if (det.IdTipoCambio != new Guid().ToString())
                        contenidoDetalle.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(canalTipoCambio.QueryContents("#Id", det.IdTipoCambio, "=").ToList().FirstOrDefault());
                    else
                    {
                        //Es dolares
                    }

                }

                contenidoDetalle.Parts.MontoAAmpliarMonedaCotizada = det.MontoAAmpliarMonedaCotizada;
                contenidoDetalle.Parts.VidaUtilAmpliar = det.VidaUtilAmpliar;
                contenidoDetalle.Parts.MontoAmpliarUSD = det.MontoAAmpliarUSD;


                ManejadorLogSimpleBL.WriteLog("ENTRO 13");

                detalle = contenidoDetalle.Save();

            }


            #endregion SetearDetalle

            return detalle;

        }

        public bool InsertarDetalleAmpliacionSolicitudInversionNuevo(RDetalleSolicitudInversionBE det, string IdSolicitudInversion)
        {
            bool detalle = false;
            List<string> resultadoDetalle = new List<string>();

            #region SetearDetalle
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorContent cotenidoCabecera = canalCabecera.QueryContents("#Id", IdSolicitudInversion, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoDetalle;

            #region GuardarInversion

            bool resultInversion = false;
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            ScriptorContent inv = canalInversion.NewContent();

            inv.Parts.IdCeCo = cotenidoCabecera.Parts.IdCeCo.ToString();
            if (det.IdTipoActivo != null)
                if (det.IdTipoActivo.Id != null)
                    inv.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());            
            inv.Parts.CodigoProyecto = cotenidoCabecera.Parts.CodigoProyecto;
            inv.Parts.NombreProyecto = cotenidoCabecera.Parts.NombreProyecto;
            inv.Parts.MontoDisponible = det.PrecioMontoUSD;
            inv.Parts.MontoContable = det.PrecioMontoUSD;
            inv.Parts.Descripcion = cotenidoCabecera.Parts.NumSolicitud;
            inv.Parts.FlagAmpliacion = det.FlagAmpliacion;
            inv.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault());                        
            resultInversion = inv.Save();


            #endregion

            if (resultInversion)
            {
                contenidoDetalle = canalDetalle.NewContent();

                contenidoDetalle.Parts.Cantidad = det.Cantidad;
                contenidoDetalle.Parts.PrecioUnitario = det.PrecioUnitario;
                contenidoDetalle.Parts.PrecioUnitarioUSD = det.PrecioUnitarioUSD;
                contenidoDetalle.Parts.PrecioMontoOrigen = det.PrecioMontoOrigen;
                contenidoDetalle.Parts.PrecioMontoUSD = det.PrecioMontoUSD;
                
                if(!String.IsNullOrEmpty(IdSolicitudInversion))
                    contenidoDetalle.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(canalCabecera.QueryContents("#Id", IdSolicitudInversion, "=").ToList().FirstOrDefault());

                if (det.IdMonedaCotizada != null)
                    contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());


                if (det.IdTipoCambio != null)
                {
                    if (det.IdTipoCambio != new Guid().ToString())
                        contenidoDetalle.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(canalTipoCambio.QueryContents("#Id", det.IdTipoCambio, "=").ToList().FirstOrDefault());
                    else
                    {
                        //Es dolares
                    }

                }

                contenidoDetalle.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                ManejadorLogSimpleBL.WriteLog("ENTRO 11");
                contenidoDetalle.Parts.VidaUtil = det.VidaUtil;

                ManejadorLogSimpleBL.WriteLog("ENTRO 12");
                if (det.IdTipoActivo != null)
                    if (det.IdTipoActivo.Id != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                ManejadorLogSimpleBL.WriteLog("ENTRO 13");

                detalle = contenidoDetalle.Save();

            }


            #endregion SetearDetalle

            return detalle;

        }

        public bool ModificarDetalleAmpliacionSolicitudInversionNuevo(RDetalleSolicitudInversionBE det, string IdSolicitudInversion)
        {

            bool detalle = false;
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            #region SetearDetalle

            if (det.IdDetalle != null)
            {
                ScriptorContent contenidoDetalle = canalDetalle.QueryContents("#Id", det.IdDetalle, "=").ToList().FirstOrDefault();
                //contenidoDetalle = oDetalles.Where(x => x.Id == det.IdDetalle).ToList().FirstOrDefault();
                //Actualizo
                if (contenidoDetalle != null)
                {
                    contenidoDetalle.Parts.Cantidad = det.Cantidad;
                    contenidoDetalle.Parts.PrecioUnitario = det.PrecioUnitario;
                    contenidoDetalle.Parts.PrecioUnitarioUSD = det.PrecioUnitarioUSD;
                    contenidoDetalle.Parts.PrecioMontoOrigen = det.PrecioMontoOrigen;
                    contenidoDetalle.Parts.PrecioMontoUSD = det.PrecioMontoUSD;

                    if (det.IdMonedaCotizada != null)
                        contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());

                    if (det.IdTipoActivo != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                    //contenidoDetalle.Parts.IdInversion = det.IdInversion;
                    contenidoDetalle.Parts.VidaUtil = det.VidaUtil;
                    contenidoDetalle.Parts.Monto = det.PrecioMontoUSD;

                    #region ModificarInversion
                    ScriptorContent contenidoInversion = canalInversion.QueryContents("#Id", ((ScriptorDropdownListValue)contenidoDetalle.Parts.IdInversion).Content.Id, "=").ToList().FirstOrDefault();
                    contenidoInversion.Parts.MontoDisponible = det.PrecioMontoUSD;
                    contenidoInversion.Parts.MontoContable = det.PrecioMontoUSD;
                    if (det.IdTipoActivo != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
                    contenidoInversion.Save();
                    #endregion
                                        
                    detalle = contenidoDetalle.Save();


                    if (!detalle)
                    {
                        ManejadorLogSimpleBL.WriteLog("Error al actualizar el  detalle");
                    }

                    //Fin Actualizar Detalle  
                }
            }

            #endregion SetearDetalle

            return detalle;

        }

        public List<ListarDetalleSolicitudInversionDTO> BuscarDetallesInversionNuevos(string IdSolicitud)
        {
            List<ListarDetalleSolicitudInversionDTO> oLista = new List<ListarDetalleSolicitudInversionDTO>();
            ListarDetalleSolicitudInversionDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleSolicitudInversionNuevoAmpliacion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudInversion", IdSolicitud);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarDetalleSolicitudInversionDTO();
                        oSolicitudInversionBE.Cantidad = Convert.ToInt32(dataReader["Cantidad"]);
                        oSolicitudInversionBE.codOI = dataReader["codOI"].ToString();
                        oSolicitudInversionBE.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oSolicitudInversionBE.DescTipoActivo = dataReader["DescTipoActivo"].ToString();
                        oSolicitudInversionBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionBE.IdInversion = dataReader["IdInversion"].ToString();
                        oSolicitudInversionBE.IdMonedaCotizada = dataReader["IdMonedaCotizada"].ToString();
                        oSolicitudInversionBE.IdSolicitudInversion = dataReader["IdSolicitudInversion"].ToString();
                        oSolicitudInversionBE.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oSolicitudInversionBE.MontoAmpliarMonedaCotizada = Convert.ToDouble(dataReader["MontoAmpliarMonedaCotizada"]);
                        oSolicitudInversionBE.MontoAmpliarUSD = Convert.ToDouble(dataReader["MontoAmpliarUSD"]);
                        oSolicitudInversionBE.NombreMoneda = dataReader["NombreMoneda"].ToString();
                        oSolicitudInversionBE.PrecioMontoOrigen = Convert.ToDouble(dataReader["PrecioMontoOrigen"]);
                        oSolicitudInversionBE.PrecioMontoUSD = Convert.ToDouble(dataReader["PrecioMontoUSD"]);
                        oSolicitudInversionBE.PrecioUnitario = Convert.ToDouble(dataReader["PrecioUnitario"]);
                        oSolicitudInversionBE.PrecioUnitarioUSD = Convert.ToDouble(dataReader["PrecioUnitarioUSD"]);
                        oSolicitudInversionBE.VidaUtil = Convert.ToInt32(dataReader["VidaUtil"]);
                        oSolicitudInversionBE.VidaUtilAmpliar = Convert.ToInt32(dataReader["VidaUtilAmpliar"]);
                        oLista.Add(oSolicitudInversionBE);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return oLista;
        }

        public ListarSolicitudInversionDTO BuscarAmpliacionSolicitudInversion(string Id)
        {
            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCoordinadores = new ScriptorClient().GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(Id), "=").ToList().FirstOrDefault();
                         

            if (oInversion != null)
            {
                #region CargarCabecera

                string IdCeco = oInversion.Parts.IdCeCo.ToString().ToLower();

                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                oSolicitudInversion.FlagTipoBolsa = int.Parse(oInversion.Parts.FlagTipoBolsa.ToString());
                oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                oSolicitudInversion.MontoAprobadoPlanBase = oInversion.Parts.MontoAprobadoPlanBase;
                oSolicitudInversion.Id = oInversion.Id.ToString();

                if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                {
                    ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                    oSolicitudInversion.IdCeCo = contenidoCeCo.Id.ToString();
                    oSolicitudInversion.CodCeCo = contenidoCeCo.Parts.CodCECO;
                    oSolicitudInversion.DescCeCo = contenidoCeCo.Parts.CodCECO + " " + contenidoCeCo.Parts.DescCECO;
                }

                if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                {
                    oSolicitudInversion.IdAPIInicial = oInversion.Parts.IdAPIInicial.ToString();
                }

                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    ScriptorContent contenidoCoordinador = canalCoordinadores.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    oSolicitudInversion.IdCoordinador = contenidoCoordinador.Id.ToString();
                    oSolicitudInversion.DescCoordinador = contenidoCoordinador.Parts.Nombre;
                    oSolicitudInversion.CuentaCoordinador = contenidoCoordinador.Parts.CuentaRed;
                }
                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudInversion.IdEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id.ToString();
                    oSolicitudInversion.DescEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;

                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                {
                    oSolicitudInversion.IdMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id.ToString();
                    oSolicitudInversion.DescMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                {
                    oSolicitudInversion.IdSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id.ToString();
                    oSolicitudInversion.DescSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudInversion.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();
                    oSolicitudInversion.CodSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudInversion.DescSociedad = oSolicitudInversion.CodSociedad + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                {
                    oSolicitudInversion.IdTipoAPI = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();

                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                {
                    oSolicitudInversion.IdTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id.ToString();
                    oSolicitudInversion.DescTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;

                    if (oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ahorro.ToUpper() || oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ingreso.ToUpper())
                    {
                        oSolicitudInversion.FlagEvaluacionInversion = 1;
                    }
                    else
                    {
                        oSolicitudInversion.FlagEvaluacionInversion = 0;
                    }

                }

                oSolicitudInversion.Marca = oInversion.Parts.Marca;
                oSolicitudInversion.MontoTotal = oInversion.Parts.MontoTotal;
                oSolicitudInversion.NombreCortoSolicitud = oInversion.Parts.NombreCortoSolicitud;
                oSolicitudInversion.NombreProyecto = oInversion.Parts.NombreProyecto;
                oSolicitudInversion.NumSolicitud = oInversion.Parts.NumSolicitud;
                oSolicitudInversion.Observaciones = oInversion.Parts.Observaciones;
                oSolicitudInversion.ObservacionesFinancieras = oInversion.Parts.ObservacionesFinancieras;
                oSolicitudInversion.PRI = oInversion.Parts.Pri;
                if (oInversion.Parts.IdPeriodoPRI != null)
                {

                    if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRI.ToString()))
                    {
                        oSolicitudInversion.IdPeriodoPRI = 0;
                    }
                    oSolicitudInversion.IdPeriodoPRI = oInversion.Parts.IdPeriodoPRI;
                }
                oSolicitudInversion.Responsable = oInversion.Parts.Responsable;
                oSolicitudInversion.ResponsableNombre = String.IsNullOrEmpty(oSolicitudInversion.Responsable) ? "" : ObtenerNombreUsuario(oSolicitudInversion.Responsable);

                oSolicitudInversion.ResponsableProyecto = oInversion.Parts.ResponsableProyecto;
                oSolicitudInversion.ResponsableProyectoNombre = String.IsNullOrEmpty(oSolicitudInversion.ResponsableProyecto) ? "" : ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);

                oSolicitudInversion.TIR = oInversion.Parts.Tir;
                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;

                oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;

                oSolicitudInversion.VAN = oInversion.Parts.Van;

                RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();

                oSolicitudInversion.IdMonedaSociedad = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).IdMoneda;
                oSolicitudInversion.DescMoneda = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).Moneda;
                
                oSolicitudInversion.VANAmpliacion = oInversion.Parts.VanAmpliacion;
                oSolicitudInversion.TIRAmpliacion = oInversion.Parts.TirAmpliacion;
                oSolicitudInversion.PRIAmpliacion = oInversion.Parts.PriAmpliacion;
                oSolicitudInversion.ObservacionesFinancierasAmpliacion = oInversion.Parts.ObservacionesFinancierasAmpliacion;
                oSolicitudInversion.MontoAAmpliar = oInversion.Parts.MontoAAmpliar;
                if (oInversion.Parts.IdPeriodoPRIAmpliacion != null)
                {
                    if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRIAmpliacion.ToString()))
                    {
                        oSolicitudInversion.IdPeriodoPRIAmpliacion = 0;
                    }
                    oSolicitudInversion.IdPeriodoPRIAmpliacion = oInversion.Parts.IdPeriodoPRIAmpliacion;
                }
                #endregion
              
                return oSolicitudInversion;
            }
            
            return oSolicitudInversion;

        }

        public DataTable BuscarSolicitudInversionParaExportar(string Id)
        {
            ListarSolicitudInversionExportarDTO oSolicitudInversion = new ListarSolicitudInversionExportarDTO();

            ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(Id), "=").ToList().FirstOrDefault();
            DataTable TableSolicitudInversion = new DataTable();

            if (oInversion != null)
            {
                #region CargarCabecera

                TableSolicitudInversion.Columns.Add("NumSolicitud");
                TableSolicitudInversion.Columns.Add("FechaCreacion");
                TableSolicitudInversion.Columns.Add("NombreCortoSolicitud");
                TableSolicitudInversion.Columns.Add("FechaInicio");
                TableSolicitudInversion.Columns.Add("FechaCierre");
                TableSolicitudInversion.Columns.Add("Ubicacion");
                TableSolicitudInversion.Columns.Add("Marca");
                TableSolicitudInversion.Columns.Add("Descripcion");
                TableSolicitudInversion.Columns.Add("Observaciones");
                TableSolicitudInversion.Columns.Add("Responsable");
                TableSolicitudInversion.Columns.Add("ResponsableNombre");
                TableSolicitudInversion.Columns.Add("VAN");
                TableSolicitudInversion.Columns.Add("TIR");
                TableSolicitudInversion.Columns.Add("PRI");
                TableSolicitudInversion.Columns.Add("IdPeriodoPRI");
                TableSolicitudInversion.Columns.Add("ObservacionesFinancieras");
                TableSolicitudInversion.Columns.Add("VANAmpliacion");
                TableSolicitudInversion.Columns.Add("TIRAmpliacion");
                TableSolicitudInversion.Columns.Add("PRIAmpliacion");
                TableSolicitudInversion.Columns.Add("ObservacionesFinancierasAmpliacion");
                TableSolicitudInversion.Columns.Add("IdPeriodoPRIAmpliacion");
                TableSolicitudInversion.Columns.Add("CodigoProyecto");
                TableSolicitudInversion.Columns.Add("NombreProyecto");
                TableSolicitudInversion.Columns.Add("ResponsableProyecto");
                TableSolicitudInversion.Columns.Add("ResponsableProyectoNombre");
                TableSolicitudInversion.Columns.Add("MontoAAmpliar");
                TableSolicitudInversion.Columns.Add("FlagPlanBase");
                TableSolicitudInversion.Columns.Add("FechaModificacion");
                TableSolicitudInversion.Columns.Add("UsuarioCreador");
                TableSolicitudInversion.Columns.Add("UsuarioModifico");
                TableSolicitudInversion.Columns.Add("Activo");
                TableSolicitudInversion.Columns.Add("MontoTotal");
                TableSolicitudInversion.Columns.Add("DescTipoCapex");
                TableSolicitudInversion.Columns.Add("DescEstado");
                TableSolicitudInversion.Columns.Add("CodCeCo");
                TableSolicitudInversion.Columns.Add("DescCeCo");
                TableSolicitudInversion.Columns.Add("DescCoordinador");
                TableSolicitudInversion.Columns.Add("CuentaCoordinador");
                TableSolicitudInversion.Columns.Add("CodSociedad");
                TableSolicitudInversion.Columns.Add("DescSociedad");
                TableSolicitudInversion.Columns.Add("DescMacroServicio");
                TableSolicitudInversion.Columns.Add("DescSector");
                TableSolicitudInversion.Columns.Add("DescMoneda");
                TableSolicitudInversion.Columns.Add("FlagTipoBolsa");
                TableSolicitudInversion.Columns.Add("MontoAprobadoPlanBase");

                DataRow row = TableSolicitudInversion.NewRow();
                
                string IdCeco = oInversion.Parts.IdCeCo.ToString().ToLower();

                row["Activo"] = "";
                row["CodigoProyecto"] = oInversion.Parts.CodigoProyecto;
                row["Descripcion"] = oInversion.Parts.Descripcion;
                row["FechaCierre"] = Convert.ToDateTime(oInversion.Parts.FechaCierre) == DateTime.MinValue ? "" : Convert.ToDateTime(oInversion.Parts.FechaCierre).ToShortDateString();
                row["FechaCreacion"] = String.Format("{0:g}", Convert.ToDateTime(oInversion.Parts.FechaCreacion));
                row["FechaInicio"] = Convert.ToDateTime(oInversion.Parts.FechaInicio) == DateTime.MinValue ? "" : Convert.ToDateTime(oInversion.Parts.FechaInicio).ToShortDateString();
                row["FechaModificacion"] = String.Format("{0:g}",Convert.ToDateTime(oInversion.Parts.FechaModificacion));
                row["FlagPlanBase"] = oInversion.Parts.FlagPlanBase.ToString();

                if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                {
                    ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                    row["CodCeCo"] = contenidoCeCo.Parts.CodCECO;
                    row["DescCeCo"] = row["CodCeCo"] + " - " + contenidoCeCo.Parts.DescCECO;
                }
                
                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    row["DescCoordinador"] = contenidoCoordinador.Parts.Nombre;
                    row["CuentaCoordinador"] = contenidoCoordinador.Parts.CuentaRed;
                }                               
                
                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    row["DescEstado"] = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                {
                    row["DescMacroServicio"] = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                {
                    row["DescSector"] = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    row["CodSociedad"] = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    row["DescSociedad"] = row["CodSociedad"] + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }
                
                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                {
                    row["DescTipoCapex"] = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;
                }

                row["Marca"] = oInversion.Parts.Marca;
                row["MontoTotal"] = oInversion.Parts.MontoTotal.ToString();
                row["NombreCortoSolicitud"] = oInversion.Parts.NombreCortoSolicitud;
                row["NombreProyecto"] = oInversion.Parts.NombreProyecto;
                row["NumSolicitud"] = oInversion.Parts.NumSolicitud;
                row["Observaciones"] = oInversion.Parts.Observaciones;
                row["ObservacionesFinancieras"] = oInversion.Parts.ObservacionesFinancieras;
                row["PRI"] = oInversion.Parts.Pri.ToString();
                if (oInversion.Parts.IdPeriodoPRI != null)
                {

                    if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRI.ToString()))
                    {
                        row["IdPeriodoPRI"] = "0";
                    }
                    if (oInversion.Parts.IdPeriodoPRI.ToString() == "0" && oInversion.Parts.Pri.ToString() != "0" )
                        row["IdPeriodoPRI"] = "Años";
                    else if (oInversion.Parts.IdPeriodoPRI.ToString() == "1" && oInversion.Parts.Pri.ToString() != "0")
                        row["IdPeriodoPRI"] = "Meses";
                    else
                        row["IdPeriodoPRI"] = "";
                }
                row["Responsable"] = oInversion.Parts.Responsable;
                row["ResponsableNombre"] = String.IsNullOrEmpty(oInversion.Parts.Responsable) ? "" : ObtenerNombreUsuario(row["Responsable"].ToString());

                //row["ResponsableProyecto"] = oInversion.Parts.ResponsableProyecto;
                row["ResponsableProyecto"] = String.IsNullOrEmpty(oInversion.Parts.ResponsableProyecto) ? "" : ObtenerNombreUsuario(oInversion.Parts.ResponsableProyecto);

                row["TIR"] = oInversion.Parts.Tir.ToString();
                row["Ubicacion"] = oInversion.Parts.Ubicacion;

                row["UsuarioCreador"] = oInversion.Parts.UsuarioCreador;

                row["UsuarioModifico"] = oInversion.Parts.UsuarioModifico;

                row["VAN"] = oInversion.Parts.Van.ToString();

                RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();
                row["DescMoneda"] = MonedaSociedad.ObtenerTipoCambioPorSociedad(oInversion.Parts.IdSociedad.Value.ToString()).Moneda;

                row["VANAmpliacion"] = oInversion.Parts.VanAmpliacion.ToString();
                row["TIRAmpliacion"] = oInversion.Parts.TirAmpliacion.ToString();
                row["PRIAmpliacion"] = oInversion.Parts.PriAmpliacion.ToString();
                row["ObservacionesFinancierasAmpliacion"] = oInversion.Parts.ObservacionesFinancierasAmpliacion;
                row["MontoAAmpliar"] = oInversion.Parts.MontoAAmpliar.ToString();
                row["FlagTipoBolsa"] = oInversion.Parts.FlagTipoBolsa.ToString();
                row["MontoAprobadoPlanBase"] = oInversion.Parts.MontoAprobadoPlanBase.ToString();
                
                if (oInversion.Parts.IdPeriodoPRIAmpliacion != null)
                {
                    if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRIAmpliacion.ToString()))
                    {
                        row["IdPeriodoPRIAmpliacion"] = "0";
                    }

                    if (oInversion.Parts.IdPeriodoPRIAmpliacion.ToString() == "0" && oInversion.Parts.PriAmpliacion.ToString() != "0")
                        row["IdPeriodoPRIAmpliacion"] = "Años";
                    else if (oInversion.Parts.IdPeriodoPRIAmpliacion.ToString() == "1" && oInversion.Parts.PriAmpliacion.ToString() != "0")
                        row["IdPeriodoPRIAmpliacion"] = "Meses";
                    else
                        row["IdPeriodoPRIAmpliacion"] = "";
                    
                }
                TableSolicitudInversion.Rows.Add(row);
                #endregion

                return TableSolicitudInversion;
            }

            return TableSolicitudInversion;

        }

        public List<ListarDetalleSolicitudInversionDTO> BuscarDetalleAmpliacionSolicitudInversion(string Id)
        {
            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(Id), "=").ToList().FirstOrDefault();

            if (oInversion != null)
            {

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();
                oDetalleInversion = oDetalleInversion.Where(x => x.Parts.IdSolicitudInversion.Value.ToString().ToLower() == oInversion.Id.ToString().ToLower()).ToList();

                List<ListarDetalleSolicitudInversionDTO> oListaDetalle = new List<ListarDetalleSolicitudInversionDTO>();
                ListarDetalleSolicitudInversionDTO oDetalle;


                foreach (ScriptorContent item in oDetalleInversion)
                {

                    oDetalle = new ListarDetalleSolicitudInversionDTO();

                    oDetalle.Id = item.Id.ToString();
                    oDetalle.Cantidad = item.Parts.Cantidad;
                    oDetalle.IdInversion = item.Parts.IdInversion.Value;
                    oDetalle.IdSolicitudInversion = item.Parts.IdSolicitudInversion.Value;

                    if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                    {
                        oDetalle.codOI = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.CodigoOI;
                        oDetalle.DescripcionOI = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.DescripcionOI;

                    }
                    if (((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content != null)
                    {
                        oDetalle.NombreMoneda = ((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content.Parts.Nombre;

                    }

                    oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada.Value;

                    if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                    {
                        oDetalle.IdTipoActivo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id.ToString();
                        oDetalle.DescTipoActivo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                    }

                    oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                    oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                    oDetalle.PrecioMontoOrigen = item.Parts.PrecioMontoOrigen;
                    oDetalle.PrecioMontoUSD = item.Parts.PrecioMontoUSD;
                    oDetalle.VidaUtil = item.Parts.VidaUtil;
                    oDetalle.VidaUtilAmpliar = item.Parts.VidaUtilAmpliar;
                    oDetalle.MontoAmpliarMonedaCotizada = item.Parts.MontoAAmpliarMonedaCotizada;
                    oDetalle.MontoAmpliarUSD = item.Parts.MontoAmpliarUSD;

                    oListaDetalle.Add(oDetalle);

                }
                return oListaDetalle;

            }

            return null;
            

        }

        public DataTable BuscarDetalleSolicitudInversionExportar(string Id)
        {
            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(Id), "=").ToList().FirstOrDefault();
            DataTable TableDetalleSolicitudInversion = new DataTable();

            if (oInversion != null)
            {
                TableDetalleSolicitudInversion.Columns.Add("Cantidad");
                TableDetalleSolicitudInversion.Columns.Add("PrecioUnitario");
                TableDetalleSolicitudInversion.Columns.Add("PrecioUnitarioUSD");
                TableDetalleSolicitudInversion.Columns.Add("IdInversion");
                TableDetalleSolicitudInversion.Columns.Add("VidaUtil");
                TableDetalleSolicitudInversion.Columns.Add("DescTipoActivo");
                TableDetalleSolicitudInversion.Columns.Add("NombreMoneda");
                TableDetalleSolicitudInversion.Columns.Add("PrecioMontoOrigen");
                TableDetalleSolicitudInversion.Columns.Add("PrecioMontoUSD");
                TableDetalleSolicitudInversion.Columns.Add("codOI");
                TableDetalleSolicitudInversion.Columns.Add("DescripcionOI");
                TableDetalleSolicitudInversion.Columns.Add("MontoAmpliarMonedaCotizada");
                TableDetalleSolicitudInversion.Columns.Add("MontoAmpliarUSD");
                TableDetalleSolicitudInversion.Columns.Add("VidaUtilAmpliar");

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();
                oDetalleInversion = oDetalleInversion.Where(x => x.Parts.IdSolicitudInversion.Value.ToString().ToLower() == oInversion.Id.ToString().ToLower()).ToList();
                
                foreach (ScriptorContent item in oDetalleInversion)
                {
                    DataRow row = TableDetalleSolicitudInversion.NewRow();
                    
                    row["Cantidad"] = item.Parts.Cantidad.ToString();
                    row["IdInversion"] = item.Parts.IdInversion.Value.ToString();
                  
                    if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                    {
                        row["codOI"] = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.CodigoOI;
                        row["DescripcionOI"] = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.DescripcionOI;

                    }
                    if (((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content != null)
                    {
                        row["NombreMoneda"] = ((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content.Parts.Nombre;

                    }

                    if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                    {
                        row["DescTipoActivo"] = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                    }

                    row["PrecioUnitario"] = item.Parts.PrecioUnitario.ToString();
                    row["PrecioUnitarioUSD"] = item.Parts.PrecioUnitarioUSD.ToString();
                    row["PrecioMontoOrigen"] = item.Parts.PrecioMontoOrigen.ToString();
                    row["PrecioMontoUSD"] = item.Parts.PrecioMontoUSD.ToString();
                    row["VidaUtil"] = item.Parts.VidaUtil.ToString();
                    row["VidaUtilAmpliar"] = item.Parts.VidaUtilAmpliar.ToString();
                    row["MontoAmpliarMonedaCotizada"] = item.Parts.MontoAAmpliarMonedaCotizada.ToString();
                    row["MontoAmpliarUSD"] = item.Parts.MontoAmpliarUSD.ToString();

                    TableDetalleSolicitudInversion.Rows.Add(row);

                }
                return TableDetalleSolicitudInversion;

            }

            return null;


        }
        public RResultadoAmpliacionAPIBE ModificarAmpliacionSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {
            string track = "";
            RSolicitudInversionDAL oSolicitudInversionDAL = new RSolicitudInversionDAL();
            RResultadoAmpliacionAPIBE oRespuesta = new RResultadoAmpliacionAPIBE();

            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            /*ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));*/
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            /*RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RMacroservicioDAL om = new RMacroservicioDAL();
            RSectorDAL os = new RSectorDAL();*/

            if (oSolicitudInversion == null)
            {
                oRespuesta.success = false;

                return oRespuesta;
            }

            List<string> resultadoDetalle = new List<string>();
            bool resultadoCabecera = false;

            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalAdjunto = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Adjuntos));

            ScriptorContent contenidoCabecera = canalCabecera.QueryContents("NumSolicitud", oSolicitudInversion.NumSolicitud, "=").ToList().FirstOrDefault();

            try
            {

                #region SetearCabecera

                
                contenidoCabecera.Parts.Descripcion = oSolicitudInversion.Descripcion != null ? oSolicitudInversion.Descripcion : "";
                contenidoCabecera.Parts.Observaciones = oSolicitudInversion.Observaciones != null ? oSolicitudInversion.Observaciones : "";
                contenidoCabecera.Parts.VanAmpliacion = oSolicitudInversion.VANAmpliacion;
                contenidoCabecera.Parts.TirAmpliacion = oSolicitudInversion.TIRAmpliacion;
                contenidoCabecera.Parts.PriAmpliacion = oSolicitudInversion.PRIAmpliacion;
                contenidoCabecera.Parts.ObservacionesFinancierasAmpliacion = oSolicitudInversion.ObservacionesFinancierasAmpliacion != null ? oSolicitudInversion.ObservacionesFinancierasAmpliacion : "";
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico != null ? oSolicitudInversion.UsuarioModifico.CuentaUsuario : "";
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                
                
                if (oSolicitudInversion.FlagPlanBase == 1)
                {
                    contenidoCabecera.Parts.NombreCortoSolicitud = oSolicitudInversion.NombreCortoSolicitud;
                    contenidoCabecera.Parts.Responsable = oSolicitudInversion.Responsable.CuentaUsuario;
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;
                    contenidoCabecera.Parts.Marca = oSolicitudInversion.Marca;
                    contenidoCabecera.Parts.Ubicacion = oSolicitudInversion.Ubicacion;
                    
                }

                if (oSolicitudInversion.IdPeriodoPRIAmpliacion != null)
                    contenidoCabecera.Parts.IdPeriodoPRIAmpliacion = oSolicitudInversion.IdPeriodoPRIAmpliacion.codigo;

                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());

                #endregion SetearCabecera

                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera                
                if (oRespuesta.success)
                {
                    oRespuesta.Id = contenidoCabecera.Id.ToString();
                    oRespuesta.NumSolicitud = oSolicitudInversion.NumSolicitud;
                    if (oSolicitudInversion.FlagPlanBase == 1)
                    {
                        oSolicitudInversionDAL.ActualizarFecha(contenidoCabecera.Id.ToString(), oSolicitudInversion.FechaInicio, oSolicitudInversion.FechaCierre);
                    }

                    if (contenidoCabecera.Parts.IdEstado != null)
                    {
                        oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();
                        oRespuesta.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    }

                    oRespuesta.FechaCreacion = contenidoCabecera.Parts.FechaCreacion;

                    if (contenidoCabecera.Parts.IdCoordinador != null && contenidoCabecera.Parts.IdCoordinador!= "")
                    {
                        oRespuesta.CoordinadorNombre = canalCoordinador.GetContent(new Guid(contenidoCabecera.Parts.IdCoordinador.ToString())).Parts.Nombre;
                    }
                }

                track = "Obtuvo Id de cabecera guardada";

            }
            catch (Exception ex)
            {
                return oRespuesta;
            }

            return oRespuesta;

        }

        public bool ActualizarInversionEstado(RSolicitudInversionBE oSolicitudInversion)
        {
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalEstadosMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalTipoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoMovimiento));

            bool resultado = false;

            foreach (RDetalleSolicitudInversionBE det in oSolicitudInversion.DetalleSolicitudInversion)
            {
                //Actualizamos registro de inversion
                ScriptorContent inv = canalInversion.QueryContents("#Id", det.IdInversion.Id, "=").ToList().FirstOrDefault();
                inv.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Pendiente, "=").ToList().FirstOrDefault());

                resultado = inv.Save();

                //Registramos un movimiento

                ScriptorContent contenido = canalMovimiento.NewContent();

                contenido.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Pendiente, "=").ToList().FirstOrDefault());
                contenido.Parts.IdTipoMovimiento = ScriptorDropdownListValue.FromContent(canalTipoMovimiento.QueryContents("#Id", TiposMovimiento.API, "=").ToList().FirstOrDefault());
                contenido.Parts.Monto = det.FlagAmpliacion == 1 ? det.PrecioMontoUSD : det.MontoAAmpliarUSD;
                contenido.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                contenido.Parts.Descripcion = "";
                contenido.Parts.FechaMovimiento = DateTime.Now;
                contenido.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(canalSolicitudInversion.QueryContents("#Id", oSolicitudInversion.Id, "=").ToList().FirstOrDefault());
                //contenido.Parts.IdSolicitudInversionTraslado = "";

                resultado = contenido.Save();

            }
            return resultado;
            //ScriptorContent oInversion = canalInversion.QueryContents("#Id", oSolicitudInversion.Id, "=").ToList().FirstOrDefault();


        }
        public bool ModificarDetalleSolicitudInversion(RDetalleSolicitudInversionBE det)
        {

            bool detalle = false;
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));

            #region SetearDetalle

            if (det.IdDetalle != null)
            {
                ScriptorContent contenidoDetalle = canalDetalle.QueryContents("#Id", det.IdDetalle, "=").ToList().FirstOrDefault();
                //contenidoDetalle = oDetalles.Where(x => x.Id == det.IdDetalle).ToList().FirstOrDefault();
                //Actualizo
                if (contenidoDetalle != null)
                {
                    contenidoDetalle.Parts.MontoAAmpliarMonedaCotizada = det.MontoAAmpliarMonedaCotizada;
                    contenidoDetalle.Parts.VidaUtilAmpliar = det.VidaUtilAmpliar;
                    contenidoDetalle.Parts.MontoAmpliarUSD = det.MontoAAmpliarUSD;

                    #region ModificarInversion
                    ScriptorContent contenidoInversion = canalInversion.QueryContents("#Id", ((ScriptorDropdownListValue)contenidoDetalle.Parts.IdInversion).Content.Id, "=").ToList().FirstOrDefault();
                    contenidoInversion.Parts.MontoDisponible = det.MontoAAmpliarUSD;
                    contenidoInversion.Parts.MontoContable = det.MontoAAmpliarUSD;
                    contenidoInversion.Save();                   
                    #endregion
                    detalle = contenidoDetalle.Save();


                    if (!detalle)
                    {
                        ManejadorLogSimpleBL.WriteLog("Error al actualizar el  detalle");
                    }

                    //Fin Actualizar Detalle  
                }
            }

            #endregion SetearDetalle

            return detalle;

        }
        public string GenerarCorrelativo(string IdSociedad, string IdTipoAPI, string FechaInicio)
        {
            string Correlativo = "";
            string track = "";
            string num = "";

            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorContent sociedad = canalSociedad.QueryContents("#Id", IdSociedad, "=").ToList().FirstOrDefault();
            track += "trajo la sociedad";

            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorContent tipoAPI = canalTipoAPI.QueryContents("#Id", IdTipoAPI, "=").ToList().FirstOrDefault();
            track += "trajo el tipo de api";

            Correlativo += sociedad.Parts.Codigo;
            Correlativo += "-";
            Correlativo += tipoAPI.Parts.Codigo;
            Correlativo += "-";
            Correlativo += DateTime.Parse(FechaInicio).ToString("yy") + DateTime.Parse(FechaInicio).Month.ToString().PadLeft(2,'0');
            Correlativo += "-";
            if (tipoAPI.Parts.Correlativo != 0)
            {
                num = Convert.ToString(tipoAPI.Parts.Correlativo + 1);//.ToString();
                Correlativo += num.PadLeft(4, '0');
            }
            else
                Correlativo += "0001";
            track += "concatenó sociedad tipo api y fecha";

            return Correlativo;


        }

        private string ObtenerNombreUsuario(string CuentaUsuario)
        {

            string dominioGrupoCogesa = "GRUPOCOGESA";
            //string dominioGrupoAlicorp = "GRUPOALICORP";
            string dominioGrupoRansa = "GRUPORANSA";
            // string dominioAlicorp = "ALICORP";

            string UsuarioADGrupoAlicorp = ConfigurationManager.AppSettings["UsuarioAD"];
            string PasswordADGrupoAlicorp = ConfigurationManager.AppSettings["PasswordAD"];

            string UsuarioADGrupoCogesa = ConfigurationManager.AppSettings["UsuarioGrupoCogesa"];
            string PasswordADGrupoCogesa = ConfigurationManager.AppSettings["PasswordGrupoCogesa"];

            string UsuarioADAlicorp = ConfigurationManager.AppSettings["UsuarioAlicorp"];
            string PasswordADAlicorp = ConfigurationManager.AppSettings["PasswordAlicorp"];


            string UsuarioADGrupoRansa = ConfigurationManager.AppSettings["UsuarioADRansa"];
            string PasswordADGrupoRansa = ConfigurationManager.AppSettings["PasswordADRansa"];

            string Nombres = "";

            List<Dominio> dominios = new List<Dominio>();
            dominios.Add(new Dominio() { NombreDominio = dominioGrupoRansa, UsuarioDominio = UsuarioADGrupoRansa, PasswordUsuarioDominio = PasswordADGrupoRansa });
            //  dominios.Add(new Dominio() { NombreDominio = dominioAlicorp, UsuarioDominio = UsuarioADAlicorp, PasswordUsuarioDominio = PasswordADAlicorp });
            dominios.Add(new Dominio() { NombreDominio = dominioGrupoCogesa, UsuarioDominio = UsuarioADGrupoCogesa, PasswordUsuarioDominio = PasswordADGrupoCogesa });


            List<UsuarioAD> usuarios = new List<UsuarioAD>();
            foreach (Dominio dominio in dominios)
            {
                using (var pctx = new PrincipalContext(ContextType.Domain, dominio.NombreDominio, dominio.UsuarioDominio, dominio.PasswordUsuarioDominio))
                {
                    UserPrincipal up = UserPrincipal.FindByIdentity(pctx, CuentaUsuario);

                    if (up != null)
                    {
                        Nombres = !String.IsNullOrEmpty(up.DisplayName) ? up.DisplayName : string.Empty;
                        break;
                    }

                }
            }
            try
            {

            }
            catch
            {/*
                UsuarioAD usuario = new UsuarioAD();
                usuario.id = ex.Message;
                usuario.name = ex.ToString();
                usuarios.Add(usuario);*/

                throw;
            }


            return Nombres;
        }
    }
}
