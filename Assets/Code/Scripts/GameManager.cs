using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Librer�a para cambiar entre escenas
using TMPro; //Librer�a para poder usar los TextMeshPro

public class GameManager : MonoBehaviour
{
    //Referencia para guardar la direcci�n del disco
    Vector2 direction;
    //Referencia al objeto del disco
    public GameObject disk;
    //Referencia para las palas
    public GameObject playerLeft, playerRight, playerTop, playerBottom;
    //Referencia para el texto del ganador
    public GameObject panelWin;

    //Referencias a las porterias
    public GameObject goalLeft, goalRight, goalTop, goalBottom;
    //Referencia para acceder al cartel de ganar
    public TextMeshProUGUI winText;

    //Referencia al script del disco
    public Disk diskRef;

    public string lastCollision;

    //M�todo para hacer lo que ocurre al marcar un punto
    public void GoalScored()
    {
        //Ponemos el disco al marcar un gol en la posici�n de origen
        disk.transform.position = Vector2.zero; //Vector2.zero <-> new Vector2(0,0)
        //Ponemos a los jugadores en sus posiciones de origen
        playerLeft.transform.position = new Vector2(-5f, 0f);
        playerRight.transform.position = new Vector2(5f, 0f);
        playerTop.transform.position = new Vector2(0f, 4.83f);
        playerBottom.transform.position = new Vector2(0f, -4.83f);

        lastCollision = diskRef.lastCollisionRacket;

        if (lastCollision == "PlayerLeft" || lastCollision == "PlayerRight")
        {
            //Aqu� guardamos la velocidad en X que llevaba el disco e invertimos su signo
            direction = new Vector2(-disk.GetComponent<Rigidbody2D>().velocity.x, 0f);

            //Paramos el disco
            disk.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else if (lastCollision == "PlayerTop" || lastCollision == "PlayerBottom")
        {
            direction = new Vector2(0f, -disk.GetComponent<Rigidbody2D>().velocity.x);

            disk.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        //Usando Invoke esperamos X segundos antes de llamara un m�todo
        Invoke("LaunchDisk", 2.0f); //Le decimos el m�todo que quiero invocar y el tiempo que tiene que pasar en segundos para que eso suceda
    }

    //M�todo para hacer que el disco se lance
    void LaunchDisk()
    {
        //Aplicamos esa nueva direcci�n en el disco
        disk.GetComponent<Rigidbody2D>().velocity = direction * diskRef.diskSpeed;
    }

    //M�todo para resetear el juego cuando uno gana
    public void WinGame()
    {
        //SetActive sirve para activar o desactivar objetos
        panelWin.SetActive(true);
        //Si la puntuaci�n que tenemos guardada en esa porter�a es mayor de 9
        if (goalLeft.GetComponent<GoalZone>().score > 9)
            winText.text = "El Jugador Rojo ha ganado!!";
        else if(goalRight.GetComponent<GoalZone>().score > 9)
            winText.text = "El Jugador Morado ha ganado!!";
        else if (goalTop.GetComponent<GoalZone>().score > 9)
            winText.text = "El Jugador Verde ha ganado!!";
        else if (goalBottom.GetComponent<GoalZone>().score > 9)
            winText.text = "El Jugador Azul ha ganado!!";
        //Esperamos 2 segundos antes de ir a la pantalla del t�tulo
        Invoke("GoMenu", 2f);
    }

    //M�todo para ir a la pantalla de t�tulo
    public void GoMenu()
    {
        //Vamos a la escena de t�tulo
        SceneManager.LoadScene("MainMenu");
    }
}
