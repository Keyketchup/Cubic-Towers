using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Tower
{

    public float freezePower = 0.01f;

    protected override void Shoot(Mob mob, int dam)
    {
        base.Shoot(mob, dam);
        mob.Freeze(freezePower);
    }
}
