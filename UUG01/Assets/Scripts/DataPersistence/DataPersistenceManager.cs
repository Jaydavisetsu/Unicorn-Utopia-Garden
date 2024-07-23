using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Gives nicer syntax for finding the data of resistance objects.

/// <summary>
/// This singleton class keeps track of the current state of GameData and is responsible for orchestrating all the logic that goes into saving and loading the game.
/// The new, load, and save methods are called depending on how I want to laod and save the game. 
/// The methods are public so that they can be called from other scripts.
/// This class is set up as a singleton instance so that there is only one in the scene.
/// </summary>

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string Filename; //Filename we want to save the data to.
    [SerializeField] private bool UseEncryption;

    private GameData GameData; //Class that stoes the state of the game.
    private List<IDataPersistence> DataPersistenceObjects; //When this manager starts, it will gather and store a reference to each IDataPersistence script in a list so that we can call scripts when we save and load the game.
    private FileDataHandler DataHandler;

    public static DataPersistenceManager Instance { get; private set; } //Able to get instance publicly but only modifying privatley in this class.

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        Instance = this;
    }

    //For now (until multiple saves) the game is loaded on startup.
    private void Start()
    {
        this.DataHandler = new FileDataHandler(Application.persistentDataPath, Filename, UseEncryption); //Applicaton.persistentDataPath give the operating system standard directory for persisting data in a unity project. You can change where the data will be saved on machine but this is a good choice.
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        Debug.Log("1. Going to load game at Application.persistenDataPath: " + Application.persistentDataPath + " --Filename: " + Filename);
        Debug.Log("1. Going to load game at Application.persistenDataPath: " + Application.persistentDataPath + "/" + Filename);
        LoadGame();
    }

    public void NewGame()
    {
        GameData = new GameData();
    }

    public void LoadGame()
    {
        
        // load any saved data from a file using the data handler
        this.GameData = DataHandler.Load();

        // if no data can be loaded, initialize to a new game
        if (this.GameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
        /*
        if (GameData != null)
        {
            Debug.Log("Data found.");

            // load any saved data from a file using the data handler
            GameData = DataHandler.Load(); //When save data does not exist, this will result in null.

            Debug.Log("Loaded data.");
        }
        else if(GameData == null) //If no data can be loaded, initialize to a new game
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
            Debug.Log("Created new game.");
        }
        // push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
        */
        /*
        if (this.GameData == null) //If no data can be loaded, then initialize to a new game.
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }
        else
        {
            //Loading any saved data from a file using the data handler.
            this.GameData = DataHandler.Load(); //When save data does not exist, this will result in null.
            Debug.Log("Data found. Loading data.");
        }
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects) //Pushing/passing the loaded data to all other scripts that need it.
        {
            dataPersistenceObj.LoadData(GameData);
        }
        */

        Debug.Log("Loaded positions" + GameData.PlayerPosition.ToString()); //CAN REMOVE
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects) //Passing the data to other scripts so they can update it.
        {
            dataPersistenceObj.SaveData(GameData);
        }

        Debug.Log("Saved positions" + GameData.PlayerPosition.ToString()); //CAN REMOVE

        DataHandler.Save(GameData); //Saving that data to a file using the data handler.
    }

    //TODO: Maybe one to delete a game data if the player chooses too?

    /*
    private void OnApplicationQuit() //Gets called any time the game exits ... set this up to be when the save button is pressed?... this method might be unnecessary later.
    {
        SaveGame();
    }
    */

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> DataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>(); //System.Linq helps find all scripts that implement the IDataPersistence interface and are seen. Scripts need to extend in monobehavior to be found.

        return new List<IDataPersistence>(DataPersistenceObjects); //Returning a new list and passing in the result of that call to initialize the list.
    }
}

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.
