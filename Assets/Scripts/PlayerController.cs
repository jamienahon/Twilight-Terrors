using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float defaultMovementSpeed;
    float movementSpeed;
    public float aimSpeed;
    public float sprintSpeed;

    public Slider sprintMeter;
    float sprintValue;
    public float sprintDecay;
    public float sprintRegenDelay;
    float startSprintRegen;

    bool isSprinting = false;
    bool canSprint = true;
    bool isAiming = false;

    public float timeBetweenShots;
    float nextShot;

    LineRenderer aimLine;
    public Vector2 mousePos;

    public float rotationZ;

    public Sprite[] sprites;
    SpriteRenderer sr;

    void Start()
    {
        aimLine = GetComponent<LineRenderer>();
        aimLine.enabled = false;
        movementSpeed = defaultMovementSpeed;
        sprintValue = sprintMeter.maxValue;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RenderAimLine();
        HandlePlayerInputs();
        ChangePlayerMovementState();
        HandleSprintMeter();
        HandleSpriteChanges();

        sprintMeter.value = sprintValue;
    }

    void ChangePlayerMovementState()
    {
        if (isSprinting && canSprint)
        {
            movementSpeed = sprintSpeed;
            aimLine.enabled = false;
        }
        else if (isAiming)
        {
            movementSpeed = aimSpeed;
            aimLine.enabled = true;
        }
        else
        {
            movementSpeed = defaultMovementSpeed;
            aimLine.enabled = false;
        }
    }

    void HandleSprintMeter()
    {
        if (isSprinting && canSprint)
        {
            sprintValue -= sprintDecay * Time.deltaTime;
            if (sprintValue <= 0)
            {
                canSprint = false;
            }
        }
        if (Time.time >= startSprintRegen && sprintValue != sprintMeter.maxValue && !isSprinting)

        {
            canSprint = true;
            if (sprintValue + sprintDecay * Time.deltaTime > sprintMeter.maxValue)
                sprintValue = sprintMeter.maxValue;
            else
                sprintValue += sprintDecay * Time.deltaTime;
        }
    }

    void HandlePlayerInputs()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            moveDirection.y = 1;
        else if (Input.GetKey(KeyCode.S))
            moveDirection.y = -1;

        if (Input.GetKey(KeyCode.A))
            moveDirection.x = -1;
        else if (Input.GetKey(KeyCode.D))
            moveDirection.x = 1;

        transform.Translate(moveDirection.normalized * movementSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            isSprinting = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            startSprintRegen = Time.time + sprintRegenDelay;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            isAiming = true;
            isSprinting = false;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            isAiming = false;
            if (Input.GetKey(KeyCode.LeftShift))
                isSprinting = true;
        }
    }

    void RenderAimLine()
    {
        aimLine.SetPosition(0, (Vector2)transform.position);
        aimLine.SetPosition(1, (mousePos - (Vector2)transform.position).normalized * 1000);
    }

    void HandleSpriteChanges()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 difference = worldPoint - transform.position;
        difference.Normalize();

        rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (isAiming)
        {
            if (rotationZ < 135 && rotationZ > 45)
                sr.sprite = sprites[3];
            else if (rotationZ > -135 && rotationZ < -45)
                sr.sprite = sprites[5];
            else if (rotationZ < 45 && rotationZ > -45)
            {
                sr.sprite = sprites[4];
                sr.flipX = false;
            }
            else
            {
                sr.sprite = sprites[4];
                sr.flipX = true;
            }
        }
        else
        {
            if (rotationZ < 135 && rotationZ > 45)
                sr.sprite = sprites[0];
            else if (rotationZ > -135 && rotationZ < -45)
                sr.sprite = sprites[2];
            else if (rotationZ < 45 && rotationZ > -45)
            {
                sr.sprite = sprites[1];
                sr.flipX = true;
            }
            else
            {
                sr.sprite = sprites[1];
                sr.flipX = false;
            }
        }
    }
}
