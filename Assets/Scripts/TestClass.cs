using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{

    public Weapon _BasicSword = new Weapon("Basic Sword", "A basic sword", 1, 2, WeaponType.Sword);

    public Weapon _BasicStaff = new Weapon("Basic Staff", "A basic staff", 1, 1, WeaponType.Staff);

    public Weapon _BasicHammer = new Weapon("Basic Hammer", "A basic hammer", 1, 2, WeaponType.Hammer);

    public Weapon _BasicBow = new Weapon("Basic Slingshot", "A basic bow", 1, 2, WeaponType.Bow);

    public Weapon _BasicAxe = new Weapon("Basic Axe", "A basic axe", 1, 2, WeaponType.Axe);

    // Start is called before the first frame update
    void Start()
    {
        _BasicSword.Use();


    }

    

}
