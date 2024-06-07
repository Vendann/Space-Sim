using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    [CreateAssetMenu(fileName = "Def Asteroid", menuName = "Definitions/Def Asteroid")]
    public class AsteroidDefinition : FlyweightDefinition
    {
        [SerializeField] private float minScale = 0.6f;
        [SerializeField] private float maxScale = 1.4f;

        // [SerializeField] private float rotationOffset = 100f;
        // private Vector3 randomRotation;

        public override Flyweight Create() {
            var gameObject = Instantiate(DefinitionPrefab);
            gameObject.SetActive(false);
            gameObject.name = DefinitionPrefab.name;

            var flyweight = gameObject.AddComponent<Asteroid>();
            flyweight.Definition = this;

            Vector3 originalScale = flyweight.transform.localScale;
            //xyz
            Vector3 newScale = new Vector3(Random.Range(minScale, maxScale),
                Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
            flyweight.transform.localScale = new Vector3(originalScale.x * newScale.x,
                originalScale.y * newScale.y, originalScale.z * newScale.z);

            //randomRotation = new Vector3(Random.Range(-rotationOffset, rotationOffset),
            //    Random.Range(-rotationOffset, rotationOffset), Random.Range(-rotationOffset, rotationOffset));

            return flyweight;
        }
    }
}
