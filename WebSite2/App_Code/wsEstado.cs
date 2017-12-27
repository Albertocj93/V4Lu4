using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BusinessEntities;
using BusinessLogic;
using System.Web.Script.Serialization;
using Common;
using System.Configuration;
using System.Net;
using System.IO;
using System.Globalization;

/// <summary>
/// Descripción breve de wsEstado
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsEstado : System.Web.Services.WebService {

    public wsEstado () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    [WebMethod]
    public void GetAll()
    {
        EstadoBL EstadoBL = new EstadoBL();
        List<EstadoBE> oLista = new List<EstadoBE>();

        oLista = EstadoBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void EstadosGetByIdEstadoInicial(string IdEstadoInicial)
    {
        EstadoBL EstadoBL = new EstadoBL();
        List<EstadoBE> oLista = new List<EstadoBE>();

        oLista = EstadoBL.EstadosGetByIdEstadoInicial(connstring,Convert.ToInt32(IdEstadoInicial));

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetSiguienteByIdPuesto(string IdPuesto)
    {
        EstadoBL EstadoBL = new EstadoBL();
        List<EstadoBE> oLista = new List<EstadoBE>();

        int idPues = string.IsNullOrEmpty(IdPuesto) ? -1 : Convert.ToInt32(IdPuesto);

        oLista = EstadoBL.GetSiguienteByIdPuesto(connstring, idPues);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    
}
