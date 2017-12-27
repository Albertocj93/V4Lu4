using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using BusinessEntities;
using Utility;

namespace BusinessLogic
{
    public class UsuarioBL
    {
        
        public UsuarioBE GetByAccount(string connstring, string cuenta)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetByAccount(_oDBHelper, cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioBE GetVistaByAccount(string connstring, string cuenta)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetVistaByAccount(_oDBHelper, cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioBE GetUserRansaByCuentaRed(string connstring, string cuenta)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetUserRansaByCuentaRed(_oDBHelper, cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioBE GetPermisosByPuesto(string connstring, string cuenta, int IdPuesto)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetPermisosByPuesto(_oDBHelper, cuenta,IdPuesto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioBE UsuarioGetPerfil(string connstring, string cuenta)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.UsuarioGetPerfil(_oDBHelper, cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UsuarioBE> GetAll(string connstring)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetAll(_oDBHelper);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsuarioBE> GetPermisosByIdUsuario(string connstring,UsuarioBE oUsuario)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.GetPermisosByIdUsuario(_oDBHelper, oUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean Grabar(string connstring, UsuarioBE UsuarioBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                if(UsuarioBE.Id > 0)
                {
                    return UsuarioDA.Instanse.Update(_oDBHelper, UsuarioBE);
                }
                else
                {
                    return UsuarioDA.Instanse.Insert(_oDBHelper, UsuarioBE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean EliminarTodosPerfiles(string connstring, UsuarioBE UsuarioBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                
                return UsuarioDA.Instanse.EliminarTodosPerfiles(_oDBHelper, UsuarioBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean AsignarAdministrador(string connstring, UsuarioBE UsuarioBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                UsuarioDA.Instanse.EliminarTodosPerfiles(_oDBHelper, UsuarioBE);

                _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.AsignarAdministrador(_oDBHelper, UsuarioBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Boolean UsuarioPerfilEmpresaInsert(string connstring, UsuarioBE UsuarioBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.UsuarioPerfilEmpresaInsert(_oDBHelper, UsuarioBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UsuarioPerfilEmpresaDelete(string connstring, UsuarioBE UsuarioBE)
        {
            try
            {
                DBHelper _oDBHelper = new DBHelper(connstring, Providers.SqlServer);
                return UsuarioDA.Instanse.UsuarioPerfilEmpresaDelete(_oDBHelper, UsuarioBE);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        

    }
}
