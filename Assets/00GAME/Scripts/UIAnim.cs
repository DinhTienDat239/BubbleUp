using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnim : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();
    [SerializeField]
    float timePerFrame;
    float timer;
    int index;

    [SerializeField]
    Image spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timePerFrame;
        index = 0;
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
        }
    }
}
