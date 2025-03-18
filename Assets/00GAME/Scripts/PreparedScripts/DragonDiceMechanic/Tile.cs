using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IDropHandler
{
    public int i, j;
    public int damage;
    [SerializeField]
    Text _damageTxt;
    [SerializeField]
    SpriteRenderer _sprite;

    public bool _used;

    public DDEnemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _damageTxt.text = damage.ToString();
    }
    public void SetColor(Color color)
    {
        this._sprite.color = color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (_used)
        {
            return;
        }
        if (eventData.pointerDrag != null)
        {

            eventData.pointerDrag.GetComponent<Transform>().position = this.transform.position;
            GridManager.instance.Tiles[eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j]._used = false;
            GridManager.instance.UnHightLight(eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j, eventData.pointerDrag.GetComponent<Dice>().type);
            GridManager.instance.CaculateDamage(eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j, eventData.pointerDrag.GetComponent<Dice>().type, -eventData.pointerDrag.GetComponent<Dice>().damage);
            eventData.pointerDrag.GetComponent<Dice>().i = this.i;
            eventData.pointerDrag.GetComponent<Dice>().j = this.j;
            GridManager.instance.Tiles[eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j]._used = true;
            GridManager.instance.HightLight(eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j, eventData.pointerDrag.GetComponent<Dice>().type, Color.red);
            GridManager.instance.CaculateDamage(eventData.pointerDrag.GetComponent<Dice>().i, eventData.pointerDrag.GetComponent<Dice>().j, eventData.pointerDrag.GetComponent<Dice>().type, eventData.pointerDrag.GetComponent<Dice>().damage);
            eventData.pointerDrag.GetComponent<Dice>().placed = true;
        }
        Debug.Log("d231");
    }
    public void CheckDamage()
    {
        if (_enemy != null)
        {
            _enemy.health -= this.damage;
        }
    }
}
