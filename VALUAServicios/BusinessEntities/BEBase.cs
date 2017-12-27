using System;
using Utility;


/* 
 * Creado por           : Jorge Girao
 * Fecha creación       : 2013-05-18
 * Modificado por       :
 * Fecha modificación   :
*/

/// <summary>
/// Summary description for BEBase
/// </summary>
public class BEBase
{
    public BEBase()
    {
        this.Activo = -1;

        this.FechaCreacion = DateTime.MinValue;
        this.FechaModificacion = DateTime.MinValue;
    }

    private int _Activo;
    public int Activo
    {
        get { return _Activo; }
        set { _Activo = value; }
    }

    private DateTime? _FechaCreacion;
    public DateTime? FechaCreacion
    {
        get { return _FechaCreacion; }
        set { _FechaCreacion = value; }
    }

    private DateTime? _FechaModificacion;
    public DateTime? FechaModificacion
    {
        get { return _FechaModificacion; }
        set { _FechaModificacion = value; }
    }

    private string _UsuarioCreacion;
    public string UsuarioCreacion
    {
        get { return _UsuarioCreacion; }
        set { _UsuarioCreacion = value; }
    }

    private string _UsuarioModificacion;
    public string UsuarioModificacion
    {
        get { return _UsuarioModificacion; }
        set { _UsuarioModificacion = value; }
    }
}