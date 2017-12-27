using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class VisitaBL
    {

        public bool Insert(string connstring, VisitaBE obj)
        {
            try
            {
                UsuarioBL ubl = new UsuarioBL();
                UsuarioBE usuario = ubl.GetByAccount(connstring, obj.UsuarioCreacion);

                obj.IdEmpresa = usuario.IdEmpresa;

                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return VisitaDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ReporteBE ReporteUsoEmpresa(string connstring)
        {
            try
            {
                ReporteBE reporte = new ReporteBE();
                reporte.text = ".";


                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                reporte.children = VisitaDA.Instanse.ReporteUsoGetEmpresa(_oDBHelper);

                foreach (ReporteChildBE childEmpresa in reporte.children)
                {
                    childEmpresa.leaf = false;
                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                    childEmpresa.children = VisitaDA.Instanse.ReporteUsoGetAnho(_oDBHelper, childEmpresa);

                    foreach (ReporteChildBE childAnho in childEmpresa.children)
                    {
                        childAnho.leaf = false;
                        childAnho.task = childAnho.anho.ToString();
                        _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                        childAnho.children = VisitaDA.Instanse.ReporteUsoGetSemestre(_oDBHelper, childAnho);

                        foreach (ReporteChildBE childSemestre in childAnho.children)
                        {
                            childSemestre.leaf = false;
                            childSemestre.task = "Semestre " + childSemestre.semestre;
                            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                            childSemestre.children = VisitaDA.Instanse.ReporteUsoGetTrimestre(_oDBHelper, childSemestre);

                            foreach (ReporteChildBE childTrimestre in childSemestre.children)
                            {
                                childTrimestre.leaf = false;
                                childTrimestre.task = "Trimestre " + childTrimestre.trimestre;
                                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                childTrimestre.children = VisitaDA.Instanse.ReporteUsoGetMes(_oDBHelper, childTrimestre);

                                foreach (ReporteChildBE childMes in childTrimestre.children)
                                {
                                    childMes.leaf = false;
                                    childMes.task = new DateTime(childMes.anho, childMes.mes, 01).ToString("MMMM");
                                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                    childMes.children = VisitaDA.Instanse.ReporteUsoGetDia(_oDBHelper, childMes);

                                    foreach (ReporteChildBE childDia in childMes.children)
                                    {
                                        childDia.leaf = true;
                                        childDia.task = new DateTime(childDia.anho, childDia.mes, childDia.dia).ToString("dd dddd");

                                    }
                                }
                            }

                        }
                    }
                }


                return reporte;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteBE ReporteUsoUsuarioEmpresa(string connstring,int IdEmpresa)
        {
            try
            {
                ReporteBE reporte = new ReporteBE();
                reporte.text = ".";


                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                reporte.children = VisitaDA.Instanse.ReporteUsoUsuGetUsuarios(_oDBHelper,IdEmpresa);
                foreach (ReporteChildBE childUsuario in reporte.children)
                {
                    childUsuario.leaf = false;
                    childUsuario.task = childUsuario.NombreUsuario;

                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                    childUsuario.children = VisitaDA.Instanse.ReporteUsoUsuGetAnho(_oDBHelper, childUsuario);
                    foreach(ReporteChildBE childAnho in childUsuario.children)
                    {
                        childAnho.leaf = false;
                        childAnho.task = childAnho.anho.ToString();

                        _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                        childAnho.children = VisitaDA.Instanse.ReporteUsoUsuGetSemestre(_oDBHelper,childAnho);
                        foreach(ReporteChildBE childSemestre in childAnho.children)
                        {
                            childSemestre.leaf = false;
                            childSemestre.task = "Semestre " + childSemestre.semestre;

                            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                            childSemestre.children = VisitaDA.Instanse.ReporteUsoUsuGetTrimestre(_oDBHelper,childSemestre);
                            foreach(ReporteChildBE childTrimestre in childSemestre.children)
                            {
                                childTrimestre.leaf = false;
                                childTrimestre.task = "Trimestre " + childTrimestre.trimestre;

                                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                childTrimestre.children = VisitaDA.Instanse.ReporteUsoUsuGetMes(_oDBHelper,childTrimestre);
                                foreach(ReporteChildBE childMes in childTrimestre.children)
                                {
                                    childMes.leaf = false;
                                    childMes.task = new DateTime(childMes.anho, childMes.mes, 01).ToString("MMMM");

                                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                    childMes.children = VisitaDA.Instanse.ReporteUsoUsuGetDia(_oDBHelper, childMes);

                                    foreach(ReporteChildBE childDia in childMes.children)
                                    {
                                        childDia.leaf = true;
                                        childDia.task = new DateTime(childDia.anho, childDia.mes, childDia.dia).ToString("dd dddd");

                                    }

                                }
                            }
                        }
                    }
                }

                return reporte;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
