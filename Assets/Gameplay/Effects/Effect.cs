using System;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.Effects
{
    [Serializable]
    public abstract class Effect
    {
        public abstract GameAction GetGameAction();
    }
}