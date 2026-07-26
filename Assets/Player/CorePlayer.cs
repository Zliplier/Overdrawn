using Unity.Cinemachine;
using UnityEngine;
using Zlipacket.CoreZlipacket.Player.Input;
using Zlipacket.CoreZlipacket.Tools;

namespace Player
{
    public class CorePlayer : Singleton<CorePlayer>
    {
        [Header("Components")]
        public SO_InputReader inputReader;
        public GameObject bodyRoot;
        public Rigidbody rb;
        public Collider col;
        public Animator animator;
        public CinemachineCamera cam;
        
    }

    public enum FaceDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}