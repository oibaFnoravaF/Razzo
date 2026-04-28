
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 5f;
    public bool death = true;
    public GameObject flame;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplier = 10f;
    private float hscore;
    private float timer = 0f;
    public float spawnRate = 1f;
    public UIDocument uiDocument;
    private Label scoreText;
    private Button restartButton;
    private Label highScoreText;
    public GameObject explosionEffect;
    public GameObject borderParent;
    private NewObstacle newObt;
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        newObt = GetComponent<NewObstacle>();
        

        //Gestore se high Score esiste Inutile per ora
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetFloat("score", 0f);
            PlayerPrefs.Save();
            Debug.Log("set 0 in start");
        }

        // gestore UI
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        highScoreText = uiDocument.rootVisualElement.Q<Label>("HighScoreLabel");
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        highScoreText.style.display = DisplayStyle.None;
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        

    }

    // Update is called once per frame
    void Update()
    {  
        UpdateScore();
        MovePlayer();
        
        timer += Time.deltaTime;
        if (timer>=spawnRate)
        {
            newObt.spawnObstacle();
            timer = 0f;
            
            
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (death)
        {
            Destroy(gameObject);

            hscore = PlayerPrefs.GetFloat("score");
            highScoreText.text = "High Score: " + hscore;
            if (score > hscore)
            {
                PlayerPrefs.SetFloat("score", score);
                PlayerPrefs.Save();
                highScoreText.text = "High Score: " + score;
            }
            
            borderParent.SetActive(false);

            restartButton.style.display = DisplayStyle.Flex;
            highScoreText.style.display = DisplayStyle.Flex;
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
    }

    void UpdateScore()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        scoreText.text = "Score: " + score;

    }

    void MovePlayer()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;

            rb.AddForce(direction * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }


        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            flame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            flame.SetActive(false);
            rb.angularVelocity = rb.angularVelocity/2;
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
