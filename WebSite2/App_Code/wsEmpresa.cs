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
using System.Security.Principal;

/// <summary>
/// Descripción breve de wsEmpresa
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsEmpresa : System.Web.Services.WebService {

    public wsEmpresa () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
   
    [WebMethod]
    public void GetByUser()
    {
        EmpresaBL EmpresaBL = new EmpresaBL();
        List<EmpresaBE> oLista = new List<EmpresaBE>();
        String CuentaUsuario = User.Identity.Name;

        oLista = EmpresaBL.GetByUser(CuentaUsuario);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }

    [WebMethod(EnableSession = true)]
    public bool Save(int id, string descripcion, string sigla)
    {
        bool resultado = false;

        EmpresaBE obj = new EmpresaBE();
        obj.Id = id;
        obj.Descripcion = descripcion;
        obj.Sigla = sigla;

        EmpresaBL bl = new EmpresaBL();

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

        EmpresaBE obj = new EmpresaBE();
        obj.Id = id;

        EmpresaBL bl = new EmpresaBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Delete(connstring, obj);

        return resultado;
    }

    [WebMethod]
    public void GetAll()
    {
        EmpresaBL EmpresaBL = new EmpresaBL();
        List<EmpresaBE> oLista = new List<EmpresaBE>();

        oLista = EmpresaBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByIdEmpresa(int IdEmpresa)
    {
        EmpresaBL EmpresaBL = new EmpresaBL();
        EmpresaBE oempresa = EmpresaBL.GetByIdEmpresa(connstring,IdEmpresa);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oempresa);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod(EnableSession = true)]
    public void GetIdByDesc(string Descripcion)
    {
        EmpresaBL EmpresaBL = new EmpresaBL();
        int res = EmpresaBL.GetIdByDesc(connstring, Descripcion);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    
}
