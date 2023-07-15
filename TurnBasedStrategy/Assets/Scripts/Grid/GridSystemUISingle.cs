using UnityEngine;

public class GridSystemUISingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void EnableMeshRenderer()
    {
        meshRenderer.enabled = true;
    }

    public void DisableMeshRenderer()
    {
        meshRenderer.enabled = false; 
    }
}