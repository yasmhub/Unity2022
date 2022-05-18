using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class TestPlayerData : MonoBehaviour
{
    public bool writeExamples = false;
    // serialize some default PlayerData with random names 
    private void Start()
    {
        if (writeExamples) { WriteExamples(); }

        var fileNames = PlayerDataController.GetFileNames();
        Debug.Log(fileNames.Item1);
    }

    public void WriteExamples()
    {

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        for (int i = 0; i < 10; ++i)
        {
            // pick a 1-letter name
            string newFileName = "PNAME" + Random.Range(0, 101);

            // open a new file with this name
            FileStream newFile = File.Open(Application.persistentDataPath + "/Save/" + newFileName, FileMode.Create);

            // make a new player data, save it in the binary file
            PlayerData newPlayerData = new PlayerData();
            binaryFormatter.Serialize(newFile, newPlayerData);
            newFile.Close();
        }
    }
}
