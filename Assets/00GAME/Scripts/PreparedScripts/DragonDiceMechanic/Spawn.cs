using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Singleton<Spawn>
{
    public int wave;
    [SerializeField]
    Dice _dicePrefab;
    [SerializeField]
    DDEnemy _enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RollDices()
    {
        SpawnDice();
        SpawnDice();
        SpawnDice();

        SpawnEnemies();
    }
    public void SpawnDice()
    {
        Dice dice = Instantiate(_dicePrefab);
        dice.Init();
        
        int x = (int)Random.Range(0, GridManager.instance._width);
        int y = (int)Random.Range(0, GridManager.instance._height);
        while (GridManager.instance.Tiles[x,y]._used)
        {
            x = (int)Random.Range(0, GridManager.instance._width);
            y = (int)Random.Range(0, GridManager.instance._height);
        }
        GridManager.instance.Tiles[x, y]._used = true;
        dice.i = x;
        dice.j = y;
        dice.transform.position = new Vector3(dice.i, dice.j);
        GridManager.instance.CaculateDamage(dice.i, dice.j, dice.type, dice.damage);
        Debug.Log("d1");
    }
    public void SpawnEnemies()
    {
        if(wave == 0)
        {
            DDEnemy e = Instantiate(_enemyPrefab);
            e.Init(5,Random.Range(0,(int)GridManager.instance._height),3);
        }
        else
        {

            DDEnemy e = Instantiate(_enemyPrefab);
            e.Init(5, (int)Random.Range(0, GridManager.instance._height), 5);

            int y = (int)Random.Range(0, GridManager.instance._height);
            while (GridManager.instance.Tiles[5, y]._used)
            {
                y = (int)Random.Range(0, GridManager.instance._height);
            }
            DDEnemy e2 = Instantiate(_enemyPrefab);
            e.Init(5, y, 5);
        }
    }
}
