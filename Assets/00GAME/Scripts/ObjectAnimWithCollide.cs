using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectAnimWithCollide : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    List<PolygonCollider2D> colliders = new List<PolygonCollider2D>();
    [SerializeField]
    float timePerFrame;
    float timer;
    int index;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timePerFrame;
        index = 0;
        colliders = this.GetComponents<PolygonCollider2D>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            index++;
            if (index >= sprites.Count)
            {
                index = 0;
            }

            timer = timePerFrame;
            spriteRenderer.sprite = sprites[index];
            for(int i =0;i < colliders.Count; i++)
            {
                colliders[i].enabled = false;
            }
            colliders[index].enabled = true;
        }
    }
}
