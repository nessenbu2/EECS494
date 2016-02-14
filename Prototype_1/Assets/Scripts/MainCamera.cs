using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public GameObject hero, deathMenu, victoryMenu;

    private bool spawned = false;

	void Update ()
    {
        if (Hero.hero != null && Hero.hero.Dead())
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

        if (!spawned && EnemyBase.NumEnemies() == 0)
        {
            spawned = true;
            Instantiate(victoryMenu);
        }
	}
}
