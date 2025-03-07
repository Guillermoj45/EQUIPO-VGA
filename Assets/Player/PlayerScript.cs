using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
public class PlayerController : MonoBehaviour
{
    private readonly int _spawn = Animator.StringToHash("spawn");
    private readonly int _jump1 = Animator.StringToHash("jump");
    private readonly int _corriendo = Animator.StringToHash("corriendo");


   // Velocidad de movimiento del personaje
   public float movementSpeed;
   // Velocidad máxima que el personaje puede alcanzar
   public float maxSpeed;

   //Tamaño del área para detectar el suelo
   public Vector3 areaSize;
   //Variacion respecto al posicionamiento del área
   public Vector3 areaOffset;

    //Si true, el personaje toca suelo
    public bool grounded;
   //Aquí se recogen todos los objetos que se encuentran dentro del área dedetección del suelo.
   //Si hay uno o más, el personaje está tocando suelo
   public Collider2D[] items;
   //Para que solo se detecte esteLayer
   public LayerMask groundLayer;

   //Fuerza de salto
   public float jumpForce;

   // Referencia al Rigidbody2D
   private Rigidbody2D _rB;
   // Referencia al Animator
   private Animator _anim;

   // Referencia al contador de cerezas
   public CerezasController contadorCerezas;

   // Referencia al contador de piñas
   public PinaController contadorPinas;

    // spawn point 
    public Transform spawnPoint;
        
    // sonidos del personaje
    public AudioClip maullidoSpawn;

        // Los putos pies de los cojones
        public PiesDetector piesDetector;




   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
{
  // Recuperación de referencias
  _rB = GetComponent<Rigidbody2D>();
  _anim = GetComponent<Animator>();
  AudioSource.PlayClipAtPoint(maullidoSpawn, transform.position);

       piesDetector = GetComponentInChildren<PiesDetector>();
       if (piesDetector == null)
           Debug.Log("Y una vagina");

}
   
   void FixedUpdate()
   {
    Move();
   }
   
        void Update()
        {
            Jump();
            colisiones();
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            // CheckGrounded();
            grounded = false;
        }
        

   void Move()
   {
       // Recuperamos el valor del eje horizontal
       float x = Input.GetAxisRaw("Horizontal");
       // Generamos el vector de fuerza a aplicar
       Vector2 force = new Vector2(x * movementSpeed, 0f);
       if (_rB.linearVelocity.magnitude < maxSpeed)
       {
           // Aplicamos la fuerza al Rigidbody2D
           _rB.AddForce(force, ForceMode2D.Impulse);
       }

       // Aplicamos un freno si no se está presionando un botón de dirección
       _rB.linearDamping = (x == 0 && grounded) ? 5f : 1f;

       //Ajustamos parámetro del animator
       _anim.SetBool(_corriendo, x !=0);
       //Hacemos que la persona mire hacia donde se mueve
       FaceDirection(x);
   }

   void FaceDirection(float x){
     //Dependiendo del valor de desplazamiento en x ajustamos la escala del personaje en -1 o 1
     if(x<0){
       transform.localScale = new Vector3(-Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
     } else if(x>0){
      transform.localScale = new Vector3(Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
     }
   }

   void colisiones()
   {

        MuerteControlador controladorMuerte = GetComponentInChildren<MuerteControlador>();

        if(controladorMuerte.estaMuerto){
            Spawn();
            controladorMuerte.estaMuerto = false;
        }
   }

   private void OnDrawGizmos(){
    Gizmos.color = Color.magenta; //color para el área de detección
    //Pide un centro y un tamaño
    Gizmos.DrawCube(transform.position + areaOffset, areaSize);
   }

   
    private void Jump(){
            
        //saltamos si hemos pulsado el botón de salto y si estamos tocando suelo
        if (Input.GetKeyDown(KeyCode.Space))// && grounded)
        {
                Debug.Log("Hola" + piesDetector.collidingElements.Count);
                
                foreach (Collider2D collider in piesDetector.collidingElements)
                {
                    Debug.Log("Hola1212" + collider.gameObject.tag);
                    if (collider.gameObject.CompareTag("Ground"))
                    {
                        _anim.SetTrigger(_jump1);  // Activa la animación de salto
                        _rB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        break;
                    }
                }
        }
            
    }

        

    //void CheckGrounded()
    //{
    //    // Verifica si hay colisiones en el área definida
    //    items = Physics2D.OverlapBoxAll(transform.position + areaOffset, areaSize, 0f, groundLayer);
    //    grounded = false;
    //    foreach (var item in items)
    //    {
    //        if (item.CompareTag("Ground"))
    //        {
    //            grounded = true;
    //            break;
    //        }
    //    }
    //    Debug.Log($"Grounded: {grounded}, Items: {items.Length}");
    //}
        
    // Se activa cuando el sensor (collider trigger) entra en contacto con otro collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Comprueba si el objeto con el que colisiona tiene el tag "Ground"
  
        if (collision.gameObject.CompareTag("limite"))
        {
            Spawn();
        }
        if(collision.gameObject.CompareTag("Final") && contadorCerezas.cantidadCerezas == 3)
        {           
                    contadorCerezas.cantidadCerezas = 0;
                    SceneManager.LoadScene(2);
                    Debug.Log("Escena actual: " + SceneManager.GetActiveScene().buildIndex);
        }
        if(collision.gameObject.CompareTag("Final2") && contadorPinas.cantidadPina == 3)
        {
                    SceneManager.LoadScene(3);
                    Debug.Log("Escena actual: " + SceneManager.GetActiveScene().buildIndex);
        }
    }
    // // Se activa cuando el sensor deja de colisionar con otro collider
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (!collision.gameObject.CompareTag("Ground"))
    //     {
    //         grounded = false;
    //     }
// 
    // }

    private void Spawn()
    {
        // Reubicar al gato en el punto de respawn
        transform.position = spawnPoint.position;

        // Obtener el componente Rigidbody2D del gato
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Reiniciar la velocidad lineal y angular para evitar que el gato siga moviéndose
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        AudioSource.PlayClipAtPoint(maullidoSpawn, transform.position);
        _anim.SetTrigger(_spawn);
    }
}
}
