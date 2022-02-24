using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ListOfTasks : MonoBehaviour
{
    [SerializeField] float endOfDayFourThreshold = 0.4f;
    private bool _isDeletingATask = false;
    private List<RectTransform> myTasks = new List<RectTransform>();
    private SanityContoller _sanityContoller;
    private PlayerControls _playerControls;

    private void Awake()
    {
        InitializeTasks();
        _sanityContoller = FindObjectOfType<SanityContoller>();
        #if UNITY_EDITOR // For dev shortcuts
            _playerControls = new PlayerControls();
        #endif

    }

    private void OnEnable()
    {
        InteractableTask.OnFinishedInteraction += CompleteCurrentTask;
        #if UNITY_EDITOR // for devv shortcuts
            _playerControls.DevShortcuts.Enable();
            _playerControls.DevShortcuts.CompleteTaskOne.performed += CompleteTaskOne;
        #endif
    }

    private void OnDisable()
    {
        InteractableTask.OnFinishedInteraction -= CompleteCurrentTask;
        #if UNITY_EDITOR // for devv shortcuts
            _playerControls.DevShortcuts.Disable();
            _playerControls.DevShortcuts.CompleteTaskOne.performed -= CompleteTaskOne;
        #endif
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeTasks()
    {
        // Get all children rect transforms, excluding parent
        for(int i = 0; i < transform.childCount; i++)
        {
            myTasks.Add(transform.GetChild(i).GetComponent<RectTransform>());
            Debug.Log(myTasks[i].gameObject.name);
        }
        SetTaskPositions();
    }

    private void SetTaskPositions()
    {
        // Set an appropriate position
        if(myTasks.Count > 0)
        {
            if(!myTasks[0].gameObject.activeInHierarchy)
            {
                myTasks[0].gameObject.SetActive(true);
                myTasks[0].anchoredPosition = new Vector2(-75, -5);
            }
            for(int i = 1; i < myTasks.Count; i++)
            {
                myTasks[i].gameObject.SetActive(false);
            }
        }
    }

    public void CompleteCurrentTask()
    {if(myTasks.Count > 0 && !_isDeletingATask)
        {
            StartCoroutine(DeleteTask(0));
        }
    }
    public void CompleteTask(int index)
    {
        if(myTasks.Count > index && !_isDeletingATask)
        {
            StartCoroutine(DeleteTask(index));
        }
    }

    public IEnumerator DeleteTask(int index)
    {
        TaskScript currentTask = myTasks[index].GetComponent<TaskScript>();
        if(currentTask.Finish(_sanityContoller))
        {
            Animation taskAnimation = myTasks[index].GetComponent<Animation>();
            Debug.Log(taskAnimation.Play());
            _isDeletingATask = true;
            while(taskAnimation.isPlaying)
                yield return null;
            myTasks[index].gameObject.SetActive(false);
            myTasks.RemoveAt(index);
            SetTaskPositions();
            _isDeletingATask = false;

            if(myTasks.Count <= 0)
            {
                if(gameObject.scene.name == "Day4")
                {
                    if(_sanityContoller.GetSanity() < endOfDayFourThreshold)
                        _sanityContoller.ResetToDayOne();
                }
                else
                    _sanityContoller.ProceedToNextDay();
            }

        }
    }

    public void CompleteTaskOne(InputAction.CallbackContext context)
    {
        CompleteTask(0);
    }
}
