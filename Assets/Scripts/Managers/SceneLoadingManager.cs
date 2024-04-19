using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AsyncSceneLoad());
    }
    IEnumerator AsyncSceneLoad()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.23f));

        SceneManager.LoadSceneAsync(2);
    }
}
