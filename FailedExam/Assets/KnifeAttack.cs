using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAttack : MonoBehaviour
{
    

    Vector2 rightAttackOffset;
    Collider2D knifeCollider;
    public void Start()
    {
        knifeCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }
    
    public void AttackRight()
    {
        print("right");
        knifeCollider.enabled = true;
        transform.position = rightAttackOffset;
    }
    public void AttackLeft() {
        print("left");
        knifeCollider.enabled = true;
        
        transform.position = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }
    public void StopAttack()
    {
        knifeCollider.enabled = false;
    }


}
