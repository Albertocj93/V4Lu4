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
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;

namespace DataLayer
{
    public class RSolicitudInversionDAL
    {
  /*      public string InsertarSolicitudInversionAdjuntos(RSolicitudInversionBE oSolicitudInversion,List<ArchivoAdjuntoBE> Adjuntos)
        {
            string track = "";

            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            

            if (oSolicitudInversion.NumSolicitud == null)
                return "No se recibieron datos";


            List<string> resultadoDetalle = new List<string>();
            bool resultadoCabecera = false;
            
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalAdjunto = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Adjuntos));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            ScriptorContent contenidoCabecera = canalCabecera.NewContent();
            ScriptorContent contenidoDetalle;
            ScriptorContent contenidoAdjunto;
            ScriptorContent contenidoInversion;
            
            try
            {

                #region SetearCabecera
                //Para nuevo API
                oSolicitudInversion.IdTipoAPI.Id = TiposAPI.NuevoProyecto;
                oSolicitudInversion.FechaCreacion = DateTime.Now;

                string Correlativo = GenerarCorrelativo(oSolicitudInversion.IdSociedad.Id.ToString(), oSolicitudInversion.IdTipoAPI.Id.ToString(), oSolicitudInversion.FechaCreacion.ToShortDateString());
                track += "genero el correlativo " + Correlativo;
                contenidoCabecera.Parts.NumSolicitud = Correlativo;
                contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.NombreCortoSolicitud = oSolicitudInversion.NombreCortoSolicitud;
                contenidoCabecera.Parts.FechaInicio = oSolicitudInversion.FechaInicio;
                contenidoCabecera.Parts.FechaCierre = oSolicitudInversion.FechaCierre;
                contenidoCabecera.Parts.Ubicacion = oSolicitudInversion.Ubicacion;
                contenidoCabecera.Parts.Marca = oSolicitudInversion.Marca;
                contenidoCabecera.Parts.Descripcion = oSolicitudInversion.Descripcion;
                contenidoCabecera.Parts.Observaciones = oSolicitudInversion.Observaciones;
                contenidoCabecera.Parts.Responsable = oSolicitudInversion.Responsable;
                contenidoCabecera.Parts.Van = oSolicitudInversion.VAN;
                contenidoCabecera.Parts.Tir = oSolicitudInversion.TIR;
                contenidoCabecera.Parts.Pri = oSolicitudInversion.PRI;
                contenidoCabecera.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras;
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto;
                contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto;
                contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioCreador = "";
                contenidoCabecera.Parts.UsuarioModifico = "";
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                //contenidoCabecera.Parts.IdAPIInicial = oSolicitudInversion.IdAPIInicial;
                contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdCoordinador = oSolicitudInversion.IdCoordinador;
                contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
                contenidoCabecera.Parts.IdMacroServicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oSolicitudInversion.IdMacroServicio, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSolicitudInversion.IdSector, "=").ToList().FirstOrDefault());

                #endregion SetearCabecera


                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                resultadoCabecera = contenidoCabecera.Save();

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera
                Guid? id = null;
                if (resultadoCabecera)
                    id = contenidoCabecera.Id;

                track = "Obtuvo Id de cabecera guardada";

                if (!String.IsNullOrEmpty(id.ToString()))
                {
                    #region SetearDetalle
                    foreach (RDetalleSolicitudInversionBE det in oSolicitudInversion.DetalleSolicitudInversion)
                    {
                        contenidoDetalle = canalDetalle.NewContent();

                        contenidoDetalle.Parts.Cantidad = det.Cantidad;
                        contenidoDetalle.Parts.PrecioUnitario = det.PrecioUnitario;
                        contenidoDetalle.Parts.PrecioUnitarioUSD = det.PrecioUnitarioUSD;
                        contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalTipoCambio.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());
                        contenidoDetalle.Parts.IdMoneda = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMoneda, "=").ToList().FirstOrDefault());
                        contenidoDetalle.Parts.IdInversion = det.IdInversion;
                        contenidoDetalle.Parts.VidaUtil = det.VidaUtil;
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo, "=").ToList().FirstOrDefault());
                        contenidoDetalle.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad, "=").ToList().FirstOrDefault());
                        contenidoDetalle.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(contenidoCabecera);


                        track = "Asigno datos de un detalle";
                        resultadoDetalle.Add(contenidoDetalle.Save().ToString() + " -- " + contenidoDetalle.LastError.ToString());

                        track = "Guardo datos de un detalle";

                        //Setear Inversion en blanco

                        contenidoInversion = canalInversion.NewContent();
                        contenidoInversion.Parts.IdCeCo = contenidoCabecera.Parts.IdCeCo;
                        contenidoInversion.Parts.IdTipoActivo = contenidoDetalle.Parts.IdTipoActivo;

                        contenidoInversion.Save();

                        //Actualizar Detalle con Inversion

                        contenidoDetalle.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contenidoInversion);
                        contenidoDetalle.Save();
                        
                    }
                    #endregion SetearDetalle

                    #region SetearAdjuntos
                    foreach (ArchivoAdjuntoBE adjunto in Adjuntos)
                    {
                        contenidoAdjunto = canalAdjunto.NewContent();

                        contenidoAdjunto.Parts.NombreArchivo = adjunto.NombreArchivo;
                        ScriptorFile oFile = new ScriptorFile(adjunto.ArchivoFisico,adjunto.NombreArchivo);
                        contenidoAdjunto.Parts.ArchivoFisico = oFile;

                        contenidoAdjunto.Parts.TamanoArchivo = adjunto.TamanioArchivo;
                        contenidoAdjunto.Parts.ContentType = adjunto.ContentType;
                        contenidoAdjunto.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(contenidoCabecera);
                        contenidoAdjunto.Save();
                    }
                    

                    #endregion SetearAdjuntos

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "cabecera =>" + resultadoCabecera + " detalle =>" + Common.WebUtil.ToJson(resultadoDetalle);
            
 
        }
*/

        public RResultadoNuevoProyectoBE InsertarSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {            
            string track = "";
            string Correlativo = "";
            RResultadoNuevoProyectoBE oRespuesta = new RResultadoNuevoProyectoBE();
           
            //ScriptorClient clie = new ScriptorClient();
            //clie.DisableCache();

            ManejadorLogSimpleBL.WriteLog("ENTRO 1");
            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));            
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            RMacroservicioDAL om = new RMacroservicioDAL();
            RSectorDAL os = new RSectorDAL();
            RCoordinadorBE oCoordinador = new RCoordinadorBE();            
            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            
            ManejadorLogSimpleBL.WriteLog("ENTRO 2" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
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
                //Para nuevo API
                oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                oSolicitudInversion.IdTipoAPI.Id = TiposAPI.IdNuevoProyecto;
                oSolicitudInversion.FechaCreacion = DateTime.Now;
                ManejadorLogSimpleBL.WriteLog("ENTRO 3" +" "+DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);                
                if(oSolicitudInversion.IdSociedad != null)
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
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto != null ? oSolicitudInversion.NombreProyecto : "";

                if (oSolicitudInversion.ResponsableProyecto != null)
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;

                contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioCreador = oSolicitudInversion.UsuarioCreador.CuentaUsuario;
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario;
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
                //contenidoCabecera.Parts.IdAPIInicial = oSolicitudInversion.IdAPIInicial;
                ManejadorLogSimpleBL.WriteLog("ENTRO 4" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);


                //if (oSolicitudInversion.IdCoordinador != null)
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudInversion.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());

                if (oSolicitudInversion.IdCoordinador != null)
                {
                    oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                    //ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                    //contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                    contenidoCabecera.Parts.IdCoordinador = oCoordinador.Id.Value.ToString();
                }
                ManejadorLogSimpleBL.WriteLog("ENTRO 5" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);

                //ScriptorContent prueba = canalTipoCapex.GetContent(new Guid("6F9CFD9E-B799-4441-8969-35E87F6A9FAC"));

                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 6" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());
               
                if (oSolicitudInversion.IdSociedad != null)
                    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 7" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
                if (oSolicitudInversion.IdTipoAPI != null)
                    contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault());

                ManejadorLogSimpleBL.WriteLog("ENTRO 8" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
                if (oSolicitudInversion.IdCeCo != null)
                {
                    RMacroservicioBE oMacroServicioBE = new RMacroservicioBE();
                    oMacroServicioBE = om.ObtenerMacroservicioPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if(oMacroServicioBE.Id != null)
                        contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oMacroServicioBE.Id, "=").ToList().FirstOrDefault());

                    RSectorBE oSectorBE = new RSectorBE();
                    oSectorBE = os.ObtenerSectorPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if(oSectorBE.Id != null)
                        contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSectorBE.Id, "=").ToList().FirstOrDefault());

                    //contenidoCabecera.Parts.IdCeCo = ScriptorDropdownListValue.FromContent(canalCeCo.QueryContents("#Id", oSolicitudInversion.IdCeCo.Id, "=").ToList().FirstOrDefault());
                    contenidoCabecera.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.Value.ToString();
                }
                
                if (oSolicitudInversion.IdPeriodoPRI != null)
                    contenidoCabecera.Parts.IdPeriodoPRI = oSolicitudInversion.IdPeriodoPRI.codigo;
                #endregion SetearCabecera


                track = "Asigno variables a cabecera";
                ManejadorLogSimpleBL.WriteLog("ENTRO 9" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();
                ManejadorLogSimpleBL.WriteLog("ENTRO 10" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
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
                    oRespuesta.FechaCreacion = oSolicitudInversion.FechaCreacion;
                    oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();
                    

                    track = "Obtuvo Id de cabecera guardada";
                    
                    if (!String.IsNullOrEmpty(id.ToString()))
                    {
                        ManejadorLogSimpleBL.WriteLog("ENTRO 11" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
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
                        ContenidoTipoAPI.Save("Grabar");
                        ManejadorLogSimpleBL.WriteLog("FIN" + " " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond + Environment.NewLine);
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
                //oRespuesta.Mensaje = ex.Message;
                return oRespuesta;
            }
            
            return oRespuesta;
            //return "cabecera =>" + resultadoCabecera + " detalle =>" + Common.WebUtil.ToJson(resultadoDetalle);


        }

        public RResultadoNuevoProyectoBE InsertarSolicitudInversionParaTraslado(RSolicitudInversionBE oSolicitudInversion, string IdSolicitudTraslado)
        {
            string track = "";
            string Correlativo = "";
            RResultadoNuevoProyectoBE oRespuesta = new RResultadoNuevoProyectoBE();

            //ScriptorClient clie = new ScriptorClient();
            //clie.DisableCache();

            ManejadorLogSimpleBL.WriteLog("ENTRO 1");
            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            ScriptorChannel canalTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCabeceraDestinoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorChannel canalDetalleDestinoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));

            //Obtenemos el traslado para usar sus datos al guardar cabecera destino,etc
            RSolicitudTrasladoDAL oSolicitudTrasladoDAL = new RSolicitudTrasladoDAL();
            RSolicitudTrasladoBE oSolicitudTrasladoBE = new RSolicitudTrasladoBE();
            oSolicitudTrasladoBE = oSolicitudTrasladoDAL.BuscarPorId(IdSolicitudTraslado);

            RMacroservicioDAL om = new RMacroservicioDAL();
            RSectorDAL os = new RSectorDAL();
            RCoordinadorBE oCoordinador = new RCoordinadorBE();
            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();

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
                //Para nuevo API
                oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                oSolicitudInversion.IdTipoAPI.Id = TiposAPI.IdNuevoProyecto;
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
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto != null ? oSolicitudInversion.NombreProyecto : "";

                if (oSolicitudInversion.ResponsableProyecto != null)
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;

                contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioCreador = oSolicitudInversion.UsuarioCreador.CuentaUsuario;
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario;
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
                //contenidoCabecera.Parts.IdAPIInicial = oSolicitudInversion.IdAPIInicial;
                ManejadorLogSimpleBL.WriteLog("ENTRO 4");


                //if (oSolicitudInversion.IdCoordinador != null)
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudInversion.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());

                //if (oSolicitudInversion.IdCoordinador != null)
                //{
                //    RCoordinadorBE oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                //    ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", new Guid(), "<>")
                //                                            .QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                //}
                if (oSolicitudInversion.IdCoordinador != null)
                {
                    oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                    //ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", new Guid(), "<>")
                    //                                        .QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                    contenidoCabecera.Parts.IdCoordinador = oCoordinador.Id.Value.ToString();
                }
                ManejadorLogSimpleBL.WriteLog("ENTRO 5");

                //ScriptorContent prueba = canalTipoCapex.GetContent(new Guid("6F9CFD9E-B799-4441-8969-35E87F6A9FAC"));

                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 6");
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());

                if (oSolicitudInversion.IdSociedad != null)
                    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad.Id, "=").ToList().FirstOrDefault());
                ManejadorLogSimpleBL.WriteLog("ENTRO 7");
                if (oSolicitudInversion.IdTipoAPI != null)
                    contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault());

                ManejadorLogSimpleBL.WriteLog("ENTRO 10");
                if (oSolicitudInversion.IdCeCo != null)
                {
                    RMacroservicioBE oMacroServicioBE = new RMacroservicioBE();
                    oMacroServicioBE = om.ObtenerMacroservicioPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if (oMacroServicioBE.Id != null)
                        contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oMacroServicioBE.Id, "=").ToList().FirstOrDefault());

                    RSectorBE oSectorBE = new RSectorBE();
                    oSectorBE = os.ObtenerSectorPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if (oSectorBE.Id != null)
                        contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSectorBE.Id, "=").ToList().FirstOrDefault());

                    //contenidoCabecera.Parts.IdCeCo = ScriptorDropdownListValue.FromContent(canalCeCo.QueryContents("#Id", oSolicitudInversion.IdCeCo.Id, "=").ToList().FirstOrDefault());
                    contenidoCabecera.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.Value.ToString();
                }
                if (oSolicitudInversion.IdPeriodoPRI != null)
                    contenidoCabecera.Parts.IdPeriodoPRI = oSolicitudInversion.IdPeriodoPRI.codigo;
                #endregion SetearCabecera


                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();
                CambiarEstado(objScriptor, new Guid(Canales.SolicitudInversion), contenidoCabecera.Id, "notificar");
                oRespuesta.NumSolicitud = Correlativo;

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera
                Guid? id = null;
                if (oRespuesta.success)
                {
                    //GuardarCabeceraDestino
                    ScriptorContent contenidoDestino = canalCabeceraDestinoTraslado.NewContent();

                    contenidoDestino.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                    contenidoDestino.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto;
                    contenidoDestino.Parts.PptAprobadoUSD = 0;
                    contenidoDestino.Parts.MontoATrasladarUSD = oSolicitudInversion.MontoTotal;
                    contenidoDestino.Parts.NuevoPptoUSD = contenidoDestino.Parts.PptAprobadoUSD + oSolicitudInversion.MontoTotal;
                    //contenidoDestino.Parts.Motivo = "";
                    contenidoDestino.Parts.Van = oSolicitudInversion.VAN;
                    contenidoDestino.Parts.Tir = oSolicitudInversion.TIR;
                    contenidoDestino.Parts.Pri = oSolicitudInversion.PRI;
                    contenidoDestino.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras;
                    contenidoDestino.Parts.IdPriTraslado = oSolicitudInversion.IdPeriodoPRI.codigo;

                    var resultadoCabeceraDestino=contenidoDestino.Save();

                    if(resultadoCabeceraDestino)
                    { 
                        //Guardar detalle traslado
                        ScriptorContent contenidoDetalleTraslado = canalDetalleTraslado.NewContent();
                        contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(canalTraslado.QueryContents("#Id", oSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault());
                        contenidoDetalleTraslado.Parts.Tipo = "Destino";
                        contenidoDetalleTraslado.Parts.MontoATrasladar = oSolicitudInversion.MontoTotal;
                        contenidoDetalleTraslado.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                        contenidoDetalleTraslado.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto;
                        contenidoDetalleTraslado.Parts.PptoAprobado = 0;
                        contenidoDetalleTraslado.Parts.NuevoPpto = contenidoDetalleTraslado.Parts.PptoAprobado + oSolicitudInversion.MontoTotal;
                        contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoDestino);

                        contenidoDetalleTraslado.Save();

                        //Continuar con los detalles de destino AQUIIII

                        id = contenidoCabecera.Id;
                        oRespuesta.Id = id.ToString();
                        oRespuesta.CoordinadorCuenta = oSolicitudInversion.IdCoordinador.CuentaRed;
                        oRespuesta.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                        oRespuesta.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                        oRespuesta.FechaCreacion = oSolicitudInversion.FechaCreacion;
                        oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();
                        oRespuesta.IdCabeceraDestino = contenidoDestino.Id.ToString();
                        oRespuesta.IdDetalleTraslado = contenidoDetalleTraslado.Id.ToString();

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
                //oRespuesta.Mensaje = ex.Message;
                return oRespuesta;
            }

            return oRespuesta;
            //return "cabecera =>" + resultadoCabecera + " detalle =>" + Common.WebUtil.ToJson(resultadoDetalle);


        }

        public bool InsertarDetalleSolicitudInversion(RDetalleSolicitudInversionBE det, string IdSolicitudInversion)
        {            
            bool detalle = false;
            List<string> resultadoDetalle = new List<string>();

            #region SetearDetalle
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorContent contenidoDetalle;

            #region GuardarInversion

                bool resultInversion = false;
                ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

                ScriptorContent contentInversion = canalInversion.NewContent();

                /*
                 if (det.IdTipoActivo != null)
                    if (det.IdTipoActivo.Id != null)
                        contentInversion.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
                */
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

                contenidoDetalle.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contentInversion);
                ManejadorLogSimpleBL.WriteLog("ENTRO 11");
                contenidoDetalle.Parts.VidaUtil = det.VidaUtil;

                
                contenidoDetalle.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contentInversion);
                
                ManejadorLogSimpleBL.WriteLog("ENTRO 12");
                if (det.IdTipoActivo != null)
                    if (det.IdTipoActivo.Id != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                //contenidoDetalle.Parts.MontoAmpliarMonedaCotiazada = det.MontoAAmpliarMonedaCotizada;
                //    contenidoDetalle.Parts.VidaUtilAmpliar = det.VidaUtilAmpliar;
                //contenidoDetalle.Parts.MontoAmpliarUSD = det.MontoAAmpliarUSD;


                ManejadorLogSimpleBL.WriteLog("ENTRO 13");

                detalle = contenidoDetalle.Save();
                
            }
                       

            #endregion SetearDetalle

            return detalle;
 
        }

        public bool InsertarDetalleSolicitudInversionParaTraslado(RDetalleSolicitudInversionBE det, string IdSolicitudInversion, string IdSolicitudInversionTraslado, string IdCabeceraDestino)
        {
            bool detalle = false;
            List<string> resultadoDetalle = new List<string>();

            #region SetearDetalle
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalEstadosMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalTipoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoMovimiento));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalSolicitudInversionTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));
            ScriptorChannel canalCabeceraDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorContent contenidoDetalle;

            RSolicitudInversionDAL oSolicitudDAL = new RSolicitudInversionDAL();
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();
            oSolicitudInversion = oSolicitudDAL.BuscarPorId(IdSolicitudInversion,"");

            //Se crea inversion y Movimiento
            #region GuardarInversion

            bool resultInversion = false;
            bool resultMovimiento = false;
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            ScriptorContent inv = canalInversion.NewContent();
                       
            inv.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.ToString();
            inv.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

            inv.Parts.CodigoProyecto = String.IsNullOrEmpty(oSolicitudInversion.CodigoProyecto) ? "" : oSolicitudInversion.CodigoProyecto;
            inv.Parts.NombreProyecto = String.IsNullOrEmpty(oSolicitudInversion.NombreProyecto) ? "" : oSolicitudInversion.NombreProyecto;
            inv.Parts.MontoDisponible = det.PrecioMontoUSD;
            inv.Parts.MontoContable = det.PrecioMontoUSD;
            inv.Parts.Descripcion = oSolicitudInversion.NumSolicitud;
            inv.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault());

            resultInversion = inv.Save();

            if (resultInversion)
            {
                //Registramos un movimiento
                ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
                ScriptorContent contenido = canalMovimiento.NewContent();

                contenido.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault());
                contenido.Parts.IdTipoMovimiento = ScriptorDropdownListValue.FromContent(canalTipoMovimiento.QueryContents("#Id", TiposMovimiento.API, "=").ToList().FirstOrDefault());
                contenido.Parts.Monto = det.PrecioMontoUSD;
                contenido.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                contenido.Parts.Descripcion = "";
                contenido.Parts.FechaMovimiento = DateTime.Now;
                contenido.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(canalSolicitudInversion.QueryContents("#Id", oSolicitudInversion.Id, "=").ToList().FirstOrDefault());
                contenido.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(canalSolicitudInversionTraslado.QueryContents("#Id", IdSolicitudInversionTraslado, "=").ToList().FirstOrDefault());
                //contenido.Parts.IdSolicitudInversionTraslado = "";

                resultMovimiento = contenido.Save();

            }
                /*-----------------------------------------------------------------------*/

            #endregion

            if (resultMovimiento)
            {

                #region GuardarDetalleDestino

                    ScriptorContent contentDetalleDestinoTraslado = canalDetalleDestino.NewContent();

                    contentDetalleDestinoTraslado.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
                    if (det.IdTipoCambio != null)
                    {
                        if (det.IdTipoCambio != new Guid().ToString())
                            contentDetalleDestinoTraslado.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(canalTipoCambio.QueryContents("#Id", det.IdTipoCambio, "=").ToList().FirstOrDefault());
                        else
                        {
                            //Es dolares
                        }

                    }
                    contentDetalleDestinoTraslado.Parts.PptAprobado = 0;
                    contentDetalleDestinoTraslado.Parts.VidaUtil = det.VidaUtil;
                    contentDetalleDestinoTraslado.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                    contentDetalleDestinoTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(canalCabeceraDestino.QueryContents("#Id", IdCabeceraDestino, "=").ToList().FirstOrDefault());
                    contentDetalleDestinoTraslado.Parts.NuevoPptoMonedaCotizada = contentDetalleDestinoTraslado.Parts.PptAprobado + det.PrecioMontoOrigen;
                    contentDetalleDestinoTraslado.Parts.NuevoPptoOIUSD = det.PrecioMontoUSD;
                    contentDetalleDestinoTraslado.Parts.MontoATrasladar = det.PrecioMontoUSD;

                    contentDetalleDestinoTraslado.Save();
                    

                #endregion

                #region GuardarDetalleProyectoNuevo

                    contenidoDetalle = canalDetalle.NewContent();

                    contenidoDetalle.Parts.Cantidad = det.Cantidad;
                    contenidoDetalle.Parts.PrecioUnitario = det.PrecioUnitario;
                    contenidoDetalle.Parts.PrecioUnitarioUSD = det.PrecioUnitarioUSD;
                    contenidoDetalle.Parts.PrecioMontoOrigen = det.PrecioMontoOrigen;
                    contenidoDetalle.Parts.PrecioMontoUSD = det.PrecioMontoUSD;
                    contenidoDetalle.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(canalCabecera.QueryContents("#Id", IdSolicitudInversion, "=").ToList().FirstOrDefault());
                    contenidoDetalle.Parts.IdDetalleDestinoTraslado = ScriptorDropdownListValue.FromContent(contentDetalleDestinoTraslado);
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

                    contenidoDetalle.Save("notificar");

                    #endregion GuardarDetalleProyectoNuevo
            }
            #endregion SetearDetalle

            return detalle;

        }

        public RResultadoNuevoProyectoBE ModificarSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {            
            string track = "";
            RResultadoNuevoProyectoBE oRespuesta = new RResultadoNuevoProyectoBE();


            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RMacroservicioDAL om = new RMacroservicioDAL();
            RSectorDAL os = new RSectorDAL();

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

            ScriptorContent contenidoCabecera = canalCabecera.QueryContents("#Id", oSolicitudInversion.Id, "=").ToList().FirstOrDefault();

            try
            {

                #region SetearCabecera
                
                //contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.NombreCortoSolicitud = oSolicitudInversion.NombreCortoSolicitud;
                
                contenidoCabecera.Parts.Ubicacion = oSolicitudInversion.Ubicacion;
                contenidoCabecera.Parts.Marca = oSolicitudInversion.Marca;
                contenidoCabecera.Parts.Descripcion = oSolicitudInversion.Descripcion != null ? oSolicitudInversion.Descripcion : "";
                contenidoCabecera.Parts.Observaciones = oSolicitudInversion.Observaciones != null ? oSolicitudInversion.Observaciones : "";
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;

                if (oSolicitudInversion.Responsable != null)
                    contenidoCabecera.Parts.Responsable = oSolicitudInversion.Responsable.CuentaUsuario;

                contenidoCabecera.Parts.Van = oSolicitudInversion.VAN;
                contenidoCabecera.Parts.Tir = oSolicitudInversion.TIR;
                contenidoCabecera.Parts.Pri = oSolicitudInversion.PRI;
                contenidoCabecera.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras != null ? oSolicitudInversion.ObservacionesFinancieras : "";
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto != null ? oSolicitudInversion.NombreProyecto : "";

                if (oSolicitudInversion.IdPeriodoPRI != null)
                    contenidoCabecera.Parts.IdPeriodoPRI = oSolicitudInversion.IdPeriodoPRI.codigo;

                if (oSolicitudInversion.ResponsableProyecto != null)
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;

                //contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                //contenidoCabecera.Parts.UsuarioCreador = oSolicitudInversion.UsuarioCreador;
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario;
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                //contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;    
 
                RCoordinadorBE oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                /*Anderson */
                //if (oSolicitudInversion.IdCoordinador != null)
                //{
                //    RCoordinadorBE oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                //    ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", new Guid(), "<>")
                //                                            .QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                //}
                /*Anderson */
                //if (oSolicitudInversion.IdCoordinador != null)
                    
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudInversion.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());

                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());

                //contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());

                //if (oSolicitudInversion.IdSociedad != null)
                //    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad.Id, "=").ToList().FirstOrDefault());

                //if (oSolicitudInversion.IdTipoAPI != null)
                //    contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault());

                /*Anderson */
                if (oSolicitudInversion.IdCeCo != null)
                {
                    RMacroservicioBE oMacroServicioBE = new RMacroservicioBE();
                    oMacroServicioBE = om.ObtenerMacroservicioPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());
                    if (oMacroServicioBE.Id != null)
                        contenidoCabecera.Parts.IdMacroServicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oMacroServicioBE.Id, "=").ToList().FirstOrDefault());

                    RSectorBE oSectorBE = new RSectorBE();
                    oSectorBE = os.ObtenerSectorPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if (oSectorBE.Id != null)
                        contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSectorBE.Id, "=").ToList().FirstOrDefault());

                    contenidoCabecera.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.ToString();
                }
                /*Anderson */
                #endregion SetearCabecera
                
                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera                
                if (oRespuesta.success)
                {
                    ActualizarFecha(oSolicitudInversion.Id.ToString(), oSolicitudInversion.FechaInicio, oSolicitudInversion.FechaCierre);
                    oRespuesta.Id = contenidoCabecera.Id.ToString();
                    oRespuesta.NumSolicitud = contenidoCabecera.Parts.NumSolicitud;
                    oRespuesta.CoordinadorCuenta = oSolicitudInversion.IdCoordinador.CuentaRed;
                    oRespuesta.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                    oRespuesta.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    oRespuesta.FechaCreacion = contenidoCabecera.Parts.FechaCreacion;
                    oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();
                }

                track = "Obtuvo Id de cabecera guardada";
                           
            }
            catch (Exception ex)
            {                
                return oRespuesta;
            }

            return oRespuesta;
 
        }

        public RResultadoNuevoProyectoBE ModificarSolicitudInversionTraslado(RSolicitudInversionBE oSolicitudInversion, string IdDetalleTraslado)
        {
            string track = "";
            RResultadoNuevoProyectoBE oRespuesta = new RResultadoNuevoProyectoBE();


            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalTipoCapex = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCapex));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            ScriptorChannel canalTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCabeceraDestinoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorChannel canalDetalleDestinoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RMacroservicioDAL om = new RMacroservicioDAL();
            RSectorDAL os = new RSectorDAL();

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

            ScriptorContent contenidoCabecera = canalCabecera.QueryContents("#Id", oSolicitudInversion.Id, "=").ToList().FirstOrDefault();

            
                #region SetearCabecera

                //contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.NombreCortoSolicitud = oSolicitudInversion.NombreCortoSolicitud;
                //contenidoCabecera.Parts.FechaInicio = oSolicitudInversion.FechaInicio;
                //contenidoCabecera.Parts.FechaCierre = oSolicitudInversion.FechaCierre;
                contenidoCabecera.Parts.Ubicacion = oSolicitudInversion.Ubicacion;
                contenidoCabecera.Parts.Marca = oSolicitudInversion.Marca;
                contenidoCabecera.Parts.Descripcion = oSolicitudInversion.Descripcion != null ? oSolicitudInversion.Descripcion : "";
                contenidoCabecera.Parts.Observaciones = oSolicitudInversion.Observaciones != null ? oSolicitudInversion.Observaciones : "";
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;

                if (oSolicitudInversion.IdPeriodoPRI != null)
                    contenidoCabecera.Parts.IdPeriodoPRI = oSolicitudInversion.IdPeriodoPRI.codigo;

                if (oSolicitudInversion.Responsable != null)
                    contenidoCabecera.Parts.Responsable = oSolicitudInversion.Responsable.CuentaUsuario;

                contenidoCabecera.Parts.Van = oSolicitudInversion.VAN;
                contenidoCabecera.Parts.Tir = oSolicitudInversion.TIR;
                contenidoCabecera.Parts.Pri = oSolicitudInversion.PRI;
                contenidoCabecera.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras != null ? oSolicitudInversion.ObservacionesFinancieras : "";
                contenidoCabecera.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                contenidoCabecera.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto != null ? oSolicitudInversion.NombreProyecto : "";

                if (oSolicitudInversion.ResponsableProyecto != null)
                    contenidoCabecera.Parts.ResponsableProyecto = oSolicitudInversion.ResponsableProyecto.CuentaUsuario;

                //contenidoCabecera.Parts.MontoAAmpliar = oSolicitudInversion.MontoAAmpliar;
                contenidoCabecera.Parts.FlagPlanBase = oSolicitudInversion.FlagPlanBase;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                //contenidoCabecera.Parts.UsuarioCreador = oSolicitudInversion.UsuarioCreador;
                contenidoCabecera.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario;
                contenidoCabecera.Parts.Activo = oSolicitudInversion.Activo;
                contenidoCabecera.Parts.MontoTotal = oSolicitudInversion.MontoTotal;
                //contenidoCabecera.Parts.IdInstancia = oSolicitudInversion.IdInstancia;     

                RCoordinadorBE oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                /*Anderson */
                //if (oSolicitudInversion.IdCoordinador != null)
                //{
                //    RCoordinadorBE oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorIdCeCo(oSolicitudInversion.IdCeCo.Id.Value, oSolicitudInversion.IdCoordinador.CuentaRed);
                //    ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", new Guid(), "<>")
                //                                            .QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                //}
                /*Anderson */
                //if (oSolicitudInversion.IdCoordinador != null)

                //    contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudInversion.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());

                if (oSolicitudInversion.IdTipoCapex != null)
                    contenidoCabecera.Parts.IdTipoCapex = ScriptorDropdownListValue.FromContent(canalTipoCapex.QueryContents("#Id", oSolicitudInversion.IdTipoCapex.Id, "=").ToList().FirstOrDefault());

                //contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());

                //if (oSolicitudInversion.IdSociedad != null)
                //    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudInversion.IdSociedad.Id, "=").ToList().FirstOrDefault());

                //if (oSolicitudInversion.IdTipoAPI != null)
                //    contenidoCabecera.Parts.IdTipoAPI = ScriptorDropdownListValue.FromContent(canalTipoAPI.QueryContents("#Id", oSolicitudInversion.IdTipoAPI.Id, "=").ToList().FirstOrDefault());

                /*Anderson */
                if (oSolicitudInversion.IdCeCo != null)
                {
                    RMacroservicioBE oMacroServicioBE = new RMacroservicioBE();
                    oMacroServicioBE = om.ObtenerMacroservicioPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());
                    if (oMacroServicioBE.Id != null)
                        contenidoCabecera.Parts.IdMacroServicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oMacroServicioBE.Id, "=").ToList().FirstOrDefault());

                    RSectorBE oSectorBE = new RSectorBE();
                    oSectorBE = os.ObtenerSectorPorCeCo(oSolicitudInversion.IdCeCo.Id.ToString());

                    if (oSectorBE.Id != null)
                        contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSectorBE.Id, "=").ToList().FirstOrDefault());

                    contenidoCabecera.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.Value.ToString();
                }
                /*Anderson */
                #endregion SetearCabecera

                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oRespuesta.success = contenidoCabecera.Save();
                
                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera                
                if (oRespuesta.success)
                {
                    ActualizarFecha(oSolicitudInversion.Id.ToString(), oSolicitudInversion.FechaInicio, oSolicitudInversion.FechaCierre);
                    //Guardar detalle traslado
                    ScriptorContent contenidoDetalleTraslado = canalDetalleTraslado.QueryContents("#Id", IdDetalleTraslado, "=").ToList().FirstOrDefault();
                    //contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(canalTraslado.QueryContents("#Id", oSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault());
                    //contenidoDetalleTraslado.Parts.Tipo = "Destino";
                    contenidoDetalleTraslado.Parts.MontoATrasladar = oSolicitudInversion.MontoTotal;
                    contenidoDetalleTraslado.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                    contenidoDetalleTraslado.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto;
                    contenidoDetalleTraslado.Parts.PptoAprobado = 0;
                    contenidoDetalleTraslado.Parts.NuevoPpto = contenidoDetalleTraslado.Parts.PptoAprobado + oSolicitudInversion.MontoTotal;
                    //contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoDestino);
                    contenidoDetalleTraslado.Save();

                    ScriptorContent contenidoDestino = ((ScriptorDropdownListValue)contenidoDetalleTraslado.Parts.IdCabeceraDestino).Content;
                    //ScriptorContent contenidoDestino = canalCabeceraDestinoTraslado.QueryContents("#Id", IdDetalleTraslado, "=").ToList().FirstOrDefault();
                    contenidoDestino.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;
                    contenidoDestino.Parts.NombreProyecto = oSolicitudInversion.NombreProyecto;
                    contenidoDestino.Parts.PptAprobadoUSD = 0;
                    contenidoDestino.Parts.MontoATrasladarUSD = oSolicitudInversion.MontoTotal;
                    contenidoDestino.Parts.NuevoPptoUSD = contenidoDestino.Parts.PptAprobadoUSD + oSolicitudInversion.MontoTotal;
                    //contenidoDestino.Parts.Motivo = "";
                    contenidoDestino.Parts.Van = oSolicitudInversion.VAN;
                    contenidoDestino.Parts.Tir = oSolicitudInversion.TIR;
                    contenidoDestino.Parts.Pri = oSolicitudInversion.PRI;
                    contenidoDestino.Parts.ObservacionesFinancieras = oSolicitudInversion.ObservacionesFinancieras;
                    contenidoDestino.Parts.IdPriTraslado = oSolicitudInversion.IdPeriodoPRI.codigo;
                    contenidoDestino.Save();
                                                               

                    oRespuesta.Id = contenidoCabecera.Id.ToString();
                    oRespuesta.NumSolicitud = contenidoCabecera.Parts.NumSolicitud;
                    oRespuesta.CoordinadorCuenta = oSolicitudInversion.IdCoordinador.CuentaRed;
                    oRespuesta.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                    oRespuesta.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    oRespuesta.FechaCreacion = contenidoCabecera.Parts.FechaCreacion;
                    oRespuesta.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();
                    oRespuesta.IdCabeceraDestino = contenidoDestino.Id.ToString();
                    oRespuesta.IdDetalleTraslado = contenidoDetalleTraslado.Id.ToString();
                    
                }

                track = "Obtuvo Id de cabecera guardada";

            
            return oRespuesta;

        }
               

        public bool ModificarDetalleSolicitudInversion(RDetalleSolicitudInversionBE det, string IdSolicitudInversion)
        {
            
            bool detalle = false;
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));



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

                    if (det.IdMonedaCotizada != null)
                        contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());

                    if(det.IdTipoActivo != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                    contenidoDetalle.Parts.IdInversion = det.IdInversion;
                    contenidoDetalle.Parts.VidaUtil = det.VidaUtil;
                    contenidoDetalle.Parts.Monto = det.PrecioMontoUSD;
                    

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

        public bool ModificarDetalleSolicitudInversionParaTraslado(RDetalleSolicitudInversionBE det, string IdSolicitudInversion)
        {

            bool detalle = false;
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));

            RSolicitudInversionDAL oSolicitudDAL = new RSolicitudInversionDAL();
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();
            oSolicitudInversion = oSolicitudDAL.BuscarPorId(IdSolicitudInversion,"");

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

                    if (det.IdMonedaCotizada != null)
                        contenidoDetalle.Parts.IdMonedaCotizada = ScriptorDropdownListValue.FromContent(canalMoneda.QueryContents("#Id", det.IdMonedaCotizada, "=").ToList().FirstOrDefault());

                    if (det.IdTipoActivo != null)
                        contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                    contenidoDetalle.Parts.IdInversion = det.IdInversion;
                    contenidoDetalle.Parts.VidaUtil = det.VidaUtil;
                    contenidoDetalle.Parts.Monto = det.PrecioMontoUSD;

                    #region ModificarDetalleDestino
                    if (((ScriptorDropdownListValue)contenidoDetalle.Parts.IdDetalleDestinoTraslado) != null)
                    {
                        ScriptorContent contentDetalleDestinoTraslado = ((ScriptorDropdownListValue)contenidoDetalle.Parts.IdDetalleDestinoTraslado).Content;

                        contentDetalleDestinoTraslado.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
                        if (det.IdTipoCambio != null)
                        {
                            if (det.IdTipoCambio != new Guid().ToString())
                                contentDetalleDestinoTraslado.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(canalTipoCambio.QueryContents("#Id", det.IdTipoCambio, "=").ToList().FirstOrDefault());
                            else
                            {
                                //Es dolares
                            }

                        }
                        contentDetalleDestinoTraslado.Parts.PptAprobado = 0;
                        contentDetalleDestinoTraslado.Parts.VidaUtil = det.VidaUtil;
                        //contentDetalleDestinoTraslado.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                        //contentDetalleDestinoTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(canalCabeceraDestino.QueryContents("#Id", IdCabeceraDestino, "=").ToList().FirstOrDefault());
                        contentDetalleDestinoTraslado.Parts.NuevoPptoMonedaCotizada = contentDetalleDestinoTraslado.Parts.PptAprobado + det.PrecioMontoOrigen;
                        contentDetalleDestinoTraslado.Parts.NuevoPptoOIUSD = det.PrecioMontoUSD;
                        contentDetalleDestinoTraslado.Parts.MontoATrasladar = det.PrecioMontoUSD;

                        contentDetalleDestinoTraslado.Save();

                        #region Modificar Inversion

                        ScriptorContent inv = ((ScriptorDropdownListValue)contentDetalleDestinoTraslado.Parts.IdInversion).Content;
                        bool resultInversion = false;
                        bool resultMovimiento = false;  

                        inv.Parts.IdCeCo = oSolicitudInversion.IdCeCo.Id.ToString();
                        inv.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());
                        inv.Parts.CodigoProyecto = String.IsNullOrEmpty(oSolicitudInversion.CodigoProyecto) ? "" : oSolicitudInversion.CodigoProyecto;
                        inv.Parts.NombreProyecto = String.IsNullOrEmpty(oSolicitudInversion.NombreProyecto) ? "" : oSolicitudInversion.NombreProyecto;
                        inv.Parts.MontoDisponible = det.PrecioMontoUSD;
                        inv.Parts.MontoContable = det.PrecioMontoUSD;
                        //inv.Parts.Descripcion = oSolicitudInversion.NumSolicitud;                      
                        resultInversion = inv.Save();

                        if (resultInversion)
                        {
                            //Modificamos movimiento asociado a la inversion
                            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
                            ScriptorContent contenido = canalMovimiento.QueryContents("IdInversion", inv.Id, "=").ToList().FirstOrDefault();
                            
                            contenido.Parts.Monto = det.PrecioMontoUSD;
                            //contenido.Parts.IdInversion = ScriptorDropdownListValue.FromContent(inv);
                            //ontenido.Parts.Descripcion = "";
                            //contenido.Parts.FechaMovimiento = DateTime.Now;
                            
                            resultMovimiento = contenido.Save();

                        }
                        /*-----------------------------------------------------------------------*/

                        #endregion

                    }
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
                

        public bool EliminarDetalle(string IdDetalle)
        {
            bool detalle = false;
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalMovimientos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            
            ScriptorContent contenidoDetalle = canalDetalle.QueryContents("#Id", IdDetalle, "=").ToList().FirstOrDefault();
            detalle = CambiarEstado(objScriptor, new Guid(Canales.DetalleSolicitudInversion), new Guid(IdDetalle), "Borrar");

            if(detalle)
            {
                if (contenidoDetalle.Parts.IdInversion != null)
                {
                    ScriptorContent contenidoInversion = canalInversion.QueryContents("#Id", ((ScriptorDropdownListValue)contenidoDetalle.Parts.IdInversion).Content.Id, "=").ToList().FirstOrDefault();
                    detalle = CambiarEstado(objScriptor, new Guid(Canales.Inversion), contenidoInversion.Id, "Borrar");

                    ScriptorContent contenidoMovimiento = canalMovimientos.QueryContents("IdInversion", ((ScriptorDropdownListValue)contenidoDetalle.Parts.IdInversion).Content.Id, "=").ToList().FirstOrDefault();
                    if (contenidoMovimiento != null)
                    {
                        detalle = CambiarEstado(objScriptor, new Guid(Canales.Movimientos), contenidoMovimiento.Id, "Borrar");
                    }
                }

            }
            return detalle;
 
        }

        public bool AnularSolicitudInversion(string IdSolicitud, string CuentaUsuario)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();
            if (oSolicitud != null)
            {
                if (((ScriptorDropdownListValue)oSolicitud.Parts.IdEstado).Content.Id.ToString().ToUpper() == Estados.Creado)
                {
                    //Apago
                    List<ScriptorContent> oDetalles = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oSolicitud.Id, "=").ToList();
                    List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                    try
                    {
                        //resultado = oSolicitud.Save("Borrar");
                        oSolicitud.Parts.UsuarioModifico = CuentaUsuario;
                        oSolicitud.Parts.FechaModificacion = DateTime.Now;
                        oSolicitud.Save();

                        resultadoCabecera = CambiarEstado(objScriptor, canalSolicitudInversion.Id, oSolicitud.Id, "Borrar");
                        if (!resultadoCabecera)
                            throw new Exception("Error al anular cabecera");

                        foreach (ScriptorContent item in oDetalles)
                        {
                            if (!CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, item.Id, "Borrar"))
                            {
                                throw new Exception("Error al anular detalle");
                            }
                            else
                            {
                                oDetallesOk.Add(item);
                            }

                            //Borramos la inversion
                            ScriptorContent contentInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content;

                            if (contentInversion != null)
                            {
                                CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");
 
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //RollBAck
                        foreach (ScriptorContent item in oDetallesOk)
                        {
                            CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, item.Id, "Recuperar");
                        }

                        CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, oSolicitud.Id, "Recuperar");


                    }
                }                
                else if (((ScriptorDropdownListValue)oSolicitud.Parts.IdEstado).Content.Id.ToString().ToUpper() != Estados.Cerrado)
                {
                    //Cambio estado a anulado
                    //Solo Administradores

                    List<ScriptorContent> oDetalles = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oSolicitud.Id, "=").ToList();
                    List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                    try
                    {
                        oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Anulado, "=").ToList().FirstOrDefault());
                        oSolicitud.Parts.UsuarioModifico = CuentaUsuario;
                        oSolicitud.Parts.FechaModificacion = DateTime.Now;

                        resultadoCabecera = oSolicitud.Save();
                        if (!resultadoCabecera)
                            throw new Exception("Error al anular cabecera");

                        foreach (ScriptorContent item in oDetalles)
                        {
                            //Rechazamos la inversion
                            ScriptorContent contentInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content;
                            if (contentInversion != null)
                            {
                                contentInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                contentInversion.Save();


                            }

                            ScriptorContent contentMovimiento = canalMovimiento.QueryContents("IdInversion", contentInversion.Id, "=").ToList().FirstOrDefault();
                            if (contentMovimiento != null)
                            {
                                contentMovimiento.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                contentMovimiento.Save();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //RollBAck
                        foreach (ScriptorContent item in oDetallesOk)
                        {
                            CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, item.Id, "Recuperar");
                        }

                        CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, oSolicitud.Id, "Recuperar");


                    }

                }
                else
                    ManejadorLogSimpleBL.WriteLog("No se puede anular la solciitud");
            }

            return resultadoCabecera;
 
        }

        public bool AnularApagado(string NumSolicitud, string CuentaUsuario)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();
            if (oSolicitud != null)
            {
                List<ScriptorContent> oDetalles = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oSolicitud.Id, "=").ToList();
                List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                try
                {
                    //resultado = oSolicitud.Save("Borrar");
                    oSolicitud.Parts.UsuarioModifico = CuentaUsuario;
                    oSolicitud.Parts.FechaModificacion = DateTime.Now;
                    oSolicitud.Save();

                    resultadoCabecera = CambiarEstado(objScriptor, canalSolicitudInversion.Id, oSolicitud.Id, "Borrar");
                    if (!resultadoCabecera)
                        throw new Exception("Error al anular cabecera");

                    foreach (ScriptorContent item in oDetalles)
                    {
                        if (!CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, item.Id, "Borrar"))
                        {
                            throw new Exception("Error al anular detalle");
                        }
                        else
                        {
                            oDetallesOk.Add(item);
                        }

                        //Borramos la inversion
                        ScriptorContent contentInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content;

                        if (contentInversion != null)
                        {
                            CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");

                        }
                    }
                }
                catch (Exception ex)
                {
                    ManejadorLogSimpleBL.WriteLog("No se borró::::" + ex.Message);

                }
                
            }

            return resultadoCabecera;

        }

        public bool AnularAnulado(string NumSolicitud, string CuentaUsuario)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();
            if (oSolicitud != null)
            {
                List<ScriptorContent> oDetalles = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oSolicitud.Id, "=").ToList();
                List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                try
                {
                    
                    //resultadoCabecera = CambiarEstado(objScriptor, canalSolicitudInversion.Id, oSolicitud.Id, "Borrar");
                    oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Anulado, "=").ToList().FirstOrDefault());
                    oSolicitud.Parts.UsuarioModifico = CuentaUsuario;
                    oSolicitud.Parts.FechaModificacion = DateTime.Now;

                    resultadoCabecera = oSolicitud.Save();
                    if (!resultadoCabecera)
                        throw new Exception("Error al anular cabecera");

                    foreach (ScriptorContent item in oDetalles)
                    {
                        //Rechazamos la inversion
                        ScriptorContent contentInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content;
                        if (contentInversion != null)
                        {
                            contentInversion.Parts.IdEstadoMovimiento = canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault();
                            contentInversion.Save();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    ManejadorLogSimpleBL.WriteLog("No se borró :::: " + ex.Message);

                }

            }

            return resultadoCabecera;

        }

        public bool ActualizarEstadoSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));            
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));            

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;            
            
            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("NumSolicitud", oSolicitudInversion.NumSolicitud, "=").ToList().FirstOrDefault();

            oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", oSolicitudInversion.IdEstado.Id, "=").ToList().FirstOrDefault());
            oSolicitud.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
            oSolicitud.Parts.FechaEnvio = DateTime.Now;
            oSolicitud.Parts.FechaModificacion = DateTime.Now;
            if (oSolicitudInversion.UsuarioModifico != null)
            {
                oSolicitud.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario != null ? oSolicitudInversion.UsuarioModifico.CuentaUsuario : "";
            }
            else
            {
                oSolicitud.Parts.UsuarioModifico = "";
            }
            
            resultadoCabecera = oSolicitud.Save();
                               

            return resultadoCabecera;

        }

        public bool ActualizarEstadoWorkflowProyectoNuevoTraslado(string IdSolicitudInversion)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            //ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            try
            {
                ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("Id", new Guid(IdSolicitudInversion), "=").ToList().FirstOrDefault();
                CambiarEstado(objScriptor, canalSolicitudInversion.Id, oSolicitud.Id, "Publicar");


                List<ScriptorContent> oDetalleSolicitud = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", new Guid(IdSolicitudInversion), "=").ToList();

                foreach (ScriptorContent item in oDetalleSolicitud)
                {
                    CambiarEstado(objScriptor, canalDetalleSolicitudInversion.Id, item.Id, "Publicar");
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }

        public bool CerrarSolicitudInversion(RSolicitudInversionBE oSolicitudInversion)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("NumSolicitud", oSolicitudInversion.NumSolicitud, "=").ToList().FirstOrDefault();

            oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", oSolicitudInversion.IdEstado.Id, "=").ToList().FirstOrDefault());
            //oSolicitud.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
            oSolicitud.Parts.FechaEnvio = DateTime.Now;
            oSolicitud.Parts.FechaModificacion = DateTime.Now;
            if (oSolicitudInversion.UsuarioModifico != null)
            {
                oSolicitud.Parts.UsuarioModifico = oSolicitudInversion.UsuarioModifico.CuentaUsuario != null ? oSolicitudInversion.UsuarioModifico.CuentaUsuario : "";
            }
            else
            {
                oSolicitud.Parts.UsuarioModifico = "";
            }
            oSolicitud.Parts.CodigoProyecto = oSolicitudInversion.CodigoProyecto;

            resultadoCabecera = oSolicitud.Save();

            return resultadoCabecera;

        }

        public bool ActualizarEstadoInversion(RSolicitudInversionBE oSolicitudInversion, string IdEstado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;            
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                foreach (RDetalleSolicitudInversionBE det in oSolicitudInversion.DetalleSolicitudInversion)
                {
                    string nombreProcedure = "ActualizarEstadoInversion";
                    SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter par1 = new SqlParameter("@IdInversion", det.IdInversion.Id);
                    cmd.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@IdEstado", IdEstado);
                    cmd.Parameters.Add(par2);
                    
                    if(cmd.ExecuteNonQuery()>0)
                        Resultado = true;
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
            return Resultado;
        }

        public bool ActualizarFecha(string IdSolicitud, DateTime FechaInicio, DateTime FechaCierre)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "ActualizarFechaSolicitudInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitud", IdSolicitud);
                cmd.Parameters.Add(par1);

                if(FechaInicio==DateTime.MinValue)
                    par1 = new SqlParameter("@FechaInicio", DBNull.Value);
                else
                    par1 = new SqlParameter("@FechaInicio", FechaInicio);
                cmd.Parameters.Add(par1);

                if (FechaCierre == DateTime.MinValue)
                    par1 = new SqlParameter("@FechaCierre", DBNull.Value);
                else
                    par1 = new SqlParameter("@FechaCierre", FechaCierre);
                cmd.Parameters.Add(par1);

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
        }

        public bool CerrarInversionProyectoNuevo(string Id, string CodigoOI, string CodigoProyecto, string DescripcionOI)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "CerrarInversionProyectoNuevo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalle", Id);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoOI", CodigoOI);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdEstado", EstadosMovimiento.Aprobado);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoProyecto",CodigoProyecto);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@DescripcionOI", DescripcionOI);
                cmd.Parameters.Add(par1);

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
        }

        public bool ActualizarCodigoProyecto(string Id, string CodigoProyecto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "ActualizarCodigoProyecto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudInversion", Id);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoProyecto", CodigoProyecto);
                cmd.Parameters.Add(par1);

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
        }
        public bool GuardarEnEstadoAprobado(string Id, string CodigoOI, string CodigoProyecto, string DescripcionOI)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            bool Resultado = false;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();

                string nombreProcedure = "GuardarEnEstadoAprobado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalle", Id);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoOI", CodigoOI);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoProyecto", CodigoProyecto);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@DescripcionOI", DescripcionOI);
                cmd.Parameters.Add(par1);

                if (cmd.ExecuteNonQuery() > 0)
                    Resultado = true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Resultado;
        }
        public RSolicitudInversionBE BuscarCodigoInversion(string codigoInversion)
        {
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            
            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("CodigoProyecto", codigoInversion, "=").ToList().FirstOrDefault();

            if (oInversion != null)
            {
                #region CargarCabecera

                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                oSolicitudInversion.FlagPlanBase = oInversion.Parts.FlagPlanBase;
                oSolicitudInversion.Id = oInversion.Id;

                if (((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content != null)
                {
                    oSolicitudInversion.IdCeCo = new RCentroCostoBE();
                    ScriptorContent itemCeco = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content;
                    oSolicitudInversion.IdCeCo.Id = itemCeco.Id;
                    oSolicitudInversion.IdCeCo.Codigo = itemCeco.Parts.CodCECO;
                    oSolicitudInversion.IdCeCo.Descripcion = itemCeco.Parts.DescCECO;

                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdAPIInicial).Content != null)
                {
                    oSolicitudInversion.IdAPIInicial = new RSolicitudInversionBE();
                    oSolicitudInversion.IdAPIInicial.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdAPIInicial).Content.Id;
                    //oSolicitudInversion.IdAPIInicial.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdAPIInicial).Content.Id;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
                {
                    oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                    oSolicitudInversion.IdCoordinador.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id;
                    oSolicitudInversion.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.CuentaRed;
                    oSolicitudInversion.IdCoordinador.Nombre = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.Nombre;
                }
                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudInversion.IdEstado = new REstadoBE();
                    oSolicitudInversion.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                    oSolicitudInversion.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                {
                    oSolicitudInversion.IdMacroServicio = new RMacroservicioBE();
                    oSolicitudInversion.IdMacroServicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                    oSolicitudInversion.IdMacroServicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                    oSolicitudInversion.IdMacroServicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                {
                    oSolicitudInversion.IdSector = new RSectorBE();
                    oSolicitudInversion.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                    oSolicitudInversion.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                    oSolicitudInversion.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudInversion.IdSociedad = new RSociedadBE();
                    oSolicitudInversion.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                    oSolicitudInversion.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudInversion.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                {
                    oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                    oSolicitudInversion.IdTipoAPI.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();
                    oSolicitudInversion.IdTipoAPI.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoAPI.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                {
                    oSolicitudInversion.IdTipoCapex = new RTipoCapexBE();
                    oSolicitudInversion.IdTipoCapex.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id;
                    oSolicitudInversion.IdTipoCapex.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoCapex.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;
                }

                
                oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;                
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
                    oSolicitudInversion.IdPeriodoPRI = new RPeriodoPRIBE();
                    int idPeriodo = oInversion.Parts.IdPeriodoPRI;                    
                    oSolicitudInversion.IdPeriodoPRI.codigo = idPeriodo;
                }

                oSolicitudInversion.Responsable = new UsuarioBE();
                oSolicitudInversion.Responsable.CuentaUsuario = oInversion.Parts.Responsable;

                oSolicitudInversion.ResponsableProyecto = new UsuarioBE();
                oSolicitudInversion.ResponsableProyecto.CuentaUsuario = oInversion.Parts.ResponsableProyecto;

                oSolicitudInversion.TIR = oInversion.Parts.Tir;
                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                oSolicitudInversion.UsuarioCreador = new UsuarioBE();
                oSolicitudInversion.UsuarioCreador.CuentaUsuario = oInversion.Parts.UsuarioCreador;

                oSolicitudInversion.UsuarioModifico = new UsuarioBE();
                oSolicitudInversion.UsuarioModifico.CuentaUsuario = oInversion.Parts.UsuarioModifico;

                oSolicitudInversion.VAN = oInversion.Parts.Van;

                #endregion

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                RDetalleSolicitudInversionBE oDetalle;


                foreach(ScriptorContent item in oDetalleInversion)
                {
                    oDetalle = new RDetalleSolicitudInversionBE();

                    oDetalle.Cantidad = item.Parts.Cantidad;
                    oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada;
                    oDetalle.IdSociedad = item.Parts.IdSociedad;
                    oDetalle.IdTipoActivo = item.Parts.IdTipoActivo;
                    oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                    oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                    oDetalle.VidaUtil = item.Parts.VidaUtil;

                    oListaDetalle.Add(oDetalle);
                    
                }
                oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;

                return oSolicitudInversion;
            }
            else
            {
                //Buscar por Orden de Invesion
                oInversion = null;
                ScriptorContent oInversionTmp = canalInversion.QueryContents("CodigoOI", codigoInversion, "=").ToList().FirstOrDefault();
                
                ScriptorContent oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdInversion", oInversionTmp.Id, "=").ToList().FirstOrDefault();

                oInversion = ((ScriptorContentInsert)oDetalleInversion.Parts.IdSolicitudInversion).Content;

                oSolicitudInversion = null;
                
                #region CargarCabecera
                
                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                oSolicitudInversion.FlagPlanBase = oInversion.Parts.FlagPlanBase;
                oSolicitudInversion.Id = oInversion.Id;
                oSolicitudInversion.IdAPIInicial = oInversion.Parts.IdAPIInicial;
                oSolicitudInversion.IdCoordinador = oInversion.Parts.IdCoordinador;
                oSolicitudInversion.IdEstado = oInversion.Parts.IdEstado;
                oSolicitudInversion.IdMacroServicio = oInversion.Parts.IdMacroServicio;
                oSolicitudInversion.IdSector = oInversion.Parts.IdSector;
                oSolicitudInversion.IdSociedad = oInversion.Parts.IdSociedad;
                oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                oSolicitudInversion.IdTipoAPI = oInversion.Parts.IdTipoAPI;
                oSolicitudInversion.IdTipoCapex = oInversion.Parts.IdTipoCapex;
                oSolicitudInversion.Marca = oInversion.Parts.Marca;
                oSolicitudInversion.MontoTotal = oInversion.Parts.MontoTotal;
                oSolicitudInversion.NombreCortoSolicitud = oInversion.Parts.NombreCortoSolicitud;
                oSolicitudInversion.NombreProyecto = oInversion.Parts.NombreProyecto;
                oSolicitudInversion.NumSolicitud = oInversion.Parts.NumSolicitud;
                oSolicitudInversion.Observaciones = oInversion.Parts.Observaciones;
                oSolicitudInversion.ObservacionesFinancieras = oInversion.Parts.ObservacionesFinancieras;
                oSolicitudInversion.PRI = oInversion.Parts.Pri;
                oSolicitudInversion.Responsable = oInversion.Parts.Responsable;
                oSolicitudInversion.ResponsableProyecto = oInversion.Parts.ResponsableProyecto;
                oSolicitudInversion.TIR = oInversion.Parts.Tir;
                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;
                oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;
                oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;
                oSolicitudInversion.VAN = oInversion.Parts.Van;

                #endregion

                List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                RDetalleSolicitudInversionBE oDetalle;

                oDetalle = new RDetalleSolicitudInversionBE();

                oDetalle.Cantidad = oDetalleInversion.Parts.Cantidad;
                oDetalle.IdMonedaCotizada = oDetalleInversion.Parts.IdMonedaCotizada;
                oDetalle.IdSociedad = oDetalleInversion.Parts.IdSociedad;
                oDetalle.IdTipoActivo = oDetalleInversion.Parts.IdTipoActivo;
                oDetalle.PrecioUnitario = oDetalleInversion.Parts.PrecioUnitario;
                oDetalle.PrecioUnitarioUSD = oDetalleInversion.Parts.PrecioUnitarioUSD;
                oDetalle.VidaUtil = oDetalleInversion.Parts.VidaUtil;

                oListaDetalle.Add(oDetalle);

                oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;
                

            }

            return oSolicitudInversion;
 
        }

        //public ListarSolicitudInversionDTO BuscarCodigoInversionDTO(string codigoInversion, string cuenta)
        //{
        //    ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

        //    ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
        //    ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
        //    ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
        //    ScriptorChannel canalCoordinadores = new ScriptorClient().GetChannel(new Guid(Canales.Coordinador));
        //    ScriptorContent oInversionTmp = null;
        //    ScriptorContent oInversion = null;

        //    List<ScriptorContent> oInversionaux = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CodigoProyecto", codigoInversion, "=").QueryContents("fk_workflowstate", "published","=").Where(x => x.Parts.IdTipoAPI.Value.ToString().ToLower() == TiposAPI.IdNuevoProyecto.ToLower()).ToList();
        //    if (oInversionaux.Count == 0)
        //    {
        //        oInversionTmp = canalInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CodigoOI", codigoInversion, "=").QueryContents("fk_workflowstate", "published","=").Where(x=>x.Parts.Descripcion.ToString().Contains("A")).ToList().FirstOrDefault();
        //        if(oInversionTmp == null)
        //        { 
        //            oSolicitudInversion.Mensaje = "El Código proyecto/OI no se puede ampliar. No existe un API tipo Proyecto Nuevo asociado.";
        //            oSolicitudInversion.idMensaje = 1;
        //            return oSolicitudInversion;
        //        }
        //    }
        //    else
        //    {
        //        oInversion = oInversionaux.Where(x => x.Parts.IdEstado.Value.ToString().ToLower() == "eccd738c-86f4-44e5-ab92-689d3fa3abaa").ToList().FirstOrDefault();
        //    }            

        //    if (oInversion != null)
        //    {
        //        #region CargarCabecera

        //        string IdCeco = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Id.ToString().ToLower();

        //        //ScriptorContent coordinador = canalCoordinadores.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CuentaRed", cuenta, "=").Where(x => x.Parts.IdCeCo.Value.ToString().ToLower() == IdCeco).OrderByDescending(c => c.Parts.Version).ToList().FirstOrDefault();

        //        RCoordinadorDAL coordinador = new RCoordinadorDAL();
        //        RCoordinadorBE oExisteCeco = coordinador.ValidatExisteCeCoUltimaVersion(new Guid(IdCeco));
        //        RCoordinadorBE oCordinador = coordinador.ObtenerCoordinadorPorCuentaPorCodigoCeCo(new Guid(IdCeco), cuenta);
        //        if (oExisteCeco.Id == null)
        //        {
        //            oSolicitudInversion.Mensaje = "El ceco ya no existe y no se puede ampliar.";
        //            oSolicitudInversion.idMensaje = 3;
        //        }
        //        else
        //        {
        //            if (oCordinador.Id != null)
        //            {

        //                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
        //                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
        //                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
        //                //oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
        //                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
        //                //oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
        //                oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
        //                //oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
        //                //oSolicitudInversion.Id = oInversion.Id.ToString();

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content != null)
        //                {

        //                    //oSolicitudInversion.IdCeCo = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Id.ToString();
        //                    //oSolicitudInversion.CodCeCo = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Parts.CodCECO;
        //                    //oSolicitudInversion.DescCeCo = oSolicitudInversion.CodCeCo + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Parts.DescCECO;
        //                    oSolicitudInversion.IdCeCo = oCordinador.IdCeCo;
        //                    oSolicitudInversion.CodCeCo = oCordinador.CodCeCo;
        //                    oSolicitudInversion.DescCeCo = oCordinador.CodCeCo + " - " + oCordinador.DescCeCo;

        //                }

        //                //if (((ScriptorDropdownListValue)oInversion.Parts.IdAPIInicial).Content != null)
        //                //{
        //                oSolicitudInversion.IdAPIInicial = oInversion.Id.ToString();
        //                //}

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
        //                {
        //                    oSolicitudInversion.IdCoordinador = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id.ToString();
        //                }
        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
        //                {
        //                    oSolicitudInversion.IdEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id.ToString();
        //                    oSolicitudInversion.DescEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;

        //                }

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
        //                {
        //                    oSolicitudInversion.IdMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id.ToString();
        //                    oSolicitudInversion.DescMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
        //                }

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
        //                {
        //                    oSolicitudInversion.IdSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id.ToString();
        //                    oSolicitudInversion.DescSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
        //                }

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
        //                {
        //                    oSolicitudInversion.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();
        //                    oSolicitudInversion.CodSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
        //                    oSolicitudInversion.DescSociedad = oSolicitudInversion.CodSociedad + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
        //                }

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
        //                {
        //                    oSolicitudInversion.IdTipoAPI = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();

        //                }

        //                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
        //                {
        //                    oSolicitudInversion.IdTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id.ToString();
        //                    oSolicitudInversion.DescTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;

        //                    if (oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ahorro.ToUpper() || oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ingreso.ToUpper())
        //                    {
        //                        oSolicitudInversion.FlagEvaluacionInversion = 1;
        //                    }
        //                    else
        //                    {
        //                        oSolicitudInversion.FlagEvaluacionInversion = 0;
        //                    }

        //                }

        //                oSolicitudInversion.Marca = oInversion.Parts.Marca;
        //                oSolicitudInversion.MontoTotal = oInversion.Parts.MontoTotal;
        //                oSolicitudInversion.NombreCortoSolicitud = oInversion.Parts.NombreCortoSolicitud;
        //                oSolicitudInversion.NombreProyecto = oInversion.Parts.NombreProyecto;
        //                //oSolicitudInversion.NumSolicitud = oInversion.Parts.NumSolicitud;
        //                oSolicitudInversion.Observaciones = oInversion.Parts.Observaciones;
        //                oSolicitudInversion.ObservacionesFinancieras = oInversion.Parts.ObservacionesFinancieras;
        //                oSolicitudInversion.PRI = oInversion.Parts.Pri;
        //                if (oInversion.Parts.IdPeriodoPRI != null)
        //                {

        //                    if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRI.ToString()))
        //                    {
        //                        oSolicitudInversion.IdPeriodoPRI = 0;
        //                    }
        //                    oSolicitudInversion.IdPeriodoPRI = oInversion.Parts.IdPeriodoPRI;
        //                }
        //                oSolicitudInversion.Responsable = oInversion.Parts.Responsable;
        //                oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);

        //                oSolicitudInversion.ResponsableProyecto = oInversion.Parts.ResponsableProyecto;
        //                oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);

        //                oSolicitudInversion.TIR = oInversion.Parts.Tir;
        //                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

        //                oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;

        //                oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;

        //                oSolicitudInversion.VAN = oInversion.Parts.Van;

        //                RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();

        //                oSolicitudInversion.IdMonedaSociedad = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).IdMoneda;
        //                oSolicitudInversion.DescMoneda = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).Moneda;
        //        #endregion
        //            }
        //            else
        //            {
        //                oSolicitudInversion.Mensaje = "El usuario no es coordinador del Centro de Costo asociado al API.";
        //                oSolicitudInversion.idMensaje = 2;
        //            }
        //        }

        //        oSolicitudInversion.IdEstado = "";
        //        oSolicitudInversion.DescEstado = "";

        //        return oSolicitudInversion;
        //    }
        //    else
        //    {
        //        //Buscar por Orden de Invesion
        //        oInversion = null;
                
        //        if (oInversionTmp != null)
        //        {
        //            ScriptorContent oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdInversion", oInversionTmp.Id, "=").ToList().FirstOrDefault();

        //            oInversion = ((ScriptorDropdownListValue)oDetalleInversion.Parts.IdSolicitudInversion).Content;

        //            string IdCeco = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Id.ToString().ToLower();

        //            //ScriptorContent coordinador = canalCoordinadores.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CuentaRed", cuenta, "=").Where(x => x.Parts.IdCeCo.Value.ToString().ToLower() == IdCeco).OrderByDescending(c => c.Parts.Version).ToList().FirstOrDefault();
        //            RCoordinadorDAL coordinador = new RCoordinadorDAL();
        //            RCoordinadorBE oExisteCeco = coordinador.ValidatExisteCeCoUltimaVersion(new Guid(IdCeco));
        //            RCoordinadorBE oCordinador = coordinador.ObtenerCoordinadorPorCuentaPorCodigoCeCo(new Guid(IdCeco), cuenta);
        //            if (oExisteCeco.Id == null)
        //            {
        //                oSolicitudInversion.Mensaje = "El ceco ya no existe y no se puede ampliar.";
        //                oSolicitudInversion.idMensaje = 3;
        //            }
        //            else
        //            {
        //                if (oCordinador.Id != null)
        //                {
        //                    //oSolicitudInversion = null;

        //                    #region CargarCabecera

        //                    oSolicitudInversion.CodigoProyecto = ((ScriptorDropdownListValue)oDetalleInversion.Parts.IdSolicitudInversion).Content.Parts.CodigoProyecto;
        //                    oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
        //                    oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
        //                    //oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
        //                    oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
        //                    //oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
        //                    oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
        //                    //oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
        //                    //oSolicitudInversion.Id = oInversion.Id.ToString();

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content != null)
        //                    {
        //                        //oSolicitudInversion.IdCeCo = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Id.ToString();
        //                        //oSolicitudInversion.IdCeCo = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Id.ToString();
        //                        //oSolicitudInversion.CodCeCo = ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Parts.CodCECO;
        //                        //oSolicitudInversion.DescCeCo = oSolicitudInversion.CodCeCo + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdCeCo).Content.Parts.DescCECO;
        //                        oSolicitudInversion.IdCeCo = oCordinador.IdCeCo;
        //                        oSolicitudInversion.CodCeCo = oCordinador.CodCeCo;
        //                        oSolicitudInversion.DescCeCo = oCordinador.CodCeCo + " - " + oCordinador.DescCeCo;

        //                    }

        //                    //if (((ScriptorDropdownListValue)oInversion.Parts.IdAPIInicial).Content != null)
        //                    //{
        //                    oSolicitudInversion.IdAPIInicial = oInversion.Id.ToString();
        //                    //}

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
        //                    {
        //                        oSolicitudInversion.IdCoordinador = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id.ToString();
        //                    }
        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
        //                    {
        //                        oSolicitudInversion.IdEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id.ToString();
        //                        oSolicitudInversion.DescEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;

        //                    }

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
        //                    {
        //                        oSolicitudInversion.IdMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id.ToString();
        //                        oSolicitudInversion.DescMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
        //                    }

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
        //                    {
        //                        oSolicitudInversion.IdSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id.ToString();
        //                        oSolicitudInversion.DescSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
        //                    }

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
        //                    {
        //                        oSolicitudInversion.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();
        //                        oSolicitudInversion.CodSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
        //                        oSolicitudInversion.DescSociedad = oSolicitudInversion.CodSociedad + " - " + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
        //                    }

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
        //                    {
        //                        oSolicitudInversion.IdTipoAPI = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();

        //                    }

        //                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
        //                    {
        //                        oSolicitudInversion.IdTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id.ToString();
        //                        oSolicitudInversion.DescTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;

        //                        if (oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ahorro.ToUpper() || oSolicitudInversion.IdTipoCapex.ToUpper() == TiposCapex.Ingreso.ToUpper())
        //                        {
        //                            oSolicitudInversion.FlagEvaluacionInversion = 1;
        //                        }
        //                        else
        //                        {
        //                            oSolicitudInversion.FlagEvaluacionInversion = 0;
        //                        }
        //                    }


        //                    oSolicitudInversion.Marca = oInversion.Parts.Marca;
        //                    oSolicitudInversion.MontoTotal = oInversion.Parts.MontoTotal;
        //                    oSolicitudInversion.NombreCortoSolicitud = oInversion.Parts.NombreCortoSolicitud;
        //                    oSolicitudInversion.NombreProyecto = oInversion.Parts.NombreProyecto;
        //                    //oSolicitudInversion.NumSolicitud = oInversion.Parts.NumSolicitud;
        //                    oSolicitudInversion.Observaciones = oInversion.Parts.Observaciones;
        //                    oSolicitudInversion.ObservacionesFinancieras = oInversion.Parts.ObservacionesFinancieras;
        //                    oSolicitudInversion.PRI = oInversion.Parts.Pri;
        //                    if (oInversion.Parts.IdPeriodoPRI != null)
        //                    {

        //                        if (String.IsNullOrEmpty(oInversion.Parts.IdPeriodoPRI.ToString()))
        //                        {
        //                            oSolicitudInversion.IdPeriodoPRI = 0;
        //                        }
        //                        oSolicitudInversion.IdPeriodoPRI = oInversion.Parts.IdPeriodoPRI;
        //                    }
        //                    oSolicitudInversion.Responsable = oInversion.Parts.Responsable;
        //                    oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);

        //                    oSolicitudInversion.ResponsableProyecto = oInversion.Parts.ResponsableProyecto;
        //                    oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);

        //                    oSolicitudInversion.TIR = oInversion.Parts.Tir;
        //                    oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

        //                    oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;

        //                    oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;

        //                    oSolicitudInversion.VAN = oInversion.Parts.Van;
        //                    RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();

        //                    oSolicitudInversion.IdMonedaSociedad = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).IdMoneda;
        //                    oSolicitudInversion.DescMoneda = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).Moneda;
        //                    #endregion
        //                }
        //                else
        //                {
        //                    oSolicitudInversion.Mensaje = "El usuario no es coordinador del Centro de Costo asociado al API";
        //                    oSolicitudInversion.idMensaje = 2;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            oSolicitudInversion.Mensaje = "El Código proyecto/OI no se puede ampliar. La API asociada no se encuentra en estado Cerrado";
        //            oSolicitudInversion.idMensaje = 1;
        //        }
        //    }

        //    oSolicitudInversion.IdEstado = "";
        //    oSolicitudInversion.DescEstado = "";
        //    return oSolicitudInversion;

        //}

        public ListarSolicitudInversionDTO BuscarCodigoInversionDTO(string codigoInversion, string cuenta)
        {
            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            //if (ValidarAmpliacionEnEstadoPendiente(codigoInversion) == 0)
            //{
                string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

                SqlConnection con = new SqlConnection();
                try
                {
                    con.ConnectionString = connectionString;
                    con.Open();
                    string nombreProcedure = "BuscarProyecto_OIparaAmpliacion";
                    SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter par1;
                    if (codigoInversion == "")
                        par1 = new SqlParameter("@Codigo", DBNull.Value);
                    else
                        par1 = new SqlParameter("@Codigo", codigoInversion);
                    cmd.Parameters.Add(par1);

                    using (IDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                       
                                string IdCeco = dataReader["IdCeCo"].ToString();
                                RCoordinadorDAL coordinador = new RCoordinadorDAL();
                                RCoordinadorBE oExisteCeco = coordinador.ValidatExisteCeCoUltimaVersion(new Guid(IdCeco));
                                RCoordinadorBE oCordinador = coordinador.ObtenerCoordinadorPorCuentaPorCodigoCeCo(new Guid(IdCeco), cuenta);
                                if (oExisteCeco.Id == null)
                                {
                                    oSolicitudInversion.idMensaje = 3;
                                    return oSolicitudInversion;
                                }
                                else
                                {
                                    if (oCordinador.Id != null)
                                    {
                                        oSolicitudInversion.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                                        oSolicitudInversion.Descripcion= dataReader["Descripcion"].ToString();
                                        if (!Convert.IsDBNull(dataReader["FechaInicio"]))
                                        {
                                            oSolicitudInversion.FechaInicio = Convert.ToDateTime(dataReader["FechaInicio"]);  
                                        }
                                        if (!Convert.IsDBNull(dataReader["FechaCierre"]))
                                        {
                                            oSolicitudInversion.FechaCierre = Convert.ToDateTime(dataReader["FechaCierre"]);
                                        }                                        
                                        oSolicitudInversion.FlagPlanBase=Convert.ToInt32(dataReader["FlagPlanBase"]);
                                        oSolicitudInversion.IdCeCo=dataReader["IdCeCo"].ToString();
                                        oSolicitudInversion.CodCeCo=dataReader["CodCeCo"].ToString();
                                        oSolicitudInversion.DescCeCo=dataReader["DescCeCo"].ToString();
                                        oSolicitudInversion.IdAPIInicial=dataReader["IdAPIInicial"].ToString();
                                        oSolicitudInversion.IdCoordinador = dataReader["IdCoordinador"].ToString();
                                        oSolicitudInversion.IdEstado="";
                                        oSolicitudInversion.DescEstado="";
                                        oSolicitudInversion.IdMacroServicio=dataReader["IdMacroservicio"].ToString();
                                        oSolicitudInversion.DescMacroServicio=dataReader["DescMacroservicio"].ToString();
                                        oSolicitudInversion.IdSector=dataReader["IdSector"].ToString();
                                        oSolicitudInversion.DescSector=dataReader["DescSector"].ToString();
                                        oSolicitudInversion.IdSociedad=dataReader["IdSociedad"].ToString();
                                        oSolicitudInversion.CodSociedad=dataReader["CodSociedad"].ToString();
                                        oSolicitudInversion.DescSociedad=dataReader["DescSociedad"].ToString();
                                        oSolicitudInversion.IdTipoAPI=dataReader["IdTipoAPI"].ToString();
                                        oSolicitudInversion.IdTipoCapex=dataReader["IdTipoCapex"].ToString();
                                        oSolicitudInversion.DescTipoCapex=dataReader["DescTipoCapex"].ToString();
                                        oSolicitudInversion.FlagEvaluacionInversion=Convert.ToInt32(dataReader["FlagEvaluacionInversion"]);
                                        oSolicitudInversion.Marca=dataReader["Marca"].ToString();
                                        oSolicitudInversion.MontoTotal=Convert.ToDouble(dataReader["MontoTotal"]);
                                        oSolicitudInversion.NombreCortoSolicitud=dataReader["NombreCortoSolicitud"].ToString();
	                                    oSolicitudInversion.NombreProyecto=dataReader["NombreProyecto"].ToString();
                                        oSolicitudInversion.Observaciones=dataReader["Observaciones"].ToString();
                                        oSolicitudInversion.ObservacionesFinancieras=dataReader["ObservacionesFinancieras"].ToString();
                                        oSolicitudInversion.PRI=Convert.ToDouble(dataReader["PRI"]);
	                                    oSolicitudInversion.IdPeriodoPRI=Convert.ToInt32(dataReader["IdPeriodoPRI"]);
                                        oSolicitudInversion.Responsable=dataReader["Responsable"].ToString();
                                        oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);
                                        oSolicitudInversion.ResponsableProyecto = dataReader["ResponsableProyecto"].ToString();
                                        oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);
                                        oSolicitudInversion.TIR=Convert.ToDouble(dataReader["TIR"]);
                                        oSolicitudInversion.Ubicacion=dataReader["Ubicacion"].ToString();
                                        oSolicitudInversion.UsuarioCreador=dataReader["UsuarioCreador"].ToString();
                                        oSolicitudInversion.UsuarioModifico=dataReader["UsuarioModifico"].ToString();
                                        oSolicitudInversion.VAN=Convert.ToDouble(dataReader["VAN"]);

                                        RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();
                                        oSolicitudInversion.IdMonedaSociedad = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).IdMoneda;
                                        oSolicitudInversion.DescMoneda = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).Moneda;
                                    }
                                    else
                                    {
                                        oSolicitudInversion.idMensaje = 2;
                                        return oSolicitudInversion;
                                    }
                                }
                            
                        
                        }
                        else
                        {
                            oSolicitudInversion.idMensaje = 1;
                            return oSolicitudInversion;
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
            //}
            //else
            //{
            //    oSolicitudInversion.idMensaje = 4;
            //    return oSolicitudInversion;
            //}
            return oSolicitudInversion;
        }

        public ListarSolicitudInversionDTO BuscarCodigoInversionDTOPB(string codigoInversion, string cuenta)
        {
            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            //if (ValidarAmpliacionEnEstadoPendiente(codigoInversion) == 0)
            //{
                string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

                SqlConnection con = new SqlConnection();
                try
                {
                    con.ConnectionString = connectionString;
                    con.Open();
                    string nombreProcedure = "APIBuscarOIPlanBase";
                    SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter par1;
                    if (codigoInversion == "")
                        par1 = new SqlParameter("@CodigoInversion", DBNull.Value);
                    else
                        par1 = new SqlParameter("@CodigoInversion", codigoInversion);
                    cmd.Parameters.Add(par1);

                    using (IDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {

                            string IdCeco = dataReader["IdCeCo"].ToString();
                            RCoordinadorDAL coordinador = new RCoordinadorDAL();
                            RCoordinadorBE oExisteCeco = coordinador.ValidatExisteCeCoUltimaVersion(new Guid(IdCeco));
                            RCoordinadorBE oCordinador = coordinador.ObtenerCoordinadorPorCuentaPorCodigoCeCo(new Guid(IdCeco), cuenta);
                            if (oExisteCeco.Id == null)
                            {
                                oSolicitudInversion.idMensaje = 3; //ceco no existe
                                return oSolicitudInversion;
                            }
                            else
                            {
                                if (oCordinador.Id != null)
                                {
                                    oSolicitudInversion.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                                    //oSolicitudInversion.Descripcion = dataReader["Descripcion"].ToString();
                                    //oSolicitudInversion.FechaInicio = Convert.ToDateTime(dataReader["FechaInicio"]);
                                    //oSolicitudInversion.FechaCierre = Convert.ToDateTime(dataReader["FechaCierre"]);
                                    //oSolicitudInversion.FlagPlanBase = Convert.ToInt32(dataReader["FlagPlanBase"]);
                                    oSolicitudInversion.IdCeCo = dataReader["IdCeCo"].ToString();
                                    oSolicitudInversion.CodCeCo = dataReader["CodCeCo"].ToString();
                                    oSolicitudInversion.DescCeCo = dataReader["DescCeCo"].ToString();
                                    //oSolicitudInversion.IdAPIInicial = dataReader["IdAPIInicial"].ToString();
                                    //oSolicitudInversion.IdCoordinador = dataReader["IdCoordinador"].ToString();
                                    //oSolicitudInversion.IdEstado = dataReader["IdEstado"].ToString();
                                    //oSolicitudInversion.DescEstado = dataReader["DescEstado"].ToString();
                                    oSolicitudInversion.IdMacroServicio = dataReader["IdMacroservicio"].ToString();
                                    oSolicitudInversion.DescMacroServicio = dataReader["DescMacroservicio"].ToString();
                                    oSolicitudInversion.IdSector = dataReader["IdSector"].ToString();
                                    oSolicitudInversion.DescSector = dataReader["DescSector"].ToString();
                                    oSolicitudInversion.IdSociedad = dataReader["IdSociedad"].ToString();
                                    oSolicitudInversion.CodSociedad = dataReader["CodSociedad"].ToString();
                                    oSolicitudInversion.DescSociedad = dataReader["DescSociedad"].ToString();
                                    //oSolicitudInversion.IdTipoAPI = dataReader["IdTipoAPI"].ToString();
                                    oSolicitudInversion.IdTipoCapex = dataReader["IdTipoCapex"].ToString().ToLower();
                                    //oSolicitudInversion.DescTipoCapex = dataReader["DescTipoCapex"].ToString();
                                    //oSolicitudInversion.FlagEvaluacionInversion = Convert.ToInt32(dataReader["FlagEvaluacionInversion"]);
                                    //oSolicitudInversion.Marca = dataReader["Marca"].ToString();
                                    //oSolicitudInversion.MontoTotal = Convert.ToDouble(dataReader["MontoTotal"]);
                                    //oSolicitudInversion.NombreCortoSolicitud = dataReader["NombreCortoSolicitud"].ToString();
                                    oSolicitudInversion.NombreProyecto = dataReader["NombreProyecto"].ToString();
                                    //oSolicitudInversion.Observaciones = dataReader["Observaciones"].ToString();
                                    //oSolicitudInversion.ObservacionesFinancieras = dataReader["ObservacionesFinancieras"].ToString();
                                    //oSolicitudInversion.PRI = Convert.ToDouble(dataReader["PRI"]);
                                    //oSolicitudInversion.IdPeriodoPRI = Convert.ToInt32(dataReader["IdPeriodoPRI"]);
                                    //oSolicitudInversion.Responsable = dataReader["Responsable"].ToString();
                                    //oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);
                                    //oSolicitudInversion.ResponsableProyecto = dataReader["ResponsableProyecto"].ToString();
                                    //oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);
                                    //oSolicitudInversion.TIR = Convert.ToDouble(dataReader["TIR"]);
                                    //oSolicitudInversion.Ubicacion = dataReader["Ubicacion"].ToString();
                                    //oSolicitudInversion.UsuarioCreador = dataReader["UsuarioCreador"].ToString();
                                    //oSolicitudInversion.UsuarioModifico = dataReader["UsuarioModifico"].ToString();
                                    //oSolicitudInversion.VAN = Convert.ToDouble(dataReader["VAN"]);
                                    oSolicitudInversion.FlagTipoBolsa = Convert.ToInt32(dataReader["TipoBolsa"]);
                                    oSolicitudInversion.MontoAprobadoPlanBase = Convert.ToInt32(dataReader["MontoAprobadoPlanBase"]);
                                    RTipoCambioDAL MonedaSociedad = new RTipoCambioDAL();
                                    oSolicitudInversion.IdMonedaSociedad = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).IdMoneda;
                                    oSolicitudInversion.DescMoneda = MonedaSociedad.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad).Moneda;
                                }
                                else
                                {
                                    oSolicitudInversion.idMensaje = 2; //No e coordinador
                                    return oSolicitudInversion;
                                }
                            }


                        }
                        else
                        {
                            oSolicitudInversion.idMensaje = 1; //No existe
                            return oSolicitudInversion;
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
            //}
            //else
            //{
            //    oSolicitudInversion.idMensaje = 5; //Hay ampliaciones pendientes
            //}
            return oSolicitudInversion;
        }

        public List<ListarDetalleSolicitudInversionDTO> BuscarDetalleCodigoInversion(string codigoInversion, string Id)
        {
            List<ListarDetalleSolicitudInversionDTO> oLista = new List<ListarDetalleSolicitudInversionDTO>();
            ListarDetalleSolicitudInversionDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleCodigoInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;
                if (codigoInversion == "")
                    par1 = new SqlParameter("@Codigo", DBNull.Value);
                else
                    par1 = new SqlParameter("@Codigo", codigoInversion);
                cmd.Parameters.Add(par1);

                if (Id == "")
                    par1 = new SqlParameter("@Id", DBNull.Value);
                else
                    par1 = new SqlParameter("@Id", Id);
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
                        //oSolicitudInversionBE.IdInversion = dataReader["IdInversion"].ToString();
                        oSolicitudInversionBE.IdMonedaCotizada = dataReader["IdMonedaCotizada"].ToString();
                        //oSolicitudInversionBE.IdSolicitudInversion = dataReader["IdSolicitudInversion"].ToString();
                        oSolicitudInversionBE.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oSolicitudInversionBE.MontoAmpliarMonedaCotizada = Convert.ToDouble(dataReader["MontoAmpliarMonedaCotizada"]);
                        oSolicitudInversionBE.MontoAmpliarUSD = Convert.ToDouble(dataReader["MontoAmpliarUSD"]);
                        oSolicitudInversionBE.NombreMoneda = dataReader["NombreMoneda"].ToString();
                        oSolicitudInversionBE.PrecioMontoOrigen = Convert.ToDouble(dataReader["PrecioMontoOrigen"]);
                        oSolicitudInversionBE.PrecioMontoUSD = Convert.ToDouble(dataReader["PrecioMontoUSD"]);
                        oSolicitudInversionBE.PrecioUnitario = Convert.ToDouble(dataReader["PrecioUnitario"]);
                        oSolicitudInversionBE.PrecioUnitarioUSD = Convert.ToDouble(dataReader["PrecioUnitarioUSD"]);
                        oSolicitudInversionBE.VidaUtil = Convert.ToInt32(dataReader["VidaUtil"]);
                        oSolicitudInversionBE.VidaUtilAmpliar = Convert.ToDouble(dataReader["VidaUtilAmpliar"]);
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

        public List<ListarDetalleSolicitudInversionDTO> BuscarDetalleCodigoInversionPB(string codigoInversion, string Id)
        {
            List<ListarDetalleSolicitudInversionDTO> oLista = new List<ListarDetalleSolicitudInversionDTO>();
            ListarDetalleSolicitudInversionDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIObtenerDetallePlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;
                if (codigoInversion == "")
                    par1 = new SqlParameter("@CodigoInversion", DBNull.Value);
                else
                    par1 = new SqlParameter("@CodigoInversion", codigoInversion);
                cmd.Parameters.Add(par1);

                if (Id == "")
                    par1 = new SqlParameter("@Id", DBNull.Value);
                else
                    par1 = new SqlParameter("@Id", Id);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarDetalleSolicitudInversionDTO();
                        //oSolicitudInversionBE.Cantidad = Convert.ToInt32(dataReader["Cantidad"]);
                        //oSolicitudInversionBE.DescTipoActivo = dataReader["DescTipoActivo"].ToString();
                        //oSolicitudInversionBE.IdMonedaCotizada = dataReader["IdMonedaCotizada"].ToString();
                        //oSolicitudInversionBE.PrecioMontoUSD = Convert.ToDouble(dataReader["PptoAprobadoOI"]);
                        //oSolicitudInversionBE.VidaUtil = Convert.ToInt32(dataReader["VidaUtil"]);
                        //oSolicitudInversionBE.codOI = dataReader["NumeroOI"].ToString();
                        //oSolicitudInversionBE.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        //oSolicitudInversionBE.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oSolicitudInversionBE.Cantidad = Convert.ToInt32(dataReader["Cantidad"]);
                        oSolicitudInversionBE.codOI = dataReader["codOI"].ToString();
                        oSolicitudInversionBE.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oSolicitudInversionBE.DescTipoActivo = dataReader["DescTipoActivo"].ToString();
                        oSolicitudInversionBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        //oSolicitudInversionBE.IdInversion = dataReader["IdInversion"].ToString();
                        oSolicitudInversionBE.IdMonedaCotizada = dataReader["IdMonedaCotizada"].ToString();
                        //oSolicitudInversionBE.IdSolicitudInversion = dataReader["IdSolicitudInversion"].ToString();
                        oSolicitudInversionBE.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oSolicitudInversionBE.MontoAmpliarMonedaCotizada = Convert.ToDouble(dataReader["MontoAmpliarMonedaCotizada"]);
                        oSolicitudInversionBE.MontoAmpliarUSD = Convert.ToDouble(dataReader["MontoAmpliarUSD"]);
                        oSolicitudInversionBE.NombreMoneda = dataReader["NombreMoneda"].ToString();
                        oSolicitudInversionBE.PrecioMontoOrigen = Convert.ToDouble(dataReader["PrecioMontoOrigen"]);
                        oSolicitudInversionBE.PrecioMontoUSD = Convert.ToDouble(dataReader["PrecioMontoUSD"]);
                        oSolicitudInversionBE.PrecioUnitario = Convert.ToDouble(dataReader["PrecioUnitario"]);
                        oSolicitudInversionBE.PrecioUnitarioUSD = Convert.ToDouble(dataReader["PrecioUnitarioUSD"]);
                        oSolicitudInversionBE.VidaUtil = Convert.ToInt32(dataReader["VidaUtil"]);
                        oSolicitudInversionBE.VidaUtilAmpliar = Convert.ToDouble(dataReader["VidaUtilAmpliar"]);
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
        
        /*public List<ListarDetalleSolicitudInversionDTO> BuscarDetalleCodigoInversion(string codigoInversion, string Id)
        {

            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            ScriptorChannel canalInversion = new ScriptorClient().GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorContent oInversion=null;
            if (codigoInversion != "")
            {
                oInversion = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CodigoProyecto", codigoInversion, "=").Where(c => c.StateDescription == "Publicado").Where(x => x.Parts.IdTipoAPI.Value.ToString().ToLower() == TiposAPI.IdNuevoProyecto.ToLower()).ToList().FirstOrDefault();
            }

            else if(Id!="")
            { 
                oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(Id), "=").ToList().FirstOrDefault();
            }
            
            if (oInversion != null)
            {
               
                //List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").Where(x => x.StateDescription == "Publicado").ToList();

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();
                oDetalleInversion = oDetalleInversion.Where(x => x.Parts.IdSolicitudInversion.Value.ToString().ToLower() == oInversion.Id.ToString().ToLower()).ToList();

                List<ListarDetalleSolicitudInversionDTO> oListaDetalle = new List<ListarDetalleSolicitudInversionDTO>();
                ListarDetalleSolicitudInversionDTO oDetalle;


                foreach (ScriptorContent item in oDetalleInversion)
                {

                    oDetalle = new ListarDetalleSolicitudInversionDTO();

                    oDetalle.Id = codigoInversion != "" ? null : item.Id.ToString();
                    oDetalle.Cantidad = item.Parts.Cantidad;
                    
                    //oDetalle.IdMoneda = item.Parts.IdMoneda.Value;
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
                    

                    //if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    //{
                    //    oDetalle.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();

                    //}

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
            else
            {
                //Buscar por Orden de Invesion
                oInversion = null;
                ScriptorContent oInversionTmp = canalInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CodigoOI", codigoInversion, "=").Where(x=>x.Parts.Descripcion.ToString().Contains("A")).ToList().FirstOrDefault();

                if (oInversionTmp != null)
                {
                    ScriptorContent item = canalDetalleSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("IdInversion", oInversionTmp.Id, "=").ToList().FirstOrDefault();

                    List<ListarDetalleSolicitudInversionDTO> oListaDetalle = new List<ListarDetalleSolicitudInversionDTO>();
                    ListarDetalleSolicitudInversionDTO oDetalle;

                    oDetalle = new ListarDetalleSolicitudInversionDTO();

                    oDetalle.Id = null;
                    oDetalle.Cantidad = item.Parts.Cantidad;
                    oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada.Value;
                    oDetalle.NombreMoneda = item.Parts.IdMonedaCotizada.Title;
                    oDetalle.codOI=oInversionTmp.Parts.CodigoOI;
                    oDetalle.DescripcionOI = oInversionTmp.Parts.DescripcionOI;

                    //if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    //{
                    //    oDetalle.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();

                    //}

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

                    return oListaDetalle;

                }
                else
                    return null;
                

            }
                       
 
        }*/

        public bool ExisteSolicitudInversion(string NroApi, Guid idTipoActivo, double Monto)
        {
            bool resultado = false;
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            List<ScriptorContent> oListaSolicitudInversion = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                                    .QueryContents("NumSolicitud", NroApi, "=").ToList();
            if (oListaSolicitudInversion.Count > 0)
            {
                ScriptorContent oInversion = oListaSolicitudInversion[0];
                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=")
                                                                                                       .QueryContents("IdTipoActivo", idTipoActivo, "=")
                                                                                                       .QueryContents("Monto", Monto, "=").ToList();
                List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                if (oDetalleInversion.Count > 0)
                {
                    resultado = true;
                }
                oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;
            }
            return resultado;
        }
        public RDetalleSolicitudInversionBE ExisteSolicitudInversionBD(string NroApi, Guid idTipoActivo, double Monto)
        {
            RDetalleSolicitudInversionBE DetalleSolicitudBE = new RDetalleSolicitudInversionBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarSolicitudPor_Activo_Monto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@NroAPI", NroApi);
                cmd.Parameters.Add(par1);
                SqlParameter par2 = new SqlParameter("@IdTipoActivo", idTipoActivo);
                cmd.Parameters.Add(par2);
                SqlParameter par3 = new SqlParameter("@Monto", Monto);
                cmd.Parameters.Add(par3);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadDetalleSolicitudInversion(DetalleSolicitudBE, dataReader);
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
            return DetalleSolicitudBE;
        }
        public RSolicitudInversionBE ObtenerSolicitudPorNroAPI(string NroApi)
        {
            RSolicitudInversionBE resultado = new RSolicitudInversionBE();
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            List<ScriptorContent> oListaSolicitudInversion = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                                    .QueryContents("NumSolicitud", NroApi, "=").ToList();
            if (oListaSolicitudInversion.Count > 0)
            {
                ScriptorContent oInversion = oListaSolicitudInversion[0];
            }
            return resultado;
        }
        public RSolicitudInversionBE ObtenerSolicitudPorNroAPIBD(string NroApi)
        {
            RSolicitudInversionBE oSolicitudInversionBE = new RSolicitudInversionBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudPorNroApi";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@NroAPI", NroApi);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadSolicitudInversion(oSolicitudInversionBE, dataReader);
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
            return oSolicitudInversionBE;
        }

        /*public RSolicitudInversionBE BuscarPorId(string IdSolicitud)
        {

            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", new Guid(IdSolicitud), "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                //if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                //{
                #region CargarCabecera

                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                
                oSolicitudInversion.Id = oInversion.Id;

                if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                {
                    oSolicitudInversion.IdCeCo = new RCentroCostoBE();
                    ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                    oSolicitudInversion.IdCeCo.Id = contenidoCeCo.Id;
                    oSolicitudInversion.IdCeCo.Codigo = contenidoCeCo.Parts.CodCECO;
                    oSolicitudInversion.IdCeCo.Descripcion = contenidoCeCo.Parts.DescCECO;
                }
                if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                {
                    oSolicitudInversion.IdAPIInicial = new RSolicitudInversionBE();
                    oSolicitudInversion.IdAPIInicial.Id = oInversion.Parts.IdAPIInicial.ToString();
                }

                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    oSolicitudInversion.IdCoordinador.Id = contenidoCoordinador.Id;
                    oSolicitudInversion.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                    oSolicitudInversion.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                }
                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudInversion.IdEstado = new REstadoBE();
                    oSolicitudInversion.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                    oSolicitudInversion.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                {
                    oSolicitudInversion.IdMacroServicio = new RMacroservicioBE();
                    oSolicitudInversion.IdMacroServicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                    oSolicitudInversion.IdMacroServicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                    oSolicitudInversion.IdMacroServicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                {
                    oSolicitudInversion.IdSector = new RSectorBE();
                    oSolicitudInversion.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                    oSolicitudInversion.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                    oSolicitudInversion.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudInversion.IdSociedad = new RSociedadBE();
                    oSolicitudInversion.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                    oSolicitudInversion.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudInversion.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                {
                    oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                    oSolicitudInversion.IdTipoAPI.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();
                    oSolicitudInversion.IdTipoAPI.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoAPI.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                {
                    oSolicitudInversion.IdTipoCapex = new RTipoCapexBE();
                    oSolicitudInversion.IdTipoCapex.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id;
                    oSolicitudInversion.IdTipoCapex.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoCapex.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;
                    oSolicitudInversion.IdTipoCapex.IdentificadorFlujo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.IdentificadorFlujo;
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
                    oSolicitudInversion.IdPeriodoPRI = new RPeriodoPRIBE();
                    int idPeriodo = oInversion.Parts.IdPeriodoPRI;

                    oSolicitudInversion.IdPeriodoPRI.codigo = idPeriodo;
                }
                oSolicitudInversion.Responsable = new UsuarioBE();
                oSolicitudInversion.Responsable.CuentaUsuario = oInversion.Parts.Responsable;

                oSolicitudInversion.ResponsableProyecto = new UsuarioBE();
                oSolicitudInversion.ResponsableProyecto.CuentaUsuario = oInversion.Parts.ResponsableProyecto;

                oSolicitudInversion.TIR = oInversion.Parts.Tir;
                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                oSolicitudInversion.UsuarioCreador = new UsuarioBE();
                oSolicitudInversion.UsuarioCreador.CuentaUsuario = oInversion.Parts.UsuarioCreador;

                oSolicitudInversion.UsuarioModifico = new UsuarioBE();
                oSolicitudInversion.UsuarioModifico.CuentaUsuario = oInversion.Parts.UsuarioModifico;

                oSolicitudInversion.VAN = oInversion.Parts.Van;
                oSolicitudInversion.MontoAAmpliar = oInversion.Parts.MontoAAmpliar;

                oSolicitudInversion.VANAmpliacion = oInversion.Parts.VanAmpliacion;
                oSolicitudInversion.TIRAmpliacion = oInversion.Parts.TirAmpliacion;
                oSolicitudInversion.PRIAmpliacion = oInversion.Parts.PriAmpliacion;

                if (oInversion.Parts.IdPeriodoPRIAmpliacion != null)
                {
                    oSolicitudInversion.IdPeriodoPRIAmpliacion = new RPeriodoPRIBE();
                    int idPeriodo = oInversion.Parts.IdPeriodoPRIAmpliacion;

                    oSolicitudInversion.IdPeriodoPRIAmpliacion.codigo = idPeriodo;
                }

                #endregion

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").QueryContents("fk_workflowstate", "published", "=").ToList();

                List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                RDetalleSolicitudInversionBE oDetalle;


                foreach (ScriptorContent item in oDetalleInversion)
                {
                    oDetalle = new RDetalleSolicitudInversionBE();

                    oDetalle.Cantidad = item.Parts.Cantidad;
                    oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada.Value;

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oDetalle.IdSociedad = new RSociedadBE();
                        oDetalle.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                        oDetalle.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                        oDetalle.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                    {
                        oDetalle.IdTipoActivo = new RTipoActivoBE();
                        oDetalle.IdTipoActivo.Id = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id;
                        oDetalle.IdTipoActivo.Codigo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Codigo;
                        oDetalle.IdTipoActivo.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                        oDetalle.IdTipoActivo.IdentificadorFlujo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.IdentificadorFlujo;
                    }


                    oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                    oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                    oDetalle.VidaUtil = item.Parts.VidaUtil;
                    oDetalle.PrecioMontoUSD = item.Parts.PrecioMontoUSD;
                    oDetalle.MontoAAmpliarUSD = item.Parts.MontoAmpliarUSD;

                    if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                    {
                        oDetalle.IdInversion = new RInversionBE();
                        oDetalle.IdInversion.Id = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id.ToString();
                        oDetalle.FlagAmpliacion = Convert.ToInt32(((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.FlagAmpliacion);

                    }

                    oListaDetalle.Add(oDetalle);

                }
                oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;


                return oSolicitudInversion;

            }
            else
            {
                return null;
            }

        }*/

        public RSolicitudInversionBE BuscarPorId(string IdSolicitud, string NumSolicitud)
        {
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIObtenerSolicitudInversionPor_Id_NumSolicitud";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;

                if(String.IsNullOrEmpty(IdSolicitud))
                    par1 = new SqlParameter("@Id", DBNull.Value);
                else
                    par1 = new SqlParameter("@Id", IdSolicitud);
                cmd.Parameters.Add(par1);

                if(String.IsNullOrEmpty(NumSolicitud))
                    par1 = new SqlParameter("@NumSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NumSolicitud", NumSolicitud);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        #region CargarCabecera

                        if(!Convert.IsDBNull(dataReader["CodigoProyecto"]))
                            oSolicitudInversion.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        if(!Convert.IsDBNull(dataReader["Descripcion"]))
                             oSolicitudInversion.Descripcion = dataReader["Descripcion"].ToString();
                        if(!Convert.IsDBNull(dataReader["FechaCierre"]))                       
                         oSolicitudInversion.FechaCierre = Convert.ToDateTime(dataReader["FechaCierre"]);
                        if(!Convert.IsDBNull(dataReader["FechaCreacion"])) 
                            oSolicitudInversion.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        if(!Convert.IsDBNull(dataReader["FechaInicio"])) 
                            oSolicitudInversion.FechaInicio = Convert.ToDateTime(dataReader["FechaInicio"]);
                        if(!Convert.IsDBNull(dataReader["FechaModificacion"])) 
                            oSolicitudInversion.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        if(!Convert.IsDBNull(dataReader["FlagPlanBase"]))
                            oSolicitudInversion.FlagPlanBase = int.Parse(dataReader["FlagPlanBase"].ToString());
                        if(!Convert.IsDBNull(dataReader["IdInstancia"]))
                            oSolicitudInversion.IdInstancia = int.Parse(dataReader["IdInstancia"].ToString());

                        oSolicitudInversion.Id = new Guid(dataReader["Id"].ToString());

                        if (!Convert.IsDBNull(dataReader["IdCeCo"]))
                        {
                            oSolicitudInversion.IdCeCo = new RCentroCostoBE();
                            oSolicitudInversion.IdCeCo.Id = new Guid(dataReader["IdCeCo"].ToString());
                            oSolicitudInversion.IdCeCo.Codigo = dataReader["CodCECO"].ToString();
                            oSolicitudInversion.IdCeCo.Descripcion = dataReader["DescCECO"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdAPIInicial"]))
                        {
                            oSolicitudInversion.IdAPIInicial = new RSolicitudInversionBE();
                            oSolicitudInversion.IdAPIInicial.Id = new Guid(dataReader["IdAPIInicial"].ToString());
                        }

                        if (!Convert.IsDBNull(dataReader["IdCoordinador"]))
                        {
                            oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                            oSolicitudInversion.IdCoordinador.Id = new Guid(dataReader["IdCoordinador"].ToString());
                            oSolicitudInversion.IdCoordinador.Nombre = dataReader["Nombre"].ToString();
                            oSolicitudInversion.IdCoordinador.CuentaRed = dataReader["CuentaRed"].ToString();
                        }
                        if (!Convert.IsDBNull(dataReader["IdEstado"]))
                        {
                            oSolicitudInversion.IdEstado = new REstadoBE();
                            oSolicitudInversion.IdEstado.Id = new Guid(dataReader["IdEstado"].ToString());
                            oSolicitudInversion.IdEstado.Descripcion = dataReader["DescEstado"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdMacroservicio"]))
                        {
                            oSolicitudInversion.IdMacroServicio = new RMacroservicioBE();
                            oSolicitudInversion.IdMacroServicio.Id = new Guid(dataReader["IdMacroservicio"].ToString());
                            oSolicitudInversion.IdMacroServicio.Descripcion = dataReader["DescMacroservicio"].ToString();
                            oSolicitudInversion.IdMacroServicio.Codigo = dataReader["CodMacroservicio"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdSector"]))
                        {
                            oSolicitudInversion.IdSector = new RSectorBE();
                            oSolicitudInversion.IdSector.Id = new Guid(dataReader["IdSector"].ToString());
                            oSolicitudInversion.IdSector.Codigo = dataReader["CodSector"].ToString();
                            oSolicitudInversion.IdSector.Descripcion = dataReader["DescSector"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdSociedad"]))
                        {
                            oSolicitudInversion.IdSociedad = new RSociedadBE();
                            oSolicitudInversion.IdSociedad.Id = new Guid(dataReader["IdSociedad"].ToString());
                            oSolicitudInversion.IdSociedad.Codigo = dataReader["CodSociedad"].ToString();
                            oSolicitudInversion.IdSociedad.Descripcion = dataReader["DescSociedad"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdTipoAPI"]))
                        {
                            oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                            oSolicitudInversion.IdTipoAPI.Id = dataReader["IdTipoAPI"].ToString();
                            oSolicitudInversion.IdTipoAPI.Codigo = dataReader["CodTipoAPI"].ToString();
                            oSolicitudInversion.IdTipoAPI.Descripcion = dataReader["DescTipoAPI"].ToString();
                        }

                        if (!Convert.IsDBNull(dataReader["IdTipoCapex"]))
                        {
                            oSolicitudInversion.IdTipoCapex = new RTipoCapexBE();
                            oSolicitudInversion.IdTipoCapex.Id = new Guid(dataReader["IdTipoCapex"].ToString());;
                            oSolicitudInversion.IdTipoCapex.Codigo = dataReader["CodTipoCapex"].ToString();
                            oSolicitudInversion.IdTipoCapex.Descripcion = dataReader["DescTipoCapex"].ToString();
                            oSolicitudInversion.IdTipoCapex.IdentificadorFlujo = dataReader["IdentificadorFlujo"].ToString();
                        }

                        if(!Convert.IsDBNull(dataReader["Marca"]))
                            oSolicitudInversion.Marca = dataReader["Marca"].ToString();
                        if(!Convert.IsDBNull(dataReader["MontoTotal"]))
                            oSolicitudInversion.MontoTotal = Convert.ToDouble(dataReader["MontoTotal"]);
                        if(!Convert.IsDBNull(dataReader["NombreCortoSolicitud"]))
                            oSolicitudInversion.NombreCortoSolicitud = dataReader["NombreCortoSolicitud"].ToString();
                        if(!Convert.IsDBNull(dataReader["NombreProyecto"]))
                            oSolicitudInversion.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        if(!Convert.IsDBNull(dataReader["NumSolicitud"]))
                            oSolicitudInversion.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        if(!Convert.IsDBNull(dataReader["Observaciones"]))
                            oSolicitudInversion.Observaciones = dataReader["Observaciones"].ToString();
                        if(!Convert.IsDBNull(dataReader["ObservacionesFinancieras"]))
                            oSolicitudInversion.ObservacionesFinancieras = dataReader["ObservacionesFinancieras"].ToString();
                        if(!Convert.IsDBNull(dataReader["PRI"]))
                            oSolicitudInversion.PRI = Convert.ToDouble(dataReader["PRI"]);
                        if(!Convert.IsDBNull(dataReader["IdPeriodoPRI"]))
                        {
                            oSolicitudInversion.IdPeriodoPRI = new RPeriodoPRIBE();
                            oSolicitudInversion.IdPeriodoPRI.codigo = int.Parse(dataReader["IdPeriodoPRI"].ToString());
                        }
                        if(!Convert.IsDBNull(dataReader["Responsable"]))
                        {
                            oSolicitudInversion.Responsable = new UsuarioBE();
                            oSolicitudInversion.Responsable.CuentaUsuario = dataReader["Responsable"].ToString();
                        }
                        if(!Convert.IsDBNull(dataReader["ResponsableProyecto"]))
                        {
                            oSolicitudInversion.ResponsableProyecto = new UsuarioBE();
                            oSolicitudInversion.ResponsableProyecto.CuentaUsuario = dataReader["ResponsableProyecto"].ToString();
                        }                        
                        if(!Convert.IsDBNull(dataReader["TIR"]))
                            oSolicitudInversion.TIR = Convert.ToDouble(dataReader["TIR"]);
                        if(!Convert.IsDBNull(dataReader["Ubicacion"]))
                            oSolicitudInversion.Ubicacion = dataReader["Ubicacion"].ToString();

                        if(!Convert.IsDBNull(dataReader["UsuarioCreador"]))
                        {
                            oSolicitudInversion.UsuarioCreador = new UsuarioBE();
                            oSolicitudInversion.UsuarioCreador.CuentaUsuario = dataReader["UsuarioCreador"].ToString();
                        }
                        if(!Convert.IsDBNull(dataReader["UsuarioModifico"]))
                        {
                            oSolicitudInversion.UsuarioModifico = new UsuarioBE();
                            oSolicitudInversion.UsuarioModifico.CuentaUsuario = dataReader["UsuarioModifico"].ToString();
                        }

                        if(!Convert.IsDBNull(dataReader["VAN"]))
                            oSolicitudInversion.VAN = Convert.ToDouble(dataReader["VAN"]);
                        if(!Convert.IsDBNull(dataReader["MontoAAmpliar"]))
                            oSolicitudInversion.MontoAAmpliar = Convert.ToDouble(dataReader["MontoAAmpliar"].ToString());
                        if(!Convert.IsDBNull(dataReader["VANAmpliacion"]))
                            oSolicitudInversion.VANAmpliacion = Convert.ToDouble(dataReader["VANAmpliacion"].ToString());
                        if(!Convert.IsDBNull(dataReader["TIRAmpliacion"]))
                            oSolicitudInversion.TIRAmpliacion = Convert.ToDouble(dataReader["TIRAmpliacion"].ToString());
                        if(!Convert.IsDBNull(dataReader["PRIAmpliacion"]))
                            oSolicitudInversion.PRIAmpliacion = Convert.ToDouble(dataReader["PRIAmpliacion"].ToString());
                        if(!Convert.IsDBNull(dataReader["IdPeriodoPRIAmpliacion"]))
                        {
                            oSolicitudInversion.IdPeriodoPRIAmpliacion = new RPeriodoPRIBE();
                            oSolicitudInversion.IdPeriodoPRIAmpliacion.codigo = int.Parse(dataReader["IdPeriodoPRIAmpliacion"].ToString());
                        }
                        
                        #endregion

                        oSolicitudInversion.DetalleSolicitudInversion = ObtenerDetalleSolicitudInversionPorId(oSolicitudInversion);
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
            return oSolicitudInversion;
            

        }

        public List<RDetalleSolicitudInversionBE> ObtenerDetalleSolicitudInversionPorId(RSolicitudInversionBE oSolicitudInversion)
        {
            List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleSolicitudInversionPorId";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudInversion", oSolicitudInversion.Id);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RDetalleSolicitudInversionBE oDetalle = new RDetalleSolicitudInversionBE();

                        oDetalle.Cantidad = Convert.ToInt32(dataReader["Cantidad"]);
                        oDetalle.IdMonedaCotizada = dataReader["IdMonedaCotizada"].ToString();

                        if (oSolicitudInversion.IdSociedad != null)
                        {
                            oDetalle.IdSociedad = new RSociedadBE();
                            oDetalle.IdSociedad.Id = oSolicitudInversion.IdSociedad.Id;
                            oDetalle.IdSociedad.Codigo = oSolicitudInversion.IdSociedad.Codigo;
                            oDetalle.IdSociedad.Descripcion = oSolicitudInversion.IdSociedad.Descripcion;
                        }

                        oDetalle.IdTipoActivo = new RTipoActivoBE();
                        oDetalle.IdTipoActivo.Id = new Guid(dataReader["IdTipoActivo"].ToString());
                        oDetalle.IdTipoActivo.Codigo = dataReader["CodTipoActivo"].ToString();
                        oDetalle.IdTipoActivo.Descripcion = dataReader["DescTipoActivo"].ToString();
                        oDetalle.IdTipoActivo.IdentificadorFlujo = dataReader["IdentificadorFlujo"].ToString();

                        if (!Convert.IsDBNull(dataReader["PrecioUnitario"]))
                            oDetalle.PrecioUnitario = Convert.ToDouble(dataReader["PrecioUnitario"]);
                        if (!Convert.IsDBNull(dataReader["PrecioUnitarioUSD"]))
                            oDetalle.PrecioUnitarioUSD = Convert.ToDouble(dataReader["PrecioUnitarioUSD"]);
                        if (!Convert.IsDBNull(dataReader["VidaUtil"]))
                            oDetalle.VidaUtil = Convert.ToInt32(dataReader["VidaUtil"]);
                        if (!Convert.IsDBNull(dataReader["PrecioMontoUSD"]))
                            oDetalle.PrecioMontoUSD = Convert.ToDouble(dataReader["PrecioMontoUSD"]);
                        if (!Convert.IsDBNull(dataReader["MontoAmpliarUSD"]))
                            oDetalle.MontoAAmpliarUSD = Convert.ToDouble(dataReader["MontoAmpliarUSD"]);

                        oDetalle.IdInversion = new RInversionBE();
                        oDetalle.IdInversion.Id = dataReader["IdInversion"].ToString();
                        
                        if (!Convert.IsDBNull(dataReader["FlagAmpliacion"]))
                            oDetalle.FlagAmpliacion = Convert.ToInt32(dataReader["FlagAmpliacion"]);                        

                        oListaDetalle.Add(oDetalle);

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
            return oListaDetalle;
        }
            
        public RSolicitudInversionBE BuscarPorNumSolicitud(string NumSolicitud)
        {

            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                //if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                //{
                #region CargarCabecera

                oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                oSolicitudInversion.Id = oInversion.Id;

                if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                {
                    oSolicitudInversion.IdCeCo = new RCentroCostoBE();
                    ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                    oSolicitudInversion.IdCeCo.Id = contenidoCeCo.Id;
                    oSolicitudInversion.IdCeCo.Codigo = contenidoCeCo.Parts.CodCECO;
                    oSolicitudInversion.IdCeCo.Descripcion = contenidoCeCo.Parts.DescCECO;
                }

                if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                {
                    oSolicitudInversion.IdAPIInicial = new RSolicitudInversionBE();
                    oSolicitudInversion.IdAPIInicial.Id = oInversion.Parts.IdAPIInicial.ToString();
                }
                if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
                {
                    oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                    oSolicitudInversion.IdCoordinador.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id;
                    oSolicitudInversion.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.CuentaRed;
                    oSolicitudInversion.IdCoordinador.Nombre = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.Nombre;
                }

                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    oSolicitudInversion.IdCoordinador.Id = contenidoCoordinador.Id;
                    oSolicitudInversion.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.Nombre;
                    oSolicitudInversion.IdCoordinador.Nombre = contenidoCoordinador.Parts.CuentaRed;
                }
                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudInversion.IdEstado = new REstadoBE();
                    oSolicitudInversion.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                    oSolicitudInversion.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                {
                    oSolicitudInversion.IdMacroServicio = new RMacroservicioBE();
                    oSolicitudInversion.IdMacroServicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                    oSolicitudInversion.IdMacroServicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                    oSolicitudInversion.IdMacroServicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                {
                    oSolicitudInversion.IdSector = new RSectorBE();
                    oSolicitudInversion.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                    oSolicitudInversion.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                    oSolicitudInversion.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudInversion.IdSociedad = new RSociedadBE();
                    oSolicitudInversion.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                    oSolicitudInversion.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudInversion.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                {
                    oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                    oSolicitudInversion.IdTipoAPI.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();
                    oSolicitudInversion.IdTipoAPI.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoAPI.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                {
                    oSolicitudInversion.IdTipoCapex = new RTipoCapexBE();
                    oSolicitudInversion.IdTipoCapex.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id;
                    oSolicitudInversion.IdTipoCapex.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Codigo;
                    oSolicitudInversion.IdTipoCapex.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;
                    oSolicitudInversion.IdTipoCapex.IdentificadorFlujo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.IdentificadorFlujo;
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
                    oSolicitudInversion.IdPeriodoPRI = new RPeriodoPRIBE();
                    int idPeriodo = oInversion.Parts.IdPeriodoPRI;

                    oSolicitudInversion.IdPeriodoPRI.codigo = idPeriodo;
                }
                oSolicitudInversion.Responsable = new UsuarioBE();
                oSolicitudInversion.Responsable.CuentaUsuario = oInversion.Parts.Responsable;

                oSolicitudInversion.ResponsableProyecto = new UsuarioBE();
                oSolicitudInversion.ResponsableProyecto.CuentaUsuario = oInversion.Parts.ResponsableProyecto;

                oSolicitudInversion.TIR = oInversion.Parts.Tir;
                oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                oSolicitudInversion.UsuarioCreador = new UsuarioBE();
                oSolicitudInversion.UsuarioCreador.CuentaUsuario = oInversion.Parts.UsuarioCreador;

                oSolicitudInversion.UsuarioModifico = new UsuarioBE();
                oSolicitudInversion.UsuarioModifico.CuentaUsuario = oInversion.Parts.UsuarioModifico;

                oSolicitudInversion.VAN = oInversion.Parts.Van;
                oSolicitudInversion.MontoAAmpliar = oInversion.Parts.MontoAAmpliar;

                oSolicitudInversion.VANAmpliacion = oInversion.Parts.VanAmpliacion;
                oSolicitudInversion.TIRAmpliacion = oInversion.Parts.TirAmpliacion;
                oSolicitudInversion.PRIAmpliacion = oInversion.Parts.PriAmpliacion;

                if (oInversion.Parts.IdPeriodoPRIAmpliacion != null)
                {
                    oSolicitudInversion.IdPeriodoPRIAmpliacion = new RPeriodoPRIBE();
                    int idPeriodo = oInversion.Parts.IdPeriodoPRIAmpliacion;

                    oSolicitudInversion.IdPeriodoPRIAmpliacion.codigo = idPeriodo;
                }


                #endregion

                List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                RDetalleSolicitudInversionBE oDetalle;


                foreach (ScriptorContent item in oDetalleInversion)
                {
                    oDetalle = new RDetalleSolicitudInversionBE();

                    oDetalle.Cantidad = item.Parts.Cantidad;
                    oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada.Value;

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oDetalle.IdSociedad = new RSociedadBE();
                        oDetalle.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                        oDetalle.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                        oDetalle.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                    {
                        oDetalle.IdTipoActivo = new RTipoActivoBE();
                        oDetalle.IdTipoActivo.Id = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id;
                        oDetalle.IdTipoActivo.Codigo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Codigo;
                        oDetalle.IdTipoActivo.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                        oDetalle.IdTipoActivo.IdentificadorFlujo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.IdentificadorFlujo;
                    }


                    oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                    oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                    oDetalle.VidaUtil = item.Parts.VidaUtil;
                    

                    if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                    {
                        oDetalle.IdInversion = new RInversionBE();
                        oDetalle.IdInversion.Id = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id.ToString();
                        oDetalle.FlagAmpliacion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.FlagAmpliacion;

                    }

                    oListaDetalle.Add(oDetalle);

                }
                oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;


                return oSolicitudInversion;

            }
            else
            {
                return null;
            }

        }

        public RSolicitudInversionBE BuscarPorNumSolicitudBD(string NumSolicitud, string TipoAPI)
        {
            RSolicitudInversionBE oSolicitudInversionBE = new RSolicitudInversionBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            /*
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "BuscarPorNumSolicitudBD";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@NroAPI", NroApi);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        LlenarEntidadSolicitudInversion(oSolicitudInversionBE, dataReader);
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
             */
            return oSolicitudInversionBE;
 
        }

        public RSolicitudInversionBE BuscarPorIdSolicitud(string IdSolicitud, string TipoAPI)
        {

            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));


            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                {
                    #region CargarCabecera

                    oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                    oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                    oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                    oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                    oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                    oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                    oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                    oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                    oSolicitudInversion.Id = oInversion.Id;

                    if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                    {
                        oSolicitudInversion.IdCeCo = new RCentroCostoBE();
                        ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                        oSolicitudInversion.IdCeCo.Id = contenidoCeCo.Id;
                        oSolicitudInversion.IdCeCo.Codigo = contenidoCeCo.Parts.CodCECO;
                        oSolicitudInversion.IdCeCo.Descripcion = contenidoCeCo.Parts.DescCECO;
                    }

                    if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                    {
                        oSolicitudInversion.IdAPIInicial = new RSolicitudInversionBE();
                        oSolicitudInversion.IdAPIInicial.Id = oInversion.Parts.IdAPIInicial.ToString();
                    }
                    
                    if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                    {
                        oSolicitudInversion.IdCoordinador = new RCoordinadorBE();
                        ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                        oSolicitudInversion.IdCoordinador.Id = contenidoCoordinador.Id;
                        oSolicitudInversion.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                        oSolicitudInversion.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                    {
                        oSolicitudInversion.IdEstado = new REstadoBE();
                        oSolicitudInversion.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                        oSolicitudInversion.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                    {
                        oSolicitudInversion.IdMacroServicio = new RMacroservicioBE();
                        oSolicitudInversion.IdMacroServicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                        oSolicitudInversion.IdMacroServicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                        oSolicitudInversion.IdMacroServicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                    {
                        oSolicitudInversion.IdSector = new RSectorBE();
                        oSolicitudInversion.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                        oSolicitudInversion.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                        oSolicitudInversion.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oSolicitudInversion.IdSociedad = new RSociedadBE();
                        oSolicitudInversion.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                        oSolicitudInversion.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                        oSolicitudInversion.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                    {
                        oSolicitudInversion.IdTipoAPI = new RTipoAPIBE();
                        oSolicitudInversion.IdTipoAPI.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();
                        oSolicitudInversion.IdTipoAPI.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Codigo;
                        oSolicitudInversion.IdTipoAPI.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                    {
                        oSolicitudInversion.IdTipoCapex = new RTipoCapexBE();
                        oSolicitudInversion.IdTipoCapex.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id;
                        oSolicitudInversion.IdTipoCapex.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Codigo;
                        oSolicitudInversion.IdTipoCapex.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;
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
                        oSolicitudInversion.IdPeriodoPRI = new RPeriodoPRIBE();
                        int idPeriodo = oInversion.Parts.IdPeriodoPRI;

                        oSolicitudInversion.IdPeriodoPRI.codigo = idPeriodo;
                    }
                    oSolicitudInversion.Responsable = new UsuarioBE();
                    oSolicitudInversion.Responsable.CuentaUsuario = oInversion.Parts.Responsable;

                    oSolicitudInversion.ResponsableProyecto = new UsuarioBE();
                    oSolicitudInversion.ResponsableProyecto.CuentaUsuario = oInversion.Parts.ResponsableProyecto;

                    oSolicitudInversion.TIR = oInversion.Parts.Tir;
                    oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                    oSolicitudInversion.UsuarioCreador = new UsuarioBE();
                    oSolicitudInversion.UsuarioCreador.CuentaUsuario = oInversion.Parts.UsuarioCreador;

                    oSolicitudInversion.UsuarioModifico = new UsuarioBE();
                    oSolicitudInversion.UsuarioModifico.CuentaUsuario = oInversion.Parts.UsuarioModifico;

                    oSolicitudInversion.VAN = oInversion.Parts.Van;

                    #endregion

                    List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                    List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                    RDetalleSolicitudInversionBE oDetalle;


                    foreach (ScriptorContent item in oDetalleInversion)
                    {
                        oDetalle = new RDetalleSolicitudInversionBE();

                        oDetalle.Cantidad = item.Parts.Cantidad;
                        oDetalle.IdMonedaCotizada = item.Parts.IdMonedaCotizada.Value;

                        if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                        {
                            oDetalle.IdSociedad = new RSociedadBE();
                            oDetalle.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                            oDetalle.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                            oDetalle.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                        }

                        if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                        {
                            oDetalle.IdTipoActivo = new RTipoActivoBE();
                            oDetalle.IdTipoActivo.Id = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id;
                            oDetalle.IdTipoActivo.Codigo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Codigo;
                            oDetalle.IdTipoActivo.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                        }


                        oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                        oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                        oDetalle.VidaUtil = item.Parts.VidaUtil;

                        if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                        {
                            oDetalle.IdInversion = new RInversionBE();
                            oDetalle.IdInversion.Id = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id.ToString();

                        }

                        oListaDetalle.Add(oDetalle);

                    }
                    oSolicitudInversion.DetalleSolicitudInversion = oListaDetalle;


                    return oSolicitudInversion;
                }
            }
            else
            {
                return null;
            }

            return oSolicitudInversion;

        }

        public ListarSolicitudInversionDTO BuscarPorNumSolicitudDTO(string IdSolicitud, string TipoAPI)
        {

            ListarSolicitudInversionDTO oSolicitudInversion = new ListarSolicitudInversionDTO();

            ScriptorChannel canalSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCoordinador = new ScriptorClient().GetChannel(new Guid(Canales.Coordinador));
            ScriptorChannel canalCeCo = new ScriptorClient().GetChannel(new Guid(Canales.MaestroCeCo));


            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();

            RTipoCambioDAL oTipoCambioDAL = new RTipoCambioDAL();

            if (oInversion != null)
            {
                if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                {
                    #region CargarCabecera

                    oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                    oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                    oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                    oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                    oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                    oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                    oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                    oSolicitudInversion.IdInstancia = oInversion.Parts.IdInstancia;
                    oSolicitudInversion.Id = oInversion.Id.ToString();

                    if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                    {
                        ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                        oSolicitudInversion.IdCeCo = contenidoCeCo.Id.ToString();
                        oSolicitudInversion.DescCeCo = contenidoCeCo.Parts.CodCECO + " " + contenidoCeCo.Parts.DescCECO;
                    }

                    if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                    {
                        oSolicitudInversion.IdAPIInicial = oInversion.Parts.IdAPIInicial.ToString();
                    }

                    if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                    {
                        ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
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
                        oSolicitudInversion.IdMacroServicio =  ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id.ToString();
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
                        oSolicitudInversion.DescSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo + ' ' + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                        RTipoCambioBE oTipoCambioBE = new RTipoCambioBE();
                        oTipoCambioBE = oTipoCambioDAL.ObtenerTipoCambioPorSociedad(oSolicitudInversion.IdSociedad);

                        oSolicitudInversion.DescMoneda = oTipoCambioBE.Moneda;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                    {
                        oSolicitudInversion.IdTipoAPI = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Id.ToString();
                        
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                    {
                        oSolicitudInversion.IdTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Id.ToString();
                        oSolicitudInversion.DescTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion.ToString();
                        
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
                        oSolicitudInversion.IdPeriodoPRI = oInversion.Parts.IdPeriodoPRI;
                    }
                    oSolicitudInversion.Responsable =  oInversion.Parts.Responsable;
                    oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);

                    oSolicitudInversion.ResponsableProyecto =  oInversion.Parts.ResponsableProyecto;
                    oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);

                    oSolicitudInversion.TIR = oInversion.Parts.Tir;
                    oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                    oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;

                    oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;

                    oSolicitudInversion.VAN = oInversion.Parts.Van;

                    #endregion
                                       
                    return oSolicitudInversion;
                }
            }
            else
            {
                return null;
            }

            return oSolicitudInversion;

        }



        public bool ActualizarInversion(RSolicitudInversionBE oSolicitudInversion)
        {
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalEstadosMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalTipoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoMovimiento));

            bool resultado = false;

            foreach(RDetalleSolicitudInversionBE det in oSolicitudInversion.DetalleSolicitudInversion)
            {
                //Actualizamos registro de inversion
                ScriptorContent inv = canalInversion.QueryContents("#Id", det.IdInversion.Id, "=").ToList().FirstOrDefault();
                inv.Parts.IdCeCo =oSolicitudInversion.IdCeCo.Id.ToString();
                inv.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo.Id, "=").ToList().FirstOrDefault());

                inv.Parts.CodigoProyecto = String.IsNullOrEmpty(oSolicitudInversion.CodigoProyecto) ? "" : oSolicitudInversion.CodigoProyecto;
                inv.Parts.NombreProyecto = String.IsNullOrEmpty(oSolicitudInversion.NombreProyecto) ? "" : oSolicitudInversion.NombreProyecto;
                inv.Parts.MontoDisponible = det.PrecioMontoUSD;
                inv.Parts.MontoContable = det.PrecioMontoUSD;
                inv.Parts.Descripcion = oSolicitudInversion.NumSolicitud;
                inv.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Pendiente, "=").ToList().FirstOrDefault());
                
                resultado = inv.Save();

                //Registramos un movimiento

                ScriptorContent contenido = canalMovimiento.NewContent();

                contenido.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadosMovimiento.QueryContents("#Id", EstadosMovimiento.Pendiente, "=").ToList().FirstOrDefault());
                contenido.Parts.IdTipoMovimiento = ScriptorDropdownListValue.FromContent(canalTipoMovimiento.QueryContents("#Id", TiposMovimiento.API, "=").ToList().FirstOrDefault());
                contenido.Parts.Monto = det.PrecioMontoUSD;
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

       
        /*public List<RDetalleSolicitudInversionBE> BuscarDetallesPorNumSolicitud(string NumSolicitud, string TipoAPI)
        {
            
            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();
                        
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                {   

                    List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                    List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
                    RDetalleSolicitudInversionBE oDetalle;


                    foreach (ScriptorContent item in oDetalleInversion)
                    {
                        oDetalle = new RDetalleSolicitudInversionBE();

                        oDetalle.Cantidad = item.Parts.Cantidad;
                        oDetalle.IdMoneda = item.Parts.IdMoneda.Value;

                        if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                        {
                            oDetalle.IdSociedad = new RSociedadBE();
                            oDetalle.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                            oDetalle.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                            oDetalle.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                        }

                        if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                        {
                            oDetalle.IdTipoActivo = new RTipoActivoBE();
                            oDetalle.IdTipoActivo.Id = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id;
                            oDetalle.IdTipoActivo.Codigo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Codigo;
                            oDetalle.IdTipoActivo.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Parts.Descripcion;
                        }

                        
                        oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                        oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                        oDetalle.VidaUtil = item.Parts.VidaUtil;

                        oListaDetalle.Add(oDetalle);

                    }
                    return oListaDetalle;


                }
            }
            else 
            {
                return null;
            }

            return null;
        }
         */

        /*public List<ListarDetalleSolicitudInversionDTO> BuscarDetallesPorNumSolicitud(string IdSolicitud, string TipoAPI)
        {

            RSolicitudInversionBE oSolicitudInversion = new RSolicitudInversionBE();

            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                {
                    var test = canalDetalleSolicitudInversion.QueryContents("#Id", new Guid(), "<>").ToList().Count;
                    List<ScriptorContent> oDetalleInversion = canalDetalleSolicitudInversion.QueryContents("#fk_workflowstate","published", "=").
                                                                                            QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                    List<ListarDetalleSolicitudInversionDTO> oListaDetalle = new List<ListarDetalleSolicitudInversionDTO>();
                    ListarDetalleSolicitudInversionDTO oDetalle;


                    foreach (ScriptorContent item in oDetalleInversion)
                    {
                        
                        oDetalle = new ListarDetalleSolicitudInversionDTO();

                        oDetalle.Id = item.Id.ToString();
                        oDetalle.Cantidad = item.Parts.Cantidad;                   
                        oDetalle.PrecioUnitario = item.Parts.PrecioUnitario;
                        oDetalle.PrecioUnitarioUSD = item.Parts.PrecioUnitarioUSD;
                        oDetalle.PrecioMontoOrigen = item.Parts.PrecioMontoOrigen;
                        oDetalle.PrecioMontoUSD = item.Parts.PrecioMontoUSD;
                        
                        if (((ScriptorDropdownListValue)item.Parts.IdSolicitudInversion).Content != null)
                            oDetalle.IdSolicitudInversion = ((ScriptorDropdownListValue)item.Parts.IdSolicitudInversion).Content.Id.ToString();
                        
                        if (((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content != null)
                        {
                            
                            oDetalle.IdMonedaCotizada = ((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content.Id.ToString();
                            oDetalle.NombreMoneda = ((ScriptorDropdownListValue)item.Parts.IdMonedaCotizada).Content.Parts.Nombre.ToString();
                        }
                        
                        if (((ScriptorDropdownListValue)item.Parts.IdInversion).Content != null)
                        {
                            oDetalle.IdInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id.ToString();
                            oDetalle.codOI = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.CodigoOI.ToString();
                            oDetalle.DescripcionOI = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Parts.DescripcionOI.ToString();
                        }


                        oDetalle.VidaUtil = item.Parts.VidaUtil;

                        if (((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content != null)
                        {
                            oDetalle.IdTipoActivo = ((ScriptorDropdownListValue)item.Parts.IdTipoActivo).Content.Id.ToString();
                        }

                        oListaDetalle.Add(oDetalle);

                    }
                    return oListaDetalle;


                }
            }
            else
            {
                return null;
            }

            return null;
        }*/

        public List<ListarDetalleSolicitudInversionDTO> BuscarDetallesPorNumSolicitud(string IdSolicitud, string TipoAPI)
        {
            List<ListarDetalleSolicitudInversionDTO> oLista = new List<ListarDetalleSolicitudInversionDTO>();
            ListarDetalleSolicitudInversionDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleSolicitudInversionPorIdSolicitud";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudInversion", IdSolicitud);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdTipoAPI", TipoAPI);
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

        public List<ListarSolicitudesCreadasDTO> ObtenerSolicitudesCreadas(string loginName, string IdTipoAPI, string NumSolicitud, string FechaDel, string FechaAl)
        {
            List<ListarSolicitudesCreadasDTO> oLista = new List<ListarSolicitudesCreadasDTO>();
            ListarSolicitudesCreadasDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesCreadas";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@cuenta", loginName);
                cmd.Parameters.Add(par1);

                if(IdTipoAPI == null)
                    par1 = new SqlParameter("@IdTipoAPI", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoAPI", IdTipoAPI);
                
                cmd.Parameters.Add(par1);

                if(NumSolicitud == null)
                    par1 = new SqlParameter("@NumSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NumSolicitud",NumSolicitud);
                cmd.Parameters.Add(par1);

                DateTime fd;
                if (DateTime.TryParse(FechaDel, out fd))
                    par1 = new SqlParameter("@FechaDel", fd);
                else
                    par1 = new SqlParameter("@FechaDel", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;                
                cmd.Parameters.Add(par1);
                
                DateTime fa;
                if(DateTime.TryParse(FechaAl, out fa))
                    par1 = new SqlParameter("@FechaAl", FechaAl);
                else
                    par1 = new SqlParameter("@FechaAl", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarSolicitudesCreadasDTO();
                        oSolicitudInversionBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oSolicitudInversionBE.TotalUSD = dataReader["TotalUSD"].ToString();
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionBE.FlagPlanBase = Convert.ToInt32(dataReader["FlagPlanBase"]);
                        oSolicitudInversionBE.TipoAPI = oSolicitudInversionBE.NumSolicitud.Contains(TiposAPI.NuevoProyecto) ? TiposAPI.NuevoProyecto : TiposAPI.Ampliacion;
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

        public List<ListarSolicitudesCreadasDTO> ObtenerSolicitudesAdministracion(string IdTipoAPI,string NombreCortoSolicitud,string IdTipoActivo, string NumSolicitud,string CodigoProyecto,string IdEstado1,
                                                                                  string NombreProyecto,string IdEstado2, string IdSociedad,string IdTipoCapex,string IdCeCo,string IdCoordinador, string IdSector,
                                                                                  string ResponsableInv,string IdMacroservicio,string FechaDel, string FechaAl, int Rol)
        {
            List<ListarSolicitudesCreadasDTO> oLista = new List<ListarSolicitudesCreadasDTO>();
            ListarSolicitudesCreadasDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesInversionAdministracion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;

                if (IdTipoAPI == null)
                    par1 = new SqlParameter("@IdTipoAPI", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoAPI", IdTipoAPI);
                cmd.Parameters.Add(par1);

                if (NombreCortoSolicitud == null)
                    par1 = new SqlParameter("@NombCortoSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NombCortoSolicitud", NombreCortoSolicitud);
                cmd.Parameters.Add(par1);

                if (NumSolicitud == null)
                    par1 = new SqlParameter("@NumSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NumSolicitud", NumSolicitud);
                cmd.Parameters.Add(par1);

                if (IdTipoActivo == null)
                    par1 = new SqlParameter("@IdTipoActivo", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoActivo", IdTipoActivo);
                cmd.Parameters.Add(par1);

                if (CodigoProyecto == null)
                    par1 = new SqlParameter("@CodigoProyecto_OI", DBNull.Value);
                else
                    par1 = new SqlParameter("@CodigoProyecto_OI", CodigoProyecto);
                cmd.Parameters.Add(par1);

                if (IdEstado1 == null)
                    par1 = new SqlParameter("@IdEstado1", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdEstado1", IdEstado1);
                cmd.Parameters.Add(par1);

                if (NombreProyecto == null)
                    par1 = new SqlParameter("@NombreProyecto", DBNull.Value);
                else
                    par1 = new SqlParameter("@NombreProyecto", NombreProyecto);
                cmd.Parameters.Add(par1);

                if (IdEstado2 == null)
                    par1 = new SqlParameter("@IdEstado2", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdEstado2", IdEstado2);
                cmd.Parameters.Add(par1);

                if (IdSociedad == null)
                    par1 = new SqlParameter("@IdSociedad", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdSociedad", IdSociedad);
                cmd.Parameters.Add(par1);

                if (IdTipoCapex == null)
                    par1 = new SqlParameter("@IdTipoCapex", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoCapex", IdTipoCapex);
                cmd.Parameters.Add(par1);

                if (IdCeCo == null)
                    par1 = new SqlParameter("@IdCeCo", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdCeCo", IdCeCo);
                cmd.Parameters.Add(par1);

                if (IdCoordinador == null)
                    par1 = new SqlParameter("@IdCoordinador", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdCoordinador", IdCoordinador);
                cmd.Parameters.Add(par1);

                if (IdSector == null)
                    par1 = new SqlParameter("@IdSector", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdSector", IdSector);
                cmd.Parameters.Add(par1);

                if (ResponsableInv == null)
                    par1 = new SqlParameter("@ResponsableInv", DBNull.Value);
                else
                    par1 = new SqlParameter("@ResponsableInv", ResponsableInv);
                cmd.Parameters.Add(par1);

                if (IdMacroservicio == null)
                    par1 = new SqlParameter("@IdMacroservicio", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdMacroservicio", IdMacroservicio);
                cmd.Parameters.Add(par1);

                DateTime fd;
                if (DateTime.TryParse(FechaDel, out fd))
                    par1 = new SqlParameter("@FechaDel", fd);
                else
                    par1 = new SqlParameter("@FechaDel", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                DateTime fa;
                if (DateTime.TryParse(FechaAl, out fa))
                    par1 = new SqlParameter("@FechaAl", FechaAl);
                else
                    par1 = new SqlParameter("@FechaAl", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@Rol", Rol);
                par1.SqlDbType = SqlDbType.Int;
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarSolicitudesCreadasDTO();
                        oSolicitudInversionBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.NombreCortoSolicitud = dataReader["NombreCortoSolicitud"].ToString();
                        oSolicitudInversionBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.TipoCapex = dataReader["TipoCapex"].ToString();
                        oSolicitudInversionBE.TotalUSD = dataReader["TotalUSD"].ToString();
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionBE.FlagPlanBase = Convert.ToInt32(dataReader["FlagPlanBase"]);
                        oSolicitudInversionBE.TipoAPI = oSolicitudInversionBE.NumSolicitud.Contains(TiposAPI.NuevoProyecto) ? TiposAPI.NuevoProyecto : TiposAPI.Ampliacion;
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

        
        public List<ListarSolicitudesAdministracionExportar> ObtenerSolicitudesAdministracionExportar(string IdTipoAPI, string NombreCortoSolicitud, string IdTipoActivo, string NumSolicitud, string CodigoProyecto, string IdEstado1,
                                                                                  string NombreProyecto, string IdEstado2, string IdSociedad, string IdTipoCapex, string IdCeCo, string IdCoordinador, string IdSector,
                                                                                  string ResponsableInv, string IdMacroservicio, string FechaDel, string FechaAl, int rol)
        {
            List<ListarSolicitudesAdministracionExportar> oLista = new List<ListarSolicitudesAdministracionExportar>();
            ListarSolicitudesAdministracionExportar oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesInversionAdministracionExportar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;

                if (IdTipoAPI == null)
                    par1 = new SqlParameter("@IdTipoAPI", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoAPI", IdTipoAPI);
                cmd.Parameters.Add(par1);

                if (NombreCortoSolicitud == null)
                    par1 = new SqlParameter("@NombCortoSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NombCortoSolicitud", NombreCortoSolicitud);
                cmd.Parameters.Add(par1);

                if (NumSolicitud == null)
                    par1 = new SqlParameter("@NumSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NumSolicitud", NumSolicitud);
                cmd.Parameters.Add(par1);

                if (IdTipoActivo == null)
                    par1 = new SqlParameter("@IdTipoActivo", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoActivo", IdTipoActivo);
                cmd.Parameters.Add(par1);

                if (CodigoProyecto == null)
                    par1 = new SqlParameter("@CodigoProyecto_OI", DBNull.Value);
                else
                    par1 = new SqlParameter("@CodigoProyecto_OI", CodigoProyecto);
                cmd.Parameters.Add(par1);

                if (IdEstado1 == null)
                    par1 = new SqlParameter("@IdEstado1", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdEstado1", IdEstado1);
                cmd.Parameters.Add(par1);

                if (NombreProyecto == null)
                    par1 = new SqlParameter("@NombreProyecto", DBNull.Value);
                else
                    par1 = new SqlParameter("@NombreProyecto", NombreProyecto);
                cmd.Parameters.Add(par1);

                if (IdEstado2 == null)
                    par1 = new SqlParameter("@IdEstado2", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdEstado2", IdEstado2);
                cmd.Parameters.Add(par1);

                if (IdSociedad == null)
                    par1 = new SqlParameter("@IdSociedad", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdSociedad", IdSociedad);
                cmd.Parameters.Add(par1);

                if (IdTipoCapex == null)
                    par1 = new SqlParameter("@IdTipoCapex", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoCapex", IdTipoCapex);
                cmd.Parameters.Add(par1);

                if (IdCeCo == null)
                    par1 = new SqlParameter("@IdCeCo", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdCeCo", IdCeCo);
                cmd.Parameters.Add(par1);

                if (IdCoordinador == null)
                    par1 = new SqlParameter("@IdCoordinador", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdCoordinador", IdCoordinador);
                cmd.Parameters.Add(par1);

                if (IdSector == null)
                    par1 = new SqlParameter("@IdSector", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdSector", IdSector);
                cmd.Parameters.Add(par1);

                if (ResponsableInv == null)
                    par1 = new SqlParameter("@ResponsableInv", DBNull.Value);
                else
                    par1 = new SqlParameter("@ResponsableInv", ResponsableInv);
                cmd.Parameters.Add(par1);

                if (IdMacroservicio == null)
                    par1 = new SqlParameter("@IdMacroservicio", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdMacroservicio", IdMacroservicio);
                cmd.Parameters.Add(par1);

                DateTime fd;
                if (DateTime.TryParse(FechaDel, out fd))
                    par1 = new SqlParameter("@FechaDel", fd);
                else
                    par1 = new SqlParameter("@FechaDel", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                DateTime fa;
                if (DateTime.TryParse(FechaAl, out fa))
                    par1 = new SqlParameter("@FechaAl", FechaAl);
                else
                    par1 = new SqlParameter("@FechaAl", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@Rol",rol);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarSolicitudesAdministracionExportar();
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.NombreCortoSolicitud = dataReader["NombreCortoSolicitud"].ToString();
                        oSolicitudInversionBE.NombreProyecto=dataReader["NombreProyecto"].ToString();
                        oSolicitudInversionBE.ResponsableProyecto=dataReader["ResponsableProyecto"].ToString();
                        oSolicitudInversionBE.ResponsableInversion=dataReader["ResponsableInversion"].ToString();
                        oSolicitudInversionBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oSolicitudInversionBE.Estado = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.TipoAPI=dataReader["TipoAPI"].ToString();
                        oSolicitudInversionBE.TipoCapex = dataReader["TipoCapex"].ToString();
                        oSolicitudInversionBE.TotalUSD = Convert.ToDouble(dataReader["TotalUSD"]).ToString("N", CultureInfo.InvariantCulture);
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.UsuarioModifico=dataReader["UsuarioModifico"].ToString();
                        oSolicitudInversionBE.Marca=dataReader["Marca"].ToString();
                        oSolicitudInversionBE.Ubicacion=dataReader["Ubicacion"].ToString();
                        oSolicitudInversionBE.VAN=dataReader["VAN"].ToString();
                        oSolicitudInversionBE.TIR=dataReader["TIR"].ToString();
                        oSolicitudInversionBE.PRI=dataReader["PRI"].ToString();

                        if (Convert.ToInt32(dataReader["PeriodoPRI"]) == 0)
                            oSolicitudInversionBE.PeriodoPRI = "Años";
                        else if (Convert.ToInt32(dataReader["PeriodoPRI"]) == 1)
                            oSolicitudInversionBE.PeriodoPRI = "Meses";
                        else
                            oSolicitudInversionBE.PeriodoPRI = "";

                        oSolicitudInversionBE.ObservacionesFinancieras=dataReader["ObservacionesFinancieras"].ToString();
                        oSolicitudInversionBE.TipoActivo=dataReader["TipoActivo"].ToString();
                        oSolicitudInversionBE.CodigoOI=dataReader["CodigoOI"].ToString();
                        oSolicitudInversionBE.DescripcionOI=dataReader["DescripcionOI"].ToString();
                        oSolicitudInversionBE.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oSolicitudInversionBE.MontoOI = Convert.ToDouble(dataReader["MontoOI"]).ToString("N", CultureInfo.InvariantCulture);
                        oSolicitudInversionBE.Moneda=dataReader["Moneda"].ToString();
                        oSolicitudInversionBE.FechaCreacion = Convert.ToDateTime(dataReader["FechaCreacion"]).ToString();
                        oSolicitudInversionBE.FechaModificacion = Convert.ToDateTime(dataReader["FechaModificacion"]).ToString();
                        oSolicitudInversionBE.TipoAPI = oSolicitudInversionBE.NumSolicitud.Contains(TiposAPI.NuevoProyecto) ? TiposAPI.NuevoProyecto : TiposAPI.Ampliacion;
                        oSolicitudInversionBE.Descripcion = dataReader["Descripcion"].ToString();
                        oSolicitudInversionBE.Observaciones = dataReader["Observaciones"].ToString();
                        oSolicitudInversionBE.VidaUtil = dataReader["VidaUtil"].ToString();
                        oSolicitudInversionBE.VidaUtilAmpliar = dataReader["VidaUtilAmpliar"].ToString();
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

        public List<ListarSolicitudesCreadasExportarDTO> ObtenerSolicitudesCreadasExportar(string loginName, string IdTipoAPI, string NumSolicitud, string FechaDel, string FechaAl)
        {
            List<ListarSolicitudesCreadasExportarDTO> oLista = new List<ListarSolicitudesCreadasExportarDTO>();
            ListarSolicitudesCreadasExportarDTO oSolicitudInversionBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesCreadas";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@cuenta", loginName);
                cmd.Parameters.Add(par1);

                if (IdTipoAPI == null)
                    par1 = new SqlParameter("@IdTipoAPI", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdTipoAPI", IdTipoAPI);

                cmd.Parameters.Add(par1);

                if (NumSolicitud == null)
                    par1 = new SqlParameter("@NumSolicitud", DBNull.Value);
                else
                    par1 = new SqlParameter("@NumSolicitud", NumSolicitud);
                cmd.Parameters.Add(par1);

                DateTime fd;
                if (DateTime.TryParse(FechaDel, out fd))
                    par1 = new SqlParameter("@FechaDel", fd);
                else
                    par1 = new SqlParameter("@FechaDel", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                DateTime fa;
                if (DateTime.TryParse(FechaAl, out fa))
                    par1 = new SqlParameter("@FechaAl", FechaAl);
                else
                    par1 = new SqlParameter("@FechaAl", DBNull.Value);

                par1.SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE = new ListarSolicitudesCreadasExportarDTO();
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.TotalUSD = dataReader["TotalUSD"].ToString();
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.FechaCreado = dataReader["FechaCreacion"].ToString();
                        oSolicitudInversionBE.FechaModifico = dataReader["FechaModificacion"].ToString();
                        oSolicitudInversionBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();

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

        public ListarSolicitudesCreadasDTO ObtenerTareasAprobacion(string Id)
        {
            ListarSolicitudesCreadasDTO oSolicitudInversionBE = new ListarSolicitudesCreadasDTO();
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesPorInstancia";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", Convert.ToInt32(Id));
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.TotalUSD = dataReader["TotalUSD"].ToString();
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionBE.FlagPlanBase = Convert.ToInt32(dataReader["FlagPlanBase"]);
                        return oSolicitudInversionBE;
                    }
                }
                return null;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            
        }

        public ListarSolicitudesCreadasExportarDTO ObtenerTareasAprobacionExportar(string Id)
        {
            ListarSolicitudesCreadasExportarDTO oSolicitudInversionBE = new ListarSolicitudesCreadasExportarDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesPorInstancia";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", Convert.ToInt32(Id));
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionBE.CeCo = dataReader["CeCo"].ToString();
                        oSolicitudInversionBE.Sector = dataReader["Sector"].ToString();
                        oSolicitudInversionBE.Macroservicio = dataReader["Macroservicio"].ToString();
                        oSolicitudInversionBE.TotalUSD = dataReader["TotalUSD"].ToString();
                        oSolicitudInversionBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionBE.FechaCreado = dataReader["FechaCreacion"].ToString();
                        oSolicitudInversionBE.FechaModifico = dataReader["FechaModificacion"].ToString();
                        return oSolicitudInversionBE;
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public int ObtenerIdInstancia(string IdSolicitud)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));

            ScriptorContent contenido = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();
            return contenido.Parts.IdInstancia; 
        }

        public ResultadoConsultaExisteProyectoDTO ConsultarExistenciaProyecto(string id,string CodigoProyecto)
        {
            ResultadoConsultaExisteProyectoDTO oResultado = new ResultadoConsultaExisteProyectoDTO();

            //ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            //ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            //ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));

            ////Buscar en solicitudes de invesion por si esta en estado creado solo aqui se encontrará
            //List<ScriptorContent> oListaContenidos;
            //oListaContenidos = canalSolicitudInversion.QueryContents("CodigoProyecto", CodigoProyecto, "=").QueryContents("IdTipoAPI", new Guid(TiposAPI.IdNuevoProyecto), "=").ToList();

            List<RSolicitudInversionBE> listaSolicitudes = obtenerSolicitudesPorCodigoProyecto(CodigoProyecto);
            
            if (listaSolicitudes.Count > 0)
            {
                if (!String.IsNullOrEmpty(id))
                {
                    List<RSolicitudInversionBE> listaSolicitudesPorId = listaSolicitudes.Where(c => c.Id == new Guid(id)).ToList();
                    if (listaSolicitudesPorId.Count > 0)
                    {
                        oResultado.NumAPI = "";
                        oResultado.PlanBase = "";
                    }
                    else
                    {
                        oResultado.NumAPI = listaSolicitudes[0].Id.ToString();
                        oResultado.PlanBase = "";
                    }
                }
                else
                {
                    oResultado.NumAPI = listaSolicitudes[0].Id.ToString();
                    oResultado.PlanBase = "";                
                }
            
                return oResultado;
            }
            //else
           // {
                //Pertenece a un plan base
                //oListaContenidos = canalInversion.QueryContents("CodigoProyecto", CodigoProyecto, "=").ToList();
                //if (oListaContenidos.Count > 0)
                //{
                //    foreach (ScriptorContent item in oListaContenidos)
                //    {
                //        ScriptorContent contenido = canalMovimiento.QueryContents("IdInversion", item.Id, "=").ToList().FirstOrDefault();

                //        if (contenido != null)
                //        {
                //            oResultado.PlanBase = "Plan Base";
                //            oResultado.NumAPI = "";
                //        }
                //    }                   
                //    return oResultado;
                //}
           // }
            return oResultado;
        }

        public List<RSolicitudInversionBE> obtenerSolicitudesPorCodigoProyecto(string codProyecto)
        {
            List<RSolicitudInversionBE> resultado = new List<RSolicitudInversionBE>();
            RSolicitudInversionBE solitudBE = new RSolicitudInversionBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarExisteProyecto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoProyecto", codProyecto);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        solitudBE = new RSolicitudInversionBE();
                        solitudBE.Id = (Guid)dataReader["id"];
                        resultado.Add(solitudBE);

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
            return resultado;
        }

        public int ValidarAmpliacionEnEstadoPendiente(string codProyecto)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarAmpliacionEnEstadoPendiente";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Codigo", codProyecto);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        resultado = Convert.ToInt32(dataReader["Cantidad"]);

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
            return resultado;
        }
        /*public List<RSolicitudInversionBE> ObtenerSolicitudesConsultasAPI(RequestCabeceraConsultasAPIDTO filtros)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            List<RSolicitudInversionBE> oListaSolicitud = new List<RSolicitudInversionBE>();
            List<ScriptorContent> oLista = new List<ScriptorContent>();
            

            if (filtros IdTipoAPI.ToUpper() == TiposAPI.IdAmpliacion || IdTipoAPI.ToUpper() == TiposAPI.IdNuevoProyecto || IdTipoAPI == "Todos" || String.IsNullOrEmpty(IdTipoAPI) || IdTipoAPI == "undefined")
            {
                ScriptorQueryEnumerable<ScriptorContent> oInversion = null;

                if (!String.IsNullOrEmpty(loginName))
                {
                    ScriptorChannel canalCoordinadores = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
                    ScriptorContent coordinador = canalCoordinadores.QueryContents("CuentaRed", loginName, "=").ToList().FirstOrDefault();

                    if (coordinador != null)
                    {
                        oInversion = canalSolicitudInversion.QueryContents("IdCoordinador", coordinador.Id, "=");

                        if (IdTipoAPI.ToUpper() == TiposAPI.IdAmpliacion)
                        {
                            oInversion = oInversion.QueryContents("IdTipoAPI", TiposAPI.IdAmpliacion, "=");
                        }
                        else if (IdTipoAPI.ToUpper() == TiposAPI.IdNuevoProyecto)
                        {
                            oInversion = oInversion.QueryContents("IdTipoAPI", TiposAPI.IdNuevoProyecto, "=");
                        }


                        if (!String.IsNullOrEmpty(NumSolicitud))
                        {
                            oInversion = oInversion.QueryContents("NumSolicitud", NumSolicitud, "=");
                        }
                        else
                        {
                            oInversion = oInversion.QueryContents("NumSolicitud", "", "<>");
                        }

                        if (!String.IsNullOrEmpty(FechaDel))
                        {
                            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                            oInversion = oInversion.QueryContents("FechaCreacion", Convert.ToDateTime(FechaDel), ">=");
                        }

                        if (!String.IsNullOrEmpty(FechaAl))
                        {
                            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
                            oInversion = oInversion.QueryContents("FechaCreacion", Convert.ToDateTime(FechaAl), "<=");
                        }
                    }
                }


                if (oInversion != null)
                    oLista = oInversion.ToList();
                else
                    oLista = new List<ScriptorContent>();

            }

            RSolicitudInversionBE oSolicitud = null;

            foreach (ScriptorContent item in oLista)
            {
                oSolicitud = new RSolicitudInversionBE();

                oSolicitud.NumSolicitud = item.Parts.NumSolicitud;

                if (((ScriptorDropdownListValue)item.Parts.IdEstado).Content != null)
                {
                    oSolicitud.IdEstado = new REstadoBE();
                    oSolicitud.IdEstado.Id = ((ScriptorDropdownListValue)item.Parts.IdEstado).Content.Id;
                    oSolicitud.IdEstado.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdEstado).Content.Parts.Descripcion;
                }

                // A cargo de falta

                if (((ScriptorDropdownListValue)item.Parts.IdSociedad).Content != null)
                {
                    oSolicitud.IdSociedad = new RSociedadBE();
                    oSolicitud.IdSociedad.Id = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Id;
                    oSolicitud.IdSociedad.Codigo = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitud.IdSociedad.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)item.Parts.IdCeCo).Content != null)
                {
                    oSolicitud.IdCeCo = new RCentroCostoBE();
                    oSolicitud.IdCeCo.Id = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Id;
                    oSolicitud.IdCeCo.Codigo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.CodCECO;
                    oSolicitud.IdCeCo.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.DescCECO;
                }

                if (((ScriptorDropdownListValue)item.Parts.IdSector).Content != null)
                {
                    oSolicitud.IdSector = new RSectorBE();
                    oSolicitud.IdSector.Id = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Id;
                    oSolicitud.IdSector.Codigo = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Codigo;
                    oSolicitud.IdSector.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdSector).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content != null)
                {
                    oSolicitud.IdMacroServicio = new RMacroservicioBE();
                    oSolicitud.IdMacroServicio.Id = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Id;
                    oSolicitud.IdMacroServicio.Codigo = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Codigo;
                    oSolicitud.IdMacroServicio.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdMacroservicio).Content.Parts.Descripcion;
                }

                oSolicitud.CodigoProyecto = item.Parts.CodigoProyecto;
                oSolicitud.MontoTotal = item.Parts.MontoTotal;

                if (((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content != null)
                {
                    oSolicitud.IdCoordinador = new RCoordinadorBE();
                    oSolicitud.IdCoordinador.Id = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Id;
                    oSolicitud.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Parts.CuentaRed;
                    oSolicitud.IdCoordinador.Nombre = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Parts.Nombre;
                }

                oSolicitud.FechaCreacion = item.Parts.FechaCreacion;
                oSolicitud.FechaModificacion = item.Parts.FechaModificacion;

                oListaSolicitud.Add(oSolicitud);

            }

            return oListaSolicitud;            
        }
        */

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

        public RAprobadoresAPIWFBE ObtenerAprobadores(RSolicitudInversionBE oSolicitudInversion)
        {
            RAprobadoresAPIWFBE oAprobadores = new RAprobadoresAPIWFBE();
            //RValoresAprobacionBE valorAprobacion;
            //Setear a vacio los aprobadores
            oAprobadores.expertoCompras = "";
            oAprobadores.expertoInmobiliaria = "";
            oAprobadores.expertoMantenimiento = "";
            oAprobadores.expertoSistemas = "";
            oAprobadores.gerenteCentral="";
            oAprobadores.finanzas = "";
            oAprobadores.gerenteFinanzas = "";
            oAprobadores.gerenteGeneral = "";
            oAprobadores.gerenteLinea = "";
            oAprobadores.controlGestion = "";
            oAprobadores.controlGestion2 = "";
            oAprobadores.TipoActivo = "";
            oAprobadores.Administradores = "";
            oAprobadores.Contabilidad = "";

            RCentroCostoDAL oCeCo = new RCentroCostoDAL();            

            #region Constantes

                //Aprobadores
                const string ExpertoInmobiliaria = "Experto Inmobiliaria";
                const string ExpertoSistemas = "Experto Sistemas";
                const string ExpertoMantenimiento = "Experto Mantenimiento";
                const string ExpertoCompras = "Experto Compras";

                const string GerenteFinanzas = "Gerente Central de Finanzas y Estrategia";
                const string ControlGestion = "Control de Gestión 1";
                const string ControlGestion2 = "Control de Gestión 2";
                const string Finanzas = "Finanzas";
                const string GerenciaGeneral = "Gerencia General";
                const string Administradores = "Administradores";
                const string Contabilidad = "Contabilidad";
            

                /*              
                //Capex
                const string CapexReemblosable = "Capex Reembolsable";
                const string CapexOperacional = "Capex Operacional";
                const string CapexRegulatorio = "Capex Regulatorio";
                const string CapexMantenimiento = "Capex Mantenimiento";
                const string CapexIngreso = "Capex Ingreso";
                const string CapexAhorro = "Capex Ahorro";

                //Tipo Activo
                const string ActivoTerreno = "Terreno";
                const string ActivoEdificaciones = "Edificaciones";
                const string ActivoUnidadesTransporte = "Unidades de Transporte";
                const string ActivoMaquinariasEquipos = "Maquinarias y Equipos";
                const string ActivoEquiposComputo = "Equipos de computo";
                const string ActivoMueblesEnseres = "Muebles y Enseres";
                const string ActivoEquiposDiversos = "Equipos diversos";
                const string ActivoSoftware = "Software";	*/

            #endregion
            #region Administradores
            List<RAdministradoresBE> oListaAdmin = ObtenerAdministradores(Administradores);

            foreach (RAdministradoresBE i in oListaAdmin)
            {
                if (!String.IsNullOrEmpty(oAprobadores.Administradores))
                    oAprobadores.Administradores += "|";

                oAprobadores.Administradores += i.Usuario;

            }
            #endregion

            #region Contabilidad
            oListaAdmin = ObtenerAdministradores(Contabilidad);

            foreach (RAdministradoresBE i in oListaAdmin)
            {
                if (!String.IsNullOrEmpty(oAprobadores.Contabilidad))
                    oAprobadores.Contabilidad += "|";

                oAprobadores.Contabilidad += i.Usuario;

            }
            #endregion
            #region Gerente de Linea y Gerente Central

            if(oSolicitudInversion.IdCeCo != null)
            {
                oAprobadores.gerenteLinea = oCeCo.obtenerCentroCostoPorCodigoBD(oSolicitudInversion.IdCeCo.Codigo).GerenteLinea;

                oAprobadores.gerenteCentral = oCeCo.obtenerCentroCostoPorCodigoBD(oSolicitudInversion.IdCeCo.Codigo).GerenteCentral;
                                
            }
            #endregion

            #region TipoActivo
            foreach (RDetalleSolicitudInversionBE item in oSolicitudInversion.DetalleSolicitudInversion)
            {
                if (!String.IsNullOrEmpty(oAprobadores.TipoActivo))
                    oAprobadores.TipoActivo += "|";

                oAprobadores.TipoActivo += item.IdTipoActivo.IdentificadorFlujo;
            }
            #endregion

            #region ExpertoInmobiliaria
            List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);

            foreach (RAprobadoresBE i in oLista)
            {
                if (!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                    oAprobadores.expertoInmobiliaria += "|";

                oAprobadores.expertoInmobiliaria += i.Usuario;

            }
            #endregion

            #region ExpertoCompras
            oLista = ObtenerAprobadoresPorRol(ExpertoCompras);

            foreach (RAprobadoresBE i in oLista)
            {
                if (!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                    oAprobadores.expertoCompras += "|";

                oAprobadores.expertoCompras += i.Usuario;

            }
            #endregion

            #region ExpertoSistemas
            oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);

            foreach (RAprobadoresBE i in oLista)
            {
                if (!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                    oAprobadores.expertoSistemas += "|";

                oAprobadores.expertoSistemas += i.Usuario;

            }
            #endregion

            #region ExpertoMantenimiento
            oLista = ObtenerAprobadoresPorRol(ExpertoMantenimiento);

            foreach (RAprobadoresBE i in oLista)
            {
                if (!String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                    oAprobadores.expertoMantenimiento += "|";

                oAprobadores.expertoMantenimiento += i.Usuario;

            }
            #endregion

            #region ControlGestion
            oLista = ObtenerAprobadoresPorRol(ControlGestion);

            foreach (RAprobadoresBE i in oLista)
            {
                if (!String.IsNullOrEmpty(oAprobadores.controlGestion))
                    oAprobadores.controlGestion += "|";

                oAprobadores.controlGestion += i.Usuario;

            }
            #endregion

            #region ControlGEstion2
                oLista = ObtenerAprobadoresPorRol(ControlGestion2);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.controlGestion2))
                        oAprobadores.controlGestion2 += "|";

                    oAprobadores.controlGestion2 += i.Usuario;

                }
            #endregion

            #region Finanzas

             oLista = ObtenerAprobadoresPorRol(Finanzas);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.finanzas))
                        oAprobadores.finanzas += "|";

                    oAprobadores.finanzas += i.Usuario;

                }

            
            #endregion

            #region GerenteFinanzas
                        
                oLista = ObtenerAprobadoresPorRol(GerenteFinanzas);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.gerenteFinanzas))
                        oAprobadores.gerenteFinanzas += "|";

                    oAprobadores.gerenteFinanzas += i.Usuario;

                }

            
            #endregion

            #region GerenteGeneral

             oLista = ObtenerAprobadoresPorRol(GerenciaGeneral);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.gerenteGeneral))
                        oAprobadores.gerenteGeneral += "|";

                    oAprobadores.gerenteGeneral += i.Usuario;

                }

            #endregion

            #region ObtenerAnterior
            //List<RValoresAprobacionBE> oListaValoresAprobacion = ObtenerValoresAprobacion();
            /*
            #region AreaExperta
            foreach(RDetalleSolicitudInversionBE item in oSolicitudInversion.DetalleSolicitudInversion)
            {
                switch(item.IdTipoActivo.Descripcion)
                {
                    case ActivoTerreno:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion
                        
                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion
                        
                        break;

                    case ActivoEdificaciones:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion
                        
                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Inmobiliaria
                            if(String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoInmobiliaria).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoInmobiliaria);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoInmobiliaria))
                                            oAprobadores.expertoInmobiliaria += "|";

                                        oAprobadores.expertoInmobiliaria += i.Usuario;
                                    
                                    }

                                }

                            }
                            #endregion

                        }
                        #endregion
                        
                        break;

                    case ActivoUnidadesTransporte:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Mantenimiento
                            if(String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoMantenimiento).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoMantenimiento);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                                            oAprobadores.expertoMantenimiento += "|";

                                        oAprobadores.expertoMantenimiento += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;

                    case ActivoMaquinariasEquipos:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Mantenimiento
                            if(String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoMantenimiento).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoMantenimiento);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                                            oAprobadores.expertoMantenimiento += "|";

                                        oAprobadores.expertoMantenimiento += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;
                        
                    case ActivoEquiposComputo:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;

                    case ActivoMueblesEnseres:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Mantenimiento
                            if(String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoMantenimiento).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoMantenimiento);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                                            oAprobadores.expertoMantenimiento += "|";

                                        oAprobadores.expertoMantenimiento += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;

                    case ActivoEquiposDiversos:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Mantenimiento
                            if(String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoMantenimiento).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoMantenimiento);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoMantenimiento))
                                            oAprobadores.expertoMantenimiento += "|";

                                        oAprobadores.expertoMantenimiento += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion

                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }

                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Compras
                            if(String.IsNullOrEmpty(oAprobadores.expertoCompras))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoCompras).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoCompras);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoCompras))
                                            oAprobadores.expertoCompras += "|";

                                        oAprobadores.expertoCompras += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;

                    case ActivoSoftware:

                        #region CapexIngreso
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexIngreso)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexAhorro
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexAhorro)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexMantenimiento
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexMantenimiento)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexOperacional
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexOperacional)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexRegulatorio
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexRegulatorio)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region CapexReembolsable
                        if(oSolicitudInversion.IdTipoCapex.Descripcion == CapexReemblosable)
                        {
                            #region Sistemas
                            if(String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                            {
                                valorAprobacion = new RValoresAprobacionBE();
                                valorAprobacion = oListaValoresAprobacion.Where( x=> x.IdRol.Descripcion == ExpertoSistemas).FirstOrDefault();
                                if(oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
                                {
                                    List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ExpertoSistemas);
                                
                                    foreach(RAprobadoresBE i in oLista)
                                    {
                                        if(!String.IsNullOrEmpty(oAprobadores.expertoSistemas))
                                            oAprobadores.expertoSistemas += "|";

                                        oAprobadores.expertoSistemas += i.Usuario;
                                    
                                    }

                                }
                            }
                            #endregion
                        }
                        #endregion

                        break;

                }


            }
            #endregion

            #region ControlGestion

            valorAprobacion = new RValoresAprobacionBE();
            valorAprobacion = oListaValoresAprobacion.Where(x => x.IdRol.Descripcion == ControlGestion).FirstOrDefault();

            if (oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
            {
                List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ControlGestion);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.controlGestion))
                        oAprobadores.controlGestion += "|";

                    oAprobadores.controlGestion += i.Usuario;

                }

            }
            #endregion

            #region ControlGestion2

            valorAprobacion = new RValoresAprobacionBE();
            valorAprobacion = oListaValoresAprobacion.Where(x => x.IdRol.Descripcion == ControlGestion2).FirstOrDefault();

            if (oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
            {
                List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(ControlGestion2);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.controlGestion2))
                        oAprobadores.controlGestion2 += "|";

                    oAprobadores.controlGestion2 += i.Usuario;

                }

            }
            #endregion

            #region Finanzas

            valorAprobacion = new RValoresAprobacionBE();
            valorAprobacion = oListaValoresAprobacion.Where(x => x.IdRol.Descripcion == Finanzas).FirstOrDefault();

            if (oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
            {
                List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(Finanzas);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.finanzas))
                        oAprobadores.finanzas += "|";

                    oAprobadores.finanzas += i.Usuario;

                }

            }
            #endregion

            #region GerenteFinanzas

            valorAprobacion = new RValoresAprobacionBE();
            valorAprobacion = oListaValoresAprobacion.Where(x => x.IdRol.Descripcion == GerenteFinanzas).FirstOrDefault();

            if (oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
            {
                List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(GerenteFinanzas);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.gerenteFinanzas))
                        oAprobadores.gerenteFinanzas += "|";

                    oAprobadores.gerenteFinanzas += i.Usuario;

                }

            }
            #endregion

            #region GerenteCentral

            valorAprobacion = new RValoresAprobacionBE();
            valorAprobacion = oListaValoresAprobacion.Where(x => x.IdRol.Descripcion == GerenciaGeneral).FirstOrDefault();

            if (oSolicitudInversion.MontoTotal >= valorAprobacion.Monto)
            {
                List<RAprobadoresBE> oLista = ObtenerAprobadoresPorRol(GerenciaGeneral);

                foreach (RAprobadoresBE i in oLista)
                {
                    if (!String.IsNullOrEmpty(oAprobadores.gerenteCentral))
                        oAprobadores.gerenteCentral += "|";

                    oAprobadores.gerenteCentral += i.Usuario;

                }

            }*/
            #endregion


            return oAprobadores;
        }

        /*public List<RValoresAprobacionBE> ObtenerValoresAprobacionPorAnio()
        {
            //ScriptorChannel canalValoresAprobacion = new ScriptorClient().GetChannel(new Guid(Canales.ValoresAprobacion));

            List<RValoresAprobacionBE> oLista = new List<RValoresAprobacionBE>();
            RValoresAprobacionBE oValorAprobacion;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerAprobadoresPorRol";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@rol", rol);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oAprobadorBE = new RAprobadoresBE();
                        oAprobadorBE.Usuario = dataReader["Usuario"].ToString();
                        oLista.Add(oAprobadorBE);

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

            //Verificar si hay que validar el año
            /*List<ScriptorContent> oListaValoresAprobacion = canalValoresAprobacion.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            foreach (ScriptorContent item in oListaValoresAprobacion)
            {
                oValorAprobacion = new RValoresAprobacionBE();

                oValorAprobacion.Id = item.Id;

                if(item.Parts.IdRol != null)
                {
                    oValorAprobacion.IdRol = new RRolBE();
                    oValorAprobacion.IdRol.Id = new Guid(((ScriptorDropdownListValue)item.Parts.IdRol).Value);
                    oValorAprobacion.IdRol.Descripcion = ((ScriptorDropdownListValue)item.Parts.IdRol).Content.Parts.Descripcion;

                    if (((ScriptorDropdownListValue)item.Parts.IdRol).Content.Parts.IdTipoAprobador != null)
                    {
                        oValorAprobacion.IdRol.TipoAprobador = ((ScriptorDropdownListValue)((ScriptorDropdownListValue)item.Parts.IdRol).Content.Parts.IdTipoAprobador).Content.Parts.Descripcion;                        
                    }                    
                }

                oValorAprobacion.Monto = item.Parts.Monto;
                oValorAprobacion.Anio = item.Parts.Anio.Title;

                oLista.Add(oValorAprobacion);
            }

            return oLista;

            
        }*/

        public List<RAdministradoresBE> ObtenerAdministradores(string nombre)
        {
            ScriptorChannel canalAdministradores = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Administradores));
            List<RAdministradoresBE> oLista = new List<RAdministradoresBE>();
            RAdministradoresBE oAdministradorBE;
            List<ScriptorContent> contenidos = canalAdministradores.QueryContents("#Id", Guid.NewGuid(), "<>").Where(x => x.StateDescription == "Publicado").ToList();

            foreach (ScriptorContent content in contenidos)
            {
                foreach (ScriptorContent item in (ScriptorContentInsert)content.Parts.Idtipo)
                {
                    if (item.Parts.descripcion == nombre)
                    {
                        oAdministradorBE = new RAdministradoresBE();
                        //oAdministradorBE.Nombre = content.Parts.Nombre;
                        oAdministradorBE.Usuario = content.Parts.CuentaRed;

                        oLista.Add(oAdministradorBE);
                    }
                }
            }

            return oLista;
        }

        public List<RAprobadoresBE> ObtenerAprobadoresPorRol(string rol)
        {
            List<RAprobadoresBE> oLista = new List<RAprobadoresBE>();

            RAprobadoresBE oAprobadorBE;
                    
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerAprobadoresPorRol";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@rol", rol);
                cmd.Parameters.Add(par1);

               
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oAprobadorBE = new RAprobadoresBE();
                        oAprobadorBE.Usuario = dataReader["Usuario"].ToString();
                        oLista.Add(oAprobadorBE);
                                                
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

            /*
            ScriptorChannel canalAprobador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Aprobadores));
            ScriptorChannel canalRol = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Rol));

            ScriptorContent oRol = canalRol.QueryContents("Descripcion", rol, "=").ToList().FirstOrDefault();
            List<ScriptorContent> oListaAprobador = canalAprobador.QueryContents("IdRol", oRol.Id, "=").ToList();

            List<RAprobadoresBE> oLista = new List<RAprobadoresBE>();

            RAprobadoresBE oAprobadorBE;
            foreach(ScriptorContent item in oListaAprobador)
            {
                oAprobadorBE = new RAprobadoresBE();
                oAprobadorBE.Usuario = item.Parts.Usuario;
                oLista.Add(oAprobadorBE);

            }            
            return oLista;
             * */

        }

       
        public List<RStatusPresupuestoBE> ObtenerStatusPresupuesto(string IdSolicitud)
        {
            
            List<RStatusPresupuestoBE> oListaStatus = new List<RStatusPresupuestoBE>();
            RStatusPresupuestoBE oStatus;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerStatusPresupuesto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitud", IdSolicitud);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oStatus = new RStatusPresupuestoBE();

                        oStatus.PlanBase = Convert.ToDouble(dataReader["PLANBASE"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.APIsCerradas = Convert.ToDouble(dataReader["APISCERRADAS"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.APIsPendientes = Convert.ToDouble(dataReader["APISPENDIENTES"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.SaldoActual = Convert.ToDouble(dataReader["SALDOACTUAL"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.NuevaAPI = Convert.ToDouble(dataReader["NUEVAAPI"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.SaldoFinal = Convert.ToDouble(dataReader["SALDOFINAL"]).ToString("N", CultureInfo.InvariantCulture);
                        oStatus.Exceso = Convert.ToDouble(dataReader["EXCESO"]).ToString("N", CultureInfo.InvariantCulture);

                        oListaStatus.Add(oStatus);

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


            /*
            RSolicitudInversionBE oSolicitud = BuscarPorIdSolicitud(IdSolicitud, TiposAPI.IdNuevoProyecto);

            
            
            //Calcular Suma Plan Base apis cerrados y pendientes

            //Proyecto
            oStatus = new RStatusPresupuestoBE();
            oStatus.PlanBase = 0;
            oStatus.APIsCerradas = 0;
            oStatus.APIsPendientes = 0;
            oStatus.SaldoActual = oStatus.PlanBase - oStatus.APIsCerradas + oStatus.APIsPendientes;
            oStatus.NuevaAPI = oSolicitud.MontoTotal;
            oStatus.SaldoFinal = oStatus.SaldoActual + oStatus.NuevaAPI;
            oStatus.Exceso = oStatus.SaldoFinal - oStatus.PlanBase;

            oListaStatus.Add(oStatus);

            //CeCo
            oStatus = new RStatusPresupuestoBE();
            oStatus.PlanBase = 0; // CalcularMontoPlanBaseCeCo(oSolicitud.IdCeCo.Id.ToString());
            oStatus.APIsCerradas = 0;
            oStatus.APIsPendientes = 0;
            oStatus.SaldoActual = oStatus.PlanBase - oStatus.APIsCerradas + oStatus.APIsPendientes;
            oStatus.NuevaAPI = 0;
            oStatus.SaldoFinal = oStatus.SaldoActual + oStatus.NuevaAPI;
            oStatus.Exceso = oStatus.SaldoFinal - oStatus.PlanBase;
            oListaStatus.Add(oStatus);

            //MacroServicio
            oStatus = new RStatusPresupuestoBE();
            oStatus.PlanBase = 0;
            oStatus.APIsCerradas = 0;
            oStatus.APIsPendientes = 0;
            oStatus.SaldoActual = oStatus.PlanBase - oStatus.APIsCerradas + oStatus.APIsPendientes;
            oStatus.NuevaAPI = 0;
            oStatus.SaldoFinal = oStatus.SaldoActual + oStatus.NuevaAPI;
            oStatus.Exceso = oStatus.SaldoFinal - oStatus.PlanBase;
            oListaStatus.Add(oStatus);

            //Sociedad
            oStatus = new RStatusPresupuestoBE();
            oStatus.PlanBase = 0;
            oStatus.APIsCerradas = 0;
            oStatus.APIsPendientes = 0;
            oStatus.SaldoActual = oStatus.PlanBase - oStatus.APIsCerradas + oStatus.APIsPendientes;
            oStatus.NuevaAPI = 0;
            oStatus.SaldoFinal = oStatus.SaldoActual + oStatus.NuevaAPI;
            oStatus.Exceso = oStatus.SaldoFinal - oStatus.PlanBase;
            oListaStatus.Add(oStatus);
            */
            return oListaStatus;
        }

        public DataTable ObtenerStatusPresupuestoExportar(string IdSolicitud,string idTipoAPI)
        {
            RSolicitudInversionBE oSolicitud = BuscarPorIdSolicitud(IdSolicitud, idTipoAPI);

            DataTable oListaStatus = new DataTable();
            //RStatusPresupuestoBE oStatus;
            oListaStatus.Columns.Add("PlanBase");
            oListaStatus.Columns.Add("APIsCerradas");
            oListaStatus.Columns.Add("APIsPendientes");
            oListaStatus.Columns.Add("SaldoActual");
            oListaStatus.Columns.Add("NuevaAPI");
            oListaStatus.Columns.Add("SaldoFinal");
            oListaStatus.Columns.Add("Exceso");
            oListaStatus.Columns.Add("Descripcion");
            
            //Calcular Suma Plan Base apis cerrados y pendientes

            //Proyecto
            DataRow row1 = oListaStatus.NewRow();
            row1["PlanBase"] = 0;
            row1["APIsCerradas"] = 0;
            row1["APIsPendientes"] = 0;
            row1["SaldoActual"] = (Convert.ToDouble(row1["PlanBase"]) - Convert.ToDouble(row1["APIsCerradas"]) + Convert.ToDouble(row1["APIsPendientes"])).ToString();
            row1["NuevaAPI"] = oSolicitud.MontoTotal;
            row1["SaldoFinal"] = (Convert.ToDouble(row1["SaldoActual"]) + Convert.ToDouble(row1["NuevaAPI"])).ToString();
            row1["Exceso"] = (Convert.ToDouble(row1["SaldoFinal"]) - Convert.ToDouble(row1["PlanBase"])).ToString();
            row1["Descripcion"] = "Proyecto";
            oListaStatus.Rows.Add(row1);

            //CeCo
            DataRow row2 = oListaStatus.NewRow();
            
            row2["PlanBase"] = 0;
            row2["APIsCerradas"] = 0;
            row2["APIsPendientes"] = 0;
            row2["SaldoActual"] = (Convert.ToDouble(row1["PlanBase"]) - Convert.ToDouble(row1["APIsCerradas"]) + Convert.ToDouble(row1["APIsPendientes"])).ToString();
            row2["NuevaAPI"] = oSolicitud.MontoTotal;
            row2["SaldoFinal"] = (Convert.ToDouble(row1["SaldoActual"]) + Convert.ToDouble(row1["NuevaAPI"])).ToString();
            row2["Exceso"] = (Convert.ToDouble(row1["SaldoFinal"]) - Convert.ToDouble(row1["PlanBase"])).ToString();
            row2["Descripcion"] = "CeCo";
            oListaStatus.Rows.Add(row2);

            //MacroServicio
            DataRow row3 = oListaStatus.NewRow();
           
            row3["PlanBase"] = 0;
            row3["APIsCerradas"] = 0;
            row3["APIsPendientes"] = 0;
            row3["SaldoActual"] = (Convert.ToDouble(row1["PlanBase"]) - Convert.ToDouble(row1["APIsCerradas"]) + Convert.ToDouble(row1["APIsPendientes"])).ToString();
            row3["NuevaAPI"] = oSolicitud.MontoTotal;
            row3["SaldoFinal"] = (Convert.ToDouble(row1["SaldoActual"]) + Convert.ToDouble(row1["NuevaAPI"])).ToString();
            row3["Exceso"] = (Convert.ToDouble(row1["SaldoFinal"]) - Convert.ToDouble(row1["PlanBase"])).ToString();
            row3["Descripcion"] = "MacroServicio";
            oListaStatus.Rows.Add(row3);

            //Sociedad
            DataRow row4 = oListaStatus.NewRow();
            
            row4["PlanBase"] = 0;
            row4["APIsCerradas"] = 0;
            row4["APIsPendientes"] = 0;
            row4["SaldoActual"] = (Convert.ToDouble(row1["PlanBase"]) - Convert.ToDouble(row1["APIsCerradas"]) + Convert.ToDouble(row1["APIsPendientes"])).ToString();
            row4["NuevaAPI"] = oSolicitud.MontoTotal;
            row4["SaldoFinal"] = (Convert.ToDouble(row1["SaldoActual"]) + Convert.ToDouble(row1["NuevaAPI"])).ToString();
            row4["Exceso"] = (Convert.ToDouble(row1["SaldoFinal"]) - Convert.ToDouble(row1["PlanBase"])).ToString();
            row4["Descripcion"] = "Sociedad";
            oListaStatus.Rows.Add(row4);

            return oListaStatus;
        }

        public ListarSolicitudInversionLecturaDTO BuscarPorNumSolicitudLecturaDTO(string IdSolicitud)
        {


            ListarSolicitudInversionLecturaDTO oSolicitudInversion = new ListarSolicitudInversionLecturaDTO();

            ScriptorChannel canalSolicitudInversion =  new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = new ScriptorClient().GetChannel(new Guid(Canales.DetalleSolicitudInversion));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            ScriptorContent oInversion = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();

            RTipoCambioDAL oTipoCambioDAL = new RTipoCambioDAL();

            if (oInversion != null)
            {
                //if (oInversion.Parts.IdTipoAPI.Value.ToString().ToUpper() == TipoAPI)
                //{
                    #region CargarCabecera

                    oSolicitudInversion.CodigoProyecto = oInversion.Parts.CodigoProyecto;
                    oSolicitudInversion.Descripcion = oInversion.Parts.Descripcion;
                    oSolicitudInversion.FechaCierre = oInversion.Parts.FechaCierre;
                    oSolicitudInversion.FechaCreacion = oInversion.Parts.FechaCreacion;
                    oSolicitudInversion.FechaInicio = oInversion.Parts.FechaInicio;
                    oSolicitudInversion.FechaModificacion = oInversion.Parts.FechaModificacion;
                    oSolicitudInversion.FlagPlanBase = int.Parse(oInversion.Parts.FlagPlanBase.ToString());
                    oSolicitudInversion.FlagTipoBolsa = int.Parse(oInversion.Parts.FlagTipoBolsa.ToString());
                    oSolicitudInversion.MontoAprobadoPlanBase = oInversion.Parts.MontoAprobadoPlanBase;
                    oSolicitudInversion.Instancia = oInversion.Parts.IdInstancia;
                    oSolicitudInversion.Id = oInversion.Id.ToString();

                    if (oInversion.Parts.IdCeCo != null && oInversion.Parts.IdCeCo != "")
                    {
                        ScriptorContent contenidoCeCo = canalCeCo.GetContent(new Guid(oInversion.Parts.IdCeCo.ToString()));
                        oSolicitudInversion.DescCeCo = contenidoCeCo.Parts.CodCECO + " " + contenidoCeCo.Parts.DescCECO;
                    }

                    if (oInversion.Parts.IdAPIInicial != null && oInversion.Parts.IdAPIInicial != "")
                    {
                        oSolicitudInversion.IdAPIInicial = oInversion.Parts.IdAPIInicial.ToString();
                    }

                    if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                    {
                        ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                        oSolicitudInversion.DescCoordinador = contenidoCoordinador.Parts.Nombre;                        
                    }
                    if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                    {
                        oSolicitudInversion.DescEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;

                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                    {
                        oSolicitudInversion.DescMacroServicio = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;

                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                    {
                        oSolicitudInversion.DescSector = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oSolicitudInversion.DescSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo + " " + ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;

                        RTipoCambioBE oTipoCambioBE = new RTipoCambioBE();
                        oTipoCambioBE = oTipoCambioDAL.ObtenerTipoCambioPorSociedad(((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString());

                        oSolicitudInversion.DescMoneda = oTipoCambioBE.Moneda;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content != null)
                    {
                        oSolicitudInversion.DescTipoAPI = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoAPI).Content.Parts.Descripcion;

                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content != null)
                    {
                        oSolicitudInversion.DescTipoCapex = ((ScriptorDropdownListValue)oInversion.Parts.IdTipoCapex).Content.Parts.Descripcion;

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

                        if (oInversion.Parts.IdPeriodoPRI == 0 && oInversion.Parts.Pri.ToString() != "0")
                            oSolicitudInversion.DescPeriodoPRI = "Años";
                        else if (oInversion.Parts.IdPeriodoPRI == 1 && oInversion.Parts.Pri.ToString() != "0")
                            oSolicitudInversion.DescPeriodoPRI = "Meses";
                        else
                            oSolicitudInversion.DescPeriodoPRI = "";

                    }
                    oSolicitudInversion.Responsable = oInversion.Parts.Responsable;
                    oSolicitudInversion.ResponsableNombre = ObtenerNombreUsuario(oSolicitudInversion.Responsable);

                    oSolicitudInversion.ResponsableProyecto = oInversion.Parts.ResponsableProyecto;
                    oSolicitudInversion.ResponsableProyectoNombre = ObtenerNombreUsuario(oSolicitudInversion.ResponsableProyecto);

                    oSolicitudInversion.TIR = oInversion.Parts.Tir;
                    oSolicitudInversion.Ubicacion = oInversion.Parts.Ubicacion;

                    oSolicitudInversion.UsuarioCreador = oInversion.Parts.UsuarioCreador;

                    oSolicitudInversion.UsuarioModifico = oInversion.Parts.UsuarioModifico;

                    oSolicitudInversion.VAN = oInversion.Parts.Van;

                    //Ampliaciones
                    oSolicitudInversion.VANAmpliacion = oInversion.Parts.VanAmpliacion;
                    oSolicitudInversion.TIRAmpliacion = oInversion.Parts.TirAmpliacion;
                    oSolicitudInversion.PRIAmpliacion = oInversion.Parts.PriAmpliacion;
                    oSolicitudInversion.ObservacionesFinancierasAmpliacion = oInversion.Parts.ObservacionesFinancierasAmpliacion;
                    oSolicitudInversion.MontoAAmpliar = oInversion.Parts.MontoAAmpliar;
                    if (oInversion.Parts.IdPeriodoPRIAmpliacion != null)
                    {
                        if (oInversion.Parts.IdPeriodoPRIAmpliacion == 0 && oInversion.Parts.PriAmpliacion.ToString() != "0")
                            oSolicitudInversion.DescPeriodoPRIAmpliacion = "Años";
                        else if (oInversion.Parts.IdPeriodoPRIAmpliacion == 1 && oInversion.Parts.PriAmpliacion.ToString() != "0")
                            oSolicitudInversion.DescPeriodoPRIAmpliacion = "Meses";
                        else
                            oSolicitudInversion.DescPeriodoPRIAmpliacion = "";
                    }
                    
                    #endregion

                    return oSolicitudInversion;
            //    }
            }
            else
            {
                return null;
            }

            
        }

        public bool InsertarLogAprobacionMovil(string CuentaUsuario, string NumSolicitud, bool Respuesta)
        {
            bool resultado = false;
            ScriptorChannel canalLogAprobacionMovil = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.LogAprobacionMovil));

            ScriptorContent contenido = canalLogAprobacionMovil.NewContent();

            contenido.Parts.Cuenta = CuentaUsuario;
            contenido.Parts.NumSolicitud = NumSolicitud;
            contenido.Parts.Respuesta = Respuesta.ToString();
            contenido.Parts.Fecha = DateTime.Now;
            resultado = contenido.Save();

            return resultado;
        }

        private bool CambiarEstado(Scriptor.scrEdit objScriptor, Guid idCanal, Guid idContenido, string accion)
        {
            bool salida = false;
            string idRootChannel = idCanal.ToString().ToUpper();
            string txtContentID = idContenido.ToString().ToUpper();
            string txtLang = "pt";
            string txtVer = "1";
            System.Xml.XmlDocument xml_content_edit = objScriptor.simpleGetContentEditXML(idRootChannel, txtContentID, txtLang, txtVer);
            string srcAction = accion;
            //if (item.StateDescription == "Publicado")
            //{
            //    srcAction = "Archivar";
            //}
            //else
            //{
            //    srcAction = "Recuperar";
            //}
            System.Xml.XmlDocument respuesta = null;
            bool conerrorControlado = false;
            try
            {

                respuesta = objScriptor.simplePutContentEditXML(srcAction, xml_content_edit.SelectSingleNode("scrContentEditResponseV001/scrContent"));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("downloading file imagedownload.aspx?"))
                {
                    ManejadorLogSimpleBL.WriteLog("Error al ejecutar el metodo simplePutContentEditXML " + e.Message);
                    conerrorControlado = true;
                }
                else
                {
                    throw new Exception("Error");
                }
            }
            if (conerrorControlado == false)
            {
                if (respuesta.InnerXml.Contains("ErrorMessage=\"No") == false)
                {
                    salida = true;
                }
            }
            else
            {
                salida = true;
            }
            return salida;
        }

        private void LlenarEntidadSolicitudInversion(RSolicitudInversionBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["NumSolicitud"]))
            {
                item.NumSolicitud = Convert.ToString(iDataReader["NumSolicitud"]);
            }
        }

        private void LlenarEntidadDetalleSolicitudInversion(RDetalleSolicitudInversionBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.IdDetalle = (Guid)(iDataReader["Id"]);
            }
         
        }

        public string ObtenerNombreUsuario(string CuentaUsuario)
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

        public List<RDetalleSolicitudInversionBE> ObtenerDetallePorSolicitud(Guid idSolicitudInversion)
        {
            List<RDetalleSolicitudInversionBE> oListaDetalle = new List<RDetalleSolicitudInversionBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetallePorSolicitudInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdSolicitudInversion", idSolicitudInversion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RDetalleSolicitudInversionBE DetalleBE = new RDetalleSolicitudInversionBE();
                        LlenarEntidaDetalle(DetalleBE, dataReader);
                        oListaDetalle.Add(DetalleBE);
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
            return oListaDetalle;
            return null;
        }
        private void LlenarEntidaDetalle(RDetalleSolicitudInversionBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.IdDetalle = (Guid)(iDataReader["Id"]);
            }

        }


        private double CalcularSaldoActual()
        {
            double total = 0;
            RCentroCostoDAL oCeCoDAL = new RCentroCostoDAL();

            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.PlanBase));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));

            double MontoPB = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("Version", oCeCoDAL.ObtenerUltimaVersionMaestro(Canales.PlanBase), "=").Sum(x => x.Parts.MontoBase);

            List<string> oEstados = new List<string>();
            oEstados.Add(Estados.Cerrado);
            oEstados.Add(Estados.Pendiente);

            double MontoSI = canalSolicitudInversion.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("IdEstado", oEstados, "IN").Sum(x => x.Parts.MontoTotal);

            total = MontoPB + MontoSI;

            return total;
        }

        private double CalcularMontoPlanBaseCeCo(string IdCeCo)
        {
            RCentroCostoDAL oCeCoDAL = new RCentroCostoDAL();
            RCentroCostoBE oCeCo = new RCentroCostoBE();
            oCeCo = oCeCoDAL.obtenerCentroCostoPorId(IdCeCo);
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.PlanBase));

            double MontoPB = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("CodCeCo", oCeCo.Codigo, "=").QueryContents("Version", oCeCoDAL.ObtenerUltimaVersionMaestro(Canales.PlanBase), "=").Sum(x => x.Parts.MontoBase);

            return MontoPB;
 
        }

        public bool ValidarEsPlanBase(string CodigoInversion)
        {
            bool res = false;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarEsPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        if (Convert.ToInt32(dataReader["Resultado"]) > 0)
                        {
                            res = true;
                        }
                        
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
            return res;
 
        }

        public double ObtenerMontoAprobado(string CodigoInversion)
        {
            double monto = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIObtenerMontoAprobado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        monto = Convert.ToDouble(dataReader["MontoAprobado"]);
                       
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
            return monto;
 
        }

        
    }
}

