using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float timer;
    public int[] leis;
    public int sheDir;
    public Queue<GameObject> she;
    public bool isChushihua;

    public bool isAIMode;

    public GameObject dizuoPrefab;
    public GameObject[][] dizuos;

    public GameObject qipanPrefab;
    public GameObject[][] qipans;
    public int qipanDaxiao;

    public GameObject AIModeButton;
    public GameObject MatualModeButton;

    public int score;
    public GameObject scoreText;
    private void Awake()
    {
        ChushihuaBianliangs();
        CreateDizuo();
        ChushihuaQipan();
        ShengchengShe();
        AddShiwu();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChushihua)
        {
            UpdateYincang();
            UpdateDir();
            UpdateSnake();
            UpdateAIMode();
            UpdateScoreText();
        }
    }
    public void UpdateScoreText()
    {
        scoreText.GetComponent<Text>().text = "Score : " + score;
    }
    public void UpdateAIMode()
    {
        if(isAIMode == true)
        {
            AIModeButton.GetComponent<Image>().color = new Color(241f/255f, 146f/255f, 3f/255f, 125f/255f);
            MatualModeButton.GetComponent<Image>().color = new Color(255f/255f, 255f/255f, 255f/255f, 125f/255f);
        }
        else
        {
            AIModeButton.GetComponent<Image>().color = new Color(255f/255f, 255f/255f, 255f/255f, 125f/255f);
            MatualModeButton.GetComponent<Image>().color = new Color(241f/255f, 146f/255f, 3f/255f, 125f/255f);
        }
    }
    public void DianjiMatualMode()
    {
        isAIMode = false;
    }
    public void DianjiAIMode()
    {
        isAIMode = true;
    }
    public void UpdateYincang()
    {
        for(int i = 0; i < qipanDaxiao; i++)
        {
            for(int j = 0; j < qipanDaxiao; j++)
            {
                qipans[i][j].SetActive(true);
            }
        }
        for (int i = 0; i < qipanDaxiao; i++)
        {
            for (int j = 0; j < qipanDaxiao; j++)
            {
                if (qipans[i][j].GetComponent<TanchisheKuai>().shu == 1)
                {
                    qipans[i][j].SetActive(false);
                }
            }
        }
    }
    public void ChushihuaBianliangs()
    {
        score = 0;
        isAIMode = false;
        qipanDaxiao = 20;
        dizuos = new GameObject[qipanDaxiao][];
        for(int i = 0; i < qipanDaxiao; i++)
        {
            dizuos[i] = new GameObject[qipanDaxiao];
        }
        qipans = new GameObject[qipanDaxiao][];
        for (int i = 0; i < qipanDaxiao; i++)
        {
            qipans[i] = new GameObject[qipanDaxiao];
        }
        sheDir = 1;
        timer = 0.5f;

        she = new Queue<GameObject>();
        isChushihua = true;
    }
    public void CreateDizuo()
    {
        float chushiX = -qipanDaxiao / 2;
        float chushiY = -qipanDaxiao / 2;
        for (int i = 0; i < qipanDaxiao; i++)
        {
            for (int j = 0; j < qipanDaxiao; j++)
            {
                dizuos[i][j]= GameObject.Instantiate(dizuoPrefab);
                dizuos[i][j].transform.position = new Vector3(chushiX + i, chushiY + j, 0f);
            }
        }
    }
    public void ChushihuaQipan()
    {
        float chushiX = -qipanDaxiao / 2;
        float chushiY = -qipanDaxiao / 2;
        for (int i = 0; i < qipanDaxiao; i++)
        {
            for (int j = 0; j < qipanDaxiao; j++)
            {
                qipans[i][j] = GameObject.Instantiate(qipanPrefab);
                qipans[i][j].transform.position = new Vector3(chushiX + i, chushiY + j, -1f);
                qipans[i][j].GetComponent<TanchisheKuai>().Chushihua(i, j);
                if (i == 0 || j == 0 || i == qipanDaxiao - 1 || j == qipanDaxiao - 1)
                {
                    qipans[i][j].GetComponent<TanchisheKuai>().shu = 0;
                }
                else
                {
                    qipans[i][j].GetComponent<TanchisheKuai>().shu = 1;
                }
            }
        }
    }

    public void AddShiwu()
    {
        int count = 0;
        bool isFind = false;
        for (int i = 0; i < qipanDaxiao; i++)
        {
            for (int j = 0; j < qipanDaxiao; j++)
            {
                if (qipans[i][j].GetComponent<TanchisheKuai>().shu == 1)
                {
                    count++;
                }
            }
        }
        int c = FanhuiRandom(0, count);
        c++;
        count = 0;
        for (int i = 0; i < qipanDaxiao && !isFind; i++)
        {
            for (int j = 0; j < qipanDaxiao && !isFind; j++)
            {
                if (qipans[i][j].GetComponent<TanchisheKuai>().shu == 1)
                {
                    count++;
                    if (count >= c)
                    {
                        isFind = true;
                        qipans[i][j].GetComponent<TanchisheKuai>().shu = 2;
                    }
                }
            }
        }

    }
    public void UpdateSnake()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Qianjin();
        }
    }
    public void Qianjin()
    {
        timer = 0.5f;
        int[] d1 = { -1, 0 };
        int[] d2 = { 1, 0 };
        int[] d3 = { 0, -1 };
        int[] d4 = { 0, 1 };
        int[][] dir = { d1, d2, d3, d4 };

        GameObject[] tempDui = new GameObject[100];
        she.CopyTo(tempDui, 0);
        int nowX = tempDui[she.Count - 1].GetComponent<TanchisheKuai>().x + dir[sheDir][0];
        int nowY = tempDui[she.Count - 1].GetComponent<TanchisheKuai>().y + dir[sheDir][1];



        if (nowX >= 0 && nowX < qipanDaxiao)
        {
            if (nowY >= 0 && nowY < qipanDaxiao)
            {
                Debug.Log("nowX : " + nowX + " nowY" + nowY);
                switch (qipans[nowX][nowY].GetComponent<TanchisheKuai>().shu)
                {
                    case 0:
                        jieshu();
                        break;
                    case 1:
                        qipans[nowX][nowY].GetComponent<TanchisheKuai>().shu = 3;
                        tempDui[she.Count - 1].GetComponent<TanchisheKuai>().shu = 4;
                        she.Enqueue(qipans[nowX][nowY]);
                        she.Dequeue().GetComponent<TanchisheKuai>().shu = 1;
                        break;
                    case 2:
                        qipans[nowX][nowY].GetComponent<TanchisheKuai>().shu = 3;
                        tempDui[she.Count - 1].GetComponent<TanchisheKuai>().shu = 4;
                        she.Enqueue(qipans[nowX][nowY]);
                        score++;

                        AddShiwu();
                        break;
                    case 3:
                        jieshu();
                        break;
                    case 4:
                        jieshu();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void jieshu()
    {

    }
    public void ChangeSheDir(int ind)
    {
        sheDir = ind;
        Qianjin();
    }
    public void UpdateDir()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSheDir(3);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSheDir(2);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSheDir(0);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSheDir(1);
        }

    }
    public void ShengchengShe()
    {
        qipans[qipanDaxiao / 2][qipanDaxiao / 2].GetComponent<TanchisheKuai>().shu = 3;
        qipans[qipanDaxiao / 2][qipanDaxiao / 2 - 1].GetComponent<TanchisheKuai>().shu = 4;
        qipans[qipanDaxiao / 2][qipanDaxiao / 2 - 2].GetComponent<TanchisheKuai>().shu = 4;
        she.Enqueue(qipans[qipanDaxiao / 2][qipanDaxiao / 2 - 2]);
        she.Enqueue(qipans[qipanDaxiao / 2][qipanDaxiao / 2 - 1]);
        she.Enqueue(qipans[qipanDaxiao / 2][qipanDaxiao / 2]);
    }
    public int FanhuiRandom(int xiaxianBaohan, int shangxianBubao)
    {
        byte[] buffer = System.Guid.NewGuid().ToByteArray();
        int iSeed = System.BitConverter.ToInt32(buffer, 0);
        System.Random ran = new System.Random(iSeed);
        if (shangxianBubao - xiaxianBaohan == 0)
        {
            return xiaxianBaohan;
        }
        return xiaxianBaohan + ran.Next() % (shangxianBubao - xiaxianBaohan);
    }
}
