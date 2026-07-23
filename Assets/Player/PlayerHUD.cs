using System;
using Player.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zlipacket.CoreZlipacket.Tools;

namespace Player
{
    public class PlayerHUD : Singleton<PlayerHUD>
    {
        public PlayerStats playerStats;
        
        public Slider healthBar;
        public Slider exhaustBar;
        public TextMeshProUGUI exhaustText;

        private void Start()
        {
            playerStats.onHealthChanged.AddListener((value) => healthBar.value = value);
            playerStats.onExhaustionChanged.AddListener((value) =>
            {
                exhaustBar.value = value;
                exhaustText.SetText(value.ToString("N0"));
            });
        }
        
        
    }
}