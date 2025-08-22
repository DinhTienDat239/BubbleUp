using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterPipeController : MonoBehaviour
{
    [SerializeField]
    GameObject water;
    [SerializeField]
    Vector2 waterPos;

    float timer;
    [SerializeField]
    public float timeSpray;
    [SerializeField]
    public float firstTimeSpray;

    Vector2 worldWaterPos;
    // Start is called before the first frame update
    void Start()
    {
        timer = firstTimeSpray;
        water.transform.localPosition = waterPos;
        worldWaterPos = water.transform.position;
        water.transform.position = this.transform.position;
    }
    private void Update()
    {
        if (InGamePlayManager.instance.blacked)
        {
            DOTween.Kill(water.gameObject);
            timer = firstTimeSpray;
            StopAllCoroutines();
            Init();
            return;
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SprayWater();
            timer = timeSpray;
        }
    }
    public void Init()
    {
        water.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        water.transform.localPosition = Vector3.zero;
    }
    // Update is called once per frame
    public void SprayWater()
    {
        StartCoroutine(SprayWaterIE());
    }
    IEnumerator SprayWaterIE()
    {
        water.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        water.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.15f);
        water.transform.localPosition = Vector3.zero;
        water.transform.DOMove(worldWaterPos, 0.1f);
        yield return new WaitForSeconds(2.5f);
        water.GetComponent<SpriteRenderer>().DOColor(new Color(0, 0, 0, 0), 0.05f);
        yield return new WaitForSeconds(0.25f);
        water.transform.position = this.transform.position;
    }
}
