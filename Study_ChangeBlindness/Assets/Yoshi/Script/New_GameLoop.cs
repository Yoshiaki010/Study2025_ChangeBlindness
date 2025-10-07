using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameLoop_new : MonoBehaviour
{
    float gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == 0)
        {
            //Waiting or Tutorial mode
            Debug.Log($"GameLoop : gameStatus = 0 = {gameStatus}");
            gameStatus = 1;
        }
        else if(gameStatus == 1)
        {
            //Object Number is Changeing mode
            Debug.Log($"GameLoop : gameStatus = 1 = {gameStatus}");
            gameStatus = 2;
        }
        else
        {
            Debug.Log($"GameLoop : gameStatus = Unknow = {gameStatus}");
        }
    }
}