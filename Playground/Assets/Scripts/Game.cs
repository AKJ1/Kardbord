using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public Player Player;

    public Enemy[] ActiveEnemies;
    // Use this for initialization

    void Start()
    {
        ActiveEnemies = new Enemy[15];
        for (int i = 0; i < 15; i++)
        {
            ActiveEnemies[i] = GameObject.Instantiate(EnemyPrefab).GetComponent<Enemy>();
            ActiveEnemies[i].Target = Player.gameObject;
            ActiveEnemies[i].gameObject.SetActive(false);
            ActiveEnemies[i].isDead = true;
        }

        StartCoroutine(TimeoutEnemy());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator TimeoutEnemy()
    {
        while (true)
        {
            foreach (Enemy activeEnemy in ActiveEnemies)
            {
                if (activeEnemy.isDead)
                {
                    SpawnEnemy(activeEnemy);

                }
            }
            yield return new WaitForSeconds(Random.Range(2, 8));
        }
    }

    void SpawnEnemy(Enemy enemy)
    {

        enemy.isDead = false;
        enemy.gameObject.SetActive(true);
            
        enemy.transform.position = (Player.transform.position + (Player.transform.forward * 15) +
             (Player.transform.forward * Random.Range(0, 20f))) + Player.transform.right * Random.Range(-40,40f);
    }
}
