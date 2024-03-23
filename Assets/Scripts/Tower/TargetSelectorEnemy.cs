using System.Collections.Generic;
using System.Linq;
using Tower;
using Tower.Floor;
using UnityEngine;

public class TargetSelectorEnemy : MonoBehaviour
{
    public TowerController mainTower;
    int randomListElemt;
    public List<FloorMine>
    floorsMain = new(),
    floorsHealth2Low = new(),//
    floorsPower2Least = new(),//
    floorsNotTargeted = new(),
    floorsTargeted = new(),
    floorsFreezed = new(),
    floorsNoShield = new(),//
    floorsAttackingToMe = new();
    public FloorMine tempTarget;
    public void FillLists()
    {
        FillHealt2Low();
        floorsPower2Least = new List<FloorMine>(floorsMain.Where(x => x.attachedGun != null).OrderBy(x => x.attachedGun.myGun.myBullet.damage));
        FillNotTargeted();
        FillFreezed();
        FillNoShield();
    }
    public FloorMine GetAttackingToMe(Transform enemyFloor)
    {
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        var f = floorsMain.Where(x => x.attackTo == enemyFloor).ToList();
        if (f.Count > 0)
            return f[0];
        else
        {
            return GetRandomFromList(floorsHealth2Low, 0, enemyFloor);
        }
    }
    public void FillHealt2Low()
    {
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        floorsHealth2Low = new List<FloorMine>(floorsMain./*Where(x => x.attachedGun != null).*/OrderBy(x => x.myHealth.Current));
    }
    public void FillFreezed()
    {
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        floorsFreezed = new List<FloorMine>(floorsMain.Where(x => x.isFreezed)).ToList();
    }
    public void FillNoShield()
    {
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        floorsNoShield = new List<FloorMine>(floorsMain.Where(x => !x.hasShield)).ToList();
    }
    public void FillNotTargeted()
    {
        // floorsNotTargeted.Clear();
        // floorsTargeted.Clear();
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        floorsNotTargeted = new List<FloorMine>(floorsMain.Where(x => !x.isTargeted && x.gameObject.activeInHierarchy)).ToList();
        floorsTargeted = new List<FloorMine>(floorsMain.Where(x => x.isTargeted && x.gameObject.activeInHierarchy)).ToList();
    }
    public void FirstFill()
    {
        floorsMain = new List<FloorMine>(mainTower.floorMineList);
        FillLists();
    }
    public FloorMine SelectTarget(int difficulty, Transform me)
    {
        if (floorsMain.Count == 0) return null;
        // FillLists();
        switch (difficulty)
        {
            case 0:
                tempTarget = GetRandomFromList(floorsNotTargeted, difficulty, me);
                break;
            case 1:
                tempTarget = GetRandomFromList(floorsHealth2Low, difficulty, me);
                break;
            case 2:
                tempTarget = GetRandomFromList(floorsTargeted, difficulty, me);
                break;
            case 3:
                tempTarget = GetRandomFromList(floorsPower2Least, difficulty, me);
                break;
            case 4:
                tempTarget = GetRandomFromList(floorsHealth2Low, difficulty, me);
                break;
            case 5:
                tempTarget = GetAttackingToMe(me);
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                tempTarget = FreezeCheck(difficulty, me);
                break;
        }

        tempTarget.isTargeted = true;
        FillNotTargeted();
        // FirstFill();
        return tempTarget;
    }
    public FloorMine GetRandomFromList(List<FloorMine> floors, int tempDifficulty, Transform tempMe)
    {
        if (floors.Count <= 0)
        {
            tempDifficulty++;
            if (tempDifficulty > 9)
                tempDifficulty = 0;
            return SelectTarget(tempDifficulty, tempMe);
        }
        else
        {
            randomListElemt = Random.Range(0, floors.Count);
            return floors[randomListElemt];
        }
    }
    public FloorMine FreezeCheck(int diff, Transform tempMe)
    {
        FillFreezed();
        if (floorsFreezed.Count <= 0)
        {
            return diff switch
            {
                6 or 7 => GetRandomFromList(floorsPower2Least, diff, tempMe),
                8 or 9 => GetRandomFromList(floorsHealth2Low, diff, tempMe),
                _ => floorsMain[0],
            };
        }
        else
            return floorsFreezed[0];
    }
}
