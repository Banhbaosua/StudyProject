using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelSelector : MonoBehaviour
{
    public GameObject prefabBtnLockedLevel;
    public GameObject prefabBtnPassedLevel;
    public GameObject prefabBtnCurrentLevel;
    public GameObject prefabBtnUnlockedLevel;
    public Button btnNext;
    public Button btnPrevious;
    public Button btnPlay;
    public ScrollRect ScrollView;
    public Transform Board;
    private int pageCurrent=1;
    private int pageMax=12; //example
    private int levelMax=65; //example
    private int currentLevelSelector;
    private int playerUnlocked = 5;
    public class Level
    {
        public GameObject level;
        public bool unlock;
        public Level(GameObject newlevel, bool newstatus)
        {
            level = newlevel;
            unlock = newstatus;
        }
    }

    List<Level> items = new List<Level>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
        foreach(var i in GetPageLevel(1))
            i.level.SetActive(true);
        btnNext.GetComponent<Button>().onClick.AddListener(()=>btnNextPage(1));
        btnPrevious.GetComponent<Button>().onClick.AddListener(()=>btnNextPage(-1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLevel()
    {
        for (int i = 1; i<=levelMax; i++)
        {
            if(i<playerUnlocked)
            {
                GameObject newBtn = Instantiate(prefabBtnPassedLevel,Board,true) as GameObject;
                newBtn.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newBtn.transform.name = "level "+ i.ToString();
                newBtn.GetComponent<Button>().onClick.AddListener(delegate{
                    
                });
                items.Add(new Level(newBtn,true));
            }

            if(i==playerUnlocked)
            {
                GameObject newBtn = Instantiate(prefabBtnCurrentLevel,Board,true) as GameObject;
                newBtn.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newBtn.transform.name = "level "+ i.ToString();
                newBtn.GetComponent<Button>().onClick.AddListener(delegate{

                });
                items.Add(new Level(newBtn,true));
            }

            if(i>playerUnlocked)
            {
                GameObject newBtn = Instantiate(prefabBtnLockedLevel,Board,true) as GameObject;
                newBtn.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
                newBtn.transform.name = "level "+ i.ToString();
                items.Add(new Level(newBtn,false));
            }
        }
    }

    public void btnNextPage(int p)
    {
        pageCurrent += p;
        if(levelMax%pageMax==0)
            if(pageCurrent>(levelMax/pageMax) || pageCurrent <=1)
                pageCurrent=1;
        if(levelMax%pageMax!=0)
            if(pageCurrent>(levelMax/pageMax)+1 || pageCurrent <=1)
                pageCurrent=1;
        ScrollView.transform.GetChild(1).GetComponent<Text>().text = "Page "+pageCurrent.ToString();

        //hide all level
        for(int i =0; i<levelMax; i++)
            items[i].level.SetActive(false);
        
        //only show selected page level
        foreach (var o in GetPageLevel(pageCurrent))
        {
            o.level.SetActive(true);
        }
    }

    IEnumerable<Level> GetPageLevel(int pageNum)
    {
        return items.Skip((pageNum-1)*pageMax).Take(pageMax);
    }
}
