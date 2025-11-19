using Common.Data;
using Services;
using UnityEngine;

public class TeleporterObject : MonoBehaviour
{
    public int ID;
    private Mesh mesh = null;

    // Use this for initialization
    private void Start()
    {
        this.mesh = this.GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    private void Update()
    {
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (this.mesh == null)
        {
            Gizmos.DrawWireMesh(this.mesh, this.transform.position + Vector3.up * this.transform.localScale.y * .5f, this.transform.rotation, this.transform.localScale);
        }
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.ArrowHandleCap(0, this.transform.position, this.transform.rotation, 1f, EventType.Repaint);
    }

#endif

    private void OnTriggerEnter(Collider other)
    {
        PlayerInputController playerInputController = other.GetComponent<PlayerInputController>();
        if (playerInputController != null && playerInputController.isActiveAndEnabled)
        {
            TeleporterDefine teleporterDefine = DataManager.Instance.Teleporters[this.ID];
            if (teleporterDefine == null)
            {
                Debug.LogFormat("TelePorterObject: Character [{0}] Enter Teleporter [{1}], But TeleporterDefine not existed", playerInputController.character.Info.Name, this.ID);
                return;
            }
            Debug.LogFormat("TelePorterObject: Character [{0}] Enter Teleporter [{1}:{2}]", playerInputController.character.Info.Name, teleporterDefine.ID, teleporterDefine.Name);
            if (teleporterDefine.LinkTo > 0)
            {
                if (DataManager.Instance.Teleporters.ContainsKey(teleporterDefine.LinkTo))
                    MapService.Instance.SendMapTeleporter(this.ID);
                else
                    Debug.LogFormat("Teleporter ID:{0} LinkID {1} error!", teleporterDefine.ID, teleporterDefine.LinkTo);
            }
        }
    }
}