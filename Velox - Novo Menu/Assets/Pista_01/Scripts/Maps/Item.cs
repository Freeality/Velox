using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite sceneMap;
    public int itemID = 0;
    public bool isSelected = false;
    public Button.ButtonClickedEvent nextScene;
}