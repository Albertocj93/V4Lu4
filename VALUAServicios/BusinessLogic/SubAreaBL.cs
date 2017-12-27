using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class SubAreaBL
    {
        public List<SubAreaBE> GetAll()
        {
            List<SubAreaBE> oLista = new List<SubAreaBE>();
            oLista = SubAreaDA.Instanse.GetAll();

            return oLista;
        }

        public List<SubAreaBE> GetByIdArea(int IdArea)
        {
            List<SubAreaBE> oLista = new List<SubAreaBE>();
            oLista = SubAreaDA.Instanse.GetByIdArea(IdArea);

            return oLista;
        }
        public int GetIdByDescSArAreDepEmp(string connstring ,string SubArea, string DescArea, string DescEmpresa, string DescDepartamento)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return SubAreaDA.Instanse.GetIdByDescSArAreDepEmp(_oDBHelper,SubArea, DescArea, DescEmpresa, DescDepartamento);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public List<SubAreaBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return SubAreaDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(string connstring, SubAreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return SubAreaDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(string connstring, SubAreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return SubAreaDA.Instanse.Update(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string connstring, SubAreaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return SubAreaDA.Instanse.Delete(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
