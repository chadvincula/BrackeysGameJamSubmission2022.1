using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ListOfTasks : MonoBehaviour
{
    private List<RectTransform> myTasks = new List<RectTransform>();
    private PlayerControls _playerControls;

    private void Awake()
    {
        InitializeTasks();
        #if UNITY_EDITOR // For dev shortcuts
            _playerControls = new PlayerControls();
        #endif

    }

    #if UNITY_EDITOR // for devv shortcuts
        private void OnEnable()
        {
            _playerControls.DevShortcuts.Enable();
            _playerControls.DevShortcuts.CompleteTaskOne.performed += CompleteTaskOne;
            _playerControls.DevShortcuts.CompleteTaskTwo.performed += CompleteTaskTwo;
            _playerControls.DevShortcuts.CompleteTaskThree.performed += CompleteTaskThree;
            _playerControls.DevShortcuts.CompleteTaskFour.performed += CompleteTaskFour;
            _playerControls.DevShortcuts.CompleteTaskFive.performed += CompleteTaskFive;
        }

        private void OnDisable()
        {
            _playerControls.DevShortcuts.Disable();
            _playerControls.DevShortcuts.CompleteTaskOne.performed -= CompleteTaskOne;
            _playerControls.DevShortcuts.CompleteTaskTwo.performed -= CompleteTaskTwo;
            _playerControls.DevShortcuts.CompleteTaskThree.performed -= CompleteTaskThree;
            _playerControls.DevShortcuts.CompleteTaskFour.performed -= CompleteTaskFour;
            _playerControls.DevShortcuts.CompleteTaskFive.performed -= CompleteTaskFive;
        }
    #endif
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
        for(int i = 0; i < myTasks.Count; i++)
        {
            myTasks[i].anchoredPosition = new Vector2(-75 + 190 * i, -5);
        }
    }

    public void CompleteTask(int index)
    {
        if(myTasks.Count > index)
        {
            Destroy(myTasks[index].gameObject);
            myTasks.RemoveAt(index);
            SetTaskPositions();
        }
    }

    public void CompleteTaskOne(InputAction.CallbackContext context)
    {
        CompleteTask(0);
    }

    public void CompleteTaskTwo(InputAction.CallbackContext context)
    {
        CompleteTask(1);
    }

    public void CompleteTaskThree(InputAction.CallbackContext context)
    {
        CompleteTask(2);
    }

    public void CompleteTaskFour(InputAction.CallbackContext context)
    {
        CompleteTask(3);
    }

    public void CompleteTaskFive(InputAction.CallbackContext context)
    {
        CompleteTask(4);
    }
}
