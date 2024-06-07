using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class ShipHealth : MonoBehaviour, IDamageable {
        [SerializeField] private float health;
        public float Health => health;
        [SerializeField] private GameObject PrefabEffectDestr;
        [SerializeField] private GameObject ScoreBonus;

        public void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender) {
            health -= damageAmount;
            if (health <= 0)
            {
                if (PrefabEffectDestr)
                    Instantiate(PrefabEffectDestr, transform.position, Quaternion.identity);
                if (ScoreBonus)
                    Instantiate(ScoreBonus, transform.position, Quaternion.identity);
                Debug.Log("!!!!!!!!!!!!!!!!!!");
                //Debug.Log($"Attacker: {sender.gameObject.name}\nAttacker faction: {sender.ShipFaction}");
                Destroy(gameObject);
            }
        }

        public void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender) {
            throw new System.NotImplementedException();
        }
    }
}