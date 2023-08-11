using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fenceBlocker : MonoBehaviour
{
    public GameObject objectToToggle;

    private MeshRenderer meshRenderer;
    private Collider collider;

    private void Start()
    {
        // Obtener los componentes MeshRenderer y Collider del objeto
        meshRenderer = objectToToggle.GetComponent<MeshRenderer>();
        collider = objectToToggle.GetComponent<Collider>();

        // Asegurarse de que el objeto comienza deshabilitado
        if (meshRenderer != null)
            meshRenderer.enabled = false;

        if (collider != null)
            collider.enabled = false;
    }
    public void Update()
    {
        if (boss.instance != null)
        {
            ToggleObjectVisibility();
        }
    }

    public void ToggleObjectVisibility()
    {
        // Habilitar o deshabilitar el MeshRenderer y el Collider según su estado actual
        if (meshRenderer != null)
            meshRenderer.enabled = true;
        if (collider != null)
            collider.enabled = true;
    }
}
