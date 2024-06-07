using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Space {
    public class ManagerScore : SingletonManager<ManagerScore> {
        public int Score;
        public TextMeshProUGUI ScoreText;

        public static Action<int> OnAddScore;
        public static Action<int> OnNewScore;

        public UnityEvent<int> EventAddScore;

        private void Start() {
            if (ScoreText == null)
                Debug.LogError("ManagerScore. ScoreText null");
            else
                ScoreText.text = Score.ToString();
        }

        private void OnEnable() {
            OnAddScore += IncreaseScore;
        }

        private void OnDisable() {
            OnAddScore -= IncreaseScore;
        }

        public void AddScore(int scoreDelta) {
            if (Debugging)
                Debug.Log("AddScore", this);
            OnAddScore?.Invoke(scoreDelta);
        }

        public void IncreaseScore(int scoreDelta) {
            Score += scoreDelta;
            ScoreText.text = Score.ToString();

            OnNewScore?.Invoke(Score);
            EventAddScore.Invoke(scoreDelta);
        }

        public void ReactOnAddScore() {
            Debug.Log("ReactOnAddScore");
        }
    }
}