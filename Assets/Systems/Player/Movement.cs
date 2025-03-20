using Photon.Pun;
using UnityEngine;

namespace Systems.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(PhotonView))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
    
        

        private Rigidbody _rb;
    
        private PhotonView _photonView;
    

        private Vector3 _moveDirection;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _photonView = GetComponent<PhotonView>();
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
            _moveDirection.x = Input.GetAxisRaw("Horizontal");
            _moveDirection.z = Input.GetAxisRaw("Vertical");

            _moveDirection.y = 0;

            _moveDirection *= speed;
        }

        private void Move()
        {
            _rb.linearVelocity = new Vector3(_moveDirection.x, _rb.linearVelocity.y, _moveDirection.z);
        }
    }
}
