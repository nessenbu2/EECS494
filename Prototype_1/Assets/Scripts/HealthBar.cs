using UnityEngine;
using System.Collections;

public class HealthBar : Bar {

    protected override int GetMax()
    {
        return Hero.hero.GetMaxHealth();
    }

    protected override float GetFracOfScreen()
    {
        return 2f;
    }

    protected override Vector2 GetLocation()
    {
        return new Vector2(10, 10);
    }
}
