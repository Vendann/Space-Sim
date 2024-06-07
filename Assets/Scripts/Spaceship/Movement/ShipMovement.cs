using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class ShipMovement : MonoBehaviour {
        [Header("Movement Multipliers")]
        [SerializeField] private float pitchSpeed = 125000f;
        [SerializeField] private float yawSpeed = 125000f;
        [SerializeField] private float rollSpeed = 125000f;

        [SerializeField] private float moveSpeed = 125000f;
        [SerializeField] private float strafeSpeed = 125000f;
        private Vector3 strafeDirection = new Vector3(1, 0, 0);

        [Header("Drag Multipliers")]
        // [Range(0.5f, 50f)]
        // [SerializeField] private float _proportionalAngularDrag = 5f;
        [Range(10f, 1000f)]
        [SerializeField] private float _proportionalDrag = 100f;

        [Header("Links")]
        public Spaceship SpaceShip;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private List<Engine> Engines = new List<Engine>();
        [SerializeField] private IBehaviourRotate behaviourRotate;

        private void OnEnable() {
            if (SpaceShip.shipLogicType == ShipLogicType.PlayerFighter)
                behaviourRotate = gameObject.AddComponent<BehaviourRotateAddTorque>();

            else if (SpaceShip.shipLogicType == ShipLogicType.EnemyFighter)
                behaviourRotate = gameObject.AddComponent<BehaviourRotateQuaternion>();

            behaviourRotate.Initialize(_rigidbody, pitchSpeed, yawSpeed, rollSpeed);
        }

        void FixedUpdate() {
            behaviourRotate.Turn(SpaceShip.IInputShipMovement.CurrentInputRotatePitch,
                SpaceShip.IInputShipMovement.CurrentInputRotateYaw,
                SpaceShip.IInputShipMovement.CurrentInputRotateRoll);
            Move(SpaceShip.IInputShipMovement.CurrentInputMove);
            Strafe(SpaceShip.IInputShipMovement.CurrentInputStrafe);
        }

        //private void Turn(float inputPitch, float inputYaw, float inputRoll) {
        //    if (!Mathf.Approximately(0f, inputPitch)) {
        //        _rigidbody.AddTorque(transform.right * inputPitch * pitchSpeed * Time.fixedDeltaTime);
        //    }
        //    if (!Mathf.Approximately(0f, inputYaw)) {
        //        _rigidbody.AddTorque(transform.up * inputYaw * yawSpeed * Time.fixedDeltaTime);
        //    }
        //    if (!Mathf.Approximately(0f, inputRoll)) {
        //        _rigidbody.AddTorque(transform.forward * inputRoll * rollSpeed * Time.fixedDeltaTime);
        //    }

        //    _rigidbody.AddTorque(-_rigidbody.angularVelocity * _proportionalAngularDrag * Time.fixedDeltaTime);
        //}

        private void Move(float inputMove) {
            Vector3 resultingThrust = new Vector3();
            foreach (var engine in Engines) {
                resultingThrust += engine.Thrust(inputMove);
            }
            _rigidbody.AddForce(resultingThrust * moveSpeed * Time.fixedDeltaTime);
            _rigidbody.AddForce(- _rigidbody.velocity * _proportionalDrag * Time.fixedDeltaTime);
        }

        private void Strafe(float inputStrafe) {
            _rigidbody.AddForce(strafeDirection * inputStrafe * strafeSpeed * Time.fixedDeltaTime);
            _rigidbody.AddForce(-_rigidbody.velocity * _proportionalDrag * Time.fixedDeltaTime);
        }
    }
}