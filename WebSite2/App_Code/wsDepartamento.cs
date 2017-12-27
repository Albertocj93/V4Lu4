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
/// Descripción breve de wsDepartamento
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsDepartamento : System.Web.Services.WebService {

    public wsDepartamento () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }


    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    [WebMethod]
    public void GetByIdEmpresa(int idEmpresa)
    {
        DepartamentoBE obj = new DepartamentoBE();
        obj.IdEmpresa = idEmpresa;

        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        List<DepartamentoBE> oLista = new List<DepartamentoBE>();

        oLista = DepartamentoBL.GetByIdEmpresa(connstring,obj);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }


   

    [WebMethod(EnableSession = true)]
    public bool Save(int id, int idEmpresa, string descripcion, string sigla)
    {
        bool resultado = false;

        DepartamentoBE obj = new DepartamentoBE();
        obj.Id = id;
        obj.IdEmpresa = idEmpresa;
        obj.Descripcion = descripcion;
        obj.Sigla = sigla;

        DepartamentoBL bl = new DepartamentoBL();

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

        DepartamentoBE obj = new DepartamentoBE();
        obj.Id = id;

        DepartamentoBL bl = new DepartamentoBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Delete(connstring, obj);

        return resultado;
    }

    [WebMethod]
    public void GetAll()
    {
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        List<DepartamentoBE> oLista = new List<DepartamentoBE>();

        oLista = DepartamentoBL.GetAll(connstring);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetIdByDescDepEmp(string connstring, string empresa, string departamento)
    {
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        int res = DepartamentoBL.GetIdByDescDepEmp(connstring, empresa, departamento);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);

    }
}
