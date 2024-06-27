using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuItem : MonoBehaviour
{
    [HideInInspector] public Image img;
    [HideInInspector] public RectTransform rectTrans;

    //OptionsMenu reference
    OptionsMenu optionsMenu;

    //item button
    Button button;

    //index of the item in the hierarchy
    int index;

    void Awake()
    {
        img = GetComponent<Image>();
        rectTrans = GetComponent<RectTransform>();

        optionsMenu = rectTrans.parent.GetComponent<OptionsMenu>();

        //-1 to ignore the main button
        index = rectTrans.GetSiblingIndex() - 1;

        //add click listener
        button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClick);
    }

    void OnItemClick()
    {
        optionsMenu.OnItemClick(index);
    }

    void OnDestroy()
    {
        //remove click listener to avoid memory leaks
        button.onClick.RemoveListener(OnItemClick);
    }
}

//Source: https://www.youtube.com/watch?v=Rko64-LQd9s