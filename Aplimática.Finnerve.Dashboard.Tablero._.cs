
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Tablero : Entidad
    {
        #region Constructores
        public Tablero() : base() { }

        public Tablero(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public new Grupos Grupos { get { return (Grupos)this.Atributos["Grupos"].Colección; } set { this.Atributos["Grupos"].Colección = value; } }

        public Tarjetas Tarjetas { get { return (Tarjetas)this.Atributos["Tarjetas"].Colección; } set { this.Atributos["Tarjetas"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Tablero(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Tablero";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Tableros";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Grupo(this.Solución));

            this.AccesosDirectos.Agregar(new Tarjeta(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Grupos", TipoAtributo.Carpeta) { Colección = new Grupos(this.Solución) });

            this.Atributos.Add(new Atributo("Tarjetas", TipoAtributo.Carpeta) { Colección = new Tarjetas(this.Solución) });
        }

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Tableros : Colección
    {
        #region Constructores
        public Tableros(Entidad Solución) : base(Solución, new Tablero(Solución)) { }
        #endregion

        #region Métodos
        public Tablero Obtener(string Script) { return (Tablero)this.ObtenerX(Script); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Tablero(this.Solución); }
        #endregion
    }
}
