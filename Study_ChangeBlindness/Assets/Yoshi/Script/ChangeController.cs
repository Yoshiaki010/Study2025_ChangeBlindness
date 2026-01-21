
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChangeController;

public class ChangeController : MonoBehaviour
{
    public List<ChangeStageDict> changeDB;
    public List<GameObject> changeObjects;

    public GameLoop gameLoop;
    public float changeSpeed;

    public bool changeTiming;
    public bool morphingChange;
    public bool switchChange;
    public bool colorChange;

    // Start is called before the first frame update
    public void Start()
    {
//        viewingTime = gameLoop.viewingTime;
        changeTiming = false;
    }

    // Update is called once per frame
    public void Update()
    {
        if(changeTiming)
        {
            foreach (GameObject obj in changeObjects)
            {
                Changer obj_changer = obj.GetComponent<Changer>();

                bool inFocus = obj_changer.inFocus;
                bool saw = obj_changer.saw;
                saw = true;

                if (!inFocus && saw)
                {
                    Debug.Log($"ChangeController : send the signal to changer");
                    foreach (ChangeStageDict onePattern in changeDB)
                    {
                        if (obj.tag == onePattern.patternName)
                        {
                            if (morphingChange)
                                obj_changer.MorphingChange();
                            if (switchChange)
                            {
                                Debug.Log("No");
                                obj_changer.SwitchChange(onePattern.switchStage[0]);
                                Debug.Log("Yes");
                                Destroy(obj);
                            }
                            if (colorChange)
                            {
                                obj_changer.ColorChange(onePattern.colorStage[0]);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Reset()
    {
        if (changeObjects != null)
        {
            foreach (GameObject obj in changeObjects)
                Destroy(obj);
        }
    }

    [System.Serializable]
    public class ChangeStageDict
    { 
        public string patternName;
        public List<GameObject> switchStage;
        public List<Material> colorStage;
    }
}