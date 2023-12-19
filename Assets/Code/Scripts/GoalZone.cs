using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Librer�a para poder acceder a los TextMeshPro

public class GoalZone : MonoBehaviour
{
    //Referencia para acceder al marcador de puntos
    public TextMeshProUGUI pointsLeft, pointsRight, pointsTop, pointsBottom;

    //Referencia al GameManager
    public GameManager gMRef;

    //Referencia al script del disco
    public Disk diskRef;

    //Antes de que empiece el juego
    private void Awake()
    {
        //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
        pointsLeft.text = gMRef.leftScore.ToString();
        pointsRight.text = gMRef.rightScore.ToString();
        pointsTop.text = gMRef.topScore.ToString();
        pointsBottom.text = gMRef.bottomScore.ToString();

        //Para transformar un int en un string hay 3 maneras
        //scoreText.text = score + ""; le sumo un string vac�o a ese int, luego ya ser� todo un string
        //scoreText.text = score.ToString(); transformo(cast) del int en un string
        //scoreText.text = "Score: " + score; a un string le ponemos despu�s un int, con lo cu�l ya todo es string
    }

    //M�todo para detectar cuando algo ha entrado en el trigger(zona de gol)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Solo aquellos GameObjects etiquetados como Disco, que hayan entrado en el trigger
        if (collision.CompareTag("Disco"))
        {
            //Si la puntuaci�n es mayor de 9
            if (gMRef.leftScore < 9 && gMRef.rightScore < 9 && gMRef.topScore < 9 && gMRef.bottomScore < 9)
            {
                if (diskRef.lastCollisionRacket == "PlayerLeft" && gameObject.name != "GoalLeft")
                {
                    gMRef.leftScore++;
                    //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
                    pointsLeft.text = gMRef.leftScore.ToString();
                    //Ejecuto el m�todo que hace que se pase a otra ronda
                    gMRef.GoalScored();
                }
                else if (diskRef.lastCollisionRacket == "PlayerRight" && gameObject.name != "GoalRight")
                {
                    gMRef.rightScore++;
                    //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
                    pointsRight.text = gMRef.rightScore.ToString();
                    //Ejecuto el m�todo que hace que se pase a otra ronda
                    gMRef.GoalScored();
                }
                else if (diskRef.lastCollisionRacket == "PlayerTop" && gameObject.name != "GoalTop")
                {
                    gMRef.topScore++;
                    //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
                    pointsTop.text = gMRef.topScore.ToString();
                    //Ejecuto el m�todo que hace que se pase a otra ronda
                    gMRef.GoalScored();
                }
                else if (diskRef.lastCollisionRacket == "PlayerBottom" && gameObject.name != "GoalBottom")
                {
                    gMRef.bottomScore++;
                    //Cambiamos el texto de la puntuaci�n al valor que tenga en ese momento el score
                    pointsBottom.text = gMRef.bottomScore.ToString();
                    //Ejecuto el m�todo que hace que se pase a otra ronda
                    gMRef.GoalScored();
                }
                else
                    gMRef.GoalScored();
            }
            else
            {
                //Ejecuto el m�todo que hace que se pase a otra ronda
                gMRef.GoalScored();
                //Ejecuto el m�todo que termina esta partida
                gMRef.WinGame();
            }
        }
    }
}
