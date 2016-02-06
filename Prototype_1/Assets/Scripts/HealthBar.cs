using UnityEngine;
using System.Collections;

public class HealthBar : Bar {

    protected override int GetMax()
    {
        return 10;
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
