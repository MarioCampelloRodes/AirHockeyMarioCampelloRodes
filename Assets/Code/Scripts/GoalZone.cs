using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Librería para poder acceder a los TextMeshPro

public class GoalZone : MonoBehaviour
{
    //Referencia para acceder al marcador de puntos
    public TextMeshProUGUI pointsLeft, pointsRight, pointsTop, pointsBottom;
    //Variable para guardar los puntos marcados en esa portería
    public int score;

    //Referencia al GameManager
    public GameManager gMReference;

    public Disk diskRef;

    //Antes de que empiece el juego
    private void Awake()
    {
        //Ponemos la puntuación en 0
        score = 0;
        //Cambiamos el texto de la puntuación al valor que tenga en ese momento el score
        pointsLeft.text = score.ToString();
        pointsRight.text = score.ToString();
        pointsTop.text = score.ToString();
        pointsBottom.text = score.ToString();

        //Para transformar un int en un string hay 3 maneras
        //scoreText.text = score + ""; le sumo un string vacío a ese int, luego ya será todo un string
        //scoreText.text = score.ToString(); transformo(cast) del int en un string
        //scoreText.text = "Score: " + score; a un string le ponemos después un int, con lo cuál ya todo es string
    }

    //Método para detectar cuando algo ha entrado en el trigger(zona de gol)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Solo aquellos GameObjects etiquetados como Disco, que hayan entrado en el trigger
        if (collision.CompareTag("Disco"))
        {
            //Sumo 1 a la puntuación
            score++;
            //Si la puntuación es mayor de 9
            if (score > 9)
            {
                //Ejecuto el método que hace que se pase a otra ronda
                gMReference.GoalScored();
                //Ejecuto el método que termina esta partida
                gMReference.WinGame();
            }
            //Si no
            else
            {
                if(diskRef.lastCollisionRacket == "PlayerLeft" && gameObject.name != "GoalLeft")
                {
                    //Cambiamos el texto de la puntuación al valor que tenga en ese momento el score
                    pointsLeft.text = score.ToString();
                    //Ejecuto el método que hace que se pase a otra ronda
                    gMReference.GoalScored();
                }

                if (diskRef.lastCollisionRacket == "PlayerRight" && gameObject.name != "GoalRight")
                {
                    //Cambiamos el texto de la puntuación al valor que tenga en ese momento el score
                    pointsRight.text = score.ToString();
                    //Ejecuto el método que hace que se pase a otra ronda
                    gMReference.GoalScored();
                }

                if (diskRef.lastCollisionRacket == "PlayerTop" && gameObject.name != "GoalTop")
                {
                    //Cambiamos el texto de la puntuación al valor que tenga en ese momento el score
                    pointsTop.text = score.ToString();
                    //Ejecuto el método que hace que se pase a otra ronda
                    gMReference.GoalScored();
                }

                if (diskRef.lastCollisionRacket == "PlayerBottom" && gameObject.name != "GoalBottom")
                {
                    //Cambiamos el texto de la puntuación al valor que tenga en ese momento el score
                    pointsBottom.text = score.ToString();
                    //Ejecuto el método que hace que se pase a otra ronda
                    gMReference.GoalScored();
                }
            }
        }
    }
}
