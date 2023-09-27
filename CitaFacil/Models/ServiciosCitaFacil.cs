using CitaFacil.Data;
using CitaFacil.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CitaFacil.Models
{
    public class ServiciosCitaFacil
    {
        private readonly CitaFacilContext _context;
        private readonly IEmailService _emailService;

        public ServiciosCitaFacil(CitaFacilContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public ServiciosCitaFacil(CitaFacilContext context)
        {
            _context = context;
        }

        public static string CifrarContraseña(string contraseña)
        {
            using(SHA256 sha256Hash=SHA256.Create())
            {
                //Convertir la contraseña en un array de bytes y calcular el hash
                byte[] bytesContraseña=Encoding.UTF8.GetBytes(contraseña);
                byte[] hashContraseña=sha256Hash.ComputeHash(bytesContraseña);

                //convetir el hash en una cadena hexadecimal
                StringBuilder builder=new StringBuilder();
                for(int i=0;i<hashContraseña.Length;i++)
                {
                    builder.Append(hashContraseña[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public Registrar_Cliente comprobarContraseñaCliente(string correo, string contraseña)
        {
            Registrar_Cliente cliente = _context.Registrar_Cliente.Include(c => c.Id_Rol).FirstOrDefault(c => c.Correo == correo);
            if (cliente != null && contraseña!=null)
            {
                contraseña=CifrarContraseña(contraseña);
                if (cliente.passsword == CifrarContraseña(contraseña))
                {
                    return cliente;
                }
                else {                    
                    return null;
                }
            }
            else { 
            return null;
            }
        }
        public Registrar_Negocio ComprobarContraseñaNegocio(string correo, string contraseña)
        {
            Registrar_Negocio negocio = _context.Registrar_Negocio.Include(c => c.Id_Rol).FirstOrDefault(c => c.Correo == correo);
            if (negocio != null && contraseña != null)
            {
                contraseña = CifrarContraseña(contraseña);
                if (negocio.passsword == CifrarContraseña(contraseña))
                {
                    return negocio;
                }
                else
                {
                    return null;
                }
            }else
            {
                return null;
            }
        }
        public void EnviarCorreoCliente(string correo)
        {
            Registrar_Cliente cliente = _context.Registrar_Cliente.Include(r => r.Id_Rol).FirstOrDefault(r => r.Correo == correo);
            string fechaActual=DateTime.Now.ToString("dd/MM/yyyy");
            string cuerpo="Hola "+cliente.Primer_Nombre+cliente.Primer_Apellido+"! \n\n"+
                "Te damos la bienvenida a CitaFacil, la plataforma que te permite gestionar tus citas de una manera fácil y rápida. \n\n"+
                "Tu registro se ha realizado con éxito. \n\n"+
                "Fecha de registro: "+fechaActual+"\n\n"+
                "¡Gracias por confiar en nosotros!";
            EMail solicitud= new EMail();
            solicitud.Para=cliente.Correo;
            solicitud.Asunto="Bienvenido a CitaFacil";
            solicitud.Contenido=cuerpo;
            _emailService.SendEmail(solicitud);
        }
        public void EnviarCorreoNegocio(string correo)
        {
            Registrar_Negocio negocio = _context.Registrar_Negocio.Include(r => r.Id_Rol).FirstOrDefault(r => r.Correo == correo);
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            string cuerpo = "Hola " + negocio.Nombre + "! \n\n" +
                "Te damos la bienvenida a CitaFacil, la plataforma que te permite gestionar tus citas de una manera fácil y rápida. \n\n" +
                "Tu registro se ha realizado con éxito. \n\n" +
                "Fecha de registro: " + fechaActual + "\n\n" +
                "¡Gracias por confiar en nosotros!";
            EMail solicitud = new EMail();
            solicitud.Para = negocio.Correo;
            solicitud.Asunto = "Bienvenido a CitaFacil";
            solicitud.Contenido = cuerpo;
            _emailService.SendEmail(solicitud);
        }
    }
}
