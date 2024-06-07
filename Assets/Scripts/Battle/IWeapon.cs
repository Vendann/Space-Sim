using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public interface IWeapon {
        Vector3 FireWeapon(Vector3 targetPosition);
        void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);
        void VisualizeFiring(Vector3 targetPosition);
    }
}