using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class Asteroid : Flyweight {
        //[SerializeField] private float minScale = 0.6f;
        //[SerializeField] private float maxScale = 1.4f;

        [SerializeField] private float rotationOffset = 100f;
        private Vector3 randomRotation;

        //private void RandomScale() {
        //    Vector3 originalScale = transform.localScale;
        //    //xyz
        //    Vector3 newScale = new Vector3(Random.Range(minScale, maxScale),
        //        Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
        //    transform.localScale = new Vector3(originalScale.x * newScale.x,
        //        originalScale.y * newScale.y, originalScale.z * newScale.z);

        //    randomRotation = new Vector3(Random.Range(-rotationOffset, rotationOffset),
        //        Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));
        //}


        private void Start() {
            randomRotation = new Vector3(Random.Range(-rotationOffset, rotationOffset),
                Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));
        }

        private void Update() {
            transform.Rotate(randomRotation * Time.deltaTime);
        }
    }
}