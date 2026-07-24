using UnityEngine;

namespace Gameplay.Cards
{
    public class Card
    {
        private readonly SO_CardData cardData;

        public Card(SO_CardData cardData)
        {
            this.cardData = cardData;
            Effect = cardData.Effect;
            Cost = cardData.Cost;
        }
        
        public Sprite Sprite { get => cardData.Sprite; }
        public string Title { get => cardData.name; }
        public int Cost { get; set; }
        public string Effect { get; set; }

        public void PerformEffect()
        {
            Debug.Log("Effect " + Effect + " cost: " + Cost);
        }
    }
}