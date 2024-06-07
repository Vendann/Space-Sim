using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
	public class WeaponSpawner : MonoBehaviour, IWeapon {
		[SerializeField] private FlyweightDefinition flyweightDefinition;

		// Data Intrinsic
		[SerializeField] private WeaponSpawnerDefinition weaponSpawnerDef;

		// Data Extrinsic
		private DataWeaponExtrinsic _dataWeaponExtrinsic;

		// Serialized for Debug
		[Header("Inner Workings")]
		[SerializeField] private bool canFire = true;

		// private WaitForSeconds _waitForSecondsSpawning;
		private float _waitTimeSpawning => weaponSpawnerDef.cooldownTimeTotal;

		private WaitForSeconds waitForSecondsMuzzleflash;
		[SerializeField] private float waitTimeMuzzleflash = 0.05f;

		[Header("Links")]
		[SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject muzzleflash;

		private void OnEnable() {
			canFire = true;
		}

        public void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic) {
			_dataWeaponExtrinsic = dataWeaponExtrinsic;
            waitForSecondsMuzzleflash = new WaitForSeconds(waitTimeMuzzleflash);
        }


        public Vector3 FireWeapon(Vector3 targetPosition) {
			if (!canFire) return Vector3.zero;

			// var spawned = Instantiate(_projectilePrefab, _muzzle.position, Quaternion.LookRotation
			// (targetPosition - transform.position));
			var spawned = FactoryFlyweight.Instance.Spawn(flyweightDefinition, transform.position, Quaternion.LookRotation
				(targetPosition - transform.position));
            spawned.GetComponent<IWeaponSpawnable>().Initialize(_dataWeaponExtrinsic);
			VisualizeFiring(targetPosition);
			
			StartCoroutine(ExecuteCooldown(_waitTimeSpawning));
			
			return Vector3.zero;
		}

		// _muzzle VFX
		public void VisualizeFiring(Vector3 targetPosition) {
			if (muzzleflash != null) {
                StartCoroutine(VisualizeMuzzleflash(targetPosition));
            }
		}

        private IEnumerator VisualizeMuzzleflash(Vector3 targetPosition) {
            muzzleflash.transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
            muzzleflash.SetActive(true);

            yield return waitForSecondsMuzzleflash;
            muzzleflash.SetActive(false);
        }

        private IEnumerator ExecuteCooldown(float delay) {
			canFire = false;

			yield return new WaitForSeconds(delay);
			canFire = true;
		}
    }
}