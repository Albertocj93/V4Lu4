using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Common
{
    public static class Grid
    {
        /// <summary>
        /// devuelve una cadena de texto en formato JSON con la estructura que soporta el jqgrid
        /// </summary>
        /// <typeparam name="T">El tipo de dato de la entidad</typeparam>
        /// <param name="rows">Los resultados que se mostraran en el grid</param>
        /// <param name="page">el número de página actual</param>
        /// <param name="records">El número de resultados por página</param>
        /// <param name="total">el total de páginas</param>
        /// <param name="llave">El nombre del atributo de la entidad que será considerada como llave</param>
        /// <param name="orden">una lista del tipo String contienendo los nombres de las columnas en el orden en que el Grid los leerá</param>
        /// <returns>String</returns>
        public static String toJSONFormat<T>(List<T> rows, int page, int records, int total, String llave = "", List<String> orden = null)
        {
            var props = new Dictionary<String, PropertyInfo>();
            if (orden == null)
            {
                var tipo = typeof(T);
                var pis = tipo.GetProperties();
                orden = new List<string>();
                foreach (PropertyInfo pi in pis)
                {
                    props.Add(pi.Name, pi);
                }
            }
            else
            {
                var tipo = typeof(T);
                foreach (String item in orden)
                {
                    var pi = tipo.GetProperty(item);
                    if (pi != null)
                    {
                        props.Add(pi.Name, pi);
                    }
                }
            }
            if (string.IsNullOrEmpty(llave))
            {
                llave = orden[0];
            }
            var lista = new List<JsonRow>();
            foreach (T fila in rows)
            {
                var x = fila.GetType();
                object mid = x.GetProperty(llave).GetValue(fila, null);
                lista.Add(new JsonRow
                {
                    id = (mid == null ? "" : mid.ToString()),
                    cell = (from xx in props
                            select (xx.Value.GetValue(fila, null) == null ? string.Empty : xx.Value.GetValue(fila, null).ToString())).ToArray()
                });
            }
            var obj = new
            {
                page = page,
                records = records,
                total = total,
                rows = lista
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }


        /// <summary>
        /// devuelve una cadena de texto en formato JSON con la estructura que soporta el jqgrid, Obtiene con el Name de cada Columna
        /// </summary>
        /// <typeparam name="T">El tipo de dato de la entidad</typeparam>
        /// <param name="rows">Los resultados que se mostraran en el grid</param>
        /// <param name="page">el número de página actual</param>
        /// <param name="records">El número de resultados por página</param>
        /// <param name="total">el total de páginas</param>
        /// <param name="llave">El nombre del atributo de la entidad que será considerada como llave</param>
        /// <param name="orden">una lista del tipo String contienendo los nombres de las columnas en el orden en que el Grid los leerá</param>
        /// <returns>String</returns>
        public static String toJSONFormat2<T>(List<T> rows, int page, int records, int total, String llave = "")
        {
            var props = new Dictionary<String, PropertyInfo>();

            var tipo = typeof(T);
            var pis = tipo.GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                props.Add(pi.Name, pi);
            }

            if (string.IsNullOrEmpty(llave))
            {
                llave = "";
            }
            var lista = new List<JsonRow>();
            foreach (T fila in rows)
            {
                var x = fila.GetType();
                object mid = x.GetProperty(llave).GetValue(fila, null);

                var lstcelda = new Dictionary<string, object>();
                lstcelda = (from xx in props
                            select new
                            {
                                Codigo = xx.Key,
                                Descripcion = (Object)(xx.Value.GetValue(fila, null) == null ? string.Empty : xx.Value.GetValue(fila, null).ToString())
                            }).ToList().ToDictionary(m => m.Codigo, n => n.Descripcion);

                lista.Add(new JsonRow
                {
                    id = (mid == null ? "" : mid.ToString()),
                    cell = lstcelda
                });
            }
            var obj = new
            {
                page = page,
                records = records,
                total = total,
                rows = lista
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static String toJSONFormat3(DataTable dt, int page, int records, int total, String llave = "")
        {
            //if (string.IsNullOrEmpty(llave))
            //{
            //    llave = "";
            //}
            var lista = new List<JsonRow>();

            foreach (DataRow fila in dt.Rows)
            {
                var lstcelda = new Dictionary<string, object>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    lstcelda.Add(dt.Columns[i].ColumnName, fila[i].ToString());
                }
                lista.Add(new JsonRow
                {
                    id = fila[0].ToString(),
                    cell = lstcelda
                });
            }
            //
            List<string> cols = new List<string>();
            foreach (DataColumn column in dt.Columns)
            {
                cols.Add(column.ColumnName);
            }

            List<object> colcontent = new List<object>();
            foreach (var item in cols)
            {
                var obj2 = new
                {
                    name = item.ToString(),
                    index = item.ToString()
                };
                colcontent.Add(obj2);
            }
            //
            var obj = new
            {
                page = page,
                total = total,
                records = records,
                rows = lista,
                rowsHead = cols,
                rowsM = colcontent
            };

            //JavaScriptSerializer jser = new JavaScriptSerializer();
            //return jser.Serialize(obj);
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static String emptyStrJSON()
        {
            return emptyStrJSON("", new Dictionary<string, string>());
        }

        public static String emptyStrJSON(String msg)
        {
            return emptyStrJSON(msg, new Dictionary<string, string>());
        }

        /// <summary>
        /// devuelve una cadena de texto en formato JSON con la estructura que soporta el jqGrid y con lso datos vacios
        /// </summary>
        /// <remarks>
        /// su itlizacion es necesaria en caso s equiera devolver 0 filas al grid, no enviar al grid un valor vacio "" o null pues la página donde se muestra saldrá con errores
        /// </remarks>
        /// <returns>String</returns>
        public static String emptyStrJSON<T>(String msg, Dictionary<string, T> objetos = null)
        {
            var obj = new
            {
                page = 0,
                records = 0,
                total = 0,
                rows = string.Empty,
                error = msg,
                objetos = objetos
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static int GetTotalPages(int nroRegistros, int nroFilas)
        {
            return int.Parse("" + Math.Ceiling(Convert.ToDouble(nroRegistros) / nroFilas).ToString());
        }


        public class JsonRow
        {
            public string id;
            public object cell { get; set; }
        }
    }
}