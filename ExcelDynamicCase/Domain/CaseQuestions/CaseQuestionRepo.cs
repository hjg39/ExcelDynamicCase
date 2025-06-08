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
                    QuestionTitle = "Project Euler - Multiples of 3 or 5",
                    HasLink = true,
                    QuestionLink = "https://projecteuler.net/problem=1",
                    Data = null,
                    Map = null,
                    MapColours = null,
                    Answer = "233168",
                    ExampleAnswer = 10000d,
                    Minutes = 5,
                }
            }
        };
    }
}
