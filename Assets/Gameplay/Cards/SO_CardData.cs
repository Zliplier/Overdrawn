using System.Collections.Generic;
using Gameplay.Effects;
using SerializeReferenceEditor;
using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu(fileName = "Card Data", menuName = "Gameplay/Cards/Card Data")]
    public class SO_CardData : ScriptableObject
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: Space]
        [field: SerializeField] public string Title { get; private set; }
        [field: TextArea(5, 10)] [field: SerializeField] public string Description { get; private set; }
        [field: Space]
        [field: SerializeField] public int Cost { get; private set; }
        [field: Space]
        [field: SerializeReference, SR] public List<Effect> Effects { get; private set; }
    }
}