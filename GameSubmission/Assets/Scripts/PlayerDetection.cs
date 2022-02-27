using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public bool playerDetected { get; private set; } = false;

    private AudioSource _aggroSounds;
    // Start is called before the first frame update
    void Start()
    {
        _aggroSounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            Debug.Log(gameObject.name + " DETECTED PLAYER");
            playerDetected = true;
            _aggroSounds.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            Debug.Log(gameObject.name + " UNDETECTING PLAYER");
            playerDetected = false;
        }
    }
}
