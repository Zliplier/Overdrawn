using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu(fileName = "Card Data", menuName = "Gameplay/Cards/Card Data")]
    public class SO_CardData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }
        [field: SerializeField] public string Effect { get; private set; }
    }
}