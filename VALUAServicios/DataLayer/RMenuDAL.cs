using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessEntities;

namespace DataLayer
{
    public class RMenuDAL
    {
        public List<RMenuBE> ObtenerMenu()
        {
            
            List<RMenuBE> oListaMenu = new List<RMenuBE>();
            RMenuBE oMenu;            
            RSubMenuBE oSubMenu;
            string connectionString = ConfigurationManager.ConnectionStrings["conRomSql"].ConnectionString;

            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = connectionString;
                con.Open();
                string nombreProcedure = "ObtenerMenu";
                SqlCommand cmd = new SqlCommand(nombreProcedure, con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (IDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        oSubMenu = new RSubMenuBE();
                        oSubMenu.Titulo = dataReader["TituloSubMenu"].ToString();
                        oSubMenu.Url = dataReader["UrlSubMenu"].ToString();

                        if (oListaMenu.Where(x => x.Titulo == dataReader["TituloMenu"].ToString()).ToList().Count > 0)
                        {
                            oListaMenu.Where(x => x.Titulo == dataReader["TituloMenu"].ToString()).ToList().FirstOrDefault().SubMenu.Add(oSubMenu);

                        }
                        else
                        {
                            oMenu = new RMenuBE();
                            oMenu.Titulo = dataReader["TituloMenu"].ToString();
                            oMenu.Url = dataReader["UrlMenu"].ToString();
                            oMenu.SubMenu = new List<RSubMenuBE>();
                            oMenu.SubMenu.Add(oSubMenu);
                            oListaMenu.Add(oMenu);
                                
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
            return oListaMenu;
        }
    }
}
