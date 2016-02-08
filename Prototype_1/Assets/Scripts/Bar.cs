using UnityEngine;
using System.Collections;

public abstract class Bar : MonoBehaviour {

    private int max, current;
    private float len;

    protected abstract int GetMax();
    protected abstract float GetFracOfScreen();
    protected abstract Vector2 GetLocation();
    protected abstract Color GetColor();
    protected abstract Color GetTextColor();

	// Use this for initialization
	void Start()
    {
	    current = GetMax();
        len = Screen.width / GetFracOfScreen();
	}
	
    void OnGUI()
    {
        Vector2 loc = GetLocation();
        string display = current + "/" + GetMax();
        GUIStyle style = MakeStyle(20, (int)len);
        GUI.Box(new Rect(loc.x, loc.y, len, 20), display, style);
    }
    
    public void Add(int amount)
    {
        current += amount;
        current = (current > GetMax() ? GetMax() : current);
        UpdateLen();
    }

    public void Remove(int amount)
    {
        current -= amount;
        current = (current < 0 ? 0 : current);
        UpdateLen();
    }

    private void UpdateLen()
    {
        len = (Screen.width / 2) * (current / (float) GetMax());
    }

    private GUIStyle MakeStyle(int x, int y)
    {
        GUIStyle style = new GUIStyle(GUI.skin.box);
        style.normal.background = MakeTexture(x, y);
        style.normal.textColor = GetTextColor();
        return style;
    }

    private Texture2D MakeTexture(int x, int y)
    {
        Color[] pix = new Color[x * y];

        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = GetColor();
        }

        Texture2D text = new Texture2D(x, y);
        text.SetPixels(pix);
        text.Apply();
        return text;
    }
}
