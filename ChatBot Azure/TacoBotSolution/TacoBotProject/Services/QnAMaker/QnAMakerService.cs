using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TacoBotProject.Constantes;

namespace TacoBotProject.Services.QnAMaker
{
    public class QnAMakerService
    {
        public string GetAnswer(string query)
        {
            var client = new RestClient(QnAMakerConstantes.Host + "/knowledgebases/" + QnAMakerConstantes.KnowledgeBaseID + "/generateAnswer");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "EndPointKey " + QnAMakerConstantes.EndPointKey);
            request.AddParameter(QnAMakerConstantes.FormatJson, "{\"question\": \"" + query + "\"}", ParameterType.RequestBody);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<QnAMakerModel>(response.Content);

            if (result.Answers.Count > 0)
            {
                var respuesta = result.Answers[0].Answer;
                var score = result.Answers[0].Score;
                if (!respuesta.ToLower().Equals(QnAMakerConstantes.AnswerNotFound) && score > 40)
                    return respuesta;
            }
            return QnAMakerConstantes.AnswerNotFound;
        }
    }
}