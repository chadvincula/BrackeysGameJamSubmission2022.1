using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTask : InteractScript
{
    [SerializeField] private TaskScript myTask = null;
    [SerializeField] private InteractScript reward = null;
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
        if(myTask.gameObject.activeInHierarchy && reward != null)
            reward.enabled = true;
        base.OnTriggerEnter(other);
    }
}
