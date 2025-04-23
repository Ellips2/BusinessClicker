using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetLoader : IAssetLoader
    {
        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load(path);
            var businessNodeObj = (GameObject) Object.Instantiate(prefab, parent);
            return businessNodeObj;
        }
    }
}