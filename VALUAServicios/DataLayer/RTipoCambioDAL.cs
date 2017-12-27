using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viatecla.Factory.Scriptor;
using Viatecla.Factory.Web;
using ScriptorModel = Viatecla.Factory.Scriptor.ModularSite.Models;
using BusinessEntities;
using Common;

namespace DataLayer
{
    public class RTipoCambioDAL
    {
        public RTipoCambioBE ObtenerTipoCambioPorSociedad(string idSociedad)
        {
            //ScriptorChannel canalTipoCambio = ScriptorModel.Common.ScriptorClient.GetChannel(new Guid(Canales.TipoCambio));

            ScriptorChannel canalTipoCambio = new ScriptorClient().GetChannel(new Guid(Canales.TipoCambio));
            
            //Traer todos los tipos de cambio
            List<ScriptorContent> TiposCambio = canalTipoCambio.QueryContents("#Id", Guid.NewGuid(), "<>").ToList();

            //Traer el tipo de cambio de la sociedad, año y mes actual
            ScriptorContent contenido = TiposCambio.Where(x => x.Parts.Anio.Title == DateTime.Now.Year.ToString() && x.Parts.Mes.Value.ToString() == DateTime.Now.Month.ToString() && x.Parts.IdSociedad.Value.ToUpper() == idSociedad.ToUpper()).ToList().FirstOrDefault();
            RTipoCambioBE oTipoCambio = new RTipoCambioBE();

            if (contenido != null)
            {
                //ScriptorContent contenido = TiposCambio.First(x => idSociedad == x.Parts.IdSociedad.Value);

                oTipoCambio.Id = contenido.Id;
                oTipoCambio.Mes = contenido.Parts.Mes.Title;
                oTipoCambio.Anio = contenido.Parts.Anio.Title;
                oTipoCambio.IdMoneda = contenido.Parts.IdMoneda.Value.ToString().ToLower();
                oTipoCambio.Moneda = contenido.Parts.IdMoneda.Title;
                oTipoCambio.MontoTipoCambio = contenido.Parts.MontoTipoCambio;
                oTipoCambio.Sociedad = contenido.Parts.IdSociedad.Value;

            }
            else
            {
                //Traer el ultimo registrado
                //contenido = TiposCambio.Where(x => int.Parse(x.Parts.Anio.Title) <= DateTime.Now.Year && int.Parse(x.Parts.Mes.Value) <= DateTime.Now.Month && x.Parts.IdSociedad.Value.ToUpper() == idSociedad.ToUpper()).OrderByDescending( x => x.Parts.Anio).OrderByDescending(x => x.Parts.Mes).ToList().FirstOrDefault();
                contenido = TiposCambio.Where(x => int.Parse(x.Parts.Anio.Title) <= DateTime.Now.Year).
                    Where(x => x.Parts.IdSociedad.Value.ToUpper() == idSociedad.ToUpper()).
                    OrderByDescending(x => int.Parse(x.Parts.Anio.Title)).
                    OrderByDescending(x => int.Parse(x.Parts.Mes.Value.ToString())).ToList().FirstOrDefault();

                if (contenido != null)
                {
                    oTipoCambio.Id = contenido.Id;
                    oTipoCambio.Mes = contenido.Parts.Mes.Title;
                    oTipoCambio.Anio = contenido.Parts.Anio.Title;
                    oTipoCambio.IdMoneda = contenido.Parts.IdMoneda.Value.ToString().ToLower();
                    oTipoCambio.Moneda = contenido.Parts.IdMoneda.Title;
                    oTipoCambio.MontoTipoCambio = contenido.Parts.MontoTipoCambio;
                    oTipoCambio.Sociedad = contenido.Parts.IdSociedad.Value;
                }
 
            }

            return oTipoCambio;
        }

    }
}
