using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public void gotoScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
