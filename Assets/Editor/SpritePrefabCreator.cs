using UnityEditor;
using UnityEngine;

public class SpritePrefabCreator
{
    [MenuItem("Tools/Create Prefab From Sprite")]
    static void CreateSpritePrefab()
    {
        // 获取当前选中的对象
        Object selected = Selection.activeObject;

        // 检查是否是Sprite或Texture2D
        if (selected is Sprite || selected is Texture2D)
        {
            string assetPath = AssetDatabase.GetAssetPath(selected);

            // 获取Sprite资源（如果是Texture2D则从中获取Sprite）
            Sprite sprite = null;
            if (selected is Sprite)
            {
                sprite = (Sprite)selected;
            }
            else if (selected is Texture2D)
            {
                // 获取该贴图下的所有子资源（因为Sprite在Texture2D下）
                Object[] subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);
                foreach (Object subAsset in subAssets)
                {
                    if (subAsset is Sprite)
                    {
                        sprite = (Sprite)subAsset;
                        break;
                    }
                }
            }

            if (sprite == null)
            {
                Debug.LogError("无法从选中的资源中提取Sprite。请确保贴图已设置为Sprite类型。");
                return;
            }

            // 创建一个带SpriteRenderer的新GameObject
            GameObject go = new GameObject(sprite.name);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            // 确保Prefab目录存在
            string prefabDir = "Assets/Prefabs/";
            if (!AssetDatabase.IsValidFolder(prefabDir))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }

            // 创建并保存Prefab
            string prefabPath = prefabDir + sprite.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);

            // 清理临时GameObject
            Object.DestroyImmediate(go);

            Debug.Log("Prefab已创建: " + prefabPath);
        }
        else
        {
            Debug.LogError("请选择一个Sprite或Texture2D类型的图片资源。");
        }
    }
}
