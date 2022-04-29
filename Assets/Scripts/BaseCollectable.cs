using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseCollectable : MonoBehaviour
    {
        public int score;
        public static event Action OnCollected;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Player" || collision.collider.transform.parent?.tag == "Player")
            {
                BecomeCollected();
            }
        }

        protected virtual void BecomeCollected()
        {
            OnCollected?.Invoke();;
        }
    }
}