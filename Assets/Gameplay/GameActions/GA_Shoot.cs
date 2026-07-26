using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.GameActions
{
    public class GA_Shoot : GameAction
    {
        public Vector3 direction { get; set; }
        public GameObject projectile { get; set; }

        public GA_Shoot(Vector3 direction, GameObject projectile)
        {
            this.direction = direction;
            this.projectile = projectile;
        }
    }
}