using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;

public class GA_DrawCard : GameAction
{
    public int Amount { get; set; }

    public GA_DrawCard(int amount)
    {
        Amount = amount;
    }
}
