using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TacoBotProject.Constantes
{
    public class QnAMakerConstantes
    {
        public const string Host = "https://qnamakerservices.azurewebsites.net/qnamaker";
        public const string KnowledgeBaseID = "486168b9-d3d9-4d98-bcf4-91d4099f4f50";
        public const string EndPointKey = "95f863dc-b499-41d7-899e-78123f10b2c1";

        public const string FormatJson = "application/json";
        public const string AnswerNotFound = "No se encontraron resultados en kb.";
    }
}