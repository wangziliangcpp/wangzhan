using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanchisheKuai : MonoBehaviour
{
    public Material qiang;
    public Material AIqiang;
    public Material di;
    public Material wu;
    public Material tou;
    public Material shen;
    public GameManager gm;
    public int x;
    public int y;

    public int shu;
    private void Awake()
    {
        x = y = -1;
        shu = -1;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateXianshi();
    }
    public void Chushihua(int pX, int pY)
    {
        x = pX;
        y = pY;
    }
    public void SetShu(int num)
    {
        shu = num;
    }
    public void UpdateXianshi()
    {
        Material m = null;
        switch(shu)
        {
            case 0:
                m = qiang;
                if (gm.isAIMode)
                {
                    m = AIqiang;
                }
                break;
            case 1:
                m = di;
                break;
            case 2:
                m = wu;
                break;
            case 3:
                m = tou;
                break;
            case 4:
                m = shen;
                break;
            default:
                break;
        }
        gameObject.GetComponent<Renderer>().material = m;
        //beijing.GetComponent<SpriteRenderer>().color = om.com.sheSes[shu];
    }
}
