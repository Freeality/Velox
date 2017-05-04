using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Creating Screen Transitions
//https://docs.unity3d.com/Manual/HOWTO-UIScreenTransition.html

public class SceneController: MonoBehaviour
{
    static int current = 0;

    public static void NextScene(string targetName)
    {
        SceneManager.LoadScene("Scenes/" + targetName);
    }

    public static void NextScene()
    {
        if(current < SceneManager.sceneCountInBuildSettings)
        {
            current += 1;

            //Scene scene = SceneManager.GetSceneAt(current);
            //Debug.Log(scene.name);

            SceneManager.LoadScene(current);
        }
    }
}