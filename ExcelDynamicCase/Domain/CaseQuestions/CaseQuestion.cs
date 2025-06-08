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

        public string QuestionText { get; set; }

        public Dictionary<string, object[,]> Data { get; set; }

        public Dictionary<string, int[,]> Colours { get; set; }

        public string Answer { get; set; }

        public object ExampleAnswer { get; set; }

        public double Minutes { get; set; }
    }
}
