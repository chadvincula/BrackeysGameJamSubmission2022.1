using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskScript : MonoBehaviour
{
    [SerializeField] private float sanityCost = -0.4f;
    [SerializeField] private GameObject shortStaminaTextbox = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Finish(SanityContoller sanityContoller)
    {
        Debug.Log(sanityContoller.GetSanity() + sanityCost);
        if(sanityContoller.GetSanity() + sanityCost >= 0f)
            sanityContoller.SetSanity(sanityCost);
        else
        {
            shortStaminaTextbox.SetActive(true);
            StartCoroutine(DisableAlert(3f));
        }
        return !shortStaminaTextbox.activeInHierarchy;
    }

    private IEnumerator DisableAlert(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shortStaminaTextbox.SetActive(false);
    }
}
