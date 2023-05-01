using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectProperties : MonoBehaviour
{
    //public ObjectData objectData = new ObjectData();
    public int objectID;
    public string objectType;

    private void Start()
    {
        GazeDataManager.instance.objectTypeDictionary.Add(objectID, objectType);
        Debug.Log(GazeDataManager.instance.objectTypeDictionary[objectID]);
    }

}
