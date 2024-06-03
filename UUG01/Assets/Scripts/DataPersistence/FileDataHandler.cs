using Newtonsoft.Json; //Newtonsoft package (JSON.NET)
using System;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;


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

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        this.DataDirPath = dataDirPath;
        this.DataFileName = dataFileName;
        this.UseEncryption = useEncryption;
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Save(GameData data)
    {
        string fullPath = Path.Combine(DataDirPath, DataFileName); //Using combine instead of string concatenation because different operating systems have different file seperators.
        //string fullPath = DataDirPath + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.
        Debug.Log("From Save(Game data) Going to save game at: " + fullPath);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)); //Creating the directory the file will be written to if it does not already exist.

            string dataToStore = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }); //Serializing the C# game data object into JSON string.

            /*using (FileStream stream = File.Create(fullPath))
            {
                if (UseEncryption)
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(dataToStore);
                    Encrypt(dataBytes, stream);  //Optionally encrypt the data
                }
            }*/

            /*if (UseEncryption) //Optionally encrypt the data
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }*/

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
        /*try
        {
            // Define the desired key size in bits (e.g., 256 bits for AES-256)
            int keySizeInBits = 256;

            // Generate a random key of the specified size
            byte[] keyBytes = new byte[keySizeInBits / 8];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            // Convert the key to a Base64-encoded string for storage or transmission
            KEY = Convert.ToBase64String(keyBytes);

            // Generate a random IV for AES encryption
            byte[] ivBytes = new byte[16]; // 16 bytes for AES
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(ivBytes);
            }

            // Convert the IV to a Base64-encoded string for storage or transmission
            IV = Convert.ToBase64String(ivBytes);

            // Create an AES encryption provider
            using Aes aesProvider = Aes.Create();

            // Set the key and IV
            aesProvider.Key = keyBytes;
            aesProvider.IV = ivBytes;

            // Create an AES encryptor
            using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();

            // Create a CryptoStream to perform encryption
            using CryptoStream cryptoStream = new CryptoStream(
                stream,
                cryptoTransform,
                CryptoStreamMode.Write
            );

            // Convert the data to bytes using UTF-8 encoding and write it to the CryptoStream
            byte[] jsonDataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            cryptoStream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
        }
        catch (Exception ex)
        {
            // Handle the exception, such as logging an error message
            Debug.LogError("Error encrypting data: " + ex.Message);
            // Optionally, throw or handle the error in a way that's appropriate for your application
        }*/

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
    public GameData Load()
    {
        //string fullPath = Path.Combine(DataDirPath, DataFileName); //Using combine instead of string concatenation because different operating systems have different file seperators.
        string fullPath = DataDirPath + "/" + DataFileName; //Using combine instead of string concatenation because different operating systems have different file seperators.
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
                else
                {
                    loadedData = JsonConvert.DeserializeObject<GameData>(dataToLoad); //Deserializing the data from JSON back into the C# object.
                }
                Debug.Log($"FileDataHandler Load() data success.");

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private GameData ReadEncryptedData(string path)
    {
        /*try
        {
            string KEY2 = KEY;
            string IV2 = IV;

            // Trim whitespace
            KEY2 = KEY2.Trim();
            IV2 = IV2.Trim();

            // Decode the KEY and IV strings
            byte[] keyBytes = Convert.FromBase64String(KEY2);
            byte[] ivBytes = Convert.FromBase64String(IV2);

            // Read encrypted data from file
            byte[] fileBytes = File.ReadAllBytes(path);

            // Create AES provider
            using Aes aesProvider = Aes.Create();

            // Set key and IV
            aesProvider.Key = keyBytes;
            aesProvider.IV = ivBytes;

            // Create decryptor
            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor();

            // Decrypt data
            using MemoryStream decryptionStream = new MemoryStream(fileBytes);
            using CryptoStream cryptoStream = new CryptoStream(decryptionStream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader reader = new StreamReader(cryptoStream);

            // Read decrypted data as string
            string decryptedData = reader.ReadToEnd();

            // Deserialize decrypted data
            return JsonConvert.DeserializeObject<GameData>(decryptedData);
        }
        catch (FormatException ex)
        {
            // Handle the FormatException, such as logging an error message
            Debug.LogError("Error decoding Base64 string: " + ex.Message);
            // Optionally, throw or handle the error in a way that's appropriate for your application
            return null; // Or handle the error in a way that's appropriate for your application
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Debug.LogError("Error reading encrypted data: " + ex.Message);
            // Optionally, throw or handle the error in a way that's appropriate for your application
            return null; // Or handle the error in a way that's appropriate for your application
        }
        */

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
}

//Source 1: https://www.youtube.com/watch?v=mntS45g8OK4&list=WL&index=5 for the Newtonsoft stuff and encryption/decryption.
//Source 2: https://www.youtube.com/watch?v=aUi9aijvpgs&list=PL3viUl9h9k7-ucrHVH1fpirA63WYEgo4-&index=15 not using it completely because it uses json utility and not json net.
