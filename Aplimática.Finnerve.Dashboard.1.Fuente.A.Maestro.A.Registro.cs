
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Registro : Entidad
    {
        #region Constructores
        public Registro() : base() { }

        public Registro(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Maestro
        public Maestros Maestros { get { return (Maestros)this.Atributos["Maestro"].Colección; } set { this.Atributos["Maestro"].Colección = value; } }

        public Maestro Maestro { get { return (Maestro)this.Maestros.Obtener(); } }

        public string IdMaestro { get { return (string)this.Atributos["Maestro"].Valor.Actual; } set { this.Atributos["Maestro"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Registro(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Registro";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Registros";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Maestro", TipoAtributo.Registro) { Colección = new Maestros(this.Solución) });

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));
        }

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Registros : Colección
    {
        #region Constructores
        public Registros(Entidad Solución) : base(Solución, new Registro(Solución)) { }
        #endregion

        #region Métodos
        public Registro Obtener(string Clave) { return (Registro)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Registro(this.Solución); }
        #endregion
    }
}
