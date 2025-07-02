using UnityEditor;
using UnityEngine;

public class SpritePrefabCreator
{
    [MenuItem("Tools/Create Prefab From Sprite")]
    static void CreateSpritePrefab()
    {
        // ��ȡ��ǰѡ�еĶ���
        Object selected = Selection.activeObject;

        // ����Ƿ���Sprite��Texture2D
        if (selected is Sprite || selected is Texture2D)
        {
            string assetPath = AssetDatabase.GetAssetPath(selected);

            // ��ȡSprite��Դ�������Texture2D����л�ȡSprite��
            Sprite sprite = null;
            if (selected is Sprite)
            {
                sprite = (Sprite)selected;
            }
            else if (selected is Texture2D)
            {
                // ��ȡ����ͼ�µ���������Դ����ΪSprite��Texture2D�£�
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
                Debug.LogError("�޷���ѡ�е���Դ����ȡSprite����ȷ����ͼ������ΪSprite���͡�");
                return;
            }

            // ����һ����SpriteRenderer����GameObject
            GameObject go = new GameObject(sprite.name);
            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            // ȷ��PrefabĿ¼����
            string prefabDir = "Assets/Prefabs/";
            if (!AssetDatabase.IsValidFolder(prefabDir))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }

            // ����������Prefab
            string prefabPath = prefabDir + sprite.name + ".prefab";
            PrefabUtility.SaveAsPrefabAsset(go, prefabPath);

            // ������ʱGameObject
            Object.DestroyImmediate(go);

            Debug.Log("Prefab�Ѵ���: " + prefabPath);
        }
        else
        {
            Debug.LogError("��ѡ��һ��Sprite��Texture2D���͵�ͼƬ��Դ��");
        }
    }
}
