using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIVE.OpenXR;
using VIVE.OpenXR.EyeTracker;

public class UpdateOutGazes : MonoBehaviour
{
    public GameObject rightGazeObj;
    public GameObject leftGazeObj;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        XR_HTC_eye_tracker.Interop.GetEyeGazeData(out XrSingleEyeGazeDataHTC[] out_gazes);
        XrSingleEyeGazeDataHTC leftGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_LEFT_HTC];
        if(leftGaze.isValid)
        {
//            leftGazeObj.transform.position = leftGaze.gazePose.position.ToUnityVector();
            leftGazeObj.transform.rotation = leftGaze.gazePose.orientation.ToUnityQuaternion();
        }

        XrSingleEyeGazeDataHTC rightGaze = out_gazes[(int)XrEyePositionHTC.XR_EYE_POSITION_RIGHT_HTC];
        if (rightGaze.isValid)
        {
//            rightGazeObj.transform.position = rightGaze.gazePose.position.ToUnityVector();
            rightGazeObj.transform.rotation = rightGaze.gazePose.orientation.ToUnityQuaternion();
        }

        leftGazeObj.transform.position = mainCamera.transform.position;
        rightGazeObj.transform.position = mainCamera.transform.position;
    }

}