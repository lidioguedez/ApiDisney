using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class SendGridEnviar : ISendGridEnviar
    {
        private readonly IConfiguration _config;

        public SendGridEnviar(IConfiguration config)
        {
            _config = config;
        }

        public async void EnviarEmail(SendGridEnt data)
        {
            try
            {
                var sendGridCliente = new SendGridClient(_config["SendGrid:ApiKey"]);
                var destinatario = new EmailAddress(data.EmailDestinatario, data.NombreDestinatario);
                var tituloEmail = "Bienvenid@ - Gracias por registrarse" ;
                var sender = new EmailAddress("admin@abdisoft.com", "Lidio Guedez");
                var contenidoMensaje = $"Hola {data.Nombre},  <br /><br /><br />" +
                $" Te damos la Bienvenida a nuestra Api Disney, a continuación te hacemos llegar el Token de conexion:<br /><br />" +
                $"TOKEN: {data.Token} <br /><br />" +
                $"Recorda que tendra una duracción de 4 horas.<br /><br />" +
                $"Nos depedimos cordialmente<br /><br /><br />" +
                $"API DISNEY";

                var objMensaje = MailHelper.CreateSingleEmail(sender, destinatario, tituloEmail, contenidoMensaje, contenidoMensaje);

                await sendGridCliente.SendEmailAsync(objMensaje);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
