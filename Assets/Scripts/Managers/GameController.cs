using System;
using Data_and_Scriptable.GunSo;
using Guns;
using Tower;
using Tower.Floor;
using UnityEditorInternal;
using UnityEngine;

namespace Managers
{
    public class GameController : Utils.Singleton<GameController>
    {
        public static Action<GameObject, FloorBase> onGunPlaced;
        public static Action<bool> onEditMode;
        public static Action<FloorBase> OnDied;
        public static Action<Transform, int, TowerController, GunSo, FloorMine> onFloorAdded;

        public void InvokeEditMode(bool state)
        {
            onEditMode?.Invoke(state);
        }

    }
}