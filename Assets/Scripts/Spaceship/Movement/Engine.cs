using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class Engine : MonoBehaviour {
        [Serializable]
        class EngineVisuals {
            [SerializeField] private ParticleSystem particleSystem;

            [Header("Settings")]
            [SerializeField] private float psEmittedMax = 50;
            [SerializeField] private float psEmittedMin = 0;
            [SerializeField] private float visualizationLerpRate = 0.25f;

            [Header("Current Values")]
            [SerializeField] private float psEmittedCurr = 0;
            [SerializeField] private float visualizationCurrNormalized = 0; // 0-1
            public void VisualizeThrust(float inputMove) {
                var emission = particleSystem.emission;
                visualizationCurrNormalized = Mathf.Lerp(visualizationCurrNormalized, inputMove, visualizationLerpRate);
                psEmittedCurr = psEmittedMax * visualizationCurrNormalized;
                emission.rateOverTime = Mathf.Max(psEmittedCurr, psEmittedMin);
            }
        }

        [SerializeField] private float moveSpeed = 100f;
        [SerializeField] private List<EngineVisuals> engineVisuals = new List<EngineVisuals>();

        public Vector3 Thrust(float inputMove) {
            VisualizeThrust(inputMove);
            var calculatedThrust = inputMove * moveSpeed;
            return -transform.forward * calculatedThrust;
        }

        public void VisualizeThrust(float inputMove) {
            foreach (var ev in engineVisuals) {
                ev.VisualizeThrust(inputMove);
            }
        }
    }
}