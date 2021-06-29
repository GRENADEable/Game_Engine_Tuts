using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    #region Private Variables
    public TextMeshProUGUI displayNameText = null;
    public Renderer displayColourRenderer = null;

    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField] private string displayName = "Nil Name";

    [SyncVar(hook = nameof(HandleDisplayColourUpdated))]
    [SerializeField] private Color displayColour = Color.black;
    #endregion

    #region Functions

    #region Server
    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColour(Color newDisplayColor)
    {
        displayColour = newDisplayColor;
    }

    [Command]
    void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length < 2 || newDisplayName.Length > 16)
            return;

        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }
    #endregion

    #region Client
    void HandleDisplayColourUpdated(Color oldColour, Color newColour)
    {
        displayColourRenderer.material.SetColor("_BaseColor", newColour);
    }

    void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }
    #endregion

    #endregion
}