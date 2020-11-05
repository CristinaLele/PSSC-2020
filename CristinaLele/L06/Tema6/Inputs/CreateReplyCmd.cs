using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Inputs
{
    public class CreateReplyCmd
    {
        public int AuthorId { get; }
        public int QuestionId { get; }
        public string Reply { get; }

        public CreateReplyCmd(int authorId, int questionId, string reply)
        {
            AuthorId = authorId;
            QuestionId = questionId;
            Reply = reply;
        }
    }
}
