using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterController : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject mainWeapon;
    public GameObject sideArm;
    public GameObject bulletTrace;

    List<GameObject> bulletTraceStack;

    bool isMainWeaponSelected;


    int ammo;
    int mainWeaponCharger;
    int sideArmCharger;

    float mainWeaponFireDelay = 0.1f;
    float sideArmFireDelay = 0.5f;
    float ReloadDelay = 3.0f;
    float changeWeaponDelay = 1.0f; 

    Animation mainWeaponAnimation;
    Animation sideArmAnimation;

    float delay;

    public int playerHp;
    // Start is called before the first frame update
    void Start()
    {
        bulletTraceStack = new List<GameObject>();
        delay = 0.0f;
        playerHp = 5;
        isMainWeaponSelected = true;
        mainWeaponAnimation = mainWeapon.GetComponent<Animation>();
        sideArmAnimation = sideArm.GetComponent<Animation>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (delay < Time.deltaTime)
        {
            if (Input.GetButtonDown("Reload"))
            {
                delay += ReloadDelay;
                if (isMainWeaponSelected)
                {
                    mainWeaponAnimation.Play("reload");
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
                delay += changeWeaponDelay;
                isMainWeaponSelected = !isMainWeaponSelected;
                mainWeapon.SetActive(mainWeapon.activeInHierarchy);
                sideArm.SetActive(sideArm.activeInHierarchy);
                isMainWeaponSelected = !isMainWeaponSelected;
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
                mainWeaponAnimation.Play("fire");
                mainWeaponCharger -= 1;
                delay += mainWeaponFireDelay;
                Fire(150.0f);
            }
            if (!isMainWeaponSelected && sideArmCharger>0 && Input.GetButtonDown("Fire"))
            {
                sideArmAnimation.Play("fire");
                sideArmCharger -= 1;
                delay += sideArmFireDelay;
                Fire(50.0f);
            }

        }
    }

    void Fire(float range)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            GameObject target = hit.collider.gameObject;
            if (target.tag == "enemy")
            {
                target.SendMessage("OnHit");
            }
            if (target.tag == "destructible")
            {
                Destroy(target);
            }
            if (target.tag == "environement")
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
        playerHp -= damage;

        if (playerHp <= 0)
        {
            Time.timeScale = 0;
            deathScreen.SetActive(true);
        }
    }
}
