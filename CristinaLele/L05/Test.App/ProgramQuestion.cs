using LanguageExt;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static Profile.Domain.CreateQuestionWorkflow.CreateQuestionResult;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Test.App
{
    class ProgramQuestion
    {
        static void Main(string[] args)
        {
            var tagsResult = "C#, visual studio, debugging".Split(",").ToList();
            var questionResult = UnverifiedQuestion.Create("How do I remedy “The breakpoint will not currently be hit. No symbols have been loaded for this document.” warning?", tagsResult);

            questionResult.Match(
                Succ: question =>
                {
                    ReviewQuestionText(question);
                    VoteQuestion(question);
                    Console.WriteLine("Question is valid!");
                    return Unit.Default;
                },
                Fail: ex =>
                {
                    Console.WriteLine($"Invalid question! Reason: {ex.Message}");
                    return Unit.Default;
                }
             );

            Console.ReadLine();
        }    
        
        private static void ReviewQuestionText(UnverifiedQuestion question)
        {
            var verifiedQuestionResult = new VerifyAndPublishQuestionService().VerifyQuestion(question);
            verifiedQuestionResult.Match(
                verifiedQuestion =>
                {
                    new ReviewQuestionService().RewriteQuestionText(verifiedQuestion).Wait();
                    return Unit.Default;
                },
                ex =>
                {
                    Console.WriteLine("Question could't be verified");
                    return Unit.Default;
                }
                );
        }
        private static void VoteQuestion(UnverifiedQuestion question)
        {
            var voteQuestionResult = new VerifyAndPublishQuestionService().VerifyQuestion(question);
            voteQuestionResult.Match(
                voteQuestion =>
                {
                    new VerifyVoteService().VerifyIfQuestionIsPosted(voteQuestion).Wait();
                    return Unit.Default;
                },
                ex =>
                {
                    Console.WriteLine("You can't vote");
                    return Unit.Default;
                }
                );
        }
    }
}
