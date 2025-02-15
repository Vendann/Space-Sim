using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public interface IDamageable {
        float Health { get; }
        void ReceiveDamage(float damageAmount, Vector3 hitPosition, GameAgent sender);
        void ReceiveHeal(float healAmount, Vector3 hitPosition, GameAgent sender);
    }
}