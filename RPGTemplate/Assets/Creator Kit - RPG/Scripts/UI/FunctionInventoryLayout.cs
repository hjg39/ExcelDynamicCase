using Assets.Creator_Kit___RPG.Persistence;
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

        Vector2 minSize;

        void Awake()
        {
            minSize = spriteRenderer.size;
        }

        public void SetText(string text)
        {
            SetDialogText(text);

            SaveManager.LoadGameData(out SaveData saveData);

            List<string> savedUnlockedFunctions = saveData.UnlockedFunctions;

            Fill(unlockedFunctions.content, savedUnlockedFunctions, 0, savedUnlockedFunctions.Count);
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
