using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    private static PlatformList platformList = new PlatformList();
    public bool cubeInGame = false;//Индикатор состояния главного куба (в игре или в меню)
    private PlatformGenerator platformGenerator = new PlatformGenerator(platformList);
    void Start()
    {
        platformGenerator.CreateStartPlatform();
    }
    void FixedUpdate(){
        if(cubeInGame){//Если куб в игре (а не в меню)
            if(platformList.Count() >= 10){
                DeletePlatform();
            }
            if(platformList.Count() < 10){
               platformGenerator.GeneratePlatform();
            }
        }
    }

    public void DeletePlatform(){
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        float posCamY = mainCamera.transform.position.y;
        if(platformList. GetFirst(). GetPosition()['y'] < posCamY - 6.0f && platformList.Count() >= 10)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().RemoveGameObject(platformList.GetFirst().GetGameObject());
            platformList.DeleteFirst();
        }
    }
}

public class Platform
{
    private float posX, posY, posZ, sizeX, sizeY, sizeZ;//Позиция и размыры платформы
    private GameObject self;//ссылка на созданный объект куба
    

    public Platform(){//коструктор платформы ПЕРЕОПРЕДЕЛИТЬ!!!
        // self = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().CreatePlatform(GameObject.FindGameObjectWithTag("Platform"));
    }

    public void SetSize(float sizeX = 1.5f, float sizeY = 0.5f, float sizeZ = 0.5f){
        this.sizeX = sizeX;
        this.sizeY = sizeY;
        this.sizeZ = sizeZ;
        self.GetComponent<Transform> ().localScale = new Vector3(sizeX, sizeY, sizeZ);
    }
    public void SetPosition(float posX, float posY, float posZ = 0){
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        self.GetComponent<Transform> ().localPosition = new Vector3(posX, posY, posZ);
    }

    public Dictionary<char, float> GetPosition(){
        Dictionary <char, float> positions = new Dictionary<char, float>();
        positions.Add('x', posX);
        positions.Add('y', posY);
        positions.Add('z', posZ);
        return positions;
    }
    public Dictionary<char, float> GetSize(){
        Dictionary <char, float> positions = new Dictionary<char, float>();
        positions.Add('x', sizeX);
        positions.Add('y', sizeY);
        positions.Add('z', sizeZ);
        return positions;
    }
    public GameObject GetGameObject(){
        return self;
    }
    public void SetGameObject(GameObject obj){
        self = obj;
    }
}

public class StartPlatform : Platform
{
    public StartPlatform(){
        SetGameObject(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().CreatePlatform(GameObject.FindGameObjectWithTag("Platform")));
        SetSize(6f, 0.5f, 0.5f);
        SetPosition(0f, -3f, 0);
    }
}

public class CommonPlatform : Platform
{
    public CommonPlatform(float posX, float posY, float posZ)
    {
        SetGameObject(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().CreatePlatform(GameObject.FindGameObjectWithTag("Platform")));
        SetSize(1.2f, 0.25f, 0.25f);
        SetPosition(posX, posY, posZ);
    }
}

public class HighJumpPlatform : Platform
{
    public HighJumpPlatform(float posX, float posY, float posZ)
    {
        SetGameObject(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().CreatePlatform(GameObject.FindGameObjectWithTag("HighJumpPlatform")));
        SetPosition(posX, posY, posZ);
    }
}

public class VoidPlatform : Platform
{
    public VoidPlatform(float posX, float posY, float posZ)
    {
        SetGameObject(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<platformFather>().CreatePlatform(GameObject.FindGameObjectWithTag("VoidPlatform")));
        SetPosition(posX, posY, posZ);
    } 
}

public class PlatformList
{
    private List <Platform> platformList = new List <Platform>();

    public void Add(Platform platform){
        platformList.Add(platform);
    }
    public Platform GetFirst(){
        return platformList[0];
    }
    public Platform GetLast(){
        return platformList[platformList.Count - 1];
    }
    public void DeleteFirst(){
        platformList.RemoveAt(0);
    }
    public void DeleteLast(){
        platformList.RemoveAt(platformList.Count - 1);
        platformList.TrimExcess();
    }
    public int Count(){
        return platformList.Count;
    }
}

public class PlatformGenerator
{
    PlatformList platformList;
    public PlatformGenerator( PlatformList platformList){
        this.platformList = platformList;
    }
    public void CreateStartPlatform()
    {
        StartPlatform sp = new StartPlatform();
        GameObject plgo = sp.GetGameObject();
        plgo.AddComponent<platformFather>().RemoveBoxCollider();
        platformList.Add(sp);
    }
    public void CreatePlatform()
    {
        float posY = GetPlatformPosY();//Получение позиции по Y
        float posX = GetPlatformPosX();//Получение позиции по Х
        CommonPlatform pl = new CommonPlatform(posX, posY, 0);
        GameObject plgo = pl.GetGameObject();
        platformList.Add(pl);
    }
    public void CreateHighJumpPlatform()
    {
        float posY = GetPlatformPosY();//Получение позиции по Y
        float posX = GetPlatformPosX();//Получение позиции по Х
        HighJumpPlatform pl = new HighJumpPlatform(posX, posY, 0);
        GameObject plgo = pl.GetGameObject();
        platformList.Add(pl);
    }
    public void CreateVoidPlatform()
    {
        float posY = GetPlatformPosY();//Получение позиции по Y
        float posX = GetPlatformPosX();//Получение позиции по Х
        VoidPlatform pl = new VoidPlatform(posX, posY, 0);
        GameObject plgo = pl.GetGameObject();
        platformList.Add(pl);
    }
    private float GetPlatformPosX(){
        return Random.Range(-2.5f, 2.5f);        
    }
    private float GetPlatformPosY(){
        Platform lastPlatform = platformList.GetLast();
        float lastPlatformPositionY = lastPlatform.GetPosition()['y'];
        lastPlatformPositionY += Random.Range(1.1f, 1.45f);
        return lastPlatformPositionY;
    }
    public void GeneratePlatform(){
        float  platfomProbability = Random.Range(0f, 100.0f);
        if(platfomProbability <= 7f)
            CreateHighJumpPlatform();
        else if(7 < platfomProbability && platfomProbability <= 14 && platformList.GetLast().GetGameObject().tag != "VoidPlatform")
            CreateVoidPlatform();
        else
            CreatePlatform();
    }
}