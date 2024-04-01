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
using Utils;

public class GunButtonsManager : MonoBehaviour
{
    public int buttonNo;
    public Button swapButton;
    public Image gunLogo, damageStat, speedStat;
    public TMP_Text gunTitleText, gunButtonStateText;
    public List<GunButtonSo> gunButtons = new();
    GunButtonSo tempGunButton;
    BulletSo tempGunBulletSo;
    GunSo tempGunSo;
    public GameObject contentPanel;
    public TowerController mainTower;
    public GunData gunData;
    public GameObject closeEditModeButton;
    public void OnEnable()
    {
        GameController.onCloseCameraPressed += CloseCameraPressed;
        GameController.onSwapGun += StateSetup;
        GameController.onZoomOutFromGun += ZoomOut;
    }

    public void OnDisable()
    {
        GameController.onCloseCameraPressed -= CloseCameraPressed;
        GameController.onSwapGun -= StateSetup;
        GameController.onZoomOutFromGun -= ZoomOut;
    }
    void Start()
    {
        tempGunButton = gunButtons[buttonNo];
        tempGunSo = tempGunButton.gun;
        tempGunBulletSo = tempGunButton.gun.myBullet;

        damageStat.fillAmount = tempGunBulletSo.damage;
        speedStat.fillAmount = tempGunSo.frequency;

        gunLogo.sprite = tempGunButton.gunLogo;
        gunTitleText.text = tempGunSo.gunName;

        gunData = (GunData)DataPersistenceController.Instance.GetData("gun", new GunData(tempGunSo.gunName));
        if (buttonNo == 0 && !gunData.unlockState)
        {
            // Debug.Log("default unlock for machine gun");
            gunData.UnlockState = true;
        }
    }
    public void ZoomOut()
    {
        contentPanel.SetActive(false);
    }
    public void CloseCameraPressed(int id)
    {
        var activeState = GameController.Instance.currentFocusedGun != -1;
        swapButton.onClick.RemoveAllListeners();
        contentPanel.SetActive(activeState);
        closeEditModeButton.SetActive(!activeState);
        StateSetup();

    }
    public void StateSetup(GameObject swapedGun = null)
    {
        swapButton.interactable = true;
        StartCoroutine(StateSetupDelay());
    }
    IEnumerator StateSetupDelay()
    {
        yield return new WaitForSeconds(0.05f);
        if (IsGunUnlocked())
        {
            if (mainTower.FocusedGunSo() && mainTower.FocusedGunSo() == tempGunSo)
            {
                gunButtonStateText.text = "Equiped";
                swapButton.interactable = false;
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
            swapButton.onClick.AddListener(() => Unlock(tempGunSo.myPrefab));
        }
    }
    public void Unlock(GameObject gun)
    {
        gunData.UnlockState = true;
        gunButtonStateText.text = "Equip";
        swapButton.onClick.AddListener(() => GameController.Instance.InvokeSwapGun(gun));
    }
    public bool IsGunUnlocked()
    {
        return gunData.unlockState;
    }
}
