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
/// Descripción breve de wsPais
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsPais : System.Web.Services.WebService {

    public wsPais () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    [WebMethod]
    public void GetAll()
    {
        PaisBL PaisBL = new PaisBL();
        List<PaisBE> oLista = new List<PaisBE>();

        oLista = PaisBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod(EnableSession = true)]
    public void GetIdByDesc(string Descripcion)
    {
        PaisBL PaisBL = new PaisBL();
        int res = PaisBL.GetIdByDesc(connstring, Descripcion);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod(EnableSession = true)]
    public bool Save(int id, string descripcion, string sigla)
    {
        bool resultado = false;

        PaisBE obj = new PaisBE();
        obj.Id = id;
        obj.Descripcion = descripcion;
        obj.Sigla = sigla;

        PaisBL bl = new PaisBL();

        if (obj.Id == 0)
        {
            //FALTA: utilizar usuario 
            obj.UsuarioCreacion = User.Identity.Name;
            resultado = bl.Insert(connstring, obj);
        }
        else
        {
            //FALTA: utilizar usuario 
            obj.UsuarioModificacion = User.Identity.Name;
            resultado = bl.Update(connstring, obj);
        }

        return resultado;
    }

    [WebMethod(EnableSession = true)]
    public bool Delete(int id)
    {
        bool resultado = false;

        PaisBE obj = new PaisBE();
        obj.Id = id;

        PaisBL bl = new PaisBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Delete(connstring, obj);

        return resultado;
    }


}
