using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityScript : MonoBehaviour
{
    public GameObject[] brObject;

    //Entering Layers
    public void EnterLayer2()
    {
        brObject[1].SetActive(true);
        brObject[0].SetActive(false);
    }
    
    public void EnterLayer3()
    {
        brObject[2].SetActive(true);
        brObject[1].SetActive(false);
    }
    
    public void EnterLayer4()
    {
        brObject[3].SetActive(true);
        brObject[2].SetActive(false);
    }
    
    //Leaving Layers
    public void LeaveLayer2()
    {
        brObject[1].SetActive(false);
        brObject[0].SetActive(true);
    }
    
    public void LeaveLayer3()
    {
        brObject[2].SetActive(false);
        brObject[1].SetActive(true);
    }
    
    public void LeaveLayer4()
    {
        brObject[3].SetActive(false);
        brObject[2].SetActive(true);
    }
}
