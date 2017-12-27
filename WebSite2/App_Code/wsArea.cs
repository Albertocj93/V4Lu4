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
/// Descripción breve de wsArea
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsArea : System.Web.Services.WebService {

    public wsArea () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    [WebMethod]
    public void GetAll()
    {
        AreaBL AreaBL = new AreaBL();
        List<AreaBE> oLista = new List<AreaBE>();

        oLista = AreaBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }

    [WebMethod]
    public void GetByIdDepartamento( int IdDepartamento)
    {
        AreaBL AreaBL = new AreaBL();
        List<AreaBE> oLista = new List<AreaBE>();

        oLista = AreaBL.GetByIdDepartamento(IdDepartamento);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }


    [WebMethod(EnableSession = true)]
    public bool Save(int id, int idDepartamento, string descripcion, string sigla)
    {
        bool resultado = false;

        AreaBE obj = new AreaBE();
        obj.Id = id;
        obj.IdDepartamento = idDepartamento;
        obj.Descripcion = descripcion;
        obj.Sigla = sigla;

        AreaBL bl = new AreaBL();

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

        AreaBE obj = new AreaBE();
        obj.Id = id;

        AreaBL bl = new AreaBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Delete(connstring, obj);

        return resultado;
    }
    [WebMethod]
    public void GetIdByDescAreDepEmp(string connstring,string DescArea, string DescEmpresa, string DescDepartamento)
    {
        AreaBL AreaBL = new AreaBL();
        int res = AreaBL.GetIdByDescAreDepEmp(connstring, DescArea, DescEmpresa, DescDepartamento);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    
}
