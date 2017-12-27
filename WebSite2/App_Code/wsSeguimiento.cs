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
public class wsSeguimiento : System.Web.Services.WebService {

    public wsSeguimiento()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
  


    [WebMethod(EnableSession = true)]
    public bool visita()
    {
        bool resultado = false;

        VisitaBE obj = new VisitaBE();
        

        VisitaBL bl = new VisitaBL();

        //FALTA: utilizar usuario 
        obj.UsuarioCreacion = User.Identity.Name;
        resultado = bl.Insert(connstring, obj);
        

        return resultado;
    }



}
