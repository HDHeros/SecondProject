using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformFather : MonoBehaviour
{
    void Start()
    {

    }
    public void RemoveBoxCollider(){
        DestroyImmediate(GetComponent<BoxCollider>());
    }

    public void RemoveGameObject(GameObject obj){
        DestroyImmediate(obj);
    }

    public GameObject CreatePlatform(GameObject obj){
        return Instantiate(obj);
    }
}
