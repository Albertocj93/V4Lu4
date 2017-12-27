using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GR.Scriptor.Framework
{
    public class ExcelManager
    {
        public void List2Excel<T>(HttpResponseBase responsePage, IList<T> lista, String Titulo, String nombre, List<ReportColumnHeader> columnsNames, DataTable dt = null, Boolean ordenarColumnas=false)
        {
            var filename = nombre + ".xls";

            DataTable DataTablelista;
            if (dt != null)
            {
                DataTablelista = dt;
            }
            else
            {
                if(ordenarColumnas)
                    DataTablelista = CollectionHelper.ConvertTo(lista,columnsNames);
                else
                    DataTablelista = CollectionHelper.ConvertTo(lista);
            }

            List<DataColumn> lstIndexremoves = new List<DataColumn>();
            for (int i = 0; i < DataTablelista.Columns.Count; i++)
            {
                DataColumn col = DataTablelista.Columns[i];
                ReportColumnHeader mcolumn = columnsNames.Find(x => x.BindField == col.ColumnName);

                if (mcolumn == null)
                {
                    lstIndexremoves.Add(col);
                }
                else
                {
                    DataTablelista.Columns[i].ColumnName = mcolumn.HeaderName;
                }
            }

            for (int i = 0; i < lstIndexremoves.Count; i++)
            {
                DataTablelista.Columns.Remove(lstIndexremoves[i]);
            }


            var dgGrid = new DataGrid();
            dgGrid.GridLines = GridLines.Both;
            dgGrid.DataSource = DataTablelista;
            dgGrid.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
            dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.White;
            dgGrid.DataBind();

            var sb = new StringBuilder();
            var Page = new Page();

            var SW = new StringWriter(sb);
            var htw = new HtmlTextWriter(SW);

            var inicio = "<div> " +
                             "<table border=0 cellpadding=0 cellspacing=0>" +
                                    "<tr>" +
                                        "<td colspan=10 align=center><h1>Consulta de " + Titulo + "</h1></td>" +
                                    "</tr>" +
                                    "<tr>" +
                                        "<td colspan=1 align=left style='font-weight:bold'>Fecha de generación:</td>" +
                                        "<td colspan=2 align=left>" + DateTime.Now.ToLongDateString() + "</td>" +
                                    "</tr>" +
                                "</table>" +
                                "</div>";
            htw.Write(inicio);

            var form = new HtmlForm();

            responsePage.ContentType = "application/vnd.ms-excel";
            responsePage.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            responsePage.Charset = string.Empty;
            responsePage.ContentEncoding = Encoding.Default;
            dgGrid.EnableViewState = false;
            Page.EnableEventValidation = false;
            Page.DesignerInitialize();
            Page.Controls.Add(form);
            form.Controls.Add(dgGrid);
            Page.RenderControl(htw);
            responsePage.Clear();
            responsePage.Buffer = true;
            responsePage.Write(sb.ToString());
            responsePage.End();
        }
    }

    public class ReportColumnHeader
    {
        public string BindField { get; set; }
        public string HeaderName { get; set; }
    }
}
