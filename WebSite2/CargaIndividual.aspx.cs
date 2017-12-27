using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;
using BusinessLogic;
using Common;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;

public partial class ExampleDropZone : System.Web.UI.Page
{
    private string connstring = ConfigurationManager.ConnectionStrings["DB_Valua"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        string urltmp = HttpContext.Current.Request.Url.AbsoluteUri;
        try
        {
            idCarga.Value = Request.QueryString["IdCarga"].ToString();
        }
        catch(Exception ex)
        {}

        foreach (string s in Request.Files)
        {
            HttpPostedFile file = Request.Files[s];

            AdjuntoBE AdjuntoBE= new AdjuntoBE();
            AdjuntoBL AdjuntoBL = new AdjuntoBL();

            AdjuntoBE.IdCarga = idCarga.Value.ToString();
            AdjuntoBE.NombreAdjunto = file.FileName;
            AdjuntoBE.AdjuntoFileType = file.ContentType;
            AdjuntoBE.AdjuntoFileSize = file.ContentLength;
            AdjuntoBE.UsuarioCreacion = User.Identity.Name;

            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                AdjuntoBE.AdjuntoFisico = binaryReader.ReadBytes(file.ContentLength);
            }

            AdjuntoBL.AdjuntoTemporalInsert(connstring,AdjuntoBE);
        }

        if(!Page.IsPostBack)
        {
            
        }

    }

    


  
}