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
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            11,
            12,
            13,
            15,
            16,
            17,
            24,
            30,
            31,
            33,
            34,
            36,
            40,
            43,
            76
        };

        public readonly static IReadOnlyDictionary<int, int> MinutesAllowedPerQuestion = new Dictionary<int, int>()
        {
            { 1, 5 },
            { 2, 3 },
            { 3, 15 },
            { 4, 4 },
            { 5, 4 },
            { 6, 4 },
            { 7, 15 },
            { 8, 6 },
            { 9, 9 },
            { 11, 20 },
            { 12, 20 },
            { 13, 8 },
            { 15, 5 },
            { 16, 8 },
            { 17, 15 },
            { 24, 15 },
            { 30, 10 },
            { 31, 15 },
            { 33, 10 },
            { 34, 7 },
            { 36, 8 },
            { 40, 10 },
            { 43, 20 },
            { 76, 15 }
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
