using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Photon.Pun;

[CreateAssetMenu(fileName = "New Master Manger", menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    #region Private Variables
    [SerializeField] private GameSettings _gameSettings = default;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }
    [SerializeField] private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();
    #endregion

    #region My Functions
    public static GameObject NetworkInstantiate(GameObject obj, Vector3 pos, Quaternion rot)
    {
        foreach (NetworkedPrefab networkedPrefab in Instance._networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    GameObject results = PhotonNetwork.Instantiate(networkedPrefab.Path, pos, rot);
                    return results;
                }
                else
                {
                    Debug.LogError($"Path is empty for gameobject name{networkedPrefab.Prefab}");
                    return null;
                }
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR
        Instance._networkedPrefabs.Clear();

        GameObject[] results = Resources.LoadAll<GameObject>("");

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                string path = AssetDatabase.GetAssetPath(results[i]);
                Instance._networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }
#endif
    }
    #endregion
}