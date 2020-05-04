using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _pfxCoinCollect;

    private GameObject[] numCoins;
    private int i;
    private bool isCreated;

    void Start()

    {
        numCoins = GameObject.FindGameObjectsWithTag("PickUp");
        
    }


    // Update is called once per frame
    void Update()
    {
        
        for (i = 0; i < numCoins.Length; i++)
        {
            
            if (numCoins[i].activeSelf == false && !isCreated)
            {
                CoinCollectVFX();
             

            }
        }

        //print("" + (numCoins.Length).ToString());

    }

    public void CoinCollectVFX()
    {

        /*_pfxCoinCollect.SetActive(true);
        _pfxCoinCollect.transform.position = _coin.transform.position;
        _CoinEffect = _pfxCoinCollect.GetComponent<ParticleSystem>();*/

        Instantiate(_pfxCoinCollect, numCoins[i].transform.position, Quaternion.identity);
        
        isCreated = true;
        print( "Coin Has Been Spawned");


    }
}