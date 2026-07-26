using UnityEngine;

namespace Player.Script
{
    public class PlayerAnimator : PlayerScript
    {
        public SpriteRenderer spriteRenderer;
        
        public void SetAnimation(string animationName, int layer = 0)
        {
            animator.Play(animationName, layer);
        }
    }
}