
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player))]


public class PlayerSetup : NetworkBehaviour {

  
    [SerializeField]
    Behaviour[] componetsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    [SerializeField]
    GameObject playerGraphics;

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    Camera scaneCamera;


    void Start ()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();

        }
        else
        {
            scaneCamera = Camera.main;
            if (scaneCamera != null)
            {
                scaneCamera.gameObject.SetActive(false);

            }
            for (int i = 0; i < componetsToDisable.Length; i++)
            {
                componetsToDisable[i].enabled = true;
            }

            //disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //create playerui
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

        }

        GetComponent<Player>().Setup();



    }


    void SetLayerRecursively (GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents ()
    {

        for (int i = 0; i < componetsToDisable.Length; i++)
        {
            componetsToDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if (scaneCamera != null)
        {
            scaneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }

}
