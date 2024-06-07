using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Space {
    public class AsteroidHealth : MonoBehaviour, IDamageable {
        public float Health { get; set; }
        [SerializeField] private GameObject PrefabEffectDestr;
        [SerializeField] private GameObject PrefabAsteroidDivision;

        public bool Destroyed = false;
        public int DivisionCounter = 2;

        public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender) {
            Health -= damageAmount;
            if (Health <= 0 && !Destroyed)
            {
                if (PrefabEffectDestr)
                    Instantiate(PrefabEffectDestr, transform.position, Quaternion.identity);

                if (PrefabAsteroidDivision != null && DivisionCounter > 0)
                {
                    Vector3 shard1Pos = new Vector3(
                        transform.position.x + Random.Range(-0.2f, 0.2f),
                        transform.position.y + Random.Range(-0.2f, 0.2f),
                        transform.position.z + Random.Range(-0.2f, 0.2f));

                    Vector3 shard2Pos = new Vector3(
                        transform.position.x + Random.Range(-0.2f, 0.2f),
                        transform.position.y + Random.Range(-0.2f, 0.2f),
                        transform.position.z + Random.Range(-0.2f, 0.2f));

                    var s1 = Instantiate(PrefabAsteroidDivision, shard1Pos + PrefabAsteroidDivision.transform.localScale, Quaternion.identity);
                    var s2 = Instantiate(PrefabAsteroidDivision, shard2Pos - PrefabAsteroidDivision.transform.localScale, Quaternion.identity);

                    s1.GetComponent<AsteroidHealth>().DivisionCounter = DivisionCounter--;
                    s2.GetComponent<AsteroidHealth>().DivisionCounter = DivisionCounter--;
                }
                ManagerScore.Instance.AddScore(1);

                Destroyed = true;
                Destroy(gameObject);
            }
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender) {
            throw new System.NotImplementedException();
        }
    }
}