using System;

namespace DefaultNamespace
{
    public class CrystalCollectable : BaseCollectableBehaviour, IScoreChanger
    {
        public static event Action<int> OnScoreChanged;

        protected override void BecomeCollected()
        {
            base.BecomeCollected();
            ChangeScore();
            Destroy(gameObject);
        }

        public void ChangeScore()
        {
            OnScoreChanged?.Invoke(score);
        }
    }
}