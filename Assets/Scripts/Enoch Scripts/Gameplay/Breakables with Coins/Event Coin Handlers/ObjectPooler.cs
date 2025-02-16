﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectTypes { Breakable, Coin, HealthPack }

public class ObjectPooler : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject breakablePrefab;
    public GameObject healthPrefab;

    public static ObjectPooler objectPool; // create an instance of this and no need to reference
    public int pooledAmount = 1;
    public bool expandPool = true;    

    public List<GameObject> pooledCoins;
    public List<GameObject> pooledHealthPacks;
    public List<GameObject> pooledBreakables;

    public event EventHandler OnCoinSpawn;
    public event EventHandler OnHealthSpawn;
    public event EventHandler OnBreakableSpawn;

    // Destroy unloads object from the memory and set reference to null so in order to use it again you need to recreate it, via let's say instantiate. 
    // Meanwhile SetActive just hides the object and disables all components on it so if you need you can use it again.

    void Awake()
    {
        // the current script is equals to everything in this script
        objectPool = this;
    }

    // Use this for initialization
    void Start()
    {
        //You need to populate both the Breakable list
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Breakable");

        foreach (GameObject tempObject in temp)
        {
            pooledBreakables.Add(tempObject);
        }

        foreach (GameObject breakableObj in pooledBreakables)
            breakableObj.GetComponent<IPoolableObject>().SetObjectPool(this);

        SeedCoinList(coinPrefab, pooledCoins);
        SeedHealthList(healthPrefab, pooledHealthPacks);
    }


    public void ReturnObject(GameObject genericObject, ObjectTypes objectType)
    {
        switch (objectType) 
        {
            case ObjectTypes.Breakable:
                BreakableReturn(genericObject);
                break;
            case ObjectTypes.Coin:
                CoinReturn(genericObject);
                break;
            case ObjectTypes.HealthPack:
                HealthPackReturn(genericObject);
                break;
            default:
                break;
        }
    }





    public void BreakableReturn(GameObject returnedBreakable)
    {
        returnedBreakable.GetComponent<PickupEvent>().ResetSubscriptions();
        returnedBreakable.SetActive(false);
    }

    public void CoinReturn(GameObject returnedCoin)
    {
        Debug.Log("Coin Returned");
        returnedCoin.GetComponent<PickupEvent>().ResetSubscriptions();
        returnedCoin.SetActive(false);
        pooledCoins.Add(returnedCoin);
    }

    public void HealthPackReturn(GameObject returnedHealthPack)
    {
        Debug.Log("Health Returned");
        returnedHealthPack.GetComponent<PickupEvent>().ResetSubscriptions();
        returnedHealthPack.SetActive(false);
        pooledHealthPacks.Add(returnedHealthPack);
    }






    public GameObject GetHealthPack()
    {
        if (pooledHealthPacks.Count == 0)
        {
            SeedHealthList(healthPrefab, pooledHealthPacks);
        }

        GameObject poppedHealth = pooledHealthPacks[0];
        pooledHealthPacks.RemoveAt(0);
        RaiseCoinSpawn(poppedHealth, EventArgs.Empty);
        poppedHealth.SetActive(true);
        return poppedHealth;
    }

    public GameObject GetCoin()
    {
        if(pooledCoins.Count == 0)
        {
            SeedCoinList(coinPrefab, pooledCoins);
        }

        GameObject poppedCoin = pooledCoins[0];
        pooledCoins.RemoveAt(0);
        RaiseCoinSpawn(poppedCoin, EventArgs.Empty);
        poppedCoin.SetActive(true);
        return poppedCoin;
    }



    public GameObject GetBreakable()
    {
        if (pooledBreakables.Count == 0)
        {
            SeedCoinList(breakablePrefab, pooledBreakables);
        }

        GameObject poppedBreakable = pooledBreakables[0];
        pooledBreakables.RemoveAt(0);
        RaiseBreakableSpawn(poppedBreakable, EventArgs.Empty);
        poppedBreakable.SetActive(true);
        return poppedBreakable;
    }








    void SeedCoinList(GameObject prefab, List<GameObject> poolList)
    {
        for (int x = 0; x < 4; x++)
        {
            GameObject newCoin = Instantiate(prefab);
            newCoin.GetComponent<IPoolableObject>().SetObjectPool(this);
            newCoin.SetActive(false);
            poolList.Add(newCoin);
        }
    }
    void SeedHealthList(GameObject prefab, List<GameObject> poolList)
    {
        for (int x = 0; x < 4; x++)
        {
            GameObject newHealthPack = Instantiate(prefab);
            newHealthPack.GetComponent<IPoolableObject>().SetObjectPool(this);
            newHealthPack.SetActive(false);
            poolList.Add(newHealthPack);
        }
    }


    void RaiseBreakableSpawn(object spawnedBreakable, EventArgs args)
    {
        if (OnBreakableSpawn != null)
            OnBreakableSpawn.Invoke(spawnedBreakable, args);
    }

    void RaiseCoinSpawn(object spawnedCoin, EventArgs args)
    {
        if (OnCoinSpawn != null)
            OnCoinSpawn.Invoke(spawnedCoin, args);
    }

    void RaiseHealthSpawn(object spawnedHealth, EventArgs args)
    {
        if (OnHealthSpawn != null)
            OnHealthSpawn.Invoke(spawnedHealth, args);
    }
}

