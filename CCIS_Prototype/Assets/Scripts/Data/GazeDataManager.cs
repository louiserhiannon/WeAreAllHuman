using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GazeDataManager : MonoBehaviour
{
    public Dictionary<int, string> objectTypeDictionary = new Dictionary<int, string>();
    public List<GazeData> gazeDataList = new List<GazeData>();
    public PlayerData playerData;
    public string filename = "Assets/Data/JSON/playerData.json";
    public static GazeDataManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Debug.Log(gazeDataList.Count); 
    }

    private void OnApplicationQuit()
    {
        //populate output list
        playerData.gazeDataOutput = new List<GazeData>(gazeDataList.Count);
        for (int i = 0; i < gazeDataList.Count; i++)
        {
            playerData.gazeDataOutput.Add(gazeDataList[i]);
        }


        //send data to JSON file
        string jsonString = SaveToString(playerData);
        File.WriteAllText(filename, jsonString);
    }

    public string SaveToString(PlayerData playerData)
    {
        return JsonUtility.ToJson(playerData, true);
    }
}

[Serializable]
public struct GazeData
{
    public int objectID;
    public string targetType;
    public int gazeIndex;
    public float gazeTime;
    public float gazeCaptureDistance;
    //public string behaviour;

}

[Serializable]
public class PlayerData
{
    public List<GazeData> gazeDataOutput;
}


