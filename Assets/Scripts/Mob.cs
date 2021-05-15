using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public int mobHealth = 10;
    public float mobSpeed = 1.2f;

    private bool isFrozen = false;
    private float freezePower = 0f;

    public void Freeze(float freezePower)
    {
        if(isFrozen)
        {
            if (this.freezePower < freezePower)
            {
                this.freezePower = freezePower;
            }
        }
        else
        {
            this.freezePower = freezePower;
            isFrozen = true;
        }
    }

    public void GetDamage(int damage)
    {
        mobHealth -= damage;
        if (mobHealth <= 0)
        {
            onDeath();
        }
    }

    private void onDeath()
    {
        Destroy(this.gameObject);
        MobGenerator.singleton.whenMobDead();
    }

    private void mobSettings()
    {
        transform.gameObject.tag = "Enemy";
        transform.gameObject.layer = 10;
    }

    private void CameToEnd()
    {
        Destroy(this.gameObject);
        MobGenerator.singleton.whenMobDead();
    }

    private Vector2[] DividePath(Vector2 from, Vector2 to)
    {
        List<Vector2> res = new List<Vector2>();

        int cnt = 20;
        Vector2 gap = (to - from) / cnt;
        Vector2 tmp = from;
        for (int i = 0; i < cnt - 1; i++)
        {
            tmp += gap;
            res.Add(tmp);
        }
        res.Add(to);

        return res.ToArray();
    }
    private IEnumerator MoveMob()
    {
        float currentSpeed = mobSpeed;
        foreach (var tile in MapGenerator.singleton.pathTiles)
        {
            if (isFrozen)
            {
                mobSpeed += freezePower;
            }

            Vector2[] paths = DividePath(this.gameObject.transform.position, tile.transform.position);

            foreach (var next in paths)
            {
                this.gameObject.transform.position = next;
                yield return new WaitForSeconds(mobSpeed);
            }

        }

        CameToEnd();
        yield break;
    }

    private void Start()
    {
        mobSettings();
        StartCoroutine(MoveMob());
    }
}