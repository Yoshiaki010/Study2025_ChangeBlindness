using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public ChangeController changeController;

    public bool buttonStatus;
    public float resetTime;
    public float changeTime;
    public int gameStatus;

    public bool finGame;
    float gameTime;

    // Start is called before the first frame update
    void Start()
    {
        finGame = false;
        buttonStatus = false;
        gameStatus = 0;
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameStatus == 0)
        {
            //待機　または　チュートリアル
            Debug.Log($"GameLoop : gameStatus waiting = 0 = {gameStatus}");
            if (buttonStatus || true)
            {
                gameStatus = 1;
                gameTime = 0f;
            }
        }
        else if (gameStatus == 1)
        {
            Debug.Log($"GameLoop : gameStatus モーフィング = 1 = {gameStatus},{gameTime}");
            //モーフィング
            if (changeTime < gameTime && !finGame)
            {
                Debug.Log($"GameLoop : send changeTime");
                changeController.morphingChange = true;
                changeController.changeTime = true;
                finGame = true;
            }

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 2;
                Debug.Log($"GameLoop : gameStatus モーフィング終了 = {gameStatus},{gameTime},{finGame} || {buttonStatus} || {resetTime < gameTime}");
                gameTime = 0f;

            }
        }
        else if (gameStatus == 2)
        {
            Debug.Log($"GameLoop : gameStatus 切り替わり = 2 = {gameStatus},{gameTime}");
            if (changeTime < gameTime && !finGame)
            {
                //切り替わり
                changeController.switchChange = true;
                changeController.changeTime = true;
                finGame = true;
            }

            if (buttonStatus || resetTime < gameTime)
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
            if (changeTime < gameTime && !finGame)
            {
                changeController.colorChange = true;
                changeController.changeTime = true;
                finGame = true;
            }

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 4;
                gameTime = 0f;
                changeController.Reset();
            }
        }
        else if (gameStatus == 4)
        {
            //モーフィングx色

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 5;
                gameTime = 0f;
            }
        }
        else if (gameStatus == 5)
        {
            //切り替わりx色

            if ( buttonStatus || resetTime < gameTime)
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

    private void Reset()
    {

    }

    /*
    public ObjectManager objectManager;
    public Animator anim;
    public GameObject usersBoat;
    public GameObject staff;
    public GameObject staffBody;
    public GameObject startPos;
    public GameObject viewingPos;
    public GameObject sun;
    public GameObject water;
    public GameObject canvas;

    public float startTime;
    public float startTimeLimit;
    public float gameTime;
    public float viewingTime;
    public float moveSpeed;
    public float rotationSpeed;
    public float sunSpeed;
    public float waterUpSpead;
    public float waterUpLimit;
    public float canvasChangeSpeed;
    public float canvasChangeStart;

    public Color sunDefultColor;
    public Color sunEndColor;

//    [SerializeField] Material waterDefultMaterial;//(r:f, g:f, b:f, a:255f)
//    [SerializeField] Material waterEndMaterial;//(r:f, g:f, b:f, a:255f)

    [SerializeField] Material staffDefultMaterial;
    [SerializeField] Material staffEndMaterial;

    bool nextPedal;
    bool nowPedaling;
    bool waite;
    int gameStatus;
    int boatStatus;
    float waiteTime;

    void gameStart()
    {
        gameTime = 0f;
        objectManager.gameStart();
    }

    void gameReset()
    {
        //sun.gameObject.SetActive(true);
        Light lt;
        lt = sun.gameObject.GetComponent<Light>();
        lt.color = Color.Lerp(sunEndColor, sunDefultColor, 1f);

        Material[] mats = staffBody.gameObject.GetComponent<Renderer>().materials;
        mats[3] = staffDefultMaterial;
        staffBody.gameObject.GetComponent<Renderer>().materials = mats;

        //water.gameObject.GetComponent<MeshRenderer>().material = waterDefultMaterial;
        //reset water pos to defult pos
        Vector3 waterPos = water.gameObject.transform.position;
        water.gameObject.transform.position = new Vector3(waterPos.x, 18f, waterPos.z);

        //reset sight color
        canvas.GetComponent<Graphic>().color = new Color(0f, 0f, 0f, 0f);

        //reset userboat
        usersBoat.transform.position = startPos.transform.position;

        nextPedal = false;
        nowPedaling = false;
        waite = false;
        gameStatus = 0;
        boatStatus = 0;
        startTime = 0f;
        gameTime = 0f;
        waiteTime = 10f;

        //reset morphing
        objectManager.Reset();
    }

    IEnumerator Waiter()
    {
        //Debug.Log($"GameLoop : start Waiter()");

        yield return new WaitForSeconds(waiteTime);

        if (2 < gameStatus)
            gameStatus = 4;
        else
            gameStatus = 3;
        waite = false;
        //Debug.Log($"GameLoop : fin Waiter");
    }

    IEnumerator leftPedal()
    {
        anim.SetBool("left_pedal", true);

        yield return new WaitForSeconds(5f);

        anim.SetBool("left_pedal", false);
        nowPedaling = false;
    }

    IEnumerator rightPedal()
    {
        anim.SetBool("right_pedal", true);

        yield return new WaitForSeconds(5f);

        anim.SetBool("right_pedal", false);
        nowPedaling = false;
    }
}
*/

}