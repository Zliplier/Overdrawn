using Gameplay.Cards;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.GameActions
{
    public class GA_PlayCard : GameAction
    {
        public Card Card { get; set; }

        public GA_PlayCard(Card card)
        {
            Card = card;
        }
    }
}