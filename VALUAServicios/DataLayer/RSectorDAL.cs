using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities.DTO;
using Common;
using System.Data.SqlClient;
using System.Configuration;



namespace DataLayer
{
    public class RSectorDAL
    {
        public RSectorBE ObtenerSectorPorCeCo(string idCeCo)
        {
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            ScriptorContent contenido = canalMaestroCeCos.QueryContents("#Id", idCeCo, "=").ToList().FirstOrDefault();

            if (contenido != null)
            {
                RSectorBE oSector = new RSectorBE();
                if (((ScriptorDropdownListValue)contenido.Parts.IdSector).Content != null)
                {
                    oSector.Id = ((ScriptorDropdownListValue)contenido.Parts.IdSector).Content.Id;
                    oSector.Descripcion = ((ScriptorDropdownListValue)contenido.Parts.IdSector).Content.Parts.Descripcion;
                }
                return oSector;
            }
            else
                return null;
        }

        public List<RSectorBE> ObtenerSector()
        {
            ScriptorChannel canalSector = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Sector));

            List<ScriptorContent> oLista = canalSector.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("fk_workflowstate", "published", "=").ToList();

            List<RSectorBE> oListaSector = new List<RSectorBE>();
            RSectorBE oSector;

            foreach (ScriptorContent item in oLista)
            {
                oSector = new RSectorBE();
                oSector.Codigo = item.Parts.Codigo;
                oSector.Descripcion = item.Parts.Descripcion;
                oSector.Id = item.Id;

                oListaSector.Add(oSector);
 
            }

            return oListaSector;
        }

        
    }
}
