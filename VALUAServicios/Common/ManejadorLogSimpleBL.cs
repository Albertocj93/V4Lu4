using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ManejadorLogSimpleBL
    {
        public static string path = System.Configuration.ConfigurationManager.AppSettings["LogCarga"];

        public static void WriteLog(string mensaje)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(mensaje);

            }
        }
        public static void ValidarRuta(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public static void NewLine()
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.Write(sw.NewLine);

            }
        }

        public static void borrar()
        {
            File.Delete(path);

        }

    }
}
