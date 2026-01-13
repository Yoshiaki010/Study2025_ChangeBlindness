using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public bool see_change_obj;
    public GameObject target;
    public Camera picture_camera;
    public Ray ray;
    public Ray picture_ray;

    Vector3 hit_pos;

    // Start is called before the first frame update
    void Start()
    {
        see_change_obj = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 0.1f);
        //Debug.Log($"RayController :  Show player ray");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log($"RayController : hit1 !");
            hit_pos = hit.point;
            //Debug.Log($"RayController : player camera hit to obj :  pos (x,y) = ({hit_pos.x}, {hit_pos.y})");
            //Instantiate(target, hit.point, target.transform.rotation);
        }

        //Vector3 left_top_pos = new Vector3(0, 255, 0);//picture x = (0.75 - 3.15), y = (1 - 3)
        picture_ray = picture_camera.ScreenPointToRay(new Vector3((hit_pos.x - 0.75f) * 106.25f, (hit_pos.y - 1f) * 112.5f, 0));
        Debug.DrawRay(picture_ray.origin, picture_ray.direction * 150, Color.red, 0.1f);

        RaycastHit picture_hit;
        if (Physics.Raycast(picture_ray, out picture_hit))
        {
            //Debug.Log($"RayController : hit2 !");
            string hit_tag = picture_hit.collider.gameObject.tag;
            if (hit_tag == "Morph" || hit_tag == "Switch" || hit_tag == "Color")
            {
                see_change_obj = true;
                Debug.Log($"RayController : watch changeObject ! ");
                //Debug.Log($"RayController : picture camera hit to obj = {picture_hit.collider.gameObject.name} ");
            }
            else
                see_change_obj = false;

        }
    }
}