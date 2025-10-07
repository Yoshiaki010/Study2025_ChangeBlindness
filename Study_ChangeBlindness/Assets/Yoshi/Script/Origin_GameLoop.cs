using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class GameLoop : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        //DefultColor = sun.gameObject.GetComponent<Light>().color;
        //EndColor = Color.red;

        gameStatus = 0;
        startTime = 0f;
        gameReset();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log($"GameLoop : gameStatus = {gameStatus}");
         */

        if (gameStatus == 0)
        {
            if (startTimeLimit < startTime)
            {
                gameStatus = 1;
            }
        }
        else if (gameStatus == 1)
        {
            if (boatStatus == 0)
            {
                if (-0.3f < usersBoat.transform.rotation.y)
                {
                    anim.SetBool("right_pedal", true);
                    usersBoat.transform.Rotate(0f, -rotationSpeed, 0f);
                }
                else
                {
                    anim.SetBool("right_pedal", false);
                    nextPedal = true;
                    boatStatus = 1;
                }
            }
            else if (boatStatus == 1)
            {
                if (nowPedaling)
                {
                    /*
                    if(nextPedal)
                    {
                        usersBoat.transform.Rotate(0f, -rotationSpeed, 0f);
                    }
                    else
                    {
                        usersBoat.transform.Rotate(0f, rotationSpeed, 0f);
                    }
                    */
                }
                else
                {
                    if (nextPedal)
                    {
                        StartCoroutine("leftPedal");
                        nextPedal = false;
                    }
                    else
                    {
                        StartCoroutine("rightPedal");
                        nextPedal = true;
                    }
                    nowPedaling = true;
                    /*
                    */
                }

                //userBoat is moveing to viewingpos
                Vector3 current = usersBoat.transform.position;
                Vector3 target = viewingPos.transform.position;
                if (current != target)
                {
                    float step = moveSpeed * Time.deltaTime;
                    usersBoat.transform.position = Vector3.MoveTowards(current, target, step);
                }
                else
                {
                    if (nowPedaling!)
                    {
                        anim.SetBool("left_pedal", false);
                        anim.SetBool("right_pedal", false);
                        boatStatus = 2;
                    }
                }

            }
            else if(boatStatus == 2)
            {
                if (usersBoat.transform.rotation.y < 0f)
                {
                    anim.SetBool("left_pedal", true);
                    usersBoat.transform.Rotate(0f, rotationSpeed, 0f);
                }
                else
                {
                    anim.SetBool("left_pedal", false);
                    boatStatus = 3;
                }
            }
            else
            {
                gameStart();
                gameStatus = 2;
            }

        }
        else if (gameStatus == 2)
        {
            gameTime += Time.deltaTime;

            // change light color
            Light lt;
            lt = sun.gameObject.GetComponent<Light>();
            Color sunNowColor = lt.color;
            lt.color = Color.Lerp(sunNowColor, sunEndColor, sunSpeed);//change here

            if (viewingTime < gameTime)
            {
                // change staff cloth
                Material[] mats = staffBody.gameObject.GetComponent<Renderer>().materials;
                mats[3] = staffEndMaterial;//change here
                staffBody.gameObject.GetComponent<Renderer>().materials = mats;//applay

                //change water color
                //water.gameObject.GetComponent<MeshRenderer>().material = waterEndMaterial;
                if (!waite)
                {
                    waiteTime = 10f;
                    StartCoroutine("Waiter");
                    waite = true;
                }
            }
        }
        else if (gameStatus == 3)
        {
            //up the water
            Vector3 waterPos = water.gameObject.transform.position;//water up pos
            Color canvasColor = canvas.GetComponent<Graphic>().color;
            if (waterPos.y < objectManager.player.transform.position.y - waterUpLimit)
            {
                water.gameObject.transform.position = new Vector3(waterPos.x, waterPos.y + (waterUpSpead * Time.deltaTime), waterPos.z);//up here
                if (canvasColor.a < 2f && objectManager.player.transform.position.y - (canvasChangeStart + waterUpLimit) < waterPos.y)//in Water
                {
                    canvasColor.a += (1f / canvasChangeSpeed) * Time.deltaTime;
                    canvas.GetComponent<Graphic>().color = new Color(0f, 0f, 0f, canvasColor.a);
                }
            }
            else if (!waite)
            {
                waiteTime = 5f;
                StartCoroutine("Waiter");
                waite = true;
            }
            else
            {
                //Debug.Log("GameLoop : waite");
            }
        }
        else if (gameStatus == 4)
        {
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameReset();
            gameStatus = 0;
        }
        else
        {
            Debug.Log($"GameLoop : no status {gameStatus}");
        }
    }

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