using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public interface IAssetLoader
    {
        GameObject Instantiate(string path, Transform parent);
    }
}