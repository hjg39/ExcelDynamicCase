using RPGM.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Scripts.UI
{
    [ExecuteInEditMode]
    public class FunctionInventoryLayout : MonoBehaviour
    {
        public float padding = 0.25f;
        public SpriteRenderer spriteRenderer;
        public TextMeshPro textMeshPro;

        Vector2 minSize;

        void Awake()
        {
            minSize = spriteRenderer.size;
        }

        public void SetText(string text)
        {
            SetDialogText(text);
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
