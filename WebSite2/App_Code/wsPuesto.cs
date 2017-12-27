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
using System.Text;
/// <summary>
/// Descripción breve de wsPuesto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
 [System.Web.Script.Services.ScriptService]
public class wsPuesto : System.Web.Services.WebService {

    public wsPuesto () {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    [WebMethod]
    public void GetByUser()
    {
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();
        String CuentaUsuario = ObtenerUsuario();

        oLista = PuestoBL.GetByUser(connstring,CuentaUsuario);
        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByUserAdministrador(string IdEmpresa)
    {
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();
        String CuentaUsuario = ObtenerUsuario();

        oLista = PuestoBL.GetByUserAdministrador(connstring, CuentaUsuario, string.IsNullOrEmpty(IdEmpresa) ? Constantes.INT_NULO : Convert.ToInt32(IdEmpresa));
        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByUserAdministradorFiltros(string IdEmpresa, string IdPais, string IdDepartamento, string IdArea, string IdSubArea, 
                                                string TituloPuesto, string NombreOcupante, string CodigoValua)
    {
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();
        String CuentaUsuario = ObtenerUsuario();

        oLista = PuestoBL.GetByUserAdministradorFiltros(connstring, CuentaUsuario, 
                                                string.IsNullOrEmpty(IdEmpresa) ? Constantes.INT_NULO : Convert.ToInt32(IdEmpresa),
                                                string.IsNullOrEmpty(IdPais) ? Constantes.INT_NULO : Convert.ToInt32(IdPais),
                                                string.IsNullOrEmpty(IdDepartamento) ? Constantes.INT_NULO : Convert.ToInt32(IdDepartamento),
                                                string.IsNullOrEmpty(IdArea) ? Constantes.INT_NULO : Convert.ToInt32(IdArea),
                                                string.IsNullOrEmpty(IdSubArea) ? Constantes.INT_NULO : Convert.ToInt32(IdSubArea),
                                                TituloPuesto, NombreOcupante, CodigoValua);
        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetEliminadosByUser()
    {
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();
        String CuentaUsuario = ObtenerUsuario();

        oLista = PuestoBL.GetEliminadosByUser(connstring,CuentaUsuario);
        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void Grabar(string IdPuesto, string Estado, string Empresa, string Pais, string TituloPuesto,
                        string Departamento,string Area,string SubArea,string NombreOcupante,
                        string Grado, string CompetenciaT,string CompetenciaG,string CompetenciaRH,
                        string CompetenciaPTS,string SolucionA,string SolucionD,string SolucionPORC,
                        string SolucionPTS,string ResponsabilidadA,string ResponsabilidadM,string ResponsabilidadI,
                        string ResponsabilidadPTS,string Total,string Perfil,string PuntoMedio,string Magnitud,
                        string Comentario,string CodigoFuncion,string CodigoOcupante,string CodigoValua,string IdAdjunto)
    {
        String CuentaUsuario = ObtenerUsuario();

        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new PuestoBE();
        EmpresaBL EmpresaBL = new EmpresaBL();
        EstadoBL EstadoBL = new EstadoBL();
        PaisBL PaisBL = new PaisBL();
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        AreaBL AreaBL = new AreaBL();
        SubAreaBL SubAreaBL = new SubAreaBL();
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        if (!string.IsNullOrEmpty(IdPuesto))
        {
            PuestoBE.Id = Convert.ToInt32(IdPuesto);
        }
        if (!string.IsNullOrEmpty(IdAdjunto))
        {
            PuestoBE.IdAdjunto = Convert.ToInt32(IdAdjunto);
        }
        PuestoBE.IdEstado = EstadoBL.GetIdByDesc(connstring, Estado);
        PuestoBE.IdEmpresa = EmpresaBL.GetIdByDesc(connstring,Empresa);
        PuestoBE.IdPais = PaisBL.GetIdByDesc(connstring,Pais);
        PuestoBE.TituloPuesto = TituloPuesto;
        PuestoBE.IdDepartamento = DepartamentoBL.GetIdByDescDepEmp(connstring, Empresa, Departamento);
        PuestoBE.IdArea = AreaBL.GetIdByDescAreDepEmp(connstring,Area,Empresa,Departamento);
        PuestoBE.IdSubArea = SubAreaBL.GetIdByDescSArAreDepEmp(connstring, SubArea, Area, Empresa, Departamento);
        PuestoBE.NombreOcupante = NombreOcupante;
        if(!string.IsNullOrEmpty(CompetenciaPTS)|| !string.IsNullOrEmpty(SolucionPTS)|| !string.IsNullOrEmpty(ResponsabilidadPTS))
        {
            PuestoBE.IdCompetenciaT = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaT, ValoresEvaluacion.CompetenciaT).Id;
            PuestoBE.IdCompetenciaG = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaG, ValoresEvaluacion.CompetenciaG).Id;
            PuestoBE.IdCompetenciaRH = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaRH, ValoresEvaluacion.CompetenciaRH).Id;
            PuestoBE.CompetenciaPTS = EvaluacionBL.CalcularCompetenciaPTS(connstring, CompetenciaT, CompetenciaG, CompetenciaRH);
            PuestoBE.IdSolucionA = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, SolucionA, ValoresEvaluacion.SolucionA).Id;
            PuestoBE.IdSolucionD = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, SolucionD, ValoresEvaluacion.SolucionD).Id;
            PuestoBE.SolucionPorc = EvaluacionBL.CalcularSolucionPORC(connstring, SolucionA, SolucionD);
            PuestoBE.SolucionPTS = EvaluacionBL.CalcularSolucionPTS(connstring, PuestoBE.SolucionPorc, PuestoBE.CompetenciaPTS);
            PuestoBE.IdResponsabilidadA = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadA, ValoresEvaluacion.ResponsabilidadA).Id;
            PuestoBE.IdResponsabilidadM = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadM, ValoresEvaluacion.ResponsabilidadM).Id;
            PuestoBE.IdResponsabilidadI = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadI, ValoresEvaluacion.ResponsabilidadI).Id;
            PuestoBE.ResponsabilidadPTS = EvaluacionBL.CalcularResponsabilidadPTS(connstring, ResponsabilidadA, ResponsabilidadM, ResponsabilidadI);
            PuestoBE.Total = EvaluacionBL.CalcularTotal(connstring, PuestoBE.CompetenciaPTS, PuestoBE.SolucionPTS, PuestoBE.ResponsabilidadPTS);
            PuestoBE.Perfil = EvaluacionBL.CalcularPerfil(connstring, PuestoBE.SolucionPTS, PuestoBE.ResponsabilidadPTS, PuestoBE.Total);
            PuestoBE.PuntoMedio = EvaluacionBL.CalcularPuntoMedio(connstring, PuestoBE.Total);
            PuestoBE.Grado = EvaluacionBL.CalcularGrado(connstring, PuestoBE.Total);
        }
        else
        {
            PuestoBE.IdCompetenciaT = Constantes.INT_NULO;
            PuestoBE.IdCompetenciaG = Constantes.INT_NULO;
            PuestoBE.IdCompetenciaRH = Constantes.INT_NULO;

            PuestoBE.IdSolucionA = Constantes.INT_NULO;
            PuestoBE.IdSolucionD = Constantes.INT_NULO;

            PuestoBE.IdResponsabilidadA = Constantes.INT_NULO;
            PuestoBE.IdResponsabilidadM = Constantes.INT_NULO;
            PuestoBE.IdResponsabilidadI = Constantes.INT_NULO;

            PuestoBE.Grado = Grado;
        }
        PuestoBE.Magnitud = Magnitud;
        PuestoBE.Comentario = Comentario;
        PuestoBE.UsuarioModificacion = CuentaUsuario;
        PuestoBE.FechaModificacion = DateTime.Now;
        PuestoBE.UsuarioCreador = CuentaUsuario;
        PuestoBE.FechaCreacion = DateTime.Now;
        PuestoBE.CodigoFuncion = CodigoFuncion;
        PuestoBE.CodigoOcupante = CodigoOcupante;
        PuestoBE.CodigoValua= GenerarCodigoVALUA(IdPuesto,PuestoBE.IdPais,PuestoBE.IdEmpresa,PuestoBE.IdDepartamento);

        if (string.IsNullOrEmpty(IdPuesto))
        {
            PuestoBL.Insert(connstring,PuestoBE);
        }
        else
        {
            PuestoBL.Update(connstring, PuestoBE);
        }
    }
    
    [WebMethod]
    public void EnviaraPapelera(int IdPuesto)
    {
        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new PuestoBE();
        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.Id = IdPuesto;
        PuestoBE.UsuarioModificacion = CuentaUsuario;
        PuestoBE.UsuarioElimino = CuentaUsuario;
        PuestoBL.Delete(connstring,PuestoBE);

    }
    [WebMethod]
    public void AgregarAdjuntosAPuestosByIdCarga(string IdCarga)
    {
        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new BusinessEntities.PuestoBE();

        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.UsuarioCreador = CuentaUsuario;

        PuestoBL.AgregarAdjuntosAPuestosByIdCarga(connstring,IdCarga, PuestoBE);
    }
    //[WebMethod]
    //public void GetHistorialById(string IdPuesto)
    //{
    //    PuestoBL PuestoBL = new PuestoBL();
    //    List<PuestoBE> oLista = new List<PuestoBE>();

    //    oLista = PuestoBL.GetHistorialById(IdPuesto);
    //    // Return JSON data
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    string strJSON = js.Serialize(oLista);

    //    Context.Response.Clear();
    //    Context.Response.ContentType = "application/json";
    //    Context.Response.Flush();
    //    Context.Response.Write(strJSON);
    //}

    public string ObtenerUsuario()
    {
        string CuentaUsuario = User.Identity.Name;
        //if (CuentaUsuario.Contains(ConfigurationManager.AppSettings["GrupoAdministracion"].ToString()))
        //{
        //    CuentaUsuario = ConfigurationManager.AppSettings["UsuarioAdministracion"].ToString();
        //}

        return CuentaUsuario;
    }
    [WebMethod]
    public void SetInactive(int IdPuesto)
    {
        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new PuestoBE();
        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.Id = IdPuesto;
        PuestoBE.UsuarioModificacion = CuentaUsuario;
        PuestoBL.SetInactive(connstring, PuestoBE);
    }
    public string GenerarCodigoVALUA(string IdPuesto, int IdPais, int IdEmpresa, int IdDepartamento)
    {
        string respuesta = "";
        string cadena ="";
        PaisBL PaisBL = new PaisBL();
        EmpresaBL EmpresaBL = new EmpresaBL();
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        PuestoBL PuestoBL = new PuestoBL();

        PaisBE pPais = PaisBL.GetByIdPais(connstring,IdPais);
        EmpresaBE pEmpresa = EmpresaBL.GetByIdEmpresa(connstring,IdEmpresa);
        DepartamentoBE pDepartamento = DepartamentoBL.GetByIdDepartamento(connstring, IdDepartamento);

        if (string.IsNullOrEmpty(IdPuesto))
        {
            
            cadena = pPais.Sigla + "-" + pEmpresa.Sigla + "-" + pDepartamento.Sigla+"-";
            respuesta = cadena + PuestoBL.GenerarCorrelativo(connstring,cadena);
            
        }
        else
        {
            PuestoBE pPuesto = PuestoBL.GetById(connstring, Convert.ToInt32(IdPuesto));
            if (string.IsNullOrEmpty(pPuesto.CodigoValua))
            {
                cadena = pPais.Sigla + "_" + pEmpresa.Sigla + "_" + pDepartamento.Sigla + "_";
                respuesta = cadena + PuestoBL.GenerarCorrelativo(connstring, cadena);
            }
            else
            {
                if (pPuesto.IdPais == IdPais && pPuesto.IdEmpresa == IdEmpresa && pPuesto.IdDepartamento == IdDepartamento)
                {
                    respuesta = pPuesto.CodigoValua;
                }
                else
                {
                    cadena = pPais.Sigla + "_" + pEmpresa.Sigla + "_" + pDepartamento.Sigla + "_";
                    respuesta = cadena + PuestoBL.GenerarCorrelativo(connstring, cadena);
                }
            }
            
        }
        

        return respuesta;
        
    }
    [WebMethod]
    public void GetHistoriaById(string IdPuesto)
    {
        PuestoBE PuestoBE = new BusinessEntities.PuestoBE();
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();
        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.Id = Convert.ToInt32(IdPuesto);
        oLista = PuestoBL.GetHistoriaById(connstring, PuestoBE);
        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(oLista);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void Recuperar(string IdPuesto, string Estado, string Empresa, string Pais, string TituloPuesto,
                        string Departamento, string Area, string SubArea, string NombreOcupante,
                        string Grado, string CompetenciaT, string CompetenciaG, string CompetenciaRH,
                        string CompetenciaPTS, string SolucionA, string SolucionD, string SolucionPORC,
                        string SolucionPTS, string ResponsabilidadA, string ResponsabilidadM, string ResponsabilidadI,
                        string ResponsabilidadPTS, string Total, string Perfil, string PuntoMedio, string Magnitud,
                        string Comentario, string CodigoFuncion, string CodigoOcupante, string CodigoValua, string IdAdjunto)
    {
        String CuentaUsuario = ObtenerUsuario();

        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new PuestoBE();
        EmpresaBL EmpresaBL = new EmpresaBL();
        EstadoBL EstadoBL = new EstadoBL();
        PaisBL PaisBL = new PaisBL();
        DepartamentoBL DepartamentoBL = new DepartamentoBL();
        AreaBL AreaBL = new AreaBL();
        SubAreaBL SubAreaBL = new SubAreaBL();
        EvaluacionBL EvaluacionBL = new EvaluacionBL();
        if (!string.IsNullOrEmpty(IdPuesto))
        {
            PuestoBE.Id = Convert.ToInt32(IdPuesto);
        }
        if (!string.IsNullOrEmpty(IdAdjunto))
        {
            PuestoBE.IdAdjunto = Convert.ToInt32(IdAdjunto);
        }
        PuestoBE.IdEstado = EstadoBL.GetIdByDesc(connstring,Estados.EnElaboracion);
        PuestoBE.IdEmpresa = EmpresaBL.GetIdByDesc(connstring,Empresa);
        PuestoBE.IdPais = PaisBL.GetIdByDesc(connstring, Pais);
        PuestoBE.TituloPuesto = TituloPuesto;
        PuestoBE.IdDepartamento = DepartamentoBL.GetIdByDescDepEmp(connstring,Empresa, Departamento);
        PuestoBE.IdArea = AreaBL.GetIdByDescAreDepEmp(connstring, Area, Empresa, Departamento);
        PuestoBE.IdSubArea = SubAreaBL.GetIdByDescSArAreDepEmp(connstring, SubArea, Area, Empresa, Departamento);
        PuestoBE.NombreOcupante = NombreOcupante;
        if (!string.IsNullOrEmpty(CompetenciaPTS) || !string.IsNullOrEmpty(SolucionPTS) || !string.IsNullOrEmpty(ResponsabilidadPTS))
        {
            PuestoBE.IdCompetenciaT = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaT, ValoresEvaluacion.CompetenciaT).Id;
            PuestoBE.IdCompetenciaG = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaG, ValoresEvaluacion.CompetenciaG).Id;
            PuestoBE.IdCompetenciaRH = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, CompetenciaRH, ValoresEvaluacion.CompetenciaRH).Id;
            PuestoBE.CompetenciaPTS = EvaluacionBL.CalcularCompetenciaPTS(connstring, CompetenciaT, CompetenciaG, CompetenciaRH);
            PuestoBE.IdSolucionA = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, SolucionA, ValoresEvaluacion.SolucionA).Id;
            PuestoBE.IdSolucionD = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, SolucionD, ValoresEvaluacion.SolucionD).Id;
            PuestoBE.SolucionPorc = EvaluacionBL.CalcularSolucionPORC(connstring, SolucionA, SolucionD);
            PuestoBE.SolucionPTS = EvaluacionBL.CalcularSolucionPTS(connstring, PuestoBE.SolucionPorc, PuestoBE.CompetenciaPTS);
            PuestoBE.IdResponsabilidadA = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadA, ValoresEvaluacion.ResponsabilidadA).Id;
            PuestoBE.IdResponsabilidadM = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadM, ValoresEvaluacion.ResponsabilidadM).Id;
            PuestoBE.IdResponsabilidadI = EvaluacionBL.ValoresEvaluacionGetIdByDesc(connstring, ResponsabilidadI, ValoresEvaluacion.ResponsabilidadI).Id;
            PuestoBE.ResponsabilidadPTS = EvaluacionBL.CalcularResponsabilidadPTS(connstring, ResponsabilidadA, ResponsabilidadM, ResponsabilidadI);
            PuestoBE.Total = EvaluacionBL.CalcularTotal(connstring, PuestoBE.CompetenciaPTS, PuestoBE.SolucionPTS, PuestoBE.ResponsabilidadPTS);
            PuestoBE.Perfil = EvaluacionBL.CalcularPerfil(connstring, PuestoBE.SolucionPTS, PuestoBE.ResponsabilidadPTS, PuestoBE.Total);
            PuestoBE.PuntoMedio = EvaluacionBL.CalcularPuntoMedio(connstring, PuestoBE.Total);
            PuestoBE.Grado = EvaluacionBL.CalcularGrado(connstring, PuestoBE.Total);
        }
        else
        {
            PuestoBE.IdCompetenciaT = Constantes.INT_NULO;
            PuestoBE.IdCompetenciaG = Constantes.INT_NULO;
            PuestoBE.IdCompetenciaRH = Constantes.INT_NULO;

            PuestoBE.IdSolucionA = Constantes.INT_NULO;
            PuestoBE.IdSolucionD = Constantes.INT_NULO;

            PuestoBE.IdResponsabilidadA = Constantes.INT_NULO;
            PuestoBE.IdResponsabilidadM = Constantes.INT_NULO;
            PuestoBE.IdResponsabilidadI = Constantes.INT_NULO;

            PuestoBE.Grado = Grado;
        }
        PuestoBE.Magnitud = Magnitud;
        PuestoBE.Comentario = Comentario;
        PuestoBE.UsuarioModificacion = CuentaUsuario;
        PuestoBE.FechaModificacion = DateTime.Now;
        PuestoBE.UsuarioCreador = CuentaUsuario;
        PuestoBE.FechaCreacion = DateTime.Now;
        PuestoBE.CodigoFuncion = CodigoFuncion;
        PuestoBE.CodigoOcupante = CodigoOcupante;
        PuestoBE.CodigoValua = GenerarCodigoVALUA(IdPuesto, PuestoBE.IdPais, PuestoBE.IdEmpresa, PuestoBE.IdDepartamento);


        PuestoBL.Update(connstring, PuestoBE);

    }
    [WebMethod]
    public string AgregarAdjuntosAPuestosByIdCargaIdPuesto(string IdCarga, string IdPuesto)
    {
        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new BusinessEntities.PuestoBE();

        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.UsuarioCreador = CuentaUsuario;
        PuestoBE.Id = Convert.ToInt32(IdPuesto);

        PuestoBL.AgregarAdjuntosAPuestosByIdCargaIdPuesto(connstring, IdCarga, PuestoBE);
        return "succes";
    }
    [WebMethod]
    public void DeleteAdjuntoByIdPuesto(string IdPuesto)
    {
        PuestoBL PuestoBL = new PuestoBL();
        PuestoBE PuestoBE = new BusinessEntities.PuestoBE();

        String CuentaUsuario = ObtenerUsuario();

        PuestoBE.UsuarioModificacion = CuentaUsuario;
        PuestoBE.Id = Convert.ToInt32(IdPuesto);

        PuestoBL.DeleteAdjuntoByIdPuesto(connstring, PuestoBE);
    }
    [WebMethod]
    public void GetCountByIdPais(int IdPais)
    {
        PuestoBL PuestoBL = new PuestoBL();

        int res = PuestoBL.GetCountByIdPais(connstring, IdPais);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetCountByIdEmpresa(int IdEmpresa)
    {
        PuestoBL PuestoBL = new PuestoBL();

        int res = PuestoBL.GetCountByIdEmpresa(connstring, IdEmpresa);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByIdDepartamento(int IdDepartamento)
    {
        PuestoBL PuestoBL = new PuestoBL();

        int res = PuestoBL.GetByIdDepartamento(connstring, IdDepartamento);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByIdArea(int IdArea)
    {
        PuestoBL PuestoBL = new PuestoBL();

        int res = PuestoBL.GetByIdArea(connstring, IdArea);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
    [WebMethod]
    public void GetByIdSubArea(int IdSubArea)
    {
        PuestoBL PuestoBL = new PuestoBL();

        int res = PuestoBL.GetByIdArea(connstring, IdSubArea);

        // Return JSON data
        JavaScriptSerializer js = new JavaScriptSerializer();
        string strJSON = js.Serialize(res);

        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write(strJSON);
    }
}
