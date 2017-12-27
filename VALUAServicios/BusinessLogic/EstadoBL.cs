using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class EstadoBL
    {
        public List<EstadoBE> GetAll(string connstring)
        {
            try
            {
                List<EstadoBE> oLista = new List<EstadoBE>();
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);

                oLista = EstadoDA.Instanse.GetAll(_oDBHelper);

                return oLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoBE> EstadosGetByIdEstadoInicial(string connstring,int IdEstadoInicial)
        {
            try
            {
                List<EstadoBE> oLista = new List<EstadoBE>();
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                oLista = EstadoDA.Instanse.GetByIdEstadoInicial(_oDBHelper, IdEstadoInicial);

                return oLista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoBE> GetSiguienteByIdPuesto(string connstring, int IdPuesto)
        {
            try
            {
                List<EstadoBE> oLista = new List<EstadoBE>();
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                oLista = EstadoDA.Instanse.GetSiguienteByIdPuesto(_oDBHelper, IdPuesto);

                return oLista;
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
                return EstadoDA.Instanse.GetIdByDesc(_oDBHelper,descripcion);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
