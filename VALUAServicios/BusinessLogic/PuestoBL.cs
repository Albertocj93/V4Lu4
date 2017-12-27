using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class PuestoBL
    {
        public List<PuestoBE> ExportarByUser(string connstring, string CuentaUsuario)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.ExportarByUser(_oDBHelper, CuentaUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PuestoBE> ExportarEliminadosByUser(string connstring, string CuentaUsuario)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.ExportarEliminadosByUser(_oDBHelper, CuentaUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PuestoBE> GetByUser(string connstring, string CuentaUsuario)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GetByUser(_oDBHelper, CuentaUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PuestoBE> GetByUserAdministrador(string connstring, string CuentaUsuario,int IdEmpresa)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GetByUserAdministrador(_oDBHelper, CuentaUsuario, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PuestoBE> GetByUserAdministradorFiltros(string connstring, string CuentaUsuario, int IdEmpresa, int IdPais, int IdDepartamento,
                                                        int IdArea, int IdSubArea, string TituloPuesto, string NombreOcupante, string CodigoValua)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GetByUserAdministradorFiltros(_oDBHelper, CuentaUsuario, IdEmpresa,IdPais, IdDepartamento, IdArea, IdSubArea
                                                                        , TituloPuesto, NombreOcupante, CodigoValua );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PuestoBE> GetEliminadosByUser(string connstring, string CuentaUsuario)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GetEliminadosByUser(_oDBHelper, CuentaUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(string connstring, PuestoBE pPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.Insert(_oDBHelper, pPuesto);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(string connstring, PuestoBE pPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.Update(_oDBHelper, pPuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(string connstring, PuestoBE pPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.Delete(_oDBHelper, pPuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SetInactive(string connstring, PuestoBE pPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.SetInactive(_oDBHelper, pPuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PuestoBE GetById(string connstring, int IdPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GetById(_oDBHelper, IdPuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GenerarCorrelativo(string connstring, string cadena)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PuestoDA.Instanse.GenerarCorrelativo(_oDBHelper, cadena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgregarAdjuntosAPuestosByIdCarga(string connstring, string IdCarga, PuestoBE PuestoBE)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            List<AdjuntoBE> oListaAdjuntos = AdjuntoDA.Instanse.AdjuntoTemporalGetByIdCarga(_oDBHelper, IdCarga);

            foreach (AdjuntoBE oAdjunto in oListaAdjuntos)
            {
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                PuestoDA.Instanse.InsertAdjuntoTemporalInPuesto(_oDBHelper, oAdjunto, PuestoBE);                
            }
        }
        public List<PuestoBE> GetHistoriaById(string connstring,PuestoBE PuestoBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return PuestoDA.Instanse.GetHistoriaById(_oDBHelper,PuestoBE);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string[,] GetMatrizMapaPuestos(string connstring, string CuentaUsuario)
        {
            
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            int ColumnasMatriz = 1 + PuestoDA.Instanse.MapaPuestoGetColumnas(_oDBHelper,CuentaUsuario);

            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            List<GradeStructureBE> LGrado = PuestoDA.Instanse.MapaPuestosGetGradoInPuesto(_oDBHelper,CuentaUsuario);

            int Filas = 5 + LGrado.Count;

            string[,] MatrizMapaPuestos = new string[Filas, ColumnasMatriz]; 
            MatrizMapaPuestos[0, 0] = "Grado";
            
            int Ftemp = 5;
            foreach (GradeStructureBE oGradeStructure in LGrado)
            {
                MatrizMapaPuestos[Ftemp,0] = oGradeStructure.Gs;
                Ftemp++;
            }
            string GradoTemp;
            int Columna = 1;

            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            List<EmpresaBE> LEmpresa = PuestoDA.Instanse.MapaPuestosGetEmpresaInPuesto(_oDBHelper, CuentaUsuario);
            foreach (EmpresaBE oEmpresa in LEmpresa)
            {
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                List<PaisBE> LPais = PuestoDA.Instanse.MapaPuestosGetPaisInPuestoByEmpresa(_oDBHelper,oEmpresa.Id);
                foreach(PaisBE oPais in LPais)
                {
                    _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                    List<DepartamentoBE> LDepartamento = PuestoDA.Instanse.MapaPuestosGetDepartamentoInPuestoByEmpresaPais(_oDBHelper,oEmpresa.Id,oPais.Id);
                    foreach(DepartamentoBE oDepartamento in LDepartamento)
                    {
                        _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                        List<AreaBE> LArea = PuestoDA.Instanse.MapaPuestosGetAreaInPuestoByEmpresaPaisDepartamento(_oDBHelper, oEmpresa.Id, oPais.Id, oDepartamento.Id);
                        if(LArea.Count>0)
                        {
                            foreach(AreaBE oArea in LArea)
                            {
                                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                List<SubAreaBE> LSubArea = PuestoDA.Instanse.MapaPuestosGetSubAreaInPuestoByEmpresaPaisDepartamentoArea(_oDBHelper, oEmpresa.Id, oPais.Id, oDepartamento.Id,oArea.Id);
                                if(LSubArea.Count >0)
                                {
                                    foreach(SubAreaBE oSubArea in LSubArea)
                                    {
                                        MatrizMapaPuestos[0, Columna] = oEmpresa.Descripcion;
                                        MatrizMapaPuestos[1, Columna] = oPais.Descripcion;
                                        MatrizMapaPuestos[2, Columna] = oDepartamento.Descripcion;
                                        MatrizMapaPuestos[3, Columna] = oArea.Descripcion;
                                        MatrizMapaPuestos[4, Columna] = oSubArea.Descripcion;
                                        
                                        for (Ftemp= 5; Ftemp < Filas;Ftemp++ )
                                        {
                                            GradoTemp = MatrizMapaPuestos[Ftemp, 0];
                                            _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                            MatrizMapaPuestos[Ftemp, Columna] = PuestoDA.Instanse.MapaPuestoGetPuestos(_oDBHelper, oEmpresa.Id, oPais.Id, oDepartamento.Id, oArea.Id, oSubArea.Id,GradoTemp);
                                        }
                                            Columna++;
                                    }
                                }
                                else
                                {
                                    MatrizMapaPuestos[0, Columna] = oEmpresa.Descripcion;
                                    MatrizMapaPuestos[1, Columna] = oPais.Descripcion;
                                    MatrizMapaPuestos[2, Columna] = oDepartamento.Descripcion;
                                    MatrizMapaPuestos[3, Columna] = oArea.Descripcion;
                                    for (Ftemp = 5; Ftemp < Filas; Ftemp++)
                                    {
                                        GradoTemp = MatrizMapaPuestos[Ftemp, 0];
                                        _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                        MatrizMapaPuestos[Ftemp, Columna] = PuestoDA.Instanse.MapaPuestoGetPuestos(_oDBHelper, oEmpresa.Id, oPais.Id, oDepartamento.Id, oArea.Id, -1, GradoTemp);
                                    }
                                    Columna++;
                                }
                            }
                        }
                        else
                        {
                            MatrizMapaPuestos[0, Columna] = oEmpresa.Descripcion;
                            MatrizMapaPuestos[1, Columna] = oPais.Descripcion;
                            MatrizMapaPuestos[2, Columna] = oDepartamento.Descripcion;

                            for (Ftemp = 5; Ftemp < Filas; Ftemp++)
                            {
                                GradoTemp = MatrizMapaPuestos[Ftemp, 0];

                                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                                MatrizMapaPuestos[Ftemp, Columna] = PuestoDA.Instanse.MapaPuestoGetPuestos(_oDBHelper, oEmpresa.Id, oPais.Id, oDepartamento.Id, -1, -1, GradoTemp);

                            }
                            Columna++;
                        }
                    }
                }
            }
                return MatrizMapaPuestos;
        }
        public void AgregarAdjuntosAPuestosByIdCargaIdPuesto(string connstring, string IdCarga, PuestoBE PuestoBE)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            List<AdjuntoBE> oListaAdjuntos = AdjuntoDA.Instanse.AdjuntoTemporalGetByIdCarga(_oDBHelper, IdCarga);

            foreach (AdjuntoBE oAdjunto in oListaAdjuntos)
            {
                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                PuestoDA.Instanse.PuestoInsertAdjuntoInPuestoByIdPuesto(_oDBHelper, oAdjunto, PuestoBE);
            }
        }
        public void DeleteAdjuntoByIdPuesto(string connstring,  PuestoBE PuestoBE)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            
            PuestoDA.Instanse.DeleteAdjuntoByIdPuesto(_oDBHelper,PuestoBE);
            
        }
        public int GetCountByIdPais(string connstring, int IdPais)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            return PuestoDA.Instanse.GetCountByIdPais(_oDBHelper, IdPais);

        }
        public int GetCountByIdEmpresa(string connstring, int IdEmpresa)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            return PuestoDA.Instanse.GetCountByIdEmpresa(_oDBHelper, IdEmpresa);

        }
        public int GetByIdDepartamento(string connstring, int IdDepartamento)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            return PuestoDA.Instanse.GetByIdDepartamento(_oDBHelper, IdDepartamento);

        }
        public int GetByIdArea(string connstring, int IdArea)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            return PuestoDA.Instanse.GetByIdArea(_oDBHelper, IdArea);

        }
        public int GetByIdSubArea(string connstring, int IdSubArea)
        {

            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

            return PuestoDA.Instanse.GetByIdSubArea(_oDBHelper, IdSubArea);

        }
    }
}
