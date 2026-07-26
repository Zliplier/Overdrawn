using System.Collections.Generic;
using UnityEngine;
using Zlipacket.CoreZlipacket.UI;

namespace Player.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private FillDisplay heartPrefab;
        [SerializeField] private RectTransform heartParent;
        
        [SerializeField] private List<FillDisplay> hearts;
        
        public void OnHealthChanged(float value, float maxValue)
        {
            if (hearts.Count < maxValue)
            {
                // TODO: Add additional hearts.
            }
            
            for (int i = 0; i < hearts.Count; i++)
            {
                // Full Heart
                if (value >= 1f)
                {
                    hearts[i].Value = 1f;
                }
                // Half Heart
                else if (value > 0f)
                {
                    hearts[i].Value = 0.5f;
                }
                // Empty
                else
                {
                    hearts[i].Value = 0f;
                }
                
                value--;
            }
        }
    }
}