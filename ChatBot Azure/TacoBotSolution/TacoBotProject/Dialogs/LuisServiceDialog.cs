using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TacoBotProject.Dialogs
{
    [LuisModel(modelID: "3fd03093-31ae-48b2-945c-e834048d4993", subscriptionKey: "b8de25452b5e491e80df62707b493bc5")]

    [Serializable]
    public class LuisServiceDialog : LuisDialog<string>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento, aún no estoy programado para responder eso.");
            await Task.Delay(3000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
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
            await Task.Delay(3000);
            await context.PostAsync(reply);
        }

        [LuisIntent("Agradecimientos")]
        public async Task Agradecimientos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("A tu servicio, siempre estaré para ayudarte.");
            await Task.Delay(3000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
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