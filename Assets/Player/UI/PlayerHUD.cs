using Player.Script;
using TMPro;
using UnityEngine.UI;
using Zlipacket.CoreZlipacket.Tools;

namespace Player.UI
{
    public class PlayerHUD : Singleton<PlayerHUD>
    {
        public PlayerStats playerStats;
        
        public HealthUI healthDisplay;
        public EnergyUI energyDisplay;

        public override void Awake()
        {
            base.Awake();
            
            playerStats.onHealthChanged.AddListener((health, maxHealth) => healthDisplay.OnHealthChanged(health, maxHealth));
            playerStats.onEnergyChanged.AddListener((energy, maxEnergy) =>
            {
                energyDisplay.OnEnergyChanged(energy, maxEnergy);
            });
        }

        private void Start()
        {
            
            
            playerStats.Health = playerStats.MaxHealth;
            playerStats.Energy = 0f;
        }
        
        
    }
}