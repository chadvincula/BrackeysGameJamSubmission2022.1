using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 6f, runSpeed = 9f, gravityMod = 1f, raycastDistance = 10f;
    [SerializeField] private Vector3[] respawnPoints;
    private float _gravity = -9.81f, _moveDirection = 1f;
    private bool _playerSpotted = false, _isStandingStill = false;
    private CharacterController _body = null;
    private PlayerDetection _playerSensor = null;

    private void Awake()
    {
        _body = GetComponent<CharacterController>();
        _playerSensor = GetComponentInChildren<PlayerDetection>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _moveDirection = (transform.rotation.y == 0) ? 1f : -1f;
    }

    // Update is called once per frame
    void Update()
    {
        _playerSpotted = _playerSensor.playerDetected;
        if(_playerSpotted)
        {
            Run(_moveDirection);
        }
        else
        {
            if(!_isStandingStill)
                Patrol(_moveDirection);
        }

        //Then we move the body as affected by gravity.
        var fallingVector = new Vector3(0f, _gravity * gravityMod, 0f);
        _body.Move(fallingVector * Time.deltaTime);
        
    }

    private void FlipEntity()
    {
        if (_body.velocity.x < 0 || transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //Moving Left.
            _moveDirection = -1f;
            // Debug.Log("Turn Left");
        }
        else if (_body.velocity.x > 0 || transform.rotation.y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //Moving Right.
            _moveDirection = 1f;
            // Debug.Log("Turn Right");
        }
    }

    private IEnumerator Wait(float seconds)
    {
        // Debug.Log("Waiting");
        _isStandingStill = true;
        yield return new WaitForSeconds(seconds);
        _isStandingStill = false;
        // Debug.Log("Done waiting");
        FlipEntity();
    }

    private void Walk(float direction)
    {
        // Debug.Log("Walking");
        var movingVector = new Vector3(direction * walkSpeed, 0f, 0f);
        _body.Move(movingVector * Time.deltaTime);
    }

    private void Run(float direction)
    {
        // Debug.Log("Running");
        var movingVector = new Vector3(direction * runSpeed, 0f, 0f);
        _body.Move(movingVector * Time.deltaTime);
    }

    //Return true if still partolling, false if not (player detected)
    private void Patrol(float direction)
    {
        Walk(direction);
        //Use box collider to see player
        // if(_playerSensor.playerDetected)
        //     return false;
        // return true;
    }

    //Maybe?
    private void Shout()
    {

    }

    //Call this when chasing the player thru different layers
    private void ChangeLayers()
    {

    }

    //Call this after colliding with the player
    private void RespawnAtRandomKeyPosition()
    {

    }

    private void BecomeTranslucent()
    {
        //Set alpha = remaining sanity lvl?
    }

    private void HitPlayer()
    {
        // Debug.Log("PLAYER HIT");
        RespawnAtRandomKeyPosition();
        BecomeTranslucent();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            HitPlayer();
        }
        //Bumping into anything other than the player forces the Entity
        //to stand still
        else
        {
            Collider myCollider = GetComponent<Collider>();
            foreach (ContactPoint contact in other.contacts)
            {
                // Debug.Log("Contact Point Height: " + contact.point.y + "\nPosition Height: " + myCollider.transform.position.y);
                if(contact.point.y > myCollider.transform.position.y - myCollider.bounds.extents.y / 2)
                {
                    float timeToStandStill = 3f;
                    StartCoroutine(Wait(timeToStandStill));
                    break;
                }
            }

                //Maybe add special behavior when coming across a small box

        }
    }
}
