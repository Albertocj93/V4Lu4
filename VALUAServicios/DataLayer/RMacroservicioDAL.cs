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
    public class RMacroservicioDAL
    {
        public RMacroservicioBE ObtenerMacroservicioPorCeCo(string idCeCo)
        {
            ScriptorChannel canalMaestroCeCos = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.MaestroCeCo));

            ScriptorContent contenido = canalMaestroCeCos.QueryContents("#Id", idCeCo, "=").ToList().FirstOrDefault();
            if (contenido != null)
            {
                RMacroservicioBE oMacroservicio = new RMacroservicioBE();
                if (((ScriptorDropdownListValue)contenido.Parts.IdMacroservicio).Content != null)
                {
                    oMacroservicio.Id = ((ScriptorDropdownListValue)contenido.Parts.IdMacroservicio).Content.Id;
                    oMacroservicio.Descripcion = ((ScriptorDropdownListValue)contenido.Parts.IdMacroservicio).Content.Parts.Descripcion;
                }

                return oMacroservicio;
            }
            else
                return null;
 
        }

        public List<RMacroservicioBE> ObtenerMacroservicio()
        {
            ScriptorChannel canalMacroservicio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.Macroservicio));

            List<ScriptorContent> oLista = canalMacroservicio.QueryContents("#Id", Guid.NewGuid(), "<>").QueryContents("fk_workflowstate", "published", "=").ToList();
            List<RMacroservicioBE> oListaMacroservicio = new List<RMacroservicioBE>();
            
            RMacroservicioBE oMacroservicio;

            foreach (ScriptorContent item in oLista)
            {
                oMacroservicio = new RMacroservicioBE();

                oMacroservicio.Codigo = item.Parts.Codigo;
                oMacroservicio.Descripcion = item.Parts.Descripcion;
                oMacroservicio.Id = item.Id;

                oListaMacroservicio.Add(oMacroservicio);
            }

            return oListaMacroservicio;

        }

        
    }
}
