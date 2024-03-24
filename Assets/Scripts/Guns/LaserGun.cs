using System.Collections;
using System.Collections.Generic;
using System.Net;
using Bullets;
using DG.Tweening;
using Guns;
using Tower.Floor;
using Unity.VisualScripting;
using UnityEngine;
using Utils;
using Utils.PoolSystem;

public class LaserGun : GunBase
{
    GameObject tempBullet;
    bool damage;
    float laserFrequency;
    IDamageable damageable;
    LineRenderer line;
    private void Start()
    {
        laserFrequency = myGun.frequency;
    }
    protected override void Shoot()
    {
        if (!myFloor.attackTo || !myFloor.attackTo.gameObject.activeInHierarchy) return;

        targetObj = myFloor.attackTo.GetComponent<FloorBase>().gunPosition;
        if (targetObj.GetChild(0).TryGetComponent(out IDamageable damageable))
        {
            this.damageable = damageable;
        }
        if (gameObject.activeInHierarchy)
            StartCoroutine(LaserDelay());
        else
            DamageState(false, true);
    }

    public virtual IEnumerator LaserDelay()
    {
        if (tempBullet && tempBullet.activeInHierarchy)
        {
            DespawnTempBullet();
            if (line)
                yield return new WaitUntil(() => !line.enabled);
        }

        tempBullet = myGun.myBullet.prefab.Spawn(spawnPosition[0].position, Quaternion.identity);
        line = tempBullet.GetComponent<LineRenderer>();
        tempBullet.transform.GetChild(0).transform.position = tempBullet.transform.GetChild(1).position = tempBullet.transform.position;
        LaserShoot(tempBullet.transform.GetChild(0), tempBullet.transform.GetChild(1));
    }

    public virtual void LaserShoot(Transform firstPoint, Transform endPoint)
    {
        firstPoint.position = spawnPosition[0].position;
        // endPoint.position = myFloor.attackTo.GetComponent<FloorBase>().gunPosition.position + Vector3.up * 0.5f;
        line.enabled = true;
        endPoint.DOMove(targetObj.position + Vector3.up * 0.5f, 0.2f).OnUpdate(() =>
        {
            tempBullet.transform.GetChild(0).position = spawnPosition[0].position;
            line.SetPositions(new Vector3[] { firstPoint.position, endPoint.position });
        }).OnComplete(() => DamageState(true));
    }

    Transform targetObj;
    public virtual void DamageState(bool isOn, bool enemyDied = false)
    {
        damage = isOn;
        if (enemyDied)
        {
            DespawnTempBullet();
        }
    }

    public virtual void DespawnTempBullet()
    {
        if (!tempBullet) return;
        tempBullet.transform.GetChild(0).DOMove(tempBullet.transform.GetChild(1).position, 0.2f).OnUpdate(() =>
                {
                    if (tempBullet)
                    {
                        tempBullet.transform.GetChild(0).position = spawnPosition[0].position;
                        line.SetPositions(new Vector3[] { tempBullet.transform.GetChild(0).position, tempBullet.transform.GetChild(1).position });
                    }
                }).OnComplete(() =>
                {
                    damage = false;

                    line.enabled = false;
                    tempBullet?.Despawn();
                    tempBullet = null;
                });
    }

    protected override void Update()
    {
        if (!damage) return;
        tempBullet.transform.GetChild(0).position = spawnPosition[0].position;
        tempBullet.transform.GetChild(1).position = targetObj.position + Vector3.up * 0.5f;
        line.SetPositions(new Vector3[] { spawnPosition[0].position, targetObj.position + Vector3.up * 0.5f });
        if (laserFrequency > 0)
        {
            laserFrequency -= Time.deltaTime;
        }
        else
        {
            if (damageable == null) return;
            damageable.Damage(myGun.myBullet.damage);
            laserFrequency = myGun.frequency;
        }
    }
}

