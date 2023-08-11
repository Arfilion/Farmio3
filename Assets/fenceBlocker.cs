using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fenceBlocker : MonoBehaviour
{
    
   
    private MeshRenderer meshRenderer;
    private Collider colliderComponent;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliderComponent = GetComponent<Collider>();
        DisableRendererAndCollider();
    }

    public void Update()
    {
        if (boss.instance != null)
        {
            EnableRendererAndCollider();
        }
    }
    public void DisableRendererAndCollider()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false; 
        }

        if (colliderComponent != null)
        {
            colliderComponent.enabled = false; 
        }
    }

    public void EnableRendererAndCollider()
    {
        if (meshRenderer != null)
        {
            meshRenderer.enabled = true;
        }

        if (colliderComponent != null)
        {
            colliderComponent.enabled = true;
        }
    }

}
