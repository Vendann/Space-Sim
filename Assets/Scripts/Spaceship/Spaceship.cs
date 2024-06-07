using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public enum ShipLogicType {
        PlayerFighter, EnemyFighter
    }

    public class Spaceship : MonoBehaviour {
        public GameAgent ShipAgent;

        public ShipLogicType shipLogicType;

        public IInputShipMovement IInputShipMovement;
        public IInputShipWeapons IInputShipWeapons;

        private void OnEnable() {
            if (ShipAgent == null)
                ShipAgent = GetComponent<GameAgent>();

            // Input
            if (IInputShipMovement == null)
                IInputShipMovement = GetComponent<IInputShipMovement>();
            if (IInputShipWeapons == null)
                IInputShipWeapons = GetComponent<IInputShipWeapons>();
        }
    }
}