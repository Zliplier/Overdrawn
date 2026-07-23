using System;
using UnityEngine;
using UnityEngine.Events;

namespace Player.Script
{
    public class PlayerStats : PlayerScript
    {
        [Header("Movement")]
        public float acceleration = 5f;
        public float deceleration = 20f;
        public float walkSpeed = 15f;
        [Range(0, 1)] public float turnCompensation = 0.9f;
        
        [Header("Combat")]
        #region Health

        [SerializeField] private float health = 100f;
        public const float MaxHealth = 100f;
        public float healthPercentage => Health / MaxHealth;
        public float Health
        {
            get => health;
            set
            {
                health = Mathf.Clamp(value, 0, MaxHealth);
                onHealthChanged?.Invoke(healthPercentage);
            }
        }
        public UnityEvent<float> onHealthChanged;

        #endregion

        #region Exhaustion

        [SerializeField] private float exhaustion = 3f;
        public float exhaustionDecayRate = 1f; // 1 per 1 irl second.
        public const float MaxExhaustion = 3f;
        public float exhaustionPercentage => Exhaustion / MaxExhaustion;
        public float Exhaustion
        {
            get => exhaustion;
            set
            {
                exhaustion = Mathf.Clamp(value, 0, Mathf.Infinity);
                onExhaustionChanged?.Invoke(exhaustionPercentage);
            }
        }
        public UnityEvent<float> onExhaustionChanged;

        #endregion


        private void OnDisable()
        {
            onHealthChanged.RemoveAllListeners();
            onExhaustionChanged.RemoveAllListeners();
        }
    }
}