using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapSelect : MonoBehaviour
{
    public List<Item> itemList;
    List<SampleButton> buttons;

    public GameObject sampleButton;
    public Transform contentPanel;

    public Sprite selected;
    public Image background;
    public GameObject loadingPanel;
    public Slider slider;
    public Text loadingText;

    Color alpha = new Color(1, 1, 1, 0);

    int currentItem;
    float currentAmount;

    AsyncOperation async = null;

    // Use this for initialization
    void Start()
    {
        loadingPanel.SetActive(false);
        buttons = new List<SampleButton>();
        PopulateList();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Previous();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Next();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            //loadingPanel.SetActive(true);
            SceneController.NextScene("Rails/Rails1");
        }
    }

    void PopulateList()
    {
        foreach (Item item in itemList)
        {
            GameObject newButton = Instantiate(sampleButton);
            SampleButton component = newButton.GetComponent<SampleButton>();
            component.name = item.name;
            component.sceneMap.sprite = item.sceneMap;
            component.button.onClick = item.nextScene;
            newButton.transform.SetParent(contentPanel);
            newButton.transform.localScale = Vector3.one;

            itemList[0].isSelected = true;
            if (item.isSelected)
            {
                Debug.Log(itemList[currentItem].name);
                component.selectOrNot.color = Color.white;
                component.selectOrNot.sprite = selected;
                background.sprite = component.sceneMap.sprite;
            }
            else
                component.selectOrNot.color = alpha;

            buttons.Add(component);
        }
    }

    void Next()
    {
        if (currentItem == itemList.Count - 1)
            return;
        else
        {
            currentItem += 1;
            itemList[currentItem].isSelected = true;
            if (itemList[currentItem].isSelected)
            {
                Debug.Log(itemList[currentItem].name);
                buttons[currentItem - 1].selectOrNot.color = alpha;
                buttons[currentItem].selectOrNot.color = Color.white;
                buttons[currentItem].selectOrNot.sprite = selected;
                background.sprite = buttons[currentItem].sceneMap.sprite;
            }
            else
                buttons[currentItem].selectOrNot.color = alpha;
        }

        if (currentItem >= 4)
            contentPanel.GetComponent<HorizontalLayoutGroup>().padding.left -= 400;
    }

    void Previous()
    {
        if (currentItem == 0)
            return;
        else
        {
            currentItem -= 1;
            itemList[currentItem].isSelected = true;
            if (itemList[currentItem].isSelected)
            {
                Debug.Log(itemList[currentItem].name);
                buttons[currentItem + 1].selectOrNot.color = alpha;
                buttons[currentItem].selectOrNot.color = Color.white;
                buttons[currentItem].selectOrNot.sprite = selected;
                background.sprite = buttons[currentItem].sceneMap.sprite;
            }
        }
        if (currentItem <= 0)
        {
            if (currentItem >= 4)
                contentPanel.GetComponent<HorizontalLayoutGroup>().padding.left += 400;
        }
    }

    public void NextItem()
    {
        Next();
    }

    public void PreviousItem()
    {
        Previous();
    }

    void SyncLoadLevel(string levelName)
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelName);
        Debug.Log("progresso " + slider.value + "%");
        Load();
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(1);
        async.allowSceneActivation = false;

        if (!async.isDone)
        {
            slider.value = async.progress;

            if (async.progress >= 0.9f)
            {
                loadingText.text = "Precione F pra continuar";
                if (Input.GetKeyDown(KeyCode.F))
                    async.allowSceneActivation = true;
            }
        }
    }
}