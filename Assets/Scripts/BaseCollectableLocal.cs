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
                BecomeCollected();
            }
        }

        protected virtual void BecomeCollected()
        {
            FindObjectsOfType<CollectableSpawner>()[0].HandleOnCollected();
            Debug.Log("xxx BecomeCollected");
        }
        
    }
}