using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public interface IWeaponSpawnable
    {
        void Initialize(DataWeaponExtrinsic dataWeaponExtrinsic);
    }
}
