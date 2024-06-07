using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class BehaviourRotateAddTorque : MonoBehaviour, IBehaviourRotate
    {
        [Header("Movement Multipliers")]
        [SerializeField] private float _pitchSpeed = 125000f;
        [SerializeField] private float _yawSpeed = 125000f;
        [SerializeField] private float _rollSpeed = 125000f;

        [Header("Drag Multipliers")]
        [Range(0.5f, 50f)]
        [SerializeField] private float _proportionalAngularDrag = 5f;

        [Header("Links")]
        [SerializeField] private Rigidbody _rigidbody;

        public void Initialize(Rigidbody rigidbody, float pitchSpeed, float yawSpeed, float rollSpeed) {
            _rigidbody = rigidbody;
            _pitchSpeed = pitchSpeed;
            _yawSpeed = yawSpeed;
            _rollSpeed = rollSpeed;
        }

        public void Turn(float inputPitch, float inputYaw, float inputRoll) {
            if (!Mathf.Approximately(0f, inputPitch)) {
                _rigidbody.AddTorque(transform.right * inputPitch * _pitchSpeed * Time.fixedDeltaTime);
            }
            if (!Mathf.Approximately(0f, inputYaw)) {
                _rigidbody.AddTorque(transform.up * inputYaw * _yawSpeed * Time.fixedDeltaTime);
            }
            if (!Mathf.Approximately(0f, inputRoll)) {
                _rigidbody.AddTorque(transform.forward * inputRoll * _rollSpeed * Time.fixedDeltaTime);
            }

            _rigidbody.AddTorque(-_rigidbody.angularVelocity * _proportionalAngularDrag * Time.fixedDeltaTime);
        }
    }
}
