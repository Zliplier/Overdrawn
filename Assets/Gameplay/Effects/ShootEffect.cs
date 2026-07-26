using System;
using Gameplay.Cards;
using Gameplay.GameActions;
using Player;
using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.Effects
{
    [Serializable]
    public class ShootEffect : Effect
    {
        [SerializeField] private GameObject projectile;
        
        public override GameAction GetGameAction()
        {
            Vector3 aimDirection = CardManager.Instance.aimPosition - CorePlayer.Instance.transform.position;
            aimDirection = new Vector3(aimDirection.x, 0f, aimDirection.z).normalized;
            
            GA_Shoot gaShoot = new(aimDirection, projectile);
            return gaShoot;
        }
    }
}