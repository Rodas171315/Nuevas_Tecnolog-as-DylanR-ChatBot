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
    [LuisModel(modelID: "3fd03093-31ae-48b2-945c-e834048d4993", subscriptionKey: "b8de25452b5e491e80df62707b493bc5", domain: "westus.api.cognitive.microsoft.com", threshold: 0.5)]

    [Serializable]
    public class LuisQnADialog : LuisDialog<string>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await QnaMakerMessage(context, result.Query);
            await Task.Delay(3000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }
        [LuisIntent("Saludos")]
        public async Task Saludos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hola {context.Activity.From.Name}! Soy TacoBot el reclutador o tu buscador de empleos.");
            await Task.Delay(3000);
            await context.PostAsync("¿Cómo puedo ayudarte?");
        }
        [LuisIntent("CentroContacto")]
        public async Task CentroContacto(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("El número de centro de contacto es: +502 40636615");
            await context.PostAsync("El correo de centro de contacto es: info@tacobot.com");
        }
        [LuisIntent("Reclamos")]
        public async Task Reclamos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Gracias por reportarlo.");
            await Task.Delay(3000);
            await context.PostAsync("Por favor registra tu problema en el siguiente enlace:");
            await Task.Delay(3000);

            var reply = context.MakeMessage();
            reply.Attachments.Add(GetCard());
            await Task.Delay(2000);
            await context.PostAsync(reply);
        }
        [LuisIntent("Agradecimientos")]
        public async Task Agradecimientos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("A tu servicio, siempre estaré para ayudarte.");
            await Task.Delay(3000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
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

        private Attachment GetCard()
        {
            var card = new HeroCard
            {
                Title = "Asistente Virtual",
                Buttons = new List<CardAction>
                {
                    new CardAction(type: ActionTypes.OpenUrl, title:"Ir a la web", value:"https://azure.microsoft.com/es-es/support/options/")
                }
            };
            return card.ToAttachment();
        }
    }
}