using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using BusinessEntities;
using System.Data.OleDb;
using System.IO;
using System.Data;

namespace Common
{
    public static class ManejadorLeerExcel
    {

        public static List<ExcelDataDTO> LeerArchivo(ArchivoDTO archivo, out int nroColumnas)
        {
            try
            {
                nroColumnas = 0;
                string NombreArchivo = archivo.Nombre; //"archivo del fileupload";
                string filePath = Path.GetTempPath() + NombreArchivo; // @"C:\TempFiles";//ConfigurationManager.AppSettings["TempLoaction"] + "\\" + fileName;

                List<List<String>> lst = ReadTextExcel(filePath, archivo.FulArchivo);

                //List<List<String>> lst = ReadTextFile(filePath, archivo.FulArchivo);
                //For each row after the first
                List<ExcelDataDTO> lstData = new List<ExcelDataDTO>();

                foreach (List<string> reg in lst)
                {
                    ExcelDataDTO data = new ExcelDataDTO();
                    if (nroColumnas == 0)
                        nroColumnas = reg.Count;

                    if (reg.Count > 0)
                        data.Columna0 = reg[0];
                    if (reg.Count > 1)
                        data.Columna1 = reg[1];
                    if (reg.Count > 2)
                        data.Columna2 = reg[2];
                    if (reg.Count > 3)
                        data.Columna3 = reg[3];
                    if (reg.Count > 4)
                        data.Columna4 = reg[4];
                    if (reg.Count > 5)
                        data.Columna5 = reg[5];
                    if (reg.Count > 6)
                        data.Columna6 = reg[6];
                    if (reg.Count > 7)
                        data.Columna7 = reg[7];
                    if (reg.Count > 8)
                        data.Columna8 = reg[8];
                    if (reg.Count > 9)
                        data.Columna9 = reg[9];
                    if (reg.Count > 10)
                        data.Columna10 = reg[10];
                    if (reg.Count > 11)
                        data.Columna11 = reg[11];
                    if (reg.Count > 12)
                        data.Columna12 = reg[12];
                    if (reg.Count > 13)
                        data.Columna13 = reg[13];
                    if (reg.Count > 14)
                        data.Columna14 = reg[14];
                    if (reg.Count > 15)
                        data.Columna15 = reg[15];
                    if (reg.Count > 16)
                        data.Columna16 = reg[16];
                    if (reg.Count > 17)
                        data.Columna17 = reg[17];
                    if (reg.Count > 18)
                        data.Columna18 = reg[18];
                    if (reg.Count > 19)
                        data.Columna19 = reg[19];
                    if (reg.Count > 20)
                        data.Columna20 = reg[20];

                    lstData.Add(data);

                }

                return lstData;

            }
            catch (Exception ex)
            {
                ManejadorLogSimpleBL.WriteLog(ex.ToString());
                throw new Exception(ex.Message);
            }


        }
        public static List<List<String>> ReadTextExcel(string filePath, Stream fulArchivo)
        {
            List<List<String>> salida = new List<List<String>>();

            OleDbConnection ExcelConnection = null;
            FileMode fileMode = FileMode.Create;
         //   byte[] byteArray = fulArchivo; // _file.OpenBinary();
          //  MemoryStream dataStream = new MemoryStream(byteArray);
            Stream stream = fulArchivo;
           
            using (FileStream fs = File.Open(filePath, fileMode))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
                fs.Close();
            }
            
            //Create the Connection String

            string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";//@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source ='" + filePath + "'; Extended Properties=Excel 5.0";
            //Create the connection
            ExcelConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
            //create a string for the query
            string ExcelQuery;
            //read columns from the Excel file
            ExcelQuery = "Select * from [Hoja1$]"; // from Sheet1";
            //use "Select * ... " to select the entire sheet
            //create the command
            System.Data.OleDb.OleDbCommand ExcelCommand = new System.Data.OleDb.OleDbCommand(ExcelQuery, ExcelConnection);
            //Open the connection
            ExcelConnection.Open();


            //Create a reader
            System.Data.OleDb.OleDbDataReader ExcelReader;
            ExcelReader = ExcelCommand.ExecuteReader();

            while (ExcelReader.Read())
            {
                List<String> lstFila = new List<String>();

                for (int i = 0; i < ExcelReader.FieldCount; i++)
                {
                    //ExcelReader.GetName(i).ToString() + '|' +;
                    lstFila.Add(ExcelReader.GetValue(i).ToString());
                }
                salida.Add(lstFila);
                //char[] delimiters = new char[] { '\t' };
                //string[] parts = line.Split(delimiters, StringSplitOptions.None);
                //for (int i = 0; i < parts.Length; i++)
                //{
                //    lstFila.Add(parts[i]);
                //}
                //salida.Add(lstFila);
            }

            ExcelConnection.Close();
            return salida;
        }
        public static List<Dictionary<string,string>> LeerCamposExcel(string filePath, Stream fulArchivo,byte[] test)
        {
            List<Dictionary<string, string>> lstFila = new List<Dictionary<string, string>>();
            OleDbConnection ExcelConnection = null;
            FileMode fileMode = FileMode.Create;
            byte[] byteArray = test; // _file.OpenBinary();
            MemoryStream dataStream = new MemoryStream(byteArray);

            Stream stream = dataStream; //fulArchivo;

            using (FileStream fs = File.Open(filePath, fileMode))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
                fs.Close();
            }

            //Create the Connection String

            string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";//@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source ='" + filePath + "'; Extended Properties=Excel 5.0";
            //Create the connection
            ExcelConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
            //create a string for the query
            string ExcelQuery;
            //read columns from the Excel file
            ExcelQuery = "Select * from [Hoja1$]"; // from Sheet1";
            //use "Select * ... " to select the entire sheet
            //create the command
            System.Data.OleDb.OleDbCommand ExcelCommand = new System.Data.OleDb.OleDbCommand(ExcelQuery, ExcelConnection);
            //Open the connection
            ExcelConnection.Open();
            //Create a reader
            System.Data.OleDb.OleDbDataReader ExcelReader;
            ExcelReader = ExcelCommand.ExecuteReader();

            ManejadorLogSimpleBL.WriteLog("Numero de Campos: " + ExcelReader.FieldCount +" "+DateTime.Now+DateTime.Now.Millisecond );
            while (ExcelReader.Read())
            {
                Dictionary<string, string> dato = new Dictionary<string, string>();
                for (int i = 0; i < ExcelReader.FieldCount; i++)
                {
                    if (ExcelReader.GetName(i).ToString().Trim() == "CodSector")
                    {
                        if (ExcelReader.GetValue(i).ToString().Trim() != "0")
                        {
                            var s = ExcelReader.GetValue(i);
                        }
                    }
                    dato.Add(ExcelReader.GetName(i).ToString().Trim(), ExcelReader.GetValue(i).ToString().Trim());
                }
                lstFila.Add(dato);
            }
            ExcelConnection.Close();
            return lstFila;
        }

        public static DataTable LeerCamposExcel1(string filePath, Stream fulArchivo, byte[] test)
        {
            DataTable firstTable = new DataTable();
            OleDbConnection ExcelConnection = null;
            FileMode fileMode = FileMode.Create;
            byte[] byteArray = test; // _file.OpenBinary();
            MemoryStream dataStream = new MemoryStream(byteArray);

            Stream stream = dataStream; //fulArchivo;

            using (FileStream fs = File.Open(filePath, fileMode))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    fs.Write(buffer, 0, bytesRead);
                }
                fs.Close();
            }

            //Create the Connection String

            string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";//@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source ='" + filePath + "'; Extended Properties=Excel 5.0";
            //Create the connection
            ExcelConnection = new System.Data.OleDb.OleDbConnection(ConnectionString);
            //create a string for the query
            string ExcelQuery;
            //read columns from the Excel file
            ExcelQuery = "Select * from [Hoja1$]"; // from Sheet1";
            //use "Select * ... " to select the entire sheet
            //create the command
            //System.Data.OleDb.OleDbCommand ExcelCommand = new System.Data.OleDb.OleDbCommand(ExcelQuery, ExcelConnection);
            ////Open the connection
            //ExcelConnection.Open();
            ////Create a reader
            //System.Data.OleDb.OleDbDataReader ExcelReader;
            //ExcelReader = ExcelCommand.ExecuteReader();

            //ManejadorLogSimpleBL.WriteLog("Numero de Campos: " + ExcelReader.FieldCount + " " + DateTime.Now + DateTime.Now.Millisecond);
            //while (ExcelReader.Read())
            //{
            //    Dictionary<string, string> dato = new Dictionary<string, string>();
            //    for (int i = 0; i < ExcelReader.FieldCount; i++)
            //    {
            //        if (ExcelReader.GetName(i).ToString().Trim() == "CodSector")
            //        {
            //            if (ExcelReader.GetValue(i).ToString().Trim() != "0")
            //            {
            //                var s = ExcelReader.GetValue(i);
            //            }
            //        }
            //        dato.Add(ExcelReader.GetName(i).ToString().Trim(), ExcelReader.GetValue(i).ToString().Trim());
            //    }
            //    lstFila.Add(dato);
            //}
            //ExcelConnection.Close();
            using (var connection = new OleDbConnection(ConnectionString))
            {
                using (var da = new OleDbDataAdapter(ExcelQuery, connection))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    firstTable = ds.Tables[0];
                }
            }
            return firstTable;
        }
    }
}
