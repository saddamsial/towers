using System.Collections;
using System.Collections.Generic;
using Data_and_Scriptable.BulletSo;
using Data_and_Scriptable.GunSo;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunButtonsManager : MonoBehaviour
{
    public int buttonNo;
    public Button swapButton;
    public Image gunLogo;
    public TMP_Text gunTitleText, gunStatsText, gunButtonStateText;
    public List<GunButtonSo> gunButtons = new();
    GunButtonSo tempGunButton;
    BulletSo tempGunBulletSo;
    GunSo tempGunSo;
    void Start()
    {
        tempGunButton = gunButtons[buttonNo];
        tempGunSo = tempGunButton.gun;
        tempGunBulletSo = tempGunButton.gun.myBullet;


        gunLogo.sprite = tempGunButton.gunLogo;
        gunTitleText.text = tempGunSo.gunName;
        swapButton.onClick.RemoveAllListeners();
        swapButton.onClick.AddListener(() => GameController.Instance.InvokeSwapGun(tempGunSo.myPrefab));
    }
}
