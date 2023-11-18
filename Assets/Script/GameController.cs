using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    int functionCallCount = 0;
    int num,next;
    //アイテムリスト
    [SerializeField] private List<GameObject> fruits = new List<GameObject>();

    Dictionary<string, int> FruitDic;

    [SerializeField] private Transform cloudLocation = null;

    GameObject NextFruit_obj;

    [SerializeField] private GameObject RestartButton = null;
    //タイトルボタンボタン
    [SerializeField] private GameObject ToTitleButton = null;


    //ゲームオーバーか？
    bool isInArea = false;
    float timeInArea = 0f;
    [SerializeField] private float requiredTime = 3f;

    [SerializeField] GameObject GameOverText = null;

    //生成されているフルーツのリスト
    List<GameObject> SpawnedFruits = new List<GameObject>();

    string FruitsTag;

    bool isGameOver = false;

    //スコアの変数
    [SerializeField] private Text ScoreText = null;
    int Score = 0;



    // Start is called before the first frame update
    void Start()
    {   
        ToTitleButton.SetActive(false);
        RestartButton.SetActive(false);
        GameOverText.SetActive(false);
        FruitDic = new Dictionary<string, int>()
        {
            {"saku",0},
            {"Ichigo", 1},
            {"Budou", 2},
            {"Ponkan",3},
            {"kaki",4},
            {"Ringo",5},
            {"nashi",6},
            {"momo",7},
            {"pai",8},  
            {"Melon",9},
            {"Suika",10}
        };
        Init();
    }


    void Init(){
        isGameOver = false;
        num = Random.Range(0,fruits.Count - 4);
        Score = 0;
        ScoreText.text = Score.ToString("D"); 
        newFruitFromCloud();
    }

    public void BoolFruit(Transform OldFruit,string fruitsName){
        functionCallCount++;
        if(functionCallCount == 2){
            if(FruitDic[fruitsName] == fruits.Count - 1){ 
                functionCallCount = 0;
                return;
            }
            CreateFruit(fruits[FruitDic[fruitsName] + 1],OldFruit);
            functionCallCount = 0;
        }
    }
    //ぶつかったときの生成
    void CreateFruit(GameObject NewFruit_prefab,Transform OldFruit){
        GameObject NewFruit = Instantiate(NewFruit_prefab,OldFruit.position,OldFruit.rotation);
        NewFruit.GetComponent<Rigidbody2D>().simulated = true;
        
        var Fru = NewFruit.GetComponent<FruitBase>();
        Fru.GameOverEvent.AddListener(GameOver);
        Fru.FruitsDestroyEvent.AddListener(Destory_obj);
        
        PointUp();
        
        SpawnedFruits.Add(NewFruit);
    }
    //雲からの生成
    public void newFruitFromCloud(){
        next = Random.Range(0,fruits.Count - 6);
        Vector2 FruitPosition = new Vector2(cloudLocation.position.x,cloudLocation.position.y - 45f);
        GameObject NewFruit = Instantiate(fruits[num],FruitPosition,Quaternion.identity);
        NewFruit.GetComponent<FruitBase>().FruitsTag = NewFruit.tag;
        NewFruit.gameObject.tag = "Fruits";
        NewFruit.GetComponent<Rigidbody2D>().simulated = false;
        NewFruit.transform.SetParent(cloudLocation);

        SpawnedFruits.Add(NewFruit);
        NewFruit.GetComponent<FruitBase>().FruitsDestroyEvent.AddListener(Destory_obj);
        NewFruit.GetComponent<FruitBase>().GameOverEvent.AddListener(GameOver);

        
        if(NextFruit_obj != null)Destroy(NextFruit_obj);
        NextFruit(next);
        num = next;
    }
    //Nextの生成
    void NextFruit(int num){
        Vector2 posi = new Vector2(831,406);
        NextFruit_obj = Instantiate(fruits[num],posi,Quaternion.identity);
        Destroy(NextFruit_obj.GetComponent<Rigidbody2D>());
    }

    void GameOver(){
        if(isGameOver) return;
        isGameOver = true;
        ToTitleButton.SetActive(true);
        RestartButton.SetActive(true);
        GameOverText.SetActive(true);
        
        this.GetComponent<Ranking>().UpdateRank();

    }

    public void Reset(){
        foreach(var FrutisObj in SpawnedFruits){
            Destroy(FrutisObj);
        }
        SpawnedFruits.Clear();
        RestartButton.SetActive(false);
        GameOverText.SetActive(false);
        ToTitleButton.SetActive(false);
        Init();
    }
    //ポイントの加算処理
    void PointUp(){
        Score += 10;
        ScoreText.text = Score.ToString("D"); 
    }

    public void ToTitle(){
        SceneManager.LoadScene("Title");
    }

    void Destory_obj(GameObject other){
        if(SpawnedFruits.Contains(other)) {
            SpawnedFruits.Remove(other);
            Destroy(other);    
        }
    }

}
