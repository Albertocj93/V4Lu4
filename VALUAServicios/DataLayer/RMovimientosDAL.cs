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
    public class RMovimientosDAL
    {
        public List<RMovimientosBE> ObtenerMovimientos()
        {

            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaMovimientos= canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<RMovimientosBE> oListaMovimientos= new List<RMovimientosBE>();
            RMovimientosBE oMovimiento;

            foreach (ScriptorContent item in listaMovimientos)
            {
                oMovimiento = new RMovimientosBE();
                //oPlanBase.CodCeCo = ((ScriptorDropdownListValue)item.Parts.IdCeCo).Content.Parts.CodCECO;
                oMovimiento.Monto = item.Parts.Monto;
                oMovimiento.IdInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id;
                oMovimiento.IdEstadoMovimiento = ((ScriptorDropdownListValue)item.Parts.IdEstadoMovimiento).Content.Id;
                oMovimiento.FechaMovimiento = item.Parts.FechaMovimiento;
                oMovimiento.Descripcion = item.Parts.Descripcion;
                oMovimiento.IdSolicitudInversion = ((ScriptorDropdownListValue)item.Parts.IdSolicitudInversion).Content.Id;
                oMovimiento.IdSolicitudInversionTraslado = ((ScriptorDropdownListValue)item.Parts.IdSolicitudInversionTraslado).Content.Id;
                oListaMovimientos.Add(oMovimiento);
            }
            return oListaMovimientos;
        }
        public List<RMovimientosBE> ObtenerMovimientoPorInversion(Guid idInversion)
        {
            ScriptorChannel canalPlanBase = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Movimientos));
            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaMovimientos = canalPlanBase.QueryContents("#Id", Guid.NewGuid(), "<>")
                                                                   .QueryContents("IdInversion", idInversion, "<>").ToList();
            List<RMovimientosBE> oListaMovimientos = new List<RMovimientosBE>();
            RMovimientosBE oMovimiento;
            foreach (ScriptorContent item in listaMovimientos)
            {
                oMovimiento = new RMovimientosBE();
                oMovimiento.Id = item.Id;
                oMovimiento.Monto = item.Parts.Monto;
                oMovimiento.IdInversion = ((ScriptorDropdownListValue)item.Parts.IdInversion).Content.Id;
                oMovimiento.IdEstadoMovimiento = ((ScriptorDropdownListValue)item.Parts.IdEstadoMovimiento).Content.Id;
                oMovimiento.FechaMovimiento = item.Parts.FechaMovimiento;
                oMovimiento.Descripcion = item.Parts.Descripcion;
                oMovimiento.IdSolicitudInversion = ((ScriptorDropdownListValue)item.Parts.IdSolicitudInversion).Content.Id;
                oMovimiento.IdSolicitudInversionTraslado = ((ScriptorDropdownListValue)item.Parts.IdSolicitudInversionTraslado).Content.Id;
                oListaMovimientos.Add(oMovimiento);
            }
            return oListaMovimientos;
        }
        public List<RMovimientosBE> ObtenerMovimientoPorInversionBD(Guid idInversion)
        {
            List<RMovimientosBE> oListaMovimientos = new List<RMovimientosBE>();
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerListaMovimientoPorInversion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter par1 = new SqlParameter("@IdInversion", idInversion);
                cmd.Parameters.Add(par1);
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        RMovimientosBE MovimientoBE = new RMovimientosBE();
                        LlenarEntidadMovimiento(MovimientoBE, dataReader);
                        oListaMovimientos.Add(MovimientoBE);
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
            return oListaMovimientos;
        }
        private void LlenarEntidadMovimiento(RMovimientosBE item, IDataReader iDataReader)
        {
            if (!Convert.IsDBNull(iDataReader["Id"]))
            {
                item.Id = (Guid)(iDataReader["Id"]);
            }
            if (!Convert.IsDBNull(iDataReader["Monto"]))
            {
                item.Monto = Convert.ToString(iDataReader["Monto"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdInversion"]))
            {
                item.IdInversion = (Guid)(iDataReader["IdInversion"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdEstadoMovimiento"]))
            {
                item.IdEstadoMovimiento = (Guid)(iDataReader["IdEstadoMovimiento"]);
            }
            if (!Convert.IsDBNull(iDataReader["FechaMovimiento"]))
            {
                item.FechaMovimiento = Convert.ToDateTime(iDataReader["FechaMovimiento"]);
            }
            if (!Convert.IsDBNull(iDataReader["Descripcion"]))
            {
                item.Descripcion = Convert.ToString(iDataReader["Descripcion"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdSolicitudInversion"]))
            {
                item.IdSolicitudInversion = (Guid)(iDataReader["IdSolicitudInversion"]);
            }
            if (!Convert.IsDBNull(iDataReader["IdSolicitudInversionTraslado"]))
            {
                item.IdSolicitudInversionTraslado = (Guid)(iDataReader["IdSolicitudInversionTraslado"]);
            }
        }
    }
}
