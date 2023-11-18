using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField] private Text ScoreText = null;
    string[] rankNames = {"1st","2rd","3rd"};
    const int rankCnt = SaveData.rankCnt;

    Text[] rankTexts = new Text[rankCnt];
    SaveData data;

    [SerializeField] GameObject RankTe = null;
    
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<DataHandling>().data;

        for(int i = 0; i < rankCnt;i++){
            Transform rankChilds = RankTe.transform.GetChild(i);
            rankTexts[i] = rankChilds.GetComponent<Text>(); 
        }

        DispRank();


    }

    void DispRank(){
        for(int i = 0;i < rankCnt;i++){
            rankTexts[i].text = (rankNames[i] + ":" + data.rank[i]);
        }
    }

    public void UpdateRank(){
        string inpFld = ScoreText.text;
        int score = int.Parse(inpFld);
        Debug.Log(score);
        for(int i = 0;i < rankCnt;i++){
            if(score > data.rank[i]){
                var rep = data.rank[i];
                data.rank[i] = score;
                score = rep;
            }
        }
        DispRank();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
