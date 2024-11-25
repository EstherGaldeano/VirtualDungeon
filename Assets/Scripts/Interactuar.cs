using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactuar : MonoBehaviour
{
    public float rayDistance; // Distancia m�xima del raycast
    public LayerMask layerMask; // Selecciona las capas que deseas detectar en el Inspector

    private Material originalMaterial = null;
    private Color originalColor;
    private Color originalEmissionColor;
    private GameObject lastHitObject = null; // Guardamos el �ltimo objeto detectado

    public GameObject dungeonDoor1;
    public GameObject dungeonDoor2;
    private bool dungeonDoorsClosed;


    private void Start()
    {
        dungeonDoorsClosed = true;

        Scene currentScene = SceneManager.GetActiveScene();
        if(currentScene.name == "FirstPerson" || currentScene.name == "Shooter")
        {
            rayDistance = 3f;
        }
        else
        {
            rayDistance = 1000000f;
        }
    }
    void Update()
    {
        // Obtener la posici�n del rat�n en la pantalla (en p�xeles)
        Vector3 mousePosition = Input.mousePosition;

        // Convertir la posici�n del rat�n en un rayo en el espacio 3D usando la c�mara principal
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Realizar el raycast usando el LayerMask
        if (Physics.Raycast(ray, out RaycastHit hitObject, rayDistance, layerMask))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(hitObject.collider.gameObject.tag == "DungeonChest")
                {
                    DungeonDoors();
                }

                Animator anim = hitObject.collider.GetComponent<Animator>();
             
                if (anim == null)
                {
                    anim = hitObject.collider.GetComponentInParent<Animator>();
                }
                
                bool currentState = anim.GetBool("Accion");
                anim.SetBool("Accion", !currentState);

            }

            // Si el objeto detectado no tiene material, saltamos
            MeshRenderer renderer = hitObject.collider.gameObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                // Si estamos raycasting sobre un objeto diferente al �ltimo, restauramos el anterior
                if (lastHitObject != hitObject.collider.gameObject)
                {
                    // Restaurar el estado del �ltimo objeto, si existe
                    if (lastHitObject != null)
                    {
                        MeshRenderer lastRenderer = lastHitObject.GetComponent<MeshRenderer>();
                        if (lastRenderer != null)
                        {
                            // Restaurar color y emisi�n del objeto anterior
                            lastRenderer.material.color = originalColor;
                            if (lastRenderer.material.HasProperty("_EmissionColor"))
                            {
                                lastRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
                                lastRenderer.material.DisableKeyword("_EMISSION"); // Desactivar emisi�n
                            }
                        }
                    }

                    // Ahora guardamos los valores originales del nuevo objeto
                    originalMaterial = renderer.material;
                    originalColor = renderer.material.color;

                    if (renderer.material.HasProperty("_EmissionColor"))
                    {
                        originalEmissionColor = renderer.material.GetColor("_EmissionColor");
                    }

                    // Aplicar el nuevo color de emisi�n (cambiar a color claro, por ejemplo, amarillo)
                    renderer.material.EnableKeyword("_EMISSION");
                    renderer.material.SetColor("_EmissionColor", Color.yellow * 0.5f); // Ajusta la intensidad del color de emisi�n

                    // Marcar que estamos raycasting sobre este nuevo objeto
                    lastHitObject = hitObject.collider.gameObject;
                }
            }
        }
        else
        {
            // Si el raycast ya no detecta el objeto, restaurar los valores originales del objeto anterior
            if (lastHitObject != null)
            {
                MeshRenderer lastRenderer = lastHitObject.GetComponent<MeshRenderer>();
                if (lastRenderer != null)
                {
                    // Restaurar el color original y desactivar la emisi�n
                    lastRenderer.material.color = originalColor;
                    if (lastRenderer.material.HasProperty("_EmissionColor"))
                    {
                        lastRenderer.material.SetColor("_EmissionColor", originalEmissionColor);
                        lastRenderer.material.DisableKeyword("_EMISSION"); // Desactivar emisi�n
                    }
                }

                // Limpiar el objeto actual al que se dej� de hacer raycast
                lastHitObject = null;
            }
        }

       


    }

    public void DungeonDoors()
    {
        if (dungeonDoorsClosed)
        {
            dungeonDoor1.GetComponent<Animator>().SetBool("Accion", true);
            dungeonDoor2.GetComponent<Animator>().SetBool("Accion", true);

            dungeonDoorsClosed = false;
        }
    }

   
}
