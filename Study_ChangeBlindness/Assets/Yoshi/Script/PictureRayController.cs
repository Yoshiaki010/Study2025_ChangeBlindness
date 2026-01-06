using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureRayController : MonoBehaviour
{
    public RayController rayController; 
    Vector3 player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        player = rayController.ray;
        Ray picture_ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        Debug.DrawRay(picture_ray.origin, picture_ray.direction * 30, Color.red, 0.1f);
    }
}
