using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAnimation : MonoBehaviour
{
    private float rotationSpeed;
    private float rotationSpeedMin;
    private float rotationSpeedMax;
    private Vector3 lookDirection;


    private void Start()
    {
        rotationSpeedMin = 0.1f;
        rotationSpeedMax = 0.3f;
        lookDirection = transform.position;
    }

    public void Rotate()
    {
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
    }

    public void SetRotateParameters()
    {
        float directionX = Random.Range(-1, 1);
        float directionY = Random.Range(-1, 1);
        float directionZ = Random.Range(-1, 1);
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
        if (directionX != 0 && directionY != 0 && directionZ != 0)
        {
            lookDirection = new(directionX, directionY, directionZ);
        }
        lookDirection.Normalize();

    }


    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1, 1000) < 10)
        {
            SetRotateParameters();
        }

        Rotate();
    }
}
