using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    public void  GoToScene(string name)
    {
        SceneController.NextScene(name);
    }

    public void GoToScene()
    {
        SceneController.NextScene();
    }
}
