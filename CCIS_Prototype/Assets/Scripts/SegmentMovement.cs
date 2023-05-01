using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentMovement : MonoBehaviour
{
    public List<Transform> moveVolumeCentres;
    public float speed;
    public float timeLimit;



    
    void Update()
    {
        if (Time.time < timeLimit)
        {
            for (int i = 0; i < moveVolumeCentres.Count; i++)
            {

                if (moveVolumeCentres[i].position.z > -10)
                {
                    moveVolumeCentres[i].Translate(0, 0, -speed * Time.deltaTime);
                }
                else
                {
                    moveVolumeCentres[i].Translate(0, 0, 45);
                }
            }
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
