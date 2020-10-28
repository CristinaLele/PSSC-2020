 using Profile.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static Profile.Domain.CreateQuestionWorkflow.CreateQuestionResult;

namespace Test.App
{
    class ProgramQuestion
    {
        static void Main(string[] args)
        {
            var tag = "visual studio, debugging".Split(',').ToList();
            var cmd = new CreateQuestionCmd("How do I remedy “The breakpoint will not currently be hit. No symbols have been loaded for this document.” warning?", "Can someone give me concrete details?", tag);
            var result = CreateQuestion(cmd);

            result.Match(
                    ProcessQuestionPosted,
                    ProcessQuestionNotPosted,
                    ProcessInvalidQuestion
                );

            Console.ReadLine();
        }

        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }
           

        private static ICreateQuestionResult ProcessQuestionNotPosted(QuestionNotPosted questionNotPostedResult)
        {
            Console.WriteLine($"Question not posted: {questionNotPostedResult.Reason}");
            return questionNotPostedResult;
        }

        private static ICreateQuestionResult ProcessQuestionPosted(QuestionPosted question)
        {
            Console.WriteLine($"Question: {question.Title}");
            return question;
        }

        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            if (string.IsNullOrEmpty(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Invalid question title" };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrEmpty(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Invalid question body" };
                return new QuestionValidationFailed(errors);
            }

            if (createQuestionCommand.Tags.Count==0)
            {
                var errors = new List<string>() { "Invalid question tags" };
                return new QuestionValidationFailed(errors);
            }

            var result = new QuestionPosted(createQuestionCommand.Title);
            return result;
        }
    }
}
