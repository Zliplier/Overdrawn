using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;

namespace Gameplay.GameActions
{
    public class GA_Spawn : GameAction
    {
        public Vector3 position { get; set; }
        public GameObject spawn { get; set; }

        public GA_Spawn(Vector3 position, GameObject spawn)
        {
            this.position = position;
            this.spawn = spawn;
        }
    }
}