
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public enum TipoDeIndicador { Ninguno, Conteo, Suma }

    public enum OrdenDeIndicador { Cero, Ninguno, Ascendente, Descendente }

    public class Indicador : Entidad
    {
        #region Constructores
        public Indicador() : base() { }

        public Indicador(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Representación
        public Representaciones Representaciones { get { return (Representaciones)this.Atributos["Representación"].Colección; } set { this.Atributos["Representación"].Colección = value; } }

        public Representación Representación { get { return (Representación)this.Representaciones.Obtener(); } }

        public string IdRepresentación { get { return (string)this.Atributos["Representación"].Valor.Actual; } set { this.Atributos["Representación"].Valor.Actual = value; } }
        #endregion

        public TipoDeIndicador TipoDeIndicador { get { return (TipoDeIndicador)this.Atributos["TipoDeIndicador"].Valor.Actual; } set { this.Atributos["TipoDeIndicador"].Valor.Actual = (int)value; } }

        #region Columna
        public Columnas Columnas { get { return (Columnas)this.Atributos["Columna"].Colección; } set { this.Atributos["Columna"].Colección = value; } }

        public Columna Columna { get { return (Columna)this.Columnas.Obtener(); } }

        public string IdColumna { get { return (string)this.Atributos["Columna"].Valor.Actual; } set { this.Atributos["Columna"].Valor.Actual = value; } }
        #endregion

        public string Script { get { return (string)this.Atributos["Script"].Valor.Actual; } set { this.Atributos["Script"].Valor.Actual = value; } }

        public OrdenDeIndicador OrdenDeIndicador { get { return (OrdenDeIndicador)this.Atributos["OrdenDeIndicador"].Valor.Actual; } set { this.Atributos["OrdenDeIndicador"].Valor.Actual = (int)value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Indicador(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Indicador";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Indicadores";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.TipoDeIndicador.ToString() + " de " + this.Columna.Registro.Título.ToLower();
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Representación", TipoAtributo.Registro) { Colección = new Representaciones(this.Solución) });

            this.Atributos.Add(new Atributo("TipoDeIndicador", TipoAtributo.Enumeración));

            this.Atributos.Add(new Atributo("Columna", TipoAtributo.Registro) { Colección = new Columnas(this.Solución) });

            this.Atributos.Add(new Atributo("Script", TipoAtributo.Texto) { Dependencia = "TipoDeIndicador;Columna" });

            this.Atributos.Add(new Atributo("OrdenDeIndicador", TipoAtributo.Enumeración));
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoDeIndicador")
            {
                Atributo.Literales.Add(new Elemento(TipoDeIndicador.Conteo.GetHashCode().ToString(), TipoDeIndicador.Conteo.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeIndicador.Suma.GetHashCode().ToString(), TipoDeIndicador.Suma.ToString()));
            }

            if (Atributo.Identidad.Clave == "OrdenDeIndicador")
            {
                Atributo.Literales.Add(new Elemento(OrdenDeIndicador.Ninguno.GetHashCode().ToString(), OrdenDeIndicador.Ninguno.ToString()));

                Atributo.Literales.Add(new Elemento(OrdenDeIndicador.Ascendente.GetHashCode().ToString(), OrdenDeIndicador.Ascendente.ToString()));

                Atributo.Literales.Add(new Elemento(OrdenDeIndicador.Descendente.GetHashCode().ToString(), OrdenDeIndicador.Descendente.ToString()));
            }
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

        #region RestringiendoAtributo
        private void RestringirScript()
        {
            this.Script = this.TipoDeIndicador.ToString() + "De" + this.Columna.Clave;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Script")
                this.RestringirScript();
        }
        #endregion

        protected override void Cambiado(AcciónDeCambio AcciónDeCambio)
        {
            this.Representación.ActualizarScript();

            base.Cambiado(AcciónDeCambio);
        }
        #endregion
    }

    public class Indicadores : Colección
    {
        #region Constructores
        public Indicadores(Entidad Solución) : base(Solución, new Indicador(Solución)) { }
        #endregion

        #region Métodos
        public Indicador Obtener(string Clave) { return (Indicador)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Indicador(this.Solución); }
        #endregion
    }
}
