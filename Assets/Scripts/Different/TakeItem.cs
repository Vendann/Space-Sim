using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class TakeItem : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player"))
            {
                ManagerScore.Instance.AddScore(100);
                Destroy(gameObject);
            }
        }
    }
}
