using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Creator_Kit___RPG.Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class MusicTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("MusicLabel")]
        [SerializeField] private string musicLabel = "";

        private MusicController musicController;

        private void Awake()
        {
            // Auto-find if the field was left empty
            if (musicController == null)
            {
                musicController = FindObjectOfType<MusicController>();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (musicController != null)
            {
                musicController.CrossFadeTo(musicLabel);
            }
        }
    }
}
