
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Consulta : Entidad
    {
        #region Constructores
        public Consulta() : base() { }

        public Consulta(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        #region Fuente
        public Fuentes Fuentes { get { return (Fuentes)this.Atributos["Fuente"].Colección; } set { this.Atributos["Fuente"].Colección = value; } }

        public Fuente Fuente { get { return (Fuente)this.Fuentes.Obtener(); } }

        public string IdFuente { get { return (string)this.Atributos["Fuente"].Valor.Actual; } set { this.Atributos["Fuente"].Valor.Actual = value; } }
        #endregion

        public string Script { get { return (string)this.Atributos["Script"].Valor.Actual; } set { this.Atributos["Script"].Valor.Actual = value; } }

        public Columnas Columnas { get { return (Columnas)this.Atributos["Columnas"].Colección; } set { this.Atributos["Columnas"].Colección = value; } }
        #endregion

        #region Métodos
        public void SincronizarColumnas()
        {
            this.AccesoDatos.EjecutarComando(this.Solución, "DELETE Columnas WHERE Consulta = '" + this.Registro.Id + "'");

            this.Columnas.Cargar(TipoEstructura.Valores);

            Colección _Colección = new Colección(this.Solución, this.Fuente.CadenaDeConexión);

            _Colección.CargarDataTable(this.Script);

            #region Columns
            foreach (System.Data.DataColumn _DataColumn in _Colección.DataTable.Columns)
            {
                bool _Existe = false;

                foreach (Columna _Columna in this.Columnas)
                {
                    if (_Columna.Nombre == _DataColumn.ColumnName)
                    {
                        _Existe = true;

                        break;
                    }
                }

                if (!_Existe)
                {
                    Columna _Columna = (Columna)this.Columnas.ObtenerNuevoEntidad(TipoEstructura.Valores, this);

                    _Columna.IdConsulta = this.Registro.Id;

                    _Columna.Clave = _DataColumn.ColumnName;

                    _Columna.Nombre = _DataColumn.ColumnName;

                    if (_DataColumn.DataType == System.Type.GetType("System.Decimal"))
                        _Columna.TipoDeColumna = TipoDeColumna.Importe;

                    if (_DataColumn.DataType == System.Type.GetType("System.Int32"))
                        _Columna.TipoDeColumna = TipoDeColumna.Entero;

                    if (_DataColumn.DataType == System.Type.GetType("System.DateTime"))
                        _Columna.TipoDeColumna = TipoDeColumna.Fecha;

                    if (_DataColumn.DataType == System.Type.GetType("System.String"))
                        _Columna.TipoDeColumna = TipoDeColumna.Texto;

                    _Columna.IdMaestro = "-1";

                    _Columna.Guardar(GuardarCerrar.Sí);
                }
            }
            #endregion

            _Colección.Limpiar();
        }

        public void SincronizarMaestros()
        {
            Columnas _Columnas = new Columnas(this.Solución);

            _Columnas.TextoComando.Externo = "SELECT * FROM Columnas WHERE Consulta = '" + this.Registro.Id + "' AND TipoDeColumna = " + TipoDeColumna.Maestro.GetHashCode().ToString();

            _Columnas.Cargar(TipoEstructura.Valores);

            foreach (Columna _Columna in _Columnas)
            {
                Colección _Colección = new Colección(this.Solución, this.Fuente.CadenaDeConexión);

                _Colección.CargarDataTable("SELECT " + _Columna.Nombre + " FROM (" + this.Script + ") AS A GROUP BY " + _Columna.Nombre);

                foreach (System.Data.DataRow _DataRow in _Colección.DataTable.Rows)
                {
                    Registros _Registros = new Registros(this.Solución);

                    _Registros.TextoComando.Externo = "SELECT * FROM Registros WHERE Maestro = '" + _Columna.IdMaestro + "' AND Nombre = '" + _DataRow[_Columna.Nombre].ToString() + "'";

                    _Registros.Cargar(TipoEstructura.Básico);

                    if (_Registros.Count == 0)
                    {
                        Registro _Registro = (Registro)_Columna.Maestro.Registros.ObtenerNuevoEntidad(TipoEstructura.Valores, _Columna.Maestro);

                        _Registro.IdMaestro = _Columna.IdMaestro;

                        _Registro.Nombre = _DataRow[_Columna.Nombre].ToString();

                        _Registro.Guardar(GuardarCerrar.Sí);
                    }
                }
            }

            _Columnas.Limpiar();
        }

        public void ActualizarReportes()
        {
            Reportes _Reportes = new Reportes(this.Solución);

            _Reportes.TextoComando.Externo = "SELECT * FROM Reportes WHERE Consulta = '" + this.Registro.Id + "'";

            _Reportes.Cargar(TipoEstructura.Valores);

            foreach (Reporte _Reporte in _Reportes)
            {
                _Reporte.ActualizarScript();
            }
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Consulta(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Consulta";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Consultas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Columna(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Fuente", TipoAtributo.Registro) { Colección = new Fuentes(this.Solución) });

            this.Atributos.Add(new Atributo("Script", TipoAtributo.Edición));

            #region Comandos
            this.Atributos.Add(new Atributo("Comandos", TipoAtributo.Grupo));

            this.Atributos.Add(new Atributo("SincronizarColumnas", TipoAtributo.Comando));

            this.Atributos.Add(new Atributo("SincronizarMaestros", TipoAtributo.Comando));

            this.Atributos.Add(new Atributo("ActualizarReportes", TipoAtributo.Comando));
            #endregion

            #region Carpetas
            this.Atributos.Add(new Atributo("Carpetas", TipoAtributo.Grupo));

            this.Atributos.Add(new Atributo("Columnas", TipoAtributo.Carpeta) { Colección = new Columnas(this.Solución) });
            #endregion
        }

        #region EjecutandoComando
        private void EjecutarComandoSincronizarColumnas()
        {
            this.SincronizarColumnas();
        }

        private void EjecutarComandoSincronizarMaestros()
        {
            this.SincronizarMaestros();
        }

        private void EjecutarComandoActualizarReportes()
        {
            this.ActualizarReportes();
        }

        protected override void EjecutandoComando(Atributo Comando)
        {
            base.EjecutandoComando(Comando);

            if (Comando.Identidad.Clave == "SincronizarColumnas")
                this.EjecutarComandoSincronizarColumnas();

            if (Comando.Identidad.Clave == "SincronizarMaestros")
                this.EjecutarComandoSincronizarMaestros();

            if (Comando.Identidad.Clave == "ActualizarReportes")
                this.EjecutarComandoActualizarReportes();
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

    public class Consultas : Colección
    {
        #region Constructores
        public Consultas(Entidad Solución) : base(Solución, new Consulta(Solución)) { }
        #endregion

        #region Métodos
        public Consulta Obtener(string Script) { return (Consulta)this.ObtenerX(Script); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Consulta(this.Solución); }
        #endregion
    }
}
