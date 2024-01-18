using System;
using Guns;
using Tower.Floor;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class GameController : Utils.Singleton<GameController>
    {
        public static Action<GameObject, FloorBase> onGunPlaced;
        public static Action onEditMode;
        public static Action<FloorBase> OnDied;

    }
}