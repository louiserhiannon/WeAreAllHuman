using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRaycast : MonoBehaviour
{
    public int objectID;
    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer skinnedMesh;
    private Color colourOriginal;
    private Color colourHit;
    public Camera headset;
    public float raycastDistance;
    //[SerializeField] private LayerMask layerMask;
    public int layer;
    private int layerAsLayerMask;
    public float gazeTime = 0;
    public int gazeCount = 0;
    private float targetDistance;
    private TargetObjectProperties objectProperties;

    [SerializeField] private bool gazeActive = false;
    [SerializeField] private bool sendData = false;
    [SerializeField] private bool calculateDistance = true;

    void Start()
    {
        objectProperties = GetComponent<TargetObjectProperties>();
        if(GetComponent<MeshRenderer>() != null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            colourOriginal = meshRenderer.material.color;
        }
        else
        {
            skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
            colourOriginal = skinnedMesh.material.color;
        }  
        
        colourHit = Color.green;

        layerAsLayerMask = (1 << layer);
        
    }

    void FixedUpdate()
    {

        if (Physics.Raycast(headset.transform.position, headset.transform.forward, raycastDistance, layerAsLayerMask))
        {
            
                      
            if(meshRenderer!= null)
            {
                meshRenderer.material.color = colourHit;
            }
            else
            {
                skinnedMesh.material.color = colourHit;
            }
            
            gazeTime += Time.deltaTime;

            if (!gazeActive)
            {
                if(gazeTime > 0.5f)
                {
                    gazeCount++;
                    if (calculateDistance)
                    {
                        targetDistance = Vector3.Distance(transform.position, headset.transform.position);
                        calculateDistance= false;
                    }
                    gazeActive = true;
                    sendData = true;
                }
                
            }
        }
        else
        {
            if (meshRenderer != null)
            {
                meshRenderer.material.color = colourOriginal;
            }
            else
            {
                skinnedMesh.material.color = colourOriginal;
            }
            
            gazeActive = false;
            calculateDistance = true;
            if (sendData)
            {
                AddDataToList();
                
            }
            gazeTime = 0;
        }

       

        
    }
    public void AddDataToList()
    {
        GazeData interactionGazeData = new GazeData();
        interactionGazeData.objectID = objectProperties.objectID;
        interactionGazeData.targetType = GazeDataManager.instance.objectTypeDictionary[objectProperties.objectID];
        interactionGazeData.gazeIndex = gazeCount;
        interactionGazeData.gazeTime = gazeTime;
        interactionGazeData.gazeCaptureDistance = targetDistance;

        GazeDataManager.instance.gazeDataList.Add(interactionGazeData);
        //Debug.Log(gazeDataList.Count);
        //Debug.Log(gazeDataList[interactionGazeData.gazeIndex - 1].objectID);
        //Debug.Log(gazeDataList[interactionGazeData.gazeIndex - 1].targetType);
        //Debug.Log(gazeDataList[interactionGazeData.gazeIndex - 1].gazeIndex);
        //Debug.Log(gazeDataList[interactionGazeData.gazeIndex - 1].gazeTime);
        //Debug.Log(gazeDataList[interactionGazeData.gazeIndex - 1].gazeDistance);

        sendData = false;
    }

    
}
