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
    public class AdjuntoBL
    {
        public void Insert(AdjuntoBE AdjuntoBE)
        {
            AdjuntoDA.Instanse.Insert(AdjuntoBE);
        }

        public void DeleteByIdCargaFilename(AdjuntoBE AdjuntoBE)
        {
            AdjuntoDA.Instanse.DeleteByIdCargaFilename(AdjuntoBE);
        }
        public AdjuntoBE GetByIdAdjunto(string IdAdjunto)
        {
            AdjuntoBE AdjuntoBE = new AdjuntoBE();

            AdjuntoBE = AdjuntoDA.Instanse.GetByIdAdjunto(IdAdjunto);
            
            return AdjuntoBE;
        }
        public bool AdjuntoTemporalInsert(string connstring,AdjuntoBE AdjuntoBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return AdjuntoDA.Instanse.AdjuntoTemporalInsert(_oDBHelper, AdjuntoBE);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool AdjuntoTemporalDeleteByIdCargaFilename(string connstring, AdjuntoBE AdjuntoBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return AdjuntoDA.Instanse.AdjuntoTemporalDeleteByIdCargaFilename(_oDBHelper, AdjuntoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool AdjuntoTemporalDeleteByIdCarga(string connstring, AdjuntoBE AdjuntoBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return AdjuntoDA.Instanse.AdjuntoTemporalDeleteByIdCarga(_oDBHelper, AdjuntoBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AdjuntoBE GetByIdAdjunto(string connstring, int IdAdjunto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return AdjuntoDA.Instanse.GetByIdAdjunto(_oDBHelper, IdAdjunto);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public AdjuntoBE GetLastByUser(string connstring, string CuentaRed)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return AdjuntoDA.Instanse.GetLastByUser(_oDBHelper, CuentaRed);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
