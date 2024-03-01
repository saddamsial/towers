using System.Collections;
using System.Collections.Generic;
using GameStates;
using Tower.Floor;
using UnityEngine;
using Utils.PoolSystem;

namespace Tower
{
    public class TowerBase : MonoBehaviour
    {
        public GameObject floorPrefab;
        public GameObject tempFloor;
        public List<GameObject> floors = new();

        public virtual void RearrangeFloors(FloorBase floorObj)
        {
            floors.Remove(floorObj.gameObject);
            if (floors.Count <= 0)
            {
                GameStateManager.Instance.SetState(GameStateManager.Instance.onCompleteState);
                return;
            }
            for (int i = 0; i < floors.Count; i++)
            {
                floors[i].GetComponent<FloorBase>().MoveToNewPositionAfterDestroy(transform.localPosition + 1.6f * i * Vector3.up, delay: 0.1f * i);
            }
        }

        public virtual void AddFloor(int whichFloor)
        {
            SpawnFloor();
        }
        public virtual void AddFloor(int whichFloor, bool isNewFloor)
        {
            SpawnFloor();
        }

        public void SpawnFloor()
        {
            tempFloor = floorPrefab.Spawn(transform.localPosition + 1.6f * floors.Count * Vector3.up, transform.localRotation, transform);
        }
    }
}
