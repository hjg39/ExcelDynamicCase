using Assets.Creator_Kit___RPG.Persistence;
using RPGM.UI;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Scripts.UI
{
    public class CountdownTimer : MonoBehaviour
    {
        public float timeRemaining;
        public TMP_Text tmpText;
        public TMP_Text functions;
        public bool isRunning;
        public Camera mainCamera;
        public SpriteUIElement spriteUIElement;

        public void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void StartTimer(float from = -1f)
        {
            timeRemaining = (from > 0f) ? from : timeRemaining;
            isRunning = true;
        }

        public void StopTimer() => isRunning = false;

        void Update()
        {
            if (!isRunning) return;

            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                isRunning = false;
                // Optionally invoke an event or call a method here
            }

            UpdateDisplay(timeRemaining);
        }

        public void Show(Vector3 position, float minutes)
        {
            timeRemaining = minutes * 60;
            SaveManager.LoadGameData(out SaveData saveData);
            functions.text = string.Join(", ", saveData.UnlockedFunctions);

            this.gameObject.SetActive(true);
            //SetPosition(position);
            SetPosition();
            
            isRunning = true;
        }

        public void Hide()
        {
            this.isRunning = false;
            this.gameObject.SetActive(false);
        }

        void SetPosition()
        {
            Vector3 pixelCentre = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 3);
            transform.position = mainCamera.ScreenToWorldPoint(pixelCentre);
        }

        void UpdateDisplay(float minutesIn)
        {
            // Convert minutes → seconds once
            float totalSeconds = minutesIn;

            // Format as mm:ss
            int minutes = Mathf.FloorToInt(totalSeconds / 60f);
            int seconds = Mathf.FloorToInt(totalSeconds % 60f);

            string text = $"{minutes:00}:{seconds:00}";

            if (tmpText)           // TextMeshPro reference
                tmpText.text = text;
        }
    }
}