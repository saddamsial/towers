using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.Services.RemoteConfig;
using UnityEngine.UI;
public class SignManager : MonoBehaviour
{
    public GamePresets gamePresets;
    public struct userAttributes { }
    public struct appAttributes { }
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


                    RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;
                    RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());

                    StartCoroutine(AsyncSceneLoad());
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

    void ApplyRemoteConfig(ConfigResponse configResponse)
    {

        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session and no local cache file exists; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
                break;
        }

        Debug.Log(RemoteConfigService.Instance.appConfig.GetInt("max_possible_floors", 5));
    }

    IEnumerator AsyncSceneLoad()
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
