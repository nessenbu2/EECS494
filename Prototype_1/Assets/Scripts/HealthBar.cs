using UnityEngine;
using System.Collections;

public class HealthBar : Bar {

    public Texture2D full, empty;

    protected override int GetMax()
    {
        return 10;
    }

    protected override float GetFracOfScreen()
    {
        return 2f;
    }

    protected override Texture2D GetFullTexture()
    {
        return full;
    }

    protected override Texture2D GetEmptyTexture()
    {
        return empty;
    }

    protected override Vector2 GetLocation()
    {
        return new Vector2(10, 10);
    }
}
