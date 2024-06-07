using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space {
    public class GameAgent : MonoBehaviour {
        public enum Faction {
            Player,
            Allies,
            Enemies
        }

        public Faction ShipFaction;
    }
}