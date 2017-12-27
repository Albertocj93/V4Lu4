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
/// Descripción breve de wsSubArea
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsSubArea : System.Web.Services.WebService {
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    public wsSubArea () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetAll()
    {
        SubAreaBL SubAreaBL = new SubAreaBL();
        List<SubAreaBE> oLista = new List<SubAreaBE>();

        oLista = SubAreaBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }

    [WebMethod]
    public void GetByIdArea(int IdArea)
    {
        SubAreaBL SubAreaBL = new SubAreaBL();
        List<SubAreaBE> oLista = new List<SubAreaBE>();

        oLista = SubAreaBL.GetByIdArea(IdArea);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }


    [WebMethod(EnableSession = true)]
    public bool Save(int id, int idArea, string descripcion, string sigla)
    {
        bool resultado = false;

        SubAreaBE obj = new SubAreaBE();
        obj.Id = id;
        obj.IdArea = idArea;
        obj.Descripcion = descripcion;
        obj.Sigla = sigla;

        SubAreaBL bl = new SubAreaBL();

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

        SubAreaBE obj = new SubAreaBE();
        obj.Id = id;

        SubAreaBL bl = new SubAreaBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Delete(connstring, obj);

        return resultado;
    }
    [WebMethod]
    public void GetIdByDescSArAreDepEmp(string connstring ,string SubArea, string DescArea, string DescEmpresa, string DescDepartamento)
    {
        SubAreaBL SubAreaBL = new SubAreaBL();
        int res = SubAreaBL.GetIdByDescSArAreDepEmp(connstring,SubArea, DescArea, DescDepartamento, DescEmpresa);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
}
