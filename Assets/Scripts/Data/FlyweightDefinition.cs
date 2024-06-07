using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public abstract class FlyweightDefinition : ScriptableObject
    {
        [SerializeField] public string ID = "";
        [TextArea]
        [SerializeField] public string Name = "";

        public FlyweightDefType DefinitionType;
        public GameObject DefinitionPrefab;

        public abstract Flyweight Create();
        public virtual void OnGet(Flyweight flyweight) {
            flyweight.gameObject.SetActive(true);
        }

        public virtual void OnRelease(Flyweight flyweight) {
            flyweight.gameObject.SetActive(false);
        }

        public virtual void OnDestroyPooledObject(Flyweight flyweight) {
            Destroy(flyweight.gameObject);
        }

        protected void OnValidate() {
            if (ID == "") {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}
