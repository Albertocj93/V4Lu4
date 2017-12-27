using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Objects.DataClasses;
//using System.Data.Spatial;
//using System.Data.Objects;
using System.Linq.Expressions;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization.Json;
using System.IO;

namespace GR.Scriptor.Framework
{


    public static class Helper
    {
       

        public static string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        public static string PonerCeros(int numero, int NroCol)
        {
            string salida = "";
            string ceros = "";
            for (int i = 1; i <= NroCol; i++)
            {
                ceros = ceros + "0";
            }
            salida = ceros + numero.ToString();
            return salida.Substring(salida.Length - NroCol, NroCol);
        }
        private static bool ExisteRangoFechas(DateTime? fiBD, DateTime? ffbd, DateTime? fiparam, DateTime? ffparam)
        {
            return (
                                        (fiparam <= fiBD && fiparam <= ffbd && ffbd <= ffparam && fiBD <= ffparam) //afuera -- OK
                                    || (fiBD <= fiparam && fiparam <= ffbd && fiBD <= ffparam && ffbd <= ffparam) //medio fecha fin derecha bd -- OK
                                    || (fiparam <= fiBD && fiparam <= ffbd && fiBD <= ffparam && ffparam <= ffbd)//medio fecha inicio izquierda bd - OK
                                    || (fiBD <= fiparam && fiparam <= ffbd && fiBD <= ffparam && ffparam <= ffbd) //medio de fechas bd -- OK
                                    || (fiparam == null && ffbd <= ffparam) //si hay ff
                                    || (ffparam == null && fiBD >= fiparam) //si hay fi
                                    || (ffparam == null && fiparam == null)
                                    );
        }
        //public static D MiMapper<O, D>(O registro)
        //{
        //    Mapper.CreateMap<O, D>();
        //    return Mapper.Map<O, D>(registro);
        //}
        public static void SetSociedadPropietaria(string sociedadPropietaria)
        {
            System.Web.HttpContext.Current.Session["SociedadPropietaria"] = sociedadPropietaria;
        }
      
        public static void SetSession(string name, Object obj)
        {
            System.Web.HttpContext.Current.Session[name] = obj;
        }
        public static Object GetSession(string name)
        {
            return System.Web.HttpContext.Current.Session[name];
        }
        public static String GetSociedadPropietaria()
        {
            return "301";//(System.Web.HttpContext.Current.Session["SociedadPropietaria"] == null ? "301" : System.Web.HttpContext.Current.Session["SociedadPropietaria"].ToString());
        }
        public static string FormatearDecimales(String cadena, int decimales)
        {
            String salida = "";

            int nropto = 0;
            for (int i = 0; i < cadena.Length; i++)
            {
                if (cadena[i] == '.')
                {
                    nropto = nropto + 1;
                }
                if (nropto <= 1)
                {
                    salida = salida + cadena[i];
                }
                else
                {
                    if (cadena[i] != '.')
                    {
                        salida = salida + cadena[i];
                    }
                }
            }

            salida = (salida.Trim().Length == 0 ? "" : salida);

            string CadCeros = "";
            for (int i = 0; i < decimales; i++)
            {
                CadCeros = CadCeros + "0";
            }

            return Convert.ToDouble(salida).ToString("#,##0." + CadCeros);
        }
        public static string GetAcronimoAplicacion()
        {
            return ConfigurationManager.AppSettings["AcronimoAplicacion"] ?? string.Empty;
        }
        public static string GetDominioAplicacion()
        {
            return ConfigurationManager.AppSettings["DominioAplicacion"] ?? string.Empty;
        }
        public static string GetNombreMaquina()
        {
            return Environment.MachineName;
        }
        public static object GetPropertyValue(object obj, string name)
        {
            return obj == null ? null : obj.GetType()
                                           .GetProperty(name)
                                           .GetValue(obj, null);
        }
        public static IDictionary<string, object> ToDictionary(this object data)
        {
            return data.ToDictionary(null);
        }
        public static IDictionary<string, object> ToDictionary(this IDictionary<string, object> data)
        {
            return data;
        }

        public static IDictionary<string, object> ToDictionary(this object data, object add)
        {
            if (data == null)
            {
                return null;
            }
            if (data.GetType().FullName.ToLower().Contains("dictionary"))
            {
                return (IDictionary<string, object>)data;
            }
            var publicAttributes = BindingFlags.Public | BindingFlags.Instance;
            var dictionary = new Dictionary<string, object>();

            foreach (PropertyInfo property in
                     data.GetType().GetProperties(publicAttributes))
            {
                if (property.CanRead)
                {
                    dictionary.Add(property.Name, property.GetValue(data, null));
                }
            }

            if (add != null)
            {
                foreach (KeyValuePair<string, object> item in add.ToDictionary())
                {
                    dictionary.Add(item.Key, item.Value);
                }
            }
            return dictionary;
        }


        public static DateTime? FormatearFechaInicioParaBusqueda(DateTime? fecha)
        {
            if (fecha != null)
            {
                fecha = fecha.Value.Date;
            }
            return fecha;
        }

        public static DateTime? FormatearFechaFinParaBusqueda(DateTime? fecha)
        {
            if (fecha != null)
            {
                fecha = fecha.Value.Date;
                fecha = fecha.Value.AddDays(1).AddSeconds(-1);
            }
            return fecha;
        }

        public static string FormatoSeisDecimales(double myNumber)
        {
            var s = string.Format("{0:0.000000}", myNumber);
            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }

        public static decimal FormatoSeisDecimalesSAP(decimal myNumber)
        {
            return Math.Round(myNumber, 6);
        }

        public static string SerializarJsonObjecto(object objeto)
        {
            string jsonrpt = JsonConvert.SerializeObject(objeto, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "dd.MM.yyyy" });// HH:mm:ss

            return jsonrpt;
        }
        public static string DeserializarJsonObjecto2<T>(T request)
        {
            String jsonrpt = "";
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, request);
                jsonrpt = Encoding.Default.GetString(stream.ToArray());
            }

            return jsonrpt;
        }

        public static string FormatDate(DateTime fecha)
        {
            return string.Format("{0:dd.MM.yyyy}", fecha);
        }

        /// <summary>
        /// Formatea fecha con el patrón dd.MM.yyyy
        /// </summary>
        /// <param name="fecha">fecha con formato yyyyMMdd</param>
        /// <returns>fecha con el patrón dd.MM.yyyy</returns>
        public static string FormatDate(string fecha)
        {
            DateTime result = DateTime.Now;

            //string mask = "yyyyMMdd";
            string anio = fecha.Substring(0, 4);
            string mes = fecha.Substring(4, 2);
            string dia = fecha.Substring(6, 2);

            //if (DateTime.TryParse(string.Format("{0}-{1}-{2}", dia, mes, anio), out result))
            //    return Helper.FormatDate(result);

            return String.Format("{0}.{1}.{2}", dia, mes, anio); //Helper.FormatDate(result);
        }



        public static T ConvertirAObjeto<T>(string nombreAtributoCopiar, string json)
        {
            Object response = null;
            try
            {
                string[] array = json.Split(new String[] { nombreAtributoCopiar }, StringSplitOptions.None);
                if (array.Length > 1)
                {
                    string jsonrpt = array[1];
                    int nroParentecisAbiertos = 0, posicionUltimoParentecis = 0, posicionPrimerParentecis = 0;
                    ObtenerPosicionNroParentencis(jsonrpt, out posicionPrimerParentecis);

                    string jsonrptParte1 = jsonrpt.Remove(0, posicionPrimerParentecis);
                    ObtenerNroParentencis(jsonrptParte1, out nroParentecisAbiertos, out posicionUltimoParentecis);

                    string jsonrptParte2 = jsonrptParte1.Remove(posicionUltimoParentecis + 1, jsonrptParte1.Length - (posicionUltimoParentecis + 1));

                    jsonrptParte2 = jsonrptParte2.Replace("\\", "\\\\");

                    response = (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jsonrptParte2, typeof(T), new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, });
                }
            }
            catch (Exception)
            {
                //error al deserializar
            }

            return (T)response;
        }

        private static void ObtenerPosicionNroParentencis(string jsonrpt, out int posicionPrimerParentecis)
        {
            char caracterInicio = '{';
            posicionPrimerParentecis = 0;
            for (int i = 0; i < jsonrpt.Length; i++)
            {
                if (jsonrpt[i] == caracterInicio)
                {
                    posicionPrimerParentecis = i;
                    break;
                }
            }
        }

        private static void ObtenerNroParentencis(string jsonrpt, out int nroParentecisAbiertos, out int posicionUltimoParentecis)
        {
            bool flagEncontro = false;
            nroParentecisAbiertos = 0;
            posicionUltimoParentecis = 0;
            char caracterInicio = '{';
            char caracterfin = '}';
            int contCaracterInicio = 0;
            //int contCaracterfin = 0;
            for (int i = 0; i < jsonrpt.Length; i++)
            {
                if (jsonrpt[i] == caracterInicio)
                {
                    contCaracterInicio++;
                    nroParentecisAbiertos++;
                }
                if (jsonrpt[i] == caracterfin)
                {
                    //contCaracterfin++;
                    contCaracterInicio--;
                }

                if (jsonrpt[i] == caracterfin && contCaracterInicio == 0)
                {
                    posicionUltimoParentecis = i;
                    flagEncontro = true;
                    break;
                    ;
                }
            }

            if (flagEncontro == false)
            {
                nroParentecisAbiertos = 0;
                posicionUltimoParentecis = jsonrpt.Length - 1;
            }
        }

        public static string GenerarContrasenia()
        {
            string generado = string.Empty;

            Random r = new Random((int)DateTime.Now.Ticks);

            while (generado.Length < 8)
            {
                int numero = r.Next(48, 57);
                int mayus = r.Next(65, 90);
                int minus = r.Next(97, 122);

                if (numero % 2 == 0)
                    generado += string.Format("{0}", (char)numero);

                if (mayus % 2 != 0)
                    generado += string.Format("{0}", (char)mayus);

                if (minus % 2 != 0)
                    generado += string.Format("{0}", (char)minus);
            }
            return generado;
        }
    }

    
}
