using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    [CreateAssetMenu(fileName = "Def WS Default", menuName = "Definitions/Battle/WeaponSpawnerDefinition")]
    public class WeaponSpawnerDefinition : ScriptableObject
    {
        [SerializeField] public string ID = "";
        [TextArea]
        [SerializeField] public string Name = "";

        [Range(0f, 5f)]
        public float cooldownTimeTotal = 0.25f;

        private void OnValidate() {
            if (ID == "") {
                ID = Guid.NewGuid().ToString();
            }
        }
    }
}
