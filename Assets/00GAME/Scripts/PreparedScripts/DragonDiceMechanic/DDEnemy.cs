using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DDEnemy : MonoBehaviour
{
    public int i, j;
    public int health;

    [SerializeField]
    Text _healthTxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init(int i, int j, int health)
    {
        this.i = i;
        this.j = j;
        this.transform.position = new Vector3(i + 5, j);
        LeanTween.move(this.gameObject, new Vector2(i, j), 0.5f).setEase(LeanTweenType.easeOutQuad);
        GridManager.instance.Tiles[i, j]._used = true;
        GridManager.instance.Tiles[i, j]._enemy = this;
        this.health = health;
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GridManager.instance.Tiles[i, j]._used = false;
            GridManager.instance.Tiles[i, j]._enemy = null;
            Destroy(this.gameObject);
        }
        _healthTxt.text = health.ToString();
    }
    public void Move()
    {
        GridManager.instance.Tiles[i, j]._used = false;
        GridManager.instance.Tiles[i, j]._enemy = null;
        i = i - 1;
        LeanTween.move(this.gameObject, new Vector2(i, j), 0.5f).setEase(LeanTweenType.easeOutQuad);
        GridManager.instance.Tiles[i, j]._used = true;
        GridManager.instance.Tiles[i, j]._enemy = this;
    }

}
