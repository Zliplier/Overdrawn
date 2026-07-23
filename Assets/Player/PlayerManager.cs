using Zlipacket.CoreZlipacket.Tools;

namespace Player
{
    public class PlayerManager : PersistantSingleton<PlayerManager>
    {
        public bool IsPlayerActive => CorePlayer.Instance.gameObject.activeSelf;

        public void SetPlayerActive(bool active)
            => CorePlayer.Instance.gameObject.SetActive(active);
    }
}