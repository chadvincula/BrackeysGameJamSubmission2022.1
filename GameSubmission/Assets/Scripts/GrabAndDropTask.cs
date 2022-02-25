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
            Debug.Log(myTask.gameObject.activeInHierarchy);
            if(transform.parent.parent == null && myTask.gameObject.activeInHierarchy)
            {
                if(!myTask.CanPerformTask(_sanityContoller))
                {
                    myTask.DisplayInsufficientStaminaMessage();
                    return;
                }
                else
                {
                    completedMessage.SetActive(true);
                    OnDeliveredGoods?.Invoke();
                }
            }
        }
    }
}
