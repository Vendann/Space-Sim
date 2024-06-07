using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class ManagerAchievements : SingletonManager<ManagerAchievements> {
        private void OnEnable() {
            ManagerScore.OnNewScore += CheckAchievements;
        }

        private void OnDisable() {
            ManagerScore.OnNewScore -= CheckAchievements;
        }

        private void CheckAchievements(int newScore) {
            if (newScore > 9)
                Debug.Log("йнялня 6 дюкх кюгеп");
        }
    }
}
