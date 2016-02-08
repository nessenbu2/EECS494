using UnityEngine;
using System.Collections;

public class HealthBar : Bar {

    protected override int GetMax()
    {
        return Hero.hero.maxHealth;
    }

    protected override float GetFracOfScreen()
    {
        return 2f;
    }

    protected override Vector2 GetLocation()
    {
        return new Vector2(10, 10);
    }

    protected override Color GetColor()
    {
        return Color.red;
    }

    protected override Color GetTextColor()
    {
        return Color.black;
    }
}
