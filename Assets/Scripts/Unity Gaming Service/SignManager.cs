using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SignManager : MonoBehaviour
{
    public TMP_Text id;
    public Image fillImage;
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            if (this == null)
                return;

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    id.text = "" + AuthenticationService.Instance.PlayerId;
                    StartCoroutine(LoadYourAsyncScene());
                }
                catch (AuthenticationException ex)
                {
                    id.text = ex.Message;
                    Debug.Log(ex);
                }
                catch (RequestFailedException ex)
                {
                    id.text = ex.Message;
                    Debug.Log(ex);
                }
                if (this == null)
                    return;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.23f));
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
            fillImage.fillAmount = progress;
            id.text = progress * 100 + "%";
            yield return null;
        }
    }
}
