using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    [SerializeField]
    Sprite _emptySprite;
    [SerializeField]
    Sprite _fullSprite;
    Image image;
    public bool isEmpty;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        isEmpty = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }
    void UpdateSprite(){
        if(isEmpty){
            image.sprite = _emptySprite;
        }else{
            image.sprite = _fullSprite;
        }
    }
}
