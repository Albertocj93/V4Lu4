using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class PaisBL
    {
        //public List<PaisBE> GetAll()
        //{
        //    PaisDA PaisDA = new PaisDA();

        //    List<PaisBE> oLista = new List<PaisBE>();
        //    oLista = PaisDA.GetAll();

        //    return oLista;
        //}


        public List<PaisBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PaisDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insert(string connstring, PaisBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PaisDA.Instanse.Insert(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(string connstring, PaisBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PaisDA.Instanse.Update(_oDBHelper, obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string connstring, PaisBE obj)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PaisDA.Instanse.Delete(_oDBHelper, obj);
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

                return PaisDA.Instanse.GetIdByDesc(_oDBHelper, descripcion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PaisBE GetByIdPais(string connstring, int IdPais)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                return PaisDA.Instanse.GetByIdPais(_oDBHelper, IdPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
