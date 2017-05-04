using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RadialProgressBar : MonoBehaviour
{
    public Transform progress;
    public float speed;

    float currentAmount = 0;
    float scale = 0.7f;

    // Update is called once per frame
    private void Start()
    {
        currentAmount = 0;
    }

    void Update()
    {
        if (currentAmount < 100)
        {
            currentAmount += speed * Time.deltaTime;
        }
            

        progress.GetComponent<Image>().fillAmount = currentAmount / 100;
        Debug.Log("o currentAmount....." + currentAmount.ToString());

        if (currentAmount > 100)
        {
            Debug.Log("o currenetAmout é maior que 100...");
            SceneController.NextScene("Main/Maps");
        }
    }
}
