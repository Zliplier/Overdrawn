using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Cards
{
    [CreateAssetMenu(fileName = "Deck Data", menuName = "Gameplay/Cards/Deck Data")]
    public class SO_DeckData : ScriptableObject
    {
        public List<SO_CardData> deckList;
    }
}