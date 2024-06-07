using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class BehaviourRotateQuaternion : MonoBehaviour, IBehaviourRotate
    {
        [Header("Movement Multipliers")]
        [SerializeField] private float _pitchSpeed = 125000f;
        [SerializeField] private float _yawSpeed = 125000f;
        [SerializeField] private float _rollSpeed = 125000f;

        [Header("Links")]
        [SerializeField] private Rigidbody _rigidbody;

        public void Initialize(Rigidbody rigidbody, float pitchSpeed, float yawSpeed, float rollSpeed) {
            _rigidbody = rigidbody;
            _pitchSpeed = pitchSpeed;
            _yawSpeed = yawSpeed;
            _rollSpeed = rollSpeed;
        }

        public void Turn(float inputPitch, float inputYaw, float inputRoll) {
            _rigidbody.rotation = Quaternion.Euler(inputPitch, inputYaw, inputRoll);
        }
    }
}
