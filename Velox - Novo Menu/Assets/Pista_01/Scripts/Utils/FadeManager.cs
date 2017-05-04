using UnityEngine;
using UnityEngine.UI;

public class FadeManager: MonoBehaviour
{
    public static FadeManager Instance { get; set; }

    public Image fadeImage;
    bool isShowing;
    bool isInTransition;
    float transition;
    float duration;

    void Awake()
    {
        Instance = this;
    }

    //Use this for initialization
    void Start()
    {

        if (isShowing)
        {
            Fade(true, 1.5f);
            Fade(false, 3f);
        }
        if (!isShowing)
            Fade(true, 1.5f);
    }
    

    // Update is called once per frame
    void Update()
    {
      

        if (!isInTransition)
            return;

        transition += (isShowing) ? Time.deltaTime * (1/duration) : -Time.deltaTime * (1/duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);

        if (transition > 1 || transition < 0)
            isInTransition = false;
    }

    public void Fade(bool showing,float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }
}

