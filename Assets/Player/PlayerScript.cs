using UnityEngine;
using Zlipacket.CoreZlipacket.Player.Input;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        [SerializeField] protected CorePlayer player;
        
        public GameObject bodyRoot => player.bodyRoot;
        public Rigidbody rb => player.rb;
        public Collider col => player.col;
        public Animator animator => player.animator;
        public SO_InputReader inputReader => player.inputReader;
    }
}