using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCliks : MonoBehaviour
{
    public GameObject buttons, cube, mainCamera;
    public Text playText, gameName;
    private bool clicked = false;

    void OnMouseDown(){
        if(!clicked){
            clicked = true;
            playText.gameObject.SetActive(false);
            gameName.text = "0";
            buttons.gameObject.GetComponent <ScrollObjects>().speed = -10f;
            buttons.gameObject.GetComponent <ScrollObjects>().checkPos = -160f;
            cube.GetComponent<Animation> ().Play("StartGameCube");
            BoxCollider2D cbc = cube.AddComponent<BoxCollider2D>();
            Rigidbody2D crb = cube.AddComponent<Rigidbody2D>();
            crb.freezeRotation = true;//Замораживаем поворот
            cbc.size = new Vector2(0.5f, 1);
            cube.GetComponent<MainCubeJump> ().gameObject.SetActive(true);
            mainCamera.AddComponent<LevelGenerate>();
            mainCamera.AddComponent<MainCamera> ().follow = cube.GetComponent<Transform>();

        }
        
    }

}
