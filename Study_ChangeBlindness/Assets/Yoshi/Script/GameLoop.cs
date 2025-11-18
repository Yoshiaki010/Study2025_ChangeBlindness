using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop_new : MonoBehaviour
{
    public bool objFin;

    int gameStatus;
    int changeMode;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = 0;
        changeMode = 0;
        objFin = false;
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
            if (changeMode == 0)
            {
                //モーフィング

                if (objFin)
                    changeMode = 1;
            }
            else if (changeMode == 1)
            {
                //切り替わり


                if (objFin)
                    changeMode = 2;
            }
            else if (changeMode == 2)
            {
                //色

                if (objFin)
                    changeMode = 3;
            }
            else if (changeMode == 3)
            {
                //モーフィングx色

                if (objFin)
                    changeMode = 4;
            }
            else
            {
                //切り替わりx色

                if (objFin)
                    gameStatus = 2;
            }
        }
        else
        {
            gameStatus = 0;
            Debug.Log($"GameLoop : gameStatus = Unknow = {gameStatus}");
        }
    }
}