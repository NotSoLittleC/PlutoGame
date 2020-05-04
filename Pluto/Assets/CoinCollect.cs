using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    [SerializeField]
    private GameObject _pfxCoinCollect;
    [SerializeField]
    private Camera mainCamera;

    private float _coinPosY;
    private float _coinPosX;
    private float _coinPosZ;
    private Vector3 _coinOrigin;
    private Vector3 _coinDestination;
    public float speedMin = 1.0f;
    public float speedMax = 2.0f;
    private float randomSpeed;

    private float _coinDistanceY;
    private float _coinDistanceX;

    [SerializeField]
    private bool _movingCoin = false;

    void Start()
    {
        _coinPosX = transform.position.x;
        _coinPosY = transform.position.y;
        _coinPosZ = transform.position.z;
        _coinOrigin = transform.position;


        _coinDistanceX = 9.0f - transform.position.x;
        _coinDistanceY = 9.0f - transform.position.y;
  
        _coinDestination = new Vector3 (transform.position.x, transform.position.y + _coinDistanceY, transform.position.z);

        randomSpeed = Random.Range(speedMin, speedMax);

        
      
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingCoin == true)
        {
            transform.position = Vector3.Lerp(_coinOrigin, _coinDestination, (Mathf.Sin(randomSpeed * Time.time) + 1.0f) / 2.0f);
        }
        
    }

    private void OnDisable()
    {
       GameObject _pfxCoin = Instantiate(_pfxCoinCollect, transform.position, Quaternion.identity) as GameObject;

        ParticleSystem _pfxCoinParticle = _pfxCoin.GetComponent<ParticleSystem>();
        if (_pfxCoinParticle.particleCount <= 0)
        { Destroy(_pfxCoin, 1f); }
      
    }
}
