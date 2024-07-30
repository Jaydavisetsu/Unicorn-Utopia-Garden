using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // singletone pattern
    public static GameManager current;

    public ItemManager itemManager;
    public TileManager tileManager;
    public UI_Manager uiManager;

    public Player player;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            //initialize fields
            current = this;
        }

        // Reparenting the GameObject AudioManager because DontDestroyOnLoad only works on root GameObjects and not child game objects.
        //current.transform.SetParent(null); // Making it a root GameObject.
        DontDestroyOnLoad(this.gameObject);
        
        itemManager = GetComponent<ItemManager>();
        tileManager = GetComponent<TileManager>();
        uiManager = GetComponent<UI_Manager>();

        if (uiManager == null)
        {
            Debug.Log(uiManager + "not found!!!!!!!!!");
        }

        player = FindObjectOfType<Player>();
    }
}
// Source: https://www.youtube.com/watch?v=Bdaum2wMM20&list=PL4PNgDjMajPN51E5WzEi7cXzJ16BCHZXl&index=10