using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space
{
    public class ManagerPlayerShips : MonoBehaviour
    {
        public int CurrentShipID;
        public Transform ShipVisuals;
        public GameObject CurrentShip;

        [SerializeField] private List<GameObject> ShipsPrefabs = new List<GameObject>();

        void Update()
        {
            if (Input.GetButtonDown("ChangeShip")) {
                ChangeShipToNext();
            }
        }

        private void ChangeShipToNext() {
            CurrentShipID++;
            if (CurrentShipID == ShipsPrefabs.Count) {
                CurrentShipID = 0;
            }
            ChangeShip(CurrentShipID);
        }

        private void ChangeShip(int id) {
            Destroy(CurrentShip);
            CurrentShip = Instantiate(ShipsPrefabs[CurrentShipID], ShipVisuals);
        }
    }
}
