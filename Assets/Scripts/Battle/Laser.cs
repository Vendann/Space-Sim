using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//-----------------------------------------------—œ–Œ—»“‹ œ–Œ ƒ»—“¿Õ“

namespace Space {
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour, IWeapon
    {
        public float MaxDistance = 100f;
        public float DamageAmount = 5f;

        // Data Extrinsic
        private DataWeaponExtrinsic _dataWeaponExtrinsic;

        private Coroutine coroutineFiring;
        private WaitForSeconds waitForFiring;
        [SerializeField] private float waitTimeFiring = 0.1f;

        [Header("Inner Workings")]
        [SerializeField] private bool canFire;

        [Header("Visual FX")]
        [SerializeField] private float lineRenAnimSpeed = 1f;
        [SerializeField] private float lineRenAnimDeltaTime = 0f;

        [Header("Links")]
        [SerializeField] private LineRenderer lineRenderer;
        private ShipWeapons ShipWeapons;

        public List<IDamageable> TargetsHit = new List<IDamageable>();

        private void Awake() {
            if (ShipWeapons == null)
                ShipWeapons = GetComponentInParent<ShipWeapons>();
            if (lineRenderer == null)
                lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start() {
            waitForFiring = new WaitForSeconds(waitTimeFiring);

            lineRenderer.enabled = false;
            canFire = true;
        }

        private void Update() {
            if (!lineRenderer.enabled) return;

            lineRenderer.SetPosition(0, transform.position);
            // lineRenderer.SetPosition(1, targetPosition);

            lineRenAnimDeltaTime += Time.deltaTime;
            if (lineRenAnimDeltaTime > 1.0f)
                lineRenAnimDeltaTime = 0f;

            lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(lineRenAnimDeltaTime * lineRenAnimSpeed, 0f));
        }

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic) {
            _dataWeaponExtrinsic = dataWeaponExtrinsic;
        }

        public Vector3 FireWeapon(Vector3 targetPosition) {
            if (!canFire) return Vector3.zero;

            RaycastHit hitInfo;
            var direction = targetPosition - transform.position;
            if (Physics.Raycast(transform.position, direction, out hitInfo, MaxDistance))
            {
                var targetHit = hitInfo.transform;
                if (targetHit != null) {
                    Debug.Log($"FireWeapon. targetHit: {targetHit.name}");
                    var damageableHit = targetHit.GetComponent<IDamageable>();
                    if (damageableHit != null) {
                        TargetsHit.Add(damageableHit);
                        Damage(DamageAmount, targetHit.position, _dataWeaponExtrinsic.GameAgent);
                    }
                    VisualizeFiring(targetHit.position);
                    return targetHit.position;
                }
            }
            // ≈ÒÎË ÌÂ ·˚ÎÓ ÔÓÔ‡‰‡ÌËÈ
            VisualizeFiring(transform.position + direction.normalized * MaxDistance);
            return targetPosition;
        }
        
        private void Damage(float damageAmount, Vector3 targetHitPosition, GameAgent sender) {
            foreach (var targetHit in TargetsHit)
            {
                targetHit.ReceiveDamage(damageAmount, targetHitPosition, sender);
            }
            TargetsHit.Clear();
        }

        public void VisualizeFiring(Vector3 targetPosition) {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, targetPosition);
            canFire = false;
            coroutineFiring = StartCoroutine(FiringCor());
        }

        private IEnumerator FiringCor() {
            yield return waitForFiring;
            canFire = true;
            yield return waitForFiring;
            if (canFire)
                lineRenderer.enabled = false;
        }
    }
}