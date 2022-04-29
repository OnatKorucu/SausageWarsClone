using System;

namespace DefaultNamespace
{
    public class CrystalCollectable : BaseCollectable, IScoreChanger
    {
        public static event Action<int> OnScoreChanged;

        protected override void OnCollected()
        {
            ChangeScore();
            Destroy(gameObject);
        }

        public void ChangeScore()
        {
            OnScoreChanged?.Invoke(score);
        }

    }
}