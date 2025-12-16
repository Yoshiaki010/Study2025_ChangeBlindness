using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public List<Material> Images;
    public List<GameObject> StartObjects;
    public ChangeController changeController;
    public bool buttonStatus;
    public bool finGame;
    public float resetTime;
    public float changeTime;
    public int gameStatus;

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
            Debug.Log($"GameLoop : gameStatus waiting = 0");
            if (buttonStatus || true)
            {
                gameStatus = 1;
                gameTime = 0f;
                Reset();
            }
        }
        else if (gameStatus == 1)
        {
            Debug.Log($"GameLoop : モーフィング");
            //モーフィング
            if (changeTime < gameTime && !finGame)
            {
                Debug.Log($"GameLoop : モーフィング中");
                changeController.morphingChange = true;
                changeController.changeTiming = true;
                finGame = true;
            }

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 2;
                gameTime = 0f;
                Debug.Log($"GameLoop : モーフィング終了");
                Reset();
            }
        }
        else if (gameStatus == 2)
        {
            Debug.Log($"GameLoop : 切り替わり");
            if (changeTime < gameTime && !finGame)
            {
                //切り替わり
                changeController.switchChange = true;
                changeController.changeTiming = true;
                finGame = true;
                Debug.Log($"GameLoop : 切り替われ");
            }

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 3;
                gameTime = 0f;
                Debug.Log($"GameLoop : 切り替わり終了");
                Reset();
            }
        }
        else if (gameStatus == 3)
        {
            Debug.Log($"GameLoop : 色");
            //色
            if (changeTime < gameTime && !finGame)
            {
                changeController.colorChange = true;
                changeController.changeTiming = true;
                Debug.Log($"GameLoop : 色変化中");
                finGame = true;
            }

            if (buttonStatus || resetTime < gameTime)
            {
                gameStatus = 1;
                gameTime = 0f;
                Reset();
                Debug.Log($"GameLoop : 色終了");
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
        finGame = false;
        changeController.Reset();
        changeController.changeObjects.Clear();
        GameObject obj = Instantiate(StartObjects[gameStatus - 1], StartObjects[gameStatus - 1].transform.position, StartObjects[gameStatus - 1].transform.rotation);
        Changer obj_changer = obj.GetComponent<Changer>();
        obj_changer.gameLoop = this;
        obj_changer.changeController = changeController;
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