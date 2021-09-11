using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    [Header("Загружаемая сцена")]
    public int sceneID;
    [Header("Остальные объекты")]
    public Image ImageLoad;
    public Text ProgressText;
    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            ImageLoad.fillAmount = progress;
            ProgressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}
