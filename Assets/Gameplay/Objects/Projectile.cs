using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Objects
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody rb;

        public LayerMask targetLayer;
        public float speed = 2f;
        public float lifeTime = 3f;

        private Coroutine co_LifeTime = null;
        public bool IsRunning => co_LifeTime != null;

        public UnityEvent onHit;
        
        protected virtual void Start()
        {
            BeginLifetime();
            onHit.AddListener(Terminate);
        }

        protected virtual void Update()
        {
            Vector3 targetVelocity = transform.forward * speed;
            targetVelocity.y = rb.linearVelocity.y; 
            
            Vector3 velocityChange = targetVelocity - rb.linearVelocity;
            
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        
        public virtual void BeginLifetime()
        {
            if (IsRunning)
                return;
            co_LifeTime = StartCoroutine(LifeTiming(lifeTime));
        }

        public virtual IEnumerator LifeTiming(float time)
        {
            yield return new WaitForSeconds(time);
            
            Terminate();
        }

        private void OnDestroy()
        {
            if (IsRunning)
                StopCoroutine(co_LifeTime);
        }

        protected virtual void Terminate()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == targetLayer.value)
            {
                onHit?.Invoke();
            }
        }
    }
}