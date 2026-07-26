using Gameplay.Cards;
using Gameplay.Effects;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.GameActions
{
    public class GA_PerformEffect : GameAction
    {
        public Effect effect { get; set; }

        public GA_PerformEffect(Effect effect)
        {
            this.effect = effect;
        }
    }
}