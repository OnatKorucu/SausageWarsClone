using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseCollectable : MonoBehaviour
    {
        public int score;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Player" || collision.collider.transform.parent?.tag == "Player")
            {
                OnCollected();
            }
        }

        protected abstract void OnCollected();  

    }
}