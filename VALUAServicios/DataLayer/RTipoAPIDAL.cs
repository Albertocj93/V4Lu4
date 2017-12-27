using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using Common;

namespace DataLayer
{
    public class RTipoAPIDAL
    {
        public List<RTipoAPIBE> ObtenerTipoAPI()
        {
            ScriptorChannel canalTipoAPI = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoAPI));

            //Obtener CeCos de la ultima version de la sociedad especificada
            List<ScriptorContent> listaTipoAPI = canalTipoAPI.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            List<RTipoAPIBE> oListaTipoAPI = new List<RTipoAPIBE>();
            RTipoAPIBE oTipoAPI;

            foreach (ScriptorContent item in listaTipoAPI)
            {
                if(item.Id.ToString().ToLower() != TiposAPI.IdTraslado.ToLower())
                {
                    oTipoAPI = new RTipoAPIBE();

                    oTipoAPI.Id = item.Id.ToString();
                    oTipoAPI.Codigo = item.Parts.Codigo;
                    oTipoAPI.Descripcion = item.Parts.Descripcion;
                    oListaTipoAPI.Add(oTipoAPI);
                }

                

            }

            return oListaTipoAPI;
            

        }

        
    }
}
