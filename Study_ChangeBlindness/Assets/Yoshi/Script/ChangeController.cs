
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static ChangeController;

public class ChangeController : MonoBehaviour
{
    /*
    public List<ChangeStageDict> MorphingStageDict;
    public List<ChangeStageDict> SwitchStageDict;
    public List<ChangeStageDict> ColorStageDict;
    */

    public List<ChangeStageDict> changeDB;

    public List<GameObject> changeObjects;
    public GameLoop gameLoop;
    public float changeSpeed;

    public bool changeTime;

    /*
    public GameObject player;
    public float viewingAngle;
    public float limitTime;

    float viewingTime;
    */


    // Start is called before the first frame update
    public void Start()
    {
//        viewingTime = gameLoop.viewingTime;
        changeTime = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(changeTime)
        {
            foreach (GameObject obj in changeObjects)
            {
                Changer obj_changer = obj.GetComponent<Changer>();

                bool inFocus = obj_changer.inFocus;
                bool saw = obj_changer.saw;

                if (!inFocus && saw)
                {
                    Debug.Log($"ChangeController : now change");
                    foreach (ChangeStageDict onePattern in changeDB)
                    {
                        if (obj.tag == onePattern.patternName)
                        {
                            GameObject toObj = onePattern.changeStage[0];
                            if (gameLoop.gameStatus == 1)
                                obj_changer.MorphingChange(obj);
                            else if (gameLoop.gameStatus == 2)
                                obj_changer.SwitchChange(toObj);
                            else
                                obj_changer.ColorChange(toObj);
                        }
                    }
                }
            }
        }
    }

    public void MakeChange()
    { 
        changeTime = true;
    }

    public void FinChange()
    {
        changeTime = false;
        gameLoop.finGame = true;
    }
    public void gameStart()
    {
        foreach( GameObject obj in changeObjects)
            obj.GetComponent<Changer>().gameStart = true;
    }

    public void Reset()
    {
        foreach( GameObject obj in changeObjects )
            obj.GetComponent<Changer>().Reset();
    }

    [System.Serializable]
    public class ChangeStageDict
    { 
        public string patternName;
        public List<GameObject> changeStage;
    }
}