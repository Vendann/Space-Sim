using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Space {
    public class ShipWeapons : MonoBehaviour {
        public Spaceship Spaceship;
        [SerializeField] private Rigidbody shipRigidbody;
        public List<IWeapon> Weapons = new List<IWeapon>();
        public float MaxDistanceToTarget = 250f;
        [SerializeField] private LayerMask layerMask;

        private Ray ray;

        private void Awake() {
            if (Spaceship == null)
                Spaceship = GetComponentInParent<Spaceship>();
            if (shipRigidbody == null)
                shipRigidbody = GetComponentInParent<Rigidbody>();
        }

        [ContextMenu("InitWeapons")]
        public void InitWeapons() {
            Weapons = GetComponentsInChildren<IWeapon>().ToList();
            foreach (var weapon in Weapons)
            {
                weapon.Initialize(new DataWeaponExtrinsic()
                    { ShipRigidbody = shipRigidbody, GameAgent = Spaceship.ShipAgent });
            }
        }

        private void OnEnable() {
            InitWeapons();
            Spaceship.IInputShipWeapons.OnAttackInput += FireWeapons;
        }

        private void OnDisable() {
            Spaceship.IInputShipWeapons.OnAttackInput -= FireWeapons;
        }

        public void FireWeapons() {
            RaycastHit hit;

            if (Spaceship.shipLogicType == ShipLogicType.PlayerFighter) {
                ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                if (Physics.Raycast(ray, out hit, MaxDistanceToTarget, layerMask)) {
                    foreach (var weapon in Weapons) {
                        weapon.FireWeapon(hit.point);
                    }
                }

                else {
                    foreach (var weapon in Weapons) {
                        weapon.FireWeapon(ray.origin + ray.direction * MaxDistanceToTarget);
                    }
                }
            }

            // ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

            else if (Spaceship.shipLogicType == ShipLogicType.EnemyFighter) {
                if (Physics.Raycast(transform.position, transform.forward, out hit, MaxDistanceToTarget, layerMask)) {
                    foreach (var weapon in Weapons) {
                        weapon.FireWeapon(hit.point);
                    }
                }

                else {
                    foreach (var weapon in Weapons) {
                        weapon.FireWeapon(transform.position + transform.forward * MaxDistanceToTarget);
                    }
                }
            }
        }
    }
}