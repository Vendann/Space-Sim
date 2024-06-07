﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class SelfDestruction : MonoBehaviour {
        public float TimeBeforeDestruction = 2f;

        private void Start() {
            StartCoroutine(SelfDestruct());
        }

        private IEnumerator SelfDestruct() {
            yield return new WaitForSeconds(TimeBeforeDestruction);

            Destroy(gameObject);
        }
    }
}