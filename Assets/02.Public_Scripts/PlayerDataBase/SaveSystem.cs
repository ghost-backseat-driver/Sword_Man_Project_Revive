using UnityEngine;
using System.IO; //파일 입출력을 위해 필요하데 

//json 파일로 저장하고, 저장된 json파일을 객체로 변환하는 용도
public class SaveSystem
{
    //저장경로 설정인데, 유니티가 제공하는 경로Application.persistentDataPath가 있음.
    //번거롭게 경로 설정할 필요가 없는데, 대신 암호화 하지 않으면
    //파일 변조의 위험성은 있다고 함
    private static string savePath => Application.persistentDataPath + "/playerdata.json";

    //테이터를 json 형식으로 저장할 함수
    public static void SavePlayer(PlayerData data)
    {
        //true는 "prettyPrint"옵션 읽기쉽게 들여쓰기 추가하는거야 Tojson()하고,
        string json = JsonUtility.ToJson(data, true);
        //저장경로에 텍스트로 저장
        File.WriteAllText(savePath, json);
    }

    //저장된 json 파일을 불러올 함수 
    public static PlayerData LoadPlayer()
    {
        //파일이 저장경로에 없으면
        if (!File.Exists(savePath))
        {
            //로그 한번 뛰어주고 null처리
            Debug.LogWarning("No save file found.");
            return null;
        }

        //json 파일의 텍스트를 읽어와서 
        string json = File.ReadAllText(savePath);
        //json 문자열을 PlayerData 객체로 변환하고 반환
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
