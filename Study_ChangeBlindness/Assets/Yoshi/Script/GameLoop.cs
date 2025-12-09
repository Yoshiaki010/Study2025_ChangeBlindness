using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop_new : MonoBehaviour
{
    public bool finGame;
    public float resetTime;
    public float changeTime;

    public GameObject color_obj;
    public List<GameObject> changeObjects;

    bool buttonStatus;
    int gameStatus;
    float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        finGame = false;
        buttonStatus = false;
        gameStatus = 3;
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if(gameStatus == 0)
        {
            //待機　または　チュートリアル
            Debug.Log($"GameLoop : gameStatus waiting = 0 = {gameStatus}");
            if (buttonStatus || true)
            {
                gameStatus = 1;
                gameTime = 0f;
            }
        }
        else if(gameStatus == 1)
        {
            Debug.Log($"GameLoop : gameStatus モーフィング = 1 = {gameStatus},{gameTime}");
            //モーフィング
            if (changeTime < gameTime)
            {
                //Objectmanager.MakeChange();
                for (int i = 0; i < changeObjects.Count; i++)
                {
                    Changer obj_script = changeObjects[i].GetComponent<Changer>();
                    if (i == 0)
                        obj_script.MorphingChange(changeObjects[i]);
                }
            }

            if ( buttonStatus || resetTime < gameTime)
            {
                gameStatus = 2;
                Debug.Log($"GameLoop : gameStatus モーフィング終了 = {gameStatus},{gameTime},{finGame} || {buttonStatus} || {resetTime < gameTime}");
                gameTime = 0f;
                foreach(GameObject obj in changeObjects)
                {
                    Changer obj_script = obj.GetComponent<Changer>();
                    obj_script.Reset();
                }
            }
        }
        else if (gameStatus == 2)
        {
            Debug.Log($"GameLoop : gameStatus 切り替わり = 2 = {gameStatus},{gameTime}");
            if (changeTime < gameTime)
            {
                //切り替わり
                //Objectmanager.MakeChange();
                for (int i = 0; i < changeObjects.Count; i++)
                {
                    Changer obj_script = changeObjects[i].GetComponent<Changer>();
                    Debug.Log($"GameLoop : gameStatus 切り替わりたい = {gameStatus},{gameTime},{finGame},{!finGame}");
                    if (i == 0 && !finGame)
                    {
                        obj_script.SwitchChange( changeObjects[1] );
                        finGame = true;
                        Debug.Log($"GameLoop : gameStatus 今切り替わり = {gameStatus},{gameTime},{finGame}");
                    }
                }
            }

            if ( buttonStatus || resetTime < gameTime)
            {
                gameStatus = 3;
                Debug.Log($"GameLoop : gameStatus 切り替わり終了 = {gameStatus},{gameTime}");
                gameTime = 0f;
            }
        }
        else if (gameStatus == 3)
        {
            Debug.Log($"GameLoop : gameStatus 色 = 3 = {gameStatus}");
            //色
            if (changeTime < gameTime)
            {
                //Objectmanager.MakeChange();
                for (int i = 0; i < changeObjects.Count; i++)
                {
                    if (i == 0)
                    {
                        Debug.Log($"GameLoop : gameStatus 色変化 = 3 = {gameStatus}");
                        Changer obj_script = changeObjects[i].GetComponent<Changer>();
                        obj_script.ColorChange(color_obj);
                    }
                }
            }

            if (finGame || buttonStatus || resetTime < gameTime)
            {
                gameStatus = 4;
                gameTime = 0f;
            }
        }
        else if (gameStatus == 4)
        {
            //モーフィングx色

            if (finGame || buttonStatus || resetTime < gameTime)
            {
                gameStatus = 5;
                gameTime = 0f;
            }
        }
        else if (gameStatus == 5)
        {
            //切り替わりx色

            if (finGame || buttonStatus || resetTime < gameTime)
            {
                gameStatus = 0;
                gameTime = 0f;
            }
        }
        else
        {
            gameStatus = 0;
            Debug.Log($"GameLoop : gameStatus = Unknow = {gameStatus}");
        }
    }
}