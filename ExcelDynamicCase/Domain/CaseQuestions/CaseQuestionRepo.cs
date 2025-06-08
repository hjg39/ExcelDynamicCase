using System.Collections.Generic;

namespace ExcelDynamicCase.Domain.CaseQuestions
{
    public static class CaseQuestionRepo
    {
        public static Dictionary<CaseQuestionEnum, CaseQuestion> CaseQuestions = new Dictionary<CaseQuestionEnum, CaseQuestion>()
        {
            {
                CaseQuestionEnum.EulerOneMultiplesThreeFive,
                new CaseQuestion()
                {
                    Id = CaseQuestionEnum.EulerOneMultiplesThreeFive,
                    QuestionText = "If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.\r\n\r\nFind the sum of all the multiples of 3 or 5 below 1000.",
                    QuestionLink = "https://projecteuler.net/problem=1",
                    Data = null,
                    Answer = "233168",
                    ExampleAnswer = 10000d,
                    Minutes = 5,
                }
            }
        };
    }
}
