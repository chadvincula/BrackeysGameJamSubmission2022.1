using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDropTask : MonoBehaviour
{
    [SerializeField] private TaskScript myTask = null;
    [SerializeField] private string roomTag = "";
    private bool _isInsideDestination = false;
    private SanityContoller _sanityContoller = null;
    public GameObject completedMessage = null;
    [SerializeField] private float completedMessageDuration = 6f;
    public static event DeliveredGoods OnDeliveredGoods;
    public delegate void DeliveredGoods();

    private void Awake()
    {
        _sanityContoller = FindObjectOfType<SanityContoller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == roomTag)
        {
            if(transform.parent.parent == null)
            {
                if(myTask.gameObject.activeInHierarchy)
                {
                    if(!completedMessage.activeInHierarchy && !myTask.CanPerformTask(_sanityContoller))
                    {
                        myTask.DisplayInsufficientStaminaMessage(completedMessageDuration);
                        return;
                    }
                    else
                    {
                        completedMessage.SetActive(true);
                        OnDeliveredGoods?.Invoke();
                    }
                }
                else
                {
                    if(completedMessage.activeInHierarchy)
                    {
                        completedMessage.SetActive(false);
                    }
                }
            }
        }
    }
}
