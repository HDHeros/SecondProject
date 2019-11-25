using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    public Transform follow;//Объект, за которым вертикально следует камера (персонаж)
    private Vector3 camNewposition;
    private float camMaxY;
    private float score = 0;


    void Start()
    {
        camMaxY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CamRaise();
        DeadDetect();
        // SetWallsY();
        // camNewposition = follow.position;
        // camNewposition.z = -10f;
        // camNewposition.x = 0f;
        // if(camNewposition.y < camMaxY){
        //     camNewposition.y = camMaxY;
        // }
        // else{
        //     score +=  follow.position.y - camMaxY;
        //     SetNewScore();
        //     camMaxY = camNewposition.y;
        // }
        // transform.position = Vector3.Lerp(transform.position, camNewposition, Time.deltaTime * 2f);
    }

    private void CamRaise()
    {
        
        SetWallsY();
        camMaxY += score / 10000;
        camNewposition = follow.position;
        camNewposition.z = -10f;
        camNewposition.x = 0f;
        if(camNewposition.y < camMaxY){
            camNewposition.y = camMaxY;
        }
        else{
            score +=  follow.position.y - camMaxY;
            SetNewScore();
            camMaxY = camNewposition.y;
        }
        transform.position = Vector3.Lerp(transform.position, camNewposition, Time.deltaTime * 2f);
    }
    private void DeadDetect(){
        if(transform.position.y - 5f > follow.transform.position.y)
        {
            print("DEAD");//TODO...
        }
    }
    private void SetNewScore(){
        GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent <Text>().text = Mathf.Round(score).ToString();
    }
    private void SetWallsY(){
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach(GameObject wall in walls)
        {
            wall.transform.position = new Vector2(wall.transform.position.x, transform.position.y);
        }
    }
}
