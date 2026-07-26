using System.Collections.Generic;
using Gameplay.Effects;
using UnityEngine;

namespace Gameplay.Cards
{
    public class Card
    {
        private readonly SO_CardData cardData;

        public Card(SO_CardData cardData)
        {
            this.cardData = cardData;
            Effects = cardData.Effects;
            Cost = cardData.Cost;
            Description = cardData.Description;
        }
        
        public Sprite Sprite { get => cardData.Sprite; }
        public string Title { get => cardData.Title; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public List<Effect> Effects { get; set; }
    }
}