using UnityEngine;
using System.Collections;

public abstract class Bar : MonoBehaviour {

    private int max, current;
    private float len;

    protected abstract int GetMax();
    protected abstract float GetFracOfScreen();
    protected abstract Texture2D GetFullTexture();
    protected abstract Texture2D GetEmptyTexture();
    protected abstract Vector2 GetLocation();

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
        GUI.Box(new Rect(loc.x, loc.y, len, 20), display);
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
}
