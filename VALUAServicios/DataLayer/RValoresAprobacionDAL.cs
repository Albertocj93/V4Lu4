using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities.DTO;
using Common;
using System.Data.SqlClient;
using System.Configuration;

namespace DataLayer
{
    public class RValoresAprobacionDAL
    {
        
        public List<RAnioBE> ObtenerAnios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            List<RAnioBE> oLista = new List<RAnioBE>();
            RAnioBE oAnio;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerAnios";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oAnio = new RAnioBE();
                        oAnio.IdAnio = new Guid(dataReader["Id"].ToString());
                        oAnio.Descripcion = dataReader["Descripcion"].ToString();

                        oLista.Add(oAnio);
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

        public List<RValoresAprobacionBE> ObtenerValoresAprobacionPorAnio(string Anio)
        {
            List<RValoresAprobacionBE> oLista = new List<RValoresAprobacionBE>();
            RValoresAprobacionBE oValorAprobacion;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerValoresAprobacionPorAnio";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Anio", Anio);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oValorAprobacion = new RValoresAprobacionBE();
                        oValorAprobacion.Id = new Guid(dataReader["Id"].ToString());
                        oValorAprobacion.IdRol = dataReader["IdRol"].ToString();
                        oValorAprobacion.Rol=dataReader["Rol"].ToString();
                        oValorAprobacion.Monto = Convert.ToDouble(dataReader["Monto"]);
                        oValorAprobacion.Anio = dataReader["Anio"].ToString();

                        oLista.Add(oValorAprobacion);

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

        public RValoresAprobacionBE ObtenerValoresAprobacionPorId(string Id)
        {
            RValoresAprobacionBE oValorAprobacion=null;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerValoresAprobacionPorId";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter par1 = new SqlParameter("@Id", Id);
                cmd.Parameters.Add(par1);


                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oValorAprobacion = new RValoresAprobacionBE();
                        oValorAprobacion.Id = new Guid(dataReader["Id"].ToString());
                        oValorAprobacion.IdRol = dataReader["IdRol"].ToString();
                        oValorAprobacion.Rol = dataReader["Rol"].ToString();
                        oValorAprobacion.Monto = Convert.ToDouble(dataReader["Monto"]);
                        oValorAprobacion.Anio = dataReader["Anio"].ToString();
                        oValorAprobacion.Regla = dataReader["Regla"].ToString();
                    
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
            return oValorAprobacion;

        }

        public List<RValoresAprobacionBE> ObtenerValoresAprobacion()
        {
            List<RValoresAprobacionBE> oLista = new List<RValoresAprobacionBE>();
            RValoresAprobacionBE oValorAprobacion;

            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerValoresAprobacion";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                              
                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {

                        oValorAprobacion = new RValoresAprobacionBE();
                        oValorAprobacion.Id = new Guid(dataReader["Id"].ToString());
                        oValorAprobacion.IdRol = dataReader["IdRol"].ToString();
                        oValorAprobacion.Rol = dataReader["Rol"].ToString();
                        oValorAprobacion.Monto = Convert.ToDouble(dataReader["Monto"]);
                        oValorAprobacion.Anio = dataReader["Anio"].ToString();

                        oLista.Add(oValorAprobacion);

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
        

        public bool GuadarValoresAprobacion(RValoresAprobacionBE valor)
        {
            bool Resultado = false;
            ScriptorChannel canalValoresAprobacion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.ValoresAprobacion));
            ScriptorChannel canalRol = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Rol));
            ScriptorChannel canalAnio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Anio));

            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            ScriptorContent contenido = canalValoresAprobacion.NewContent();
            contenido.Parts.IdRol = ScriptorDropdownListValue.FromContent(canalRol.QueryContents("#Id", valor.IdRol, "=").ToList().FirstOrDefault());
            contenido.Parts.Monto = valor.Monto;
            contenido.Parts.Anio = ScriptorDropdownListValue.FromContent(canalAnio.QueryContents("Descripcion", valor.Anio, "=").ToList().FirstOrDefault());
            Resultado = contenido.Save();

            return Resultado;
        }

        public bool ModificarValoresAprobacion(RValoresAprobacionBE valor)
        {
            bool Resultado = false;
            ScriptorChannel canalValoresAprobacion = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.ValoresAprobacion));
            Scriptor.scrEdit objScriptor = new Scriptor.scrEdit(System.Configuration.ConfigurationManager.AppSettings["Viatecla.Factory.Scriptor.ConnectionString"].ToString(), "anonimo", String.Empty, "/scrEdit.log");

            ScriptorContent contenido = canalValoresAprobacion.QueryContents("#Id", valor.Id, "=").FirstOrDefault();

            contenido.Parts.Monto = valor.Monto;
            Resultado=contenido.Save();

            return Resultado;
        }
    }
}
