using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSTerceros.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace WSTerceros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration _configuration;

        public PersonaController(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this._configuration = configuration;
        }

        [Route("ConsultarPersonas")]
        [HttpPost()]
        public IActionResult ConsultarPersonas()
        {
            return Ok(context.tblPersona);
        }

        [Route("ConsultarPersonaCedula")]
        [HttpPost]
        public IActionResult ConsultarPersonaCedula([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var personaIdentificada = context.tblPersona.FirstOrDefault(identificacionPersona => identificacionPersona.Identificacion == persona.Identificacion);
            context.SaveChanges();

            return new CreatedAtRouteResult("personaIdentificada", new { personaIdentificada });
        }

        [Route("CrearPersona")]
        [HttpPost]
        public IActionResult CrearPersona([FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ejecutarSpCrearTercero(persona);
            context.tblPersona.Add(persona);
            context.SaveChanges();
            

            return new CreatedAtRouteResult("personaCreada", new { id = persona.Id });
        }

        [Route("ActualizarPersona")]
        [HttpPost]
        public IActionResult ActualizarPersona([FromBody] Persona persona)
        {
            if (persona.Id == 0)
            {
                return BadRequest();
            }
            var datosPersona = context.tblPersona.First(x => x.Id == persona.Id);
            datosPersona.Id = persona.Id;
            if (datosPersona != null)
            {
               context.Entry(datosPersona).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(datosPersona);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("EliminarPersona")]
        [HttpPost]
        public IActionResult EliminarPersona([FromBody] Persona persona)
        {
            var idPersona = context.tblPersona.FirstOrDefault(x => x.Id == persona.Id);
            if (idPersona == null)
            {
                return NotFound();
            }
            context.tblPersona.Remove(persona);
            context.SaveChanges();
            return Ok(persona);
        }

        private void ejecutarSpCrearTercero(Persona persona)
        {
            StringBuilder errorMessages = new StringBuilder();

            try
            {
                string spName = @"dbo.[sp_persona_i]";
                string conn = _configuration.GetConnectionString("DefaultConnection");
                SqlConnection connection = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand(spName, connection);

                SqlParameter nombre = new SqlParameter();
                nombre.ParameterName = "@pr_strNombre_PERS";
                nombre.SqlDbType = SqlDbType.VarChar;
                if (persona.SegundoNombre == null)
                {
                    nombre.Value = persona.PrimerNombre;
                }
                else
                {
                    nombre.Value = persona.PrimerNombre + " " + persona.SegundoNombre;
                }

                SqlParameter operacion = new SqlParameter();
                operacion.ParameterName = "@i_operacion";
                operacion.SqlDbType = SqlDbType.VarChar;
                operacion.Value = null;

                SqlParameter codigo = new SqlParameter();
                codigo.ParameterName = "@pr_intCodigo_PERS";
                codigo.SqlDbType = SqlDbType.Int;
                codigo.Value = 0;

                SqlParameter documento = new SqlParameter();
                documento.ParameterName = "@pr_strDocumento_PERS";
                documento.SqlDbType = SqlDbType.VarChar;
                documento.Value = persona.Identificacion;

                SqlParameter primerApellido = new SqlParameter();
                primerApellido.ParameterName = "@pr_strPApellido_PERS";
                primerApellido.SqlDbType = SqlDbType.VarChar;
                primerApellido.Value = persona.PrimerApellido;

                SqlParameter segundoApellido = new SqlParameter();
                segundoApellido.ParameterName = "@pr_strSApellido_PERS";
                segundoApellido.SqlDbType = SqlDbType.VarChar;
                segundoApellido.Value = persona.SegundoApellido;

                SqlParameter fechaNacimiento = new SqlParameter();
                fechaNacimiento.ParameterName = "@pr_dtmFechaNac_PERS";
                fechaNacimiento.SqlDbType = SqlDbType.DateTime;
                fechaNacimiento.Value = DateTime.Now;

                SqlParameter fechaExpedicion = new SqlParameter();
                fechaExpedicion.ParameterName = "@pr_dtmFechaExDoc_PERS";
                fechaExpedicion.SqlDbType = SqlDbType.DateTime;
                fechaExpedicion.Value = DateTime.Now;

                SqlParameter email = new SqlParameter();
                email.ParameterName = "@pr_strEmail_PERS";
                email.SqlDbType = SqlDbType.VarChar;
                email.Value = "usuario@usuario.com";

                SqlParameter direccion = new SqlParameter();
                direccion.ParameterName = "@pr_strDireccion_PERS";
                direccion.SqlDbType = SqlDbType.VarChar;
                direccion.Value = persona.Direccion;

                SqlParameter direccionCor = new SqlParameter();
                direccionCor.ParameterName = "@pr_strDireccionCor_PERS";
                direccionCor.SqlDbType = SqlDbType.VarChar;
                direccionCor.Value = "Ninguno";

                SqlParameter tipoSangre = new SqlParameter();
                tipoSangre.ParameterName = "@pr_strTipoSangre_PERS";
                tipoSangre.SqlDbType = SqlDbType.VarChar;
                tipoSangre.Value = null;

                SqlParameter rh = new SqlParameter();
                rh.ParameterName = "@pr_strRh_PERS";
                rh.SqlDbType = SqlDbType.VarChar;
                rh.Value = null;

                SqlParameter hijos = new SqlParameter();
                hijos.ParameterName = "@pr_intHijos_PERS";
                hijos.SqlDbType = SqlDbType.Int;
                hijos.Value = 0;

                SqlParameter certificado = new SqlParameter();
                certificado.ParameterName = "@pr_strNumCertificadoJu_PERS";
                certificado.SqlDbType = SqlDbType.VarChar;
                certificado.Value = null;

                SqlParameter fechaCertificado = new SqlParameter();
                fechaCertificado.ParameterName = "@pr_dtmFechaVtoCerti_PERS";
                fechaCertificado.SqlDbType = SqlDbType.DateTime;
                fechaCertificado.Value = DateTime.Now;

                SqlParameter numeroLicencia = new SqlParameter();
                numeroLicencia.ParameterName = "@pr_strNumLicencia_PERS";
                numeroLicencia.SqlDbType = SqlDbType.VarChar;
                numeroLicencia.Value = null;

                SqlParameter fechaLicencia = new SqlParameter();
                fechaLicencia.ParameterName = "@pr_dtmFechaVenceLic_PERS";
                fechaLicencia.SqlDbType = SqlDbType.DateTime;
                fechaLicencia.Value = DateTime.Now;

                SqlParameter codigoTido = new SqlParameter();
                codigoTido.ParameterName = "@pr_intCodigo_TIDO";
                codigoTido.SqlDbType = SqlDbType.Int;
                codigoTido.Value = 0;

                SqlParameter codigoGene = new SqlParameter();
                codigoGene.ParameterName = "@pr_intCodigo_GENE";
                codigoGene.SqlDbType = SqlDbType.Int;
                codigoGene.Value = 0;

                SqlParameter codigoCiud = new SqlParameter();
                codigoCiud.ParameterName = "@pr_intCodigo_CIUD";
                codigoCiud.SqlDbType = SqlDbType.Int;
                codigoCiud.Value = 0;

                SqlParameter codigoEsci = new SqlParameter();
                codigoEsci.ParameterName = "@pr_intCodigo_ESCI";
                codigoEsci.SqlDbType = SqlDbType.Int;
                codigoEsci.Value = 0;

                SqlParameter codigoAcec = new SqlParameter();
                codigoAcec.ParameterName = "@pr_intCodigo_ACEC";
                codigoAcec.SqlDbType = SqlDbType.Int;
                codigoAcec.Value = 0;

                SqlParameter codigoBarr = new SqlParameter();
                codigoBarr.ParameterName = "@pr_intCodigo_BARR";
                codigoBarr.SqlDbType = SqlDbType.Int;
                codigoBarr.Value = 0;

                SqlParameter codigoTivi = new SqlParameter();
                codigoTivi.ParameterName = "@pr_intCodigo_TIVI";
                codigoTivi.SqlDbType = SqlDbType.Int;
                codigoTivi.Value = 0;

                SqlParameter conductor = new SqlParameter();
                conductor.ParameterName = "@pr_blnConductor_PERS";
                conductor.SqlDbType = SqlDbType.Bit;
                conductor.Value = 0;

                SqlParameter propietario = new SqlParameter();
                propietario.ParameterName = "@pr_blnPropietario_PERS";
                propietario.SqlDbType = SqlDbType.Bit;
                propietario.Value = 0;

                SqlParameter administrador = new SqlParameter();
                administrador.ParameterName = "@pr_blnAdministrador_PERS";
                administrador.SqlDbType = SqlDbType.Bit;
                administrador.Value = 0;

                SqlParameter exento = new SqlParameter();
                exento.ParameterName = "@pr_blnExentoPara_PERS";
                exento.SqlDbType = SqlDbType.Bit;
                exento.Value = 0;

                SqlParameter runt = new SqlParameter();
                runt.ParameterName = "@pr_blnRunt_PERS";
                runt.SqlDbType = SqlDbType.Bit;
                runt.Value = 0;

                SqlParameter fechaRunt = new SqlParameter();
                fechaRunt.ParameterName = "@pr_blnMesExp_PERS";
                fechaRunt.SqlDbType = SqlDbType.Bit;
                fechaRunt.Value = 0;

                SqlParameter telResidencia = new SqlParameter();
                telResidencia.ParameterName = "@pr_strTelResidencia_PERS";
                telResidencia.SqlDbType = SqlDbType.VarChar;
                telResidencia.Value = persona.Telefono1;

                SqlParameter telCelular = new SqlParameter();
                telCelular.ParameterName = "@pr_strTelCelular_PERS";
                telCelular.SqlDbType = SqlDbType.VarChar;
                telCelular.Value = persona.TelefonoCelular;

                SqlParameter telAlternativo = new SqlParameter();
                telAlternativo.ParameterName = "@pr_strTelAlternativo_PERS";
                telAlternativo.SqlDbType = SqlDbType.VarChar;
                telAlternativo.Value = persona.Telefono3;

                SqlParameter user = new SqlParameter();
                user.ParameterName = "@sUser";
                user.SqlDbType = SqlDbType.VarChar;
                user.Value = null;

                cmd.Parameters.Add(nombre);
                cmd.Parameters.Add(operacion);
                cmd.Parameters.Add(codigo);
                cmd.Parameters.Add(documento);
                cmd.Parameters.Add(primerApellido);
                cmd.Parameters.Add(segundoApellido);
                cmd.Parameters.Add(fechaNacimiento);
                cmd.Parameters.Add(fechaExpedicion);
                cmd.Parameters.Add(email);
                cmd.Parameters.Add(direccion);
                cmd.Parameters.Add(direccionCor);
                cmd.Parameters.Add(tipoSangre);
                cmd.Parameters.Add(rh);
                cmd.Parameters.Add(hijos);
                cmd.Parameters.Add(certificado);
                cmd.Parameters.Add(fechaCertificado);
                cmd.Parameters.Add(numeroLicencia);
                cmd.Parameters.Add(fechaLicencia);
                cmd.Parameters.Add(codigoTido);
                cmd.Parameters.Add(codigoGene);
                cmd.Parameters.Add(codigoCiud);
                cmd.Parameters.Add(codigoEsci);
                cmd.Parameters.Add(codigoAcec);
                cmd.Parameters.Add(codigoBarr);
                cmd.Parameters.Add(codigoTivi);
                cmd.Parameters.Add(conductor);
                cmd.Parameters.Add(propietario);
                cmd.Parameters.Add(administrador);
                cmd.Parameters.Add(exento);
                cmd.Parameters.Add(runt);
                cmd.Parameters.Add(fechaRunt);
                cmd.Parameters.Add(telResidencia);
                cmd.Parameters.Add(telCelular);
                cmd.Parameters.Add(telAlternativo);
                cmd.Parameters.Add(user);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Console.WriteLine(Environment.NewLine + "El procedimiento almacenado se ejecutó correctamente." + Environment.NewLine);
                connection.Close();

            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
        }

    }
}