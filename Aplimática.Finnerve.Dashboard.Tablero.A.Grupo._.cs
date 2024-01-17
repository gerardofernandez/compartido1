
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Grupo : Entidad
    {
        #region Constructores
        public Grupo() : base() { }

        public Grupo(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Tablero
        public Tableros Tableros { get { return (Tableros)this.Atributos["Tablero"].Colección; } set { this.Atributos["Tablero"].Colección = value; } }

        public Tablero Tablero { get { return (Tablero)this.Tableros.Obtener(); } }

        public string IdTablero { get { return (string)this.Atributos["Tablero"].Valor.Actual; } set { this.Atributos["Tablero"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public Tarjetas Tarjetas { get { return (Tarjetas)this.Atributos["Tarjetas"].Colección; } set { this.Atributos["Tarjetas"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Grupo(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Grupo";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Grupos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Tarjeta(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Tablero", TipoAtributo.Registro) { Colección = new Tableros(this.Solución) });

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

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

    public class Grupos : Colección
    {
        #region Constructores
        public Grupos(Entidad Solución) : base(Solución, new Grupo(Solución)) { }
        #endregion

        #region Métodos
        public Grupo Obtener(string Clave) { return (Grupo)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Grupo(this.Solución); }
        #endregion
    }
}
