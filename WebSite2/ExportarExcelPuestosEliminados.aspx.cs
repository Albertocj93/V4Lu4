using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using Common;
using GR.Scriptor.Framework;
using System.Configuration;


public partial class ExportarExcelPuestosEliminados : System.Web.UI.Page
{
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        PuestoBL PuestoBL = new PuestoBL();
        List<PuestoBE> oLista = new List<PuestoBE>();

        String CuentaUsuario = User.Identity.Name;

        oLista = PuestoBL.ExportarEliminadosByUser(connstring,CuentaUsuario);

        string nombreReport = "PuestosEliminados_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
        List<ReportColumnHeader> columnas = new List<ReportColumnHeader>();

        columnas.Add(new ReportColumnHeader() { BindField = "Estado", HeaderName = "Estado" });
        columnas.Add(new ReportColumnHeader() { BindField = "NombreAdjunto", HeaderName = "Adjunto" });
        columnas.Add(new ReportColumnHeader() { BindField = "Empresa", HeaderName = "Empresa" });
        columnas.Add(new ReportColumnHeader() { BindField = "Pais", HeaderName = "País" });
        columnas.Add(new ReportColumnHeader() { BindField = "TituloPuesto", HeaderName = "Título del puesto" });
        columnas.Add(new ReportColumnHeader() { BindField = "Departamento", HeaderName = "Departamento" });
        columnas.Add(new ReportColumnHeader() { BindField = "Area", HeaderName = "Área" });
        columnas.Add(new ReportColumnHeader() { BindField = "SubArea", HeaderName = "Sub Área" });
        columnas.Add(new ReportColumnHeader() { BindField = "NombreOcupante", HeaderName = "Nombre del ocupante" });
        columnas.Add(new ReportColumnHeader() { BindField = "Grado", HeaderName = "Grado" });
        columnas.Add(new ReportColumnHeader() { BindField = "CompetenciaT", HeaderName = "Competencia - T" });
        columnas.Add(new ReportColumnHeader() { BindField = "CompetenciaG", HeaderName = "Competencia - G" });
        columnas.Add(new ReportColumnHeader() { BindField = "CompetenciaRH", HeaderName = "Competencia - RH" });
        columnas.Add(new ReportColumnHeader() { BindField = "CompetenciaPTS", HeaderName = "Competencia - PTS" });
        columnas.Add(new ReportColumnHeader() { BindField = "SolucionA", HeaderName = "Solución Problemas - A" });
        columnas.Add(new ReportColumnHeader() { BindField = "SolucionD", HeaderName = "Solución Problemas - D" });
        columnas.Add(new ReportColumnHeader() { BindField = "SolucionPorc", HeaderName = "Solución Problemas - %" });
        columnas.Add(new ReportColumnHeader() { BindField = "SolucionPTS", HeaderName = "Solución Problemas - PTS" });
        columnas.Add(new ReportColumnHeader() { BindField = "ResponsabilidadA", HeaderName = "Responsabilidad por RDOS - A" });
        columnas.Add(new ReportColumnHeader() { BindField = "ResponsabilidadM", HeaderName = "Responsabilidad por RDOS - M" });
        columnas.Add(new ReportColumnHeader() { BindField = "ResponsabilidadI", HeaderName = "Responsabilidad por RDOS - I" });
        columnas.Add(new ReportColumnHeader() { BindField = "ResponsabilidadPTS", HeaderName = "Responsabilidad por RDOS - PTS" });
        columnas.Add(new ReportColumnHeader() { BindField = "Total", HeaderName = "Total" });
        columnas.Add(new ReportColumnHeader() { BindField = "Perfil", HeaderName = "Perfil" });
        columnas.Add(new ReportColumnHeader() { BindField = "PuntoMedio", HeaderName = "Punto Medio" });
        columnas.Add(new ReportColumnHeader() { BindField = "Magnitud", HeaderName = "Magnitud" });
        columnas.Add(new ReportColumnHeader() { BindField = "Comentario", HeaderName = "Comentario" });
        columnas.Add(new ReportColumnHeader() { BindField = "UsuarioCreador", HeaderName = "Creador" });
        columnas.Add(new ReportColumnHeader() { BindField = "FechaCreacion", HeaderName = "Fecha Creación" });
        columnas.Add(new ReportColumnHeader() { BindField = "FechaEliminacion", HeaderName = "Fecha de eliminación" });
        columnas.Add(new ReportColumnHeader() { BindField = "UsuarioElimino", HeaderName = "Usuario que eliminó" });
        columnas.Add(new ReportColumnHeader() { BindField = "CodigoFuncion", HeaderName = "Código función" });
        columnas.Add(new ReportColumnHeader() { BindField = "CodigoOcupante", HeaderName = "Código ocupante" });
        columnas.Add(new ReportColumnHeader() { BindField = "CodigoValua", HeaderName = "Código VALUA" });

        ExportExcel.List2Excel2(Context.Response, oLista, "Puestos Eliminados", nombreReport, columnas);
        Context.Response.Clear();
        Context.Response.ContentType = "application/json";
        Context.Response.Flush();
        Context.Response.Write("");
    }
}