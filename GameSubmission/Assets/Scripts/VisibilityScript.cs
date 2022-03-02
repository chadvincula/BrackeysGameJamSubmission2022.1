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

    public void ObjectEnterLayer2(Transform obj)
    {
        SendObjectToActiveLayer(obj, 1);
        brObject[1].SetActive(true);
        brObject[0].SetActive(false);
    }
    
    public void ObjectEnterLayer3(Transform obj)
    {
        SendObjectToActiveLayer(obj, 2);
        brObject[2].SetActive(true);
        brObject[1].SetActive(false);
    }
    
    public void ObjectEnterLayer4(Transform obj)
    {
        SendObjectToActiveLayer(obj, 3);
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

    public void ObjectLeaveLayer2(Transform obj)
    {
        SendObjectToActiveLayer(obj, 0);
        brObject[1].SetActive(false);
        brObject[0].SetActive(true);
    }
    
    public void ObjectLeaveLayer3(Transform obj)
    {
        SendObjectToActiveLayer(obj, 1);
        brObject[2].SetActive(false);
        brObject[1].SetActive(true);
    }
    
    public void ObjectLeaveLayer4(Transform obj)
    {
        SendObjectToActiveLayer(obj, 2);
        brObject[3].SetActive(false);
        brObject[2].SetActive(true);
    }

    private void SendObjectToActiveLayer(Transform obj, int layerIndex)
    {
        obj.parent = brObject[layerIndex].transform;
        Vector3 tempPosition = obj.localPosition;
        tempPosition.z = -1;
        obj.localPosition = tempPosition;
    }
}
