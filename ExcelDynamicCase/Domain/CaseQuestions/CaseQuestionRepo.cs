using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            }
        };

    }
}
