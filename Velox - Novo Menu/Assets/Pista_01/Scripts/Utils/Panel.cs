using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CanvasGroup)),ExecuteInEditMode]
public class Panel : MonoBehaviour
{
    [Header("Panel override CanvasGroup values")]
    public bool blocksRaycasts = true;
    [HideInInspector]
    public CanvasGroup canvasGroup;
    public bool ignoreParentGroups;
    public bool interactable = true;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        canvasGroup.blocksRaycasts = (canvasGroup.alpha > 0f) && blocksRaycasts;

        canvasGroup.interactable = (canvasGroup.alpha > 0f) && interactable;

        canvasGroup.ignoreParentGroups = ignoreParentGroups; 

    }
}
