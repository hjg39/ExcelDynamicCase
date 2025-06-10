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

            List<int> pureCompletedQuestions = saveData.PureCompletedQuestions;
            List<int> completedQuestions = saveData.CompletedQuestions;

            string[] completedLevelFill = new string[4]
            {
                $"Easy: {completedQuestions.Where(x => basic.Contains(x)).Count()}/{basic.Length}, {pureCompletedQuestions.Where(x => basic.Contains(x)).Count()}/{basic.Length}",
                $"Advanced: {completedQuestions.Where(x => advanced.Contains(x)).Count()}/{advanced.Length}, {pureCompletedQuestions.Where(x => advanced.Contains(x)).Count()}/{advanced.Length}",
                $"Expert: {completedQuestions.Where(x => expert.Contains(x)).Count()}/{expert.Length}, {pureCompletedQuestions.Where(x => expert.Contains(x)).Count()}/{expert.Length}",
                $"Divine: {completedQuestions.Where(x => divine.Contains(x)).Count()}/{divine.Length}, {pureCompletedQuestions.Where(x => divine.Contains(x)).Count()}/{divine.Length}",
            };


            Fill(unlockedFunctions.content, savedUnlockedFunctions, 0, savedUnlockedFunctions.Count);
            Fill(lockedFunctions.content, savedLockedFunctions, 0, savedLockedFunctions.Length);
            Fill(completedLevels.content, completedLevelFill, 0, 4);
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
