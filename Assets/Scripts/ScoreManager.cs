using System;
using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int totalScore;
        [ReadOnly] private const int MINIMUM_SCORE = 0;

        private void OnEnable()
        {
            CrystalCollectable.OnScoreChanged += HandleScoreChanged;
        }
        
        private void OnDisable()
        {
            CrystalCollectable.OnScoreChanged -= HandleScoreChanged;
        }

        private void HandleScoreChanged(int scoreToAdd)
        {
            totalScore += scoreToAdd;

            if (totalScore < MINIMUM_SCORE)
            {
                totalScore = MINIMUM_SCORE;
            }
            Debug.Log("SKOR" + totalScore); //TODO: Delete
        }
    }
}