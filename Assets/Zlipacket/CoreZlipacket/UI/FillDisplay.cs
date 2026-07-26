using UnityEngine;
using UnityEngine.UI;

namespace Zlipacket.CoreZlipacket.UI
{
    public class FillDisplay : MonoBehaviour
    {
        public Image fill;

        public float Value
        {
            get => fill.fillAmount;
            set => fill.fillAmount = value;
        }
    }
}