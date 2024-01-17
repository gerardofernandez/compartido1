
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Agrupación : Entidad
    {
        #region Constructores
        public Agrupación() : base() { }

        public Agrupación(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Representación
        public Representaciones Representaciones { get { return (Representaciones)this.Atributos["Representación"].Colección; } set { this.Atributos["Representación"].Colección = value; } }

        public Representación Representación { get { return (Representación)this.Representaciones.Obtener(); } }

        public string IdRepresentación { get { return (string)this.Atributos["Representación"].Valor.Actual; } set { this.Atributos["Representación"].Valor.Actual = value; } }
        #endregion

        public int Orden { get { return (int)this.Atributos["Orden"].Valor.Actual; } set { this.Atributos["Orden"].Valor.Actual = value; } }

        #region Columna
        public Columnas Columnas { get { return (Columnas)this.Atributos["Columna"].Colección; } set { this.Atributos["Columna"].Colección = value; } }

        public Columna Columna { get { return (Columna)this.Columnas.Obtener(); } }

        public string IdColumna { get { return (string)this.Atributos["Columna"].Valor.Actual; } set { this.Atributos["Columna"].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Agrupación(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Agrupación";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Agrupaciones";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Orden.ToString() + " - " + this.Columna.Registro.Título;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Representación", TipoAtributo.Registro) { Colección = new Representaciones(this.Solución) });

            this.Atributos.Add(new Atributo("Orden", TipoAtributo.Entero));

            this.Atributos.Add(new Atributo("Columna", TipoAtributo.Registro) { Colección = new Columnas(this.Solución) });
        }

        #region SeleccionandoColección
        private void SeleccionarColumnas()
        {
            this.Columnas.TextoComando.Fuente = "SELECT * FROM Columnas WHERE Consulta = '" + this.Representación.Reporte.IdConsulta + "'";
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Columna")
                this.SeleccionarColumnas();
        }
        #endregion

        protected override void Cambiado(AcciónDeCambio AcciónDeCambio)
        {
            this.Representación.ActualizarScript();

            base.Cambiado(AcciónDeCambio);
        }
        #endregion
    }

    public class Agrupaciones : Colección
    {
        #region Constructores
        public Agrupaciones(Entidad Solución) : base(Solución, new Agrupación(Solución)) { }
        #endregion

        #region Métodos
        public Agrupación Obtener(string Clave) { return (Agrupación)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Agrupación(this.Solución); }
        #endregion
    }
}
