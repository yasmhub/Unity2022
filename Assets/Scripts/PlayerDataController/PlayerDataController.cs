using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public static class PlayerDataController 
{
    static PlayerDataController()
    {
        // set save file-path
        string savePath = Application.persistentDataPath + "/Save/";
        Debug.Log("Player Data Controller uses path: '" + savePath + "'");
        // check if save directory exists, create it
        if (Directory.Exists(savePath) == false)
        {
            Directory.CreateDirectory(savePath);
        }
    }

    // check the persistent data path for player save files
    public static (string[], bool) GetFileNames()
    {
        string savePath = Application.persistentDataPath + "/Save/";
        var directoryInfo = new DirectoryInfo(savePath);
        FileInfo[] fileInfo = directoryInfo.GetFiles();

        if (fileInfo.Length > 0)
        {
            string[] fileNames = new string[fileInfo.Length - 1];
            for (int i = fileNames.Length - 1; i >= 0; --i)
            {
                fileNames[i] = fileInfo[i].Name;
                Debug.Log("Found player save '" + fileNames[i] + "'.");
            }
            return (fileNames, true);
        }
        else
        {
            return (null, false);
        }
    }

    // create new save file
    public static (PlayerData, bool) CreatePlayerData(string PlayerName)
    {
        string savePath = Application.persistentDataPath + "/Save/";
        var directoryInfo = new DirectoryInfo(savePath);
        FileInfo[] fileInfo = directoryInfo.GetFiles();

        string[] fileNames = GetFileNames().Item1;
        // can't create if file already exists
        if (fileInfo.Length >= 1)
        {
            for (int i = fileNames.Length - 1; i >= 0; --i)
            {
                if (fileInfo[i].Name == PlayerName)
                {
                    Debug.Log("File named " + PlayerName + " already exists!");
                    return (null, false);
                }
            }
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream newFile = File.Open(savePath + PlayerName, FileMode.Create);

        PlayerData newPlayerData = new PlayerData();
        newPlayerData.name = PlayerName;

        binaryFormatter.Serialize(newFile, newPlayerData);
        newFile.Close();
        Debug.Log("File named " + PlayerName + " created.");

        return (newPlayerData, true);
    }

    // load a save file
    public static PlayerData LoadPlayerData(string PlayerName)
    {
        string savePath = Application.persistentDataPath + "/Save/";
        var directoryInfo = new DirectoryInfo(savePath);
        FileInfo[] fileInfo = directoryInfo.GetFiles();

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream newFile = File.Open(savePath + PlayerName, FileMode.Open);
        PlayerData playerData = new PlayerData();

        playerData = (PlayerData)binaryFormatter.Deserialize(newFile);
        newFile.Close();
        Debug.Log("File named " + playerData.name + " loaded.");

        return playerData;
    }

    // update a player's save file
    public static void SavePlayerData(PlayerData NewSaveData)
    {
        // BinaryFormatter binaryFormatter = new BinaryFormatter();
    }
}

