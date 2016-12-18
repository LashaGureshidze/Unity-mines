using UnityEngine;
using UnityEditor;

public class NewEditorScript1 : ScriptableObject
{
    [MenuItem("Tools/put tiles")]
    static void DoIt()
    {
        //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        GameObject tile = Resources.Load("Prefabs/Cell") as GameObject;
        GameObject board = GameObject.Find("Board");
        for (int i = 0; i < 10; i++)
        {
             for (int j = 0; j < 10; j++)
             {
               GameObject tmp = GameObject.Instantiate(tile, new Vector3(100 * i, 100 * j, 0), Quaternion.identity) as GameObject;
               tmp.name = "Cell" + i + j;

               tmp.transform.SetParent(board.transform, false);
            }
        }
    }
}