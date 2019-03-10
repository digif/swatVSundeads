using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shooterController : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject mainWeapon;
    public GameObject sideArm;
    public GameObject bulletTrace;

    List<GameObject> bulletTraceStack;

    bool isMainWeaponSelected;

    public Text ammoUI;
    public Text chargeurUI;
    public Text lifeUI;

    public AudioSource fire;

    public ParticleSystem particleMainWeapon;
    public ParticleSystem particleSideArm;

    

    static int ammo;
    static int mainWeaponCharger;
    static int sideArmCharger;

    float mainWeaponFireDelay = 0.1f;
    float sideArmFireDelay = 0.5f;
    float ReloadDelay = 3.0f;
    float changeWeaponDelay = 1.0f; 

    Animation mainWeaponAnimation;
    Animation sideArmAnimation;

    float delay;

    public int playerHp;
    private float hitDelay;

    public GameObject restartButton;        

    // Start is called before the first frame update
    void Start()
    {
        hitDelay = 1;
        bulletTraceStack = new List<GameObject>();
        delay = 0.0f;
        playerHp = 5;
        isMainWeaponSelected = true;
        mainWeaponAnimation = mainWeapon.GetComponent<Animation>();
        sideArmAnimation = sideArm.GetComponent<Animation>();
        mainWeaponCharger = 30;
        sideArmCharger = 12;
        ammo = 120;
    }

    // Update is called once per frame
    void Update()
    {
        hitDelay -= Time.deltaTime;

        ammoUI.text = ammo.ToString();
        lifeUI.text = playerHp.ToString();
        if (isMainWeaponSelected)
        {
            chargeurUI.text = mainWeaponCharger.ToString();
        }
        else
        {
            chargeurUI.text = sideArmCharger.ToString();
        }



        if (delay < Time.deltaTime)
        {
            if (Input.GetButtonDown("Reload"))
            {
                delay = ReloadDelay;
                if (isMainWeaponSelected)
                {
                    if (ammo > 0)
                    {
                        mainWeaponAnimation.Play("reload");
                    }

                    if (ammo > mainWeaponCharger + 30)
                    {
                        ammo -= (30 - mainWeaponCharger);
                        mainWeaponCharger = 30;
                    }
                    else
                    {
                        mainWeaponCharger += ammo;
                        ammo = 0;
                    }
                }
                else
                {
                    sideArmAnimation.Play("reload");
                    sideArmCharger = 12;
                }
            }
            if (Input.GetButtonDown("ChangeWeapon"))
            {
                Debug.Log(isMainWeaponSelected.ToString());
                delay = changeWeaponDelay;
                isMainWeaponSelected = !isMainWeaponSelected;
                mainWeapon.SetActive(!mainWeapon.activeInHierarchy);
                sideArm.SetActive(!sideArm.activeInHierarchy);
                Debug.Log(isMainWeaponSelected.ToString());
                if (isMainWeaponSelected)
                {
                    mainWeaponAnimation.Play("draw");

                }
                else
                { 
                    sideArmAnimation.Play("draw");
                }
            }
            if (isMainWeaponSelected && mainWeaponCharger>0 && Input.GetButton("Fire"))
            {
                fire.Play();
                particleMainWeapon.Play();
                mainWeaponCharger -= 1;
                delay = mainWeaponFireDelay;
                Fire(150.0f);
            }
            if (!isMainWeaponSelected && sideArmCharger>0 && Input.GetButtonDown("Fire"))
            {
                fire.Play();
                particleSideArm.Play();
                sideArmCharger -= 1;
                delay = sideArmFireDelay;
                Fire(50.0f);
            }

        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    void Fire(float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            GameObject target = hit.collider.gameObject;
            if (target.tag == "enemy" || target.tag == "destructible")
            {
                target.SendMessage("OnHit");
            }
            if (target.tag == "environement" || target.tag == "destructible")
            {
                if (bulletTraceStack.Count > 10)
                {
                    bulletTraceStack.RemoveAt(0);
                }
                bulletTraceStack.Add(Instantiate(bulletTrace, hit.point, Quaternion.LookRotation(hit.normal), hit.transform));
            }
        }
    }

    void OnPlayerHit(int damage)
    {
        if(hitDelay < 0)
        {
            hitDelay = 1;
            playerHp -= damage;
        }

        if (playerHp <= 0)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(restartButton);
            playerHp = 5;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ammunition")
        {
            ammo = 150 - mainWeaponCharger;
        }
    }
}
