
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public enum OperadorMatemático { Ninguno, Igual, Diferente, Mayor, Menor, MayorIgual, MenorIgual }

    public class Filtro : Entidad
    {
        #region Constructores
        public Filtro() : base() { }

        public Filtro(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Reporte
        public Reportes Reportes { get { return (Reportes)this.Atributos["Reporte"].Colección; } set { this.Atributos["Reporte"].Colección = value; } }

        public Reporte Reporte { get { return (Reporte)this.Reportes.Obtener(); } }

        public string IdReporte { get { return (string)this.Atributos["Reporte"].Valor.Actual; } set { this.Atributos["Reporte"].Valor.Actual = value; } }
        #endregion

        #region Columna
        public Columnas Columnas { get { return (Columnas)this.Atributos["Columna"].Colección; } set { this.Atributos["Columna"].Colección = value; } }

        public Columna Columna { get { return (Columna)this.Columnas.Obtener(); } }

        public string IdColumna { get { return (string)this.Atributos["Columna"].Valor.Actual; } set { this.Atributos["Columna"].Valor.Actual = value; } }
        #endregion

        public OperadorMatemático OperadorMatemático { get { return (OperadorMatemático)this.Atributos["OperadorMatemático"].Valor.Actual; } set { this.Atributos["OperadorMatemático"].Valor.Actual = (int)value; } }

        public Valor Valor = new Valor();

        public string Script { get { return (string)this.Atributos["Script"].Valor.Actual; } set { this.Atributos["Script"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Filtro(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Filtro";
        }

        protected override void PreparandoExtensiones()
        {
            base.PreparandoExtensiones();

            this.Extensiones.Agregar(this.Valor);
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Filtros";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            //this.Registro.Título = "Que " + this.Columna.Registro.Título + " sea " + this.OperadorMatemático.ToString().ToLower() + " a " + this.Valor.ObtenerValorString;
            this.Registro.Título = this.Script.Replace("'", "''");
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Reporte", TipoAtributo.Registro) { Colección = new Reportes(this.Solución) });

            this.Atributos.Add(new Atributo("Columna", TipoAtributo.Registro) { Colección = new Columnas(this.Solución) });

            this.Atributos.Add(new Atributo("OperadorMatemático", TipoAtributo.Enumeración));

            this.Valor.PrepararAtributos();

            this.Atributos.Add(new Atributo("Script", TipoAtributo.Edición) { Dependencia = "Columna;OperadorMatemático;Entero;Fecha;Importe;Texto;Registro" });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "OperadorMatemático")
            {
                Atributo.Literales.Add(new Elemento(OperadorMatemático.Igual.GetHashCode().ToString(), OperadorMatemático.Igual.ToString()));

                Atributo.Literales.Add(new Elemento(OperadorMatemático.Diferente.GetHashCode().ToString(), OperadorMatemático.Diferente.ToString()));

                Atributo.Literales.Add(new Elemento(OperadorMatemático.Mayor.GetHashCode().ToString(), OperadorMatemático.Mayor.ToString()));

                Atributo.Literales.Add(new Elemento(OperadorMatemático.Menor.GetHashCode().ToString(), OperadorMatemático.Menor.ToString()));

                Atributo.Literales.Add(new Elemento(OperadorMatemático.MayorIgual.GetHashCode().ToString(), OperadorMatemático.MayorIgual.ToString()));

                Atributo.Literales.Add(new Elemento(OperadorMatemático.MenorIgual.GetHashCode().ToString(), OperadorMatemático.MenorIgual.ToString()));
            }
        }

        #region SeleccionandoColección
        private void SeleccionarColumnas()
        {
            this.Columnas.TextoComando.Fuente = "SELECT * FROM Columnas WHERE Consulta = '" + this.Reporte.IdConsulta + "'";
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Columna")
                this.SeleccionarColumnas();
        }
        #endregion

        #region RestringiendoAtributo
        private void RestringirAtributoScript(Atributo Atributo)
        {
            this.Script = this.Columna.Nombre + " " + (this.OperadorMatemático == OperadorMatemático.Igual ? "=" : (this.OperadorMatemático == OperadorMatemático.Diferente ? "<>" : (this.OperadorMatemático == OperadorMatemático.Mayor ? ">" : (this.OperadorMatemático == OperadorMatemático.Menor ? "<" : (this.OperadorMatemático == OperadorMatemático.MayorIgual ? ">=" : (this.OperadorMatemático == OperadorMatemático.MenorIgual ? "<=" : string.Empty)))))) + " " + this.Valor.Script;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Script")
                this.RestringirAtributoScript(Atributo);
        }
        #endregion

        protected override void Abierto()
        {
            this.Reporte.Consulta.SincronizarMaestros();

            base.Abierto();
        }

        protected override void Cambiado(AcciónDeCambio AcciónDeCambio)
        {
            this.Reporte.ActualizarScript();

            base.Cambiado(AcciónDeCambio);
        }
        #endregion
    }

    public class Filtros : Colección
    {
        #region Constructores
        public Filtros(Entidad Solución) : base(Solución, new Filtro(Solución)) { }
        #endregion

        #region Métodos
        public Filtro Obtener(string Clave) { return (Filtro)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Filtro(this.Solución); }
        #endregion
    }
}
