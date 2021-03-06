using UnityEngine;
using System.Collections;

public class StaminaBar : Bar {

    protected override int GetMax()
    {
        return Hero.hero.maxStamina;
    }

    protected override float GetFracOfScreen()
    {
        return 2f;
    }

    protected override Vector2 GetLocation()
    {
        return new Vector2(10, 40);
    }

    protected override Color GetColor()
    {
        return Color.yellow;
    }

    protected override Color GetTextColor()
    {
        return Color.black;
    }
}
