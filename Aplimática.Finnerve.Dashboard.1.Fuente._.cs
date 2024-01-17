
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Fuente : Entidad
    {
        #region Constructores
        public Fuente() : base() { }

        public Fuente(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public string CadenaDeConexión { get { return (string)this.Atributos["CadenaDeConexión"].Valor.Actual; } set { this.Atributos["CadenaDeConexión"].Valor.Actual = value; } }

        public Maestros Maestros { get { return (Maestros)this.Atributos["Maestros"].Colección; } set { this.Atributos["Maestros"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Fuente(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Fuente";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Fuentes";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Maestro(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("CadenaDeConexión", TipoAtributo.Edición));

            this.Atributos.Add(new Atributo("Maestros", TipoAtributo.Carpeta) { Colección = new Maestros(this.Solución) });
        }

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Fuentes : Colección
    {
        #region Constructores
        public Fuentes(Entidad Solución) : base(Solución, new Fuente(Solución)) { }
        #endregion

        #region Métodos
        public Fuente Obtener(string Script) { return (Fuente)this.ObtenerX(Script); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Fuente(this.Solución); }
        #endregion
    }
}
