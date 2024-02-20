using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.BulletSo;
using Data_and_Scriptable.GunSo;
using Guns;
using Managers;
using TMPro;
using Tower;
using Tower.Floor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GunButtonsManager : MonoBehaviour
{
    public int buttonNo, focusedGun = -1;
    public Button swapButton;
    public Image gunLogo;
    public TMP_Text gunTitleText, gunStatsText, gunButtonStateText;
    public List<GunButtonSo> gunButtons = new();
    GunButtonSo tempGunButton;
    BulletSo tempGunBulletSo;
    GunSo tempGunSo;
    public GameObject contentPanel;

    public GunData data;

    public void OnEnable()
    {
        GameController.onCloseCameraPressed += CloseCameraPressed;
        GameController.swapGun += GunSwaped;
    }

    public void OnDisable()
    {
        GameController.onCloseCameraPressed -= CloseCameraPressed;
        GameController.swapGun -= GunSwaped;
    }
    void Start()
    {

        tempGunButton = gunButtons[buttonNo];
        tempGunSo = tempGunButton.gun;
        tempGunBulletSo = tempGunButton.gun.myBullet;


        gunLogo.sprite = tempGunButton.gunLogo;
        gunTitleText.text = tempGunSo.gunName;

        data = (GunData)DataPersistenceController.Instance.GetData("gun", new GunData(tempGunSo.gunName));

        swapButton.onClick.RemoveAllListeners();
        //swapButton.onClick.AddListener(() => GameController.Instance.InvokeSwapGun(tempGunSo.myPrefab));

        GunSwaped(tempGunSo.myPrefab);
    }

    public void GunSwaped(GameObject gun)
    {
        //if (TowerController.Instance.floors.Count < buttonNo + 1) return;

        if (data.unlockState)
        {
            // Debug.Log(gun.name);
            // Debug.Log(tempGunSo.myPrefab.name);
            if (gun == tempGunSo.myPrefab && focusedGun == buttonNo)
            {
                gunButtonStateText.text = "Equiped";
                swapButton.onClick.RemoveAllListeners();
            }
            else
            {
                gunButtonStateText.text = "Equip";
                swapButton.onClick.AddListener(() => GameController.Instance.InvokeSwapGun(tempGunSo.myPrefab));
            }
        }
        else
        {
            gunButtonStateText.text = "Unlock";
            swapButton.onClick.AddListener(() => Unlock(gun));
        }
    }

    public void Unlock(GameObject gun)
    {
        data.UnlockState = true;
        GunSwaped(gun);
    }

    public void CloseCameraPressed(int id)
    {
        // Debug.Log(id + "  " + buttonNo);
        swapButton.onClick.RemoveAllListeners();

        contentPanel.SetActive(true);
        focusedGun = id;


    }
}
