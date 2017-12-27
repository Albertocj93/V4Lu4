using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using Common;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace DataLayer
{
    public class RLogCargaMaestroDAL
    {
        public bool InsertarLogCarga(string usuarioModificador, string nombreTabla)
        {
            bool success = false;
            try
            {
                RSolicitudTrasladoBE oSolicitudTraslado = new RSolicitudTrasladoBE();

                ScriptorChannel canalLogCarga = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(ConfigurationManager.AppSettings["CanalLogCarga"]));
                ScriptorContent oLogCarga = canalLogCarga.NewContent();

                oLogCarga.Parts.UsuarioModificador = usuarioModificador;
                oLogCarga.Parts.MaestroTabla = nombreTabla;
                oLogCarga.Parts.FechaModificacion = DateTime.Now;
                success = oLogCarga.Save();
            }
            catch (Exception ex)
            {
            
            }

            return success;

        }
    }
}
