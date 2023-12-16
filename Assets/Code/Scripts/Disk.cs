using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    //Velocidad del disco
    public float diskSpeed = 8f;

    //Esto es una referencia al RigidBody del disco que nos permite cambiar su velocidad
    public Rigidbody2D rb;

    public string lastCollisionRacket, previousCollisionRacket;

    // Start is called before the first frame update
    void Start()
    {
        //El disco empieza con una velocidad que lo impulsa a la derecha
        rb.velocity = Vector2.right * diskSpeed; //Equivale a new Vector2(1, 0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * El objeto collision del par�ntesis contiene la informaci�n del choque
     * En particular, nos interesa saber cuando choca contra una pala.
     * -collision.gameObject: tiene informaci�n del objeto contra el cu�l ha colisionado (la pala)
     * -collision.transform.position: tiene informaci�n de la posici�n (pala)
     * -collision.collider: tiene informaci�n del collider (pala)
     */

    /* Es un m�todo de Unity que detecta colisi�n entre dos GO.
     * Al chocar el disco contra el objeto que choca le pasa su Colisi�n por par�metro */
    private void OnCollisionEnter2D(Collision2D collision) //El par�metro collision es el objeto que ha chocado contra el disco
    {
        previousCollisionRacket = lastCollisionRacket;

        //Si el disco colisiona con la pala izquierda
        if(collision.gameObject.name == "PlayerLeft")
        {
            //Obtenemos el factor de golpeo, pas�ndole la posici�n del disco, la posici�n de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float yF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            /*Le damos una nueva direcci�n al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(1, yF).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
        //Si el disco colisiona con la pala derecha
        if (collision.gameObject.name == "PlayerRight")
        {
            //Obtenemos el factor de golpeo, pas�ndole la posici�n del disco, la posici�n de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float yF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            /*Le damos una nueva direcci�n al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(-1, yF).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
        
        if (collision.gameObject.name == "PlayerTop")
        {
            //Obtenemos el factor de golpeo, pas�ndole la posici�n del disco, la posici�n de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float xF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            /*Le damos una nueva direcci�n al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(-xF, -1).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
        
        if (collision.gameObject.name == "PlayerBottom")
        {
            //Obtenemos el factor de golpeo, pas�ndole la posici�n del disco, la posici�n de la pala, y lo que mide de alto el collider de la pala (es decir, lo que mide la pala)
            float xF = HitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.x);
            /*Le damos una nueva direcci�n al disco
             * En este caso con una X a la derecha
             * Y nuestro factor de golpeo calculado
             * Normalizado todo el vector a 1, para que la bola no acelere */
            Vector2 direction = new Vector2(-xF, 1).normalized; //Hacemos que no acelere en su movimiento en diagonal
            //Le decimos al disco que salga con esa velocidad previamente calculada
            rb.velocity = direction * diskSpeed;
        }
        
        lastCollisionRacket = collision.gameObject.name;
    }

    /*
     * 1 - El disco choca contra la parte superior de la pala
     * 0 - El disco choca contre el centro de la pala
     * -1 - El disco choca contra la parte inferior de la pala
     */
    /*
     * Es un m�todo de tipo 3. En este caso le pasamos 3 par�metros:
     * -posici�n actual del disco
     * -posici�n actual de la pala
     * -altura de la pala
     * Y el m�todo tal y como le indicamos nos devuelve una variable de tipo float
     */
    private float HitFactor(Vector2 diskPosition, Vector2 racketPosition, float racketHeight)
    {
        return (diskPosition.y - racketPosition.y) / racketHeight;
    }
}
