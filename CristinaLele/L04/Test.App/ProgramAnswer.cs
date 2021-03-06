﻿using Profile.Domain.PostAnswerWorkflow;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using CSharp.Choices;
using static Profile.Domain.PostAnswerWorkflow.PostAnswerResult;

namespace Test.App
{
    class ProgramAnswer
    {

        private static IPostAnswerResult ProcessInvalidAnswer(AnswerValidationFailed validationErrors)
        {
            Console.WriteLine("Answer validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }


        private static IPostAnswerResult ProcessAnswerNotPosted(AnswerNotPosted answerNotPostedResult)
        {
            Console.WriteLine($"Answer not posted: {answerNotPostedResult.Reason}");
            return answerNotPostedResult;
        }

        private static IPostAnswerResult ProcessAnswerPosted(PostAnswer answer)
        {
            Console.WriteLine($"Answer: {answer.AnswerText}");
            return answer;
        }

        public static IPostAnswerResult PostAnswer(PostAnswerCmd postAnswerCommand)
        {
            if (string.IsNullOrEmpty(postAnswerCommand.AnswerText))
            {
                var errors = new List<string>() { "Invalid answer body" };
                return new AnswerValidationFailed(errors);
            }

            var result = new PostAnswer(postAnswerCommand.AnswerText);
            return result;
        }
    }
}
