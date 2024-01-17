
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public enum TipoDeColumna { Ninguno, Importe, Entero, Fecha, Maestro, Texto }

    public class Columna : Entidad
    {
        #region Constructores
        public Columna() : base() { }

        public Columna(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Consulta
        public Consultas Consultas { get { return (Consultas)this.Atributos["Consulta"].Colección; } set { this.Atributos["Consulta"].Colección = value; } }

        public Consulta Consulta { get { return (Consulta)this.Consultas.Obtener(); } }

        public string IdConsulta { get { return (string)this.Atributos["Consulta"].Valor.Actual; } set { this.Atributos["Consulta"].Valor.Actual = value; } }
        #endregion

        public string Clave { get { return (string)this.Atributos["Clave"].Valor.Actual; } set { this.Atributos["Clave"].Valor.Actual = value; } }

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public TipoDeColumna TipoDeColumna { get { return (TipoDeColumna)this.Atributos["TipoDeColumna"].Valor.Actual; } set { this.Atributos["TipoDeColumna"].Valor.Actual = (int)value; } }

        #region Maestro
        public Maestros Maestros { get { return (Maestros)this.Atributos["Maestro"].Colección; } set { this.Atributos["Maestro"].Colección = value; } }

        public Maestro Maestro { get { return (Maestro)this.Maestros.Obtener(); } }

        public string IdMaestro { get { return (string)this.Atributos["Maestro"].Valor.Actual; } set { this.Atributos["Maestro"].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Columna(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Columna";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Columnas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Consulta", TipoAtributo.Registro) { Colección = new Consultas(this.Solución) });

            this.Atributos.Add(new Atributo("Clave", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("TipoDeColumna", TipoAtributo.Enumeración));

            this.Atributos.Add(new Atributo("Maestro", TipoAtributo.Registro) { Colección = new Maestros(this.Solución) { Ninguno = true }, Dependencia = "TipoDeColumna" });

            //this.Atributos.Add(new Atributo("Enlace", TipoAtributo.Grupo));
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoDeColumna")
            {
                Atributo.Literales.Add(new Elemento(TipoDeColumna.Importe.GetHashCode().ToString(), TipoDeColumna.Importe.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeColumna.Entero.GetHashCode().ToString(), TipoDeColumna.Entero.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeColumna.Fecha.GetHashCode().ToString(), TipoDeColumna.Fecha.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeColumna.Maestro.GetHashCode().ToString(), TipoDeColumna.Maestro.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeColumna.Texto.GetHashCode().ToString(), TipoDeColumna.Texto.ToString()));
            }
        }

        #region RestringiendoAtributo
        private void RestringirAtributoMaestro(Atributo Atributo)
        {
            if (this.TipoDeColumna == TipoDeColumna.Maestro)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Maestro")
                this.RestringirAtributoMaestro(Atributo);
        }
        #endregion
        #endregion
    }

    public class Columnas : Colección
    {
        #region Constructores
        public Columnas(Entidad Solución) : base(Solución, new Columna(Solución)) { }
        #endregion

        #region Métodos
        public Columna Obtener(string Clave) { return (Columna)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Columna(this.Solución); }
        #endregion
    }
}
