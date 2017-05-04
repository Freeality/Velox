using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5F;

    float accumulated = 0; // FPS accumulated over the interval
    float timeleft; // Left time for current interval
    int frames = 0; // Frames drawn over the interval

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        if (!GetComponent<Text>())
        {
            Debug.Log("UtilityFramesPerSecond needs a Text component!");
            enabled = false;
            return;
        }

        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accumulated += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accumulated / frames;
            string format = string.Format("{0:F2} FPS", fps);

            GetComponent<Text>().text = format;

            if (fps < 30)
                SetColor(Color.yellow);
            else
                if (fps < 10)
                SetColor(Color.red);
            else
                SetColor(Color.green);

            //  DebugConsole.Log(format,level);
            timeleft = updateInterval;
            accumulated = 0.0F;
            frames = 0;
        }
    }

    void SetColor(Color color)
    {
        GetComponent<Text>().material.color = color;
    }
}