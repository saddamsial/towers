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
        public static Action<Transform, int, TowerController, GunSo, FloorMine, EnemyTower> onFloorAdded;
        public static Action<int> onCloseCameraPressed;
        public static Action onZoomOutFromGun;
        public static Action<GameObject> onSwapGun;
        public static Action<Transform, bool> onFreeze;
        public static Action<Vector2, Transform> onManagerImagePressed;
        public static Action<Transform, Transform> onManagerImageReleased;
        public static Action<bool> onManagerEditMode;
        public static Action<int> onManagerCheck;
        public static Action<float> onEnemyDamaged;

        public int currentFocusedGun;
        public void InvokeEditMode(bool state)
        {
            onEditMode?.Invoke(state);
        }
        public void InvokeSwapGun(GameObject gun = null)
        {
            onSwapGun?.Invoke(gun);
        }
        public void InvokeManagerMode(bool state)
        {
            onManagerEditMode?.Invoke(state);
        }
    }
}