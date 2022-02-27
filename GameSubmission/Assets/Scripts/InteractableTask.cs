using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTask : InteractScript
{
    [SerializeField] private SanityContoller _sanityContoller = null;
    [SerializeField] private TaskScript myTask = null;
    [SerializeField] private InteractScript reward = null;
    public GameObject completedMessage = null;
    public static event InteractionFinished OnFinishedInteraction;
    public delegate void InteractionFinished();

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PerformInteraction()
    {
        if(!textBox.activeInHierarchy && !completedMessage.activeInHierarchy)
        {
            if(myTask != null && myTask.gameObject.activeInHierarchy)
            {
                if(!myTask.CanPerformTask(_sanityContoller))
                {
                    myTask.DisplayInsufficientStaminaMessage();
                    return;
                }
                else
                {
                    currentTextbox = completedMessage;
                    if(reward != null && reward.GetType() == typeof(InteractableTask))
                        reward.enabled = true;
                    OnFinishedInteraction?.Invoke();
                }
            }
            base.PerformInteraction();
            _audioSource.Play();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(myTask != null && myTask.gameObject.activeInHierarchy && myTask.CanPerformTask(_sanityContoller) && reward != null)
        {
            if(reward.GetType() != typeof(InteractableTask))
                reward.enabled = true;
        }
        base.OnTriggerEnter(other);
        base.interactableIcon.SetActive(true);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        base.interactableIcon.SetActive(false);
        if(completedMessage.activeInHierarchy)
            completedMessage.SetActive(false);
        if(reward != null && reward.enabled)
            this.enabled = false;
    }
}
