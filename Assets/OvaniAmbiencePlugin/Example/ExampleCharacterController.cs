using UnityEngine;

public class ExampleCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    private CharacterController characterController;
    private Transform cameraTransform;

    private float footTimer = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = transform.GetChild(0);
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        
    }
    [SerializeField] AudioSource footPlayer;
    [SerializeField] AudioClip grassClip;
    [SerializeField] AudioClip woodClip;
    [SerializeField] AudioClip metalClip;
    public Material GetMaterialFromHit(RaycastHit hit)
    {
        // Check if the hit object has a MeshRenderer component
        MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // Get the mesh collider and the triangle index hit
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider != null && meshCollider.sharedMesh != null)
            {
                Mesh mesh = meshCollider.sharedMesh;
                int triangleIndex = hit.triangleIndex;
                int subMeshIndex = -1;

                // Determine which sub-mesh was hit by checking the triangle index range
                int[] triangles;
                for (int i = 0; i < mesh.subMeshCount; i++)
                {
                    triangles = mesh.GetTriangles(i);
                    if (triangleIndex * 3 < triangles.Length)
                    {
                        subMeshIndex = i;
                        break;
                    }
                    triangleIndex -= triangles.Length / 3;
                }

                // Return the material from the corresponding sub-mesh index
                if (subMeshIndex != -1 && subMeshIndex < meshRenderer.sharedMaterials.Length)
                {
                    return meshRenderer.sharedMaterials[subMeshIndex];
                }
            }
        }
        // Return null if no MeshRenderer component or no matching sub-mesh is found
        return null;
    }
    void HandleMovement()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        if (move != Vector3.zero)
            footTimer += Time.deltaTime;
        else footTimer = 0;

        move += Vector3.down * .98f * 2;

        if (characterController.isGrounded && footTimer > .5f && Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit))
        {
            footTimer = 0;

            try
            {
                Material mat = GetMaterialFromHit(hit);
                switch (mat.name)
                {
                    case "Grass":
                        footPlayer.PlayOneShot(grassClip);
                        break;
                    case "Wood":
                        footPlayer.PlayOneShot(woodClip);
                        break;
                    case "Metal":
                        footPlayer.PlayOneShot(metalClip);
                        break;
                    default:
                        break;
                }
            }
            catch {  }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
                }
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        characterController.Move((move * moveSpeed * Time.deltaTime));
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        cameraTransform.Rotate(Vector3.right * -mouseY);
    }
}
