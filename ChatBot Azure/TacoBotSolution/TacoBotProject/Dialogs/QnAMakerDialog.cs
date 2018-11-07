using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TacoBotProject.Services.QnAMaker;
using TacoBotProject.Constantes;

namespace TacoBotProject.Dialogs
{
    [Serializable]
    public class QnAMakerDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            var serviceQnAMaker = new QnAMakerService();
            var respuesta = serviceQnAMaker.GetAnswer(message.Text);
            if (respuesta.Equals(QnAMakerConstantes.AnswerNotFound))
            {
                await context.PostAsync("Lo siento, no estoy preparado para este tipo de preguntas.");
            }else{
                await context.PostAsync(respuesta);
            }
        }
    }
}