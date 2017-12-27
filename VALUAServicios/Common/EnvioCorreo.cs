using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using BusinessEntities;
using System.IO;
using System.Configuration;
namespace Common
{

    public class ArchivoAdjuntoCorreo
    {

        public string NombreArchivo { get; set; }
        public string FuenteHTML { get; set; }
        public byte[] FlujoArchivo { get; set; }

    }
    public class EnvioCorreo
    {
        private string _Host;
        private string _Usuario;
        private string _Contrasena;
        private int _Puerto;
        private bool _UsaSSL;

        public EnvioCorreo(string psHost, string psUsuario, string psContrasena, int piPuerto, bool pbUsaSSL)
        {
            _Host = psHost;
            _Usuario = psUsuario;
            _Contrasena = psContrasena;
            _Puerto = piPuerto;
            _UsaSSL = pbUsaSSL;
        }
        /// <summary>
        /// Envía un correo electrónico
        /// </summary>
        /// <param name="plDestinatarios">Lista de correos de destinatarios</param>
        /// <param name="psDe">Cuenta de correo de origen</param>
        /// <param name="psCuerpo">Cuerpo del correo</param>
        /// <param name="psAsunto">Asunto del correo</param>
        /// <param name="plArchivosAdjuntos">Lista de archivos adjuntos tipo Common.ArchivoAdjuntoCorreo</param>
        /// 
        public void EnviarCorreoElectronico(List<UsuarioBE> plDestinatarios, string psDe, string psCuerpo, string psAsunto, List<ArchivoAdjuntoCorreo> plArchivosAdjuntos)
        {
            string DestinatarioConfig=ConfigurationManager.AppSettings["Destinatario"].ToString();
            MailMessage oMailMessage = new MailMessage();
            foreach (UsuarioBE destinatario in plDestinatarios)
            {
                if (!String.IsNullOrEmpty(destinatario.Email))
                {
                    //oMailMessage.To.Add("enrique.tito@gestionysistemas.com");
                    if (DestinatarioConfig.ToUpper() == "DEFAULT")
                    {

                        oMailMessage.To.Add(destinatario.Email);
                        //oMailMessage.To.Add("gabriel.romero@gestionysistemas.com");
                    }
                    else
                    {
                        oMailMessage.To.Add(DestinatarioConfig);
                        //oMailMessage.To.Add("west_red01@hotmail.com");
                    }
                }
                else 
                {
                    oMailMessage.To.Add(DestinatarioConfig);
                }
            }

            oMailMessage.From = new MailAddress(psDe);
            oMailMessage.Body = psCuerpo;
            oMailMessage.Subject = psAsunto;
            oMailMessage.IsBodyHtml = true;
            oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            if (plArchivosAdjuntos != null)
                foreach (ArchivoAdjuntoCorreo oArchivoAdjunto in plArchivosAdjuntos)
                {

                    oMailMessage.Attachments.Add((new Attachment(new MemoryStream(oArchivoAdjunto.FlujoArchivo), oArchivoAdjunto.NombreArchivo, "application/pdf") { ContentType = new System.Net.Mime.ContentType("application/pdf; charset=utf-8") }));

                    //oMailMessage.Attachments.Add(Attachment.CreateAttachmentFromString(oArchivoAdjunto.FuenteHTML, oArchivoAdjunto.NombreArchivo, System.Text.Encoding.Default, null));
                }



            SmtpClient oSmtpClient = new SmtpClient()
           {
               Host = _Host,
               Credentials = new NetworkCredential(_Usuario, _Contrasena),
               Port = _Puerto,
               EnableSsl = false    //_UsaSSL
           };


            try
            {

                oSmtpClient.Send(oMailMessage);

            }
            catch (Exception ex)
            {
                throw  ex;

            }
        }

        public void EnviarCorreoElectronicoCC(List<UsuarioBE> plDestinatarios, string psDe, string psCuerpo, string psAsunto, List<ArchivoAdjuntoCorreo> plArchivosAdjuntos, List<UsuarioBE> plDestinatarioCC)
        {
            string DestinatarioConfig = ConfigurationManager.AppSettings["Destinatario"].ToString();
            MailMessage oMailMessage = new MailMessage();
            foreach (UsuarioBE destinatario in plDestinatarios)
            {
                if (!String.IsNullOrEmpty(destinatario.Email))
                {
                    //oMailMessage.To.Add("enrique.tito@gestionysistemas.com");
                    if (DestinatarioConfig.ToUpper() == "DEFAULT")
                    {

                        oMailMessage.To.Add(destinatario.Email);
                        //oMailMessage.To.Add("gabriel.romero@gestionysistemas.com");
                    }
                    else
                    {
                        oMailMessage.To.Add(DestinatarioConfig);
                        //oMailMessage.To.Add("west_red01@hotmail.com");
                    }
                }
                else
                {
                    oMailMessage.To.Add(DestinatarioConfig);
                }
            }

            foreach (UsuarioBE destinatario in plDestinatarioCC)
            {
                if (!String.IsNullOrEmpty(destinatario.Email))
                {
                    if (DestinatarioConfig.ToUpper() == "DEFAULT")
                    {
                        oMailMessage.CC.Add(destinatario.Email);
                    }
                    else
                    {
                        oMailMessage.CC.Add(DestinatarioConfig);
                    }
                }
            }

            oMailMessage.From = new MailAddress(psDe);
            oMailMessage.Body = psCuerpo;
            oMailMessage.Subject = psAsunto;
            oMailMessage.IsBodyHtml = true;
            oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            if (plArchivosAdjuntos != null)
                foreach (ArchivoAdjuntoCorreo oArchivoAdjunto in plArchivosAdjuntos)
                {

                    oMailMessage.Attachments.Add((new Attachment(new MemoryStream(oArchivoAdjunto.FlujoArchivo), oArchivoAdjunto.NombreArchivo, "application/pdf") { ContentType = new System.Net.Mime.ContentType("application/pdf; charset=utf-8") }));

                    //oMailMessage.Attachments.Add(Attachment.CreateAttachmentFromString(oArchivoAdjunto.FuenteHTML, oArchivoAdjunto.NombreArchivo, System.Text.Encoding.Default, null));
                }



            SmtpClient oSmtpClient = new SmtpClient()
            {
                Host = _Host,
                Credentials = new NetworkCredential(_Usuario, _Contrasena),
                Port = _Puerto,
                EnableSsl = false    //_UsaSSL
            };


            try
            {

                oSmtpClient.Send(oMailMessage);

            }
            catch (Exception ex)
            {
                //throw  ex;

            }
        }

        public void EnviarNotificacionAdministrador(List<UsuarioBE> plDestinatarios, string psDe, string psCuerpo, string psAsunto)
        {
            string DestinatarioConfig = ConfigurationManager.AppSettings["Destinatario"].ToString();
            MailMessage oMailMessage = new MailMessage();
            foreach (UsuarioBE destinatario in plDestinatarios)
            {

                if (!String.IsNullOrEmpty(destinatario.Email))
                {
                    //oMailMessage.To.Add("enrique.tito@gestionysistemas.com");
                    if (DestinatarioConfig.ToUpper() == "DEFAULT")
                    {
                        oMailMessage.To.Add(destinatario.Email);
                        //oMailMessage.To.Add("gabriel.romero@gestionysistemas.com");
                    }
                    else
                    {
                        oMailMessage.To.Add(DestinatarioConfig);
                        //oMailMessage.To.Add("west_red01@hotmail.com");
                    }
                }
                else
                {
                    oMailMessage.To.Add(DestinatarioConfig);
                }

                oMailMessage.From = new MailAddress(psDe);
                oMailMessage.Body = psCuerpo;
                oMailMessage.Subject = psAsunto;
                oMailMessage.IsBodyHtml = true;
                oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                SmtpClient oSmtpClient = new SmtpClient()
                {
                    Host = _Host,
                    Credentials = new NetworkCredential(_Usuario, _Contrasena),
                    Port = _Puerto,
                    EnableSsl = false    //_UsaSSL
                };

                try
                {
                    oSmtpClient.Send(oMailMessage);
                }
                catch (Exception ex)
                {
                }
            }
        }

    }


}
