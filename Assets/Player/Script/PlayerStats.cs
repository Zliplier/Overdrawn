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

        [SerializeField] private float health = 3f;
        public float MaxHealth = 3f;
        public float Health
        {
            get => health;
            set
            {
                health = Mathf.Clamp(value, 0, MaxHealth);
                onHealthChanged?.Invoke(health, MaxHealth);
            }
        }
        public UnityEvent<float, float> onHealthChanged;

        #endregion

        #region Energy

        [SerializeField] private float energy = 3f;
        public float energyDecayTime = 2f; // 1 per 2 irl second.
        public float MaxEnergy = 3f;
        public float Energy
        {
            get => energy;
            set
            {
                energy = Mathf.Clamp(value, 0, Mathf.Infinity);
                onEnergyChanged?.Invoke(energy, MaxEnergy);
            }
        }
        public UnityEvent<float, float> onEnergyChanged;
        public bool HasEnoughEnergy() => energy <= MaxEnergy;

        #endregion
        
        private void OnDisable()
        {
            onHealthChanged.RemoveAllListeners();
            onEnergyChanged.RemoveAllListeners();
        }

        private void FixedUpdate()
        {
            EnergyDecay();
        }

        private void EnergyDecay()
        {
            if (Energy > 0)
                Energy -= (1.0f / energyDecayTime) * Time.fixedDeltaTime;
            else
            {
                Energy = 0f;
            }
        }
    }
}