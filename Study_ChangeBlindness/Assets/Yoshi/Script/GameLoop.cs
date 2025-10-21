using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop_new : MonoBehaviour
{
    public bool fin;

    int gameStatus;
    int changeMode;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = 0;
        changeMode = 0;
        fin = false;
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
                //���[�t�B���O�@���剻�E�⏬�� �����E����

                if (fin)
                    changeMode = 1;
            }
            else if (changeMode == 1)
            {
                //�؂�ւ��@���剻�E�⏬�� �����E����

                if (fin)
                    changeMode = 2;
            }
            else if (changeMode == 2)
            {
                //�F

                if (fin)
                    changeMode = 3;
            }
            else if (changeMode == 3)
            {
                //���[�t�B���Ox�F

                if (fin)
                    changeMode = 4;
            }
            else
            {
                //�؂�ւ��x�F

                if (fin)
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