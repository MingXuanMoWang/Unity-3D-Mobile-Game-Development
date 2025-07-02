using UnityEditor;
using UnityEngine;

public class ProcessModel : MonoBehaviour
{
    [MenuItem("Tools/Create Prefab")]
    static void CreatePrefab()
    {
        // ��ȡѡ�еĶ��󣨼������Ѿ�ѡ����һ��3Dģ�ͣ�
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            // ����Prefab����·��
            string path = "Assets/Prefabs/" + selectedObject.name + ".prefab";

            // ����Prefab
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, path);

            prefab.AddComponent<Rigidbody>();

            // ���������Prefab·��
            Debug.Log("Prefab Created at: " + path);
        }
        else
        {
            Debug.LogError("Please select a GameObject to create a Prefab.");
        }
    }
}
