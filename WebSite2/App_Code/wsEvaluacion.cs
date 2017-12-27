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
/// Descripción breve de wsEvaluacion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
// [System.Web.Script.Services.ScriptService]
public class wsEvaluacion : System.Web.Services.WebService {

    public wsEvaluacion () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    [WebMethod]
    public void CompetenciaTGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring,ValoresEvaluacion.CompetenciaT);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod] 
    public void CompetenciaGGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.CompetenciaG);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]   
    public void CompetenciaRHGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.CompetenciaRH);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]   
    public void SolucionAGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.SolucionA);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]    
    public void SolucionDGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.SolucionD);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]   
    public void ResponsabilidadAGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.ResponsabilidadA);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]   
    public void ResponsabilidadIGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.ResponsabilidadI);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]    
    public void ResponsabilidadMGetAll()
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        List<ValoresEvaluacionBE> oLista = new List<ValoresEvaluacionBE>();

        oLista = EvaluacionBL.ValoresEvaluacionGetAllByTipo(connstring, ValoresEvaluacion.ResponsabilidadM);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularCompetenciaPTS(string CompetenciaT, string CompetenciaG, string CompetenciaRH)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string CompetenciaPTS = EvaluacionBL.CalcularCompetenciaPTS(connstring,CompetenciaT, CompetenciaG, CompetenciaRH);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(CompetenciaPTS);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularSolucionPORC(string SolucionA,string SolucionD)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string SolucionPORC = EvaluacionBL.CalcularSolucionPORC(connstring, SolucionA, SolucionD);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(SolucionPORC);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularSolucionPTS(string SolucionPORC,string CompetenciaPTS)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string SolucionPTS = EvaluacionBL.CalcularSolucionPTS(connstring, SolucionPORC, CompetenciaPTS);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(SolucionPTS);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularResponsabilidadPTS(string ResponsabilidadA, string ResponsabilidadM, string ResponsabilidadI)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string ResponsabilidadPTS = EvaluacionBL.CalcularResponsabilidadPTS(connstring, ResponsabilidadA, ResponsabilidadM, ResponsabilidadI);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(ResponsabilidadPTS);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularTotal(string CompetenciaPTS,string SolucionPTS,string ResponsabilidadPTS)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string Total = EvaluacionBL.CalcularTotal(connstring, CompetenciaPTS, SolucionPTS, ResponsabilidadPTS);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(Total);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularPerfil(string SolucionPTS, string ResponsabilidadPTS, string Total)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string Perfil = EvaluacionBL.CalcularPerfil(connstring, SolucionPTS, ResponsabilidadPTS, Total);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(Perfil);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularGrado(string Total)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string Grado = EvaluacionBL.CalcularGrado(connstring, Total);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(Grado);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void CalcularPuntoMedio(string Total)
    {
        EvaluacionBL EvaluacionBL = new EvaluacionBL();

        string Grado = EvaluacionBL.CalcularPuntoMedio(connstring, Total);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(Grado);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
}
