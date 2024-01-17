
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    //public enum TipoDeTarjeta { Ninguno, Gráfico, Tabla }

    public class Tarjeta : Entidad
    {
        #region Constructores
        public Tarjeta() : base() { }

        public Tarjeta(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Tablero
        public Tableros Tableros { get { return (Tableros)this.Atributos["Tablero"].Colección; } set { this.Atributos["Tablero"].Colección = value; } }

        public Tablero Tablero { get { return (Tablero)this.Tableros.Obtener(); } }

        public string IdTablero { get { return (string)this.Atributos["Tablero"].Valor.Actual; } set { this.Atributos["Tablero"].Valor.Actual = value; } }
        #endregion

        #region Grupo
        public new Grupos Grupos { get { return (Grupos)this.Atributos["Grupo"].Colección; } set { this.Atributos["Grupo"].Colección = value; } }

        public Grupo Grupo { get { return (Grupo)this.Grupos.Obtener(); } }

        public string IdGrupo { get { return (string)this.Atributos["Grupo"].Valor.Actual; } set { this.Atributos["Grupo"].Valor.Actual = value; } }
        #endregion

        #region Reporte
        public Reportes Reportes { get { return (Reportes)this.Atributos["Reporte"].Colección; } set { this.Atributos["Reporte"].Colección = value; } }

        public Reporte Reporte { get { return (Reporte)this.Reportes.Obtener(); } }

        public string IdReporte { get { return (string)this.Atributos["Reporte"].Valor.Actual; } set { this.Atributos["Reporte"].Valor.Actual = value; } }
        #endregion

        #region Representación
        public Representaciones Representaciones { get { return (Representaciones)this.Atributos["Representación"].Colección; } set { this.Atributos["Representación"].Colección = value; } }

        public Representación Representación { get { return (Representación)this.Representaciones.Obtener(); } }

        public string IdRepresentación { get { return (string)this.Atributos["Representación"].Valor.Actual; } set { this.Atributos["Representación"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public TipoDeTarjeta TipoDeTarjeta { get { return (TipoDeTarjeta)this.Atributos["TipoDeTarjeta"].Valor.Actual; } set { this.Atributos["TipoDeTarjeta"].Valor.Actual = (int)value; } }

        public TipoDeGráfico TipoDeGráfico { get { return (TipoDeGráfico)this.Atributos["TipoDeGráfico"].Valor.Actual; } set { this.Atributos["TipoDeGráfico"].Valor.Actual = (int)value; } }

        public Colección Resultados { get { return (Colección)this.Atributos["Resultados"].Colección; } set { this.Atributos["Resultados"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Tarjeta(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Tarjeta";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Tarjetas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Tablero", TipoAtributo.Registro) { Colección = new Tableros(this.Solución) });

            this.Atributos.Add(new Atributo("Grupo", TipoAtributo.Registro) { Colección = new Grupos(this.Solución), Dependencia = "Tablero" });

            this.Atributos.Add(new Atributo("Reporte", TipoAtributo.Registro) { Colección = new Reportes(this.Solución) });

            this.Atributos.Add(new Atributo("Representación", TipoAtributo.Registro) { Colección = new Representaciones(this.Solución), Dependencia = "Reporte" });

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("TipoDeTarjeta", TipoAtributo.Enumeración));

            this.Atributos.Add(new Atributo("TipoDeGráfico", TipoAtributo.Enumeración) { Dependencia = "TipoDeTarjeta" });

            this.Atributos.Add(new Atributo("Resultados", TipoAtributo.Carpeta) { Colección = new Colección(this.Solución) { TipoColección = TipoColección.DataTable }, Dependencia = "Representación" });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoDeTarjeta")
            {
                Atributo.Literales.Add(new Elemento(TipoDeTarjeta.Gráfico.GetHashCode().ToString(), TipoDeTarjeta.Gráfico.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeTarjeta.Tabla.GetHashCode().ToString(), TipoDeTarjeta.Tabla.ToString()));
            }

            if (Atributo.Identidad.Clave == "TipoDeGráfico")
            {
                Atributo.Literales.Add(new Elemento(TipoDeGráfico.Automático.GetHashCode().ToString(), TipoDeGráfico.Automático.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeGráfico.Área.GetHashCode().ToString(), TipoDeGráfico.Área.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeGráfico.Barras.GetHashCode().ToString(), TipoDeGráfico.Barras.ToString()));
            }
        }

        #region RestringiendoAtributo
        private void RestringirTipoDeGráfico(Atributo Atributo)
        {
            if (this.TipoDeTarjeta == TipoDeTarjeta.Gráfico)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "TipoDeGráfico")
                this.RestringirTipoDeGráfico(Atributo);
        }
        #endregion

        protected override void Abierto()
        {
            this.Atributos["Resultados"].Colección.CadenaDeConexión = this.Reporte.Consulta.Fuente.CadenaDeConexión;

            base.Abierto();
        }

        #region SeleccionandoColección
        private void SeleccionarGrupos()
        {
            this.Grupos.TextoComando.Fuente = "SELECT * FROM Grupos WHERE Tablero = '" + this.IdTablero + "'";
        }

        private void SeleccionarResultados()
        {
            this.Resultados.TextoComando.Fuente = this.Representación.Script;
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Grupo")
                this.SeleccionarGrupos();

            if (Atributo.Identidad.Clave == "Resultados")
                this.SeleccionarResultados();
        }
        #endregion

        #region ColecciónCargada
        private void ResultadosCargados()
        {
            this.Representación.Agrupaciones.Cargar(TipoEstructura.Valores);

            foreach (Agrupación _Agrupación in this.Representación.Agrupaciones)
            {
                this.Resultados.DataTable.Columns[_Agrupación.Columna.Clave].Caption = _Agrupación.Columna.Nombre;
            }

            this.Representación.Indicadores.Cargar(TipoEstructura.Valores);

            foreach (Indicador _Indicador in this.Representación.Indicadores)
            {
                this.Resultados.DataTable.Columns[_Indicador.Columna.Clave].Caption = _Indicador.Columna.Nombre;
            }
        }

        protected override void ColecciónCargada(Atributo Atributo)
        {
            if (Atributo.Identidad.Clave == "Resultados")
                this.ResultadosCargados();

            base.ColecciónCargada(Atributo);
        }
        #endregion

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Tarjetas : Colección
    {
        #region Constructores
        public Tarjetas(Entidad Solución) : base(Solución, new Tarjeta(Solución)) { }
        #endregion

        #region Métodos
        public Tarjeta Obtener(string Clave) { return (Tarjeta)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Tarjeta(this.Solución); }
        #endregion
    }
}
