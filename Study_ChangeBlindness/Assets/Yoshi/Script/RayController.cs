using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class RayController : MonoBehaviour
{
    public GameObject target;

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
            Vector3 pos = hit.point;
            Debug.Log($"ray = {ray}, hit = {hit} , hit pos = {pos.x}, {pos.y}, {pos.z}");
            //            Instantiate(target, hit.point, target.transform.rotation);
        }
    }
}
