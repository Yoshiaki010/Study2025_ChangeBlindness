using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    public ObjectManager objectManager;
    public GameLoop gameLoop;
    public bool isStuff;
    public bool gameStart;
    public bool inFocus;
    public bool inSight;
    public bool saw;
    public float notFocusTime;
    public int nowLevel;

    GameObject player;
    GameObject viewingPos;
    float playerSightAngle;
    float thisPosAngle;
    float limitTime;
    float viewingAngle;
    float blendchange;

    void Start()
    {
        player = objectManager.player;
        limitTime = objectManager.limitTime;
        viewingAngle = objectManager.viewingAngle;
        viewingPos = gameLoop.viewingPos;
        thisPosAngle = GetThisAngle(this.gameObject.transform);

        //辞書に登録
        objectManager.changeObjList.Add(this.gameObject);

        Reset();
    }

    void Update()
    {
        if (gameStart)
        {
            playerSightAngle = player.transform.localEulerAngles.y;

            float leftMaxRange = GetLeftMaxRange(playerSightAngle);
            float rightMaxRange = GetRightMaxRange(playerSightAngle);

            if (objectManager.viewingAngle < playerSightAngle && playerSightAngle < 360 - objectManager.viewingAngle)
            {
                if (leftMaxRange < thisPosAngle && thisPosAngle < rightMaxRange)
                    inSight = true;
                else
                    inSight = false;
            }
            else
            {
                if (leftMaxRange < thisPosAngle && thisPosAngle < 360f || -1f < thisPosAngle && thisPosAngle < rightMaxRange)
                    inSight = true;
                else
                    inSight = false;
            }

            //start counting the not focus second
            if (saw && !inFocus)
                notFocusTime += Time.deltaTime;
        }
    }

    //look
    void OnTriggerEnter(Collider other)
    {
        if (gameStart)
        {
            saw = true;
            inFocus = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isStuff)
            gameLoop.startTime += Time.deltaTime;
    }

    //not look
    void OnTriggerExit(Collider other)
    {
        if (gameStart)
        {
            inFocus = false;
            notFocusTime = 0f;
        }

        if (isStuff)
            gameLoop.startTime = 0f;
    }

    //切り替わり変化
    public void Change(GameObject target, bool onlyMaterial)
    {
        nowLevel += 1;
        if (onlyMaterial)
        {
            this.gameObject.GetComponent<Renderer>().material = target.GetComponent<Renderer>().material;
        }
        else
        {
            GameObject newObj = Instantiate(target, this.transform.position, Quaternion.identity);
            newObj.tag = this.gameObject.tag;
            Changer newObj_script = newObj.GetComponent<Changer>();
            newObj_script.objectManager = objectManager;
            newObj_script.nowLevel = this.nowLevel;
        }
    }
    
    //モーフィング変化
    public void MorphingChange( GameObject target )
    {
        blendchange += objectManager.changeSpeed;

        SkinnedMeshRenderer skinnedMeshRenderer = target.GetComponent<SkinnedMeshRenderer>();
        if (0f < blendchange && blendchange < 101f)
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendchange);
        
        int maxLevel = objectManager.GetMaxLevel(target.gameObject);
        float nextLevel_blendchange = (100 / maxLevel) * (nowLevel + 1);//get duration
        if (nextLevel_blendchange < blendchange)
        {
            saw = false;
            nowLevel += 1;
            notFocusTime = 0f;
        }
    }

    float GetRightMaxRange(float angle)
    {
        float baseAngle = 0f;

        if (360f < angle + viewingAngle)
        {
            baseAngle = (angle + viewingAngle) - 360f;
        }
        else
        {
            baseAngle = angle + viewingAngle;
        }

        return baseAngle;
    }

    float GetLeftMaxRange(float angle)
    {
        float baseAngle = 0f;

        if (angle - viewingAngle < 0)
        {
            baseAngle = 360f - (viewingAngle - angle);
        }
        else
        {
            baseAngle = angle - viewingAngle;
        }
        return baseAngle;
    }

    float GetThisAngle(Transform target)
    {
        Vector3 playerPos = viewingPos.gameObject.transform.position;
        Vector3 direction = (target.position - playerPos).normalized;
        Vector3 baseDirection = Vector3.forward;

        float angle = Vector3.SignedAngle(baseDirection, direction, Vector3.up); // get angle
        if (angle < 0f)
            angle += 360f;

        return angle;
    }

    public void Reset()
    {
        if (!isStuff)
        {
            nowLevel = 0;
            blendchange = 0f;
            gameStart = false;
            inFocus = false;
            inSight = false;
            saw = false;

            SkinnedMeshRenderer skinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
        }
    }
}