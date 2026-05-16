using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody rb;
    private bool IsJumping;
    private bool isGrounded;

    public CameraMove camScript; // Arrastra
    public float shakeAmount = 0.05f; // Qué tanto vibra

    // Referencia al Animator
    private Animator anim;
    private AudioSource audioSource;
    public ParticleSystem particulasCaida;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Buscamos el Animator en el mismo objeto o en los hijos
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            // Se activa una sola vez
            anim.SetTrigger("Jump");
            particulasCaida.Play();
            
            anim.SetBool("isGrounded", false);
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

        // --- LÓGICA DE ANIMACIÓN ---

        // Si la magnitud del movimiento es mayor a 0, está corriendo
        bool moving = moveDirection.magnitude > 0.1f;
        anim.SetBool("IsMoving", moving);
        var emission = particulasCaida.emission;

        // Solo emite si está en el suelo Y se está moviendo
        if (isGrounded && moving)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
        camScript.isShaking = (isGrounded && moving);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
           
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
    }

}