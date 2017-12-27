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
    public class DepartamentoBL
    {
       
        public List<DepartamentoBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return DepartamentoDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(string connstring, DepartamentoBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return DepartamentoDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(string connstring, DepartamentoBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return DepartamentoDA.Instanse.Update(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string connstring, DepartamentoBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return DepartamentoDA.Instanse.Delete(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DepartamentoBE> GetByIdEmpresa(string connstring, DepartamentoBE obj)
        {
            DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
            return DepartamentoDA.Instanse.GetByIdEmpresa(_oDBHelper,obj);
        }
        public DepartamentoBE GetByIdDepartamento(string connstring, int IdDepartamento)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return DepartamentoDA.Instanse.GetByIdDepartamento(_oDBHelper, IdDepartamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIdByDescDepEmp(string connstring, string empresa, string departamento)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return DepartamentoDA.Instanse.GetIdByDescDepEmp(_oDBHelper,empresa, departamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
