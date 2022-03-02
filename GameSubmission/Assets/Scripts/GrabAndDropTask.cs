using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDropTask : MonoBehaviour
{
    [SerializeField] private SanityContoller _sanityContoller = null;
    [SerializeField] private TaskScript myTask = null;
    [SerializeField] private string roomTag = "";
    private bool _isInsideDestination = false;
    public GameObject completedMessage = null;
    [SerializeField] private float completedMessageDuration = 6f;
    public static event DeliveredGoods OnDeliveredGoods;
    public delegate void DeliveredGoods();

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
        if(other.gameObject.CompareTag(roomTag))
        {
            bool isBeingHeld = transform.parent.TryGetComponent(out GrabScript grabBox);
            bool isHidingThePlayer = transform.parent.TryGetComponent(out Player player);
            if(!isBeingHeld && !isHidingThePlayer)
            {
                if(myTask != null && myTask.gameObject.activeInHierarchy)
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
