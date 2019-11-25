using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform follow;//Объект, за которым вертикально следует камера (персонаж)
    private Vector3 camNewposition;
    private float camMaxY;


    void Start()
    {
        camMaxY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        camNewposition = follow.position;
        camNewposition.z = -10f;
        camNewposition.x = 0f;
        if(camNewposition.y < camMaxY){
            camNewposition.y = camMaxY;
        }
        else{
            camMaxY = camNewposition.y;
        }
        transform.position = Vector3.Lerp(transform.position, camNewposition, Time.deltaTime * 2f);
    }
}
