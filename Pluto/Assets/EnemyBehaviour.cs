using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private GameObject _pfxEnemyDestroy;


    private float _enemyPosY;
    private float _enemyPosX;
    private float _enemyPosZ;
    private Vector3 _enemyOrigin;
    private Vector3 _enemyDestination;
    public float speedMin = 1.0f;
    public float speedMax = 2.0f;
    private float randomSpeed;

    private enum _movementEnum
    {
        Horizontal,Vertical,Rotate
    }

    [SerializeField]
    private _movementEnum _movementMode;

    [SerializeField]
    private float _enemyDistanceY = 9.0f;

    [SerializeField]
    private float _enemyDistanceX = 3.0f;

    [SerializeField]
    private float _rotateSpeed = 15.0f;
    [SerializeField]
    private float _rotateRadius = 2.0f;

    void Start()
    {
        _enemyPosX = transform.position.x;
        _enemyPosY = transform.position.y;
        _enemyPosZ = transform.position.z;
        _enemyOrigin = transform.position;

        randomSpeed = Random.Range(speedMin, speedMax); 
      
    }

    void EnemyMovement()
    {
        if (_movementMode == _movementEnum.Horizontal)
        {
            _enemyDestination = new Vector3(_enemyOrigin.x + _enemyDistanceX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(_enemyOrigin, _enemyDestination, (Mathf.Sin(randomSpeed * Time.time) + 1.0f) / 2.0f);
        }

        if (_movementMode == _movementEnum.Vertical)
        {
            _enemyDestination = new Vector3(transform.position.x,_enemyOrigin.y + _enemyDistanceY, transform.position.z);
            transform.position = Vector3.Lerp(_enemyOrigin, _enemyDestination, (Mathf.Sin(randomSpeed * Time.time) + 1.0f) / 2.0f);
        }
        if (_movementMode == _movementEnum.Rotate)
        {

            transform.RotateAround(_enemyOrigin + new Vector3 (0,_rotateRadius,0), new Vector3 (0, 0, 1), _rotateSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
       GameObject _pfxEnemyDeath = Instantiate(_pfxEnemyDestroy, transform.position + new Vector3 (0,0.42f,0), Quaternion.identity) as GameObject;

        ParticleSystem _pfxEnemyDestroyParticle = _pfxEnemyDeath.GetComponent<ParticleSystem>();
        if (_pfxEnemyDestroyParticle.particleCount <= 0)
        { Destroy(_pfxEnemyDestroy, 1f); }
      
    }
}
