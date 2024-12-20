using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class PlayerController : MonoBehaviour
{
    public float playerHealth = 100;

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
    public float reloadTime;
    float finishedReloading;
    public int bulletsInMag;
    public int bulletsPerMag;
    public int totalBullets;
    public bool canShoot = true;
    public int damage;

    LineRenderer aimLine;
    public Vector2 mousePos;

    public float rotationZ;

    public Sprite[] sprites;
    SpriteRenderer sr;

    public ParticleSystem gunShotParticles;

    public TextMeshProUGUI bulletsInMagText;
    public TextMeshProUGUI totalBulletsText;

    public ParticleSystem bloodEffect;

    Animator anim;
    public Animator parentAnim;

    float animSpeed;

    void Start()
    {
        aimLine = GetComponent<LineRenderer>();
        aimLine.enabled = false;
        movementSpeed = defaultMovementSpeed;
        sprintValue = sprintMeter.maxValue;
        sr = GetComponent<SpriteRenderer>();
        bulletsInMag = bulletsPerMag;
        anim = GetComponent<Animator>();
        animSpeed = parentAnim.speed;
    }

    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("side recoil anim")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("front recoil anim"))
        {
            anim.enabled = false;
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RenderAimLine();
        HandlePlayerInputs();
        ChangePlayerMovementState();
        HandleSprintMeter();
        HandleSpriteChanges();
        CheckIfCanShoot();

        sprintMeter.value = sprintValue;
        bulletsInMagText.text = bulletsInMag.ToString();
        totalBulletsText.text = totalBullets.ToString();
    }

    void ChangePlayerMovementState()
    {
        if (isSprinting && canSprint)
        {
            parentAnim.speed = animSpeed * 3;
            movementSpeed = sprintSpeed;
            aimLine.enabled = false;
        }
        else if (isAiming)
        {
            parentAnim.speed = animSpeed / 2;
            movementSpeed = aimSpeed;
            aimLine.enabled = true;
        }
        else
        {
            parentAnim.speed = animSpeed;
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
                parentAnim.speed = animSpeed;
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

        transform.parent.Translate(moveDirection.normalized * movementSpeed * Time.deltaTime);

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

        if (Input.GetKeyDown(KeyCode.Mouse0) && isAiming)
            Shoot();

        if (Input.GetKeyDown(KeyCode.R) && canShoot && bulletsInMag < bulletsPerMag)
        {
            canShoot = false;
            finishedReloading = Time.time + reloadTime;
        }
    }

    void RenderAimLine()
    {
        aimLine.SetPosition(0, (Vector2)transform.position);
        aimLine.SetPosition(1, (mousePos - (Vector2)transform.position).normalized * 1000);
    }

    void Shoot()
    {
        if (Time.time >= nextShot && canShoot && bulletsInMag > 0)
        {
            anim.enabled = true;
            if (rotationZ < 135 && rotationZ > 45)
                anim.enabled = false;
            else if (rotationZ > -135 && rotationZ < -45)
                anim.Play("front recoil anim");
            else
                anim.Play("side recoil anim");

            Instantiate(gunShotParticles, DecideGunShotPosition(), DecideGunshotRotation());
            nextShot = Time.time + timeBetweenShots;
            bulletsInMag--;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (mousePos - (Vector2)transform.position).normalized, 100);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.tag == "Vampire")
                {
                    hit.collider.gameObject.GetComponent<vampireController>().health -= damage;
                    Instantiate(bloodEffect, hit.point, Quaternion.Euler(0, 0, rotationZ - 180));
                    break;
                }
                else if (hit.collider.gameObject.tag == "Ghost")
                {
                    hit.collider.gameObject.GetComponent<ghostController>().health -= damage;
                    Instantiate(bloodEffect, hit.point, Quaternion.Euler(0, 0, rotationZ - 180));
                    break;
                }
                else if (hit.collider.gameObject.tag == "Frankenstein")
                {
                    hit.collider.gameObject.GetComponent<frankensteinController>().health -= damage;
                    Instantiate(bloodEffect, hit.point, Quaternion.Euler(0, 0, rotationZ - 180));
                    break;
                }
                else if (hit.collider.gameObject.tag == "Werewolf")
                {
                    hit.collider.gameObject.GetComponent<werewolfController>().health -= damage;
                    Instantiate(bloodEffect, hit.point, Quaternion.Euler(0, 0, rotationZ - 180));
                    break;
                }
            }
        }
    }

    void CheckIfCanShoot()
    {
        if (bulletsInMag == 0 && canShoot)
        {
            canShoot = false;
            finishedReloading = Time.time + reloadTime;
        }

        if (Time.time >= finishedReloading && !canShoot)
        {
            canShoot = true;
            if (bulletsInMag == 0)
            {
                if (totalBullets >= bulletsPerMag)
                {
                    bulletsInMag = bulletsPerMag;
                    totalBullets -= bulletsPerMag;
                }
                else
                {
                    bulletsInMag = totalBullets;
                    totalBullets = 0;
                }
            }
            else
            {
                if (totalBullets >= bulletsPerMag - bulletsInMag)
                {
                    totalBullets -= bulletsPerMag - bulletsInMag;
                    bulletsInMag = bulletsPerMag;
                }
                else
                {
                    bulletsInMag += totalBullets;
                }
            }
        }
    }

    Vector3 DecideGunShotPosition()
    {
        if (rotationZ < 135 && rotationZ > 45)
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, 0.5f);
        }
        else if (rotationZ > -135 && rotationZ < -45)
        {
            return new Vector3(transform.position.x, transform.position.y - 0.5f, -1);
        }
        else if (rotationZ < 45 && rotationZ > -45)
        {
            return new Vector3(transform.position.x + 1, transform.position.y + 0.1f, 0.5f);
        }
        else
            return new Vector3(transform.position.x - 1, transform.position.y + 0.1f, 0.5f);

    }

    Quaternion DecideGunshotRotation()
    {
        if (rotationZ < 135 && rotationZ > 45)
        {
            return Quaternion.Euler(0, 0, 90);
        }
        else if (rotationZ > -135 && rotationZ < -45)
        {
            return Quaternion.Euler(0, 0, -90);
        }
        else if (rotationZ < 45 && rotationZ > -45)
        {
            return gunShotParticles.transform.rotation;
        }
        else
            return Quaternion.Euler(0, 0, 180);
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
                sr.flipX = false;
            }
            else
            {
                sr.sprite = sprites[1];
                sr.flipX = true;
            }
        }
    }
}
