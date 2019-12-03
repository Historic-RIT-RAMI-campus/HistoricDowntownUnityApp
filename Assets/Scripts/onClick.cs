using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClick : MonoBehaviour
{
    int noClick = 0;
    public GameObject[] objList;
    
    public void btnClick()
    {
        print("in function");
       if(objList[noClick].activeSelf)
        {
            objList[noClick].SetActive(false);
            noClick++;

            if (noClick >= objList.Length)
            {
                noClick = 0;
            }
            objList[noClick].SetActive(true);
        }
    }
}
