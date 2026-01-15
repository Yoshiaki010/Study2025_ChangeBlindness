using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public List<Material> Images;
    public List<GameObject> StartObjects;

    public ChangeController changeController;
    public RayController rayController;

    public GameObject rdyCanvas;
    public TextMeshProUGUI resultText;
    public bool startChange;
    public float resetTime;
    public float changeTime;
    public int gameStatus;
    public int buttonN;

    string result;
    float gameTime;
    int n;
    bool rdyStatus;

    // Start is called before the first frame update
    void Start()
    {
        startChange = false;
        rdyStatus = false;
        gameStatus = 0;
        buttonN = 0;
        n = 0;
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameStatus == 0)
        {
            //待機　または　チュートリアル
            Debug.Log($"GameLoop : 待機中");
            if (Input.GetKeyDown(KeyCode.N))
            {
                n += 1;
                gameStatus = 1;
                gameTime = 0f;
                Reset();
                Rady();
                Debug.Log($"GameLoop : モーフィング　開始");
            }
        }
        else if (gameStatus == 1)
        {
            //モーフィング
            if (changeTime < gameTime)
            {
                Debug.Log($"GameLoop : モーフィング　変化可能");
                if (!startChange)
                {
                    changeController.morphingChange = true;
                    changeController.changeTiming = true;
                    startChange = true;
                }

                if (!rdyStatus && buttonN == 1 || resetTime < gameTime)
                {
                    rdyCanvas.SetActive(true);
                    result = $"Result{n}\nMorph : {gameTime}\n";
                    rdyStatus = true;
                }
            }

            if (rdyStatus)
            {
                if (buttonN == 2)
                {
                    rdyCanvas.SetActive(false);
                    gameStatus = 2;
                    gameTime = 0f;
                    Reset();
                    Rady();
                    Debug.Log($"GameLoop : モーフィング終了");
                    Debug.Log($"GameLoop : 切り替わり");
                }
            }
        }
        else if (gameStatus == 2)
        {
            //切り替わり
            if (changeTime < gameTime)
            {
                Debug.Log($"GameLoop : 切り替わり　変化可能");
                if (!startChange)
                {
                    changeController.switchChange = true;
                    changeController.changeTiming = true;
                    startChange = true;
                }

                if (!rdyStatus && buttonN == 1 || resetTime < gameTime)
                {
                    rdyCanvas.SetActive(true);
                    result += $"Switch : {gameTime}\n";
                    rdyStatus = true;
                }
            }

            if (rdyStatus)
            {
                if (buttonN == 2)
                {
                    rdyCanvas.SetActive(false);
                    gameStatus = 3;
                    gameTime = 0f;
                    Reset();
                    Rady();
                    Debug.Log($"GameLoop : 切り替わり終了");
                    Debug.Log($"GameLoop : 色　開始");
                }
            }

        }
        else if (gameStatus == 3)
        {
            //色
            if (changeTime < gameTime)
            {
                Debug.Log($"GameLoop : 色　変化可能");
                if (!startChange)
                {
                    changeController.colorChange = true;
                    changeController.changeTiming = true;
                    startChange = true;
                }

                if (!rdyStatus && buttonN == 1 || resetTime < gameTime)
                {
                    rdyCanvas.SetActive(true);
                    result += $"Color : {gameTime}\n";
                    rdyStatus = true;
                }
            }

            if (rdyStatus)
            {
                if (buttonN == 2)
                {
                    rdyCanvas.SetActive(false);
                    gameStatus = 4;
                    gameTime = 0f;
                    Reset();
                    Rady();
                    Debug.Log($"GameLoop : 色　終了");
                    Debug.Log($"GameLoop : モーフィングx色　開始");
                }
            }
        }
        else if (gameStatus == 4)
        {
            //モーフィングx色
            if (changeTime < gameTime)
            {
                Debug.Log($"GameLoop : モーフィングx色　変化可能");
                if (!startChange)
                {
                    //changeController.morphingChange = true;
                    //changeController.colorChange = true;
                    //changeController.changeTiming = true;
                    startChange = true;
                }

                if (!rdyStatus && buttonN == 1 || resetTime < gameTime)
                {
                    rdyCanvas.SetActive(true);
                    result += $"Mor&Col : {gameTime}\n";
                    rdyStatus = true;
                }
            }

            if (rdyStatus)
            {
                if (buttonN == 2)
                {
                    rdyCanvas.SetActive(false);
                    gameStatus = 5;
                    gameTime = 0f;
                    Reset();
                    Rady();
                    Debug.Log($"GameLoop : モーフィングx色　終了");
                    Debug.Log($"GameLoop : 切り替わりx色　開始");
                }
            }
        }
        else if (gameStatus == 5)
        {
            //切り替わりx色

            if (changeTime < gameTime)
            {
                Debug.Log($"GameLoop : 切り替わりx色　変化可能");
                if (!startChange)
                {
                    //changeController.switchChange = true;
                    //changeController.colorChange = true;
                    //changeController.changeTiming = true;
                    startChange = true;                
                }

                if (!rdyStatus && buttonN == 1 || resetTime < gameTime)
                {
                    rdyCanvas.SetActive(true);
                    result += $"Swi&Col : {gameTime}\n";
                    rdyStatus = true;
                }
            }

            if (rdyStatus)
            {
                if (buttonN == 2)
                {
                    resultText.text += result;
                    gameStatus = 0;
                    gameTime = 0f;
                    Reset();
                    Debug.Log($"GameLoop : 切り替わりx色　終了");
                    Debug.Log($"GameLoop : 待機　開始");
                }
            }
        }
        else
        {
            gameStatus = 0;
            Debug.Log($"GameLoop : 不明な分岐 = {gameStatus}");
        }
    }

    void Reset()
    {
        changeController.Reset();
        changeController.changeObjects.Clear();
        startChange = false;
        rdyStatus = false;
        buttonN = 0;
    }

    void Rady()
    {
        GameObject startObj = StartObjects[gameStatus - 1];
        GameObject obj = Instantiate(startObj, startObj.transform.position, startObj.transform.rotation);
        Changer obj_changer = obj.GetComponent<Changer>();
        obj_changer.gameLoop = this;
        obj_changer.rayController = rayController;
        obj_changer.changeController = changeController;
    }

}