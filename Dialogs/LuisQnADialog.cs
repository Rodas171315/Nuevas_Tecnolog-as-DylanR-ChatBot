using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using TacoBotProject.Constantes;
using TacoBotProject.Services.QnAMaker;

namespace TacoBotProject.Dialogs
{
    [LuisModel(modelID: "3fd03093-31ae-48b2-945c-e834048d4993", subscriptionKey: "b8de25452b5e491e80df62707b493bc5", domain: "westus.api.cognitive.microsoft.com")]

    [Serializable]
    public class LuisQnADialog : LuisDialog<string>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await QnaMakerMessage(context, result.Query);
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }
        [LuisIntent("Saludos")]
        public async Task Saludos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hola {context.Activity.From.Name}! Soy TacoBot el reclutador o tu buscador de empleos.");
            await Task.Delay(2000);
            await context.PostAsync("Estas son las opciones que tengo para ti:");
            await Task.Delay(3000);

            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            reply.Attachments = GetCards();
            await context.PostAsync(reply);
        }
        [LuisIntent("BuscarEmpleo")]
        public async Task BuscarEmpleo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(".");
            await Task.Delay(2000);
            await context.PostAsync(".");
        }
        [LuisIntent("BuscarTrabajadores")]
        public async Task BuscarTrabajadores(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(".");
            await Task.Delay(2000);
            await context.PostAsync(".");
        }
        [LuisIntent("GuardarEmpleo")]
        public async Task GuardarEmpleo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(".");
            await Task.Delay(2000);
            await context.PostAsync(".");
        }
        [LuisIntent("GuardarCV")]
        public async Task GuardarCV(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(".");
            await Task.Delay(2000);
            await context.PostAsync(".");
        }
        [LuisIntent("ProbarBot")]
        public async Task ProbarBot(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo está probando ahora mismo.");
            await Task.Delay(2000);
            await context.PostAsync("jeje.");
        }
        [LuisIntent("CentroContacto")]
        public async Task CentroContacto(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("El número de centro de contacto es: +502 40636615");
            await Task.Delay(2000);
            await context.PostAsync("El correo de centro de contacto es: info@tacobot.com");
            await Task.Delay(3000);
            var reply = context.MakeMessage();
            reply.Attachments.Add(GetContactoCard());
            await context.PostAsync(reply);
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }
        [LuisIntent("Reclamos")]
        public async Task Reclamos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Gracias por reportarlo.");
            await Task.Delay(2000);
            await context.PostAsync("Por favor registra tu problema en el siguiente enlace:");
            await Task.Delay(3000);
            var reply = context.MakeMessage();
            reply.Attachments.Add(GetHelpCard());
            await context.PostAsync(reply);
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }
        [LuisIntent("Agradecimientos")]
        public async Task Agradecimientos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("A tu servicio, siempre estaré para ayudarte.");
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }
        [LuisIntent("Despedidas")]
        public async Task Despedidas(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Muy bien, le deseo un feliz día.");
            await Task.Delay(2000);
            await context.PostAsync("Si te gusta mi servicio, por favor comparteme con tus amigos. :)");
        }

        private async Task QnaMakerMessage(IDialogContext context, string query)
        {
            var qnaService = new QnAMakerService();
            string respuesta = qnaService.GetAnswer(query);

            if (respuesta.Equals(QnAMakerConstantes.AnswerNotFound))
            {
                await context.PostAsync("Lo siento, no estoy preparado para este tipo de preguntas.");
            }else{
                await context.PostAsync(respuesta);
            }
        }

        private IList<Attachment> GetCards()
        {
            return new List<Attachment>()
            {
                GetRecruiterCard(),
                GetWorkerCard(),
                GetTryCard(),
                GetTacoBotCard(),
                GetContactoCard()
            };
        }

        const string TrabajoOption = "Buscar trabajadores";
        const string EmpleoOption = "Buscar empleo";
        const string PruebaOption = "Probar el bot";
        const string EmailOption = "Enviar email";
        const string CelOption = "Llamar";
        const string VacanteOption = "Guardar puesto vacante";
        const string CVOption = "Guardar CV";



        private Attachment GetHelpCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Asistente Virtual",
                Subtitle = "Opciones",
                Buttons = new List<CardAction> { new CardAction(type: ActionTypes.OpenUrl, title:"Web de Soporte T.", value:"https://azure.microsoft.com/es-es/support/options/") }
            };
            return heroCard.ToAttachment();
        }
        private Attachment GetRecruiterCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Soy un Reclutador",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://raw.githubusercontent.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot/master/Resource/Imagen/reclutador.jpg") },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.PostBack, "Buscar trabajadores", value: "Buscar trabajadores"),
                    new CardAction(ActionTypes.PostBack, "Guardar puesto vacante", value: "Guardar puesto vacante"),
                }
            };
            return heroCard.ToAttachment();
        }
        private Attachment GetWorkerCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Soy un Trabajador",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://github.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot/blob/master/Resource/Imagen/worker.jpg?raw=true") },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.PostBack, "Buscar empleo", value: "Buscar empleo"),
                    new CardAction(ActionTypes.PostBack, "Guardar CV", value: "Guardar CV")
                }
            };
            return heroCard.ToAttachment();
        }
        private Attachment GetTryCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Probar TacoBot",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://github.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot/blob/master/Resource/Imagen/try.jpg?raw=true") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, "Probar el bot", value: "Probar el bot.") }
            };
            return heroCard.ToAttachment();
        }
        private Attachment GetTacoBotCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Sugerencias",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://github.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot/blob/master/Resource/Imagen/sugerencias.png?raw=true") },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "Conoce mi código fuente", value: "https://github.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot"),
                    new CardAction(ActionTypes.OpenUrl, "Conoce a mi creador Dylan", value: "https://www.facebook.com/dylan.rodas.512"),
                    new CardAction(ActionTypes.OpenUrl, "Conoce a mi creador Mario", value: "https://www.facebook.com/mariofer.pons")
                }
            };
            return heroCard.ToAttachment();
        }
        private Attachment GetContactoCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Contactar",
                Subtitle = "Opciones",
                Images = new List<CardImage> { new CardImage("https://github.com/Rodas171315/Nuevas_Tecnologias-DylanR-ChatBot/blob/master/Resource/Imagen/contacto.jpg?raw=true") },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "Facebook", value: "https://www.facebook.com/TacoBot-2264436180455233"),
                    new CardAction(ActionTypes.PostBack, "Enviar email", value: "Enviar email"),
                    new CardAction(ActionTypes.PostBack, "Llamar", value: "Llamar")
                }
            };
            return heroCard.ToAttachment();
        }

    }
}