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

    public bool CanPerformTask(SanityContoller sanityContoller)
    {
        Debug.Log(sanityContoller.GetSanity() + sanityCost);
        return sanityContoller.GetSanity() + sanityCost >= 0f;
    }

    public void DisplayInsufficientStaminaMessage()
    {
        shortStaminaTextbox.SetActive(true);
        StartCoroutine(DisableAlert(3f));
    }

    public void DisplayInsufficientStaminaMessage(float seconds)
    {
        shortStaminaTextbox.SetActive(true);
        StartCoroutine(DisableAlert(seconds));
    }

    public bool Finish(SanityContoller sanityContoller)
    {
        if(CanPerformTask(sanityContoller))
            sanityContoller.SetSanity(sanityCost);
        else
        {
            DisplayInsufficientStaminaMessage();
        }
        return !shortStaminaTextbox.activeInHierarchy;
    }

    private IEnumerator DisableAlert(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        shortStaminaTextbox.SetActive(false);
    }
}
