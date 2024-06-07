using UnityEngine;
using System.Collections;

namespace Space {
	public class RotateMove : MonoBehaviour {

		public Transform target;
		public float rotationSpeed = 10f;
		public float movingSpeed = 0.5f;

		private IEnumerator coroutineMoveSun;
		private WaitForSeconds waitForMoveSun;
		[SerializeField] private float waitForMoveSunDelay = 0.1f;

		private void Start() {
			if (target == null)
				target = this.gameObject.transform;
			waitForMoveSun = new WaitForSeconds(waitForMoveSunDelay);
			StartCoroutineMoveSun();

		}

		private void Update() {
			Rotate();
		}

		private void MoveSun() {
			if (gameObject.name == "Sun")
				transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
		}

		private void Rotate() {
			transform.RotateAround(target.transform.position, target.transform.up, rotationSpeed * Time.deltaTime);
		}

		[ContextMenu("StartCoroutineMoveSun")]
		public void StartCoroutineMoveSun() {
			coroutineMoveSun = MoveSunCor();
			StartCoroutine(coroutineMoveSun);
		}

		[ContextMenu("StopCoroutineMoveSun")]
		public void StopCoroutineMoveSun() {
			StopCoroutine(coroutineMoveSun);
		}

		private IEnumerator MoveSunCor() {
			while (true)
			{
				yield return waitForMoveSun;
				MoveSun();
			}
		}
	}
}