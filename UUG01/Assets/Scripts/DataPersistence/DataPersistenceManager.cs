using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Gives nicer syntax for finding the data of resistance objects.
using UnityEngine.SceneManagement;

/// <summary>
/// This singleton class keeps track of the current state of GameData and is responsible for orchestrating all the logic that goes into saving and loading the game.
/// The new, load, and save methods are called depending on how I want to laod and save the game. 
/// The methods are public so that they can be called from other scripts.
/// This class is set up as a singleton instance so that there is only one in the scene.
/// </summary>

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId = "test";

    [Header("File Storage Config")]
    [SerializeField] private string Filename; //Filename we want to save the data to.
    [SerializeField] private bool UseEncryption;

    [Header("Auto Saving Configuration")]
    [SerializeField] private float autoSaveTimeSeconds = 10f;

    private GameData GameData; //Class that stoes the state of the game.
    private List<IDataPersistence> DataPersistenceObjects; //When this manager starts, it will gather and store a reference to each IDataPersistence script in a list so that we can call scripts when we save and load the game.
    private FileDataHandler DataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;

    public static DataPersistenceManager Instance { get; private set; } //Able to get instance publicly but only modifying privatley in this class.

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        if (disableDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }

        this.DataHandler = new FileDataHandler(Application.persistentDataPath, Filename, UseEncryption); //Applicaton.persistentDataPath give the operating system standard directory for persisting data in a unity project. You can change where the data will be saved on machine but this is a good choice.

        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        //Debug.Log("1. Going to load game at Application.persistenDataPath: " + Application.persistentDataPath + " --Filename: " + Filename);
        //Debug.Log("1. Going to load game at Application.persistenDataPath: " + Application.persistentDataPath + "/" + Filename);
        LoadGame();

        // start up the auto saving coroutine
        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        // Update the profile to use for saving and loading.
        this.selectedProfileId = newProfileId;
        // Load the game, which will use that profile, updating our game data accordingly.
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        // delete the data for this profile id
        DataHandler.Delete(profileId);
        // initialize the selected profile id
        InitializeSelectedProfileId();
        // reload the game so that our data matches the newly selected profile id
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = DataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileId);
        }
    }

    public void NewGame()
    {
        this.GameData = new GameData();
    }

    public void LoadGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        // Load any saved data from a file using the data handler
        this.GameData = DataHandler.Load(selectedProfileId);

        // Start a new game if the data is null and we're configured to initialize data for debugging purposes.
        if (this.GameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        // If no data can be loaded, don't continue.
        if (this.GameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // Push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(GameData);
        }
        //Debug.Log("Loaded positions" + GameData.PlayerPosition.ToString()); //CAN REMOVE
    }

    public void SaveGame()
    {
        // return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }

        if (this.GameData == null) // If we don't have any data to save, log a warning here.
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in DataPersistenceObjects) //Passing the data to other scripts so they can update it.
        {
            dataPersistenceObj.SaveData(GameData);
        }

        // timestamp the data so we know when it was last saved
        GameData.lastUpdated = System.DateTime.Now.ToBinary();

        DataHandler.Save(GameData, selectedProfileId); //Saving that data to a file using the data handler.
    }

    private void OnApplicationQuit() //Gets called any time the game exits ... set this up to be when the save button is pressed?... this method might be unnecessary later. -- No need for save button anymore?
    {
        SaveGame();
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        // FindObjectsofType takes in an optional boolean to include inactive gameobjects
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();//System.Linq helps find all scripts that implement the IDataPersistence interface and are seen. Scripts need to extend in monobehavior to be found.

        return new List<IDataPersistence>(dataPersistenceObjects); //Returning a new list and passing in the result of that call to initialize the list.
    }

    public bool HasGameData()
    {
        return GameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return DataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }

}

//Source: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.
