using CSharp.Choices;
using LanguageExt.Common;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class Question
    {
        public interface IQuestion { }

        public class UnverifiedQuestion : IQuestion
        {
            public string Question { get; private set; }
            public List<string> Tags { get; private set; }
            private UnverifiedQuestion(string question, List<string> tags)
            {
                Question = question;
                Tags = tags;
            }

            public static Result<UnverifiedQuestion> Create(string question, List<string> tags)
            {
                if (IsQuestionValid(question) && IsTagsValid(tags))
                {
                    return new UnverifiedQuestion(question, tags);
                }
                else if (!IsQuestionValid(question))
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionException(question));
                }
                else 
                {
                    return new Result<UnverifiedQuestion>(new InvalidQuestionTagsException(tags));
                }
            }

            public static bool IsQuestionValid(string question)
            {
                //a question cannot be longer than 1000 characters
                if (question.Length <= 1000)
                {
                   // enableVoting(question);
                    return true;
                }
                return false;
            }

            public static bool IsTagsValid(List<string> tags)
            {
                //a question must have at least one tag and no more than 3 tags
                if (tags.Count>=1 && tags.Count<=3)
                {
                    return true;
                }
                return false;
            }
        }
        
        public class VerifiedQuestion : IQuestion
        {
            public string Question { get; private set; }
            public List<string> Tags { get; private set; }

            internal VerifiedQuestion(string question, List<string> tags)
            {
                Question = question;
                Tags = tags;
            }
        }
    }
}
