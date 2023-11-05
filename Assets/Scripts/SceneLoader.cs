using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider loadingBar;
    
    

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));

    }

    IEnumerator LoadSceneAsync (int index)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
       
        loadingScreen.SetActive(true);
        while( !operation.isDone)
        {
            loadingBar.value = operation.progress ;
            yield return null;
        }
       
    }

   
}
