using Newtonsoft.Json; //Newtonsoft package (JSON.NET)
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;


/// <summary>
/// Coverts GameData to a more compressed format (JSON) (serialize).
/// Writes to the file.
/// Reads file.
/// Coverts compressed format (JSON) back to GameData.cs (deserialize).
/// </summary>

public class FileDataHandler
{
    private string DataDirPath = ""; //Directory path of where we want to save the data on the computer.
    private string DataFileName = ""; //Name of the file that we want to save to.
    private bool UseEncryption = false;
    private const string KEY = "ggdPhkeOoiv6YMiPWa34kIuOdDUL7NwQFg6l1DVdwN8=";
    private const string IV = "JZuM0HQsWSBVpRHTeRZMYQ==";
    //private readonly string encryptionCodeWord = "word";
    private readonly string backupExtension = ".bak";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.DataDirPath = dataDirPath;
        this.DataFileName = dataFileName;
        this.UseEncryption = useEncryption;
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public GameData Load(string profileID, bool allowRestoreFromBackup = true)
    {
        // base case - if the profileId is null, return right away
        if (profileID == null)
        {
            return null;
        }

        //string fullPath = Path.Combine(DataDirPath, DataFileName); //Using combine instead of string concatenation because different operating systems have different file seperators.
        string fullPath = DataDirPath + "/" + profileID + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.
        //string fullPath = Path.Combine(DataDirPath, profileID, DataFileName);

        Debug.Log("From Load() Going to load game from: " + fullPath);

        GameData loadedData = null; //Variable we are going to load into.

        if (File.Exists(fullPath)) //Checking if file we are going to load from exists. If data exists then it will be loaded and if it does not then it will return null.
        {
            Debug.Log($"File does exist.");
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) //Loading the serialized data from the file.
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); //Loading file text into the dataToLoad variable as a string.
                    }
                }

                if (UseEncryption) //Optionally decrypt the data
                {
                    loadedData = ReadEncryptedData(fullPath); //MESSSSSSSSEEDDDDDDDD UPPPPPPPPPPPPPPPP
                    //dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad); //Deserializing the data from JSON back into the C# object.

                Debug.Log($"FileDataHandler Load() data success.");

            }
            catch (Exception e)
            {
                // since we're calling Load(..) recursively, we need to account for the case where
                // the rollback succeeds, but data is still failing to load for some other reason,
                // which without this check may cause an infinite recursion loop.
                if (allowRestoreFromBackup)
                {
                    Debug.LogWarning("Failed to load data file. Attempting to roll back.\n" + e);
                    bool rollbackSuccess = AttemptRollback(fullPath);
                    if (rollbackSuccess)
                    {
                        // try to load again recursively
                        loadedData = Load(profileID, false);
                    }
                }
                // if we hit this else block, one possibility is that the backup file is also corrupt
                else
                {
                    Debug.LogError("Error occured when trying to load file at path: "
                        + fullPath + " and backup did not work.\n" + e);
                }
            }
        }
        return loadedData;
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Save(GameData data, string profileID)
    {
        // base case - if the profileId is null, return right away
        if (profileID == null)
        {
            return;
        }

        string fullPath = DataDirPath + "/" + profileID + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.
        //string fullPath = Path.Combine(DataDirPath, profileID, DataFileName); //Using combine instead of string concatenation because different operating systems have different file seperators.
        //string fullPath = DataDirPath + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.

        string backupFilePath = fullPath + backupExtension;
        Debug.Log("From Save(Game data) Going to save game at: " + fullPath);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); //Creating the directory the file will be written to if it does not already exist.

            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }); //Serializing the C# game data object into JSON string.

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) //Writing the serialized data to the file.
            {
                if (UseEncryption)
                {
                    Encrypt(data, stream);
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            // verify the newly saved file can be loaded successfully
            GameData verifiedGameData = Load(profileID);
            // if the data can be verified, back it up
            if (verifiedGameData != null)
            {
                File.Copy(fullPath, backupFilePath, true);
            }
            // otherwise, something went wrong and we should throw an exception
            else
            {
                throw new Exception("Save file could not be verified and backup could not be created.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
        Debug.Log($"FileDataHandler Save() data success.");
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //private void Encrypt(byte[] data, FileStream stream)
    private void Encrypt(GameData data, FileStream stream)
    {
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();
        using CryptoStream cryptoStream = new CryptoStream(
            stream,
            cryptoTransform,
            CryptoStreamMode.Write
            );

        //Can uncomment the below to see a generated value for the IV & key.
        //Can also generate my own.
        //Debug.Log($"Initialization Vector: {Convert.ToBase64String(aesProvider.IV)}");
        //Debug.Log($"Key: {Convert.ToBase64String(aesProvider.Key)}");

        //cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data))); //Encoding because it accepts bits.
        cryptoStream.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }))); //Serializing the C# game data object into JSON string.))); //Encoding because it accepts bits.
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private GameData ReadEncryptedData(string path)
    {
        byte[] fileBytes = File.ReadAllBytes(path);
        Debug.Log("Filebytes size in load: " + fileBytes.Length);
        using Aes aesProvider = Aes.Create();

        aesProvider.Key = Convert.FromBase64String(KEY);
        aesProvider.IV = Convert.FromBase64String(IV);

        using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
            aesProvider.Key,
            aesProvider.IV
            ); //New cryptoTransform

        using MemoryStream decryptionStream = new MemoryStream(fileBytes); //Memory having all the file data.

        using CryptoStream cryptoStream = new CryptoStream(
            decryptionStream, //Stream we want to read from first.
            cryptoTransform, //Understands how to translate the encrypted data to normal bytes.
            CryptoStreamMode.Read //Reading the data.
            );

        using StreamReader reader = new StreamReader(cryptoStream);

        string decryptedData = reader.ReadToEnd(); //Gives a string with all of data

        //Debug.Log($"Decrypted result (if the following is not legible, probably wrong key or iv): {result}");
        return JsonConvert.DeserializeObject<GameData>(decryptedData);  //here
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Delete(string profileId)
    {
        // base case - if the profileId is null, return right away
        if (profileId == null)
        {
            return;
        }

        //string fullPath = Path.Combine(DataDirPath, profileId, DataFileName);
        string fullPath = DataDirPath + "/" + profileId + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.

        try
        {
            // ensure the data file exists at this path before deleting the directory
            if (File.Exists(fullPath))
            {
                // delete the profile folder and everything within it
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete profile data for profileId: "
                + profileId + " at path: " + fullPath + "\n" + e);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        // loop over all directory names in the data directory path
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(DataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            Debug.Log(dirInfo.Name);

            string profileId = dirInfo.Name;

            // Defensive programming - check if the data file exists.
            // If it doesn't, then this folder isn't a profile and should be skipped.
            //String fullPath = Path.Combine(DataDirPath, profileId, DataFileName);
            string fullPath = DataDirPath + "/" + profileId + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.

            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when loading all profiles because it does not contain data: "
                    + profileId);
                continue;
            }

            // Load the game data for this profile and put it in the dictionary.
            GameData profileData = Load(profileId);
            // Defensive programming - ensure the profile data isn't null, because if it is then something went wrong and we should let ourselves know.
            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong. ProfileId: " + profileId);
            }
        }
        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;

        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            // skip this entry if the gamedata is null
            if (gameData == null)
            {
                continue;
            }

            // if this is the first data we've come across that exists, it's the most recent so far
            if (mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }

            // otherwise, compare to see which date is the most recent
            else
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);
                // the greatest DateTime value is the most recent
                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;

    }

    private bool AttemptRollback(string fullPath)
    {
        bool success = false;
        string backupFilePath = fullPath + backupExtension;
        try
        {
            // if the file exists, attempt to roll back to it by overwriting the original file
            if (File.Exists(backupFilePath))
            {
                File.Copy(backupFilePath, fullPath, true);
                success = true;
                Debug.LogWarning("Had to roll back to backup file at: " + backupFilePath);
            }
            // otherwise, we don't yet have a backup file - so there's nothing to roll back to
            else
            {
                throw new Exception("Tried to roll back, but no backup file exists to roll back to.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to roll back to backup file at: "
                + backupFilePath + "\n" + e);
        }

        return success;
    }
}

//Source 1: https://www.youtube.com/watch?v=mntS45g8OK4&list=WL&index=5 for the Newtonsoft stuff and encryption/decryption.
//Source 2: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.
