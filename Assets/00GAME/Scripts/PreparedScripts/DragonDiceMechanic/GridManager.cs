using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class GridManager : Singleton<GridManager>
{
    public Tile[,] Tiles;
    [SerializeField]
    public float _width;
    [SerializeField]
    public float _height;
    [SerializeField]
    Tile _tilePrefab;

    [SerializeField]
    Transform _cam;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = new Tile[(int)_width, (int)_height];
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Tile tile = Instantiate(_tilePrefab);
                tile.name = "Tile " + "[" + i + "," + j + "]";
                tile.transform.position = new Vector3(i, j, 0);
                tile.i = i;
                tile.j = j;
                Tiles[i, j] = tile;
            }
        }
        _cam.position = new Vector3(_width / 2 - 0.5f, _height / 2 - 0.5f, -10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Attack()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Tiles[i, j].CheckDamage();
                if (Tiles[i,j]._enemy != null)
                {
                    Tiles[i, j]._enemy.Move();
                }
            }
        }
        Spawn.instance.SpawnEnemies();
    }
    public void CaculateDamage(int row, int column, int type, int damage)
    {
        if (type == 0)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row || j == column)
                    {
                        if (Tiles[i, j].damage == 0 && damage < 0)
                            return;
                        Tiles[i, j].damage += damage;

                    }
                }
            }
        }else
        if (type == 1)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row)
                    {
                        if (Tiles[i, j].damage == 0 && damage < 0)
                            return;
                        Tiles[i, j].damage += damage;

                    }
                }
            }
        }
        else
        if (type == 2)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (j == column)
                    {
                        if (Tiles[i, j].damage == 0 && damage < 0)
                            return;
                        Tiles[i, j].damage += damage;

                    }
                }
            }
        }
    }
    public void HightLight(int row, int column, int type,Color color)
    {
        if (type == 0)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row || j == column)
                    {

                        Tiles[i, j].SetColor(color);

                    }
                }
            }
        }else
        if (type == 1)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row)
                    {

                        Tiles[i, j].SetColor(color);

                    }
                }
            }
        }
        else
        if (type == 2)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (j == column)
                    {

                        Tiles[i, j].SetColor(color);

                    }
                }
            }
        }
    }
    public void UnHightLight(int row, int column, int type)
    {
        if (type == 0)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row || j == column)
                    {
                        Tiles[i, j].SetColor(Color.white);

                    }
                }
            }
        }
        else
        if (type == 1)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (i == row)
                    {

                        Tiles[i, j].SetColor(Color.white);

                    }
                }
            }
        }
        else
        if (type == 2)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (j == column)
                    {

                        Tiles[i, j].SetColor(Color.white);

                    }
                }
            }
        }
    }
}
