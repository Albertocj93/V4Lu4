using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using BusinessEntities;
using Common;
using System.Configuration;
using System.Data;

public partial class ExportarMapaDePuestos : System.Web.UI.Page
{
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string CuentaUsuario = User.Identity.Name;
        PuestoBL PuestoBL = new PuestoBL();

        string[,] items = PuestoBL.GetMatrizMapaPuestos(connstring, CuentaUsuario);
        string nombreReport = "MapaPuestos_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;

        ExportExcel.Matriz2Excel(items,"Mapa de Puestos" ,nombreReport);
    }

}