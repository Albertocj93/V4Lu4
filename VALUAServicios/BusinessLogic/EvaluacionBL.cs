using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessEntities;
using Common;
using Utility;

namespace BusinessLogic
{
    public class EvaluacionBL
    {
        public string CalcularCompetenciaPTS(string connstring, string CompetenciaT, string CompetenciaG, string CompetenciaRH)
        {
            
            string CompetenciaPTS="";
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            if (!string.IsNullOrEmpty(CompetenciaT) || !string.IsNullOrEmpty(CompetenciaG) || !string.IsNullOrEmpty(CompetenciaRH))
            {
                MatrizEvaluacionBE MatrizKHT = new MatrizEvaluacionBE();
                MatrizEvaluacionBE MatrizKHG = new MatrizEvaluacionBE();
                MatrizEvaluacionBE MatrizTAB = new MatrizEvaluacionBE();

                MatrizKHT = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizKHT_Tabla, CompetenciaT, "", "");
                
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizKHG = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper,MatricesEvaluacion.MatrizKHG_Tabla, CompetenciaG, "", "");

                int X;
                if(MatrizKHG.Signo ==MatrizKHT.Signo)
                {
                    // Valor de la variable RH + Columna “Sumar” de la tabla KHG para la variable “G” seleccionada.
                    X = Convert.ToInt32(CompetenciaRH) + Convert.ToInt32(MatrizKHG.Sumar)-2;
                }
                else
                {
                    // Valor de la variable RH + Columna “Sumar” de la tabla KHG para la variable “G” seleccionada 
                    // + Columna “Sumar” de la tabla KHT para la variable “T” seleccionada – 2
                    X = Convert.ToInt32(CompetenciaRH) + Convert.ToInt32(MatrizKHG.Sumar) + Convert.ToInt32(MatrizKHT.Sumar) - 2;
                }

                int PTS;

                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                //Buscar en la tabla TAB 
                MatrizTAB = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(
                    _oDBHelper,
                    MatricesEvaluacion.MatrizTAB_Tabla,
                    "",
                    ((Convert.ToInt32(MatrizKHT.Valor) + X )).ToString(), // la fila (columna valor de la tabla KHT de la variable T elegida + “X” + 1)
                    (Convert.ToInt32(MatrizKHG.Valor) ).ToString() // columna (columna valor de la tabla KHG de la variable G elegida + 1)
                    );

                PTS = Convert.ToInt32(MatrizTAB.Valor);

                if(!string.IsNullOrEmpty(CompetenciaRH))
                { 
                    CompetenciaPTS = PTS.ToString(); 
                }
                
            }

            return CompetenciaPTS;
        }
        public string CalcularSolucionPORC(string connstring,string SolucionA,string SolucionD)
        {
            string SolucionPORC="";
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            if (!string.IsNullOrEmpty(SolucionA) || !string.IsNullOrEmpty(SolucionA))
            {
                MatrizEvaluacionBE MatrizRC = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizRC_Tabla, SolucionA, "", "");
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizRD = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizRD_Tabla, SolucionD, "", "");
                

                int X,Y,W3,X3,Z;

                X = Convert.ToInt32(MatrizRD.Valor);
                Y = Convert.ToInt32(MatrizRC.Valor);
                W3 = Convert.ToInt32(MatrizRC.Sumar);
                X3 = Convert.ToInt32(MatrizRD.Sumar);

                if((W3+X3)>0)
                { Z = 1; }
                else
                { Z = 0; }

                if(!string.IsNullOrEmpty(SolucionD))
                {
                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                    MatrizEvaluacionBE MatrizSP = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(
                                _oDBHelper, 
                                MatricesEvaluacion.MatrizSP_Tabla, 
                                "", 
                                (Y+Z).ToString(), 
                                (X).ToString());

                    SolucionPORC = MatrizSP.Valor;
                }
            }

            return SolucionPORC;
        }
        public string CalcularSolucionPTS(string connstring,string SolucionPORC,string CompetenciaPTS)
        {
            string SolucionPTS = "";

            if(!string.IsNullOrEmpty(SolucionPORC) || !string.IsNullOrEmpty(CompetenciaPTS))
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizNivel = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizNivel_Tabla, CompetenciaPTS, "", "");
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizSPNV = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizSPNV_Tabla, SolucionPORC, "", "");

                int X, Y,W;

                X = Convert.ToInt32(MatrizNivel.Valor);
                Y = Convert.ToInt32(MatrizSPNV.Valor);

                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizPONTO = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizPONTO_Tabla, (X - Y).ToString(), "", "");

                W = Convert.ToInt32(MatrizPONTO.Valor);

                if(!string.IsNullOrEmpty(SolucionPORC))
                {
                    SolucionPTS = W.ToString();
                }
            }
            return SolucionPTS;
        }
        public string CalcularResponsabilidadPTS(string connstring, string ResponsabilidadA, string ResponsabilidadM,string ResponsabilidadI)
        {
            string ResponsabilidadPTS = "";
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            if(!string.IsNullOrEmpty(ResponsabilidadA) || !string.IsNullOrEmpty(ResponsabilidadM) || !string.IsNullOrEmpty(ResponsabilidadI))
            {
                MatrizEvaluacionBE MatrizLA = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizLA_Tabla, ResponsabilidadA, "", "");
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizM = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizM_Tabla, ResponsabilidadM, "", "");
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizI = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizI_Tabla, ResponsabilidadI, "", "");

                int X,AB3,AA3,Y,Z,K,MS;

                X = Convert.ToInt32(MatrizLA.Valor);
                K = Convert.ToInt32(MatrizLA.Sumar);
                MS = Convert.ToInt32(MatrizM.Sumar);
                AB3 = Convert.ToInt32(MatrizI.Valor);
                AA3 = Convert.ToInt32(MatrizI.Sumar);


                if ((K + MS + AA3) > 1)
                {
                    Y = 1;
                }
                else
                {
                    if ((K + MS + AA3) < -1)
                    {
                        Y = -1;
                    }
                    else
                    {
                        Y = K + MS + AA3;
                    }
                }

                Z = Convert.ToInt32(MatrizM.Valor) + AB3 + 1;

                if(!string.IsNullOrEmpty(ResponsabilidadM))
                {
                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                    MatrizEvaluacionBE MatrizRR = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(
                        _oDBHelper,
                        MatricesEvaluacion.MatrizRR_Tabla,
                        "",
                        (X + Y ).ToString(),
                        (Z-1).ToString()
                        );

                    ResponsabilidadPTS = MatrizRR.Valor;
                }
            }
            return ResponsabilidadPTS;
        }
        public string CalcularTotal(string connstring,string CompetenciaPTS,string SolucionPTS,string ResponsabilidadPTS)
        {
            string Total = "";

            if (!string.IsNullOrEmpty(CompetenciaPTS) || !string.IsNullOrEmpty(SolucionPTS) || !string.IsNullOrEmpty(ResponsabilidadPTS))
            {
                Total = (
                    Convert.ToInt32(CompetenciaPTS) + 
                    Convert.ToInt32(SolucionPTS) + 
                    Convert.ToInt32(ResponsabilidadPTS)
                        ).ToString();
            }

            return Total;
        }
        public string CalcularPerfil(string connstring, string SolucionPTS, string ResponsabilidadPTS, string Total)
        {
            string Perfil = "";

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            MatrizEvaluacionBE MatrizNivelX = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizNivel_Tabla, ResponsabilidadPTS, "", "");
            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            MatrizEvaluacionBE MatrizNivelY = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizNivel_Tabla, SolucionPTS, "", "");

            int X, Y;

            X = Convert.ToInt32(MatrizNivelX.Valor);
            Y = Convert.ToInt32(MatrizNivelY.Valor);

            if(!string.IsNullOrEmpty(Total))
            {
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                MatrizEvaluacionBE MatrizPF = EvaluacionDA.Instanse.MatricesEvaluacionGetByTablaVariableFilaColumna(_oDBHelper, MatricesEvaluacion.MatrizPF_Tabla, (X-Y).ToString(), "", "");

                Perfil = MatrizPF.Valor.ToString();
            }

            return Perfil;
        }
        public string CalcularGrado(string connstring,string Total)
        {
            string Grado = "";

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            if(!string.IsNullOrEmpty(Total))
            {
                GradeStructureBE GradeStructure = EvaluacionDA.Instanse.GradeStructureGetByTotal(_oDBHelper, Total);

                Grado = GradeStructure.Gs;
            }
            return Grado;
        }
        public string CalcularPuntoMedio(string connstring, string Total)
        {
            string PuntoMedio = "";

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            if (!string.IsNullOrEmpty(Total))
            {
                GradeStructureBE GradeStructure = EvaluacionDA.Instanse.GradeStructureGetByTotal(_oDBHelper, Total);

                PuntoMedio = GradeStructure.Mid;
            }
            return PuntoMedio;
        }
        public ValoresEvaluacionBE ValoresEvaluacionGetIdByDesc(string connstring, string Descripcion, string Tipo)
        {
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            ValoresEvaluacionBE ValoresEvaluacionBE = EvaluacionDA.Instanse.ValoresEvaluacionGetIdByDesc(_oDBHelper, Descripcion, Tipo);
            return ValoresEvaluacionBE;
        }
        public List<ValoresEvaluacionBE> ValoresEvaluacionGetAllByTipo(string connstring, string Tipo)
        {
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            List<ValoresEvaluacionBE> LValoresEvaluacion = EvaluacionDA.Instanse.ValoresEvaluacionGetAllByTipo(_oDBHelper, Tipo);

            return LValoresEvaluacion;
        }
    }
}
