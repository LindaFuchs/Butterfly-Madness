using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ButterMovement : MonoBehaviour
{
    [SerializeField] float forwardBackSpeed = 5f;
    // [SerializeField] float strafeSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    public bool grounded = false;
    private bool isAlive = true;

    StaminUp staminUp;
    CatMotion catMotion;

    Rigidbody butterRigidbody;
    BoxCollider boxCollider;
    Transform cameraT;

    // Start is called before the first frame update
    void Start()
    {
        butterRigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        cameraT = Camera.main.transform;
        staminUp = FindObjectOfType<StaminUp>();
        catMotion = FindObjectOfType<CatMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (StaminUp.Instance.StaminaRemaining() > 20)
            {
                Fly();
                grounded = false;
                catMotion.StopRunningAnimation_();
                StaminUp.Instance.UseStamina(20);
                Debug.Log("Stamina Used!");
            }
        }
        CallRegen();
    }

    private void Move()
    {
        if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && !grounded)
        {
            DirectionCalc();
            ForwardBackward();
        }
    }

    private void DirectionCalc()
    {
        Vector3 input = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0f);
        Vector3 inputDir = input.normalized;

        if (inputDir != Vector3.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
    }

    private void ForwardBackward()
    {
        transform.Translate(transform.forward * forwardBackSpeed * Time.deltaTime, Space.World);
    }

    private void Fly()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector3 jumpVelocityToAdd = new Vector3(0f, jumpSpeed, 0f);
            butterRigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void OnCollisionEnter(Collision ground)
    {
        if (ground.collider.tag == "Ground")
        {
            grounded = true;
            catMotion.SetRunningAnimation_();
        }
    }

    public void Death()
    {
        isAlive = false;
    }

    public void CallRegen()
    {
        if (grounded)
        {
            staminUp.Regenerate();
        }
        else if (!grounded)
        {
            staminUp.StopRegenerate();
        }
    }
}
