using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public GameObject target;
    public GameObject picture_camera;
    public Ray ray;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.red, 0.1f);
        Debug.Log($"RayController :  show");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hit_pos = hit.point;
            Ray picture_ray = picture_camera.ScreenPointToRay(hit_pos);
            Debug.DrawRay(picture_ray.origin, ray.direction * 30, Color.red, 0.1f);
            Debug.Log($"ray = {picture_ray} , hit pos x,y = {hit_pos.x, hit_pos.y}");
//            Instantiate(target, hit.point, target.transform.rotation);
        }
    }
}
