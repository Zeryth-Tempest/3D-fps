using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public enum WeaponState
{
    Unarmed,
    HitScan,
    Projectile,
    Total
}
public class PlayerMovement : MonoBehaviour
{
    public static Camera myCamera = null;

    public List<Weapon> AvailableWeapons = new List<Weapon>();
    public Weapon CurrentWeapon = null;
    
    public float ScrollWheelBreakpoint = 1.0f;
    private float ScrollWheelDelta = 0.0f;

    private WeaponState CurrentWeaponState = WeaponState.Unarmed;


    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    Vector3 velocity;

    public void Awake()
    {
        PlayerMovement.myCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        //Weapon Switch
        HandleWeaponSwap();

        if(Input.GetMouseButtonDown(0))
        {
            FireHeldWeapons();
        }
    }

    private void HandleWeaponSwap()
    {
        ScrollWheelDelta += Input.mouseScrollDelta.y;
        if (Mathf.Abs(ScrollWheelDelta) > ScrollWheelBreakpoint)
        {
            int swapDirectionNumber = (int)Mathf.Sign(ScrollWheelDelta);
            ScrollWheelDelta -= swapDirectionNumber * ScrollWheelBreakpoint;

            int currentWeaponIndex = (int)(CurrentWeapon.WeaponType);
            currentWeaponIndex += swapDirectionNumber;
            
            if (currentWeaponIndex < 0)
            {
                currentWeaponIndex = (int)WeaponState.Total + -1;
            }
            else if (currentWeaponIndex > (int)WeaponState.Total)
            {
                currentWeaponIndex = 0;
            }
            WeaponSwapAnimation(currentWeaponIndex);
        }
    }

    private void WeaponSwapAnimation(int currentWeaponIndex)
    {
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = AvailableWeapons[currentWeaponIndex];
        CurrentWeapon.gameObject.SetActive(true);
    }

    public void FireHeldWeapons()
    {
        if(CurrentWeapon != null)
        {
            CurrentWeapon.Fire();
        }
    }
}
