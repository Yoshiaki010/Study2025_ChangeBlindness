
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<ChangeStageDict> changeStageDict;
    public List<GameObject> changeObjList;

    public GameLoop gameLoop;

    public GameObject player;
    public float viewingAngle;
    public float limitTime;
    public float changeSpeed;

    float viewingTime;

    // Start is called before the first frame update
    public void Start()
    {
        viewingTime = gameLoop.viewingTime;
    }

    // Update is called once per frame
    public void Update()
    {
        foreach(GameObject obj in changeObjList)
        {
            Changer obj_changer = obj.GetComponent<Changer>();

            int nowLevel = obj_changer.nowLevel;
            float notFocusTime = obj_changer.notFocusTime;
            bool inFocus = obj_changer.inFocus;
            bool inSight = obj_changer.inSight;
            bool saw = obj_changer.saw;
            //Debug.Log($"ObjectManager {obj} : !{inFocus} && {inSight} && 0 < {saw}, {limitTime} < {notFocusTime}");
            if (!inFocus && inSight && saw && limitTime < notFocusTime)
            {
                foreach (ChangeStageDict onePattern in changeStageDict)
                {
                    if (obj.tag == onePattern.patternName && nowLevel < onePattern.maxLevel)
                    {
                        obj_changer.MorphingChange(obj);
                    }
                }
            }
            else
            {
                foreach (ChangeStageDict onePattern in changeStageDict)
                {
                    if (obj.tag == onePattern.patternName)
                    {
                        if (viewingTime / onePattern.maxLevel < gameLoop.gameTime)
                        {
                            obj_changer.saw = true;
                            obj_changer.notFocusTime += limitTime + 1f;
                        }
                    }
                }

            }
        }
    }

    public int GetMaxLevel(GameObject target)
    {
        int maxLevel = 0;
        foreach(ChangeStageDict onePattern in changeStageDict)
        {
            if(target.tag == onePattern.patternName)
                maxLevel = onePattern.maxLevel;
        }
        return maxLevel;
    }

    public void gameStart()
    {
        foreach( GameObject obj in changeObjList)
        {
            Changer obj_changer = obj.GetComponent<Changer>();
            obj_changer.gameStart = true;
        }
    }

    public void Reset()
    {
        foreach( GameObject obj in changeObjList )
        {
            Changer obj_changer = obj.GetComponent<Changer>();
            obj_changer.Reset();
        }
    }

    [System.Serializable]//System.Serializable SerializeField
    public class ChangeStageDict
    { 
        public string patternName;
        public int maxLevel;
        public List<bool> onlyMaterial;
        public List<GameObject> objectes;
    }
}
