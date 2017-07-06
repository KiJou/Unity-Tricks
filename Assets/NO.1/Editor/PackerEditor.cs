using UnityEngine;
using UnityEditor;
using System.IO;

public static class PackerEditor
{
    [MenuItem("Assets/Packer")]
    static void Packer()
    {
        // 获取旋转对象
        Texture2D image = Selection.activeObject as Texture2D;
        // 目录路径名称
        string dirPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(image));
        // 图片路径名称
        string imgPath = dirPath + "/" + image.name + ".png";
        // 资源导入器
        TextureImporter texImp = AssetImporter.GetAtPath(imgPath) as TextureImporter;
        // 创建文件夹
        AssetDatabase.CreateFolder(dirPath, image.name);

        // 遍历小图集
        foreach (SpriteMetaData metaData in texImp.spritesheet)
        {
            Texture2D mImage = new Texture2D((int)metaData.rect.width, (int)metaData.rect.height);
            // Y轴像素
            for (int y = (int)metaData.rect.y; y < metaData.rect.y + metaData.rect.height; y++)
            {
                // X轴像素
                for (int x = (int)metaData.rect.x; x < metaData.rect.x + metaData.rect.width; x++)
                {
                    // 设置像素
                    mImage.SetPixel(x - (int)metaData.rect.x, y - (int)metaData.rect.y, image.GetPixel(x, y));
                }
            }
            // 转换纹理到EncodeToPNG兼容格式
            if (mImage.format != TextureFormat.ARGB32 && mImage.format != TextureFormat.RGB24)
            {
                Texture2D newTexture = new Texture2D(mImage.width, mImage.height);
                newTexture.SetPixels(mImage.GetPixels(0), 0);
                mImage = newTexture;
            }
            var pngData = mImage.EncodeToPNG();
            File.WriteAllBytes(dirPath + "/" + image.name + "/" + metaData.name + ".png", pngData);
        }
    }
}
