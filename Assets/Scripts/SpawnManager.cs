using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject tiles;
    public GameObject[] weapons;
    public GameObject[] bombs;

    public GameObject coinsPrefab;
     Transform back, front;
    public LayerMask ground;
    float xPosCoins;
    float zOffset;
    float startZPosCoins;
    float startZPosCircleCoins;


    public GameObject circleCoinsPrefab;
    float zCircleSpawn;

    public float zSpawn;
    public float tileLength;
    public int numberOfTiles;

    public Transform player;

    GameObject coins;
    GameObject circleCoins;

    private List<GameObject> activeWeapons = new List<GameObject>();
    public  static List<GameObject> activeTiles = new List<GameObject>();

    public float timeToSpawnWeapon;
    public float timeToSpawnBomb;
    public float timeToSpawnCoins;
    private void Awake()
    {
        zOffset = 10;
        zCircleSpawn = 6;
        zSpawn = 0;
        numberOfTiles = 3;
        tileLength = 15.5f;
        timeToSpawnWeapon = 2;
        timeToSpawnBomb = 1.5f;
        timeToSpawnCoins = 2;
    }


    void Start()
    {

      for(int i=0; i<numberOfTiles;i++)
        {
            SpawnTile();
            SpawnCircleCoins();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z -18 > zSpawn - (tileLength *numberOfTiles))
        {

            SpawnTile();
            DeleteTile();
            SpawnCircleCoins();
        }


        if (!PlayerManager.isGameStarted)
                return;

        if(PlayerManager.isGameOver)
        {
            return;
        }



        timeToSpawnWeapon -= Time.deltaTime;
        if (timeToSpawnWeapon <= 0)
        {
            SpawnWeapon(Random.Range(0,weapons.Length));
            timeToSpawnWeapon = 2;
        }


        timeToSpawnBomb -= Time.deltaTime;
        if(timeToSpawnBomb <= 0)
        {

            SpawnBomb(Random.Range(0, 3));
            timeToSpawnBomb = 3;
        }

        timeToSpawnCoins -= Time.deltaTime;
        if(timeToSpawnCoins <= 0)
        {
            SpawnCoins();
            timeToSpawnCoins = 2;
        }

        if(coins !=null)
        {
            Debug.Log("coins instatniated");

            if(Physics.Raycast(back.position,Vector3.down,ground)==false 
                || Physics.Raycast(front.position,Vector3.down,ground)==false)
            {
                zOffset++;
                coins.transform.position = new Vector3(xPosCoins, 0, startZPosCoins + zOffset);


            }

        }
        
    }


    public void SpawnTile()
    {

       GameObject go =  Instantiate(tiles, new Vector3(0, 1.05f, zSpawn), transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
        

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

    public void SpawnWeapon(int index)
    {
        GameObject weapon = Instantiate(weapons[index]);
        Destroy(weapon, 3);
       

    }

    public void SpawnBomb(int index)
    {
        Instantiate(bombs[index]);
    }
    public void SpawnCoins()
    {
        

        int randomPosCoins = Random.Range(0, 3);
        switch(randomPosCoins)
        {
            case 0:
                xPosCoins = 0;
                break;
            case 1:
                xPosCoins = 1.2f;
                break;
            case 2:
                xPosCoins = -1.2f;
                break;

        }

        startZPosCoins = player.transform.position.z;
         coins =  Instantiate(coinsPrefab, new Vector3(xPosCoins,0, startZPosCoins + zOffset), coinsPrefab.transform.rotation);
        back = coins.transform.GetChild(3);
        front = coins.transform.GetChild(4);
        

    }

    public void SpawnCircleCoins()
    {


        int randomPosCircleCoins = Random.Range(0, 3);
        switch (randomPosCircleCoins)
        {
            case 0:
                xPosCoins = 0;
                break;
            case 1:
                xPosCoins = 1.2f;
                break;
            case 2:
                xPosCoins = -1.2f;
                break;

        }

        
        circleCoins = Instantiate(circleCoinsPrefab, new Vector3(xPosCoins, 0, zCircleSpawn), circleCoinsPrefab.transform.rotation);
        zCircleSpawn += tileLength;


    }




}
