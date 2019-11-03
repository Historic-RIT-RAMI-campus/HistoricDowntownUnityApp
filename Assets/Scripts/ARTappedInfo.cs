using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTappedInfo : MonoBehaviour
{
    public float increaseYby = 1;
    public GameObject infoPlane;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(infoPlane, new Vector3(transform.position.x, increaseYby, transform.position.z), Quaternion.identity);
        }
    }
}
