
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class TableroDeMando : Entidad
    {
        #region Constructores
        public TableroDeMando() : base() { }

        public TableroDeMando(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Tablero
        public Tableros Tableros { get { return (Tableros)this.Atributos["Tablero"].Colección; } set { this.Atributos["Tablero"].Colección = value; } }

        public Tablero Tablero { get { return (Tablero)this.Tableros.Obtener(); } }

        public string IdTablero { get { return (string)this.Atributos["Tablero"].Valor.Actual; } set { this.Atributos["Tablero"].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Herramienta;

            #region Identidad
            this.Identidad.Clave = "TableroDeMando";

            this.Identidad.Nombre = "Tablero de mando";
            #endregion
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Tablero", TipoAtributo.Registro) { Colección = new Tableros(this.Solución) });

            this.Atributos.Add(new Atributo("IndicadoresDinámicos", TipoAtributo.Grupo) { Dependencia = "Tablero" });
        }

        private Tarjetas ObtenerTarjetas()
        {
            Tarjetas _Tarjetas = new Tarjetas(this.Solución);

            _Tarjetas.TextoComando.Externo = "SELECT * FROM Tarjetas WHERE Grupo IN (SELECT Id FROM Grupos WHERE Tablero = '" + this.IdTablero + "') ORDER BY Título DESC";

            _Tarjetas.Cargar(TipoEstructura.Valores);

            return _Tarjetas;
        }

        #region SeleccionandoColección
        private void SeleccionarTableros()
        {
            this.Tableros.TextoComando.Fuente = "SELECT * FROM Tableros WHERE Id = '65c16a5c-efd2-4a67-a1d9-6ace46c2853b'";
        }

        private void SeleccionarCarpetaDinámica(Atributo Atributo)
        {
            foreach (Tarjeta _Tarjeta in this.ObtenerTarjetas())
            {
                if (_Tarjeta.Registro.Id == Atributo.Identidad.Clave)
                {
                    Atributo.Colección.TextoComando.Fuente = _Tarjeta.Representación.Script;

                    break;
                }
            }
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Grupo.Identidad.Clave == "Tablero")
                this.SeleccionarTableros();

            if (Atributo.Grupo.Identidad.Clave == "IndicadoresDinámicos")
                this.SeleccionarCarpetaDinámica(Atributo);
        }
        #endregion

        #region ColecciónCargada
        private void ResultadosCargados(Atributo Atributo)
        {
            foreach (Tarjeta _Tarjeta in this.ObtenerTarjetas())
            {
                if (_Tarjeta.Registro.Id == Atributo.Identidad.Clave)
                {
                    _Tarjeta.Representación.Agrupaciones.Cargar(TipoEstructura.Valores);

                    foreach (Agrupación _Agrupación in _Tarjeta.Representación.Agrupaciones)
                    {
                        Atributo.Colección.DataTable.Columns[_Agrupación.Columna.Clave].Caption = _Agrupación.Columna.Nombre;
                    }

                    _Tarjeta.Representación.Indicadores.Cargar(TipoEstructura.Valores);

                    foreach (Indicador _Indicador in _Tarjeta.Representación.Indicadores)
                    {
                        Atributo.Colección.DataTable.Columns[_Indicador.Columna.Clave].Caption = _Indicador.Columna.Nombre;
                    }

                    break;
                }
            }
        }

        protected override void ColecciónCargada(Atributo Atributo)
        {
            //if (Atributo.Identidad.Clave == "Resultados")
                this.ResultadosCargados(Atributo);

            base.ColecciónCargada(Atributo);
        }
        #endregion

        #region PreparandoAtributosDinámicos
        private void QuitarIndicadoresDinámicos()
        {
            this.Dashboard.Limpiar();

            Atributos _Atributos = new Atributos();

            this.CopiarAtributos(this.Atributos["IndicadoresDinámicos"].Atributos, _Atributos);

            foreach (Atributo _Atributo in _Atributos)
            {
                this.QuitarRestricciones(_Atributo);

                _Atributo.Grupo.Atributos.Remove(_Atributo);

                _Atributo.Sección.Atributos.Remove(_Atributo);

                _Atributo.Sección.Carpetas.Remove(_Atributo);

                this.Atributos.Remove(_Atributo);
            }
        }

        private void AgregarIndicadoresDinámicos()
        {
            Tarjetas _Tarjetas = this.ObtenerTarjetas();

            foreach (Tarjeta _Tarjeta in _Tarjetas)
            {
                Atributo _Atributo = new Atributo(_Tarjeta.Registro.Id, _Tarjeta.Registro.Título, TipoAtributo.Carpeta) { Colección = new Colección(this.Solución) { TipoColección = TipoColección.DataTable }, Entidad = this };

                _Atributo.Grupo = this.Atributos["IndicadoresDinámicos"];

                _Atributo.Sección = this.Atributos["IndicadoresDinámicos"].Sección;

                this.Atributos.Insert(this.Atributos.IndexOf(this.Atributos["IndicadoresDinámicos"]) + 1, _Atributo);

                _Atributo.Grupo.Atributos.Add(_Atributo);

                _Atributo.Sección.Atributos.Add(_Atributo);

                _Atributo.PrepararEstructura();

                _Atributo.Colección.Atributo = _Atributo;

                _Atributo.Colección.CadenaDeConexión = _Tarjeta.Reporte.Consulta.Fuente.CadenaDeConexión;
            }

            this.PrepararEstructuraSeccionesGrupos();

            this.ActualizarCarpetasVisibles();
        }

        private void PrepararDashboard()
        {
            this.Tablero.Grupos.Cargar(TipoEstructura.Valores);

            foreach (Grupo _Grupo in this.Tablero.Grupos)
            {
                Panel _Panel = new Panel(_Grupo.Registro.Id, _Grupo.Nombre);

                _Grupo.Tarjetas.Cargar(TipoEstructura.Valores);

                foreach (Dashboard.Tarjeta _Tarjeta in _Grupo.Tarjetas)
                {
                    Framework.Tarjeta _TarjetaDePanel = new Framework.Tarjeta(_Tarjeta.Registro.Id, _Tarjeta.Registro.Título, _Tarjeta.Reporte.Registro.Título);

                    //_Tarjeta.Atributo = _Indicador.Atributos["Resultados"];
                    _TarjetaDePanel.Atributo = this.Atributos[_Tarjeta.Registro.Id];

                    _TarjetaDePanel.Indicadores = _Tarjeta.Representación.CantidadDeIndicadores;

                    _TarjetaDePanel.TipoDeTarjeta = (_Tarjeta.TipoDeTarjeta == TipoDeTarjeta.Gráfico ? TipoDeTarjeta.Gráfico : (_Tarjeta.TipoDeTarjeta == TipoDeTarjeta.Tabla ? TipoDeTarjeta.Tabla : TipoDeTarjeta.Ninguno));

                    _TarjetaDePanel.TipoDeGráfico = _Tarjeta.TipoDeGráfico;

                    _Panel.Tarjetas.Add(_TarjetaDePanel);
                }

                if (!this.Dashboard.Paneles.Contains(_Panel))
                    this.Dashboard.Paneles.Add(_Panel);
            }
        }

        private void PrepararIndicadoresDinámicos()
        {
            this.QuitarIndicadoresDinámicos();

            if (this.Tablero.TipoEntidad == TipoEntidad.Registro)
            {
                this.AgregarIndicadoresDinámicos();

                this.PrepararDashboard();
            }

            this.ActualizarPresentación = true;
        }

        protected override void PreparandoAtributosDinámicos()
        {
            base.PreparandoAtributosDinámicos();

            //this.PrepararIndicadoresDinámicos();
        }
        #endregion

        #region RestringiendoAtributo
        private void RestringiendoIndicadoresDinámicos(Atributo Atributo)
        {
            if (this.Atributos["Tablero"].Valor.EsDiferenteEntreActualConOriginal)
                this.PrepararIndicadoresDinámicos();

            //if (this.Carpetas.Count > 0)
            //    Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            //else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "IndicadoresDinámicos")
                this.RestringiendoIndicadoresDinámicos(Atributo);
        }
        #endregion
        #endregion
    }
}
