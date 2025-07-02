using UnityEditor;
using UnityEngine;

public class ProcessModel : MonoBehaviour
{
    [MenuItem("Tools/Create Prefab")]
    static void CreatePrefab()
    {
        // 获取选中的对象（假设你已经选中了一个3D模型）
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject != null)
        {
            // 设置Prefab保存路径
            string path = "Assets/Prefabs/" + selectedObject.name + ".prefab";

            // 创建Prefab
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(selectedObject, path);

            prefab.AddComponent<Rigidbody>();

            // 输出创建的Prefab路径
            Debug.Log("Prefab Created at: " + path);
        }
        else
        {
            Debug.LogError("Please select a GameObject to create a Prefab.");
        }
    }
}
