using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class ShipLogicEnemyFighter : MonoBehaviour, IInputShipMovement, IInputShipWeapons {
        [field: Header("Ship Movement Values")]
        [field: SerializeField] public float CurrentInputMove { get; private set; }
        [field: SerializeField] public float CurrentInputStrafe { get; private set; }
        [field: SerializeField] public float CurrentInputRotatePitch { get; private set; }
        [field: SerializeField] public float CurrentInputRotateYaw { get; private set; }
        [field: SerializeField] public float CurrentInputRotateRoll { get; private set; }


        [field: Header("Ship Attack Values")]
        [field: SerializeField] public bool CurrentInputAttack { get; private set; }
        public event Action OnAttackInput;

        [Header("Multipliers")]
        [SerializeField] private float MovementSpeedDefault = 1f;
        [SerializeField] private float RotationSpeedDefault = 4f;

        [Header("Links")]
        private Rigidbody shipRigidbody;
        [SerializeField] private GameObject target;

        private void Awake() {
            if (shipRigidbody == null)
                shipRigidbody = GetComponent<Rigidbody>();
        }

        private void Update() {
            Think();
        }

        private void Think() {
            if (target == null)
                target = GameObject.FindGameObjectWithTag("Player");
            if (target != null) {
                OnAttackInput?.Invoke();

                CurrentInputMove = MovementSpeedDefault;

                var direction = target.transform.position - shipRigidbody.position;
                Debug.DrawRay(shipRigidbody.position, direction.normalized * 100, Color.cyan);

                var desiredRotation = Quaternion.LookRotation(direction);
                var lerpRotation = Quaternion.Slerp(shipRigidbody.rotation, desiredRotation, RotationSpeedDefault * Time.deltaTime);
                var lerpRotationVector3 = lerpRotation.eulerAngles;

                CurrentInputRotatePitch = lerpRotationVector3.x;
                CurrentInputRotateYaw = lerpRotationVector3.y;
                CurrentInputRotateRoll = lerpRotationVector3.z;
            }
        }
    }
}
