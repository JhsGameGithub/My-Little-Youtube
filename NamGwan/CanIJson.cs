using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CanIJson : MonoBehaviour
{

    public RequiredData get;

    // Start is called before the first frame update
    void Start()
    {

      //  SaveData();
       // LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveData()
    {
        string test = JsonUtility.ToJson(new RequiredData(DatabaseManager.Player.required), true);

        File.WriteAllText(Application.persistentDataPath + "/jsonUtillyTestByNamgwasn.txt", test);
    }


        
        public void LoadData()
        {
            string test = File.ReadAllText(Application.persistentDataPath + "/jsonUtillyTestByNamgwasn.txt");

            get = JsonUtility.FromJson<RequiredData>(test);

        }
}
