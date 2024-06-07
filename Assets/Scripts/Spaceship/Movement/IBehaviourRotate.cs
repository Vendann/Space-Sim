using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public interface IBehaviourRotate
    {
        void Initialize(Rigidbody rigidbody, float pitchSpeed, float yawSpeed, float rollSpeed);
        void Turn(float inputPitch, float inputYaw, float inputRoll);
    }
}
