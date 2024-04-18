using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JsonDataService : IDataService
{
    public bool SaveDate<T>(string RelativePath, T Data, bool Encrypted)
    {
        //Getting the path that we want to write to.
        string path = Application.persistentDataPath + RelativePath;

        try
        {
            if (File.Exists(path)) //Checking if file exists, then it will delete and write a new file. 
            {
                Debug.Log("Data exists. Deleting old file and writing new one.");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Creating file for the first time.");
            }
            using FileStream stream = File.Create(path);
            stream.Close();

            File.WriteAllText(path, JsonConvert.SerializeObject(Data)); //Creating new file.
            return true;
        }
        catch (Exception e) //If anything wrong happens during the process above.
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string RelativePath, bool Encrypted)
    {
        throw new System.NotImplementedException();
    }
}

//Source: https://www.youtube.com/watch?v=mntS45g8OK4&list=WL&index=5

