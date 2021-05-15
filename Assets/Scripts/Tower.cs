using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Properties")]
    public float checkTimer = 0.5f;
    public float range = 3f;
    public int power = 1;
    public LayerMask enemyLayer;
    public IEnumerator ActiveTower()
    {
        WaitForSeconds delay = new WaitForSeconds(checkTimer);
        for (; ; )
        {
            Collider2D col = Physics2D.OverlapCircle(this.transform.position, range, enemyLayer);
            if(col != null)
            {
                if (col.TryGetComponent<Mob>(out var mob))
                {
                    Shoot(mob, power);
                }
            }
            yield return delay;
        }
    }

    protected virtual void Shoot(Mob mob, int dam)
    {
        mob.GetDamage(dam);
    }

    void Start()
    {
        StartCoroutine(ActiveTower());
    }
}