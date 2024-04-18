using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataService 
{
    //Generic T accept any arbitrary data type, will accept a string of relative path, T data is the data that will be saved, and encryption.
    bool SaveDate<T>(string RelativePath, T Data, bool Encrypted);

    T LoadData<T>(string RelativePath, bool Encrypted);
}

//Source: https://www.youtube.com/watch?v=mntS45g8OK4&list=WL&index=5
