using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    public ChangeController changeController;
    public GameLoop gameLoop;

    public bool gameStart;
    public bool inFocus;
    public bool saw;

    float blendchange;

    void Start()
    {
        //辞書に登録
        changeController.changeObjects.Add(this.gameObject);
        Debug.Log("Changer : Add me to change objects List");

        Reset();

        blendchange = 0f;
        gameStart = true;

        inFocus = false;
        saw = true;
    }

    //look
    void OnTriggerEnter(Collider other)
    {
        if (gameStart)
        {
            saw = true;
            inFocus = false;
        }
    }

    public void MorphingChange()
    {
        //Debug.Log($"Changer : MorphingChange blendchange = {blendchange}");
        //モーフィング変化
        //blendchange += .changeSpeed;
        blendchange += 0.5f;

        SkinnedMeshRenderer skinnedMeshRenderer = this.GetComponent<SkinnedMeshRenderer>();
        if (0f < blendchange && blendchange < 101f)
            skinnedMeshRenderer.SetBlendShapeWeight(0, blendchange);

        if (blendchange >= 100f)
        {
            blendchange = 0f;
            changeController.changeTiming = false;
            changeController.morphingChange = false;
        }
    }

    public void SwitchChange( GameObject target )
    {
        //切り替わり変化
        GameObject newObj = Instantiate(target, this.transform.position, target.transform.rotation);

        newObj.tag = this.gameObject.tag;
        Changer newObj_script = newObj.GetComponent<Changer>();
        newObj_script.changeController = changeController;
        newObj_script.gameLoop = gameLoop;

        changeController.changeTiming = false;
        changeController.switchChange = false;
        this.gameObject.SetActive(false);
    }

    public void ColorChange( Material target )
    {
        //色変化
        this.gameObject.GetComponent<Renderer>().material = target;
        changeController.changeTiming = false;
        changeController.colorChange = false; 
    }

    public void Reset()
    {
        blendchange = 0f;
        gameStart = false;
        inFocus = false;
        saw = false;

        try
        {
            SkinnedMeshRenderer skinnedMeshRenderer = this.gameObject.GetComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
        }
        catch (MissingComponentException e)
        { }
    }
    /*
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
    */
}