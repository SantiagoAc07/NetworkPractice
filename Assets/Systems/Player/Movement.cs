using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    public GameObject playerCamera;

    private Rigidbody _rb;
    
    private PhotonView _photonView;
    

    private Vector3 moveDirection;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
    }
    
    void Start()
    {
        if (!_photonView.IsMine)
        {
            playerCamera.SetActive(false); // Apagar la cámara si no es nuestro jugador
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine) return; // Solo mueve el jugador local
        MoveDirection();
        Move();
    }


    private void MoveDirection()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.z = Input.GetAxisRaw("Vertical");

        moveDirection.y = 0;

        moveDirection *= speed;
    }

    private void Move()
    {
        _rb.linearVelocity = new Vector3(moveDirection.x, _rb.linearVelocity.y, moveDirection.z);
    }
}
