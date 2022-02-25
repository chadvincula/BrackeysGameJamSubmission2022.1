using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 6f, runSpeed = 9f, gravityMod = 1f, raycastDistance = 10f;
    [SerializeField] private Vector3[] respawnPoints;
    private float _gravity = -9.81f, _moveDirection = 1f;
    private bool _playerSpotted = false, _isStandingStill = false;
    private SpriteRenderer _spriteRenderer = null;
    private CharacterController _body = null;
    private PlayerDetection _playerSensor = null;
    private SanityContoller _sanityContoller;

    public float sanityDamage = -0.2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _body = GetComponent<CharacterController>();
        _playerSensor = GetComponentInChildren<PlayerDetection>();
        _sanityContoller = FindObjectOfType<SanityContoller>();
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
        }
        else if (_body.velocity.x > 0 || transform.rotation.y == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //Moving Right.
            _moveDirection = 1f;
        }
    }

    private IEnumerator Wait(float seconds)
    {
        // Debug.Log("Waiting");
        _isStandingStill = true;
        yield return new WaitForSeconds(seconds);
        _isStandingStill = false;
        FlipEntity();
    }

    private void Walk(float direction)
    {
        var movingVector = new Vector3(direction * walkSpeed, 0f, 0f);
        _body.Move(movingVector * Time.deltaTime);
    }

    private void Run(float direction)
    {
        var movingVector = new Vector3(direction * runSpeed, 0f, 0f);
        _body.Move(movingVector * Time.deltaTime);
    }

    //Return true if still partolling, false if not (player detected)
    private void Patrol(float direction)
    {
        Walk(direction);
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
        float randomNum = Random.Range(0f, (float)respawnPoints.Length);
        int randomIndex = Mathf.FloorToInt(randomNum);
        Debug.Log("Random index: " + randomIndex);
        _body.enabled = false; //Disable to allow warping
        transform.position = respawnPoints[randomIndex];
        _body.enabled = true;
        Debug.Log("Respawn Position: " + respawnPoints[randomIndex] + "\nTransform: " + transform.position);
        StartCoroutine(Wait(1f));
    }

    private void BecomeTranslucent()
    {
        //Set alpha = remaining sanity lvl?
        Color semiTransparent = new Color(1f, 1f, 1f, 0.3f);
        _spriteRenderer.color = semiTransparent;
    }

    private void HitPlayer()
    {
        Debug.Log("PLAYER HIT");
        RespawnAtRandomKeyPosition();
        BecomeTranslucent();
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            HitPlayer();
            _sanityContoller.SetSanity(sanityDamage);
        }
        else
        {
            if((_body.collisionFlags & CollisionFlags.Sides) != 0 && !_isStandingStill)
            {
                Debug.Log("Finna stand still");
                float timeToStandStill = 3f;
                StartCoroutine(Wait(timeToStandStill));
            }
        }
    }
}
