using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class AreaBL
    {
       
        public List<AreaBE> GetByIdDepartamento(int IdDepartamento)
        {
            List<AreaBE> oLista = new List<AreaBE>();
            oLista = AreaDA.Instanse.GetByIdDepartamento(IdDepartamento);
            return oLista;
        }
        public int GetIdByDescAreDepEmp(string connstring,string DescArea, string DescEmpresa, string DescDepartamento)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return AreaDA.Instanse.GetIdByDescAreDepEmp(_oDBHelper,DescArea, DescEmpresa, DescDepartamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public List<AreaBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return AreaDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(string connstring, AreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return AreaDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(string connstring, AreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return AreaDA.Instanse.Update(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string connstring, AreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return AreaDA.Instanse.Delete(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
