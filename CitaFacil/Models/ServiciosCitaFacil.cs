using CitaFacil.Data;
using CitaFacil.Models.DTO;
using CitaFacil.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

        public  string CifrarContraseña(string contraseña)
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

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public Usuario comprobarContraseña(string correo, string contraseña)
        {
            Usuario cliente = _context.Usuario.Include(c => c.Id_Rol).FirstOrDefault(c => c.Correo == correo);
            if (cliente != null && contraseña!=null)
            {
                contraseña=CifrarContraseña(contraseña);
                if (cliente.contraseña == CifrarContraseña(contraseña))
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
        public Empresas ComprobarContraseñaNegocio(string correo, string contraseña)
        {
            Empresas negocio = _context.Empresa.Include(c => c.Id_Rol).FirstOrDefault(c => c.Correo == correo);
            if (negocio != null && contraseña != null)
            {
                contraseña = CifrarContraseña(contraseña);
                if (negocio.contraseña == CifrarContraseña(contraseña))
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
        public void EnviarCorreo(string correo)
        {
            string cuerpo = @"<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office""><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""><meta http-equiv=""X-UA-Compatible"" content=""IE=edge""><meta name=""format-detection"" content=""telephone=no""><meta name=""viewport"" content=""width=device-width, initial-scale=1.0""><title>¡Descubre la facilidad de CitaFacil para gestionar tus citas de belleza!</title><style type=""text/css"" emogrify=""no"">#outlook a { padding:0; } .ExternalClass { width:100%; } .ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div { line-height: 100%; } table td { border-collapse: collapse; mso-line-height-rule: exactly; } .editable.image { font-size: 0 !important; line-height: 0 !important; } .nl2go_preheader { display: none !important; mso-hide:all !important; mso-line-height-rule: exactly; visibility: hidden !important; line-height: 0px !important; font-size: 0px !important; } body { width:100% !important; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%; margin:0; padding:0; } img { outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; } a img { border:none; } table { border-collapse:collapse; mso-table-lspace:0pt; mso-table-rspace:0pt; } th { font-weight: normal; text-align: left; } *[class=""gmail-fix""] { display: none !important; } </style><style type=""text/css"" emogrify=""no""> @media (max-width: 600px) { .gmx-killpill { content: ' \03D1';} } </style><style type=""text/css"" emogrify=""no"">@media (max-width: 600px) { .gmx-killpill { content: ' \03D1';} .r0-o { border-style: solid !important; margin: 0 auto 0 auto !important; width: 100% !important } .r1-i { background-color: transparent !important } .r2-c { box-sizing: border-box !important; text-align: center !important; valign: top !important; width: 320px !important } .r3-o { border-style: solid !important; margin: 0 auto 0 auto !important; width: 320px !important } .r4-i { padding-bottom: 5px !important; padding-top: 5px !important } .r5-c { box-sizing: border-box !important; display: block !important; valign: top !important; width: 100% !important } .r6-o { border-style: solid !important; width: 100% !important } .r7-i { padding-left: 0px !important; padding-right: 0px !important } .r8-c { box-sizing: border-box !important; text-align: center !important; width: 100% !important } .r9-i { padding-bottom: 13px !important; padding-left: 10px !important; padding-right: 10px !important; padding-top: 15px !important; text-align: center !important } .r10-i { background-color: #d8d8d8 !important } .r11-c { box-sizing: border-box !important; text-align: center !important; valign: top !important; width: 100% !important } .r12-i { background-color: #ffffff !important; padding-left: 10px !important; padding-right: 10px !important; padding-top: 32px !important } .r13-c { box-sizing: border-box !important; text-align: left !important; valign: top !important; width: 100% !important } .r14-o { border-style: solid !important; margin: 0 auto 0 0 !important; width: 100% !important } .r15-i { text-align: center !important } .r16-i { background-color: #ffffff !important; padding-left: 0px !important; padding-right: 0px !important; padding-top: 30px !important } .r17-c { box-sizing: border-box !important; valign: top !important; width: 50% !important } .r18-i { padding-left: 5px !important; padding-right: 5px !important } .r19-c { box-sizing: border-box !important; text-align: center !important; valign: top !important; width: 72px !important } .r20-o { border-style: solid !important; margin: 0 auto 0 auto !important; width: 72px !important } .r21-i { padding-bottom: 0px !important; padding-left: 0px !important; padding-right: 0px !important; padding-top: 0px !important } .r22-i { background-color: #ffffff !important; padding-left: 10px !important; padding-right: 10px !important; padding-top: 30px !important } .r23-i { padding-left: 10px !important; padding-right: 10px !important } .r24-i { text-align: left !important } .r25-i { padding-top: 28px !important; text-align: left !important } .r26-i { background-color: #343434 !important; padding-bottom: 25px !important; padding-left: 10px !important; padding-right: 10px !important } .r27-i { padding-top: 25px !important; text-align: center !important } .r28-i { background-color: #D8D8D8 !important; padding-bottom: 15px !important; padding-top: 13px !important } body { -webkit-text-size-adjust: none } .nl2go-responsive-hide { display: none } .nl2go-body-table { min-width: unset !important } .mobshow { height: auto !important; overflow: visible !important; max-height: unset !important; visibility: visible !important; border: none !important } .resp-table { display: inline-table !important } .magic-resp { display: table-cell !important } } </style><!--[if !mso]><!--><style type=""text/css"" emogrify=""no"">@import url(""https://fonts.googleapis.com/css?family=Josefin+Sans:400,700|Lato:400,700&display=swap""); @import url(""https://fonts.googleapis.com/css2?family=S""); </style><!--<![endif]--><style type=""text/css"">p, h1, h2, h3, h4, ol, ul { margin: 0; } a, a:link { color: #100d0d; text-decoration: none } .nl2go-default-textstyle { color: #100D0D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word } .default-button { color: #ffffff; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; font-style: normal; font-weight: normal; line-height: 1.15; text-decoration: none; word-break: break-word } .nl2go_class_14_black_reg_u { color: #100D0D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 14px; text-decoration: underline; word-break: break-word } .nl2go_class_32_black_josefinsans_reg { color: #343434; font-family: Josefin Sans, Lato, Arial, Helvetica, sans-serif; font-size: 34px; word-break: break-word } .nl2go_class_20_black_b { color: #100D0D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: 700; word-break: break-word } .nl2go_class_20_black_reg { color: #100D0D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; word-break: break-word } .nl2go_class_20_white_reg { color: #FFFFFF; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; word-break: break-word } .nl2go_class_16_white_reg { color: #FFFFFF; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 16px; word-break: break-word } .nl2go_class_14_black_reg { color: #100D0D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 14px; word-break: break-word } .default-heading1 { color: #1F2D3D; font-family: Josefin Sans, Lato, Arial, Helvetica, sans-serif; font-size: 34px; word-break: break-word } .default-heading2 { color: #1F2D3D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 20px; word-break: break-word } .default-heading3 { color: #1F2D3D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 24px; word-break: break-word } .default-heading4 { color: #1F2D3D; font-family: Lato, Arial, Helvetica, sans-serif; font-size: 18px; word-break: break-word } a[x-apple-data-detectors] { color: inherit !important; text-decoration: inherit !important; font-size: inherit !important; font-family: inherit !important; font-weight: inherit !important; line-height: inherit !important; } .no-show-for-you { border: none; display: none; float: none; font-size: 0; height: 0; line-height: 0; max-height: 0; mso-hide: all; overflow: hidden; table-layout: fixed; visibility: hidden; width: 0; } </style><!--[if mso]><xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings> </xml><![endif]--><style type=""text/css"">a:link{color: #100d0d; text-decoration: none;}</style></head><body bgcolor=""#d8d8d8"" text=""#100D0D"" link=""#100d0d"" yahoo=""fix"" style=""background-color: #d8d8d8;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" class=""nl2go-body-table"" width=""100%"" style=""background-color: #d8d8d8; width: 100%;""><tr><td> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r1-i"" style=""background-color: transparent;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""600"" align=""center"" class=""r3-o"" style=""table-layout: fixed;""><tr><td class=""r4-i"" style=""padding-bottom: 5px; padding-top: 5px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""100%"" valign=""top"" class=""r5-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r7-i"" style=""padding-left: 10px; padding-right: 10px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r8-c"" align=""center""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""center"" class=""r9-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word; padding-bottom: 13px; padding-left: 30px; padding-right: 30px; padding-top: 15px; text-align: center;""> <div><p style=""margin: 0;""><a href=""{{ mirror }}"" style=""color: #100d0d; text-decoration: none;""><span style=""font-size: 14px;""><u>Ver en navegador</u></span></a></p></div> </td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table></td> </tr></table><table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""600"" align=""center"" class=""r3-o"" style=""table-layout: fixed; width: 600px;""><tr><td valign=""top"" class=""r10-i"" style=""background-color: #d8d8d8;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td class=""r12-i"" style=""background-color: #ffffff; padding-left: 20px; padding-right: 20px; padding-top: 32px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""100%"" valign=""top"" class=""r5-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r7-i""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r13-c"" align=""left""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r14-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""center"" valign=""top"" class=""r15-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; word-break: break-word; line-height: 1.3; text-align: center;""> <div><p style=""margin: 0;""><span style=""font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-size: 48px;"">¡BIENVENIDO A CITAFACIL!</span></p></div> </td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table><table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td class=""r16-i"" style=""background-color: #ffffff; padding-left: 5px; padding-right: 5px; padding-top: 30px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""50%"" valign=""top"" class=""r17-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r18-i"" style=""padding-left: 5px; padding-right: 5px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r19-c"" align=""center""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""285"" class=""r20-o"" style=""table-layout: fixed; width: 285px;""><tr><td class=""r21-i"" style=""font-size: 0px; line-height: 0px;""> <img src=""https://img.mailinblue.com/6736238/images/content_library/original/65499139f7f2005a475535ef.jpeg"" width=""285"" border=""0"" style=""display: block; width: 100%;""></td> </tr></table></td> </tr></table></td> </tr></table></th> <th width=""50%"" valign=""top"" class=""r17-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r18-i"" style=""padding-left: 5px; padding-right: 5px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r19-c"" align=""center""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""285"" class=""r20-o"" style=""table-layout: fixed; width: 285px;""><tr><td style=""font-size: 0px; line-height: 0px;""> <img src=""https://img.mailinblue.com/6736238/images/content_library/original/6549914787f5da63ca0f780e.jpeg"" width=""285"" border=""0"" style=""display: block; width: 100%;""></td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table><table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td class=""r22-i"" style=""background-color: #ffffff; padding-left: 20px; padding-right: 20px; padding-top: 30px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""100%"" valign=""top"" class=""r5-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r23-i"" style=""padding-left: 10px; padding-right: 10px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r13-c"" align=""left""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r14-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""left"" valign=""top"" class=""r24-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word; text-align: left;""> <div><h2 class=""default-heading2"" style=""margin: 0; color: #1f2d3d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; word-break: break-word;""><span style=""font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji;"">¡Bienvenido a CitaFacil! </span></h2></div> </td> </tr></table></td> </tr><tr><td class=""r13-c"" align=""left""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r14-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""left"" valign=""top"" class=""r25-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word; padding-top: 28px; text-align: left;""> <div><p style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;"">Estamos emocionados de tenerte a bordo para simplificar la gestión de tus citas de belleza.</p><p style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;"">En CitaFacil, te ofrecemos la comodidad de reservar tus citas en barberías y salones de belleza favoritos con tan solo unos clics. Olvídate de las largas esperas y organiza tu tiempo como nunca antes.</p><p style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;"">¿Qué puedes esperar de CitaFacil?</p><ul style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;""><li>Reserva fácil y rápida: Encuentra y reserva tus citas con tan solo unos clics.</li><li>Recordatorios automáticos: Nunca olvides una cita gracias a nuestros recordatorios automáticos.</li><li>Gestión de citas sin complicaciones: Cambia o cancela tus citas de manera sencilla.</li></ul><p style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;"">Estamos comprometidos a hacer tu experiencia con nosotros tan placentera como sea posible. ¡Explora la facilidad de CitaFacil ahora y disfruta de un nuevo nivel de comodidad en tu rutina de belleza!</p><p style=""--tw-border-spacing-x: 0; --tw-border-spacing-y: 0; --tw-ring-offset-width: 0px; --tw-ring-shadow: 0 0 transparent; --tw-rotate: 0; --tw-scale-x: 1; --tw-scale-y: 1; --tw-scroll-snap-strictness: proximity; --tw-shadow-colored: 0 0 transparent; --tw-shadow: 0 0 transparent; --tw-skew-x: 0; --tw-skew-y: 0; --tw-translate-x: 0; --tw-translate-y: 0; -webkit-text-stroke-width: 0px; border: 0px solid rgb(217, 217, 227); box-sizing: border-box; font-family: Söhne, ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Ubuntu, Cantarell, Noto Sans, sans-serif, Helvetica Neue, Arial, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol, Noto Color Emoji; font-style: normal; font-weight: 400; letter-spacing: normal; margin: 1.25em 0px; orphans: 2; text-decoration-style: initial; text-decoration-thickness: initial; text-indent: 0px; text-transform: none; white-space: pre-wrap; widows: 2; word-spacing: 0px;"">¡Gracias por elegir CitaFacil!</p></div> </td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table><table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td class=""r26-i"" style=""background-color: #343434; padding-bottom: 25px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""100%"" valign=""top"" class=""r5-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r7-i""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r11-c"" align=""center""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""center"" valign=""top"" class=""r27-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word; padding-top: 25px; text-align: center;""> <div><div class=""nl2go_class_16_white_reg"" style=""color: #fff; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 16px; word-break: break-word;""><span style=""color: #ffffff;"">CitaFacil </span><br><span style=""color: #ffffff;"">Dg. 15 N° 19-81</span></div><div class=""nl2go_class_16_white_reg"" style=""color: #fff; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 16px; word-break: break-word;""><span style=""color: #ffffff;"">253051 Facatativa </span><br><a href=""http://"" style=""color: #100d0d; text-decoration: none;""><span style=""color: #ffffff;"">CitaFacil1@gmail.com</span></a></div></div> </td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table><table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" align=""center"" class=""r0-o"" style=""table-layout: fixed; width: 100%;""><tr><td class=""r28-i"" style=""background-color: #D8D8D8; padding-bottom: 15px; padding-top: 13px;""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><th width=""100%"" valign=""top"" class=""r5-c"" style=""font-weight: normal;""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r6-o"" style=""table-layout: fixed; width: 100%;""><tr><td valign=""top"" class=""r7-i""> <table width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation""><tr><td class=""r13-c"" align=""left""> <table cellspacing=""0"" cellpadding=""0"" border=""0"" role=""presentation"" width=""100%"" class=""r14-o"" style=""table-layout: fixed; width: 100%;""><tr><td align=""center"" valign=""top"" class=""r15-i nl2go-default-textstyle"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 20px; line-height: 1.4; word-break: break-word; text-align: center;""> <div><div class=""nl2go_class_14_black_reg"" style=""color: #100d0d; font-family: Lato,Arial,Helvetica,sans-serif; font-size: 14px; word-break: break-word;""><a href=""{{ unsubscribe }}"" target=""_blank"" style=""color: #100d0d; text-decoration: none;""><u>Cancelar suscripción</u></a></div></div> </td> </tr></table></td> </tr></table></td> </tr></table></th> </tr></table></td> </tr></table></td> </tr></table></td> </tr></table></body></html>";
            EMail solicitud= new EMail();
            solicitud.Para = correo;
            solicitud.Asunto = "Bienvenido a CitaFacil";
            solicitud.Contenido = cuerpo;
            _emailService.SendEmail(solicitud);
        }

        public string ValidarUsuario(UsuarioDTO usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.primerApellido))
            {
                return "El primer apellido es requerido.";
            }
            if (usuario.primerApellido.Length <= 3)
            {
                return "El primer apellido debe tener más de 3 caracteres.";
            }
            if (string.IsNullOrWhiteSpace(usuario.primerNombre))
            {
                return "El primer nombre es requerido.";
            }
            if (usuario.primerNombre.Length <= 3)
            {
                return "El primer nombre debe tener más de 3 caracteres.";
            }
            if (string.IsNullOrWhiteSpace(usuario.segundoNombre))
            {
                return "El segundo nombre es requerido.";
            }
            if (usuario.segundoNombre.Length <= 3)
            {
                return "El segundo nombre debe tener más de 3 caracteres.";
            }
            if (string.IsNullOrWhiteSpace(usuario.segundoApellido))
            {
                return "El segundo apellido es requerido.";
            }
            if (usuario.segundoApellido.Length <= 3)
            {
                return "El segundo apellido debe tener más de 3 caracteres.";
            }
            if (string.IsNullOrWhiteSpace(usuario.telefonoC))
            {
                return "El teléfono celular es requerido.";
            }
            if (!Regex.IsMatch(usuario.telefonoC, @"^\d+$") || usuario.telefonoC.Length < 8)
            {
                return "El teléfono celular no es válido.";
            }
            if (string.IsNullOrWhiteSpace(usuario.telefonoF))
            {
                return "El teléfono fijo es requerido.";
            }
            if (!Regex.IsMatch(usuario.telefonoF, @"^\d+$") || usuario.telefonoF.Length < 8)
            {
                return "El teléfono fijo no es válido.";
            }
            if (string.IsNullOrWhiteSpace(usuario.correo))
            {
                return "El correo electrónico es requerido.";
            }
            if (!Regex.IsMatch(usuario.correo, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                return "El correo electrónico no es válido.";
            }
            if (string.IsNullOrWhiteSpace(usuario.contrasena))
            {
                return "La contraseña es requerida.";
            }
            if (string.IsNullOrWhiteSpace(usuario.contrasenaConf))
            {
                return "La confirmación de la contraseña es requerida.";
            }
            if (!Regex.IsMatch(usuario.contrasena, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!_./])[A-Za-z\d@#$%^&+=!_./]{8,}$"))
            {
                return "La contraseña no cumple con los requisitos de seguridad.";
            }
            if (!usuario.contrasena.Equals(usuario.contrasenaConf))
            {
                return "Las contraseñas no coinciden.";
            }

            return null; 
        }

        public int AgregarCliente(UsuarioDTO usuario)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string nombreCompleto = usuario.primerNombre + " " + usuario.segundoNombre + " "+ usuario.primerApellido + " " + usuario.segundoApellido;
            _context.Usuario.Add
                (
                    new Usuario
                    {
                        Nombre = textInfo.ToTitleCase(nombreCompleto),
                        Cedula = usuario.cedula,
                        Correo = usuario.correo,
                        contraseña = HashPassword(usuario.contrasena),
                        Celular = usuario.telefonoC,
                        Telefono = usuario.telefonoF,
                        Id_EstadoUsuario = 4,
                        Id_Rol = 3,
                    }
                );
            return _context.SaveChanges();
        }

        public string ValidarEmpresa(EmpresaDTO empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa.Nombre))
            {
                return "Nombre de la empresa requerido";
            }
            if (empresa.Nombre.Length <= 3)
            {
                return "El nombre debe tener más de 3 caracteres.";
            }
            if (string.IsNullOrWhiteSpace(empresa.direccion))
            {
                return "La direccion es requerida.";
            }
            if (empresa.servicio.Equals("Seleccionar"))
            {
                return "Debe seleccionar un servicio";
            }
            if(empresa.NIT <= 0)
            {
                return "Numero Incorrecto para un NIT";
            }
            if (string.IsNullOrWhiteSpace(empresa.telefonoC))
            {
                return "El teléfono celular es requerido.";
            }
            if (!Regex.IsMatch(empresa.telefonoC, @"^\d+$") || empresa.telefonoC.Length < 8)
            {
                return "El teléfono celular no es válido.";
            }
            if (string.IsNullOrWhiteSpace(empresa.telefonoF))
            {
                return "El teléfono fijo es requerido.";
            }
            if (!Regex.IsMatch(empresa.telefonoF, @"^\d+$") || empresa.telefonoF.Length < 8)
            {
                return "El teléfono fijo no es válido.";
            }
            if (string.IsNullOrWhiteSpace(empresa.correo))
            {
                return "El correo electrónico es requerido.";
            }
            if (!Regex.IsMatch(empresa.correo, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                return "El correo electrónico no es válido.";
            }
            if (string.IsNullOrWhiteSpace(empresa.contrasena))
            {
                return "La contraseña es requerida.";
            }
            if (string.IsNullOrWhiteSpace(empresa.contrasenaConf))
            {
                return "La confirmación de la contraseña es requerida.";
            }
            if (!Regex.IsMatch(empresa.contrasena, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!_./])[A-Za-z\d@#$%^&+=!_./]{8,}$"))
            {
                return "La contraseña no cumple con los requisitos de seguridad.";
            }
            if (!empresa.contrasena.Equals(empresa.contrasenaConf))
            {
                return "Las contraseñas no coinciden.";
            }

            return null;
        }

        public int AgregarEmpresa(EmpresaDTO empresa)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            _context.Empresa.Add
                (
                    new Empresas
                    {
                        Nombre = textInfo.ToTitleCase(empresa.Nombre),
                        NIT = empresa.NIT,
                        Correo = empresa.correo,
                        contraseña = HashPassword(empresa.contrasena),
                        Celular = empresa.telefonoC,
                        Telefono = empresa.telefonoF,
                        Id_EstadoEmpresa = 4,
<<<<<<< HEAD
                        Id_Rol = 2,
=======
                        Id_Rol = 3,
>>>>>>> c56473f1decee8c3559ed27d8d1f1207e063efde
                        fecha_Registro = DateTime.Now,
                        plan = 'E',
                        Tipo = empresa.servicio,
                        ubicacion = empresa.direccion
                        
                    }
                );
            return _context.SaveChanges();
        }

<<<<<<< HEAD
        public bool IsUserLogin(string correo, string contrasena)
        {
            bool isUserLogin = true;

            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Correo == correo);

            if (usuario == null)
            {
                var company = _context.Empresa.FirstOrDefault(u => u.Correo == correo);

                if (company != null && !VerifyPassword(contrasena, company.contraseña))
                {
                    throw new Exception("Correo o Contraseña Incorrectos");
                }

                isUserLogin = false;
            }
            else if(!VerifyPassword(contrasena,usuario.contraseña))
            {
                throw new Exception("Correo o Contraseña Incorrectos");
            }
            return isUserLogin;
=======
        public string VerificarLogin(string correo, string contrasena)
        {
            Usuario usuario = _context.Usuario.FirstOrDefault(u => u.Correo == correo);
            if(usuario == null || !VerifyPassword(contrasena,usuario.contraseña))
            {
                return "Correo o Contraseña Incorrectos";
            }
            return null;
>>>>>>> c56473f1decee8c3559ed27d8d1f1207e063efde
        }
    }
}
