
using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Reporte : Entidad
    {
        #region Constructores
        public Reporte() : base() { }

        public Reporte(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)this.Atributos["Nombre"].Valor.Actual; } set { this.Atributos["Nombre"].Valor.Actual = value; } }

        #region Fuente
        public Fuentes Fuentes { get { return (Fuentes)this.Atributos["Fuente"].Colección; } set { this.Atributos["Fuente"].Colección = value; } }

        public Fuente Fuente { get { return (Fuente)this.Fuentes.Obtener(); } }

        public string IdFuente { get { return (string)this.Atributos["Fuente"].Valor.Actual; } set { this.Atributos["Fuente"].Valor.Actual = value; } }
        #endregion

        #region Consulta
        public Consultas Consultas { get { return (Consultas)this.Atributos["Consulta"].Colección; } set { this.Atributos["Consulta"].Colección = value; } }

        public Consulta Consulta { get { return (Consulta)this.Consultas.Obtener(); } }

        public string IdConsulta { get { return (string)this.Atributos["Consulta"].Valor.Actual; } set { this.Atributos["Consulta"].Valor.Actual = value; } }
        #endregion

        public string Script { get { return (string)this.Atributos["Script"].Valor.Actual; } set { this.Atributos["Script"].Valor.Actual = value; } }

        public Filtros Filtros { get { return (Filtros)this.Atributos["Filtros"].Colección; } set { this.Atributos["Filtros"].Colección = value; } }

        public Colección Resultados { get { return (Colección)this.Atributos["Resultados"].Colección; } set { this.Atributos["Resultados"].Colección = value; } }

        public Representaciones Representaciones { get { return (Representaciones)this.Atributos["Representaciones"].Colección; } set { this.Atributos["Representaciones"].Colección = value; } }
        #endregion

        #region Métodos
        private void ActualizarRepresentaciones()
        {
            this.Representaciones.Cargar(TipoEstructura.Valores);

            foreach (Representación _Representación in this.Representaciones)
                _Representación.ActualizarScript();
        }

        public void ActualizarScript()
        {
            bool _Abierto = this.Abrir(TipoEstructura.Valores);

            this.RestringirScript();

            this.Guardar(GuardarCerrar.Sí, _Abierto);

            this.ActualizarRepresentaciones();
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Reporte(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            this.TipoEntidad = TipoEntidad.Registro;

            this.Identidad.Clave = "Reporte";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Reportes";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            this.AccesosDirectos.Agregar(new Filtro(this.Solución));

            this.AccesosDirectos.Agregar(new Representación(this.Solución));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            this.Atributos.Add(new Atributo("Fuente", TipoAtributo.Registro) { Colección = new Fuentes(this.Solución) });

            this.Atributos.Add(new Atributo("Consulta", TipoAtributo.Registro) { Colección = new Consultas(this.Solución), Dependencia = "Fuente" });

            this.Atributos.Add(new Atributo("Script", TipoAtributo.Edición) { Dependencia = "Consulta" });

            #region Carpetas
            this.Atributos.Add(new Atributo("Carpetas", TipoAtributo.Grupo));

            this.Atributos.Add(new Atributo("Filtros", TipoAtributo.Carpeta) { Colección = new Filtros(this.Solución) });

            this.Atributos.Add(new Atributo("Resultados", TipoAtributo.Carpeta) { Colección = new Colección(this.Solución) { TipoColección = TipoColección.DataTable } });

            this.Atributos.Add(new Atributo("Representaciones", TipoAtributo.Carpeta) { Colección = new Representaciones(this.Solución) });
            #endregion
        }

        protected override void Abierto()
        {
            this.Atributos["Resultados"].Colección.CadenaDeConexión = this.Consulta.Fuente.CadenaDeConexión;

            base.Abierto();
        }

        protected override void ColecciónPreparada(Atributo Atributo)
        {
            //if (Atributo.Identidad.Clave == "Resultados")
            //    Atributo.Colección.CadenaDeConexión = this.Consulta.Fuente.CadenaDeConexión;

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

        private void RestringirScript()
        {
            this.Script = string.Empty;

            this.Filtros.Cargar(TipoEstructura.Valores);

            string _IdColumna = null;

            foreach (Filtro _Filtro in this.Filtros)
            {
                if (this.Script != string.Empty)
                {
                    if (_Filtro.IdColumna == _IdColumna)
                        this.Script += " OR ";

                    else this.Script += " AND ";
                }

                this.Script += _Filtro.Script;

                _IdColumna = _Filtro.IdColumna;
            }

            if (this.Script == string.Empty)
                this.Script = this.Consulta.Script;

            else this.Script = "SELECT * FROM (" + this.Consulta.Script + ") AS A WHERE " + this.Script;

            this.ActualizarRepresentaciones();
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Script")
                this.RestringirScript();
        }

        protected override bool Guardando()
        {
            if (this.Nombre == string.Empty)
                this.Nombre = "(sin nombre)";

            return base.Guardando();
        }
        #endregion
    }

    public class Reportes : Colección
    {
        #region Constructores
        public Reportes(Entidad Solución) : base(Solución, new Reporte(Solución)) { }
        #endregion

        #region Métodos
        public Reporte Obtener(string Script) { return (Reporte)this.ObtenerX(Script); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Reporte(this.Solución); }
        #endregion
    }
}
