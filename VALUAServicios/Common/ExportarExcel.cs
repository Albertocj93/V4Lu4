using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.ComponentModel;

using GR.Scriptor.Framework;


namespace Common
{
    public static class ExportExcel
    {
        public static void List2Excel<T>(HttpResponseBase responsePage, IList<T> lista, String Titulo, String nombre, List<ReportColumnHeader> columnsNames, DataTable dt = null)
        {
            var filename = nombre + ".xls";

            DataTable DataTablelista;
            if (dt != null)
            {
                DataTablelista = dt;
            }
            else
            {
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

        public static void List2Excel2<T>(HttpResponse responsePage, IList<T> lista, String Titulo, String nombre, List<ReportColumnHeader> columnsNames, DataTable dt = null)
        {
            var filename = nombre + ".xls";

            DataTable DataTablelista;
            if (dt != null)
            {
                DataTablelista = dt;
            }
            else
            {
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

            foreach (DataGridItem gvrow in dgGrid.Items)
            {
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    // gvrow.Cells[k].Style.Add("Height", "10");
                    //gvrow.Cells[k].Attributes.Add("class", "cost");
                    //string nombreColumna = dgGrid.Columns[k].HeaderText;
                    //if (nombreColumna == "CodSector")
                    //{
                    //    var s = 2;
                    //}
                    gvrow.Cells[k].Style.Value = "mso-number-format:\\@;";
                }

            }


            var sb = new StringBuilder();
            var Page = new Page();

            var SW = new StringWriter(sb);
            var htw = new HtmlTextWriter(SW);

            var inicio = "<div> " +
                             "<table border=0 cellpadding=0 cellspacing=0>" +
                                    "<tr>" +
                                        "<td colspan=10 align=center><h1>" + Titulo + "</h1></td>" +
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

        public static void List2Excel3<T>(HttpResponse responsePage, IList<T> lista, String Titulo, String nombre, List<ReportColumnHeader> columnsNames, DataTable dt = null)
        {
            var filename = nombre + ".xls";

            DataTable DataTablelista;
            if (dt != null)
            {
                DataTablelista = dt;
            }
            else
            {
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
            //dgGrid.GridLines = GridLines.Both;
            dgGrid.DataSource = DataTablelista;
            //dgGrid.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#333333");
            //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.White;
            dgGrid.DataBind();


            foreach (DataGridItem gvrow in dgGrid.Items)
            {
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    // gvrow.Cells[k].Style.Add("Height", "10");
                    //gvrow.Cells[k].Attributes.Add("class", "cost");
                    //string nombreColumna = dgGrid.Columns[k].HeaderText;
                    //if (nombreColumna == "CodSector")
                    //{
                    //    var s = 2;
                    //}
                    gvrow.Cells[k].Style.Value = "mso-number-format:\\@;";
                }

            }

            dgGrid.EnableViewState = false;

            var sb = new StringBuilder();
            var SW = new StringWriter(sb);
            var htw = new HtmlTextWriter(SW);
            var Page = new Page();
            var form = new HtmlForm();

            Page.EnableEventValidation = false;
            Page.DesignerInitialize();
            Page.Controls.Add(form);
            form.Controls.Add(dgGrid);
            Page.RenderControl(htw);
            responsePage.Clear();
            responsePage.Buffer = true;
            responsePage.ContentType = "application/vnd.ms-excel";
            responsePage.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            responsePage.Charset = "UTF-8";// string.Empty;


            responsePage.Cache.SetCacheability(HttpCacheability.NoCache);
            responsePage.ContentEncoding = System.Text.Encoding.Default;

            int inicioTabla = 0;
            int finTabla = 0;
            string contenido = sb.ToString();
            inicioTabla = contenido.IndexOf("<table");
            finTabla = contenido.IndexOf("</table>");
            contenido = contenido.Substring(inicioTabla, (finTabla + 8) - inicioTabla);
            responsePage.Write(contenido);
           // responsePage.Write(sb.ToString());
            responsePage.End();

        }

        public static void Matriz2Excel(string[,] items,string Titulo,string nombre)
        {
            DataTable table = new DataTable();
            //Get the count of number of columns need to create for the array
            for (int dimension = 0; dimension <= items.GetUpperBound(items.Rank - 1); dimension++)
            {
                //set column name as column+ column number
                table.Columns.Add("Column" + (dimension + 1));
            }

            //Now for each rows in array, get the column value and set it to datatable rows and columns
            for (int element = 0; element <= items.GetUpperBound(items.Rank - 2); element++)
            {
                DataRow row = table.NewRow();
                for (int dimension = 0; dimension <= items.GetUpperBound(items.Rank - 1); dimension++)
                {
                    row["Column" + (dimension + 1)] = items[element, dimension];
                }
                table.Rows.Add(row);
            }


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(
              @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition",
              "attachment;filename=" + nombre + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding =
              System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");

            var inicio = "<div> " +
                            "<table border=0 cellpadding=0 cellspacing=0>" +
                                "<tr>" +
                                    "<td colspan=10 align=center><h1>" + Titulo + "</h1></td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td colspan=1 align=left style='font-weight:bold'>Fecha de generación:</td>" +
                                    "<td colspan=2 align=left>" + DateTime.Now.ToLongDateString() + "</td>" +
                                "</tr>" +
                                "<tr></tr>" +
                            "</table>" +
                        "</div>";

            HttpContext.Current.Response.Write(inicio);

            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                //"borderColor='#000000'"+
              " cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'>");

            
            //am getting my tables's column count
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                //HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                //HttpContext.Current.Response.Write("<B>");
                // HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());   //Nombre de Columnas
                //HttpContext.Current.Response.Write("</B>");
                //HttpContext.Current.Response.Write("</Td>");
            }

            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }
}