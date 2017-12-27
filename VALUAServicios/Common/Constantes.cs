using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Constantes
    {
        public const int ACTIVO = 1;
        public const int INACTIVO = 0;

        /* VALORES NULOS */
        public const int INT_NULO = -1;
        public static readonly DateTime FECHA_NULA = DateTime.MinValue;

        public const string K_TEXTO_EXISTE_CODIGO = "El código ya existe";
        public const string K_TEXTO_COMBO_TODOS = "::Todos::";
        public const string K_TEXTO_COMBO_SELECCIONE = "::Seleccione::";
        public const string K_ABREVIATURA_MONEDA_DOLAR = "US$";
        public const string K_TEXTO_GRILLA_CANTIDAD_REGISTROS = "Se han encontrado {0} registro(s).";
        public const int K_LIMITE_MAXIMO_CARGA_ARCHIVO = 5242880;
        public const int K_LIMITE_MAXIMO_ARCHIVOS = 5;
        public const string K_MENSAJE_ENVIO_RO_CORRECTO= "Se ha enviado el evento de riesgo correctamente.";
        public const string K_MENSAJE_ENVIO_CAMBIO_AREA = "El área ha sido cambiado correctamente.";
        public const string K_MENSAJE_ENVIO_CAMBIO_AREA_ERROR = "Ocurrio un erro al cambiar el área";
        public const string K_MENSAJE_ENVIO_RO_ERROR = "Ocurrio un erro al iniciar el flujo de evento de riesgo.";
        public const string K_MENSAJE_VALIDAR_RO = "Se debe seleccionar un Responsable de Observación.";
        public const string K_MENSAJE_VALIDAR_ADJUNTO_RO = "Se debe agregar un archivo adjunto.";
        public const string K_MENSAJE_REGISTRO_SOLICITUD_SATISFACTORIO = "Se ha guardado el informe correctamente. Para iniciar el flujo de aprobación primero debe enviar el informe.";
        public const string K_MENSAJE_REGISTRO_INFORME_VALIDAR_ESTADO_RS = "Por favor, registre los datos obligatorios del evento de riesgo.";
        public const string K_MENSAJE_REGISTRO_EVENTO_SATISFACTORIO = "Se ha guardado el evento correctamente. Para iniciar el flujo de aprobación primero debe enviar el informe.";
        public const string K_MENSAJE_ACTUALIZACION_SOLICITUD_SATISFACTORIO = "Se ha guardado los cambios del informe satisfactoriamente.";
        public const string K_MENSAJE_ACTUALIZACION_EVENTO_SATISFACTORIO = "Se ha guardado los cambios del evento satisfactoriamente.";
        public const string K_MENSAJE_ACTUALIZACION_EVENTO_VALIDAR_FECHA = "La fecha no debe ser menor a la fecha de aprobación del evento.";
        public const string K_MENSAJE_ELIMINACION_RUBRO = "Se ha eliminado el registro correctamente.";
        public const string K_MENSAJE_ACTUALIZACION_RUBRO = "Se ha guardado los cambios del rubro satisfactoriamente.";
        public const string K_MENSAJE_NO_EXISTE_CODIGO_DIVISION_EN_MAESTROS = "Estimado Administrador: El código de división no existe en la tabla Maestro, registre el código para proceder con la solicitud.";
        public const string K_MENSAJE_MONTO_SOLICITUD_DESEMBOLSO_IGUALES = "El monto del flujo de desembolso debe ser igual al monto de la solicitud. Por favor completar el flujo de desembolso.";
        public const string K_MENSAJE_NO_EXISTE_FECHA_INICIO_FIN = "Por favor ingrese la fecha inicio y fecha fin.";
        public const string K_MENSAJE_NO_EXISTE_FLUJO_DESEMBOLSO = "Antes de enviar aprobación la solicitud, debe ingresar los desembolsos.";
        public const string K_MENSAJE_ENVIO_SOLICITUD = "Se ha iniciado el flujo de aprobación para la solicitud.";
        public const string K_MENSAJE_ENVIO_INFORME = "Se ha iniciado el flujo de aprobación para el informe.";
        public const string K_MENSAJE_GUARDAR_VALIDACION_SOCIEDAD_PAIS = "La sociedad no pertenece al país seleccionado.";
        public const string K_MENSAJE_APROBACION_PI_EXITO = "Se han aprobado las solicitudes seleccionadas.";
        public const string K_MENSAJE_SELECCIONE_APROBACION_PI = "Por favor seleccione por lo menos una solicitud para aprobar.";
        public const string K_MENSAJE_RECHAZO_PI_EXITO = "Se han rechazado las solicitudes seleccionadas.";
        public const string K_MENSAJE_VALIDAR_EXISTENCIA_DESEMBOLSO = "No se puede eliminar el Rubro porque ya se encuentra registrado en los desembolsos.";
        public const string K_MENSAJE_TRASLADO_EXITO = "Se realizó el proceso de traslado de solicitudes correctamente.";
        public const string K_MENSAJE_APROBACION_EXITO = "Se ha aprobado la solicitud correctamente.";
        public const string K_MENSAJE_APROBACION_EXITO_INFORME = "Se ha aprobado el informe correctamente.";
        public const string K_MENSAJE_RECHAZO_EXITO = "Se ha rechazado la solicitud correctamente.";
        public const string K_MENSAJE_RECHAZO_EXITO_INFORME = "Se ha rechazado el informe correctamente.";
        public const string K_MENSAJE_CANCELACION_EXITO = "Se ha cancelado la solicitud correctamente.";
        public const string K_MENSAJE_CANCELACION_EXITO_INFORME = "Se ha anulado el informe correctamente.";
        public const string K_MENSAJE_ELIMINACION_MAESTRO_EXITO = "Se ha eliminado el registro correctamente.";
        public const string K_MENSAJE_ELIMINACION_DETALLESOCIEDADCLASEACTIVO_EXITO = "Se ha eliminado el registro correctamente.";
        public const string K_MENSAJE_EXISTE_NOTIFICACION_SOCIEDAD = "Error. Existe una notificación para esta sociedad.";
        public const string K_MENSAJE_ELIMINACION_MAESTRO_EXISTE_REFERENCIA = "No se puede eliminar el registro ya que se encuentra referenciado en una o varios maestros.";

        public const string K_MENSAJE_APROBACION_NO_PERMISO = "Usted no tiene permiso para aprobar esta solicitud.";
        public const string K_MENSAJE_APROBACION_ERROR_GUID = "Usted no puede aprobar en estos momentos inténtelo mas tarde o comuníquese con el Administrador";
        public const string K_MENSAJE_RECHAZO_NO_PERMISO = "Usted no tiene permiso para rechazar esta solicitud.";
        public const string K_MENSAJE_CANCELACION_NO_PERMISO = "Usted no tiene permiso para cancelar esta solicitud.";
        public const string K_MENSAJE_REENVIO_SOLICITUD = "Se ha reenviado la solicitud e iniciado el flujo de aprobación.";
        public const string K_MENSAJE_REENVIO_INFORME = "Se ha reenviado el informe e iniciado el flujo de aprobación.";
        public const string K_TEXTO_GRILLA_NO_REGISTROS = "No se han obtenido resultados con los criterios seleccionados.";
        public const string K_MENSAJE_NO_ADMINISTRADOR = "Error. Usted no tiene Permiso para ingresar a la Página.";
        public const string K_MENSAJE_ARCHIVO_INVALIDO = "Error, Archivo Inválido.";
        public const string K_MENSAJE_ARCHIVO_CARGA = "Archivo de carga incorrecto. Verificar número de columnas.";
        public const string K_MENSAJE_ARCHIVO_DATOS = "Error. Archivo no posee datos para carga.";
        public const string K_MENSAJE_ARCHIVO_FORMATO = "Error. El formato del archivo es incorrecto.";
        public const string K_MENSAJE_ARCHIVO_SELECCIONAR = "Error. Porfavor seleccione Archivo.";
        public const string K_MENSAJE_ARCHIVO_SATISFACTORIAMENTE = "Se Guardaron los cambios satisfactoriamente.";
        public const string K_MENSAJE_ARCHIVO_ERRORGUARDAR = "No Se Guardaron los cambios.";
        public const string K_MENSAJE_SELECCIONE_REGISTRO = "Por favor, seleccione un registro.";
        public const string K_MENSAJE_ELIMINACION_DETALLESOCIEDADCLASEACTIVO_EXISTE_REFERENCIA = "No se puede eliminar el registro ya que se encuentra referenciado en una o más solicitudes.";
        public const string K_MENSAJE_ERROR_MONTO_NO_PERMITIDO_API = "El monto mínimo para registrar un API es {0}.";
        public const string K_MENSAJE_ERROR_MONTO_NO_PERMITIDO_PI = "El monto máximo para registrar un PI es {0}.";
        public const string K_MENSAJE_MODIFICAR_FLUJO_DESEMBOLSO = "La fecha fin de la solicitud no puede ser modificada porque existen desembolsos relacionados. Por favor actualice su flujo de desembolso.";
        public const string K_MENSAJE_MODIFICAR_FLUJO_DESEMBOLSO_FORM = "La Fecha Inicio / Fecha Fin de la solicitud no puede ser modificada porque existen desembolsos relacionados. Por favor actualice su flujo de desembolso.";
        public const string K_TITULO_API = "SOLICITUD DE APROBACIÓN PARA INVERSIÓN";
        public const string K_TITULO_INFORME = "INFORME";
        public const string K_TITULO_EVENTO = "EVENTO DE RIESGO";
        public const string K_TITULO_SSL = "SOLICITUD SUB LIMITE";
        public const string K_TITULO_PI = "SOLICITUD DE PLAN DE INVERSIONES";
        public const string K_TITULO_APG = "SOLICITUD DE APROBACIÓN PARA GASTOS EXTRAORDINARIOS";
        public const string K_TITULO_SOLICITUDSUBLIMITE = "SOLICITUD DE APROBACIÓN SUB LÍMITE";

        public const string K_MENSAJE_SOLICITUD_PI = "Usted ha ingresado a la opción de registro de una solicitud de Plan de Inversiones (PI). Esto no es un API";
        public const string K_MENSAJE_SATISFACTORIO = "Se ha guardado el registro correctamente";

        //INICIATIVAS
        public const string K_TITULO_INICIATIVA = "INICIATIVAS";
        public const string K_MENSAJE_REGISTRO_INICIATIVA_SATISFACTORIO = "Se ha guardado correctamente la iniciativa. Para comenzar el flujo de aprobación primero debe enviar la iniciativa.";
        public const string K_MENSAJE_ENVIO_INICIATIVA= "Se ha iniciado el flujo de aprobación para la iniciativa.";
        public const string K_MENSAJE_ENVIO_GUARDADO_INICIATIVA = "Se ha guardado la iniciativa correctamente pero ocurrió un error al enviarla.";
        public const string K_MENSAJE_CANCELACION_EXITO_INICIATIVA = "Se ha cancelado correctamente la iniciativa.";
        public const string K_MENSAJE_ELIMINACION_EXITO_INICIATIVA = "Se ha eliminado correctamente la iniciativa.";
        public const string K_MENSAJE_ACTUALIZACION_INICIATIVA_SATISFACTORIO = "Se ha guardado los cambios en la iniciativa satisfactoriamente";
        public const string K_TITULO_PRISMAS = "INICIATIVAS";
        public const string K_MENSAJE_REENVIO_INICIATIVA = "Se ha reenviado la iniciativa y se ha iniciado el flujo de aprobación.";
        public const string K_MENSAJE_APROBACION_EXITO_INICIATIVA = "Se ha aprobado correctamente la iniciativa.";
        public const string K_MENSAJE_APROBACION_NO_PERMISO_INICIATIVA = "Usted no tiene permiso para aprobar esta iniciativa.";
        public const string K_MENSAJE_APROBACION_ERROR_GUID_INICIATIVA = "Usted no puede aprobar en estos momentos inténtelo mas tarde o comuníquese con el Administrador";
        public const string K_MENSAJE_RECHAZO_EXITO_INICIATIVA = "Se ha rechazado la iniciativa correctamente.";
        public const string K_MENSAJE_RECHAZO_NO_PERMISO_INICIATIVA = "Usted no tiene permiso para rechazar esta iniciativa.";
        public const string K_MENSAJE_ACTUALIZACION_SOLICITUD_SATISFACTORIO_INICIATIVA = "Se ha guardado los cambios en la iniciativa satisfactoriamente";
        public const string K_MENSAJE_ELIMINACION_MAESTRO_INICIATIVA_EXISTE_REFERENCIA = "No se puede eliminar el registro ya que se encuentra referenciado en una o más iniciativas.";
        public const string K_MENSAJE_REGISTRO_INICIATIVA_PARTICIPANTE = "Ya existe un responsable PrisMAS+.";
        public const string K_MENSAJE_REGISTRO_INICIATIVA_PARTICIPANTE_EXISTE = "El/la participante ya existe en la lista.";
        public const string K_MENSAJE_REGISTRO_INICIATIVA_RESPONSABLE_EXISTE = "El área ya está seleccionada en otra opción. Por favor seleccione otra área.";

        //CAPTURAS
        public const int K_LIMITE_MAXIMO_ARCHIVOS_CAPTURA = 5;
        public const string KTituloCapturas = "CAPTURAS";
        public const string KMensajeEnvioCaptura = "Se ha iniciado el flujo de aprobación para la captura.";
        public const string KMensajeEnvioCapturaExiste = "Registre un mes distinto, para el mes seleccionado ya existe una captura.";
        public const string KMensajeEnvioCapturaMesesPrev = "Solo se pueden registrar capturas posteriores a la última registrada.";
        public const string KMensajeEnvioCapturaRechazadas = "Existen capturas rechazadas, primero debe reenviarlas.";
        public const string KMensajeEnvioCapturaMesActual = "Solo se pueden registrar capturas para meses menores o iguales al mes en curso.";
        public const string KMensajeCancelacionExitoCaptura = "Se ha cancelado correctamente la captura.";
        public const string KMensajeReeniciarExitoCaptura = "Se han reeniciado correctamente las capturas.";
        public const string KMensajeReenvioCaptura = "Se ha reenviado la captura.";
        public const string KMensajeAprobacionExitoCaptura = "Se ha aprobado correctamente la captura.";
        public const string KMensajeAprobacionErrorGuidCaptura = "Usted no puede aprobar en estos momentos inténtelo mas tarde o comuníquese con el Administrador";
        public const string KMensajeAprobacionErrorCapturaMasivo = "Usted no puede aprobar en estos momentos inténtelo mas tarde o comuníquese con el Administrador";
        public const string KMensajeAprobacionNoPermisoCaptura = "Usted no tiene permiso para aprobar esta captura.";
        public const string KMensajeRechazoExitoCaptura = "Se ha rechazado la captura correctamente.";
        public const string KMensajeRechazoExitoCapturaMasivo = "Se ha(n) rechazado la(s) captura(s) seleccionada(s).";
        public const string KMensajeAprobacionExitoCapturaMasivo = "Se ha(n) aprobado la(s) captura(s) seleccionada(s).";
        public const string KMensajeAprobacionSeleccionarMasivo = "Por lo menos seleccione una captura.";
        public const string KMensajeRechazoNoPermisoCaptura = "Usted no tiene permiso para rechazar esta captura.";
        public const string KMensajeRechazoNoPermisoCapturaMasivo = "Usted no tiene permiso para rechazar la(s) captura(s) seleccionada(s).";
        public const string KMensajeActualizacionSolicitudSatisfactorioCaptura = "Se ha guardado los cambios en la captura satisfactoriamente";
        public const string KMensajeRegistroCapturaSatisfactorio = "Se ha guardado correctamente la captura. Para comenzar el flujo de aprobación primero debe enviar la captura.";
        public const string KMensajeEnvioErrorCaptura = "Ocurrió un error al enviar la captura, por favor intente nuevamente.";
        public const string KMensajeEnvioCapturaSinArchivo = "La captura no tiene archivo adjunto, por favor refresque la pantalla e intente nuevamente.";
        public const string KMensajeEnvioErrorInforme= "Ocurrió un error al enviar el informe, por favor intente nuevamente.";
        public const string KMensajeEnvioResponsableSeguimiento = "La tarea ha sido ejecutada por otro usuario.";
        public const string KMensajeValidarCamposEventoRiesgo = "No se ha podido reenviar el informe. Debe registrar los datos obligatorios del evento de riesgo.";

        public const string MonedaDolares = "5E71E1A2-F9BF-4EE7-ADFF-59381CD9FAAE";


        //Tipo Detalle Traslado
        public const string TipoOrigen = "Origen";
        public const string TipoDestino = "Destino";
    }
}
