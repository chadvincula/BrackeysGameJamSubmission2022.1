using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTask : InteractScript
{
    [SerializeField] private TaskScript myTask = null;
    private SanityContoller _sanityContoller = null;
    public GameObject completedMessage = null;
    public static event InteractionFinished OnFinishedInteraction;
    public delegate void InteractionFinished();
    protected override void Awake()
    {
        base.Awake();
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

    protected override void PerformInteraction()
    {
        if(myTask.gameObject.activeInHierarchy)
        {
            if(!myTask.CanPerformTask(_sanityContoller))
            {
                myTask.DisplayInsufficientStaminaMessage();
                return;
            }
            else
            {
                currentTextbox = completedMessage;
                OnFinishedInteraction?.Invoke();
            }
        }
        base.PerformInteraction();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        base.interactableIcon.SetActive(true);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        base.interactableIcon.SetActive(false);
    }
}
