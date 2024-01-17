
using System;

using Aplimática.Framework;

namespace Aplimática.Finnerve.Dashboard
{
    public class Valor : Extensión
    {
        #region Constructores
        #endregion

        #region Propiedades
        public new TipoAtributo TipoAtributo = TipoAtributo.Ninguno;

        public Filtro Filtro { get { return (Filtro)this.Entidad; } }

        public int Entero { get { return (int)this.Entidad.Atributos["Entero"].Valor.Actual; } set { this.Entidad.Atributos["Entero"].Valor.Actual = value; } }

        public DateTime Fecha { get { return (DateTime)this.Entidad.Atributos["Fecha"].Valor.Actual; } set { this.Entidad.Atributos["Fecha"].Valor.Actual = value; } }

        public decimal Importe { get { return (decimal)this.Entidad.Atributos["Importe"].Valor.Actual; } set { this.Entidad.Atributos["Importe"].Valor.Actual = value; } }

        #region Registro
        public Registros Registros { get { return (Registros)this.Entidad.Atributos["Registro"].Colección; } set { this.Entidad.Atributos["Registro"].Colección = value; } }

        public Registro Registro { get { return (Registro)this.Registros.Obtener(); } }

        public string IdRegistro { get { return (string)this.Entidad.Atributos["Registro"].Valor.Actual; } set { this.Entidad.Atributos["Registro"].Valor.Actual = value; } }
        #endregion

        public string Texto { get { return (string)this.Entidad.Atributos["Texto"].Valor.Actual; } set { this.Entidad.Atributos["Texto"].Valor.Actual = value; } }

        public string ObtenerValorString
        {
            get
            {
                return (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Entero ? this.Entero.ToString() : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Fecha ? this.Fecha.ToString("dd/MM/yyyy") : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Importe ? this.Importe.ToString() : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Maestro ? this.Registro.Registro.Título : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Texto ? this.Texto : string.Empty)))));
            }
        }

        public string Script
        {
            get
            {
                return (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Entero ? this.Entero.ToString() : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Fecha ? this.Fecha.ToString("'yyyyMMdd'") : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Importe ? this.Importe.ToString() : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Maestro ? "'" + this.Registro.Nombre + "'" : (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Texto ? "'" + this.Texto + "'" : string.Empty)))));
            }
        }
        #endregion

        #region Métodos
        public void EstablecerValor(object Valor)
        {
            if (Valor.GetType() != Type.GetType("System.DBNull")) //System.DBNull
            {
                if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Entero)
                    this.Entero = (int)Valor;

                if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Fecha)
                    this.Fecha = (System.DateTime)Valor;

                if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Importe)
                    this.Importe = (decimal)Valor;

                if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Maestro)
                    this.IdRegistro = (string)Valor;

                if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Texto)
                    this.Texto = (string)Valor;
            }
        }
        #endregion

        #region Eventos
        protected override void Identificando()
        {
            base.Identificando();

            this.TipoAtributo = TipoAtributo.Ninguno;

            this.Identidad.Clave = "Valor";
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            this.Entidad.Atributos.Add(new Atributo("Entero", TipoAtributo.Entero) { Dependencia = "Columna" });

            this.Entidad.Atributos.Add(new Atributo("Fecha", TipoAtributo.Fecha) { Dependencia = "Columna" });

            this.Entidad.Atributos.Add(new Atributo("Importe", TipoAtributo.Importe) { Dependencia = "Columna" });

            this.Entidad.Atributos.Add(new Atributo("Texto", TipoAtributo.Texto) { Dependencia = "Columna" });

            this.Entidad.Atributos.Add(new Atributo("Registro", TipoAtributo.Registro) { Colección = new Registros(this.Entidad.Solución), Dependencia = "Columna" });
        }

        #region SeleccionandoColección
        private void SeleccionarRegistros()
        {
            this.Registros.TextoComando.Fuente = "SELECT * FROM Registros WHERE Maestro = '" + this.Filtro.Columna.IdMaestro + "'";
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Registro")
                this.SeleccionarRegistros();
        }
        #endregion

        #region RestringiendoAtributo
        private void RestringirTexto(Atributo Atributo)
        {
            if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Texto)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirFecha(Atributo Atributo)
        {
            if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Fecha)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirRegistro(Atributo Atributo)
        {
            if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Maestro)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirEntero(Atributo Atributo)
        {
            if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Entero)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirImporte(Atributo Atributo)
        {
            if (this.Filtro.Columna.TipoDeColumna == TipoDeColumna.Importe)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;

            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Texto")
                this.RestringirTexto(Atributo);

            if (Atributo.Identidad.Clave == "Fecha")
                this.RestringirFecha(Atributo);

            if (Atributo.Identidad.Clave == "Registro")
                this.RestringirRegistro(Atributo);

            if (Atributo.Identidad.Clave == "Entero")
                this.RestringirEntero(Atributo);

            if (Atributo.Identidad.Clave == "Importe")
                this.RestringirImporte(Atributo);
        }
        #endregion
        #endregion
    }
}
