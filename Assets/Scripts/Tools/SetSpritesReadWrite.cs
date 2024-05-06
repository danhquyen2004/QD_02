using UnityEngine;
using UnityEditor;

public class SetSpritesReadWrite : MonoBehaviour
{
    [MenuItem("Tools/Set Sprites Read_Write")]
    private static void SetSpritesReadWritable()
    {
        string[] guids = AssetDatabase.FindAssets("t:Sprite");
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (textureImporter != null)
            {
                textureImporter.isReadable = true;
                AssetDatabase.ImportAsset(assetPath);
            }
        }
        Debug.Log("All sprites set to Read/Write.");
    }
}
