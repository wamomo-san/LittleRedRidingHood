using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleAttackCone : MonoBehaviour {

    public Mole Mole;

    public bool isLeft = false;

    void Awake()
    {
        Mole = gameObject.GetComponentInParent<Mole>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                Mole.AttackAnim(false);
            }
            else
            {
                Mole.AttackAnim(true);
            }

        }
    }

}

