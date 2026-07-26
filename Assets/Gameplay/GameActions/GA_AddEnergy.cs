using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.GameActions
{
    public class GA_AddEnergy : GameAction
    {
        public int amount { get; set; }

        public GA_AddEnergy(int amount)
        {
            this.amount = amount;
        }
    }
}