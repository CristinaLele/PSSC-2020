using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class VerifyAndPublishQuestionService
    {
        public Result<VerifiedQuestion> VerifyQuestion(UnverifiedQuestion question)
        {
            //verify if the conditions are respected: maximum 1000 characters and the number of tags should be between 1 and 3
            //after all the conditions have been met => the question is published successfully
            return new VerifiedQuestion(question.Question,question.Tags);
        }
    }
}
