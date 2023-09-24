using CitaFacil.Models;

namespace CitaFacil.Services
{
    public interface IEmailService
    {
        public void SendEmail(EMail solicitud);
    }
}
