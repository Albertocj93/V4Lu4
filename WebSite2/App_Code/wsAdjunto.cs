using BusinessEntities;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Descripción breve de wsAdjunto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class wsAdjunto : System.Web.Services.WebService {

    public wsAdjunto () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    [WebMethod]
    public string HelloWorld() {
        return "Hola a todos";
    }
    [WebMethod]
    public void DeleteByIdCargaFilename(string FileName, string IdCarga)
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        AdjuntoBE.IdCarga = IdCarga;
        AdjuntoBE.NombreAdjunto = FileName;

        AdjuntoBL.DeleteByIdCargaFilename(AdjuntoBE);
    }
    
    //public AdjuntoBE GetByIdAdjunto(string IdAdjunto)
    //{
    //    AdjuntoBE AdjuntoBE = new AdjuntoBE();
    //    AdjuntoBL AdjuntoBL = new AdjuntoBL();

    //    AdjuntoBE = AdjuntoBL.GetByIdAdjunto(IdAdjunto);

    //    return AdjuntoBE;
    //}
    [WebMethod]
    public void AdjuntoTemporalDeleteByIdCargaFilename(string FileName, string IdCarga)
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        AdjuntoBE.IdCarga = IdCarga;
        AdjuntoBE.NombreAdjunto = FileName;
        AdjuntoBE.UsuarioModificacion = User.Identity.Name;

        AdjuntoBL.AdjuntoTemporalDeleteByIdCargaFilename(connstring, AdjuntoBE);
    }
    [WebMethod]
    public void AdjuntoTemporalDeleteByIdCarga(string IdCarga)
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        AdjuntoBE.IdCarga = IdCarga;
        AdjuntoBE.UsuarioModificacion = User.Identity.Name;

        AdjuntoBL.AdjuntoTemporalDeleteByIdCarga(connstring, AdjuntoBE);
    }

    public AdjuntoBE GetByIdAdjunto(int IdAdjunto)
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        AdjuntoBE = AdjuntoBL.GetByIdAdjunto(connstring,IdAdjunto);

        return AdjuntoBE;
    }
    [WebMethod]
    public void GetById(int IdAdjunto)
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        AdjuntoBE = AdjuntoBL.GetByIdAdjunto(connstring, IdAdjunto);

        AdjuntoBE.AdjuntoFisico = null;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(AdjuntoBE);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetLastByUser()
    {
        AdjuntoBE AdjuntoBE = new AdjuntoBE();
        AdjuntoBL AdjuntoBL = new AdjuntoBL();

        string CuentaUsuario = User.Identity.Name;

        AdjuntoBE = AdjuntoBL.GetLastByUser(connstring, CuentaUsuario);

        AdjuntoBE.AdjuntoFisico = null;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(AdjuntoBE);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
}
