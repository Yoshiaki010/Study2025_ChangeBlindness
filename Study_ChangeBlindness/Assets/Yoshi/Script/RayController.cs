using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public bool see_change_obj;
    public GameObject target;
    public GameObject player_leftGaze;
    public GameObject player_rightGaze;
    public GameObject pc_camera;
    public Camera picture_camera;

    public Ray ray;
    public Ray picture_ray;
    public Ray ray_left;
    public Ray ray_right;
    public Ray picture_ray_left;
    public Ray picture_ray_right;

    Vector3 hit_pos;
    Vector3 hit_leftGaze;
    Vector3 hit_rightGaze;

    // Start is called before the first frame update
    void Start()
    {
        see_change_obj = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray_left = new Ray(player_leftGaze.transform.position, player_leftGaze.transform.forward);
        Debug.DrawRay(ray_left.origin, ray_left.direction * 10, Color.red, 0.1f);
        RaycastHit hit_left;
        if (Physics.Raycast(ray_left, out hit_left))
            hit_leftGaze = hit_left.point;
        picture_ray_left = picture_camera.ScreenPointToRay(new Vector3((hit_leftGaze.x - 0.75f) * 106.25f, (hit_leftGaze.y - 1f) * 112.5f, 0));
        RaycastHit hit_pic_l;
        if (Physics.Raycast(picture_ray_left, out hit_pic_l))
        {
            string hit_tag = hit_pic_l.collider.gameObject.tag;
            if (hit_tag == "Morph" || hit_tag == "Switch" || hit_tag == "Color")
                see_change_obj = true;
            else
                see_change_obj = false;
        }

        ray_right = new Ray(player_rightGaze.transform.position, player_rightGaze.transform.forward);
        RaycastHit hit_right;
        if (Physics.Raycast(ray_right, out hit_right))
            hit_rightGaze = hit_right.point;
        picture_ray_right = picture_camera.ScreenPointToRay(new Vector3((hit_rightGaze.x - 0.75f) * 106.25f, (hit_rightGaze.y - 1f) * 112.5f, 0));
        RaycastHit hit_pic_r;
        if (Physics.Raycast(picture_ray_right, out hit_pic_r))
        {
            string hit_tag = hit_pic_r.collider.gameObject.tag;
            if (hit_tag == "Morph" || hit_tag == "Switch" || hit_tag == "Color")
                see_change_obj = true;
            else
                see_change_obj = false;
        }

        /*
         * PC
        ray = new Ray(pc_camera.transform.position, pc_camera.transform.forward);
        Debug.DrawRay(ray.origin, picture_ray.direction * 10, Color.red, 0.1f);

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
        */
    }
}