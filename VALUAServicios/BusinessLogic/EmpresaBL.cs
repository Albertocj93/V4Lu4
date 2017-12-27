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
    public class EmpresaBL
    {

      


        public List<EmpresaBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(string connstring, EmpresaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(string connstring, EmpresaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.Update(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string connstring, EmpresaBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.Delete(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EmpresaBE GetByIdEmpresa(string connstring, int IdEmpresa)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.GetByIdEmpresa(_oDBHelper, IdEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetIdByDesc(string connstring, string descripcion)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return EmpresaDA.Instanse.GetIdByDesc(_oDBHelper,descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EmpresaBE> GetByUser(string Usuario)
        {
            List<EmpresaBE> oLista = new List<EmpresaBE>();
            oLista = EmpresaDA.Instanse.GetByUser(Usuario);

            return oLista;
        }

    }
}
