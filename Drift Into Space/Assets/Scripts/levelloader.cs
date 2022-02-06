using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelloader : MonoBehaviour
{
    int sceneIndex;
   
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        sceneIndex = PlayerPrefs.GetInt("level", 0);
       int i = PlayerPrefs.GetInt("loading");
        if(i == 0) { StartCoroutine(ExecuteAfterTime(40)); }
        else
        {
            loadlevel(sceneIndex);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time);
    }
    public void loadlevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
       // Loadingscreen.SetActive(true);
        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(35);
        PlayerPrefs.SetInt("loading", 1);
        PlayerPrefs.Save();
        loadlevel(sceneIndex);
        
    }
}
