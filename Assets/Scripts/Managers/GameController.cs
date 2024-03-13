using System;
using Data_and_Scriptable.GunSo;
using Tower;
using Tower.Floor;
using UnityEngine;

namespace Managers
{
    public class GameController : Utils.Singleton<GameController>
    {
        public static Action<GameObject, FloorBase> onGunPlaced;
        public static Action<bool> onEditMode;
        public static Action<FloorBase> onDied;
        public static Action<Transform, int, TowerController, GunSo, FloorMine> onFloorAdded;
        public static Action<int> onCloseCameraPressed;
        public static Action onZoomOutFromGun;
        public static Action<GameObject> onSwapGun;
        public static Action<Transform> onFreeze;

        public int currentFocusedGun;
        public void InvokeEditMode(bool state)
        {
            onEditMode?.Invoke(state);
        }
        public void InvokeSwapGun(GameObject gun = null)
        {
            onSwapGun?.Invoke(gun);
        }
    }
}