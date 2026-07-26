using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zlipacket.CoreZlipacket.UI;

namespace Player.UI
{
    public class EnergyUI : MonoBehaviour
    {
        [SerializeField] private FillDisplay energyPrefab;
        [SerializeField] private RectTransform energyParent;
        [SerializeField] private TextMeshProUGUI energyText;
        
        [SerializeField] private List<FillDisplay> energyGauge;
        
        [Header("Colors")]
        [SerializeField] private Color energyColor;
        [SerializeField] private Color overdrawnColor;
        
        public void OnEnergyChanged(float value, float maxValue)
        {
            if (energyGauge.Count < (int)maxValue)
            {
                // Add additional gauge.
                FillDisplay display = Instantiate(energyPrefab, energyParent);
                display.gameObject.name = energyPrefab.name;
                energyGauge.Add(display);
                
                display.transform.SetSiblingIndex(energyParent.childCount - 2);
            }

            // Overdrawn
            if (value >= maxValue)
            {
                float overdrawnValue = value - energyGauge.Count;
                for (float i = overdrawnValue; i > 0; i--)
                {
                    FillDisplay display = Instantiate(energyPrefab, energyParent);
                    display.gameObject.name = energyPrefab.name;
                    display.transform.SetAsLastSibling();
                    energyGauge.Add(display);
                }
                EnergyOverdrawn();
            }
            else
            {
                EnergyNormal();
            }
            
            energyText.text = Mathf.CeilToInt(value).ToString();
            
            for (int i = 0; i < energyGauge.Count; i++)
            {
                // Full Gauge
                if (value >= 1f)
                {
                    energyGauge[i].Value = 1f;
                }
                // Percentile Gauge
                else if (value > 0f)
                {
                    energyGauge[i].Value = value;
                }
                // Empty
                else
                {
                    energyGauge[i].Value = 0f;

                    // This is Empty Overdrawn Gauge, so remove it.
                    if (i >= maxValue)
                    {
                        GameObject temp = energyGauge[^1].gameObject;
                        energyGauge.RemoveAt(energyGauge.Count - 1);
                        Destroy(temp);
                    }
                    
                    
                }
                
                value--;
            }
        }

        public void EnergyOverdrawn()
        {
            foreach (var energy in energyGauge)
            {
                energy.fill.color = overdrawnColor;
            }
        }

        public void EnergyNormal()
        {
            foreach (var energy in energyGauge)
            {
                energy.fill.color = energyColor;
            }
        }
    }
}