using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using BusinessEntities;
using BusinessLogic;
using System.Configuration;

/// <summary>
/// Descripción breve de wsUsuario
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsUsuario : System.Web.Services.WebService {

    public wsUsuario () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    private string connstringUserData = ConfigurationManager.ConnectionStrings["DB_UserData"].ConnectionString;


    [WebMethod]
    public void GetCuentaRed()
    {
        string CuentaUsuario = User.Identity.Name;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(CuentaUsuario);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByAccount()
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        UsuarioBE usuario = ubl.GetByAccount(connstring, CuentaUsuario);

        UsuarioBE usuarioP = ubl.UsuarioGetPerfil(connstring, CuentaUsuario);

        if(usuario.Id ==0)
        {
            usuario = ubl.GetUserRansaByCuentaRed(connstringUserData, CuentaUsuario);
        }
        usuario.IdPerfil = usuarioP.IdPerfil;
        usuario.Perfil = usuarioP.Perfil;

        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }

    [WebMethod]
    public void GetVistaByAccount()
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        UsuarioBE usuario = ubl.GetVistaByAccount(connstring, CuentaUsuario);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario.IdVista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetPermisosByPuesto(int IdPuesto)
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        UsuarioBE usuario = ubl.GetPermisosByPuesto(connstring, CuentaUsuario,IdPuesto);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetIdPerfilByUsuarioPuesto(int IdPuesto)
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        UsuarioBE usuario = ubl.GetPermisosByPuesto(connstring, CuentaUsuario, IdPuesto);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario.IdPerfil);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }

    [WebMethod]
    public void UsuarioGetPerfil()
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        UsuarioBE usuario = ubl.UsuarioGetPerfil(connstring, CuentaUsuario);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario.IdPerfil);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetAll()
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBL ubl = new UsuarioBL();
        List<UsuarioBE> usuario = ubl.GetAll(connstring);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    
    [WebMethod]
    public void GetPermisosByIdUsuario(string IdUsuario)
    {
        string CuentaUsuario = User.Identity.Name;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(IdUsuario);
        UsuarioBL ubl = new UsuarioBL();

        List<UsuarioBE> usuario = ubl.GetPermisosByIdUsuario(connstring, obj);


        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(usuario);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod(EnableSession = true)]
    public bool Grabar(string Id, string NombreCompleto, string CuentaUsuario, string Email, string IdEmpresa, string Confidencial,string Activo)
    {
        bool resultado = false;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(Id);
        obj.NombreCompleto = NombreCompleto.ToUpper();
        obj.CuentaUsuario = CuentaUsuario.ToUpper();
        obj.Email = Email.ToUpper();
        obj.IdEmpresa = Convert.ToInt32(IdEmpresa);
        obj.Confidencial = Convert.ToInt32(Confidencial);
        obj.Activo = Convert.ToInt32(Activo);

        UsuarioBL bl = new UsuarioBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.Grabar(connstring, obj);

        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public bool EliminarTodosPerfiles(string Id)
    {
        bool resultado = false;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(Id);

        UsuarioBL bl = new UsuarioBL();

        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.EliminarTodosPerfiles(connstring, obj);

        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public bool AsignarAdministrador(string Id)
    {
        bool resultado = false;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(Id);

        UsuarioBL bl = new UsuarioBL();

        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.AsignarAdministrador(connstring, obj);

        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public bool UsuarioPerfilEmpresaInsert(string Id, string IdEmpresa, string IdPerfil)
    {
        bool resultado = false;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(Id);
        obj.IdEmpresa = Convert.ToInt32(IdEmpresa);
        obj.IdPerfil = Convert.ToInt32(IdPerfil);

        UsuarioBL bl = new UsuarioBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.UsuarioPerfilEmpresaInsert(connstring, obj);

        return resultado;
    }
    [WebMethod(EnableSession = true)]
    public bool UsuarioPerfilEmpresaDelete(string Id, string IdEmpresa, string IdPerfil)
    {
        bool resultado = false;

        UsuarioBE obj = new UsuarioBE();
        obj.Id = Convert.ToInt32(Id);
        obj.IdEmpresa = Convert.ToInt32(IdEmpresa);
        obj.IdPerfil = Convert.ToInt32(IdPerfil);

        UsuarioBL bl = new UsuarioBL();

        //FALTA: utilizar usuario 
        obj.UsuarioModificacion = User.Identity.Name;
        resultado = bl.UsuarioPerfilEmpresaDelete(connstring, obj);

        return resultado;
    }

    
}
