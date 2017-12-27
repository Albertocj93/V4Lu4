using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessEntities;

public partial class DownloadAdjunto : System.Web.UI.Page
    //:  IHttpHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["IdAdjunto"]))
        {
            int IdAdjunto = Convert.ToInt32(Request.QueryString["IdAdjunto"].ToString());

            wsAdjunto BLAdjunto = new wsAdjunto();
            AdjuntoBE oAdjuntoBE = BLAdjunto.GetByIdAdjunto(IdAdjunto);

            string name = oAdjuntoBE.NombreAdjunto;
            string contentType = oAdjuntoBE.AdjuntoFileType;
            byte[] data = oAdjuntoBE.AdjuntoFisico;

            Response.Clear();

            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", name));
            Response.ContentType = "application/octet-stream";

            Response.BinaryWrite(data);
            Response.End();
        }
    }
}