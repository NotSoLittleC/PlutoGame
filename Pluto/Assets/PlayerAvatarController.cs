using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatarController : MonoBehaviour
{

    [SerializeField]
    private GameObject _pfxPlayerDeathVFX;
    [SerializeField]
    private GameObject _pfxProjectileEquipped;
    [SerializeField]
    public bool isMovingRight = false;
    public bool isMovingLeft = false;

    private Vector3 _playerOrigin;

    public float speed;
    public float scoreGoal;
    public Text countText;
    public Text winText;
    public GameObject scoreTextObject;
    public GameObject winTextObject;

    private Rigidbody2D rigidBody;
    private int scoreCount;
    public float moveSpeed;
    public float jumpHeight;

    private bool isAlive = true;

    void Start()
    {
        _playerOrigin = gameObject.transform.position;
    

        rigidBody = GetComponent<Rigidbody2D>();

        scoreCount = 0;
        winText.text = "";
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {

        Jump();
        PlayerMovement();
        ProjectileFire();

    }

    void PlayerMovement()
    {
        Vector3 moveHorizontal = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            isMovingRight = true;
            transform.position += moveHorizontal * Time.deltaTime * moveSpeed;
            gameObject.GetComponent<Animator>().SetBool("IsMovingRight", true);

        }
        if (Input.GetAxis("Horizontal") < 0.0f)
        {
            isMovingLeft = true;
            transform.position += moveHorizontal * Time.deltaTime * moveSpeed;
            gameObject.GetComponent<Animator>().SetBool("IsMovingLeft", true);
        }
        if (Input.GetAxisRaw("Horizontal") == 0.0f)
        {
            isMovingLeft = false;
            isMovingRight = false;
            gameObject.GetComponent<Animator>().SetBool("IsMovingLeft", false);
            gameObject.GetComponent<Animator>().SetBool("IsMovingRight", false);

        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

    }

    void ProjectileFire()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isMovingRight == true)
            {
                GameObject _pfxProjectile = Instantiate(_pfxProjectileEquipped, gameObject.transform.position, Quaternion.identity);
                _pfxProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f, 3f), ForceMode2D.Impulse);
            }
            if (isMovingLeft == true)
            {
                GameObject _pfxProjectile = Instantiate(_pfxProjectileEquipped, gameObject.transform.position, Quaternion.identity);
                _pfxProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(-4f, 3f), ForceMode2D.Impulse);
            }
            else
            {
                GameObject _pfxProjectile = Instantiate(_pfxProjectileEquipped, gameObject.transform.position, Quaternion.identity);
                _pfxProjectile.GetComponent<Rigidbody2D>().AddForce(new Vector2(4f, 3f), ForceMode2D.Impulse);
            }
           
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            scoreCount = scoreCount + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine( CanReset());
            //PlayerDeathVFX();
        }
    }

    void SetCountText()
    {
        scoreTextObject.GetComponent<Text>().text = "Count: " + scoreCount.ToString();


        if (scoreCount >= scoreGoal)
        {

            winText.text = "YOU WIN!";
            winTextObject.SetActive(true);
        }
    }

    public void PlayerDeathVFX()
    {
        GameObject _DeathVFX = Instantiate(_pfxPlayerDeathVFX, gameObject.transform.position, Quaternion.identity);
        ParticleSystem _pfxPlayerDeathParticle = _DeathVFX.GetComponent<ParticleSystem>();

        isAlive = false;

        if (_pfxPlayerDeathParticle.particleCount <= 0)
        {      
                Destroy(_DeathVFX, 1.0f);
               
        }

    }

 
    void ResetPlayer()
    {
        if (isAlive == false)
        {
            
            gameObject.transform.position = _playerOrigin;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    IEnumerator CanReset()
    {
        PlayerDeathVFX();
        yield return new WaitForSeconds(1f);
        ResetPlayer();
    }

}
