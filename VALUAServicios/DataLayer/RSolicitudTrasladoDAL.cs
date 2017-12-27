using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using BusinessEntities.DTO;
using Common;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;


namespace DataLayer
{
    public class RSolicitudTrasladoDAL
    {

        public string InsertarSolicitudTraslado(RSolicitudTrasladoBE oSolicitudTraslado)
        {
            /*
            string track = "";

            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalCasoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CasoTraslado));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            if (oSolicitudTraslado.NumSolicitud == null)
                return "No se recibieron datos";


            List<string> resultadoDetalle = new List<string>();
            bool resultadoCabecera = false;

            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalle = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));

            ScriptorContent contenidoCabecera = canalCabecera.NewContent();
            ScriptorContent contenidoDetalle;

            try
            {
                
                #region SetearCabecera
                
                //Para traslado
                oSolicitudTraslado.IdTipoAPI = new RTipoAPIBE();
                oSolicitudTraslado.IdTipoAPI.Id = TiposAPI.IdTraslado;

                oSolicitudTraslado.FechaCreacion = DateTime.Now;

                string Correlativo = GenerarCorrelativo(oSolicitudTraslado.IdSociedad.Id.ToString(), oSolicitudTraslado.IdTipoAPI.Id.ToString(), oSolicitudTraslado.FechaCreacion.ToShortDateString());
                track += "genero el correlativo " + Correlativo;

                contenidoCabecera.Parts.NumSolicitud = Correlativo;
                contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdCoordinador = oSolicitudTraslado.IdCoordinador;
                contenidoCabecera.Parts.MontoTotalATrasladar = oSolicitudTraslado.MontoTotalATrasladar;
                contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudTraslado.IdSociedad, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.Comentario = oSolicitudTraslado.Comentario;
                contenidoCabecera.Parts.IdCaso = ScriptorDropdownListValue.FromContent(canalCasoTraslado.QueryContents("#Id", oSolicitudTraslado.IdCaso, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.MontoTotalTraslado = oSolicitudTraslado.MontoTotalTraslado;
                contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oSolicitudTraslado.IdMacroservicio, "=").ToList().FirstOrDefault());
                contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSolicitudTraslado.IdSector, "=").ToList().FirstOrDefault());
                
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

                List<ScriptorContent> oDetallesOK = new List<ScriptorContent>();
                bool detalle = false;

                if (!String.IsNullOrEmpty(id.ToString()))
                {
                    try
                    {
                        #region SetearDetalle
                        foreach (RDetalleSolicitudTrasladoBE det in oSolicitudTraslado.DetalleSolicitudTraslado)
                        {
                            contenidoDetalle = canalDetalle.NewContent();

                            contenidoDetalle.Parts.Tipo = det.Tipo;
                            contenidoDetalle.Parts.MontoATrasladar = det.MontoATrasladar;
                            contenidoDetalle.Parts.IdInversion = det.IdInversion;
                            contenidoDetalle.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(canalTipoActivo.QueryContents("#Id", det.IdTipoActivo, "=").ToList().FirstOrDefault());
                            contenidoDetalle.Parts.Motivo = det.Motivo;
                            contenidoDetalle.Parts.Van = det.Van;
                            contenidoDetalle.Parts.Tir = det.Tir;
                            contenidoDetalle.Parts.Pri = det.Pri;
                            contenidoDetalle.Parts.ObservacionesFinancieras = det.ObservacionesFinancieras;
                            contenidoDetalle.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(contenidoCabecera);


                            track = "Asigno datos de un detalle";
                            detalle = contenidoDetalle.Save();

                            resultadoDetalle.Add(detalle.ToString() + " -- " + contenidoDetalle.LastError.ToString());

                            if (!detalle)
                            {
                                throw new Exception("Error al guardar detalle");
                            }
                            else
                            {
                                oDetallesOK.Add(contenidoDetalle);
                            }

                            track = "Guardo datos de un detalle";
                        }
                        #endregion SetearDetalle
                    }
                    catch (Exception ex)
                    {
                        //Rollback
                        foreach (ScriptorContent item in oDetallesOK)
                        {
                            CambiarEstado(objScriptor, canalDetalle.Id, item.Id, "Borrar");

                        }

                        CambiarEstado(objScriptor, canalCabecera.Id, contenidoCabecera.Id, "Borrar");

                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "cabecera =>" + resultadoCabecera + " detalle =>" + Common.WebUtil.ToJson(resultadoDetalle);

*/
            return null;
        }

        public RSolicitudTrasladoBE BuscarPorNumSolicitud(string NumSolicitud)
        {

            RSolicitudTrasladoBE oSolicitudTraslado = new RSolicitudTrasladoBE();

            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorContent oInversion = canalSolicitudTraslado.QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                    #region CargarCabecera

                    oSolicitudTraslado.Comentario = oInversion.Parts.Comentario;
                    oSolicitudTraslado.FechaCreacion = oInversion.Parts.FechaCreacion;
                    oSolicitudTraslado.Id = oInversion.Id;
                
                    if (((ScriptorDropdownListValue)oInversion.Parts.IdCaso).Content != null)
                    {
                        oSolicitudTraslado.IdCaso = new RCasoTrasladoBE();
                        oSolicitudTraslado.IdCaso.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdCaso).Content.Id;
                        oSolicitudTraslado.IdCaso.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdCaso).Content.Parts.Descripcion;
                        
                    }

                    //if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
                    //{
                    //    oSolicitudTraslado.IdCoordinador = new RCoordinadorBE();
                    //    oSolicitudTraslado.IdCoordinador.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id;
                    //    oSolicitudTraslado.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.CuentaRed;
                    //    oSolicitudTraslado.IdCoordinador.Nombre = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.Nombre;
                    //}

                    if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                    {
                        oSolicitudTraslado.IdCoordinador = new RCoordinadorBE();
                        ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                        oSolicitudTraslado.IdCoordinador.Id = contenidoCoordinador.Id;
                        oSolicitudTraslado.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                        oSolicitudTraslado.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                    {

                        oSolicitudTraslado.IdEstado = new REstadoBE();
                        oSolicitudTraslado.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                        oSolicitudTraslado.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                    {
                        oSolicitudTraslado.IdMacroservicio = new RMacroservicioBE();
                        oSolicitudTraslado.IdMacroservicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                        oSolicitudTraslado.IdMacroservicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                        oSolicitudTraslado.IdMacroservicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                    {
                        oSolicitudTraslado.IdSector = new RSectorBE();
                        oSolicitudTraslado.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                        oSolicitudTraslado.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                        oSolicitudTraslado.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                    }

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oSolicitudTraslado.IdSociedad = new RSociedadBE();
                        oSolicitudTraslado.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                        oSolicitudTraslado.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                        oSolicitudTraslado.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                    }

                    oSolicitudTraslado.IdInstancia = oInversion.Parts.IdInstancia;                    
                    
                    oSolicitudTraslado.MontoTotalATrasladarOrigen = oInversion.Parts.MontoTotalATrasladar;
                    oSolicitudTraslado.MontoTotalATrasladarDestino = oInversion.Parts.MontoTotalTraslado;
                    oSolicitudTraslado.NumSolicitud = oInversion.Parts.NumSolicitud;
               
                    #endregion

                    List<ScriptorContent> oDetalleTraslado = canalDetalleTraslado.QueryContents("IdSolicitudInversion", oInversion.Id, "=").ToList();

                    List<RDetalleSolicitudTrasladoBE> oListaDetalle = new List<RDetalleSolicitudTrasladoBE>();
                    RDetalleSolicitudTrasladoBE oDetalle;


                    foreach (ScriptorContent item in oDetalleTraslado)
                    {
                        oDetalle = new RDetalleSolicitudTrasladoBE();

                        oDetalle.IdInversion = item.Parts.IdInversion;
                        oDetalle.IdTipoActivo = item.Parts.IdTipoActivo;
                        oDetalle.MontoATrasladar = item.Parts.MontoATrasladar;
                        oDetalle.Motivo = item.Parts.Motivo;
                        oDetalle.ObservacionesFinancieras = item.Parts.ObservacionesFinancieras;
                        oDetalle.Pri = item.Parts.Pri;
                        oDetalle.Tipo = item.Parts.Tipo;
                        oDetalle.Tir = item.Parts.Tir;
                        oDetalle.Van = item.Parts.Van;


                        oListaDetalle.Add(oDetalle);

                    }
                    //oSolicitudTraslado.DetalleSolicitudTraslado = oListaDetalle;


                    return oSolicitudTraslado;
                
            }
            else
            {
                return null;
            }

            

        }

        public RSolicitudTrasladoBE BuscarPorId(string IdSolicitud)
        {

            RSolicitudTrasladoBE oSolicitudTraslado = new RSolicitudTrasladoBE();

            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorContent oInversion = canalSolicitudTraslado.QueryContents("#Id", new Guid(IdSolicitud), "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                #region CargarCabecera

                oSolicitudTraslado.Comentario = oInversion.Parts.Comentario;
                oSolicitudTraslado.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudTraslado.Id = oInversion.Id;
                oSolicitudTraslado.IdInstancia = oInversion.Parts.IdInstancia;


                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    oSolicitudTraslado.IdCoordinador = new RCoordinadorBE();
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    oSolicitudTraslado.IdCoordinador.Id = contenidoCoordinador.Id;
                    oSolicitudTraslado.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                    oSolicitudTraslado.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                }

                              
                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudTraslado.IdSociedad = new RSociedadBE();
                    oSolicitudTraslado.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                    oSolicitudTraslado.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudTraslado.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudTraslado.IdEstado = new REstadoBE();
                    oSolicitudTraslado.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                    oSolicitudTraslado.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                oSolicitudTraslado.MontoTotalATrasladarOrigen = oInversion.Parts.MontoTotalATrasladarOrigen;
                oSolicitudTraslado.MontoTotalATrasladarDestino = oInversion.Parts.MontoTotalTrasladoDestino;
                oSolicitudTraslado.NumSolicitud = oInversion.Parts.NumSolicitud;
                oSolicitudTraslado.FechaModificacion = oInversion.Parts.FechaModificacion;
                #endregion

                List<ScriptorContent> oDetalleTraslado = canalDetalleTraslado.QueryContents("IdSolicitudInversionTraslado", oInversion.Id, "=").ToList();

                List<RDetallePrincipalSolicitudTrasladoBE> oListaDetalle = new List<RDetallePrincipalSolicitudTrasladoBE>();
                RDetallePrincipalSolicitudTrasladoBE oDetalle;


                foreach (ScriptorContent item in oDetalleTraslado)
                {
                    oDetalle = new RDetallePrincipalSolicitudTrasladoBE();

                    oDetalle.Id = item.Id;
                    oDetalle.Tipo = item.Parts.Tipo;
                    oDetalle.MontoATrasladar = item.Parts.MontoATrasladar;
                    oDetalle.CodigoProyecto = item.Parts.CodigoProyecto;
                    oDetalle.NombreProyecto = item.Parts.NombreProyecto;
                    oDetalle.PptoAprobado = item.Parts.PptoAprobado;
                    oDetalle.NuevoPpto = item.Parts.NuevoPpto;
                    oDetalle.IdCabeceraOrigen = item.Parts.IdCabeceraOrigen.Value;
                    oDetalle.IdCabeceraDestino = item.Parts.IdCabeceraDestino.Value;
                   
                    oListaDetalle.Add(oDetalle);

                }
                oSolicitudTraslado.DetalleSolicitudTraslado = oListaDetalle;


                return oSolicitudTraslado;

            }
            else
            {
                return null;
            }



        }

        public bool GuardarMontoTraslado(string IdSolicitud, string CuentaUsuario)
        {
            bool success = false;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIGuardarMontoTraslado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitud);
                cmd.Parameters.Add(par1);
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if(dataReader.RecordsAffected > 0)
                    {
                        success = true;

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
            return success;

        }

        public RSolicitudTrasladoBE BuscarPorNumSolicitudTraslado(string NumSolicitud)
        {

            RSolicitudTrasladoBE oSolicitudTraslado = new RSolicitudTrasladoBE();

            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorContent oInversion = canalSolicitudTraslado.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("NumSolicitud",NumSolicitud,"=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                #region CargarCabecera

                oSolicitudTraslado.Comentario = oInversion.Parts.Comentario;
                oSolicitudTraslado.FechaCreacion = oInversion.Parts.FechaCreacion;
                oSolicitudTraslado.Id = oInversion.Id;
                oSolicitudTraslado.IdInstancia = oInversion.Parts.IdInstancia;
                
                //if (((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content != null)
                //{
                //    oSolicitudTraslado.IdCoordinador = new RCoordinadorBE();
                //    oSolicitudTraslado.IdCoordinador.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Id;
                //    oSolicitudTraslado.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.CuentaRed;
                //    oSolicitudTraslado.IdCoordinador.Nombre = ((ScriptorDropdownListValue)oInversion.Parts.IdCoordinador).Content.Parts.Nombre;
                //}

                if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                {
                    oSolicitudTraslado.IdCoordinador = new RCoordinadorBE();
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                    oSolicitudTraslado.IdCoordinador.Id = contenidoCoordinador.Id;
                    oSolicitudTraslado.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                    oSolicitudTraslado.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                {
                    oSolicitudTraslado.IdSociedad = new RSociedadBE();
                    oSolicitudTraslado.IdSociedad.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id;
                    oSolicitudTraslado.IdSociedad.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo;
                    oSolicitudTraslado.IdSociedad.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                }

                if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                {
                    oSolicitudTraslado.IdEstado = new REstadoBE();
                    oSolicitudTraslado.IdEstado.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Id;
                    oSolicitudTraslado.IdEstado.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                }

                oSolicitudTraslado.MontoTotalATrasladarOrigen = oInversion.Parts.MontoTotalATrasladarOrigen;
                oSolicitudTraslado.MontoTotalATrasladarDestino = oInversion.Parts.MontoTotalTrasladoDestino;
                oSolicitudTraslado.NumSolicitud = oInversion.Parts.NumSolicitud;
                oSolicitudTraslado.FechaModificacion = oInversion.Parts.FechaModificacion;
                #endregion

                List<ScriptorContent> oDetalleTraslado = canalDetalleTraslado.QueryContents("IdSolicitudInversionTraslado", oInversion.Id, "=").ToList();

                List<RDetallePrincipalSolicitudTrasladoBE> oListaDetalle = new List<RDetallePrincipalSolicitudTrasladoBE>();
                RDetallePrincipalSolicitudTrasladoBE oDetalle;


                foreach (ScriptorContent item in oDetalleTraslado)
                {
                    oDetalle = new RDetallePrincipalSolicitudTrasladoBE();

                    oDetalle.Id = item.Id;
                    oDetalle.Tipo = item.Parts.Tipo;
                    oDetalle.MontoATrasladar = item.Parts.MontoATrasladar;
                    oDetalle.CodigoProyecto = item.Parts.CodigoProyecto;
                    oDetalle.NombreProyecto = item.Parts.NombreProyecto;
                    oDetalle.PptoAprobado = item.Parts.PptoAprobado;
                    oDetalle.NuevoPpto = item.Parts.NuevoPpto;
                    oDetalle.IdCabeceraOrigen = item.Parts.IdCabeceraOrigen.Value;
                    oDetalle.IdCabeceraDestino = item.Parts.IdCabeceraDestino.Value;

                    oListaDetalle.Add(oDetalle);

                }
                oSolicitudTraslado.DetalleSolicitudTraslado = oListaDetalle;


                return oSolicitudTraslado;

            }
            else
            {
                return null;
            }



        }

        public List<RSolicitudTrasladoBE> ObtenerSolicitudesCreadas(string loginName, string IdTipoAPI, string NumSolicitud, string FechaDel, string FechaAl)
        {
            
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            List<RSolicitudTrasladoBE> oListaSolicitud = new List<RSolicitudTrasladoBE>();
            List<ScriptorContent> oLista = new List<ScriptorContent>();
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            if (NumSolicitud == "undefined")
                NumSolicitud = null;

            if (IdTipoAPI == null)
                IdTipoAPI = "Todos";

            if (IdTipoAPI.ToUpper() == TiposAPI.IdTraslado || IdTipoAPI == "Todos" || String.IsNullOrEmpty(IdTipoAPI) || IdTipoAPI == "undefined")
            {
                ScriptorQueryEnumerable<ScriptorContent> oInversion = null;

                if (!String.IsNullOrEmpty(loginName))
                {
                    ScriptorChannel canalCoordinadores = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
                    ScriptorContent coordinador = canalCoordinadores.QueryContents("CuentaRed", loginName, "=").ToList().FirstOrDefault();

                    if (coordinador != null)
                    {
                        oInversion = canalSolicitudTraslado.QueryContents("IdCoordinador", coordinador.Id, "=");

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

            RSolicitudTrasladoBE oSolicitud = null;

            foreach (ScriptorContent item in oLista)
            {
                oSolicitud = new RSolicitudTrasladoBE();

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
                
                //if (((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content != null)
                //{
                //    oSolicitud.IdCoordinador = new RCoordinadorBE();
                //    oSolicitud.IdCoordinador.Id = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Id;
                //    oSolicitud.IdCoordinador.CuentaRed = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Parts.CuentaRed;
                //    oSolicitud.IdCoordinador.Nombre = ((ScriptorDropdownListValue)item.Parts.IdCoordinador).Content.Parts.Nombre;
                //}


                if (item.Parts.IdCoordinador != null && item.Parts.IdCoordinador != "")
                {
                    oSolicitud.IdCoordinador = new RCoordinadorBE();
                    ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(item.Parts.IdCoordinador.ToString()));
                    oSolicitud.IdCoordinador.Id = contenidoCoordinador.Id;
                    oSolicitud.IdCoordinador.Nombre = contenidoCoordinador.Parts.Nombre;
                    oSolicitud.IdCoordinador.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                }

                oSolicitud.FechaCreacion = item.Parts.FechaCreacion;
                oSolicitud.FechaModificacion = item.Parts.FechaModificacion;

                oListaSolicitud.Add(oSolicitud);

            }

            return oListaSolicitud;

        }

        public List<ListarSolicitudesTrasladoCreadasDTO> ObtenerSolicitudesCreadas(string loginName, string NumSolicitud, string FechaDel, string FechaAl)
        {
            List<ListarSolicitudesTrasladoCreadasDTO> oLista = new List<ListarSolicitudesTrasladoCreadasDTO>();

            ListarSolicitudesTrasladoCreadasDTO oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesCreadasTraslados";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@cuenta", loginName);
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
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoCreadasDTO();
                        oSolicitudInversionTrasladoBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionTrasladoBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionTrasladoBE.MontoTrasladoUSD = dataReader["MontoTrasladoUSD"].ToString();

                        oLista.Add(oSolicitudInversionTrasladoBE);

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

        public List<ListarSolicitudesTrasladoCreadasDTO> ObtenerSolicitudesTrasladoAdministracion(string NombreCortoSolicitud, string IdTipoActivo, string NumSolicitud, string CodigoProyecto, string IdEstado1,
                                                                                  string NombreProyecto, string IdEstado2, string IdSociedad, string IdTipoCapex, string IdCeCo, string IdCoordinador, string IdSector,
                                                                                  string ResponsableInv, string IdMacroservicio, string FechaDel, string FechaAl, int Rol)
        {
            List<ListarSolicitudesTrasladoCreadasDTO> oLista = new List<ListarSolicitudesTrasladoCreadasDTO>();
            ListarSolicitudesTrasladoCreadasDTO oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesTrasladoAdministracion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;

                
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
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoCreadasDTO(); 
                        oSolicitudInversionTrasladoBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionTrasladoBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionTrasladoBE.MontoTrasladoUSD = dataReader["MontoTrasladarUSD"].ToString();
                        oLista.Add(oSolicitudInversionTrasladoBE);

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

        public List<ListarSolicitudesTrasladoAdministracionExportar> ObtenerSolicitudesTrasladoAdministracionExportar(string NombreCortoSolicitud, string IdTipoActivo, string NumSolicitud, string CodigoProyecto, string IdEstado1,
                                                                                  string NombreProyecto, string IdEstado2, string IdSociedad, string IdTipoCapex, string IdCeCo, string IdCoordinador, string IdSector,
                                                                                  string ResponsableInv, string IdMacroservicio, string FechaDel, string FechaAl,int Rol)
        {
            List<ListarSolicitudesTrasladoAdministracionExportar> oLista = new List<ListarSolicitudesTrasladoAdministracionExportar>();
            ListarSolicitudesTrasladoAdministracionExportar oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesTrasladoAdministracionExportar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1;


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
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoAdministracionExportar();
                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.CeCo = dataReader["DescripcionCeCo"].ToString();
                        oSolicitudInversionTrasladoBE.Macroservicio = dataReader["DescripcionMacroservicio"].ToString();
                        oSolicitudInversionTrasladoBE.Sector = dataReader["DescripcionSector"].ToString();
                        oSolicitudInversionTrasladoBE.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Estado = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreacion = dataReader["FechaCreacion"].ToString();
                        oSolicitudInversionTrasladoBE.FechaModificacion = dataReader["FechaModificacion"].ToString();
                        oSolicitudInversionTrasladoBE.GerenciaCentral = dataReader["GerenciaCentral"].ToString();
                        oSolicitudInversionTrasladoBE.MontoATrasladarUSD = dataReader["MontoATrasladarUSD"].ToString();
                        oSolicitudInversionTrasladoBE.NuevoPptoUSD = dataReader["NuevoPptoUSD"].ToString();
                        oSolicitudInversionTrasladoBE.PptoAprobadoUSD = dataReader["PptoAprobadoUSD"].ToString();
                        oSolicitudInversionTrasladoBE.Tipo = dataReader["Tipo"].ToString();
                        oSolicitudInversionTrasladoBE.CodOI = dataReader["OrdenInversion"].ToString();
                        
                        oLista.Add(oSolicitudInversionTrasladoBE);

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

        public List<ListarSolicitudesTrasladoCreadasExportarDTO> ObtenerSolicitudesCreadasExportar(string loginName, string NumSolicitud, string FechaDel, string FechaAl)
        {
            List<ListarSolicitudesTrasladoCreadasExportarDTO> oLista = new List<ListarSolicitudesTrasladoCreadasExportarDTO>();

            ListarSolicitudesTrasladoCreadasExportarDTO oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesCreadasTraslados";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@cuenta", loginName);
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
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoCreadasExportarDTO();
                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreado = dataReader["FechaCreacion"].ToString();
                        oSolicitudInversionTrasladoBE.FechaModifico = dataReader["FechaModificacion"].ToString();
                        oSolicitudInversionTrasladoBE.MontoTrasladoUSD = dataReader["MontoTrasladoUSD"].ToString();

                        oLista.Add(oSolicitudInversionTrasladoBE);

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

        public ListarSolicitudesTrasladoCreadasDTO ObtenerTareasAprobacion(string Id)
        {
            ListarSolicitudesTrasladoCreadasDTO oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesTrasladoPorInstancia";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", Convert.ToInt32(Id));
                cmd.Parameters.Add(par1);

                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoCreadasDTO();

                        oSolicitudInversionTrasladoBE.Id = dataReader["Id"].ToString();
                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreado = Convert.ToDateTime(dataReader["FechaCreacion"]);
                        oSolicitudInversionTrasladoBE.FechaModifico = Convert.ToDateTime(dataReader["FechaModificacion"]);
                        oSolicitudInversionTrasladoBE.MontoTrasladoUSD = dataReader["MontoTrasladoUSD"].ToString();

                        return oSolicitudInversionTrasladoBE;

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
            return null;

        }

        public ListarSolicitudesTrasladoCreadasExportarDTO ObtenerTareasAprobacionExportar(string Id)
        {
            ListarSolicitudesTrasladoCreadasExportarDTO oSolicitudInversionTrasladoBE;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerSolicitudesTrasladoPorInstancia";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdInstancia", Convert.ToInt32(Id));
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSolicitudInversionTrasladoBE = new ListarSolicitudesTrasladoCreadasExportarDTO();

                        oSolicitudInversionTrasladoBE.NumSolicitud = dataReader["NumSolicitud"].ToString();
                        oSolicitudInversionTrasladoBE.Estado1 = dataReader["Estado"].ToString();
                        oSolicitudInversionTrasladoBE.Estado2 = dataReader["Estado2"].ToString();
                        oSolicitudInversionTrasladoBE.Sociedad = dataReader["Sociedad"].ToString();
                        oSolicitudInversionTrasladoBE.Coordinador = dataReader["Coordinador"].ToString();
                        oSolicitudInversionTrasladoBE.FechaCreado = dataReader["FechaCreacion"].ToString();
                        oSolicitudInversionTrasladoBE.FechaModifico = dataReader["FechaModificacion"].ToString();
                        oSolicitudInversionTrasladoBE.MontoTrasladoUSD = dataReader["MontoTrasladoUSD"].ToString();

                        return oSolicitudInversionTrasladoBE;

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
            return null;

        }
        public string AnularSolicitudTraslado(string NumSolicitud)
        {
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;
            string mensaje = "";

            ScriptorContent oSolicitud = canalSolicitudTraslado.QueryContents("NumSolicitud", NumSolicitud, "=").ToList().FirstOrDefault();
            if (oSolicitud != null)
            {
                if (((ScriptorDropdownListValue)oSolicitud.Parts.IdEstado).Content.Id.ToString().ToUpper() == Estados.Creado)
                {
                    List<ScriptorContent> oDetalles = canalDetalleSolicitudTraslado.QueryContents("IdSolicitudInversion", oSolicitud.Id, "=").ToList();
                    List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();

                    try
                    {                       

                        //resultado = oSolicitud.Save("Borrar");
                        resultadoCabecera = CambiarEstado(objScriptor, canalSolicitudTraslado.Id, oSolicitud.Id, "Borrar");
                        if (!resultadoCabecera)
                            throw new Exception("Error al anular cabecera");

                        foreach (ScriptorContent item in oDetalles)
                        {
                            if (!CambiarEstado(objScriptor, canalDetalleSolicitudTraslado.Id, item.Id, "Borrar"))
                            {
                                throw new Exception("Error al anular detalle");
                            }
                            else
                            {
                                oDetallesOk.Add(item);
                            }
                        }
                    }
                    catch(Exception ex) 
                    {
                        //RollBack
                        foreach (ScriptorContent item in oDetallesOk)
                        {
                            CambiarEstado(objScriptor, canalDetalleSolicitudTraslado.Id, item.Id, "Recuperar");
                        }

                        CambiarEstado(objScriptor, canalSolicitudTraslado.Id, oSolicitud.Id, "Recuperar");
                        mensaje = "Error, rollback";
                    }                    
                }
                else
                    mensaje = "No se puede eliminar la solicitud";
            }

            return resultadoCabecera + " - " + mensaje;

        }

        public List<RSolicitudTrasladoBE> ObtenerTareasAprobacion(string loginName, string IdEstado, string NumSolicitud, string FechaDel, string FechaAl)
        {
            return null;
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

            track += "concatenó sociedad tipo api y fecha";

            if (tipoAPI.Parts.Correlativo != 0)
            {
                num = Convert.ToString(tipoAPI.Parts.Correlativo + 1);//.ToString();
                Correlativo += num.PadLeft(4, '0');
            }
            else
                Correlativo += "0001";
            
            return Correlativo;

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
        public RResultadoTrasladoCabeceraDestinoBE InsertarDetalleTrasladoDestino(RTrasladoCabeceraDestinoBE oCabeceraDestinoTraslado)
        {
            RResultadoTrasladoCabeceraDestinoBE oRespuesta = new RResultadoTrasladoCabeceraDestinoBE();
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));

            ScriptorContent contenidoDetalleTraslado = null;
            if (oCabeceraDestinoTraslado.DetalleSolicitudTrasladoBE.Id != null)
            {
                contenidoDetalleTraslado = canalDetalleTraslado.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", oCabeceraDestinoTraslado.DetalleSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoDetalleTraslado = canalDetalleTraslado.NewContent();
            }

            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                                       .QueryContents("Id", oCabeceraDestinoTraslado.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();

            try
            {
                contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                contenidoDetalleTraslado.Parts.Tipo = Constantes.TipoDestino;
                contenidoDetalleTraslado.Parts.MontoATrasladar = oCabeceraDestinoTraslado.MontoATrasladarUSD;
                contenidoDetalleTraslado.Parts.CodigoProyecto = oCabeceraDestinoTraslado.CodigoProyecto;
                contenidoDetalleTraslado.Parts.NombreProyecto = oCabeceraDestinoTraslado.NombreProyecto;
                contenidoDetalleTraslado.Parts.PptoAprobado = oCabeceraDestinoTraslado.PptoAprobadoUSD;
                contenidoDetalleTraslado.Parts.NuevoPpto = oCabeceraDestinoTraslado.NuevoPtoUSD;
                //contenidoDetalleTraslado.Parts.IdCabeceraOrigen = new Guid();
                //contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoCabeceraDestino);
                oRespuesta.success = contenidoDetalleTraslado.Save();
                oRespuesta.IdDetalleSolicitudTraslado = contenidoDetalleTraslado.Id.ToString();
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
        }

        public RResultadoTrasladoCabeceraDestinoBE InsertarTrasladoCabeceraDestino(RTrasladoCabeceraDestinoBE oCabeceraDestinoTraslado)
        {
            RResultadoTrasladoCabeceraDestinoBE oRespuesta = new RResultadoTrasladoCabeceraDestinoBE();
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCabeceraTrasladoDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorContent contenidoCabeceraDestino = null;
            if (oCabeceraDestinoTraslado.Id != new Guid())
            {
                contenidoCabeceraDestino = canalCabeceraTrasladoDestino.QueryContents("Id", new Guid(), "<>")
                                                            .QueryContents("Id", oCabeceraDestinoTraslado.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoCabeceraDestino = canalCabeceraTrasladoDestino.NewContent();
            }
            ScriptorContent contenidoDetalleTraslado = null;
            if (oCabeceraDestinoTraslado.DetalleSolicitudTrasladoBE.Id != null)
            {
                contenidoDetalleTraslado = canalDetalleTraslado.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", oCabeceraDestinoTraslado.DetalleSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoDetalleTraslado = canalDetalleTraslado.NewContent();
            }

            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                                       .QueryContents("Id", oCabeceraDestinoTraslado.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            try
            {
                contenidoCabeceraDestino.Parts.CodigoProyecto = oCabeceraDestinoTraslado.CodigoProyecto;
                contenidoCabeceraDestino.Parts.NombreProyecto = oCabeceraDestinoTraslado.NombreProyecto;
                contenidoCabeceraDestino.Parts.PptAprobadoUSD = oCabeceraDestinoTraslado.PptoAprobadoUSD;
                contenidoCabeceraDestino.Parts.MontoATrasladarUSD = oCabeceraDestinoTraslado.MontoATrasladarUSD;
                contenidoCabeceraDestino.Parts.NuevoPptoUSD = oCabeceraDestinoTraslado.NuevoPtoUSD;
                contenidoCabeceraDestino.Parts.Motivo = oCabeceraDestinoTraslado.Motivo;
                contenidoCabeceraDestino.Parts.Van = oCabeceraDestinoTraslado.Van;
                contenidoCabeceraDestino.Parts.Tir = oCabeceraDestinoTraslado.Tir;
                contenidoCabeceraDestino.Parts.Pri = oCabeceraDestinoTraslado.Pri;
                contenidoCabeceraDestino.Parts.IdCeCo = oCabeceraDestinoTraslado.IdCeCo;

                contenidoCabeceraDestino.Parts.ObservacionesFinancieras = oCabeceraDestinoTraslado.ObservacionesFinancieras;
                contenidoCabeceraDestino.Parts.IdPriTraslado = oCabeceraDestinoTraslado.IdPriTraslado;
                oRespuesta.success = contenidoCabeceraDestino.Save();
                oRespuesta.Id = contenidoCabeceraDestino.Id.ToString();

                if (oRespuesta.success)
                {
                    contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                    contenidoDetalleTraslado.Parts.Tipo = Constantes.TipoDestino;
                    contenidoDetalleTraslado.Parts.MontoATrasladar = oCabeceraDestinoTraslado.MontoATrasladarUSD;
                    contenidoDetalleTraslado.Parts.CodigoProyecto = oCabeceraDestinoTraslado.CodigoProyecto;
                    contenidoDetalleTraslado.Parts.NombreProyecto = oCabeceraDestinoTraslado.NombreProyecto;
                    contenidoDetalleTraslado.Parts.PptoAprobado = oCabeceraDestinoTraslado.PptoAprobadoUSD;
                    contenidoDetalleTraslado.Parts.NuevoPpto = oCabeceraDestinoTraslado.NuevoPtoUSD;
                    //contenidoDetalleTraslado.Parts.IdCabeceraOrigen = new Guid();
                    contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoCabeceraDestino);
                    oRespuesta.success = contenidoDetalleTraslado.Save();
                    oRespuesta.IdDetalleSolicitudTraslado = contenidoDetalleTraslado.Id.ToString();
                    
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
        }

        public RResultadoTrasladoCabeceraOrigenBE InsertarTrasladoCabeceraOrigen(RTrasladoCabeceraOrigenBE oCabeceraOrigenTraslado, string CuentaUsuario)
        {
            RResultadoTrasladoCabeceraOrigenBE oRespuesta = new RResultadoTrasladoCabeceraOrigenBE();
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCabeceraTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorContent contenidoCabeceraOrigen = null;
            if (oCabeceraOrigenTraslado.Id != new Guid())
            {
                contenidoCabeceraOrigen = canalCabeceraTrasladoOrigen.QueryContents("Id", new Guid(), "<>")
                                                            .QueryContents("Id", oCabeceraOrigenTraslado.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoCabeceraOrigen = canalCabeceraTrasladoOrigen.NewContent();
            }
            ScriptorContent contenidoDetalleTraslado = null;
            if (oCabeceraOrigenTraslado.DetalleSolicitudTrasladoBE.Id != null)
            {
                contenidoDetalleTraslado = canalDetalleTraslado.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", oCabeceraOrigenTraslado.DetalleSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoDetalleTraslado = canalDetalleTraslado.NewContent();
            }

            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                                       .QueryContents("Id", oCabeceraOrigenTraslado.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            try
            {
                contenidoSolicitudTraslado.Parts.UsuarioModifico=CuentaUsuario;
                contenidoSolicitudTraslado.Parts.FechaModificacion = DateTime.Now;

                contenidoCabeceraOrigen.Parts.CodigoProyecto = oCabeceraOrigenTraslado.CodigoProyecto;
                contenidoCabeceraOrigen.Parts.NombreProyecto = oCabeceraOrigenTraslado.NombreProyecto;
                contenidoCabeceraOrigen.Parts.PptAprobadoUSD = oCabeceraOrigenTraslado.PptoAprobadoUSD;
                contenidoCabeceraOrigen.Parts.MontoATrasladarUSD = oCabeceraOrigenTraslado.MontoATrasladarUSD;
                contenidoCabeceraOrigen.Parts.IdCeCo = oCabeceraOrigenTraslado.IdCeCo;
                contenidoCabeceraOrigen.Parts.NuevoPptoUSD = oCabeceraOrigenTraslado.NuevoPtoUSD;
                oRespuesta.success = contenidoCabeceraOrigen.Save();
                oRespuesta.Id = contenidoCabeceraOrigen.Id.ToString();

                if (oRespuesta.success)
                {
                    contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                    contenidoDetalleTraslado.Parts.Tipo = Constantes.TipoOrigen;
                    contenidoDetalleTraslado.Parts.MontoATrasladar = oCabeceraOrigenTraslado.MontoATrasladarUSD;
                    contenidoDetalleTraslado.Parts.CodigoProyecto = oCabeceraOrigenTraslado.CodigoProyecto;
                    contenidoDetalleTraslado.Parts.NombreProyecto = oCabeceraOrigenTraslado.NombreProyecto;
                    contenidoDetalleTraslado.Parts.PptoAprobado = oCabeceraOrigenTraslado.PptoAprobadoUSD;
                    contenidoDetalleTraslado.Parts.NuevoPpto = oCabeceraOrigenTraslado.NuevoPtoUSD;
                    contenidoDetalleTraslado.Parts.IdCabeceraOrigen = ScriptorDropdownListValue.FromContent(contenidoCabeceraOrigen);
                   // contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoCabeceraDestino);
                    oRespuesta.success = contenidoDetalleTraslado.Save();
                    oRespuesta.IdDetalleSolicitudTraslado = contenidoDetalleTraslado.Id.ToString();
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
        }
        public RResultadoTrasladoCabeceraOrigenBE ModificarTrasladoCabeceraOrigen(RTrasladoCabeceraOrigenBE oCabeceraOrigenTraslado, string CuentaUsuario)
        {
            RResultadoTrasladoCabeceraOrigenBE oRespuesta = new RResultadoTrasladoCabeceraOrigenBE();
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCabeceraTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorContent contenidoCabeceraOrigen = canalCabeceraTrasladoOrigen.QueryContents("Id", new Guid(), "<>")
                                                        .QueryContents("Id", oCabeceraOrigenTraslado.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoDetalleTraslado =canalDetalleTraslado.QueryContents("Id", new Guid(), "<>")
                                 .QueryContents("Id", oCabeceraOrigenTraslado.DetalleSolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                                       .QueryContents("Id", oCabeceraOrigenTraslado.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            try
            {
                contenidoSolicitudTraslado.Parts.UsuarioModifico = CuentaUsuario;
                contenidoSolicitudTraslado.Parts.FechaModificacion = DateTime.Now;
                //contenidoCabeceraOrigen.Parts.CodigoProyecto = oCabeceraOrigenTraslado.CodigoProyecto;
                //contenidoCabeceraOrigen.Parts.NombreProyecto = oCabeceraOrigenTraslado.NombreProyecto;
                contenidoCabeceraOrigen.Parts.PptAprobadoUSD = oCabeceraOrigenTraslado.PptoAprobadoUSD;
                contenidoCabeceraOrigen.Parts.MontoATrasladarUSD = oCabeceraOrigenTraslado.MontoATrasladarUSD;
                contenidoCabeceraOrigen.Parts.NuevoPptoUSD = oCabeceraOrigenTraslado.NuevoPtoUSD;
                oRespuesta.success = contenidoCabeceraOrigen.Save();
                oRespuesta.Id = contenidoCabeceraOrigen.Id.ToString();

                if (oRespuesta.success)
                {
                 // contenidoDetalleTraslado.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                 // contenidoDetalleTraslado.Parts.Tipo = Constantes.TipoOrigen;
                    contenidoDetalleTraslado.Parts.MontoATrasladar = oCabeceraOrigenTraslado.MontoATrasladarUSD;
                 // contenidoDetalleTraslado.Parts.CodigoProyecto = oCabeceraOrigenTraslado.CodigoProyecto;
                 // contenidoDetalleTraslado.Parts.NombreProyecto = oCabeceraOrigenTraslado.NombreProyecto;
                    contenidoDetalleTraslado.Parts.PptoAprobado = oCabeceraOrigenTraslado.PptoAprobadoUSD;
                    contenidoDetalleTraslado.Parts.NuevoPpto = oCabeceraOrigenTraslado.NuevoPtoUSD;
                 // contenidoDetalleTraslado.Parts.IdCabeceraOrigen = ScriptorDropdownListValue.FromContent(contenidoCabeceraOrigen);
                 // contenidoDetalleTraslado.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoCabeceraDestino);
                    oRespuesta.success = contenidoDetalleTraslado.Save();
                    oRespuesta.IdDetalleSolicitudTraslado = contenidoDetalleTraslado.Id.ToString();
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
        }

        public bool InsertarTrasladoDetalleDestino(RTrasladoDetalleDestinoBE det)
        {
            bool resultado = false;
            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCabeceraTrasladoDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalDetalleTrasladoDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalTipoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoMovimiento));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));

            ScriptorContent contenidoDetalleDestino = null;

            if (det.Id != null)
            {
                contenidoDetalleDestino = canalDetalleTrasladoDestino.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoDetalleDestino = canalDetalleTrasladoDestino.NewContent();
            }
            ScriptorContent contenidoInversion = null;
            if (!String.IsNullOrEmpty(det.InversionBE.Id))
            {
                contenidoInversion = canalInversion.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", new Guid(det.InversionBE.Id), "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoInversion = canalInversion.NewContent();
            }
            ScriptorContent contenidoMovimiento = null;
            if (det.MovimientoBE.Id != null)
            {
                contenidoMovimiento = canalMovimiento.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.MovimientoBE.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoMovimiento = canalMovimiento.NewContent();
            }

            ScriptorContent contenidoTipoActivo = canalTipoActivo.QueryContents("Id", new Guid(), "<>")
                                                               .QueryContents("Id", det.TipoActivoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoTipoCambio = canalTipoCambio.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("IdMoneda", det.TipoCambioBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoCeCo = canalCeCo.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("Id", det.CentroCostoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoEstado = canalEstadoMovimiento.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                            .QueryContents("Id", det.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoTipoMovimiento = canalTipoMovimiento.QueryContents("Id", new Guid(), "<>")
                                           .QueryContents("Id",TiposMovimiento.API, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoSolicitudInversion = null;
            //Verificar si es proyecto nuevo
            if(det.SolicitudInversionBE.Id!=null)
            { 
                contenidoSolicitudInversion = canalSolicitudInversion.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("Id", det.SolicitudInversionBE.Id, "=").ToList().FirstOrDefault();
            }

            try
            {
                if (det.MontoATrasladar == 0)
                {
                    EliminarTrasladoDetalleOrigenDestino(det.Id.ToString());
                }
                else
                { 
                    contenidoInversion.Parts.CodigoOI = det.CodigoOI;
                    contenidoInversion.Parts.MontoDisponible = det.MontoATrasladar;
                    contenidoInversion.Parts.MontoContable = det.MontoATrasladar;
                    contenidoInversion.Parts.Descripcion = det.NumSolicitud;

                    if(contenidoTipoActivo != null)
                        contenidoInversion.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(contenidoTipoActivo);

                    contenidoInversion.Parts.NombreProyecto = det.NombreProyecto;
                    contenidoInversion.Parts.DescripcionOI = det.DescripcionOI;

                    //if(contenidoCeCo != null)
                    //    contenidoInversion.Parts.IdCeCo = ScriptorDropdownListValue.FromContent(contenidoCeCo);

                    contenidoInversion.Parts.IdCeCo = det.IdCeCo;
                    contenidoInversion.Parts.CodigoProyecto = det.CodigoProyecto;
                    contenidoInversion.Parts.Version = 0;

                    if(contenidoEstado != null)
                        contenidoInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(contenidoEstado); ;

                    resultado = contenidoInversion.Save();

                    if (resultado)
                    {
                        contenidoMovimiento.Parts.Monto = det.MontoATrasladar;
                        contenidoMovimiento.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contenidoInversion);
                        contenidoMovimiento.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(contenidoEstado);
                        contenidoMovimiento.Parts.FechaMovimiento = DateTime.Now;
                        contenidoMovimiento.Parts.Descripcion = det.NumSolicitud;
                        contenidoMovimiento.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                        contenidoMovimiento.Parts.IdTipoMovimiento = ScriptorDropdownListValue.FromContent(contenidoTipoMovimiento);
                        if (det.SolicitudInversionBE.Id != null)
                        { 
                            contenidoMovimiento.Parts.IdSolicitudInversion = ScriptorDropdownListValue.FromContent(contenidoSolicitudInversion);
                        }
                        resultado = contenidoMovimiento.Save();

                        if (resultado)
                        {
                            ScriptorContent contenidoCabeceraDestinoTraslado = canalCabeceraTrasladoDestino.QueryContents("Id", new Guid(), "<>")
                                                            .QueryContents("Id", det.CabeceraDestinoBE.Id, "=").ToList().FirstOrDefault();
                         
                            if(contenidoTipoActivo != null)
                                contenidoDetalleDestino.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(contenidoTipoActivo);

                            if(contenidoTipoCambio != null)
                                contenidoDetalleDestino.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(contenidoTipoCambio);

                            contenidoDetalleDestino.Parts.PptAprobado = det.PptAprobado;
                            contenidoDetalleDestino.Parts.VidaUtil = det.VidaUtil;

                            if(contenidoInversion != null)
                                contenidoDetalleDestino.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contenidoInversion);

                            if(contenidoCabeceraDestinoTraslado != null)
                                contenidoDetalleDestino.Parts.IdCabeceraDestino = ScriptorDropdownListValue.FromContent(contenidoCabeceraDestinoTraslado);

                            contenidoDetalleDestino.Parts.NuevoPptoMonedaCotizada = det.NuevoPptoMonedaCotizada;
                            contenidoDetalleDestino.Parts.MontoATrasladar = det.MontoATrasladar;
                            contenidoDetalleDestino.Parts.NuevoPptoOIUSD = det.NuevoPptoOIUSD;
                            resultado = contenidoDetalleDestino.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog(ex.Message.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.StackTrace.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.InnerException.ToString());
                resultado = false;
                return resultado;
            }
            return resultado;
        }

        public bool EliminarTrasladoDetalle(string IdDetalleTraslado, string IdSolicitudInversion)
        {
            bool res = false;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIEliminarDetalleTraslado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalleTraslado", IdDetalleTraslado);
                cmd.Parameters.Add(par1);

                if (IdSolicitudInversion == "")
                    par1 = new SqlParameter("@IdSolicitudInversion", DBNull.Value);
                else
                    par1 = new SqlParameter("@IdSolicitudInversion", IdSolicitudInversion);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if(dataReader.RecordsAffected > 0)                    
                    {
                        res = true;
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

        public bool EliminarTrasladoDetalleOrigenDestino(string IdDetalleTraslado)
        {
            bool res = false;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIEliminarDetalleTrasladoOrigenDestino";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalleTrasladoOrigenDestino", IdDetalleTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.RecordsAffected > 0)
                    {
                        res = true;
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

        public bool InsertarTrasladoDetalleOrigen(RTrasladoDetalleOrigenBE det)
        {
            bool resultado = false;

            ScriptorChannel canalSolicitudTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalCeCo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));
            ScriptorChannel canalCabeceraTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));
            ScriptorChannel canalDetalleTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleOrigenTraslado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalTipoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoMovimiento));
            ScriptorChannel canalMoneda = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Moneda));

            ScriptorContent contenidoDetalleOrigen = null;
            if (det.Id != null)
            {
                contenidoDetalleOrigen = canalDetalleTrasladoOrigen.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoDetalleOrigen = canalDetalleTrasladoOrigen.NewContent();
            }
            ScriptorContent contenidoInversion = null;
            if (!String.IsNullOrEmpty(det.InversionBE.Id))
            {
                contenidoInversion = canalInversion.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", new Guid(det.InversionBE.Id), "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoInversion = canalInversion.NewContent();
            }
            ScriptorContent contenidoMovimiento = null;
            if (det.MovimientoBE.Id != null)
            {
                contenidoMovimiento = canalMovimiento.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.MovimientoBE.Id, "=").ToList().FirstOrDefault();
            }
            else
            {
                contenidoMovimiento = canalMovimiento.NewContent();
            }

            ScriptorContent contenidoTipoActivo = canalTipoActivo.QueryContents("Id", new Guid(), "<>")
                                                               .QueryContents("Id", det.TipoActivoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoTipoCambio = canalTipoCambio.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("IdMoneda", det.TipoCambioBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoCeCo = canalCeCo.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("Id", det.CentroCostoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoEstado = canalEstadoMovimiento.QueryContents("Id", new Guid(), "<>")
                                                                .QueryContents("Id", EstadosMovimiento.Creado, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoSolicitudTraslado = canalSolicitudTraslado.QueryContents("Id", new Guid(), "<>")
                                            .QueryContents("Id", det.SolicitudTrasladoBE.Id, "=").ToList().FirstOrDefault();
            ScriptorContent contenidoTipoMovimiento = canalTipoMovimiento.QueryContents("Id", new Guid(), "<>")
                                           .QueryContents("Id", TiposMovimiento.API, "=").ToList().FirstOrDefault();
            try
            {
                contenidoInversion.Parts.CodigoOI = det.CodigoOI;
                contenidoInversion.Parts.MontoDisponible = det.MontoATrasladar;
                contenidoInversion.Parts.MontoContable = det.MontoATrasladar;
                contenidoInversion.Parts.Descripcion = det.NumSolicitud;
                if (contenidoTipoActivo != null)
                {
                    contenidoInversion.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(contenidoTipoActivo);
                }
                contenidoInversion.Parts.NombreProyecto = det.NombreProyecto;
                contenidoInversion.Parts.DescripcionOI = det.DescripcionOI;
                if (contenidoCeCo != null)
                {
                    //contenidoInversion.Parts.IdCeCo = ScriptorDropdownListValue.FromContent(contenidoCeCo);
                    contenidoInversion.Parts.IdCeCo = contenidoCeCo.Id.ToString();
                }
                contenidoInversion.Parts.CodigoProyecto = det.CodigoProyecto;
                contenidoInversion.Parts.Version = 0;
                if (contenidoEstado != null)
                {
                    contenidoInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(contenidoEstado); ;
                }
                resultado = contenidoInversion.Save();

                if (resultado)
                {
                    contenidoMovimiento.Parts.Monto = det.MontoATrasladar * -1;
                    if (contenidoInversion != null)
                    {
                        contenidoMovimiento.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contenidoInversion);
                    }
                    if (contenidoEstado != null)
                    {
                        contenidoMovimiento.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(contenidoEstado);
                    }
                    contenidoMovimiento.Parts.FechaMovimiento = DateTime.Now;
                    contenidoMovimiento.Parts.Descripcion = det.NumSolicitud;
                    if (contenidoSolicitudTraslado != null)
                    {
                        contenidoMovimiento.Parts.IdSolicitudInversionTraslado = ScriptorDropdownListValue.FromContent(contenidoSolicitudTraslado);
                    }
                    if (contenidoTipoMovimiento != null)
                    {
                        contenidoMovimiento.Parts.IdTipoMovimiento = ScriptorDropdownListValue.FromContent(contenidoTipoMovimiento);
                    }
                    resultado = contenidoMovimiento.Save();

                    if (resultado)
                    {
                        ScriptorContent contenidoCabeceraOrigenTraslado = canalCabeceraTrasladoOrigen.QueryContents("Id", new Guid(), "<>")
                                                        .QueryContents("Id", det.CabeceraOrigenBE.Id, "=").ToList().FirstOrDefault();
                        if (contenidoTipoActivo != null)
                        {
                            contenidoDetalleOrigen.Parts.IdTipoActivo = ScriptorDropdownListValue.FromContent(contenidoTipoActivo);
                        }
                        if (contenidoTipoCambio != null)
                        {
                            contenidoDetalleOrigen.Parts.IdTipoCambio = ScriptorDropdownListValue.FromContent(contenidoTipoCambio);
                        }
                        contenidoDetalleOrigen.Parts.PptAprobado = det.PptAprobado;
                        contenidoDetalleOrigen.Parts.VidaUtil = det.VidaUtil;
                        if (contenidoInversion != null)
                        {
                            contenidoDetalleOrigen.Parts.IdInversion = ScriptorDropdownListValue.FromContent(contenidoInversion);
                        }
                        if (contenidoCabeceraOrigenTraslado != null)
                        {
                            contenidoDetalleOrigen.Parts.IdCabeceraOrigen = ScriptorDropdownListValue.FromContent(contenidoCabeceraOrigenTraslado);
                        }
                        contenidoDetalleOrigen.Parts.NuevoPptoMonedaCotizada = det.NuevoPptoMonedaCotizada;
                        contenidoDetalleOrigen.Parts.NuevoPptoOIUSD = det.NuevoPptoOIUSD;
                        resultado = contenidoDetalleOrigen.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog(ex.Message.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.StackTrace.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.InnerException.ToString());
                resultado = false;
                return resultado;
            }
            return resultado;
        }
        public bool ModificarTrasladoDetalleOrigen(RTrasladoDetalleOrigenBE det)
        {
            bool resultado = false;
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalCabeceraTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorChannel canalDetalleTrasladoOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleOrigenTraslado));


            ScriptorContent contenidoDetalleOrigen = canalDetalleTrasladoOrigen.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.Id, "=").ToList().FirstOrDefault();
  
            ScriptorContent contenidoInversion = canalInversion.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", new Guid(det.InversionBE.Id), "=").ToList().FirstOrDefault();
           
            ScriptorContent contenidoMovimiento = canalMovimiento.QueryContents("Id", new Guid(), "<>")
                                     .QueryContents("Id", det.MovimientoBE.Id, "=").ToList().FirstOrDefault();
        
            try
            {
                if (det.MontoATrasladar == 0)
                {
                    EliminarTrasladoDetalleOrigenDestino(det.Id.ToString());
                }
                else
                {
                    contenidoInversion.Parts.MontoDisponible = det.MontoATrasladar;
                    contenidoInversion.Parts.MontoContable = det.MontoATrasladar;
                    resultado = contenidoInversion.Save();

                    if (resultado)
                    {
                        contenidoMovimiento.Parts.Monto = det.MontoATrasladar * -1;
                        contenidoMovimiento.Parts.FechaMovimiento = DateTime.Now;
                        contenidoMovimiento.Parts.Descripcion = det.NumSolicitud;
                        resultado = contenidoMovimiento.Save();

                        if (resultado)
                        {
                            contenidoDetalleOrigen.Parts.PptAprobado = det.PptAprobado;
                            contenidoDetalleOrigen.Parts.VidaUtil = det.VidaUtil;
                            contenidoDetalleOrigen.Parts.NuevoPptoMonedaCotizada = det.NuevoPptoMonedaCotizada;
                            contenidoDetalleOrigen.Parts.NuevoPptoOIUSD = det.NuevoPptoOIUSD;
                            resultado = contenidoDetalleOrigen.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog(ex.Message.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.StackTrace.ToString());
                ManejadorLogSimpleBL.WriteLog(ex.InnerException.ToString());
                resultado = false;
                return resultado;
            }
            return resultado;
        }

        public RResultadoTrasladoBE ValidarGerenteCentralOrigen(string CodigoInversion, string CodigoInversionOld)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarGerenteCentralOrigen";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoInversionOld", CodigoInversionOld);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        if (Convert.ToInt32(dataReader["GerenteCentral"]) > 0)
                            oResultado.success = true;
                        else
                            oResultado.success = false;

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
            return oResultado;
 
        }

        public RResultadoTrasladoBE ValidarGerenteCentralLineaDestino(string CodigoInversion, string CodigoVersionOld)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarGerenteCentralLineaDestino";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoInversionOld", CodigoVersionOld);
                cmd.Parameters.Add(par1);

                //par1 = new SqlParameter("@GerenteLinea", GerenteLinea);
                //cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        if (Convert.ToInt32(dataReader["GerenteCentralLinea"]) > 0)
                            oResultado.success = true;
                        else
                            oResultado.success = false;

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
            return oResultado;

        }
        public RResultadoTrasladoBE ValidarGerenteCentralLineaDestinoProyectoNuevo(string Coordinador,string IdCeco, string CodigoVersionOld)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarGerenteCentralLineaDestinoProyectoNuevo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdCeco", IdCeco);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@CodigoInversionOld", CodigoVersionOld);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@Coordinador", Coordinador);
                cmd.Parameters.Add(par1);
                //par1 = new SqlParameter("@GerenteLinea", GerenteLinea);
                //cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        if (Convert.ToInt32(dataReader["GerenteCentralLinea"]) > 0)
                            oResultado.success = true;
                        else
                            oResultado.success = false;

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
            return oResultado;

        }
                    
        public RResultadoTrasladoBE InsertarCabecera(RSolicitudTrasladoBE oSolicitudTraslado, string CuentaUsuario)
        {
            string track = "";

            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();

            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalCasoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CasoTraslado));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            //Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");
                        
            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            
            ScriptorContent contenidoCabecera = canalCabecera.NewContent();
            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RCoordinadorBE oCoordinador = new RCoordinadorBE();
            try
            {

                #region SetearCabecera
                //Para traslado
                oSolicitudTraslado.IdTipoAPI = new RTipoAPIBE();
                oSolicitudTraslado.IdTipoAPI.Id = TiposAPI.IdTraslado;

                oSolicitudTraslado.FechaCreacion = DateTime.Now;

                string Correlativo = GenerarCorrelativo(oSolicitudTraslado.IdSociedad.Id.ToString(), oSolicitudTraslado.IdTipoAPI.Id.ToString(), oSolicitudTraslado.FechaCreacion.ToShortDateString());
                track += "genero el correlativo " + Correlativo;
                
                contenidoCabecera.Parts.NumSolicitud = Correlativo;
                contenidoCabecera.Parts.FechaCreacion = DateTime.Now;
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioModifico = CuentaUsuario;
                contenidoCabecera.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Creado, "=").ToList().FirstOrDefault());

                //if(oSolicitudTraslado.IdCoordinador != null)
                    //contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudTraslado.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());


                if (oSolicitudTraslado.IdCoordinador != null)
                {
                    oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorRedUltimaVersion(oSolicitudTraslado.IdCoordinador.CuentaRed);
                    //ScriptorContent contCoordinadores = canalCoordinador.QueryContents("#Id", oCoordinador.Id.Value, "=").ToList().FirstOrDefault();
                    //contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(contCoordinadores);
                    contenidoCabecera.Parts.IdCoordinador = oCoordinador.Id.Value.ToString();
                }

                //contenidoCabecera.Parts.MontoTotalATrasladarOrigen = oSolicitudTraslado.MontoTotalATrasladarOrigen;

                if (oSolicitudTraslado.IdSociedad != null)
                    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudTraslado.IdSociedad.Id, "=").ToList().FirstOrDefault());

                contenidoCabecera.Parts.Comentario = oSolicitudTraslado.Comentario;

                if (oSolicitudTraslado.IdCaso != null)
                    contenidoCabecera.Parts.IdCaso = ScriptorDropdownListValue.FromContent(canalCasoTraslado.QueryContents("#Id", oSolicitudTraslado.IdCaso.Id, "=").ToList().FirstOrDefault());

                //contenidoCabecera.Parts.MontoTotalTrasladoDestino = oSolicitudTraslado.MontoTotalATrasladarDestino;

                //if (oSolicitudTraslado.IdMacroservicio != null)
                //    contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oSolicitudTraslado.IdMacroservicio.Id, "=").ToList().FirstOrDefault());

                //if (oSolicitudTraslado.IdSector != null)
                //    contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSolicitudTraslado.IdSector.Id, "=").ToList().FirstOrDefault());

                #endregion SetearCabecera


                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oResultado.success = contenidoCabecera.Save();

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera
                Guid? id = null;
                if (oResultado.success)
                {
                    oResultado.Id = contenidoCabecera.Id.ToString();
                    oResultado.NumSolicitud = Correlativo;

                    oResultado.CoordinadorCuenta = oSolicitudTraslado.IdCoordinador.CuentaRed;
                    oResultado.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                    //oResultado.CoordinadorNombre = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdCoordinador).Content.Parts.Nombre;
                    oResultado.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    oResultado.FechaCreacion = oSolicitudTraslado.FechaCreacion;
                    oResultado.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();


                    #region IncrementarCorrelativo

                    ScriptorContent ContenidoTipoAPI = canalTipoAPI.QueryContents("#Id", TiposAPI.IdTraslado, "=").ToList().FirstOrDefault();
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
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog("Error al guardar traslado:::: " + ex.Message);
            }

            return oResultado;

 
        }
        public int ActualizarEstadoInversionBD(string IdInversion, string IdEstado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            int resultado = 0;
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
               
                    string nombreProcedure = "ActualizarEstadoInversion";
                    SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter par1 = new SqlParameter("@IdInversion", IdInversion);
                    cmd.Parameters.Add(par1);

                    SqlParameter par2 = new SqlParameter("@IdEstado", IdEstado);
                    cmd.Parameters.Add(par2);

                    resultado=cmd.ExecuteNonQuery();
           
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
        public bool ActualizarInversion(RSolicitudTrasladoBE oSolicitud, string IdEstado)
        {
            bool Resultado = false;

            foreach (RDetallePrincipalSolicitudTrasladoBE item in oSolicitud.DetalleSolicitudTraslado)
            {
                List<DetalleOrigenDestinoDTO> olista = new List<DetalleOrigenDestinoDTO>();
                
                if(!String.IsNullOrEmpty(item.IdCabeceraOrigen))
                    olista = ObtenerDetalleOrigenEdicion(item.Id.ToString());

                if (!String.IsNullOrEmpty(item.IdCabeceraDestino))
                    olista = ObtenerDetalleDestinoEdicion(item.Id.ToString());

                foreach (DetalleOrigenDestinoDTO oitem in olista)
                {
                    if (ActualizarEstadoInversionBD(oitem.IdInversion, IdEstado) > 0)
                    {
                        Resultado= true;
                    }
                    else
                    {
                        Resultado = false;
                        return Resultado;
                    }
                        
                }
            }
            return Resultado;
        }
        public RResultadoTrasladoBE ValidarPptoAprobadorUSDPorCodigoOI(RSolicitudTrasladoBE oSolicitud)
        {
            RResultadoTrasladoBE Resultado = new RResultadoTrasladoBE();

            foreach (RDetallePrincipalSolicitudTrasladoBE item in oSolicitud.DetalleSolicitudTraslado)
            {
                List<DetalleOrigenDestinoDTO> olista = ObtenerDetalleOrigenEdicion(item.IdCabeceraOrigen);

                foreach(DetalleOrigenDestinoDTO oitem in olista)
                {
                    if ((oitem.MontoATrasladar * -1)>ObtenerPptoAprobadoUSDPorCodigoOI(oitem.NumeroOI))
                    {
                        Resultado.success = false;
                        return Resultado;
                    }
                    else
                    {
                        Resultado.success = true;
                    }
                }
            }
            return Resultado;
        }

        public double ObtenerPptoAprobadoUSDPorCodigoOI(string CodigoOI)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            double Monto = 0.0;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerPptoAprobadoUSDPorCodigoOI";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoOI);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Monto = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
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
            return Monto;
        }
        public RResultadoTrasladoBE ModificarCabecera(RSolicitudTrasladoBE oSolicitudTraslado, string CuentaUsuario)
        {
            string track = "";

            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();

            ScriptorChannel canalSociedad = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sociedad));
            ScriptorChannel canalTipoActivo = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoActivo));
            ScriptorChannel canalCasoTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CasoTraslado));
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));

            //Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            ScriptorChannel canalCabecera = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));

            ScriptorContent contenidoCabecera = canalCabecera.QueryContents("#Id", oSolicitudTraslado.Id, "=").ToList().FirstOrDefault();

            RCoordinadorDAL oCoordinadorDAL = new RCoordinadorDAL();
            RCoordinadorBE oCoordinador = new RCoordinadorBE();
            try
            {
                #region SetearCabecera
                //Para traslado
                                
                contenidoCabecera.Parts.FechaModificacion = DateTime.Now;
                contenidoCabecera.Parts.UsuarioModifico = CuentaUsuario;

                //if (oSolicitudTraslado.IdCoordinador != null)
                  //  contenidoCabecera.Parts.IdCoordinador = ScriptorDropdownListValue.FromContent(canalCoordinador.QueryContents("CuentaRed", oSolicitudTraslado.IdCoordinador.CuentaRed, "=").QueryContents("Version", oCoordinadorDAL.ObtenerUltimaVersionMaestro(Canales.Coordinador), "=").ToList().FirstOrDefault());
                oCoordinador = oCoordinadorDAL.ObtenerCoordinadorPorCuentaPorRedUltimaVersion(oSolicitudTraslado.IdCoordinador.CuentaRed);

                //contenidoCabecera.Parts.MontoTotalATrasladarOrigen = oSolicitudTraslado.MontoTotalATrasladarOrigen;

                if (oSolicitudTraslado.IdSociedad != null)
                    contenidoCabecera.Parts.IdSociedad = ScriptorDropdownListValue.FromContent(canalSociedad.QueryContents("#Id", oSolicitudTraslado.IdSociedad.Id, "=").ToList().FirstOrDefault());

                contenidoCabecera.Parts.Comentario = oSolicitudTraslado.Comentario;

                if (oSolicitudTraslado.IdCaso != null)
                    contenidoCabecera.Parts.IdCaso = ScriptorDropdownListValue.FromContent(canalCasoTraslado.QueryContents("#Id", oSolicitudTraslado.IdCaso.Id, "=").ToList().FirstOrDefault());

                //contenidoCabecera.Parts.MontoTotalTrasladoDestino = oSolicitudTraslado.MontoTotalATrasladarDestino;

                //if (oSolicitudTraslado.IdMacroservicio != null)
                //    contenidoCabecera.Parts.IdMacroservicio = ScriptorDropdownListValue.FromContent(canalMacroservicio.QueryContents("#Id", oSolicitudTraslado.IdMacroservicio.Id, "=").ToList().FirstOrDefault());

                //if (oSolicitudTraslado.IdSector != null)
                //    contenidoCabecera.Parts.IdSector = ScriptorDropdownListValue.FromContent(canalSector.QueryContents("#Id", oSolicitudTraslado.IdSector.Id, "=").ToList().FirstOrDefault());

                #endregion SetearCabecera


                track = "Asigno variables a cabecera";

                //Guardar en Scriptor
                oResultado.success = contenidoCabecera.Save();

                track = "Guardo variables de cabecera";

                //Obtener Id de la cabecera
                Guid? id = null;
                if (oResultado.success)
                {
                    oResultado.Id = contenidoCabecera.Id.ToString();
                    oResultado.NumSolicitud = contenidoCabecera.Parts.NumSolicitud;

                    oResultado.CoordinadorCuenta = oSolicitudTraslado.IdCoordinador.CuentaRed;
                    oResultado.CoordinadorNombre = canalCoordinador.GetContent(oCoordinador.Id.Value).Parts.Nombre;
                    oResultado.DescEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Parts.Descripcion;
                    oResultado.FechaCreacion = oSolicitudTraslado.FechaCreacion;
                    oResultado.IdEstado = ((ScriptorDropdownListValue)contenidoCabecera.Parts.IdEstado).Content.Id.ToString();


                }

            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog("Error al editar traslado:::: " + ex.Message);
            }

            return oResultado;
        }

        public bool AnularTraslado(string IdTraslado, string CuentaUsuario)
        {
            ScriptorChannel canalTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalCabeceraOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorChannel canalCabeceraDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorChannel canalDetalleDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));
            ScriptorChannel canalDetalleOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleOrigenTraslado));
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversion));
            ScriptorChannel canalDetalleSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleSolicitudInversion));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalTraslado.QueryContents("#Id", IdTraslado, "=").ToList().FirstOrDefault();
            if (oSolicitud != null)
            {
                if (((ScriptorDropdownListValue)oSolicitud.Parts.IdEstado).Content.Id.ToString().ToUpper() == Estados.Creado)
                {
                    //Apago
                    List<ScriptorContent> oDetalles = canalDetalleTraslado.QueryContents("IdSolicitudInversionTraslado", oSolicitud.Id, "=").ToList();
                    List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                    try
                    {

                        resultadoCabecera = CambiarEstado(objScriptor, canalTraslado.Id, oSolicitud.Id, "Borrar");
                        if (!resultadoCabecera)
                            throw new Exception("Error al anular cabecera");

                        foreach (ScriptorContent item in oDetalles)
                        {
                            if (!CambiarEstado(objScriptor, canalDetalleTraslado.Id, item.Id, "Borrar"))
                            {
                                throw new Exception("Error al anular detalle");
                            }
                            else
                            {
                                oDetallesOk.Add(item);
                            }

                            #region BorrarCabeceraOrigen
                            ScriptorContent contentCabeceraOrigen = ((ScriptorDropdownListValue)item.Parts.IdCabeceraOrigen).Content;

                            if (contentCabeceraOrigen != null)
                            {
                                CambiarEstado(objScriptor, new Guid(Canales.CabeceraOrigenTraslado), contentCabeceraOrigen.Id, "Borrar");

                                #region Borrar DetalleOrigen
                                List<ScriptorContent> oDetalleOrigen = canalDetalleOrigen.QueryContents("IdCabeceraOrigen", contentCabeceraOrigen.Id, "=").ToList();

                                foreach (ScriptorContent x in oDetalleOrigen)
                                {
                                    CambiarEstado(objScriptor, new Guid(Canales.DetalleOrigenTraslado), x.Id, "Borrar");

                                    #region BorrarInversion
                                    ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                                    if (contentInversion != null)
                                    {
                                        CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");

                                        #region BorrarMovimiento
                                        ScriptorContent contentMovimiento = canalMovimiento.QueryContents("IdInversion", contentInversion.Id, "=").ToList().FirstOrDefault();
                                        if (contentMovimiento != null)
                                        {
                                            CambiarEstado(objScriptor, new Guid(Canales.Movimientos), contentMovimiento.Id, "Borrar");
                                        }
                                        #endregion

                                    }
                                    #endregion

                                    

                                }

                                #endregion

                            }

                            #endregion

                            


                            #region BorrarCabeceraDestino
                            ScriptorContent contentCabeceraDestino = ((ScriptorDropdownListValue)item.Parts.IdCabeceraDestino).Content;

                            if (contentCabeceraDestino != null)
                            {
                                CambiarEstado(objScriptor, new Guid(Canales.CabeceraDestinoTraslado), contentCabeceraDestino.Id, "Borrar");

                                #region BorrarDetalleDestino
                              
                                List<ScriptorContent> oDetalleDestino = canalDetalleDestino.QueryContents("IdCabeceraDestino", contentCabeceraDestino.Id, "=").ToList();
                                
                                foreach (ScriptorContent x in oDetalleDestino)
                                {
                                    CambiarEstado(objScriptor, new Guid(Canales.DetalleDestinoTraslado), x.Id, "Borrar");

                                    #region BorrarInversion
                                    ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                                    if (contentInversion != null)
                                    {
                                        CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");

                                        #region BorrarMovimiento
                                        ScriptorContent contentMovimiento = canalMovimiento.QueryContents("IdInversion", contentInversion.Id, "=").ToList().FirstOrDefault();
                                        if (contentMovimiento != null)
                                        {
                                            CambiarEstado(objScriptor, new Guid(Canales.Movimientos), contentMovimiento.Id, "Borrar");
                                        }
                                        #endregion

                                    }
                                    #endregion

                                }
                                #endregion

                            }
                            #endregion

                            


                        }
                    }
                    catch (Exception ex)
                    {
                        //RollBAck
                        ManejadorLogSimpleBL.WriteLog("Error al borrar::::" + ex.Message);
                    }
                }
                else if (((ScriptorDropdownListValue)oSolicitud.Parts.IdEstado).Content.Id.ToString().ToUpper() != Estados.Cerrado)
                {
                    //Cambio estado a anulado
                    //Solo Administradores

                    List<ScriptorContent> oDetalles = canalDetalleTraslado.QueryContents("IdSolicitudInversionTraslado", oSolicitud.Id, "=").ToList();
                    List<ScriptorContent> oDetallesOk = new List<ScriptorContent>();
                    try
                    {

                        oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", Estados.Anulado, "=").ToList().FirstOrDefault());                        
                        resultadoCabecera = oSolicitud.Save();

                        if (!resultadoCabecera)
                            throw new Exception("Error al anular cabecera");

                        foreach (ScriptorContent item in oDetalles)
                        {

                            if (item.Parts.IdCabeceraOrigen.Content != null)
                            {
                                List<ScriptorContent> oDetalleOrigen = canalDetalleOrigen.QueryContents("IdCabeceraOrigen", ((ScriptorDropdownListValue)item.Parts.IdCabeceraOrigen).Content.Id, "=").ToList();

                                foreach (ScriptorContent x in oDetalleOrigen)
                                {                                
                                    #region RechazarInversion
                                    ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                                    if (contentInversion != null)
                                    {
                                        contentInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                        contentInversion.Save();
                                    }                                
                                    #endregion

                                    #region RechazarMovimiento
                                    ScriptorContent contentMovimiento = canalMovimiento.QueryContents("IdInversion", contentInversion.Id, "=").ToList().FirstOrDefault();
                                    if (contentMovimiento != null)
                                    {
                                        contentMovimiento.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                        contentMovimiento.Save();
                                    }
                                    #endregion

                                }
                            }

                            if (item.Parts.IdCabeceraDestino.Content != null)
                            { 

                                List<ScriptorContent> oDetalleDestino = canalDetalleDestino.QueryContents("IdCabeceraDestino", ((ScriptorDropdownListValue)item.Parts.IdCabeceraDestino).Content.Id, "=").ToList();

                                foreach (ScriptorContent x in oDetalleDestino)
                                {

                                    #region RechazarInversion
                                    ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                                    if (contentInversion != null)
                                    {
                                        contentInversion.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                        contentInversion.Save();
                                    }
                                    #endregion

                                    #region RechazarMovimiento
                                    ScriptorContent contentMovimiento = canalMovimiento.QueryContents("IdInversion", contentInversion.Id, "=").ToList().FirstOrDefault();
                                    if (contentMovimiento != null)
                                    {
                                        contentMovimiento.Parts.IdEstadoMovimiento = ScriptorDropdownListValue.FromContent(canalEstadoMovimiento.QueryContents("#Id", EstadosMovimiento.Rechazado, "=").ToList().FirstOrDefault());
                                        contentMovimiento.Save();
                                    }
                                    #endregion

                                }
                            }

                        }                        

                    }
                    catch (Exception ex)
                    {
                        //RollBAck
                        ManejadorLogSimpleBL.WriteLog("Error al anular Traslado:::: " + ex.Message);
                    }

                }
                else
                    ManejadorLogSimpleBL.WriteLog("No se puede anular la solciitud");
            }

            return resultadoCabecera;            
 
        }

        public bool AnularDetalle(string IdDetalle, string CuentaUsuario)
        {
            //ScriptorChannel canalTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Inversion));
            //ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));
            ScriptorChannel canalEstadoMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.EstadoMovimiento));
            ScriptorChannel canalMovimiento = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            ScriptorChannel canalCabeceraOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraOrigenTraslado));
            ScriptorChannel canalCabeceraDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.CabeceraDestinoTraslado));
            ScriptorChannel canalDetalleDestino = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleDestinoTraslado));
            ScriptorChannel canalDetalleOrigen = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleOrigenTraslado));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            //bool resultadoCabecera = false;
            bool resultadoDetalle = false;

            //ScriptorContent oSolicitud = canalTraslado.QueryContents("#Id", IdTraslado, "=").ToList().FirstOrDefault();
            ScriptorContent oDetalle = canalDetalleTraslado.QueryContents("#Id", IdDetalle, "=").ToList().FirstOrDefault();

            if (oDetalle != null)
            {
                if (((ScriptorDropdownListValue)oDetalle.Parts.IdSolicitudInversion).Content.Id.ToString().ToUpper() == Estados.Creado)
                {
                    try
                    {
                    //Apago
                    resultadoDetalle = CambiarEstado(objScriptor, canalDetalleTraslado.Id, oDetalle.Id, "Borrar");
                    #region BorrarCabeceraOrigen
                    ScriptorContent contentCabeceraOrigen = ((ScriptorDropdownListValue)oDetalle.Parts.IdCabeceraOrigen).Content;

                    if (contentCabeceraOrigen != null)
                    {
                        CambiarEstado(objScriptor, new Guid(Canales.CabeceraOrigenTraslado), contentCabeceraOrigen.Id, "Borrar");

                    }

                    #endregion

                    #region Borrar DetalleOrigen
                    List<ScriptorContent> oDetalleOrigen = canalDetalleOrigen.QueryContents("IdCabeceraOrigen", contentCabeceraOrigen.Id, "=").ToList();

                    foreach (ScriptorContent x in oDetalleOrigen)
                    {
                        CambiarEstado(objScriptor, new Guid(Canales.DetalleOrigenTraslado), x.Id, "Borrar");

                        #region BorrarInversion
                        ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                        if (contentInversion != null)
                        {
                            CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");

                        }
                        #endregion

                    }

                    #endregion


                    #region BorrarCabeceraDestino
                    ScriptorContent contentCabeceraDestino = ((ScriptorDropdownListValue)oDetalle.Parts.IdCabeceraDestino).Content;

                    if (contentCabeceraDestino != null)
                    {
                        CambiarEstado(objScriptor, new Guid(Canales.CabeceraDestinoTraslado), contentCabeceraDestino.Id, "Borrar");

                    }
                    #endregion

                    #region BorrarDetalleDestino
                    List<ScriptorContent> oDetalleDestino = canalDetalleDestino.QueryContents("IdCabeceraDestino", contentCabeceraDestino.Id, "=").ToList();

                    foreach (ScriptorContent x in oDetalleDestino)
                    {
                        CambiarEstado(objScriptor, new Guid(Canales.DetalleDestinoTraslado), x.Id, "Borrar");

                        #region BorrarInversion
                        ScriptorContent contentInversion = ((ScriptorDropdownListValue)x.Parts.IdInversion).Content;

                        if (contentInversion != null)
                        {
                            CambiarEstado(objScriptor, new Guid(Canales.Inversion), contentInversion.Id, "Borrar");

                        }
                        #endregion

                    }
                    #endregion

                        
                    }
                    catch (Exception ex)
                    {
                        //RollBAck
                        ManejadorLogSimpleBL.WriteLog("Error al borrar::::" + ex.Message);
                    }
                }
                
                else
                    ManejadorLogSimpleBL.WriteLog("No se puede anular la solciitud");
            }

            return resultadoDetalle;

        }

        public ListarSolicitudTrasladoLecturaDTO BuscarPorNumSolicitudLectura(string IdTraslado)
        {

            ListarSolicitudTrasladoLecturaDTO oSolicitudTraslado = new ListarSolicitudTrasladoLecturaDTO();

            ScriptorChannel canalSolicitudTraslado = new ScriptorClient().GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalDetalleTraslado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.DetalleTraslado));
            ScriptorChannel canalCoordinador = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Coordinador));
            ScriptorContent oInversion = canalSolicitudTraslado.QueryContents("#Id", IdTraslado, "=").ToList().FirstOrDefault();


            if (oInversion != null)
            {
                    #region CargarCabecera

                    oSolicitudTraslado.Comentarios = oInversion.Parts.Comentario;
                    oSolicitudTraslado.FechaCreacion = oInversion.Parts.FechaCreacion;
                    oSolicitudTraslado.Id = oInversion.Id.ToString();
                                    
                    if (((ScriptorDropdownListValue)oInversion.Parts.IdCaso).Content != null)
                    {
                        oSolicitudTraslado.IdCaso = ((ScriptorDropdownListValue)oInversion.Parts.IdCaso).Content.Parts.Codigo;
                        
                    }

                    if (oInversion.Parts.IdCoordinador != null && oInversion.Parts.IdCoordinador != "")
                    {                        
                        ScriptorContent contenidoCoordinador = canalCoordinador.GetContent(new Guid(oInversion.Parts.IdCoordinador.ToString()));
                        oSolicitudTraslado.DescCoordinador = contenidoCoordinador.Parts.Nombre;
                        oSolicitudTraslado.CuentaRed = contenidoCoordinador.Parts.CuentaRed;
                    }
                    if (((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content != null)
                    {

                        oSolicitudTraslado.DescEstado = ((ScriptorDropdownListValue)oInversion.Parts.IdEstado).Content.Parts.Descripcion;
                    }

                    /*if (((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content != null)
                    {
                        oSolicitudTraslado.IdMacroservicio = new RMacroservicioBE();
                        oSolicitudTraslado.IdMacroservicio.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Id;
                        oSolicitudTraslado.IdMacroservicio.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Descripcion;
                        oSolicitudTraslado.IdMacroservicio.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdMacroservicio).Content.Parts.Codigo;
                    }*/

                    /*if (((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content != null)
                    {
                        oSolicitudTraslado.IdSector = new RSectorBE();
                        oSolicitudTraslado.IdSector.Id = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Id;
                        oSolicitudTraslado.IdSector.Codigo = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Codigo;
                        oSolicitudTraslado.IdSector.Descripcion = ((ScriptorDropdownListValue)oInversion.Parts.IdSector).Content.Parts.Descripcion;
                    }*/

                    if (((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content != null)
                    {
                        oSolicitudTraslado.IdSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Id.ToString();
                        oSolicitudTraslado.DescSociedad = ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Codigo + " " +  ((ScriptorDropdownListValue)oInversion.Parts.IdSociedad).Content.Parts.Descripcion;
                    }
    
                    oSolicitudTraslado.MontoTotalATrasladarOrigen = oInversion.Parts.MontoTotalATrasladarOrigen;
                    oSolicitudTraslado.MontoTotalATrasladarDestino = oInversion.Parts.MontoTotalTrasladoDestino;
                    oSolicitudTraslado.NumSolicitud = oInversion.Parts.NumSolicitud;
               
                    #endregion

                    return oSolicitudTraslado;
                
            }
            else
            {
                return null;
            }

            

        }

        public CabeceraOrigenTrasladoDTO BuscarCabeceraOrigenDestino(string CodigoInversion)
        {
            CabeceraOrigenTrasladoDTO oCabecera = new CabeceraOrigenTrasladoDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraOrigenDestinoPorCodigoInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCabecera.CeCo = dataReader["CeCo"].ToString();
                        oCabecera.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oCabecera.Macroservicio = dataReader["Macroservicio"].ToString(); ;
                        oCabecera.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oCabecera.PptoAprobadoUSD = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
                        oCabecera.Sector = dataReader["Sector"].ToString();
                        oCabecera.Sociedad = dataReader["Sociedad"].ToString();
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
                        
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
            return oCabecera;
 
        }

        public RResultadoTrasladoBE ValidarGerenteCentralLineaProyectoNuevo(string idTraslado, string idCeCo)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarGerenteCentralLineaProyectoNuevo";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@idTraslado", idTraslado);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@idCeCo", idCeCo);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        if(Convert.ToInt32(dataReader["Respuesta"]) > 0)
                            oResultado.success = true;
                        else
                            oResultado.success = false;

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
            return oResultado;

        }

        public List<CeCoOrigenDestino> ObtenerInversionOrigenDestino(string idTraslado)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            
            List<CeCoOrigenDestino> oLista = new List<CeCoOrigenDestino>();

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIObtenerInversionOrigenDestino";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdTraslado", idTraslado);
                cmd.Parameters.Add(par1);
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CeCoOrigenDestino oitem = new CeCoOrigenDestino();

                        oitem.IdCeCo = dataReader["IdCeCo"].ToString();
                        oitem.Tipo = dataReader["Tipo"].ToString();
                        oitem.CodigoOI = dataReader["CodigoOI"].ToString();

                        oLista.Add(oitem);

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

        public RResultadoTrasladoBE ValidarGerenteCentralLineaTraslado(string idTraslado)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();

            List<CeCoOrigenDestino> oLista = new List<CeCoOrigenDestino>();

            oLista = ObtenerInversionOrigenDestino(idTraslado).Where( x => x.Tipo == "Destino").ToList();

            if (oLista.Count > 1)
            {
                for (int i = 1; i < oLista.Count; i++)
                {

                    //oResultado = ValidarGerenteCentralLineaDestino(oLista[i].CodigoOI, oLista[0].CodigoOI);

                    oResultado = ValidarGerenteCentralLineaProyectoNuevo(idTraslado, oLista[i].IdCeCo);
                    if (!oResultado.success)
                        break;

                }
            }
            else
                oResultado.success = true;

            return oResultado;

        }

        public RResultadoTrasladoBE ValidarGerenteCentralTraslado(string idTraslado)
        {
            RResultadoTrasladoBE oResultado = new RResultadoTrasladoBE();

            List<CeCoOrigenDestino> oLista = new List<CeCoOrigenDestino>();

            oLista = ObtenerInversionOrigenDestino(idTraslado).Where(x => x.Tipo == "Origen").ToList();
            if (oLista.Count != 1)
            {
                for (int i = 1; i < oLista.Count; i++)
                {

                    oResultado = ValidarGerenteCentralOrigen(oLista[i].CodigoOI, oLista[0].CodigoOI);
                    if (!oResultado.success)
                        break;

                }
            }
            else
                oResultado.success = true;

            return oResultado;

        }

        public List<CabeceraOrigenTrasladoDTO> ObtenerCabeceraOrigenExportar(string IdCabeceraOrigen)
        {
            List<CabeceraOrigenTrasladoDTO> oListCabecera = new List<CabeceraOrigenTrasladoDTO>();
            CabeceraOrigenTrasladoDTO oCabecera = new CabeceraOrigenTrasladoDTO();
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraOrigenExportar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = par1 = new SqlParameter("@Id", IdCabeceraOrigen);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCabecera.CeCo = dataReader["CeCo"].ToString();
                        oCabecera.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oCabecera.Macroservicio = dataReader["Macroservicio"].ToString(); ;
                        oCabecera.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oCabecera.PptoAprobadoUSD = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
                        oCabecera.Sector = dataReader["Sector"].ToString();
                        oCabecera.Sociedad = dataReader["Sociedad"].ToString();
                        oCabecera.MontoTrasladarUSD = Convert.ToDouble(dataReader["MontoATrasladarUSD"]);
                        oCabecera.NuevoPptoUSD = Convert.ToDouble(dataReader["NuevoPptoUSD"]);
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
                        oCabecera.Id = dataReader["Id"].ToString();

                        oListCabecera.Add(oCabecera);

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
            return oListCabecera;

        }


        public CabeceraOrigenTrasladoDTO BuscarCabeceraOrigenEdicion(string CodigoInversion, string IdDetalleTraslado)
        {
            CabeceraOrigenTrasladoDTO oCabecera = new CabeceraOrigenTrasladoDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraOrigenPorCodigoInversionEdicion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdDetalleTraslado", IdDetalleTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCabecera.CeCo = dataReader["CeCo"].ToString();
                        oCabecera.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oCabecera.Macroservicio = dataReader["Macroservicio"].ToString(); ;
                        oCabecera.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oCabecera.PptoAprobadoUSD = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
                        oCabecera.Sector = dataReader["Sector"].ToString();
                        oCabecera.Sociedad = dataReader["Sociedad"].ToString();
                        oCabecera.MontoTrasladarUSD = Convert.ToDouble(dataReader["MontoATrasladarUSD"]);
                        oCabecera.NuevoPptoUSD = Convert.ToDouble(dataReader["NuevoPptoUSD"]);
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
                        oCabecera.Id = dataReader["Id"].ToString();

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
            return oCabecera;

        }
        public List<CabeceraDestinoTrasladoDTO> ObtenerCabeceraDestinoExportar(string IdCabeceraDestino)
        {
            List<CabeceraDestinoTrasladoDTO> oListaCabecera = new List<CabeceraDestinoTrasladoDTO>();
            CabeceraDestinoTrasladoDTO oCabecera = new CabeceraDestinoTrasladoDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraDestinoExportar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Id", IdCabeceraDestino);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCabecera.CeCo = dataReader["CeCo"].ToString();
                        oCabecera.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oCabecera.Macroservicio = dataReader["Macroservicio"].ToString(); ;
                        oCabecera.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oCabecera.PptoAprobadoUSD = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
                        oCabecera.Sector = dataReader["Sector"].ToString();
                        oCabecera.Sociedad = dataReader["Sociedad"].ToString();
                        oCabecera.MontoTrasladarUSD = Convert.ToDouble(dataReader["MontoATrasladarUSD"]);
                        oCabecera.NuevoPptoUSD = Convert.ToDouble(dataReader["NuevoPptoUSD"]);
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
                        oCabecera.Id = dataReader["Id"].ToString();
                        oCabecera.Motivo = dataReader["Motivo"].ToString();
                        oCabecera.VAN = Convert.ToDouble(dataReader["VAN"]);
                        oCabecera.TIR = Convert.ToDouble(dataReader["TIR"]);
                        oCabecera.PRI = Convert.ToDouble(dataReader["PRI"]);
                        oCabecera.ObservacionesFinancieras = dataReader["ObservacionesFinancieras"].ToString();
                        oCabecera.IdPriTraslado = Convert.ToInt32(dataReader["IdPriTraslado"]);

                        if (oCabecera.IdPriTraslado == 0)
                            oCabecera.DescIdPRITraslado = "Años";
                        else if (oCabecera.IdPriTraslado == 1)
                            oCabecera.DescIdPRITraslado = "Meses";
                        else
                            oCabecera.DescIdPRITraslado = "";
                            

                        oListaCabecera.Add(oCabecera);

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
            return oListaCabecera;

        }

        public CabeceraOrigenTrasladoDTO ObtenerCeCoPorDetalleDestinoTraslado(string IdDetalleDestinoTraslado)
        {
            CabeceraOrigenTrasladoDTO oCabecera = new CabeceraOrigenTrasladoDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCeCoPorDetalleDestinoTraslado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalleDestinoTraslado", IdDetalleDestinoTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
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
            return oCabecera;
        }
        public CabeceraDestinoTrasladoDTO BuscarCabeceraDestinoEdicion(string CodigoInversion, string IdDetalleTraslado)
        {
            CabeceraDestinoTrasladoDTO oCabecera = new CabeceraDestinoTrasladoDTO();

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraDestinoPorCodigoInversionEdicion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdDetalleTraslado", IdDetalleTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oCabecera.CeCo = dataReader["CeCo"].ToString();
                        oCabecera.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oCabecera.Macroservicio = dataReader["Macroservicio"].ToString(); ;
                        oCabecera.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oCabecera.PptoAprobadoUSD = Convert.ToDouble(dataReader["PptoAprobadoUSD"]);
                        oCabecera.Sector = dataReader["Sector"].ToString();
                        oCabecera.Sociedad = dataReader["Sociedad"].ToString();
                        oCabecera.MontoTrasladarUSD = Convert.ToDouble(dataReader["MontoATrasladarUSD"]);
                        oCabecera.NuevoPptoUSD = Convert.ToDouble(dataReader["NuevoPptoUSD"]);
                        oCabecera.IdCeCo = dataReader["IdCeCo"].ToString();
                        oCabecera.Id = dataReader["Id"].ToString();
                        oCabecera.Motivo = dataReader["Motivo"].ToString();
                        oCabecera.VAN = Convert.ToDouble(dataReader["VAN"]);
                        oCabecera.TIR = Convert.ToDouble(dataReader["TIR"]);
                        oCabecera.PRI = Convert.ToDouble(dataReader["PRI"]);
                        oCabecera.ObservacionesFinancieras = dataReader["ObservacionesFinancieras"].ToString();
                        oCabecera.IdPriTraslado = Convert.ToInt32(dataReader["IdPriTraslado"]);

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
            return oCabecera;

        }

        public RAprobadoresTrasladoAPIWFBE ObtenerAprobadores(RSolicitudTrasladoBE oSolicitudInversion)
        {

            RAprobadoresTrasladoAPIWFBE oAprobadores = new RAprobadoresTrasladoAPIWFBE();
            //RValoresAprobacionBE valorAprobacion;
            //Setear a vacio los aprobadores
            oAprobadores.expertoCompras = "";
            oAprobadores.expertoInmobiliaria = "";
            oAprobadores.expertoMantenimiento = "";
            oAprobadores.expertoSistemas = "";
            oAprobadores.gerenteCentral = "";
            oAprobadores.finanzas = "";
            oAprobadores.gerenteFinanzas = "";
            oAprobadores.gerenteGeneral = "";
            oAprobadores.gerenteLinea = "";
            oAprobadores.controlGestion = "";
            oAprobadores.controlGestion2 = "";
            oAprobadores.TipoActivo = "";
            oAprobadores.Administradores = "";
            oAprobadores.Contabilidad = "";
            oAprobadores.gerenteCentralOrigen = "";
            oAprobadores.TipoCapex = "";

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
            RDetallePrincipalSolicitudTrasladoBE SolicitudTrasladoOrigen = oSolicitudInversion.DetalleSolicitudTraslado.Where(x => x.Tipo == "Origen").FirstOrDefault();
            CabeceraOrigenTrasladoDTO ocabeceraOrigen = BuscarCabeceraOrigenDestino(SolicitudTrasladoOrigen.CodigoProyecto);

            List<RDetallePrincipalSolicitudTrasladoBE> SolicitudTrasladoDestino = oSolicitudInversion.DetalleSolicitudTraslado.Where(x => x.Tipo == "Destino").ToList();
            CabeceraOrigenTrasladoDTO ocabeceraDestino = SolicitudTrasladoDestino.Count > 0 ? ObtenerCeCoPorDetalleDestinoTraslado(SolicitudTrasladoDestino[0].Id.ToString()) : null;

            #region Gerente de Linea y Gerente Central

            if (ocabeceraOrigen.IdCeCo != null)
            {
                oAprobadores.gerenteCentralOrigen = oCeCo.obtenerCentroCostoPorId(ocabeceraOrigen.IdCeCo).GerenteLinea;
                                
            }
            #endregion


            #region Gerente de Linea y Gerente Central

            if (ocabeceraDestino.IdCeCo != null)
            {
                //oAprobadores.gerenteLinea = oCeCo.obtenerCentroCostoPorCodigoBD(ocabeceraDestino.IdCeCo).GerenteLinea;
                oAprobadores.gerenteLinea  = oCeCo.obtenerCentroCostoPorId(ocabeceraDestino.IdCeCo).GerenteLinea;
                //oAprobadores.gerenteCentral = oCeCo.obtenerCentroCostoPorCodigoBD(ocabeceraDestino.IdCeCo).GerenteCentral;
                oAprobadores.gerenteCentral = oCeCo.obtenerCentroCostoPorId(ocabeceraDestino.IdCeCo).GerenteCentral;

            }
            #endregion

            #region TipoActivo
            foreach (RDetallePrincipalSolicitudTrasladoBE item in SolicitudTrasladoDestino)
            {
                string TipoActivo = ObtenerTipoActivoPorIdCabeceraDestino(item.IdCabeceraDestino);
                if(!oAprobadores.TipoActivo.Contains(TipoActivo))
                {
                   if (!String.IsNullOrEmpty(oAprobadores.TipoActivo))
                        oAprobadores.TipoActivo += "|";

                        oAprobadores.TipoActivo += TipoActivo;
                }
                
            }
            #endregion

            #region TipoCapex
            foreach (RDetallePrincipalSolicitudTrasladoBE item in SolicitudTrasladoDestino)
            {
                string TipoCapex = ObtenerTipoCapexPorCodigoProyecto(item.CodigoProyecto);
                if (!oAprobadores.TipoCapex.Contains(TipoCapex))
                {
                    if (!String.IsNullOrEmpty(oAprobadores.TipoCapex))
                        oAprobadores.TipoCapex += "|";

                    oAprobadores.TipoCapex += TipoCapex;
                }

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

        /*public List<RValoresAprobacionBE> ObtenerValoresAprobacion()
        {
            ScriptorChannel canalValoresAprobacion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.ValoresAprobacion));

            List<RValoresAprobacionBE> oLista = new List<RValoresAprobacionBE>();
            RValoresAprobacionBE oValorAprobacion;

            //Verificar si hay que validar el año
            List<ScriptorContent> oListaValoresAprobacion = canalValoresAprobacion.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            foreach (ScriptorContent item in oListaValoresAprobacion)
            {
                oValorAprobacion = new RValoresAprobacionBE();

                oValorAprobacion.Id = item.Id;

                if (item.Parts.IdRol != null)
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


        }

        public string ObtenerTipoActivoPorIdCabeceraDestino(string idCabeceraDestino)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            string tipoactivo="";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTipoActivoPorIdCabeceraDestino";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Id", idCabeceraDestino);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tipoactivo= dataReader["TipoActivo"].ToString();

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
            return tipoactivo;            
        }

        public string ObtenerTipoCapexPorCodigoProyecto(string CodigoProyecto)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            string tipoactivo = "";
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerTipoCapexPorCodigoProyecto";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoProyecto", CodigoProyecto);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        tipoactivo = dataReader["TipoCapex"].ToString();

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
            return tipoactivo;
        }

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


        public List<DetalleOrigenTrasladoDTO> ObtenerDetalleTrasladoOrigen(string IdTraslado)
        {
            List<DetalleOrigenTrasladoDTO> oListaDetalle = new List<DetalleOrigenTrasladoDTO>();
            DetalleOrigenTrasladoDTO oDetalle;
           
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleTrasladoOrigen";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdTraslado", IdTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleOrigenTrasladoDTO();
                        oDetalle.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oDetalle.MontoATrasladar = Convert.ToDouble(dataReader["MontoATrasladar"]);
                        oDetalle.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oDetalle.NuevoPpto = Convert.ToDouble(dataReader["NuevoPpto"]);
                        oDetalle.OrdenInversion = dataReader["OrdenInversion"].ToString();
                        oDetalle.PptoAprobado = Convert.ToDouble(dataReader["PptoAprobado"]);
                        oDetalle.Id = dataReader["Id"].ToString();

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

        public int ObtenerIdInstancia(string IdSolicitud)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));

            ScriptorContent contenido = canalSolicitudInversion.QueryContents("#Id", IdSolicitud, "=").ToList().FirstOrDefault();
            return contenido.Parts.IdInstancia;
        }



        public DataTable ObtenerCabeceraTraslado(string IdTraslado)
        {
            DataTable CabeceraTraslado = new DataTable();
            CabeceraTraslado.Columns.Add("NumSolicitud");
            CabeceraTraslado.Columns.Add("Comentario");
            CabeceraTraslado.Columns.Add("Coordinador");
            CabeceraTraslado.Columns.Add("Sociedad");
            CabeceraTraslado.Columns.Add("Estado");
            CabeceraTraslado.Columns.Add("FechaCreacion");
            CabeceraTraslado.Columns.Add("Caso");

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerCabeceraTraslado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Id", IdTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        DataRow row = CabeceraTraslado.NewRow();
                        row["NumSolicitud"] = dataReader["NumSolicitud"].ToString();
                        row["Comentario"] = dataReader["Comentario"].ToString();
                        row["Coordinador"] = dataReader["Coordinador"].ToString();
                        row["Sociedad"] = dataReader["Sociedad"].ToString();
                        row["Estado"] = dataReader["Estado"].ToString();
                        row["FechaCreacion"] = dataReader["FechaCreacion"].ToString();
                        row["Caso"] = dataReader["Caso"].ToString();

                        CabeceraTraslado.Rows.Add(row);

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
            return CabeceraTraslado;

        }



        public List<DetalleDestinoTrasladoDTO> ObtenerDetalleTrasladoDestino(string IdTraslado)
        {
            List<DetalleDestinoTrasladoDTO> oListaDetalle = new List<DetalleDestinoTrasladoDTO>();
            DetalleDestinoTrasladoDTO oDetalle;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleTrasladoDestino";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdTraslado", IdTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleDestinoTrasladoDTO();
                        oDetalle.CodigoProyecto = dataReader["CodigoProyecto"].ToString();
                        oDetalle.MontoATrasladar = Convert.ToDouble(dataReader["MontoATrasladar"]);
                        oDetalle.NombreProyecto = dataReader["NombreProyecto"].ToString();
                        oDetalle.NuevoPpto = Convert.ToDouble(dataReader["NuevoPpto"]);
                        oDetalle.OrdenInversion = dataReader["OrdenInversion"].ToString();
                        oDetalle.PptoAprobado = Convert.ToDouble(dataReader["PptoAprobado"]);
                        oDetalle.IdSolicitudInversion = dataReader["IdSolicitudInversion"].ToString();
                        oDetalle.Id = dataReader["Id"].ToString();

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

        
        public List<DetalleOrigenDestinoDTO> ObtenerDetalleOrigenDestino(string CodigoInversion)
        {
            List<DetalleOrigenDestinoDTO> oLista = new List<DetalleOrigenDestinoDTO>();
            DetalleOrigenDestinoDTO oDetalle;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleOrigenDestinoPorCodigoInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleOrigenDestinoDTO();
                        oDetalle.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oDetalle.IdCeCo = dataReader["IdCeCo"].ToString();
                        oDetalle.TipoActivo = dataReader["TipoActivo"].ToString();
                        oDetalle.MonedaCotizada = dataReader["MonedaCotizada"].ToString();
                        oDetalle.PptoAprobadoOI = Convert.ToDouble(dataReader["PptoAprobadoOI"].ToString());
                        oDetalle.VidaUtil = int.Parse(dataReader["VidaUtil"].ToString());
                        oDetalle.NumeroOI = dataReader["NumeroOI"].ToString();
                        oDetalle.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oDetalle.MontoDisponible = Convert.ToDouble(dataReader["MontoDisponible"].ToString());
                        
                        oLista.Add(oDetalle);


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

        public List<DetalleOrigenDestinoDTO> ObtenerDetalleOrigenDestinoPorCodigoInversionPlanBase(string CodigoInversion)
        {
            List<DetalleOrigenDestinoDTO> oLista = new List<DetalleOrigenDestinoDTO>();
            DetalleOrigenDestinoDTO oDetalle;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleOrigenDestinoPorCodigoInversionPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleOrigenDestinoDTO();
                        oDetalle.IdTipoActivo = dataReader["IdTipoActivo"].ToString();
                        oDetalle.IdCeCo = dataReader["IdCeCo"].ToString();
                        oDetalle.TipoActivo = dataReader["TipoActivo"].ToString();
                        oDetalle.MonedaCotizada = dataReader["MonedaCotizada"].ToString();
                        oDetalle.PptoAprobadoOI = Convert.ToDouble(dataReader["PptoAprobadoOI"].ToString());
                        oDetalle.VidaUtil = int.Parse(dataReader["VidaUtil"].ToString());
                        oDetalle.NumeroOI = dataReader["NumeroOI"].ToString();
                        oDetalle.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oDetalle.MontoDisponible = Convert.ToDouble(dataReader["MontoDisponible"].ToString());

                        oLista.Add(oDetalle);


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

        public List<DetalleOrigenDestinoDTO> ObtenerDetalleOrigenEdicion(string IdDetalleTraslado)
        {
            //Es IdDetalleTraslado no IdCabeceraOrigen
            List<DetalleOrigenDestinoDTO> oLista = new List<DetalleOrigenDestinoDTO>();
            DetalleOrigenDestinoDTO oDetalle;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleOrigenPorCodigoInversionEdicion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalleTraslado", IdDetalleTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleOrigenDestinoDTO();

                        oDetalle.Id = dataReader["Id"].ToString();
                        oDetalle.TipoActivo = dataReader["TipoActivo"].ToString();
                        oDetalle.MonedaCotizada = dataReader["MonedaCotizada"].ToString();
                        oDetalle.PptoAprobadoOI = Convert.ToDouble(dataReader["PptoAprobado"].ToString());
                        oDetalle.VidaUtil = int.Parse(dataReader["VidaUtil"].ToString());
                        oDetalle.NumeroOI = dataReader["NumeroOI"].ToString();
                        oDetalle.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oDetalle.MontoATrasladar = Convert.ToDouble(dataReader["MontoATrasladar"]);
                        oDetalle.NuevoPptoMonedaCotizada = Convert.ToDouble(dataReader["NuevoPptoMonedaCotizada"]);
                        oDetalle.NuevoPptoOIUSD = Convert.ToDouble(dataReader["NuevoPptoOIUSD"]);
                        oDetalle.IdMovimiento = dataReader["IdMovimiento"].ToString();
                        oDetalle.IdInversion = dataReader["IdInversion"].ToString();
                        oDetalle.DescMonedaCotizada=dataReader["NombreMoneda"].ToString();
                        oDetalle.MontoDisponible = Convert.ToDouble(dataReader["MontoDisponible"]);
                        oLista.Add(oDetalle);
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

        public List<DetalleOrigenDestinoDTO> ObtenerDetalleDestinoEdicion(string IdDetalleTraslado)
        {
            //Es IdDetalleTraslado no IdCabeceraDestino
            List<DetalleOrigenDestinoDTO> oLista = new List<DetalleOrigenDestinoDTO>();
            DetalleOrigenDestinoDTO oDetalle;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerDetalleDestinoPorCodigoInversionEdicion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdDetalleTraslado", IdDetalleTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oDetalle = new DetalleOrigenDestinoDTO();

                        oDetalle.Id = dataReader["Id"].ToString();
                        oDetalle.TipoActivo = dataReader["TipoActivo"].ToString();
                        oDetalle.MonedaCotizada = dataReader["MonedaCotizada"].ToString();
                        oDetalle.PptoAprobadoOI = Convert.ToDouble(dataReader["PptoAprobado"].ToString());
                        oDetalle.VidaUtil = int.Parse(dataReader["VidaUtil"].ToString());
                        oDetalle.NumeroOI = dataReader["NumeroOI"].ToString();
                        oDetalle.DescripcionOI = dataReader["DescripcionOI"].ToString();
                        oDetalle.MontoATrasladar = Convert.ToDouble(dataReader["MontoATrasladar"]);
                        oDetalle.NuevoPptoMonedaCotizada = Convert.ToDouble(dataReader["NuevoPptoMonedaCotizada"]);
                        oDetalle.NuevoPptoOIUSD = Convert.ToDouble(dataReader["NuevoPptoOIUSD"]);
                        oDetalle.IdInversion = dataReader["IdInversion"].ToString();
                        oDetalle.IdMovimiento = dataReader["IdMovimiento"].ToString();
                        oDetalle.DescMonedaCotizada = dataReader["NombreMoneda"].ToString();
                        oLista.Add(oDetalle);


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

        public bool ValidarCierreTraslado(string IdTraslado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            bool rpta = false; 
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarCierreTraslado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdTraslado", IdTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        if (Convert.ToInt32(dataReader["Cierre"]) > 0)
                            rpta = false;
                        else
                            rpta =  true;

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

            return rpta;
 
        }

        public int ValidarExisteCodigoInversion(string CodigoInversion, bool NumeroOI)
        {           

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            int res = -1;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarCabeceraOrigenDestinoPorCodigoInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@NumeroOI", NumeroOI ? 1: 0);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {                        
                         res = Convert.ToInt32(dataReader["Respuesta"]);
                        
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

        public bool ValidarCodigoInversionAprobado(string CodigoInversion)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            bool res = false;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarCodigoInversionAproado";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@CodigoInversion", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        if (Convert.ToInt32(dataReader["Respuesta"]) > 0)
                            res = true;
                        
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

        public int ValidarPlanBase(string CodigoInversion)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            int res = -1;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarPlanBase";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Codigo", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        res = Convert.ToInt32(dataReader["Cantidad"]);

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

        public int ValidarPlanBaseTipoBolsa(string CodigoInversion)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            int res = -1;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ValidarPlanBaseTipoBolsa";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Codigo", CodigoInversion);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        res = Convert.ToInt32(dataReader["Cantidad"]);

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

        public bool ActualizarEstadoSolicitudInversionTraslado(RSolicitudTrasladoBE oSolicitudInversion)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
            ScriptorChannel canalEstado = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Estado));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            bool resultadoCabecera = false;

            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("NumSolicitud", oSolicitudInversion.NumSolicitud, "=").ToList().FirstOrDefault();

            if(oSolicitudInversion.IdEstado != null)
                oSolicitud.Parts.IdEstado = ScriptorDropdownListValue.FromContent(canalEstado.QueryContents("#Id", oSolicitudInversion.IdEstado.Id, "=").ToList().FirstOrDefault());

            oSolicitud.Parts.IdInstancia = oSolicitudInversion.IdInstancia;
            oSolicitud.Parts.FechaEnvio = DateTime.Now;
            oSolicitud.Parts.FechaModificacion = DateTime.Now;
            
            resultadoCabecera = oSolicitud.Save();


            return resultadoCabecera;

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
                string nombreProcedure = "ObtenerStatusPresupuestoTraslado";
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


            return oListaStatus;
        }

        public bool ValidarOIRepetido(string NumeroOI, string IdSolicitudTraslado)
        {

            bool res = false;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarOIRepetido";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@NumeroOI", NumeroOI);
                cmd.Parameters.Add(par1);

                par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitudTraslado);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        if (Convert.ToInt32(dataReader["Respuesta"]) > 0)
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

        public List<ResultadoValidarMontoDisponibleDTO> ValidarMontoDisponibleEnviar(string IdSolicitudTraslado)
        {
            List<ResultadoValidarMontoDisponibleDTO> oLista = new List<ResultadoValidarMontoDisponibleDTO>();
            ResultadoValidarMontoDisponibleDTO oResultado;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIValidarMontoDisponibleEnviar";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitudTraslado);
                cmd.Parameters.Add(par1);
                
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oResultado = new ResultadoValidarMontoDisponibleDTO();

                        oResultado.CodigoOI = dataReader["CodigoOI"].ToString();
                        oResultado.MontoATrasladar = Convert.ToDouble(dataReader["MontoATrasladar"]);
                        oResultado.MontoDisponible = Convert.ToDouble(dataReader["MontoDisponible"]);
                        oResultado.Correcto = Convert.ToInt32(dataReader["Correcto"]);

                        oLista.Add(oResultado);

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

        public List<string> BuscarOIEnProceso(string IdSolicitudTraslado)
        {
            List<string> oLista = new List<string>();
            string oi = "";
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIBuscarOIEnProceso";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitudTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        
                        oi = dataReader["CodigoOI"].ToString();                       

                        oLista.Add(oi);

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

        public bool InsertarOIEnProceso(string IdSolicitudTraslado)
        {
            
            bool res = false;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIInsertarOIEnProceso";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitudTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.RecordsAffected > 0)
                    {
                        res = true;
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

        public bool EliminarOIEnProceso(string IdSolicitudTraslado)
        {

            bool res = false;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "APIEliminarOIEnProceso";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@IdSolicitudTraslado", IdSolicitudTraslado);
                cmd.Parameters.Add(par1);

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    if (dataReader.RecordsAffected > 0)
                    {
                        res = true;
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

        public RSociedadBE ObtenerSociedadTraslado(string IdSolicitudTraslado)
        {
            ScriptorChannel canalSolicitudInversion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.SolicitudInversionTraslado));
                        
            ScriptorContent oSolicitud = canalSolicitudInversion.QueryContents("#Id", IdSolicitudTraslado, "=").ToList().FirstOrDefault();
            ScriptorContent oSociedad = null;

            if(oSolicitud != null)
                oSociedad = ((ScriptorDropdownListValue)oSolicitud.Parts.IdSociedad).Content;
                    

            RSociedadBE oSociedadBE = new RSociedadBE();

            oSociedadBE.Id = oSociedad.Id;
            oSociedadBE.Codigo = oSociedad.Parts.Codigo;
            oSociedadBE.Descripcion = oSociedad.Parts.Descripcion;         
            
            return oSociedadBE;

 
        }

    }
}

