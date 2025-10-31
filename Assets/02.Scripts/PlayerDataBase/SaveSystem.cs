using UnityEngine;
using System.IO; //���� ������� ���� �ʿ��ϵ� 

//json ���Ϸ� �����ϰ�, ����� json������ ��ü�� ��ȯ�ϴ� �뵵
public class SaveSystem
{
    //������ �����ε�, ����Ƽ�� �����ϴ� ���Application.persistentDataPath�� ����.
    //���ŷӰ� ��� ������ �ʿ䰡 ���µ�, ��� ��ȣȭ ���� ������
    //���� ������ ���輺�� �ִٰ� ��
    private static string savePath => Application.persistentDataPath + "/playerdata.json";

    //�����͸� json �������� ������ �Լ�
    public static void SavePlayer(PlayerData data)
    {
        //true�� "prettyPrint"�ɼ� �б⽱�� �鿩���� �߰��ϴ°ž� Tojson()�ϰ�,
        string json = JsonUtility.ToJson(data, true);
        //�����ο� �ؽ�Ʈ�� ����
        File.WriteAllText(savePath, json);
    }

    //����� json ������ �ҷ��� �Լ� 
    public static PlayerData LoadPlayer()
    {
        //������ �����ο� ������
        if (!File.Exists(savePath))
        {
            //�α� �ѹ� �پ��ְ� nulló��
            Debug.LogWarning("No save file found.");
            return null;
        }

        //json ������ �ؽ�Ʈ�� �о�ͼ� 
        string json = File.ReadAllText(savePath);
        //json ���ڿ��� PlayerData ��ü�� ��ȯ�ϰ� ��ȯ
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
