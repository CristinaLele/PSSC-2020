using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class VerifyVoteService
    {
        public Task VerifyIfQuestionIsPosted(VerifiedQuestion question)
        {
            //implement verification from votes
            //if the question is not posted, then you can not vote it  => only a published question can receive votes
            return Task.CompletedTask;
        }
     }
}