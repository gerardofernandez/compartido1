
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Solución : Entidad
    {
        #region Constructores
        #endregion

        #region Propiedades
        #endregion

        #region Métodos
        #endregion

        #region Eventos 
        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Solución;

            #region Identidad
            this.Identidad.Clave = "Aplimática.Finnerve.Dashboard";

            this.Identidad.Nombre = "Dashboard";
            #endregion
        }

        protected override void PreparandoEntidades()
        {
            base.PreparandoEntidades();

            #region Fuente
            this.Entidades.Agregar(new Fuente(this));

            #region Maestro
            this.Entidades.Agregar(new Maestro(this));

            this.Entidades.Agregar(new Registro(this));
            #endregion
            #endregion

            #region Consulta
            this.Entidades.Agregar(new Consulta(this));

            this.Entidades.Agregar(new Columna(this));
            #endregion

            #region Reporte
            this.Entidades.Agregar(new Reporte(this));

            this.Entidades.Agregar(new Filtro(this));

            #region Representación
            this.Entidades.Agregar(new Representación(this));

            this.Entidades.Agregar(new Agrupación(this));

            this.Entidades.Agregar(new Indicador(this));
            #endregion
            #endregion

            #region Tablero
            this.Entidades.Agregar(new Tablero(this));

            #region Grupo
            this.Entidades.Agregar(new Grupo(this));

            this.Entidades.Agregar(new Tarjeta(this));
            #endregion
            #endregion

            this.Entidades.Agregar(new TableroDeMando(this));
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Fuente(this));

            this.AccesosDirectos.Agregar(new Consulta(this));

            this.AccesosDirectos.Agregar(new Reporte(this));

            this.AccesosDirectos.Agregar(new Tablero(this));

            this.AccesosDirectos.Agregar(new TableroDeMando(this));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Fuentes", TipoAtributo.Carpeta) { Colección = new Fuentes(this) });

            this.Atributos.Add(new Atributo("Consultas", TipoAtributo.Carpeta) { Colección = new Consultas(this) });

            this.Atributos.Add(new Atributo("Reportes", TipoAtributo.Carpeta) { Colección = new Reportes(this) });

            this.Atributos.Add(new Atributo("Tableros", TipoAtributo.Carpeta) { Colección = new Tableros(this) });
        }
        #endregion
    }
}
