
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public enum QuitarResultadosConIndicadoresIgualACeros { Ninguno, No, Sí }

    public class Representación : Entidad
    {
        #region Constructores
        public Representación() : base() { }

        public Representación(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Reporte
        public Reportes Reportes { get { return (Reportes)this.Atributos["Reporte"].Colección; } set { this.Atributos["Reporte"].Colección = value; } }

        public Reporte Reporte { get { return (Reporte)this.Reportes.Obtener(); } }

        public string IdReporte { get { return (string)this.Atributos["Reporte"].Valor.Actual; } set { this.Atributos["Reporte"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        public int Cima { get { return (int)this.Atributos["Cima"].Valor.Actual; } set { this.Atributos["Cima"].Valor.Actual = value; } }

        public string Script { get { return (string)this.Atributos["Script"].Valor.Actual; } set { this.Atributos["Script"].Valor.Actual = value; } }

        public int CantidadDeIndicadores { get { return (int)this.Atributos["CantidadDeIndicadores"].Valor.Actual; } set { this.Atributos["CantidadDeIndicadores"].Valor.Actual = value; } }

        public QuitarResultadosConIndicadoresIgualACeros QuitarResultadosConIndicadoresIgualACeros { get { return (QuitarResultadosConIndicadoresIgualACeros)this.Atributos["QuitarResultadosConIndicadoresIgualACeros"].Valor.Actual; } set { this.Atributos["QuitarResultadosConIndicadoresIgualACeros"].Valor.Actual = (int)value; } }

        public Agrupaciones Agrupaciones { get { return (Agrupaciones)this.Atributos["Agrupaciones"].Colección; } set { this.Atributos["Agrupaciones"].Colección = value; } }

        public Indicadores Indicadores { get { return (Indicadores)this.Atributos["Indicadores"].Colección; } set { this.Atributos["Indicadores"].Colección = value; } }

        public Colección Resultados { get { return (Colección)this.Atributos["Resultados"].Colección; } set { this.Atributos["Resultados"].Colección = value; } }
        #endregion

        #region Métodos
        public void ActualizarScript()
        {
            bool _Abierto = this.Abrir(TipoEstructura.Valores);

            this.Script = string.Empty;

            #region Indicadores
            string _Cálculos = string.Empty;

            this.Indicadores.Cargar(TipoEstructura.Valores);

            foreach (Indicador _Indicador in this.Indicadores)
            {
                if (_Cálculos != string.Empty)
                    _Cálculos += ", ";

                //_Cálculos += (_Indicador.TipoDeIndicador == TipoDeIndicador.Conteo ? "COUNT(" : (_Indicador.TipoDeIndicador == TipoDeIndicador.Suma ? "SUM(" : string.Empty)) + _Indicador.Columna.Nombre + ") AS " + _Indicador.Script;
                _Cálculos += (_Indicador.TipoDeIndicador == TipoDeIndicador.Conteo ? "COUNT(" : (_Indicador.TipoDeIndicador == TipoDeIndicador.Suma ? "SUM(" : string.Empty)) + _Indicador.Columna.Clave + ") AS " + _Indicador.Columna.Clave;
            }
            #endregion

            #region Agrupaciones
            string _Agrupaciones = string.Empty;

            this.Agrupaciones.Cargar(TipoEstructura.Valores);

            foreach (Agrupación _Agrupación in this.Agrupaciones)
            {
                if (_Agrupaciones != string.Empty)
                    _Agrupaciones += ", ";

                //_Agrupaciones += _Agrupación.Columna.Nombre;
                _Agrupaciones += _Agrupación.Columna.Clave;
            }

            if (_Agrupaciones == string.Empty || _Cálculos == string.Empty)
            {
                if (this.Cima == 0)
                    this.Script = this.Reporte.Script;

                else this.Script = "SELECT TOP " + this.Cima.ToString() + " (" + this.Reporte.Script + ") AS A";
            }

            else this.Script = "SELECT " + (this.Cima == 0 ? string.Empty : "TOP " + this.Cima.ToString()) + " " + _Agrupaciones + ", " + _Cálculos + " FROM (" + this.Reporte.Script + ") AS A GROUP BY " + _Agrupaciones;
            #endregion

            #region Órdenes
            string _Órdenes = string.Empty;

            foreach (Indicador _Indicador in this.Indicadores)
            {
                if (_Indicador.OrdenDeIndicador != OrdenDeIndicador.Ninguno)
                {
                    if (_Órdenes != string.Empty)
                        _Órdenes += ", ";

                    //_Órdenes += _Indicador.Script + (_Indicador.OrdenDeIndicador == OrdenDeIndicador.Descendente ? " DESC" : string.Empty);
                    _Órdenes += _Indicador.Columna.Clave + (_Indicador.OrdenDeIndicador == OrdenDeIndicador.Descendente ? " DESC" : string.Empty);
                }
            }

            if (_Órdenes != string.Empty)
                this.Script += " ORDER BY " + _Órdenes;
            #endregion

            #region QuitarResultadosConIndicadoresIgualACeros
            if (this.QuitarResultadosConIndicadoresIgualACeros == QuitarResultadosConIndicadoresIgualACeros.Sí)
            {
                string _IndicadoresDiferenteACero = string.Empty;

                foreach (Indicador _Indicador in this.Indicadores)
                {
                    if (_IndicadoresDiferenteACero != string.Empty)
                        _IndicadoresDiferenteACero += " AND ";

                    //_IndicadoresDiferenteACero += _Indicador.Script + " <> 0";
                    _IndicadoresDiferenteACero += _Indicador.Columna.Clave + " <> 0";
                }

                if (_IndicadoresDiferenteACero != string.Empty)
                    this.Script = "SELECT * FROM (" + this.Script + ") AS B WHERE " + _IndicadoresDiferenteACero;
            }
            #endregion

            this.CantidadDeIndicadores = this.Indicadores.Count;

            this.Guardar(GuardarCerrar.Sí, _Abierto);
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Representación(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Representación";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Representaciones";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Agrupación(this.Solución));

            this.AccesosDirectos.Agregar(new Indicador(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Reporte", TipoAtributo.Registro) { Colección = new Reportes(this.Solución) });

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Cima", TipoAtributo.Entero));

            this.Atributos.Add(new Atributo("ActualizarScriptConCima", TipoAtributo.Comando));

            this.Atributos.Add(new Atributo("Script", TipoAtributo.Edición) { /*Dependencia = "Cima"*/ });

            this.Atributos.Add(new Atributo("CantidadDeIndicadores", TipoAtributo.Entero));

            this.Atributos.Add(new Atributo("QuitarResultadosConIndicadoresIgualACeros", TipoAtributo.Enumeración));

            this.Atributos.Add(new Atributo("ActualizarScript", TipoAtributo.Comando));

            this.Atributos.Add(new Atributo("Agrupaciones", TipoAtributo.Carpeta) { Colección = new Agrupaciones(this.Solución) });

            this.Atributos.Add(new Atributo("Indicadores", TipoAtributo.Carpeta) { Colección = new Indicadores(this.Solución) });

            this.Atributos.Add(new Atributo("Resultados", TipoAtributo.Carpeta) { Colección = new Colección(this.Solución) { TipoColección = TipoColección.DataTable } });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "QuitarResultadosConIndicadoresIgualACeros")
            {
                Atributo.Literales.Add(new Elemento(QuitarResultadosConIndicadoresIgualACeros.No.GetHashCode().ToString(), QuitarResultadosConIndicadoresIgualACeros.No.ToString()));

                Atributo.Literales.Add(new Elemento(QuitarResultadosConIndicadoresIgualACeros.Sí.GetHashCode().ToString(), QuitarResultadosConIndicadoresIgualACeros.Sí.ToString()));
            }
        }

        #region EjecutandoComando
        private void EjecutarComandoActualizarScriptConCima()
        {
            this.ActualizarScript();
        }

        private void EjecutarComandoActualizarScript()
        {
            this.ActualizarScript();
        }

        protected override void EjecutandoComando(Atributo Comando)
        {
            base.EjecutandoComando(Comando);

            if (Comando.Identidad.Clave == "ActualizarScriptConCima")
                this.EjecutarComandoActualizarScriptConCima();

            if (Comando.Identidad.Clave == "ActualizarScript")
                this.EjecutarComandoActualizarScript();
        }
        #endregion

        #region RestringiendoAtributo
        //private void RestringirScript()
        //{
        //    this.ActualizarScript();
        //}

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            //if (Atributo.Identidad.Clave == "Script")
            //    this.RestringirScript();
        }
        #endregion

        protected override void Abierto()
        {
            this.Atributos["Resultados"].Colección.CadenaDeConexión = this.Reporte.Consulta.Fuente.CadenaDeConexión;

            base.Abierto();
        }

        protected override void ColecciónPreparada(Atributo Atributo)
        {
            //if (Atributo.Identidad.Clave == "Resultados")
            //    Atributo.Colección.CadenaDeConexión = this.Reporte.Consulta.Fuente.CadenaDeConexión;

            base.ColecciónPreparada(Atributo);
        }

        #region SeleccionandoColección
        private void SeleccionarResultados()
        {
            this.Resultados.TextoComando.Fuente = this.Script;
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Resultados")
                this.SeleccionarResultados();
        }
        #endregion

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }

        //protected override void Cambiado(AcciónDeCambio AcciónDeCambio)
        //{
        //    this.ActualizarScript();

        //    base.Cambiado(AcciónDeCambio);
        //}
        #endregion
    }

    public class Representaciones : Colección
    {
        #region Constructores
        public Representaciones(Entidad Solución) : base(Solución, new Representación(Solución)) { }
        #endregion

        #region Métodos
        public Representación Obtener(string Clave) { return (Representación)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Representación(this.Solución); }
        #endregion
    }
}
