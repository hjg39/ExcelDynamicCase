using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDynamicCase.Domain.CaseQuestions
{
    public class CaseQuestion
    {
        public CaseQuestionEnum Id { get; set; }

        public string QuestionLink { get; set; }

        public bool HasLink { get; set; }

        public string QuestionTitle { get; set; }

        public object[,] Data { get; set; }

        public object[,] Map { get; set; }

        public int[,] MapColours { get; set; } 

    }
}
