using Assets.Creator_Kit___RPG.Logic;
using Assets.Creator_Kit___RPG.Persistence;
using Assets.ExcelDomain;
using RPGM.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Creator_Kit___RPG.Scripts.UI
{
    [ExecuteInEditMode]
    public class FunctionInventoryLayout : MonoBehaviour
    {
        public float padding = 0.25f;
        public SpriteRenderer spriteRenderer;
        public TextMeshPro textMeshPro;
        public TMP_Text itemTextMeshPro;
        public TMP_Text rankTextMeshPro;
        public ScrollRect unlockedFunctions;
        public ScrollRect lockedFunctions;
        public ScrollRect completedLevels;

        Vector2 minSize;

        void Awake()
        {
            minSize = spriteRenderer.size;
        }

        public void SetText(string text)
        {
            SetDialogText(text);

            SaveManager.LoadGameData(out SaveData saveData);

            List<string> savedUnlockedFunctions = saveData.UnlockedFunctions.OrderBy(x => x).ToList();
            HashSet<string> unlockedFunctionsByHash = savedUnlockedFunctions.ToHashSet();

            string[] savedLockedFunctions = ExcelFunctions.AllFunctions.Where(x => !unlockedFunctionsByHash.Contains(x)).ToArray();

            int[] basic = BattleManager.GetQuestionsByRewardClassification(QuestionRewardClassification.BasicAggregates);
            int[] advanced = BattleManager.GetQuestionsByRewardClassification(QuestionRewardClassification.AdvancedAggregates);
            int[] expert = BattleManager.GetQuestionsByRewardClassification(QuestionRewardClassification.ExpertAggregates);
            int[] divine = BattleManager.GetQuestionsByRewardClassification(QuestionRewardClassification.DivineAggregates);
            int[] basicCurated = BattleManager.GetQuestionsByRewardClassification(QuestionRewardClassification.BasicCurated);



            List<int> pureCompletedQuestions = saveData.PureCompletedQuestions;
            List<int> completedQuestions = saveData.CompletedQuestions;

            int basicCount = completedQuestions.Where(x => basic.Contains(x)).Count();
            int advancedCount = completedQuestions.Where(x => advanced.Contains(x)).Count();
            int expertCount = completedQuestions.Where(x => expert.Contains(x)).Count();
            int divineCount = completedQuestions.Where(x => divine.Contains(x)).Count();
            int basicCuratedCount = completedQuestions.Where(x => basicCurated.Contains(x)).Count();

            string[] completedLevelFill = new string[5]
            {
                $"Easy: {basicCount}/{basic.Length}, {pureCompletedQuestions.Where(x => basic.Contains(x)).Count()}/{basic.Length}",
                $"Advanced: {advancedCount}/{advanced.Length}, {pureCompletedQuestions.Where(x => advanced.Contains(x)).Count()}/{advanced.Length}",
                $"Expert: {expertCount}/{expert.Length}, {pureCompletedQuestions.Where(x => expert.Contains(x)).Count()}/{expert.Length}",
                $"Divine: {divineCount}/{divine.Length}, {pureCompletedQuestions.Where(x => divine.Contains(x)).Count()}/{divine.Length}",
                $"EasyCurated: {basicCuratedCount}/{basicCurated.Length}, {pureCompletedQuestions.Where(x => basicCurated.Contains(x)).Count()}/{basicCurated.Length}",
            };

            SetRank(basicCount, advancedCount, expertCount, divineCount, basicCuratedCount, basic.Length, advanced.Length, expert.Length, divine.Length, basicCurated.Length);

            Fill(unlockedFunctions.content, savedUnlockedFunctions, 0, savedUnlockedFunctions.Count);
            Fill(lockedFunctions.content, savedLockedFunctions, 0, savedLockedFunctions.Length);
            Fill(completedLevels.content, completedLevelFill, 0, completedLevelFill.Length);
        }

        void SetRank(int basicCount, int advancedCount, int expertCount, int divineCount, int basicCuratedCount, int basicFullCount, int advancedFullCount, int expertFullCount, int divineFullCount, int basicCuratedFullCount)
        {
            double points = basicCount * 10 + advancedCount * 30 + expertCount * 150 + divineCount * 500 + basicCuratedCount * 20;
            double totalPoints = basicFullCount * 10 + advancedFullCount * 30 + expertFullCount * 150 + divineFullCount * 500 + basicCuratedFullCount * 20;
            string rank;

            double ratioOfEarnedPoints = points / totalPoints;

            if (ratioOfEarnedPoints < 0.002692f) { rank = "Sheets User :("; }
            else if (ratioOfEarnedPoints < 0.005384f) { rank = "Baby"; }
            else if (ratioOfEarnedPoints < 0.008075f) { rank = "Toddler"; }
            else if (ratioOfEarnedPoints < 0.010767f) { rank = "Child"; }
            else if (ratioOfEarnedPoints < 0.013459f) { rank = "Beginner"; }
            else if (ratioOfEarnedPoints < 0.016151f) { rank = "Average Joe"; }
            else if (ratioOfEarnedPoints < 0.021534f) { rank = "Basic"; }
            else if (ratioOfEarnedPoints < 0.032301f) { rank = "Probation"; }
            else if (ratioOfEarnedPoints < 0.043069f) { rank = "Novice"; }
            else if (ratioOfEarnedPoints < 0.053836f) { rank = "Unpaid Temp"; }
            else if (ratioOfEarnedPoints < 0.064603f) { rank = "Apprentice"; }
            else if (ratioOfEarnedPoints < 0.07537f) { rank = "Prodigy"; }
            else if (ratioOfEarnedPoints < 0.086137f) { rank = "Coffee Fetcher"; }
            else if (ratioOfEarnedPoints < 0.107672f) { rank = "Junior Analyst"; }
            else if (ratioOfEarnedPoints < 0.129206f) { rank = "Analyst"; }
            else if (ratioOfEarnedPoints < 0.165545f) { rank = "Workhorse"; }
            else if (ratioOfEarnedPoints < 0.196501f) { rank = "Snr. Analyst"; }
            else if (ratioOfEarnedPoints < 0.234186f) { rank = "Team Lead"; }
            else if (ratioOfEarnedPoints < 0.263795f) { rank = "Consultant"; }
            else if (ratioOfEarnedPoints < 0.168237f) { rank = "Coffee Drinker"; }
            else if (ratioOfEarnedPoints < 0.33109f) { rank = "Snr. Consultant"; }
            else if (ratioOfEarnedPoints < 0.370121f) { rank = "I. Contributor"; }
            else if (ratioOfEarnedPoints < 0.398385f) { rank = "Manager"; }
            else if (ratioOfEarnedPoints < 0.437416f) { rank = "Expert"; }
            else if (ratioOfEarnedPoints < 0.46568f) { rank = "Head of Dept."; }
            else if (ratioOfEarnedPoints < 0.598923f) { rank = "Master"; }
            else if (ratioOfEarnedPoints < 0.733513f) { rank = "Grand Master"; }
            else if (ratioOfEarnedPoints < 0.866756f) { rank = "God"; }
            else if (ratioOfEarnedPoints < 1f) { rank = "ANgaiAllator"; }
            else { rank = "Completionist :)"; }

            rankTextMeshPro.text = $"{rank} ({points})";
        }

        void Fill(RectTransform parent, IReadOnlyList<string> src, int start, int end)
        {
            int totalChildCount = parent.childCount;
            for (int i = 0; i < totalChildCount; i++)
            {
                Destroy(parent.GetChild(totalChildCount - i - 1).gameObject);
            }

            for (int i = start; i < end; ++i)
            {
                var t = Instantiate(itemTextMeshPro, parent);
                t.text = src[i];
                t.ForceMeshUpdate();                        // ensures the layout is current :contentReference[oaicite:1]{index=1}
            }
        }

        void SetDialogText(string text)
        {
            textMeshPro.text = text;
            ScaleBackgroundToFitText();
        }

        void Update()
        {
            ScaleBackgroundToFitText();
        }


        void ScaleBackgroundToFitText()
        {
            var s = (Vector2)textMeshPro.bounds.size;
            s += Vector2.one * padding;
            s.x = Mathf.Max(minSize.x, s.x);
            s.y = Mathf.Max(minSize.y, s.y);
            spriteRenderer.size = s;
        }

        public float GetHeight()
        {
            return spriteRenderer.size.y;
        }
    }
}
