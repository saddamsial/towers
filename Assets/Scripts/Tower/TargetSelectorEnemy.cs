using System.Collections.Generic;
using System.Linq;
using Managers;
using Tower;
using Tower.Floor;
using UnityEngine;

public class TargetSelectorEnemy : MonoBehaviour
{
    public TowerController mainTower;
    public List<FloorMine>
    floorsMain = new(),
    floorsHealth2Low = new(),
    floorsPower2Least = new(),
    floorsNotTargeted = new(),
    floorsTargeted = new(),
    floorsFreezed = new(),
    floorsNoShield = new(),
    floorsAttackingToMe = new();
    public FloorMine tempTarget;
    public void OnEnable()
    {
        GameController.onDied += Die;
    }
    public void OnDisable()
    {
        GameController.onDied -= Die;
    }
    public void Die(FloorBase diedObj)
    {
        FillLists();
    }
    public void FillLists()
    {
        floorsHealth2Low = new List<FloorMine>(floorsMain./*Where(x => x.attachedGun != null).*/OrderBy(x => x.myHealth.Current));
        floorsPower2Least = new List<FloorMine>(floorsMain.Where(x => x.attachedGun != null).OrderBy(x => x.attachedGun.myGun.myBullet.damage));
        FillNotTargeted();
        FillFreezed();
        FillNoShield();
    }
    public FloorMine GetAttackingToMe()
    {
        var f = floorsMain.Where(x => x.attackTo == transform);
        return (FloorMine)f;
    }
    public void FillFreezed()
    {
        floorsFreezed = new List<FloorMine>(floorsMain.Where(x => x.isFreezed)).ToList();
    }
    public void FillNoShield()
    {
        floorsNoShield = new List<FloorMine>(floorsMain.Where(x => !x.hasShield)).ToList();
    }
    public void FillNotTargeted()
    {
        floorsNotTargeted.Clear();
        floorsTargeted.Clear();
        floorsNotTargeted = new List<FloorMine>(floorsMain.Where(x => !x.isTargeted)).ToList();
        floorsTargeted = new List<FloorMine>(floorsMain.Where(x => x.isTargeted)).ToList();
    }
    public void FirstFill()
    {
        floorsMain.Clear();
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        FillLists();
    }
    public FloorMine SelectTarget(int difficulty)
    {
        if (floorsMain.Count == 0) return null;
        // FillLists();
        switch (difficulty)
        {
            case 0:
                break;
        }
        tempTarget = floorsHealth2Low[Random.Range(0, floorsHealth2Low.Count)];
        tempTarget.isTargeted = true;
        FillNotTargeted();
        return tempTarget;
    }
}
