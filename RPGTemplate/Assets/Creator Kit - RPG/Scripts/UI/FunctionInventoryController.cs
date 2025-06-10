using RPGM.Core;
using RPGM.Gameplay;
using RPGM.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Scripts.UI
{
    public class FunctionInventoryController : MonoBehaviour
    {
        public FunctionInventoryLayout layout;

        Camera mainCamera;
        GameModel model = Schedule.GetModel<GameModel>();
        SpriteUIElement spriteUIElement;

        public void Show(Vector3 position, string text)
        {
            var d = layout;
            gameObject.SetActive(true);
            d.SetText(text);
            SetPosition(position);
            model.input.ChangeNonBattleState(InputController.State.FunctionInventoryControl);
        }

        void SetPosition(Vector3 position)
        {
            var screenPoint = mainCamera.WorldToScreenPoint(position);
            position = spriteUIElement.camera.ScreenToViewportPoint(screenPoint);
            spriteUIElement.anchor = position;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        void Awake()
        {
            gameObject.SetActive(false);
            //layout.spriteRenderer.gameObject.SetActive(false);
            spriteUIElement = GetComponent<SpriteUIElement>();
            mainCamera = Camera.main;
        }
    }
}
