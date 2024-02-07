using System.Collections;
using System.Collections.Generic;
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
    float laserFrequency = 0.3f;
    IDamageable damageable;
    protected override void Shoot()
    {
        targetObj = myFloor.attackTo.GetComponent<FloorBase>().gunPosition;
        if (targetObj.GetChild(0).TryGetComponent(out IDamageable damageable))
        {
            this.damageable = damageable;
        }
        StartCoroutine(LaserDelay());
    }

    IEnumerator LaserDelay()
    {
        if (tempBullet && tempBullet.activeInHierarchy)
        {
            DespawnTempBullet();
            yield return new WaitUntil(() => !tempBullet.activeInHierarchy);
        }

        tempBullet = myGun.myBullet.prefab.Spawn(spawnPosition[0].position, Quaternion.identity);
        tempBullet.transform.GetChild(0).transform.position = tempBullet.transform.GetChild(1).position = tempBullet.transform.position;
        LaserShoot(tempBullet.transform.GetChild(0), tempBullet.transform.GetChild(1), tempBullet.GetComponent<LineRenderer>());
    }

    public void LaserShoot(Transform firstPoint, Transform endPoint, LineRenderer lineRenderer)
    {
        firstPoint.position = spawnPosition[0].position;
        // endPoint.position = myFloor.attackTo.GetComponent<FloorBase>().gunPosition.position + Vector3.up * 0.5f;
        lineRenderer.enabled = true;
        endPoint.DOMove(targetObj.position + Vector3.up * 0.5f, 0.2f).OnUpdate(() =>
        {
            lineRenderer.SetPositions(new Vector3[] { firstPoint.position, endPoint.position });
        }).OnComplete(() => DamageState(true));
    }

    Transform targetObj;
    public void DamageState(bool isOn, bool enemyDied = false)
    {
        damage = isOn;
        if (enemyDied)
        {
            DespawnTempBullet();
        }
    }

    public void DespawnTempBullet()
    {
        var lineRenderer = tempBullet.GetComponent<LineRenderer>();
        tempBullet.transform.GetChild(0).DOMove(tempBullet.transform.GetChild(1).position, 0.2f).OnUpdate(() =>
                {
                    lineRenderer.SetPositions(new Vector3[] { tempBullet.transform.GetChild(0).position, tempBullet.transform.GetChild(1).position });
                }).OnComplete(() => tempBullet.Despawn());
    }

    protected override void Update()
    {
        if (!damage) return;

        if (laserFrequency > 0)
        {
            laserFrequency -= Time.deltaTime;
        }
        else
        {
            if (damageable == null) return;
            damageable.Damage(myGun.myBullet.damage);
            laserFrequency = 0.3f;
        }

    }
}

