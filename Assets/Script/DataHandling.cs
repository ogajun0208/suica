using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataHandling : MonoBehaviour {
        [HideInInspector] public SaveData data; //JSON変換する対象のデータクラス
        string filepath;//セーブデータのパス(JSON拡張子のファイルだよ)
        string fileName = "Data.json";//セーブデータのファイル名

        //開始時にファイルチェックと読み込み
        void Awake(){
            if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)filepath = Application.dataPath + "/" + fileName;
            if(Application.platform == RuntimePlatform.Android)filepath = Application.persistentDataPath + "/" + fileName;
            //ファイルが無いときはファイルを作成するよ!
            if(!File.Exists(filepath)){
                Save(data);
            }

            data = Load(filepath);
        }
        //セーブ
        void Save(SaveData data){
            string json = JsonUtility.ToJson(data); //SaveData型からJsonが対応してくれるものへ変換!
            StreamWriter wr = new StreamWriter(filepath, false); //ファイルの書き込みのあれこれ
            wr.WriteLine(json); //ここで書き込み
            wr.Close(); //ちゃんと閉じようね!
        }
        //読み込み
        SaveData Load(string path){
            StreamReader rd = new StreamReader(path); 
            string json = rd.ReadToEnd();
            rd.Close();

            return JsonUtility.FromJson<SaveData>(json);
        }
        void OnDestroy() {
            Save(data);   
        }
        void OnApplicationQuit(){
            Save(data);
        }

    }
