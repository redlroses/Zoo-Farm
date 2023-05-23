using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssetProvider : IService
  {
    void Cleanup();
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path, Transform inside);
    GameObject Instantiate(string path);
  }
}