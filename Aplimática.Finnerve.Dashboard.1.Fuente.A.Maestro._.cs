
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Maestro : Entidad
    {
        #region Constructores
        public Maestro() : base() { }

        public Maestro(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Fuente
        public Fuentes Fuentes { get { return (Fuentes)this.Atributos["Fuente"].Colección; } set { this.Atributos["Fuente"].Colección = value; } }

        public Fuente Fuente { get { return (Fuente)this.Fuentes.Obtener(); } }

        public string IdFuente { get { return (string)this.Atributos["Fuente"].Valor.Actual; } set { this.Atributos["Fuente"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public Registros Registros { get { return (Registros)this.Atributos["Registros"].Colección; } set { this.Atributos["Registros"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Maestro(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Maestro";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Maestros";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Registro(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Fuente", TipoAtributo.Registro) { Colección = new Fuentes(this.Solución) });

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Registros", TipoAtributo.Carpeta) { Colección = new Registros(this.Solución) });
        }

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Maestros : Colección
    {
        #region Constructores
        public Maestros(Entidad Solución) : base(Solución, new Maestro(Solución)) { }
        #endregion

        #region Métodos
        public Maestro Obtener(string Clave) { return (Maestro)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Maestro(this.Solución); }
        #endregion
    }
}
