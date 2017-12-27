using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Enumeradores
    {
        public enum ModosFormulario
        {
            Nuevo,
            Editar,
            Aprobado,
            Lectura,
            Impresion
        }
        public enum EstadoSolicitud
        {
            Creada = 1,
            Pendiente = 2,
            Aprobada = 3,
            Rechazada = 4,
            Cancelada = 5,
            Cerrada = 6,
            ErrorSAP = 6,
            EliminacionFisica = 7,
            Eliminada= 7
        }


        public enum TiposSolicitud
        {
            PI,
            API,
            APGE,
            SSLI,
            SSLG,
            PrisMAS,
            CAP,
            INFORME,
            EVENTO,
            ESTADO_RA,
            ESTADO_RO,
            AYUDA
        }

        public enum Mes
        {
            NULL,
            ENE,
            FEB,
            MAR,
            ABR,
            MAY,
            JUN,
            JUL,
            AGO,
            SET,
            OCT,
            NOV,
            DIC
        }

        public enum GruposSharepoint
        {
            Administradores_API,
            Administradores_APG,            
            AdministradoresPrisMAS
        }

        public enum TiposInversion
        {
            Industrial = 2
        }

        public enum CodigoErrorHttp
        {
            LongitudExcedida = -2147467259
        }

        public enum EstadoPeriodo
        {
            Activo = 1,
            Inactivo = 2
        }

        public enum CodigoEstadoSAP
        {
            Pendiente = 0,
            Exitoso = 1,
            //Fallido = 2,
            Pendiente_de_registro_en_SAP = 2,
            Rectificado = 3
        }

        public enum CodClaseAhorro
        {
            Cambio = 1,
            Negociacion = 2
        }

        public enum Administradores
        {
            Api = 3,
            Apge = 4,
            PrisMas = 5
        }
    }
}
