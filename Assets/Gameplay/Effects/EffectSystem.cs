
using System.Collections;
using Gameplay.GameActions;
using Player;
using UnityEngine;
using Zlipacket.CoreZlipacket.ActionSystem;
using Zlipacket.CoreZlipacket.Tools;


namespace Gameplay.Effects
{
    public class EffectSystem : Singleton<EffectSystem>
    {
        private void OnEnable()
        {
            ActionSystem.AttachPerformer<GA_PerformEffect>(EffectPerformer);
            ActionSystem.AttachPerformer<GA_Shoot>(ShootPerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<GA_PerformEffect>();
            ActionSystem.DetachPerformer<GA_Shoot>();
        }
        
        public IEnumerator EffectPerformer(GA_PerformEffect gaPerformEffect)
        {
            GameAction effectAction = gaPerformEffect.effect.GetGameAction();
            ActionSystem.Instance.AddReaction(effectAction);
            yield return null;
        }
        
        public IEnumerator ShootPerformer(GA_Shoot gaShoot)
        {
            Debug.Log($"Shoot in {gaShoot.direction} direction.");
            
            Quaternion targetRotation = Quaternion.LookRotation(gaShoot.direction, Vector3.up);
            
            GameObject projectile = Instantiate(gaShoot.projectile, CorePlayer.Instance.bodyRoot.transform.position, targetRotation);
            projectile.name = gaShoot.projectile.name;
            
            yield return null;
        }

        public IEnumerator SpawnPerformer(GA_Spawn gaSpawn)
        {
            
            
            
            yield return null;
        }
    }
}