using System;
using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int totalScore;

        private void OnEnable()
        {
            CrystalCollectable.OnScoreChanged += HandleScoreChanged;
        }

        private void HandleScoreChanged(int scoreToAdd)
        {
            totalScore += scoreToAdd;
            Debug.Log("SKOR" + totalScore);
        }
    }
}