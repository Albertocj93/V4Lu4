using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using System.Web.UI;
using System.Net;
using System.IO;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Data;

namespace Common
{
    public class Mes
    {
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }

    public class Rol
    {
        public Rol(string psRol, string psTipo)
        {
            this.Valor = psRol;
            this.Descripcion = psRol;
            this.Tipo = psTipo;
        }

        public string Valor { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

    }

    public static class ListasUtilitarias
    {
        public static List<Mes> ObtenerMeses()
        {

            List<Mes> meses = new List<Mes>();
            meses.Add(new Mes() { Descripcion = "Enero", Valor = "1" });
            meses.Add(new Mes() { Descripcion = "Febrero", Valor = "2" });
            meses.Add(new Mes() { Descripcion = "Marzo", Valor = "3" });
            meses.Add(new Mes() { Descripcion = "Abril", Valor = "4" });
            meses.Add(new Mes() { Descripcion = "Mayo", Valor = "5" });
            meses.Add(new Mes() { Descripcion = "Junio", Valor = "6" });
            meses.Add(new Mes() { Descripcion = "Julio", Valor = "7" });
            meses.Add(new Mes() { Descripcion = "Agosto", Valor = "8" });
            meses.Add(new Mes() { Descripcion = "Septiembre", Valor = "9" });
            meses.Add(new Mes() { Descripcion = "Octubre", Valor = "10" });
            meses.Add(new Mes() { Descripcion = "Noviembre", Valor = "11" });
            meses.Add(new Mes() { Descripcion = "Diciembre", Valor = "12" });
            return meses;
        }
        public static List<Mes> ObtenerMesesAnio(int anio)
        {

            List<Mes> meses = new List<Mes>();
            meses.Add(new Mes() { Descripcion = "Enero", Valor = "Enero " + anio });
            meses.Add(new Mes() { Descripcion = "Febrero", Valor = "Febrero " + anio });
            meses.Add(new Mes() { Descripcion = "Marzo", Valor = "Marzo " + anio });
            meses.Add(new Mes() { Descripcion = "Abril", Valor = "Abril " + anio });
            meses.Add(new Mes() { Descripcion = "Mayo", Valor = "Mayo " + anio });
            meses.Add(new Mes() { Descripcion = "Junio", Valor = "Junio " + anio });
            meses.Add(new Mes() { Descripcion = "Julio", Valor = "Julio " + anio });
            meses.Add(new Mes() { Descripcion = "Agosto", Valor = "Agosto " + anio });
            meses.Add(new Mes() { Descripcion = "Septiembre", Valor = "Septiembre " + anio });
            meses.Add(new Mes() { Descripcion = "Octubre", Valor = "Octubre " + anio });
            meses.Add(new Mes() { Descripcion = "Noviembre", Valor = "Noviembre " + anio });
            meses.Add(new Mes() { Descripcion = "Diciembre", Valor = "Diciembre " + anio });
            return meses;
        }

        public static List<Rol> ObtenerRoles(string psTipo)
        {

            List<Rol> roles = new List<Rol>();
            //roles.Add(new Rol("Responsable", "APG"));
            //roles.Add(new Rol("VP Solicitante", "APG"));
            //roles.Add(new Rol("Country Manager", "APG"));
            //roles.Add(new Rol("VP Internacional", "APG"));
            roles.Add(new Rol("Planeamiento y Control de Gestión", "APGE"));
            roles.Add(new Rol("V.P. Finanzas Corporativo", "APGE"));
            roles.Add(new Rol("Gerencia General", "APGE"));
            //roles.Add(new Rol("Finanzas Corporativo", "APG"));


            //roles.Add(new Rol("VP Solicitante", "API"));
            //roles.Add(new Rol("Experto", "API"));
            roles.Add(new Rol("Planeamiento y Control de Gestión", "API"));
            roles.Add(new Rol("V.P. Finanzas Corporativo", "API"));
            roles.Add(new Rol("Gerencia General", "API"));

            roles.Add(new Rol("Planeamiento y Control de Gestión", "PI"));
            roles.Add(new Rol("V.P. Finanzas Corporativo", "PI"));
            roles.Add(new Rol("Gerencia General", "PI"));

            //roles.Add(new Rol("Country Manager", "API"));
            //roles.Add(new Rol("Finanzas Local", "API"));
            //roles.Add(new Rol("Finanzas Corporativo", "API"));
            //roles.Add(new Rol("VP Supply Chain Management", "API"));
            return roles.Where(p => p.Tipo.Equals(psTipo)).ToList();

        }

        public static string[] ObtenerFormatosPermitidos()
        {
            //  return new string[] { "doc", "docx", "ppt", "pptx", "xls", "xlsx", "pdf", "jpeg", "jpg", "png", "bmp", "txt", "avi", "mp4", "gif", "mpp" };
            return ValoresConfiguracion.ExtensionesArchivosPermitidas;
        }

    }
    public static class WebUtil
    {
        /// <summary>
        /// Obtiene la ruta completa a partir de la ruta relativa enviada como parámetro
        /// </summary>
        /// <param name="psRutaRelativa"></param>
        /// <returns></returns>
        public static string ObtenerURLAbsoluta(string psRutaRelativa)
        {
            try
            {

                Control ctrl = new Control();
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + ctrl.ResolveUrl(psRutaRelativa);
            }
            catch
            {
                return "Obteniendo la ruta absoluta";
            }
        }
        public static string ObtenerStreamHtmlSource(string psUrl)
        {
            Encoding oEncoding = Encoding.UTF8;
            string resHTML = string.Empty;
            try
            {
                using (WebClient oWebClient = new WebClient())
                {
                    oWebClient.UseDefaultCredentials = true;
                    using (Stream oStream = oWebClient.OpenRead(psUrl))
                    using (StreamReader oStreamReader = new StreamReader(oStream, oEncoding))
                    {
                        resHTML = oStreamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

                return psUrl + " XX " + ex.Message.ToString();
            }




            return resHTML;
        }

        public static bool EsAdministrador
        {
            get
            {
                return true;
            }

        }


        public static string ObtenerExtensionArchivo(string psNombreArchivoCompleto)
        {
            int posicion = psNombreArchivoCompleto.LastIndexOf('.');

            return psNombreArchivoCompleto.Substring(posicion + 1);
        }

        public static string ToJson(object obj, int recursionDepth = 100)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }


        /// <summary>
        /// Prepara la grilla para ser exportada quitanto los controles asp.net estándar
        /// </summary>
        /// <param name="gv"></param>
        public static void PrepareGridViewForExport(Control gv)
        {

            Literal l = new Literal();

            string name = String.Empty;

            for (int i = 0; i < gv.Controls.Count; i++)
            {

                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {

                    l.Text = (gv.Controls[i] as LinkButton).Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }
                else if (gv.Controls[i].GetType() == typeof(HyperLink))
                {

                    l.Text = (gv.Controls[i] as HyperLink).Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }

                else if (gv.Controls[i].GetType() == typeof(DropDownList))
                {

                    l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }

                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {

                    l.Text = (gv.Controls[i] as CheckBox).Checked ? "Si" : "No";

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);

                }
                else if (gv.Controls[i].GetType().ToString() == "System.Web.UI.WebControls.DataControlLinkButton")
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;

                    gv.Controls.Remove(gv.Controls[i]);

                    gv.Controls.AddAt(i, l);
                }

                if (gv.Controls[i].HasControls())
                {

                    PrepareGridViewForExport(gv.Controls[i]);

                }

            }
        }
    }



    public static class MetodosExtensores
    {
        public static DataSet ToDataSet<T>(this IList<T> list)
        {

            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                t.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                DataRow row = t.NewRow();
                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null);
                }

                //This line was missing:
                t.Rows.Add(row);
            }


            return ds;

        }
        public static void CopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
        public static int ConvertirCadenaAEntero(this string cadena)
        {
            int res = 0;
            Int32.TryParse(cadena, out res);
            return res;
        }

        public static int ConvertirCadenaFormatoAEntero(this string cadena)
        {
            int res = 0;
            Int32.TryParse(cadena, System.Globalization.NumberStyles.Any, new  CultureInfo("es-PE"), out res);
            return res;
        }

        public static DateTime ConvertirCadenaAFecha(this string cadena)
        {
            DateTime res;

            DateTime.TryParse(cadena, out res);
            return res;
        }
        public static DateTime ConvertirCadenaAFecha2(this string cadena)
        {
            DateTime res;
            if (cadena == "")
            {
                res = DateTime.MinValue;
            }

            DateTime.TryParse(cadena, out res);
            return res;
        }


        public static Decimal ConvertirCadenaADecimal(this string cadena)
        {
            Decimal res;
            Decimal.TryParse(cadena, System.Globalization.NumberStyles.Any, new CultureInfo("es-PE"), out res);
            return res;
        }


        public static Int64 ConvertirCadenaAInt64(this string cadena)
        {
            Int64 res;
            Int64.TryParse(cadena, out res);
            return res;
        }

        public static void OrdenarLista<T>(this List<T> dataSource,
                string psNombrePropiedad, SortDirection poDireccionOrdenamiento)
        {

            PropertyInfo propInfo = typeof(T).GetProperty(psNombrePropiedad);
            Comparison<T> compare = delegate(T a, T b)
            {
                bool asc = poDireccionOrdenamiento == SortDirection.Ascending;
                object valueA = asc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
                object valueB = asc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);

                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
            };
            dataSource.Sort(compare);

        }
    }

    public static class ValoresConfiguracion
    {
        public static string CorreoAdministrador
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("CorreoUsuarioAdministrador"))
                {
                    return ConfigurationManager.AppSettings["CorreoUsuarioAdministrador"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static string URLSitio
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("URLSitio"))
                {
                    return ConfigurationManager.AppSettings["URLSitio"].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        public static string[] ExtensionesArchivosPermitidas
        {
            get
            {

                if (ConfigurationManager.AppSettings.AllKeys.Contains("ExtensionesArchivosPermitidas"))
                {
                    return ConfigurationManager.AppSettings["ExtensionesArchivosPermitidas"].ToString().Split(',');
                }
                else
                {
                    return new string[0];
                }
            }
        }
    }

}
