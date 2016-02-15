using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public GameObject hero, deathMenu, victoryMenu;

    private bool victoryMenuSpawned = false;

	void Update ()
    {
        if (!victoryMenuSpawned && EnemyBase.NumEnemies() <= 0)
        {
            victoryMenuSpawned = true;
            Instantiate(victoryMenu);
        }
        if (Hero.hero != null && Hero.hero.Dead() && !victoryMenuSpawned)
        {
            Destroy(hero);
            Instantiate(deathMenu);
        }
        else if (hero != null)
        {
            Vector3 temp = Vector3.zero;
            temp.x = hero.transform.position.x;
            temp.y = hero.transform.position.y;
            temp.z = -20;
            transform.position = temp;
            
            //pos.y
        }

    }
}
