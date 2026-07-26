using System;
using Gameplay.Cards;
using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.Effects
{
    [Serializable]
    public class DrawCardEffect : Effect
    {
        [SerializeField] private int amount;
        public override GameAction GetGameAction()
        {
            GA_DrawCard gaDrawCard = new(amount);
            return gaDrawCard;
        }
    }
}