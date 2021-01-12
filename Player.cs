using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    static private Player player;

    public float flashDuration = 0.1f;
    private float flashDurationPassed;
    private bool isShooting = false;
    private bool canShoot = true;
    public float smooth = 1;
    public GameObject feet;
    public float speed;
    private float speedDefualt;
    public GameObject camera;
    public GameObject healthBar;
    public Health health;
    public Inventory inventory;
    [HideInInspector]
    public bool oneTime = true;
    public GameObject headRenderer;
    public Sprite maleHead;
    public Sprite femaleHead;

    private Rigidbody2D rb;

    public GameObject cursor;

    public GameObject selectedWeapon;
    public GameObject emptyGunSound;
    [HideInInspector]
    public Weapon weapon;

    public float dashSpeed;
    public float dashTime;
    public float dashTimeElapsed;
    public float dashCoolDown;
    private float dashCoolDownElapsed;
    private bool canDash = true;
    public static bool isDashing = false; //Changed from private bool
    public bool canMove = true;
    //Number of potential body checks
    public static int bodyChecks = 10; 

    private void Awake()
    {
        if (player == null)
            player = this;
        else
            Debug.LogError("Player.Awake() - Attempted to assign second Player.player");
    }

    private bool specialAbility = true;

    void Start()
    {
        speedDefualt = speed;
        rb = this.GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        weapon = selectedWeapon.GetComponent<Weapon>();
        weapon.equip();
        health = gameObject.GetComponent<Health>();
        inventory = gameObject.GetComponent<Inventory>();
        if (MenuOnClick.isCurrentlyMale)
        {
            headRenderer.GetComponent<SpriteRenderer>().sprite = maleHead;
        }
        else
        {
            headRenderer.GetComponent<SpriteRenderer>().sprite = femaleHead;
        }
        if (MenuOnClick.isMedic)
        {
            health.setMaxHealth(15);
            health.setHealth(15);
        }
        else if (MenuOnClick.isBolt)
        {
            speed = speed + 3;
        }
    }

    void Update()
    {
        if (!canDash)
        {
            dashCoolDownElapsed += Time.deltaTime;
        }
        if (dashCoolDownElapsed >= dashCoolDown)
        {
            canDash = true;
        }

        healthBar.GetComponent<Image>().fillAmount = (health.getHealthPercentage());
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        cursor.GetComponent<RectTransform>().position = mouseScreen;
        this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg), smooth);

        if (Input.GetKeyDown(KeyCode.R) && !isShooting)
        {
            weapon.reload();
        }

        if (Input.GetMouseButton(0))
        {
            if (canShoot && weapon.canShoot())
            {
                isShooting = true;
                weapon.shoot();
                camera.GetComponent<CameraFollow>().startShake();

                if (!weapon.isAutomatic)
                {
                    isShooting = false;
                    canShoot = false;
                }

            }
            else if (weapon.isEmpty())
            {
                if (canShoot)
                    emptyGunSound.GetComponent<AudioSource>().Play();
                canShoot = false;
                isShooting = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            canShoot = true;
            isShooting = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (canDash)
            {
                dashTimeElapsed = 0;
                canDash = false;
                dashCoolDownElapsed = 0;
                speed = speedDefualt;
            }
        }
        //Player movement and aiming
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if ((horizontal != 0 || vertical != 0) && canMove)
        {
            if (Input.GetKey(KeyCode.Space) && canDash)
            {
                if (!isDashing)
                    isDashing = true;
            }
            if (isDashing)
            {
                if (dashTimeElapsed < dashTime)
                {
                    dashTimeElapsed += Time.deltaTime;
                    speed = dashSpeed;
                }
                else
                {
                    speed = speedDefualt;
                    dashTimeElapsed = 0;
                    canDash = false;
                    dashCoolDownElapsed = 0;
                    isDashing = false;
                    rb.velocity = Vector2.zero;
                }
            }

            feet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg);
            feet.GetComponent<Animator>().SetBool("isRunning", true);

            if (Mathf.Sqrt(Mathf.Pow(horizontal, 2) + Mathf.Pow(vertical, 2)) > 1)
            {
                float coefficient = Mathf.Sqrt(Mathf.Pow(horizontal, 2) + Mathf.Pow(vertical, 2));
                horizontal /= coefficient;
                vertical /= coefficient;
            }
            Vector2 pos2 = rb.position + new Vector2(horizontal * speed, vertical * speed) * Time.deltaTime;

            rb.MovePosition(pos2);
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;

        }

        else
        {
            feet.GetComponent<Animator>().SetBool("isRunning", false);
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        if (Input.GetKey(KeyCode.Alpha1)) // Checks what key has been pressed and calls the EquipWeapon method in Inventory 
        {
            if (oneTime) // Only executes if the "CoolDown" has occured
            {
                oneTime = false;
                inventory.EquipWeapon(0); // Passing index of weapon the user would like to equip
            }
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (oneTime)
            {
                oneTime = false;
                inventory.EquipWeapon(1);
            }

        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            if (oneTime)
            {
                oneTime = false;
                inventory.EquipWeapon(2);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            if (oneTime)
            {
                oneTime = false;
                inventory.EquipWeapon(3);
            }
        }

        if ((MenuOnClick.isMedic == true) && Input.GetKey(KeyCode.H) && specialAbility)
        {
            health.AddHealth(15);
            specialAbility = false;
        }

    }

}
