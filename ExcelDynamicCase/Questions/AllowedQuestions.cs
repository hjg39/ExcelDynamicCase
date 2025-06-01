using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDynamicCase.Questions
{
    public static class AllowedQuestions
    {
        public static List<int> AllowedQuestionNumbers = new List<int>()
        { 1 };

        public readonly static IReadOnlyDictionary<int, int> MinutesAllowedPerQuestion = new Dictionary<int, int>()
        {
            { 1, 5 },
        };

        public static int GetNumberOfMinutes(int questionNumber)
            => MinutesAllowedPerQuestion[questionNumber];

        public static int GetRandomNotYetCompletedQuestion()
        {
            List<int> alreadyCompletedQuestions = Storage.AlreadyCompletedQuestions;

            int i = 0;
            while (true)
            {
                int questionNumber = AllowedQuestionNumbers[Storage.Random.Next(AllowedQuestionNumbers.Count)];

                if (alreadyCompletedQuestions.Contains(questionNumber) && i++ < 4) 
                {
                    continue;
                }

                return questionNumber;
            }
        }

    }
}
