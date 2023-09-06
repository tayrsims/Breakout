using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;

    private bool isPaddleMoving;
    [SerializeField]private GameObject paddle;
    [SerializeField]private float paddleSpeed;
    private float moveDirection;

    [SerializeField] private GameObject brick;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text EndGameText;
    [SerializeField] private int score;

    [SerializeField] private PlayerInput playerInputInstance;
    [SerializeField] private BallController ballControllerInstance;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 brickPos = new Vector2(-9, 5);
        CreateAllBricks(brickPos);

        DefinePlayerInput();
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();
        if (score >= 4000) 
        {
            EndGameText.gameObject.SetActive(true);
            ballControllerInstance.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        restart.started -= Restart_started;
        quit.started -= Quit_started;

    }

    private void DefinePlayerInput()
    {
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("MovePaddle");
        restart = playerInput.currentActionMap.FindAction("RestartGame");
        quit = playerInput.currentActionMap.FindAction("QuitGame");

        move.started += Move_started;
        move.canceled += Move_canceled;
        restart.started += Restart_started;
        quit.started += Quit_started;

        isPaddleMoving = false;
    }

    private void CreateAllBricks(Vector2 brickPos)
    {
        for (int j = 0; j < 4; j++)
        {
            brickPos.y -= 1;
            brickPos.x = -9;

            for (int i = 0; i < 10; i++)
            {
                brickPos.x += 1.6f;

                Instantiate(brick, brickPos, Quaternion.identity);
            }
        }
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }


    private void Move_started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;
    }

    private void Quit_started(InputAction.CallbackContext obj)
    {

        
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (isPaddleMoving) 
        {
            //move the paddle
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(paddleSpeed * moveDirection,0); 
        }
        else 
        {
            //stop the paddle 
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (isPaddleMoving) 
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
