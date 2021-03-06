﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargaIndividual.aspx.cs" Inherits="ExampleDropZone" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Estilos/dropzone.css"/>
    
  

    <script src="Script/jquery-1.11.3.js"></script>
    <script src="Script/dropzone.js"></script>
  
    <style>
        .BotonVal {
            border-radius: 0;
            padding: 7px 7px 7px 7px;
            border-width: 1px;
            border-style: solid;
            background-color: transparent;
            font: 400 12px/16px 'Open Sans', 'Helvetica Neue', helvetica, arial, verdana, sans-serif;
            color: #606060;
            max-width: 100%;
            cursor: pointer;
            border-color:rgb(169, 169, 169);
        }
        .BotonVal:hover{
            border-color:#606060;
        }
        .BotonVal:disabled
        {
            background-color:#868686;
            color: #fff;
            cursor: wait;
        }
        span
        {
            font: 400 12px/16px 'Open Sans', 'Helvetica Neue', helvetica, arial, verdana, sans-serif;
        }
    </style>
</head>
<body>
    
    
    <form runat="server" style="max-height: 340px; overflow-y: auto;">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="idCarga" runat="server" value=""/>

        

        <div class="jumbotron" style="height: 100%; ">
            <div  class="dropzone" id="dropzoneForm">
                <div class="fallback">
                    <asp:FileUpload ID="file" runat="server" multiple/>

                    <asp:Button ID="Upload" value="Upload" runat="server" Text="Button" />
                </div>
            </div>
        </div>
        <br /><br />
        <div style ="text-align:center; width:auto;position: absolute;bottom: 55px;left:  50%;margin-left: -107px;">
        <asp:Button ID="AgregarPuesto" runat="server" Text="Aceptar"  CssClass="BotonVal" Width="100px" Style="margin-right:5px;"/>
        <asp:Button ID="Cancelar" runat="server" Text="Cancelar"  CssClass="BotonVal" Width="100px" Style="margin-right:5px;"/>
        </div>
    </form>

</body>
</html>




<script type="text/javascript">

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return results[1] || 0;
        }
    }
    var IdPuesto = $.urlParam('IdPuesto');
    var Carga = $.urlParam('IdCarga');
    var RowIndex = $.urlParam('RowIndex');
    //debugger;

    var cantArch = 0;

    //File Upload response from the server
    Dropzone.options.dropzoneForm = {

        url: "CargaIndividual.aspx?IdCarga=" + Carga + "&IdPuesto=" + IdPuesto + "&RowIndex=" + RowIndex,

        dictDefaultMessage: "Arrastre el archivo a esta zona o hacer click aquí",
        maxFiles: 1,
        createImageThumbnails: true,
        maxThumbnailFilesize: 20,
        acceptedFiles: ".doc,.docx,.ppt,.pptx,.xls,.xlsx,.pdf,.jpeg,.jpg,.png,.bmp,.txt",
        dictResponseError: 'Error al subir el archivo!',
        dictInvalidFileType: 'No puedes subir archivos de este tipo!',
        init: function () {
            this.on("addedfile", function (file) {

                cantArch = 1;

                $("#AgregarPuesto").prop("disabled", true);
                // Create the  remove button
                var removeButton = Dropzone.createElement("<button class='BotonVal' style='margin-top:10px; width:100%;'>Eliminar Archivo</button>");
                // Capture the Dropzone instance as closure.
                var _this = this;

                this.on("maxfilesexceeded", function (file) {

                    _this.removeFile(file);
                });

                this.on("error", function (file) {
                    cantArch = 0;
                });

                // Listen to the click event
                removeButton.addEventListener("click", function (e) {
                    // Make sure the button click doesn't submit the form:

                    e.preventDefault();
                    e.stopPropagation();

                    var fileName = file.name;
                    $.ajax({
                        type: "POST",
                        url: "/WsValua/wsAdjunto.asmx/AdjuntoTemporalDeleteByIdCargaFilename",
                        data: "{FileName:'" + fileName + "',IdCarga:'" + Carga + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {

                        }
                    });
                    cantArch = 0;
                    // Remove the file preview.
                    _this.removeFile(file);

                    // If you want to the delete the file on the server as well,
                    // you can do the AJAX request here.
                });
                // Add the button to the file preview element.
                file.previewElement.appendChild(removeButton);
            });

            this.on("queuecomplete", function () {
                $("#AgregarPuesto").prop("disabled", false);
            });
        }
    };

    $("#AgregarPuesto").click(function () {

        var idCarga = Carga;
        var idAdjunto = 0;
        //debugger;

        if (cantArch == 1) {
            window.parent.Ext.Ajax.request({
                url: '/WsValua/wsPuesto.asmx/AgregarAdjuntosAPuestosByIdCargaIdPuesto',
                async: false,
                timeout: 2500000,
                method: 'POST',
                params: {
                    IdCarga: Carga,
                    IdPuesto: IdPuesto
                },
                success: function (responseObject) {

                    window.parent.Ext.Ajax.request({
                        url: '/WsValua/wsadjunto.asmx/GetLastByUser',
                        async: false,
                        timeout: 2500000,
                        method: 'POST',
                        params: {},
                        success: function (responseObjectAdj) {
                            var obj = window.parent.Ext.decode(responseObjectAdj.responseText);
                            //debugger;
                            window.parent.Ext.getCmp("gridPuestos").enable();
                            window.parent.Ext.getCmp('gridPuestos').getStore().getRange()[RowIndex].data.IdAdjunto = obj.IdAdjunto;
                            window.parent.Ext.getCmp('gridPuestos').getStore().getRange()[RowIndex].data.NombreAdjunto = obj.NombreAdjunto;
                            window.parent.Ext.getCmp('gridPuestos').getSelectionModel().view.refresh();
                            window.parent.Ext.getCmp('gridPuestos').getStore().getRange()[RowIndex].phantom = true;
                        }
                    });

                }
            });
        }
        window.parent.Ext.getCmp("gridPuestos").enable();
        window.parent.Ext.getCmp('gridPuestos').getSelectionModel().view.refresh();
        window.parent.Ext.getCmp("winCargaIndividual").hide();
        window.parent.Ext.getCmp("winCargaIndividual").destroy();
    });

    $("#Cancelar").click(function () {
        //debugger;

        window.parent.Ext.getCmp("winCargaIndividual").hide();
        window.parent.Ext.getCmp("gridPuestos").enable();
        window.parent.Ext.getCmp("winCargaIndividual").destroy();
    });



</script>

