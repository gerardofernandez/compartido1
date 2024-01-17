
using Aplimática.Framework;

namespace Aplimática.Global
{
    public class Correlativo : Entidad
    {
        #region Constructores
        public Correlativo() : base() { }

        public Correlativo(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public string Código { get { return (string)base.Atributos["Código"].Valor.Actual; } set { base.Atributos["Código"].Valor.Actual = value; } }

        public int Número { get { return (int)base.Atributos["Número"].Valor.Actual; } set { base.Atributos["Número"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        public int Generar()
        {
            bool _Abierto = this.Abrir(TipoEstructura.Valores);

            this.Número++;

            this.Guardar(GuardarCerrar.Sí, _Abierto);

            return this.Número;
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Correlativo(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Correlativo";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Correlativos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Código", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Número", TipoAtributo.Entero));
        }
        #endregion
    }

    public class Correlativos : Colección
    {
        #region Contructor
        public Correlativos(Entidad Solución) : base(Solución, new Correlativo(Solución)) { }
        #endregion

        #region Métodos
        public Correlativo Obtener(string Clave) { return (Correlativo)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Correlativo(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Diccionario
{
    public class Opción : Entidad
    {
        #region Constructores
        public Opción() : base() { }

        public Opción(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Dato
        public Datos Datos { get { return (Datos)base.Atributos["Dato"].Colección; } set { base.Atributos["Dato"].Colección = value; } }

        public Dato Dato { get { return (Dato)this.Datos.Obtener(); } }

        public string IdDato { get { return (string)base.Atributos["Dato"].Valor.Actual; } set { base.Atributos["Dato"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Opción(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Opción";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Opciones";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Dato", TipoAtributo.Registro) { Colección = new Datos(this.Solución) });

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));
        }
        #endregion
    }

    public class Opciones : Colección
    {
        #region Constructores
        public Opciones(Entidad Solución) : base(Solución, new Opción(Solución)) { }
        #endregion

        #region Métodos
        public Opción Obtener(string Clave) { return (Opción)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Opción(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Diccionario
{
    public enum TipoDato { Ninguno, Texto, Opción, Fecha }

    public class Dato : Entidad
    {
        #region Constructores
        public Dato() : base() { }

        public Dato(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public TipoDato TipoDato { get { return (TipoDato)base.Atributos["TipoDato"].Valor.Actual; } set { base.Atributos["TipoDato"].Valor.Actual = (int)value; } }

        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public Opciones Opciones { get { return (Opciones)base.Atributos["Opciones"].Colección; } set { base.Atributos["Opciones"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Dato(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Dato";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Datos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Opción());
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("TipoDato", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Opciones", TipoAtributo.Carpeta) { Colección = new Opciones(this.Solución) });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoDato")
            {
                Atributo.Literales.Add(new Elemento(TipoDato.Texto.GetHashCode().ToString(), TipoDato.Texto.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDato.Opción.GetHashCode().ToString(), TipoDato.Opción.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDato.Fecha.GetHashCode().ToString(), TipoDato.Fecha.ToString()));
            }
        }
        #endregion
    }

    public class Datos : Colección
    {
        #region Contructor
        public Datos(Entidad Solución) : base(Solución, new Dato(Solución)) { }
        #endregion

        #region Métodos
        public Dato Obtener(string Clave) { return (Dato)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Dato(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Directorio
{
    public class Teléfono : Entidad
    {
        #region Constructores
        public Teléfono() : base() { }
        public Teléfono(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Persona
        public Personas Personas
        {
            get { return (Personas)base.Atributos["Persona"].Colección; }
            set { base.Atributos["Persona"].Colección = value; }
        }

        public Persona Persona { get { return (Persona)this.Personas.Obtener(); } }

        public string IdPersona
        {
            get { return (string)base.Atributos["Persona"].Valor.Actual; }
            set { base.Atributos["Persona"].Valor.Actual = value; }
        }
        #endregion

        public string Número
        {
            get { return (string)base.Atributos["Número"].Valor.Actual; }
            set { base.Atributos["Número"].Valor.Actual = value; }
        }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Teléfono(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Teléfono";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Teléfonos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Número;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Persona", TipoAtributo.Registro));
            base.Atributos.Add(new Atributo("Número", TipoAtributo.Texto));
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Persona")
                this.Personas = new Personas(this.Solución);
        }
        #endregion
    }

    public class Teléfonos : Colección
    {
        #region Constructores
        public Teléfonos(Entidad Solución) : base(Solución, new Teléfono(Solución)) { }
        #endregion

        #region Métodos
        public Teléfono Obtener(string Clave) { return (Teléfono)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Teléfono(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Directorio
{
    public class Dirección : Entidad
    {
        #region Constructores
        public Dirección() : base() { }
        public Dirección(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Persona
        public Personas Personas
        {
            get { return (Personas)base.Atributos["Persona"].Colección; }
            set { base.Atributos["Persona"].Colección = value; }
        }

        public Persona Persona { get { return (Persona)this.Personas.Obtener(); } }

        public string IdPersona
        {
            get { return (string)base.Atributos["Persona"].Valor.Actual; }
            set { base.Atributos["Persona"].Valor.Actual = value; }
        }
        #endregion

        public string Descripción
        {
            get { return (string)base.Atributos["Descripción"].Valor.Actual; }
            set { base.Atributos["Descripción"].Valor.Actual = value; }
        }

        public string Distrito
        {
            get { return (string)base.Atributos["Distrito"].Valor.Actual; }
            set { base.Atributos["Distrito"].Valor.Actual = value; }
        }

        public string Provincia
        {
            get { return (string)base.Atributos["Provincia"].Valor.Actual; }
            set { base.Atributos["Provincia"].Valor.Actual = value; }
        }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Dirección(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Dirección";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Direcciones";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Descripción;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Persona", TipoAtributo.Registro));
            base.Atributos.Add(new Atributo("Descripción", TipoAtributo.Texto));
            base.Atributos.Add(new Atributo("Distrito", TipoAtributo.Texto));
            base.Atributos.Add(new Atributo("Provincia", TipoAtributo.Texto));
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Persona")
                this.Personas = new Personas(this.Solución);
        }
        #endregion
    }

    public class Direcciones : Colección
    {
        #region Constructores
        public Direcciones(Entidad Solución) : base(Solución, new Dirección(Solución)) { }
        #endregion

        #region Métodos
        public Dirección Obtener(string Clave) { return (Dirección)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Dirección(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Directorio
{
    public class Contacto : Entidad
    {
        #region Constructores
        public Contacto() : base() { }
        public Contacto(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Persona
        public Personas Personas
        {
            get { return (Personas)base.Atributos["Persona"].Colección; }
            set { base.Atributos["Persona"].Colección = value; }
        }

        public Persona Persona { get { return (Persona)this.Personas.Obtener(); } }

        public string IdPersona
        {
            get { return (string)base.Atributos["Persona"].Valor.Actual; }
            set { base.Atributos["Persona"].Valor.Actual = value; }
        }
        #endregion

        #region Colaborador
        public Personas Colaboradores
        {
            get { return (Personas)base.Atributos["Colaborador"].Colección; }
            set { base.Atributos["Colaborador"].Colección = value; }
        }

        public Persona Colaborador { get { return (Persona)this.Colaboradores.Obtener(); } }

        public string IdColaborador
        {
            get { return (string)base.Atributos["Colaborador"].Valor.Actual; }
            set { base.Atributos["Colaborador"].Valor.Actual = value; }
        }
        #endregion
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Contacto(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Contacto";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Contactos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Colaborador.Registro.Título;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Persona", TipoAtributo.Registro));
            base.Atributos.Add(new Atributo("Colaborador", TipoAtributo.Registro));
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Persona")
                this.Personas = new Personas(this.Solución);

            if (Atributo.Identidad.Clave == "Colaborador")
                this.Colaboradores = new Personas(this.Solución);
        }
        #endregion
    }

    public class Contactos : Colección
    {
        #region Constructores
        public Contactos(Entidad Solución) : base(Solución, new Contacto(Solución)) { }
        #endregion

        #region Métodos
        public Contacto Obtener(string Clave) { return (Contacto)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Contacto(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Directorio
{
    public class CorreoElectrónico : Entidad
    {
        #region Constructores
        public CorreoElectrónico() : base() { }

        public CorreoElectrónico(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Persona
        public Personas Personas
        {
            get { return (Personas)base.Atributos["Persona"].Colección; }
            set { base.Atributos["Persona"].Colección = value; }
        }

        public Persona Persona { get { return (Persona)this.Personas.Obtener(); } }

        public string IdPersona
        {
            get { return (string)base.Atributos["Persona"].Valor.Actual; }
            set { base.Atributos["Persona"].Valor.Actual = value; }
        }
        #endregion

        public string Dirección
        {
            get { return (string)base.Atributos["Dirección"].Valor.Actual; }
            set { base.Atributos["Dirección"].Valor.Actual = value; }
        }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new CorreoElectrónico(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            #region Identidad
            base.Identidad.Clave = "CorreoElectrónico";

            base.Identidad.Nombre = "Correo electrónico";

            base.Identidad.NombreColección = "Correos electrónicos";
            #endregion
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "CorreosElectrónicos";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Dirección;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Persona", TipoAtributo.Registro));

            base.Atributos.Add(new Atributo("Dirección", TipoAtributo.Texto));
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Persona")
                this.Personas = new Personas(this.Solución);
        }
        #endregion
    }

    public class CorreosElectrónicos : Colección
    {
        #region Constructores
        public CorreosElectrónicos(Entidad Solución) : base(Solución, new CorreoElectrónico(Solución)) { }
        #endregion

        #region Métodos
        public CorreoElectrónico Obtener(string Clave) { return (CorreoElectrónico)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new CorreoElectrónico(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Directorio
{
    public enum TipoPersona { Ninguno, Natural, Jurídica }

    public enum TipoDocumentoIdentidad { Ninguno, DNI, RUC, CarnetExtranjería }

    public class Persona : Entidad
    {
        #region Constructores
        public Persona() : base() { }
        public Persona(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public TipoPersona TipoPersona { get { return (TipoPersona)base.Atributos["TipoPersona"].Valor.Actual; } set { base.Atributos["TipoPersona"].Valor.Actual = (int)value; } }

        public string Nombres { get { return (string)base.Atributos["Nombres"].Valor.Actual; } set { base.Atributos["Nombres"].Valor.Actual = value; } }

        public string ApellidoPaterno { get { return (string)base.Atributos["ApellidoPaterno"].Valor.Actual; } set { base.Atributos["ApellidoPaterno"].Valor.Actual = value; } }

        public string ApellidoMaterno { get { return (string)base.Atributos["ApellidoMaterno"].Valor.Actual; } set { base.Atributos["ApellidoMaterno"].Valor.Actual = value; } }

        public string RazónSocial { get { return (string)base.Atributos["RazónSocial"].Valor.Actual; } set { base.Atributos["RazónSocial"].Valor.Actual = value; } }

        public string NombreDenominaciónRazónSocial { get { return (this.TipoPersona == TipoPersona.Natural) ? this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno : this.RazónSocial; } }

        public TipoDocumentoIdentidad TipoDocumentoIdentidad { get { return (TipoDocumentoIdentidad)base.Atributos["TipoDocumentoIdentidad"].Valor.Actual; } set { base.Atributos["TipoDocumentoIdentidad"].Valor.Actual = (int)value; } }

        public string NúmeroDocumentoIdentidad { get { return (string)base.Atributos["NúmeroDocumentoIdentidad"].Valor.Actual; } set { base.Atributos["NúmeroDocumentoIdentidad"].Valor.Actual = value; } }

        #region DirecciónFiscal
        public Direcciones DireccionesFiscales { get { return (Direcciones)base.Atributos["DirecciónFiscal"].Colección; } set { base.Atributos["DirecciónFiscal"].Colección = value; } }

        public Dirección DirecciónFiscal { get { return (Dirección)this.DireccionesFiscales.Obtener(); } }

        public string IdDirecciónFiscal { get { return (string)base.Atributos["DirecciónFiscal"].Valor.Actual; } set { base.Atributos["DirecciónFiscal"].Valor.Actual = value; } }
        #endregion

        public Teléfonos Teléfonos { get { return (Teléfonos)base.Atributos["Teléfonos"].Colección; } set { base.Atributos["Teléfonos"].Colección = value; } }

        public Direcciones Direcciones { get { return (Direcciones)base.Atributos["Direcciones"].Colección; } set { base.Atributos["Direcciones"].Colección = value; } }

        public Contactos Contactos { get { return (Contactos)base.Atributos["Contactos"].Colección; } set { base.Atributos["Contactos"].Colección = value; } }

        public CorreosElectrónicos CorreosElectrónicos { get { return (CorreosElectrónicos)base.Atributos["CorreosElectrónicos"].Colección; } set { base.Atributos["CorreosElectrónicos"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Persona(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Persona";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Personas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            if (this.TipoPersona == TipoPersona.Natural)
                this.Registro.Título = this.Nombres + " " + this.ApellidoPaterno + " " + this.ApellidoMaterno;

            else if (this.TipoPersona == TipoPersona.Jurídica)
                this.Registro.Título = this.RazónSocial;

            else this.Registro.Título = "(Desconocido)";
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Teléfono());

            base.AccesosDirectos.Agregar(new Dirección());

            base.AccesosDirectos.Agregar(new Contacto());

            base.AccesosDirectos.Agregar(new CorreoElectrónico());
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            #region Nombre
            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("TipoPersona", "Tipo de persona", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Nombres", TipoAtributo.Texto) { Dependencia = "TipoPersona" });

            base.Atributos.Add(new Atributo("ApellidoPaterno", "Apellido paterno", TipoAtributo.Texto) { Dependencia = "TipoPersona" });

            base.Atributos.Add(new Atributo("ApellidoMaterno", "Apellido materno", TipoAtributo.Texto) { Dependencia = "TipoPersona" });

            base.Atributos.Add(new Atributo("RazónSocial", "Razón social", TipoAtributo.Texto) { Dependencia = "TipoPersona" });

            base.Atributos.Add(new Atributo("Lógico", TipoAtributo.Lógico) { TipoDeAtributoLógico = TipoDeAtributoLógico.NoYSí });
            #endregion

            #region Identificación
            base.Atributos.Add(new Atributo("Identificación", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("TipoDocumentoIdentidad", "Tipo de documento de identidad", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("NúmeroDocumentoIdentidad", "Número de documento de identidad", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("DirecciónFiscal", "Dirección fiscal", TipoAtributo.Registro));

            base.Atributos.Add(new Atributo("EntidadFinanciera", "Entidad financiera", TipoAtributo.Texto) { Auxiliaridad = TipoAccesibilidad.Sí });
            #endregion

            #region DemostraciónFechaHora
            base.Atributos.Add(new Atributo("DemostraciónFechaHora", "Demostración de fecha y hora", TipoAtributo.Grupo) { Auxiliaridad = TipoAccesibilidad.Sí });

            base.Atributos.Add(new Atributo("FechaNacimiento", "Fecha de ncimiento", TipoAtributo.Fecha));

            base.Atributos.Add(new Atributo("HoraNacimiento", "Hora de nacimiento", TipoAtributo.Hora));
            #endregion

            #region Carpetas
            base.Atributos.Add(new Atributo("Carpetas", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("Teléfonos", TipoAtributo.Carpeta));

            base.Atributos.Add(new Atributo("Direcciones", TipoAtributo.Carpeta));

            base.Atributos.Add(new Atributo("Contactos", TipoAtributo.Carpeta));

            base.Atributos.Add(new Atributo("CorreosElectrónicos", TipoAtributo.Carpeta));
            #endregion
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoPersona")
            {
                Atributo.Literales.Add(new Elemento(TipoPersona.Natural.GetHashCode().ToString(), TipoPersona.Natural.ToString()));

                Atributo.Literales.Add(new Elemento(TipoPersona.Jurídica.GetHashCode().ToString(), TipoPersona.Jurídica.ToString()));
            }

            if (Atributo.Identidad.Clave == "TipoDocumentoIdentidad")
            {
                Atributo.Literales.Add(new Elemento(TipoDocumentoIdentidad.DNI.GetHashCode().ToString(), TipoDocumentoIdentidad.DNI.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDocumentoIdentidad.RUC.GetHashCode().ToString(), TipoDocumentoIdentidad.RUC.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDocumentoIdentidad.CarnetExtranjería.GetHashCode().ToString(), TipoDocumentoIdentidad.CarnetExtranjería.ToString()));
            }
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "DirecciónFiscal")
                this.DireccionesFiscales = new Direcciones(this.Solución);

            if (Atributo.Identidad.Clave == "Teléfonos")
                this.Teléfonos = new Teléfonos(this.Solución);

            if (Atributo.Identidad.Clave == "Direcciones")
                this.Direcciones = new Direcciones(this.Solución);

            if (Atributo.Identidad.Clave == "Contactos")
                this.Contactos = new Contactos(this.Solución);

            if (Atributo.Identidad.Clave == "CorreosElectrónicos")
                this.CorreosElectrónicos = new CorreosElectrónicos(this.Solución);
        }

        #region Restricciones
        #region RestringiendoAtributo
        private void RestringirNombres(Atributo Atributo)
        {
            if (this.TipoPersona == TipoPersona.Natural)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;
            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirApellidoPaterno(Atributo Atributo)
        {
            if (this.TipoPersona == TipoPersona.Natural)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;
            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirApellidoMaterno(Atributo Atributo)
        {
            if (this.TipoPersona == TipoPersona.Natural)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;
            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirAFP(Atributo Atributo)
        {
            if (this.TipoPersona == TipoPersona.Natural)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;
            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        private void RestringirRazónSocial(Atributo Atributo)
        {
            if (this.TipoPersona == TipoPersona.Jurídica)
                Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.Automático;
            else Atributo.Accesibilidad.Visibilidad = TipoAccesibilidad.No;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Nombres")
                this.RestringirNombres(Atributo);

            if (Atributo.Identidad.Clave == "ApellidoPaterno")
                this.RestringirApellidoPaterno(Atributo);

            if (Atributo.Identidad.Clave == "ApellidoMaterno")
                this.RestringirApellidoMaterno(Atributo);

            if (Atributo.Identidad.Clave == "RazónSocial")
                this.RestringirRazónSocial(Atributo);
        }
        #endregion
        #endregion

        //protected override bool Guardando()
        //{
        //    this.Notificaciones.Clear();

        //    if (this.ApellidoPaterno == string.Empty)
        //    {
        //        this.Notificaciones.Agregar(System.Guid.NewGuid().ToString(), "Guardando", "El apellido paterno tiene que estar especificado.");

        //        return false;
        //    }

        //    return true;
        //}
        #endregion
    }

    public class Personas : Colección
    {
        #region Contructor
        public Personas(Entidad Solución) : base(Solución, new Persona(Solución)) { }
        #endregion

        #region Métodos
        public Persona Obtener(string Clave) { return (Persona)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Persona(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Divisas
{
    public class Importe : Extensión
    {
        #region Constructores
        public Importe()
        {
            this.IdentidadImporte.Clave = "Importe";

            this.IdentidadMoneda.Clave = "Moneda";
        }

        public Importe(string Clave)
        {
            this.IdentidadImporte.Clave = "ImporteDe" + Clave;

            this.IdentidadImporte.Nombre = "Importe";

            this.IdentidadMoneda.Clave = "MonedaDe" + Clave;

            this.IdentidadMoneda.Nombre = "Moneda";
        }
        #endregion

        #region Propiedades
        //private Identidad _IdentidadMoneda = new Identidad();
        //public Identidad IdentidadMoneda { get { return this._IdentidadMoneda; } set { this._IdentidadMoneda = value; } }

        //private Identidad _IdentidadImporte = new Identidad();
        //public Identidad IdentidadImporte { get { return this._IdentidadImporte; } set { this._IdentidadImporte = value; } }

        public Identidad IdentidadMoneda = new Identidad();

        public Identidad IdentidadImporte = new Identidad();

        private Entidad Global { get { return this.Entidad.ObtenerSolución("Aplimática.Global"); } }

        #region Moneda
        public Monedas Monedas { get { return (Monedas)base.Atributos[this.IdentidadMoneda.Clave].Colección; } set { base.Atributos[this.IdentidadMoneda.Clave].Colección = value; } }

        public Moneda Moneda { get { return (Moneda)this.Monedas.Obtener(); } }

        public string IdMoneda { get { return (string)base.Atributos[this.IdentidadMoneda.Clave].Valor.Actual; } set { base.Atributos[this.IdentidadMoneda.Clave].Valor.Actual = value; } }
        #endregion

        public decimal Valor { get { return (decimal)base.Atributos[this.IdentidadImporte.Clave].Valor.Actual; } set { base.Atributos[this.IdentidadImporte.Clave].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo(this.IdentidadMoneda.Clave, this.IdentidadMoneda.Nombre, TipoAtributo.Registro) { Colección = new Monedas(this.Global), Dependencia = this.IdentidadMoneda.Recursos });

            base.Atributos.Add(new Atributo(this.IdentidadImporte.Clave, this.IdentidadImporte.Nombre, TipoAtributo.Importe) { Dependencia = this.IdentidadImporte.Recursos });
        }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Divisas
{
    public class Cambio : Entidad
    {
        #region Constructores
        public Cambio() : base() { }
        public Cambio(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Moneda
        public Monedas Monedas { get { return (Monedas)base.Atributos["Moneda"].Colección; } set { base.Atributos["Moneda"].Colección = value; } }

        public Moneda Moneda { get { return (Moneda)this.Monedas.Obtener(); } }

        public string IdMedida { get { return (string)base.Atributos["Moneda"].Valor.Actual; } set { base.Atributos["Moneda"].Valor.Actual = value; } }
        #endregion

        public DateTime Fecha { get { return (DateTime)base.Atributos["Fecha"].Valor.Actual; } set { base.Atributos["Fecha"].Valor.Actual = value; } }

        public decimal CompraOficial { get { return (decimal)base.Atributos["CompraOficial"].Valor.Actual; } set { base.Atributos["CompraOficial"].Valor.Actual = value; } }

        public decimal VentaOficial { get { return (decimal)base.Atributos["VentaOficial"].Valor.Actual; } set { base.Atributos["VentaOficial"].Valor.Actual = value; } }

        public decimal CompraComercial { get { return (decimal)base.Atributos["CompraComercial"].Valor.Actual; } set { base.Atributos["CompraComercial"].Valor.Actual = value; } }

        public decimal VentaComercial { get { return (decimal)base.Atributos["VentaComercial"].Valor.Actual; } set { base.Atributos["VentaComercial"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Cambio(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Cambio";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Cambios";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Fecha.ToShortDateString();
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Moneda", TipoAtributo.Registro));

            base.Atributos.Add(new Atributo("Fecha", TipoAtributo.Fecha));

            base.Atributos.Add(new Atributo("CompraOficial", "Compra oficial", TipoAtributo.Tasa));

            base.Atributos.Add(new Atributo("VentaOficial", "Venta oficial", TipoAtributo.Tasa));

            base.Atributos.Add(new Atributo("CompraComercial", "Compra comercial", TipoAtributo.Tasa));

            base.Atributos.Add(new Atributo("VentaComercial", "Venta comercial", TipoAtributo.Tasa));
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Moneda")
                this.Monedas = new Monedas(this.Solución);
        }
        #endregion
    }

    public class Cambios : Colección
    {
        #region Constructores
        public Cambios(Entidad Solución) : base(Solución, new Cambio(Solución)) { }
        #endregion

        #region Métodos
        public Cambio Obtener(string Clave) { return (Cambio)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Cambio(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Divisas
{
    public enum TipoMoneda { Ninguno, Nacional, Extranjera }

    public class Moneda : Entidad
    {
        #region Constructores
        public Moneda() : base() { }
        public Moneda(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public TipoMoneda TipoMoneda { get { return (TipoMoneda)base.Atributos["TipoMoneda"].Valor.Actual; } set { base.Atributos["TipoMoneda"].Valor.Actual = (int)value; } }

        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public string Plural { get { return (string)base.Atributos["Plural"].Valor.Actual; } set { base.Atributos["Plural"].Valor.Actual = value; } }

        public string Símbolo { get { return (string)base.Atributos["Símbolo"].Valor.Actual; } set { base.Atributos["Símbolo"].Valor.Actual = value; } }

        public string CódigoISO4217 { get { return (string)base.Atributos["CódigoISO4217"].Valor.Actual; } set { base.Atributos["CódigoISO4217"].Valor.Actual = value; } }

        public string CargadorArchivo { get { return (string)base.Atributos["CargadorArchivo"].Valor.Actual; } set { base.Atributos["CargadorArchivo"].Valor.Actual = value; } }

        public Cambios Cambios { get { return (Cambios)base.Atributos["Cambios"].Colección; } set { base.Atributos["Cambios"].Colección = value; } }

        public string Login { get { return (string)base.Atributos["Login"].Valor.Actual; } set { base.Atributos["Login"].Valor.Actual = value; } }

        public string Password { get { return (string)base.Atributos["Password"].Valor.Actual; } set { base.Atributos["Password"].Valor.Actual = value; } }

        public string Secuencia { get { return (string)base.Atributos["Secuencia"].Valor.Actual; } set { base.Atributos["Secuencia"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        public string ExpresarImporteEnTexto(decimal Importe)
        {
            string _ImporteExpresadoEnTexto = string.Empty;

            _ImporteExpresadoEnTexto = this.Solución.Negocio.Contrato.LógicaNegocio.Comunicación.Librería.enletras(Importe.ToString()) + (Importe == 1 ? this.Nombre : this.Plural);

            return _ImporteExpresadoEnTexto;
        }

        private decimal ObtenerCambio(TipoCambio TipoCambio, DateTime Fecha)
        {
            decimal _CambioObtenido = 0M;

            Cambios _Cambios = new Cambios(this.Solución);

            _Cambios.TextoComando.Externo = "SELECT * FROM [Cambios] WHERE CONVERT(NVARCHAR, Fecha, 112) = '" + Fecha.Year.ToString() + Fecha.Month.ToString().PadLeft(2, '0') + Fecha.Day.ToString().PadLeft(2, '0') + "'";

            _Cambios.Cargar(TipoEstructura.Valores);

            if (_Cambios.Count > 0)
            {
                Cambio _Cambio = (Cambio)_Cambios[0];

                if (TipoCambio == TipoCambio.CompraOficial)
                    _CambioObtenido = _Cambio.CompraOficial;

                if (TipoCambio == TipoCambio.VentaOficial)
                    _CambioObtenido = _Cambio.VentaOficial;

                if (TipoCambio == TipoCambio.CompraComercial)
                    _CambioObtenido = _Cambio.CompraComercial;

                if (TipoCambio == TipoCambio.VentaComercial)
                    _CambioObtenido = _Cambio.VentaComercial;
            }

            return _CambioObtenido;
        }

        public enum TipoCambio { Ninguno, CompraOficial, VentaOficial, CompraComercial, VentaComercial }
        public decimal ConvertirA(decimal Importe, Moneda Moneda, DateTime Fecha, TipoCambio TipoCambio)
        {
            decimal _ConvertirA = 0M;

            if (this.Registro.Id == Moneda.Registro.Id)
                _ConvertirA = Importe;

            else
                _ConvertirA = Importe * this.ObtenerCambio(TipoCambio, Fecha);

            return _ConvertirA;
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Moneda(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Moneda";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Monedas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Cambio());
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("TipoMoneda", "Tipo de moneda", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Cambios", TipoAtributo.Carpeta));

            base.Atributos.Add(new Atributo("OtrosDatos", TipoAtributo.Sección));

            base.Atributos.Add(new Atributo("Tiempo", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("Fecha", TipoAtributo.Fecha) { Auxiliaridad = TipoAccesibilidad.Sí });

            base.Atributos.Add(new Atributo("Hora", TipoAtributo.Hora) { Auxiliaridad = TipoAccesibilidad.Sí });

            base.Atributos.Add(new Atributo("Códigos", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("Plural", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Símbolo", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("CódigoISO4217", "Código ISO4217", TipoAtributo.Texto));

            //base.Atributos.Add(new Atributo("CargadorArchivo_", TipoAtributo.Grupo));

            //base.Atributos.Add(new Atributo("CargadorArchivo", TipoAtributo.CargadorArchivo) { Auxiliar = true });

            //base.Atributos.Add(new Atributo("Cargar", TipoAtributo.Comando) { AcciónCargadorArchivo = AcciónCargadorArchivo.Cargar, CargadorArchivo = "CargadorArchivo" });

            //base.Atributos.Add(new Atributo("Descargar", TipoAtributo.Comando) { AcciónCargadorArchivo = AcciónCargadorArchivo.Descargar, CargadorArchivo = "CargadorArchivo" });

            base.Atributos.Add(new Atributo("Autenticación", TipoAtributo.Sección));

            base.Atributos.Add(new Atributo("Pivolt", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("Login", TipoAtributo.Texto) { Auxiliaridad = TipoAccesibilidad.Sí });

            base.Atributos.Add(new Atributo("Password", TipoAtributo.Texto) { Auxiliaridad = TipoAccesibilidad.Sí });

            base.Atributos.Add(new Atributo("Go", TipoAtributo.Comando));

            base.Atributos.Add(new Atributo("Secuencia", TipoAtributo.Edición) { Auxiliaridad = TipoAccesibilidad.Sí });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoMoneda")
            {
                Atributo.Literales.Add(new Elemento(TipoMoneda.Nacional.GetHashCode().ToString(), TipoMoneda.Nacional.ToString()));
                Atributo.Literales.Add(new Elemento(TipoMoneda.Extranjera.GetHashCode().ToString(), TipoMoneda.Extranjera.ToString()));
            }
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Cambios")
                this.Cambios = new Cambios(this.Solución);
        }

        #region EjecutandoComando
        private void EjecutarComandoGo()
        {
            //Aplimática.Finnerve.Seguridad.Autenticación.Pivolt _Pivolt = new Aplimática.Finnerve.Seguridad.Autenticación.Pivolt();

            //Aplimática.Finnerve.Seguridad.Autenticación.User _User = _Pivolt.Go(this.Login, this.Password);

            //this.Secuencia = String.Empty;

            //if (_User != null)
            //{
            //    this.Secuencia += _User.Login;

            //    this.Secuencia += "; ";

            //    this.Secuencia += _User.Email;

            //    this.Secuencia += "; ";

            //    this.Secuencia += _User.AccessAllInvestors.ToString();

            //    this.Secuencia += "; ";

            //    this.Secuencia += _User.Password;
            //}

            //else this.Secuencia = "Usuario o contraseña no existe.";
        }

        protected override void EjecutandoComando(Atributo Comando)
        {
            base.EjecutandoComando(Comando);

            if (Comando.Identidad.Clave == "Go")
                this.EjecutarComandoGo();
        }
        #endregion
        #endregion
    }

    public class Monedas : Colección
    {
        #region Contructor
        public Monedas(Entidad Solución) : base(Solución, new Moneda(Solución)) { }
        #endregion

        #region Métodos
        public Moneda Obtener(string Clave) { return (Moneda)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Moneda(this.Solución); }
        #endregion
    }
}
﻿
namespace Aplimática.Global
{
    public enum MedioPago { Ninguno, Efectivo, Visa, MasterCard, PayPal }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Herramientas
{
    #region Enumeraciones
    public enum Número
    {
        Cero = 0,
        Uno = 1,
        Dos = 2,
        Tres = 3,
        Cuatro = 4,
        Cinco = 5,
        Seis = 6,
        Siete = 7,
        Ocho = 8,
        Nueve = 9
    }

    public enum OperadorMatemático
    {
        Suma = 1,
        Resta = 2,
        Multiplicación = 3,
        División = 4,
        Potencia = 5,
        Raíz = 6,
        Redondeo = 7,
        Exceso = 8,
        Máximo = 9,
        Mínimo = 10,
        Sumatoria = 11,
        Promedio = 12
    }

    public enum OperadorLógico
    {
        Mayor = 1,
        Menor = 2,
        Igual = 3,
        Diferente = 4,
        Y = 5,
        O = 6
    }

    public enum OperadorFecha
    {
        AgregarMeses = 1,
        ObtenerAño = 2,
        Hoy = 3
    }

    public enum Otro
    {
        AbrirParéntesis = 1,
        CerrarParéntesis = 2,
        Coma = 3,
        Decimal = 4,
        Borrar = 5
    }
    #endregion

    public class GeneradorExpresiones : Entidad
    {
        #region Constructores
        public GeneradorExpresiones() : base() { }
        public GeneradorExpresiones(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region EntidadUsuario
        private Entidad _EntidadUsuario = null;
        public Entidad EntidadUsuario
        {
            get { return this._EntidadUsuario; }
            set { this._EntidadUsuario = value; }
        }
        #endregion

        #region ClaveClave
        private string _ClaveClave = string.Empty;
        public string ClaveClave
        {
            get { return this._ClaveClave; }
            set { this._ClaveClave = value; }
        }
        #endregion

        #region ClaveScript
        private string _ClaveScript = string.Empty;
        public string ClaveScript
        {
            get { return this._ClaveScript; }
            set { this._ClaveScript = value; }
        }
        #endregion

        #region ClaveVista
        private string _ClaveVista = string.Empty;
        public string ClaveVista
        {
            get { return this._ClaveVista; }
            set { this._ClaveVista = value; }
        }
        #endregion

        #region Clave
        public string Clave
        {
            get { return (string)base.Atributos["Clave"].Valor.Actual; }
            set { base.Atributos["Clave"].Valor.Actual = value; } //this.GenerarScriptVista(); }
        }
        #endregion

        #region Script
        public string Script
        {
            get { return (string)base.Atributos["Script"].Valor.Actual; }
            set { base.Atributos["Script"].Valor.Actual = value; }
        }
        #endregion

        #region Vista
        public string Vista
        {
            get { return (string)base.Atributos["Vista"].Valor.Actual; }
            set { base.Atributos["Vista"].Valor.Actual = value; }
        }
        #endregion

        #region Expresiones
        public Expresiones Expresiones
        {
            get { return (Expresiones)base.Atributos["Expresiones"].Expresiones; }
            set { base.Atributos["Expresiones"].Expresiones = value; }
        }
        #endregion
        #endregion

        #region Métodos
        #region SeleccionandoCarpeta
        protected override void SeleccionandoColección(Atributo Carpeta)
        {
            base.SeleccionandoColección(Carpeta);

            if (this._EntidadUsuario != null)
                this._EntidadUsuario.SeleccionarColección(Carpeta);
        }
        #endregion

        #region RestringiendoAtributo
        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (this._EntidadUsuario != null)
                this._EntidadUsuario.RestringirAtributo(Atributo);
        }
        #endregion

        #region PrepararGeneradorExpresiones
        public void AgregarAtributo(string Clave, string Nombre, Colección Colección)
        {
            if (!base.Atributos.Contains(Clave))
            {
                Atributo _Atributo;

                #region Registro
                _Atributo = new Atributo();
                _Atributo.Identidad.Clave = Clave;
                _Atributo.Identidad.Nombre = Nombre;
                _Atributo.TipoAtributo = TipoAtributo.Registro;
                _Atributo.Entidad = this;
                base.Atributos.Add(_Atributo);

                _Atributo.PrepararEstructura();

                _Atributo.Sección = this.Secciones[0];
                _Atributo.Grupo = this.Grupos["Variables"];

                this.Grupos["Variables"].Atributos.Add(_Atributo);
                this.Secciones[0].Atributos.Add(_Atributo);

                _Atributo.Colección = Colección;
                _Atributo.Colección.Atributo = _Atributo;
                //_Atributo.Colección.Entidad.Identidad.Clave = Clave;
                //_Atributo.Colección.Entidad = this;
                #endregion

                #region Comando
                _Atributo = new Atributo();
                _Atributo.Identidad.Clave = "Comando" + Clave;
                _Atributo.Identidad.Nombre = "Seleccionar " + Nombre.ToLower();
                _Atributo.TipoAtributo = TipoAtributo.Comando;
                _Atributo.Entidad = this;
                base.Atributos.Add(_Atributo);

                _Atributo.PrepararEstructura();

                _Atributo.Sección = this.Secciones[0];
                _Atributo.Grupo = this.Grupos["Variables"];

                this.Grupos["Variables"].Atributos.Add(_Atributo);
                this.Secciones[0].Atributos.Add(_Atributo);

                //this.AgregarOperación("Comando" + Clave);
                this.PrepararProceso(TipoProceso.Restricción, _Atributo.Identidad.Clave);

                _Atributo.Restricciones.Add(this.Restricciones[this.Restricciones.Count - 1]);
                #endregion
            }
        }

        #region AgregarOperación
        public void AgregarOperación(string Recurso)
        {
            this.AgregarOperación(Recurso, string.Empty);
        }

        public void AgregarOperación(string Recurso, string Resultado)
        {
            if (!this.Restricciones.Contains("Restricción" + Recurso))
            {
                this.PrepararProceso(TipoProceso.Restricción, Recurso);

                this.Restricciones.Add(new Proceso("Restricción" + Recurso));
                this.Restricciones["Restricción" + Recurso].Recursos.Add(base.Atributos[Recurso]);

                if (Resultado != string.Empty)
                    this.Restricciones["Restricción" + Recurso].Resultados.Add(base.Atributos[Resultado]);

                this.Restricciones["Restricción" + Recurso].Entidad = this;
                base.Atributos[Recurso].Restricciones.Add(this.Restricciones["Restricción" + Recurso]);
            }
        }
        #endregion
        #endregion

        #region Generar expresión
        public void AgregarElemento(string DetalleReporte)
        {
            if (this.Clave != string.Empty)
                this.Clave = this.Clave + ";";
            this.Clave = this.Clave + DetalleReporte;

            this.GenerarGuardarScriptVista();
        }

        public void QuitarElemento()
        {
            string[] _Claves = this.Clave.Split(new Char[] { ';' });
            string _NuevaClave = string.Empty;
            if (_Claves.Length > 1)
            {
                for (int n = 0; n < _Claves.Length - 1; n++)
                {
                    if (_NuevaClave != string.Empty)
                        _NuevaClave = _NuevaClave + ";";
                    _NuevaClave = _NuevaClave + _Claves[n];
                    this.Clave = _NuevaClave;
                }
            }
            else { this.Clave = string.Empty; }
            this.GenerarGuardarScriptVista();
        }

        private void GenerarMax(ref int Índice)
        {
            int _ÍndiceComando = Índice;
            string[] _Claves = this.Clave.Split(new Char[] { ';' });
            string _Script = string.Empty;
            string _Último = string.Empty;
            for (int _Índice = Índice + 1; _Índice < _Claves.Length; _Índice++)
            {
                string[] _Clave2 = _Claves[_Índice].Split(new Char[] { '|' });
                if (_Clave2[2] == ")") { this.Vista = this.Vista + ")"; break; }
                else
                {
                    if (_Clave2[2].ToString() == ",")
                        _Script = _Script + ")";
                    if (_Último == string.Empty || _Último == ",")
                        _Script = _Script + "(" + _Clave2[2];
                    else _Script = _Script + _Clave2[2];
                    this.Vista = this.Vista + _Clave2[3];
                }
                _Último = _Clave2[2].ToString();
                Índice = _Índice;
            }
            string[] _Claves3 = _Claves[_ÍndiceComando].Split(new Char[] { '|' });
            if (_Claves3[2].ToString() == "ROUND(AVG(")
                _Script = "(SELECT " + _Claves3[2].ToString() + "filaMaxima), 6) FROM ( VALUES" + _Script + ")) AS UNIQUECOLUMN(filaMaxima))";
            else _Script = "(SELECT " + _Claves3[2].ToString() + "filaMaxima) FROM ( VALUES" + _Script + ")) AS UNIQUECOLUMN(filaMaxima))";
            this.Script = this.Script + _Script;
        }

        private void GenerarScriptVista()
        {
            this.Expresiones.Clear();

            this.Script = string.Empty;
            this.Vista = string.Empty;

            string[] _Claves = this.Clave.Split(new Char[] { ';' });

            if (_Claves.Length > 0 && this.Clave != "")
            {
                for (int _Índice = 0; _Índice < _Claves.Length; _Índice++)
                {
                    if (this.Vista != string.Empty)
                        this.Vista = this.Vista + "";

                    string[] _Claves2 = _Claves[_Índice].Split(new Char[] { '|' });

                    this.Vista = this.Vista + _Claves2[3];

                    this.Expresiones.Add(new Aplimática.Framework.Expresión(this.Solución.Negocio.Contrato.LógicaNegocio.Comunicación.Librería.GenerarClave(), _Claves2[1], _Claves2[1], _Claves2[2], _Claves2[3]));

                    if (_Claves2[2] == "MAX(" || _Claves2[2] == "MIN(" || _Claves2[2] == "SUM(" || _Claves2[2] == "ROUND(AVG(")
                    {
                        this.GenerarMax(ref _Índice); _Índice++;
                    }

                    else this.Script = this.Script + _Claves2[2];
                }

                this.Script = this.Script.Replace("AND", " AND ");
                this.Script = this.Script.Replace("OR", " OR ");

                this.Vista = this.Vista.Replace("Y", " Y ");
                this.Vista = this.Vista.Replace("O", " O ");
                this.Vista = this.Vista.Replace("-", " - ");
                this.Vista = this.Vista.Replace("*", " * ");
                this.Vista = this.Vista.Replace("/", " / ");
                this.Vista = this.Vista.Replace(">", " > ");
                this.Vista = this.Vista.Replace("<", " < ");
                this.Vista = this.Vista.Replace("=", " = ");
                this.Vista = this.Vista.Replace("<>", " <> ");
            }

            else { this.Vista = ""; this.Script = ""; }
        }

        private void GenerarGuardarScriptVista()
        {
            this.GenerarScriptVista();

            this.GuardarValores();
        }
        #endregion

        #region Preparando
        protected override void Preparando()
        {
            base.Preparando();

            this.Clave = this.EntidadUsuario.Atributos[this._ClaveClave].Valor.Actual.ToString();

            this.GenerarScriptVista();

            //this.RestringirAtributosAbrir();
            this.Actualizar();
        }
        #endregion
        #endregion

        #region Eventos
        #region Entidad
        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Herramienta;

            base.Identidad.Clave = "GeneradorExpresiones";
            base.Identidad.Nombre = "Generador de expresiones";
        }

        #region Agregados
        #region Operaciones
        private void SeleccionarNúmero(string Número)
        {
            this.AgregarElemento("||" + Número + "|" + Número);
        }

        private void SeleccionarOperador(string Trabajador)
        {
            #region Matemático
            if (Trabajador == OperadorMatemático.Suma.ToString())
                this.AgregarElemento("||+|+");

            if (Trabajador == OperadorMatemático.Resta.ToString())
                this.AgregarElemento("||-|-");

            if (Trabajador == OperadorMatemático.Multiplicación.ToString())
                this.AgregarElemento("||*|*");

            if (Trabajador == OperadorMatemático.División.ToString())
                this.AgregarElemento("||/|/");

            if (Trabajador == OperadorMatemático.Potencia.ToString())
                this.AgregarElemento("||POWER(|Potencia(");

            if (Trabajador == OperadorMatemático.Raíz.ToString())
                this.AgregarElemento("||POWER(|Raíz(");

            if (Trabajador == OperadorMatemático.Redondeo.ToString())
                this.AgregarElemento("||ROUND(|Redondeo(");

            if (Trabajador == OperadorMatemático.Exceso.ToString())
                this.AgregarElemento("||CEILING(|Exceso(");

            if (Trabajador == OperadorMatemático.Máximo.ToString())
                this.AgregarElemento("||MAX(|Máximo(");

            if (Trabajador == OperadorMatemático.Mínimo.ToString())
                this.AgregarElemento("||MIN(|Mínimo(");

            if (Trabajador == OperadorMatemático.Sumatoria.ToString())
                this.AgregarElemento("||SUM(|Sumatoria(");

            if (Trabajador == OperadorMatemático.Promedio.ToString())
                this.AgregarElemento("||ROUND(AVG(|Promedio(");
            #endregion

            #region Lógico
            if (Trabajador == OperadorLógico.Mayor.ToString())
                this.AgregarElemento("||>|>");

            if (Trabajador == OperadorLógico.Menor.ToString())
                this.AgregarElemento("||<|<");

            if (Trabajador == OperadorLógico.Igual.ToString())
                this.AgregarElemento("||=|=");

            if (Trabajador == OperadorLógico.Diferente.ToString())
                this.AgregarElemento("||<>|<>");

            if (Trabajador == OperadorLógico.Y.ToString())
                this.AgregarElemento("||AND| Y ");

            if (Trabajador == OperadorLógico.O.ToString())
                this.AgregarElemento("||OR| O ");
            #endregion

            #region Fecha
            //DATEADD(MONTH, 6, @FechaActual)
            if (Trabajador == OperadorFecha.AgregarMeses.ToString())
                this.AgregarElemento("||DATEADD(MONTH,|AgregarMeses(");

            //DATEPART(yyyy, @FechaSolicitud)
            if (Trabajador == OperadorFecha.ObtenerAño.ToString())
                this.AgregarElemento("||DATEPART(yyyy,|ObtenerAño(");

            //GETDATE()
            if (Trabajador == OperadorFecha.Hoy.ToString())
                this.AgregarElemento("||GETDATE()|Hoy");
            #endregion

            #region Otro
            if (Trabajador == Otro.AbrirParéntesis.ToString())
                this.AgregarElemento("||(|(");

            if (Trabajador == Otro.CerrarParéntesis.ToString())
                this.AgregarElemento("||)|)");

            if (Trabajador == Otro.Coma.ToString())
                this.AgregarElemento("||,|,");

            if (Trabajador == Otro.Decimal.ToString())
                this.AgregarElemento("||.|.");

            if (Trabajador == Otro.Borrar.ToString())
                this.QuitarElemento();
            #endregion
        }

        protected override void EjecutandoOperación(Proceso Operación)
        {
            base.EjecutandoOperación(Operación);

            if (Operación.Identidad.Clave == Número.Cero.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Uno.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Dos.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Tres.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Cuatro.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Cinco.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Seis.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Siete.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Ocho.GetHashCode().ToString()
                || Operación.Identidad.Clave == Número.Nueve.GetHashCode().ToString())
                this.SeleccionarNúmero(Operación.Identidad.Clave);

            else this.SeleccionarOperador(Operación.Identidad.Clave);
        }

        protected override void EjecutandoComando(Atributo Comando)
        {
            base.EjecutandoComando(Comando);

            if (this._EntidadUsuario != null)
                this._EntidadUsuario.EjecutarComando(Comando);
        }

        private void GuardarValores()
        {
            if (this._EntidadUsuario != null)
            {
                if (this._EntidadUsuario.Atributos.Contains(this._ClaveClave))
                    this._EntidadUsuario.Atributos[this._ClaveClave].Valor.Actual = this.Clave;

                if (this._EntidadUsuario.Atributos.Contains(this._ClaveScript))
                    this._EntidadUsuario.Atributos[this._ClaveScript].Valor.Actual = this.Script;

                if (this._EntidadUsuario.Atributos.Contains(this._ClaveVista))
                    this._EntidadUsuario.Atributos[this._ClaveVista].Valor.Actual = this.Vista;

                this._EntidadUsuario.Guardar();
            }
        }

        private void CargandoRestriccionesOperación()
        {
            Atributos _Resultados = new Atributos();
            _Resultados.Add(base.Atributos["Clave"]);
            _Resultados.Add(base.Atributos["Script"]);
            _Resultados.Add(base.Atributos["Vista"]);

            #region Número
            this.Operaciones[Número.Cero.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Uno.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Dos.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Tres.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Cuatro.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Cinco.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Seis.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Siete.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Ocho.GetHashCode().ToString()].Resultados = _Resultados;
            this.Operaciones[Número.Nueve.GetHashCode().ToString()].Resultados = _Resultados;
            #endregion

            #region OperadorMatemático
            this.Operaciones[OperadorMatemático.Suma.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Resta.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Multiplicación.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.División.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Potencia.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Raíz.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Redondeo.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Exceso.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Máximo.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Mínimo.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Sumatoria.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorMatemático.Promedio.ToString()].Resultados = _Resultados;
            #endregion

            #region OperadorLógico
            this.Operaciones[OperadorLógico.Mayor.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorLógico.Menor.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorLógico.Igual.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorLógico.Diferente.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorLógico.Y.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorLógico.O.ToString()].Resultados = _Resultados;
            #endregion

            #region OperadorFecha
            this.Operaciones[OperadorFecha.AgregarMeses.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorFecha.ObtenerAño.ToString()].Resultados = _Resultados;
            this.Operaciones[OperadorFecha.Hoy.ToString()].Resultados = _Resultados;
            #endregion

            #region OperadorOtro
            this.Operaciones[Otro.AbrirParéntesis.ToString()].Resultados = _Resultados;
            this.Operaciones[Otro.CerrarParéntesis.ToString()].Resultados = _Resultados;
            this.Operaciones[Otro.Coma.ToString()].Resultados = _Resultados;
            this.Operaciones[Otro.Decimal.ToString()].Resultados = _Resultados;
            this.Operaciones[Otro.Borrar.ToString()].Resultados = _Resultados;
            #endregion
        }

        protected override void PreparandoOperaciones()
        {
            base.PreparandoOperaciones();

            #region Número
            this.Operaciones.Add(new Proceso(Número.Cero.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Uno.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Dos.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Tres.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Cuatro.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Cinco.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Seis.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Siete.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Ocho.GetHashCode().ToString()));
            this.Operaciones.Add(new Proceso(Número.Nueve.GetHashCode().ToString()));
            #endregion

            #region OperadorMatemático
            this.Operaciones.Add(new Proceso(OperadorMatemático.Suma.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Resta.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Multiplicación.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.División.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Potencia.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Raíz.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Redondeo.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Exceso.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Máximo.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Mínimo.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Sumatoria.ToString()));
            this.Operaciones.Add(new Proceso(OperadorMatemático.Promedio.ToString()));
            #endregion

            #region OperadorLógico
            this.Operaciones.Add(new Proceso(OperadorLógico.Mayor.ToString()));
            this.Operaciones.Add(new Proceso(OperadorLógico.Menor.ToString()));
            this.Operaciones.Add(new Proceso(OperadorLógico.Igual.ToString()));
            this.Operaciones.Add(new Proceso(OperadorLógico.Diferente.ToString()));
            this.Operaciones.Add(new Proceso(OperadorLógico.Y.ToString()));
            this.Operaciones.Add(new Proceso(OperadorLógico.O.ToString()));
            #endregion

            #region OperadorFecha
            this.Operaciones.Add(new Proceso(OperadorFecha.AgregarMeses.ToString()));
            this.Operaciones.Add(new Proceso(OperadorFecha.ObtenerAño.ToString()));
            this.Operaciones.Add(new Proceso(OperadorFecha.Hoy.ToString()));
            #endregion

            #region Otro
            this.Operaciones.Add(new Proceso(Otro.AbrirParéntesis.ToString()));
            this.Operaciones.Add(new Proceso(Otro.CerrarParéntesis.ToString()));
            this.Operaciones.Add(new Proceso(Otro.Coma.ToString()));
            this.Operaciones.Add(new Proceso(Otro.Decimal.ToString()));
            this.Operaciones.Add(new Proceso(Otro.Borrar.ToString()));
            #endregion

            foreach (Proceso _Operación in this.Operaciones)
            {
                _Operación.Resultados.Add(base.Atributos["Clave"]);
                _Operación.Resultados.Add(base.Atributos["Script"]);
                _Operación.Resultados.Add(base.Atributos["Vista"]);

                _Operación.Resultados.Add(base.Atributos["Expresiones"]);
            }
        }
        #endregion
        #endregion
        #endregion

        #region Atributos
        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Resultado", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("ComandoPrueba", TipoAtributo.Comando));

            base.Atributos.Add(new Atributo("Clave", TipoAtributo.Texto));
            base.Atributos.Add(new Atributo("Script", TipoAtributo.Texto));
            base.Atributos.Add(new Atributo("Vista", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("ExpresionesDisponibles", TipoAtributo.Grupo));
            base.Atributos.Add(new Atributo("Expresiones", TipoAtributo.Expresiones));

            base.Atributos.Add(new Atributo("Variables", TipoAtributo.Grupo));

        }
        #endregion
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Medición
{
    public class Cantidad : Extensión
    {
        #region Constructores
        #endregion

        #region Propiedades
        private Entidad Global { get { return this.Entidad.ObtenerSolución("Aplimática.Global"); } }

        public decimal Valor { get { return (decimal)base.Atributos["Cantidad"].Valor.Actual; } set { base.Atributos["Cantidad"].Valor.Actual = value; } }

        #region Unidad
        public Unidades Unidades { get { return (Unidades)base.Atributos["Unidad"].Colección; } set { base.Atributos["Unidad"].Colección = value; } }

        public Unidad Unidad { get { return (Unidad)this.Unidades.Obtener(); } }

        public string IdDeUnidad { get { return (string)base.Atributos["Unidad"].Valor.Actual; } set { base.Atributos["Unidad"].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Cantidad", TipoAtributo.Cantidad));

            base.Atributos.Add(new Atributo("Unidad", TipoAtributo.Registro) { Colección = new Unidades(this.Global) });
        }
        #endregion
    }
}
﻿
using Aplimática.Framework;

using Aplimática.Global.Medición;

using Aplimática.Global.Divisas;

namespace Aplimática.Global
{
    public class Imputación : Extensión
    {
        #region Constructores
        public Imputación() { }

        public Imputación(string Clave)
        {
            base.Identidad.Clave = Clave;
        }

        public Imputación(string Clave, string Nombre)
        {
            base.Identidad.Establecer(Clave, Nombre);
        }
        #endregion

        #region Propiedades
        private Identidad IdentidadDeGrupo = new Identidad();

        private Cantidad Cantidad = new Cantidad();

        private Importe Importe = new Importe();
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void Preparando()
        {
            base.Preparando();

            this.Extensiones.Agregar(this.Cantidad);

            this.Extensiones.Agregar(this.Importe);
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo(this.IdentidadDeGrupo.Clave, TipoAtributo.Grupo));

            this.Cantidad.PrepararAtributos();

            this.Importe.PrepararAtributos();
        }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Medición
{
    public enum MagnitudDeTiempo { Ninguno, No, Sí }

    public class Magnitud : Entidad
    {
        #region Constructores
        public Magnitud() : base() { }

        public Magnitud(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public MagnitudDeTiempo MagnitudDeTiempo { get { return (MagnitudDeTiempo)base.Atributos["MagnitudDeTiempo"].Valor.Actual; } set { base.Atributos["MagnitudDeTiempo"].Valor.Actual = (int)value; } }

        public Unidades Unidades { get { return (Unidades)base.Atributos["Unidades"].Colección; } set { base.Atributos["Unidades"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Magnitud(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Magnitud";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Magnitudes";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Unidad());
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("MagnitudDeTiempo", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Unidades", TipoAtributo.Carpeta) { Colección = new Unidades(this.Solución) });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "MagnitudDeTiempo")
            {
                Atributo.Literales.Add(new Elemento(MagnitudDeTiempo.No.GetHashCode().ToString(), MagnitudDeTiempo.No.ToString()));

                Atributo.Literales.Add(new Elemento(MagnitudDeTiempo.Sí.GetHashCode().ToString(), MagnitudDeTiempo.Sí.ToString()));
            }
        }
        #endregion
    }

    public class Magnitudes : Colección
    {
        #region Contructor
        public Magnitudes(Entidad Solución) : base(Solución, new Magnitud(Solución)) { }
        #endregion

        #region Propiedades
        #endregion

        #region Métodos
        public Magnitud Obtener(string Clave) { return (Magnitud)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Magnitud(this.Solución); }
        #endregion
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Aplimática.Framework;

namespace Aplimática.Global.Medición
{
    public enum TipoDeUnidad { Ninguno, Base, Múltiplo, Submúltiplo }

    public class Unidad : Entidad
    {
        #region Constructores
        public Unidad() : base() { }
        public Unidad(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Medida
        public Medidas Medidas { get { return (Medidas)base.Atributos["Medida"].Colección; } set { base.Atributos["Medida"].Colección = value; } }

        public Medida Medida { get { return (Medida)this.Medidas.Obtener(); } }

        public string IdDeMedida { get { return (string)base.Atributos["Medida"].Valor.Actual; } set { base.Atributos["Medida"].Valor.Actual = value; } }
        #endregion

        public TipoDeUnidad TipoDeUnidad { get { return (TipoDeUnidad)base.Atributos["TipoDeUnidad"].Valor.Actual; } set { base.Atributos["TipoDeUnidad"].Valor.Actual = (int)value; } }

        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public string Abreviatura { get { return (string)base.Atributos["Abreviatura"].Valor.Actual; } set { base.Atributos["Abreviatura"].Valor.Actual = value; } }

        public decimal Equivalencia { get { return (decimal)base.Atributos["Equivalencia"].Valor.Actual; } set { base.Atributos["Equivalencia"].Valor.Actual = value; } }

        #region Base
        public Unidades Bases { get { return (Unidades)base.Atributos["Base"].Colección; } set { base.Atributos["Base"].Colección = value; } }

        public Unidad Base { get { return (Unidad)this.Bases.Obtener(); } }

        public string IdDeBase { get { return (string)base.Atributos["Base"].Valor.Actual; } set { base.Atributos["Base"].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        public decimal ConvertirA(decimal Cantidad, Unidad Unidad)
        {
            return (Unidad.Equivalencia == 0 ? 0 : (Cantidad * this.Equivalencia) / Unidad.Equivalencia);
        }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Unidad(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Unidad";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Unidades";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;

            if (this.TipoDeUnidad != TipoDeUnidad.Base)
                this.Registro.Título += " (" + this.Equivalencia + " " + this.Base.Abreviatura + ")";

            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                this.Registro.Título += " (" + this.Abreviatura + ")";
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Medida", TipoAtributo.Registro) { Colección = new Medidas(this.Solución) });

            base.Atributos.Add(new Atributo("TipoDeUnidad", "Tipo de unidad", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Abreviatura", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("Equivalencia", TipoAtributo.Cantidad) { Dependencia = "TipoDeUnidad" });

            base.Atributos.Add(new Atributo("Base", TipoAtributo.Registro) { Colección = new Unidades(this.Solución) { Ninguno = true }, Dependencia = "TipoDeUnidad" });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "TipoDeUnidad")
            {
                Atributo.Literales.Add(new Elemento(TipoDeUnidad.Base.GetHashCode().ToString(), TipoDeUnidad.Base.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeUnidad.Múltiplo.GetHashCode().ToString(), TipoDeUnidad.Múltiplo.ToString()));

                Atributo.Literales.Add(new Elemento(TipoDeUnidad.Submúltiplo.GetHashCode().ToString(), TipoDeUnidad.Submúltiplo.ToString()));
            }
        }

        protected override void SeleccionandoColección(Atributo Carpeta)
        {
            base.SeleccionandoColección(Carpeta);

            if (Carpeta.Identidad.Clave == "Base")
                Carpeta.Colección.TextoComando.Fuente = "SELECT * FROM [Unidades] WHERE Medida = '" + this.IdDeMedida + "' AND TipoDeUnidad = " + TipoDeUnidad.Base.GetHashCode().ToString() + "";
        }

        protected override void Guardado()
        {
            base.Guardado();

            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                this.IdDeBase = this.Registro.Id;
        }

        #region RestringiendoAtributo
        private void RestringirBase(Atributo Atributo)
        {
            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                Atributo.Visibilidad = TipoAccesibilidad.No;

            else Atributo.Visibilidad = TipoAccesibilidad.Automático;

            Atributo.Disponibilidad = TipoAccesibilidad.No;

            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                this.Bases.Ninguno = true;

            else this.Bases.Ninguno = false;
        }

        private void RestringirEquivalencia(Atributo Atributo)
        {
            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                Atributo.Visibilidad = TipoAccesibilidad.No;

            else Atributo.Visibilidad = TipoAccesibilidad.Automático;

            if (this.TipoDeUnidad == TipoDeUnidad.Base)
                this.Equivalencia = 1;
        }

        protected override void RestringiendoAtributo(Atributo Atributo)
        {
            base.RestringiendoAtributo(Atributo);

            if (Atributo.Identidad.Clave == "Equivalencia")
                this.RestringirEquivalencia(Atributo);

            if (Atributo.Identidad.Clave == "Base")
                this.RestringirBase(Atributo);
        }
        #endregion
        #endregion
    }

    public class Unidades : Colección
    {
        #region Constructores
        public Unidades(Entidad Solución) : base(Solución, new Unidad(Solución)) { }
        #endregion

        #region Propiedades
        #endregion

        #region Métodos
        public Unidad Obtener(string Clave) { return (Unidad)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Unidad(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Medición
{
    public enum MedidaEspecífica { Cero, Ninguno, Cantidad, Tiempo }

    public class Medida : Entidad
    {
        #region Constructores
        public Medida() : base() { }

        public Medida(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }

        public MedidaEspecífica MedidaEspecífica { get { return (MedidaEspecífica)base.Atributos["MedidaEspecífica"].Valor.Actual; } set { base.Atributos["MedidaEspecífica"].Valor.Actual = (int)value; } }

        public Unidades Unidades { get { return (Unidades)base.Atributos["Unidades"].Colección; } set { base.Atributos["Unidades"].Colección = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Medida(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Medida";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Medidas";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Unidad());
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));

            base.Atributos.Add(new Atributo("MedidaEspecífica", TipoAtributo.Enumeración));

            base.Atributos.Add(new Atributo("Unidades", TipoAtributo.Carpeta) { Colección = new Unidades(this.Solución) });
        }

        protected override void PreparandoLiterales(Atributo Atributo)
        {
            base.PreparandoLiterales(Atributo);

            if (Atributo.Identidad.Clave == "MedidaEspecífica")
            {
                Atributo.Literales.Add(new Elemento(MedidaEspecífica.Ninguno.GetHashCode().ToString(), MedidaEspecífica.Ninguno.ToString()));

                Atributo.Literales.Add(new Elemento(MedidaEspecífica.Cantidad.GetHashCode().ToString(), MedidaEspecífica.Cantidad.ToString()));

                Atributo.Literales.Add(new Elemento(MedidaEspecífica.Tiempo.GetHashCode().ToString(), MedidaEspecífica.Tiempo.ToString()));
            }
        }
        #endregion
    }

    public class Medidas : Colección
    {
        #region Contructor
        public Medidas(Entidad Solución) : base(Solución, new Medida(Solución)) { }
        #endregion

        #region Propiedades
        #endregion

        #region Métodos
        public Medida Obtener(string Clave) { return (Medida)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Medida(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Medición
{
    public class Tiempo : Extensión
    {
        #region Constructores
        public Tiempo() { }

        public Tiempo(string Clave)
        {
            base.Identidad.Clave = Clave;

            this.IdentidadDeTiempo.Establecer("CantidadDe" + Clave, this.IdentidadDeTiempo.Nombre);

            this.IdentidadDeUnidad.Establecer("UnidadDe" + Clave, this.IdentidadDeUnidad.Nombre);
        }

        public Tiempo(string Clave, string Nombre)
        {
            base.Identidad.Establecer(Clave, Nombre);

            this.IdentidadDeTiempo.Establecer("CantidadDe" + Clave, this.IdentidadDeTiempo.Nombre);

            this.IdentidadDeUnidad.Establecer("UnidadDe" + Clave, this.IdentidadDeUnidad.Nombre);
        }
        #endregion

        #region Propiedades
        public Identidad IdentidadDeTiempo = new Identidad("Cantidad");

        public Identidad IdentidadDeUnidad = new Identidad("Unidad");

        private Entidad Global { get { return this.Entidad.ObtenerSolución("Aplimática.Global"); } }

        public decimal Valor { get { return (decimal)base.Atributos[this.IdentidadDeTiempo.Clave].Valor.Actual; } set { base.Atributos[this.IdentidadDeTiempo.Clave].Valor.Actual = value; } }

        #region Unidad
        public Unidades Unidades { get { return (Unidades)base.Atributos[this.IdentidadDeUnidad.Clave].Colección; } set { base.Atributos[this.IdentidadDeUnidad.Clave].Colección = value; } }

        public Unidad Unidad { get { return (Unidad)this.Unidades.Obtener(); } }

        public string IdDeUnidad { get { return (string)base.Atributos[this.IdentidadDeUnidad.Clave].Valor.Actual; } set { base.Atributos[this.IdentidadDeUnidad.Clave].Valor.Actual = value; } }
        #endregion
        #endregion

        #region Métodos
        public void EstablecerValoresDesde(Tiempo Tiempo)
        {
            this.Valor = Tiempo.Valor;

            this.IdDeUnidad = Tiempo.IdDeUnidad;
        }
        #endregion

        #region Eventos
        protected override void Identificando()
        {
            base.Identificando();

            this.TipoAtributo = TipoAtributo.Grupo;

            //base.Identidad.Clave = "Tiempo";
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo(this.IdentidadDeTiempo.Clave, this.IdentidadDeTiempo.Nombre, TipoAtributo.Cantidad));

            base.Atributos.Add(new Atributo(this.IdentidadDeUnidad.Clave, this.IdentidadDeUnidad.Nombre, TipoAtributo.Registro) { Colección = new Unidades(this.Global) });
        }

        #region SeleccionandoColección
        private void SeleccionarUnidades()
        {
            this.Unidades.TextoComando.Fuente = "SELECT * FROM Unidades WHERE Medida IN (SELECT Id FROM Medidas WHERE MedidaEspecífica = " + MedidaEspecífica.Tiempo.GetHashCode().ToString() + ")";
        }

        protected override void SeleccionandoColección(Atributo Atributo)
        {
            base.SeleccionandoColección(Atributo);

            if (Atributo.Identidad.Clave == this.IdentidadDeUnidad.Clave)
                this.SeleccionarUnidades();
        }
        #endregion
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global.Diccionario
{
    public class Nivel : Entidad
    {
        #region Constructores
        public Nivel() : base() { }

        public Nivel(Entidad Solución) : base(Solución) { }
        #endregion

        #region Propiedades
        #region Superior
        public Niveles Superiores { get { return (Niveles)base.Atributos["Superior"].Colección; } set { base.Atributos["Superior"].Colección = value; } }

        public Nivel Superior { get { return (Nivel)this.Superiores.Obtener(); } }

        public string IdSuperior { get { return (string)base.Atributos["Superior"].Valor.Actual; } set { base.Atributos["Superior"].Valor.Actual = value; } }
        #endregion

        public string Nombre { get { return (string)base.Atributos["Nombre"].Valor.Actual; } set { base.Atributos["Nombre"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Nivel(this.Solución); }

        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Registro;

            base.Identidad.Clave = "Nivel";
        }

        protected override void PreparandoRegistro()
        {
            base.PreparandoRegistro();

            this.Registro.Tabla.Nombre = "Niveles";
        }

        protected override void GenerandoTítulo()
        {
            base.GenerandoTítulo();

            this.Registro.Título = this.Nombre;
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Superior", TipoAtributo.Registro) { Colección = new Niveles(this.Solución) });

            base.Atributos.Add(new Atributo("Nombre", TipoAtributo.Texto));
        }
        #endregion
    }

    public class Niveles : Colección
    {
        #region Constructores
        public Niveles(Entidad Solución) : base(Solución, new Nivel(Solución)) { }
        #endregion

        #region Métodos
        public Nivel Obtener(string Clave) { return (Nivel)this.ObtenerX(Clave); }
        #endregion

        #region Eventos
        protected override void AgregandoNuevoEntidad() { base.AgregandoNuevoEntidad(); this.NuevoEntidad = new Nivel(this.Solución); }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global
{
    public class Numeración : Extensión
    {
        #region Constructores
        #endregion

        #region Propiedades
        #region Correlativo
        public Correlativos Correlativos { get { return (Correlativos)base.Atributos["Correlativo"].Colección; } set { base.Atributos["Correlativo"].Colección = value; } }

        public Correlativo Correlativo { get { return (Correlativo)this.Correlativos.Obtener(); } }

        public string IdCorrelativo { get { return (string)base.Atributos["Correlativo"].Valor.Actual; } set { base.Atributos["Correlativo"].Valor.Actual = value; } }
        #endregion

        public int Número { get { return (int)base.Atributos["Número"].Valor.Actual; } set { base.Atributos["Número"].Valor.Actual = value; } }
        #endregion

        #region Métodos
        public void Generar()
        {
            this.Número = this.Correlativo.Generar();
        }
        #endregion

        #region Eventos
        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Numeración", TipoAtributo.Grupo));

            base.Atributos.Add(new Atributo("Correlativo", TipoAtributo.Registro));

            base.Atributos.Add(new Atributo("Número", TipoAtributo.Entero) { Disponibilidad = TipoAccesibilidad.No });
        }

        protected override void PreparandoColección(Atributo Atributo)
        {
            base.PreparandoColección(Atributo);

            if (Atributo.Identidad.Clave == "Correlativo")
                this.Correlativos = new Correlativos(this.Entidad.Solución);
        }
        #endregion
    }
}
﻿
using Aplimática.Framework;

namespace Aplimática.Global
{
    public class Solución : Entidad
    {
        #region Constructores
        #endregion

        #region Propiedades
        #endregion

        #region Métodos
        #endregion

        #region Eventos 
        protected override void Identificando()
        {
            base.Identificando();

            base.TipoEntidad = TipoEntidad.Solución;

            #region Identidad
            base.Identidad.Clave = "Aplimática.Global";

            base.Identidad.Nombre = "Global";
            #endregion
        }

        protected override void PreparandoEntidades()
        {
            base.PreparandoEntidades();

            #region Divisas
            base.Entidades.Add(new Divisas.Moneda());

            base.Entidades.Add(new Divisas.Cambio());
            #endregion

            #region Medición
            base.Entidades.Add(new Medición.Medida());

            base.Entidades.Add(new Medición.Unidad());
            #endregion

            #region Directorio
            base.Entidades.Add(new Directorio.Persona());

            base.Entidades.Add(new Directorio.Teléfono());

            base.Entidades.Add(new Directorio.Dirección());

            base.Entidades.Add(new Directorio.Contacto());

            base.Entidades.Add(new Directorio.CorreoElectrónico());
            #endregion

            #region Diccionario
            base.Entidades.Add(new Diccionario.Dato());

            base.Entidades.Add(new Diccionario.Opción());
            #endregion

            base.Entidades.Add(new Correlativo());
        }

        protected override void PreparandoAccesosDirectos()
        {
            base.PreparandoAccesosDirectos();

            base.AccesosDirectos.Agregar(new Divisas.Moneda(this));

            base.AccesosDirectos.Agregar(new Medición.Medida(this));

            base.AccesosDirectos.Agregar(new Directorio.Persona(this));

            base.AccesosDirectos.Agregar(new Diccionario.Dato(this));

            base.AccesosDirectos.Agregar(new Correlativo(this));
        }

        protected override void PreparandoAtributos()
        {
            base.PreparandoAtributos();

            base.Atributos.Add(new Atributo("Monedas", TipoAtributo.Carpeta) { Colección = new Divisas.Monedas(this.Solución) });

            base.Atributos.Add(new Atributo("Medidas", TipoAtributo.Carpeta) { Colección = new Medición.Medidas(this.Solución) });

            base.Atributos.Add(new Atributo("Personas", TipoAtributo.Carpeta) { Colección = new Directorio.Personas(this.Solución) });

            base.Atributos.Add(new Atributo("Datos", TipoAtributo.Carpeta) { Colección = new Diccionario.Datos(this) });

            base.Atributos.Add(new Atributo("Correlativos", TipoAtributo.Carpeta));
        }
        #endregion
    }
}
