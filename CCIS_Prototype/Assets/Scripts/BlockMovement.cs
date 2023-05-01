using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public float distance;
    public float minDistance = 0.1f;
    public float speed;
    public Transform targetTransform;
    public Transform moveVolumeCentre;
    public int transformChangeFrequency;
    public int speedChangeFrequency;
    private float zOffset = 2f;
    private float xOffset = 3f;
    private float yOffset = 3f;
    private float speedMin = 0.05f;
    private float speedMax = 0.15f;
    


    private void Awake()
    {
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
    }

    private void Start()
    {
        distance = Vector3.Distance(targetTransform.position, transform.position);
        SetParameters();
        SetTransform();
    }
    public void MoveToMinimumDistance()
    {
        if (targetTransform != null)
        {
            Vector3 moveDirection = targetTransform.position - transform.position;
            this.transform.Translate(speed * Time.deltaTime * moveDirection);
            
            //Vector3 lookDirection = new (Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
                       

            distance = Vector3.Distance(targetTransform.position, transform.position);
        }

    }
    public void SetParameters()
    {
        speed = Random.Range(speedMin, speedMax);
        
    }

    public void SetTransform()
    {
        targetTransform.position = moveVolumeCentre.position + Random.Range(-zOffset, zOffset) * Vector3.forward + Random.Range(-xOffset, xOffset) * Vector3.right + Random.Range(-yOffset, yOffset) * Vector3.up;
    }

    private void Update()
    {
        if (Random.Range(1, 10000) < transformChangeFrequency)
        {
            SetTransform();
        }

        if (Random.Range(1, 1000) < speedChangeFrequency)
        {
            SetParameters();
        }

        MoveToMinimumDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        targetTransform.position = transform.position + (transform.position - other.transform.position) * 5;
    }


}
