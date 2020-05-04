using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Button _playButton;
    public Button _optionsButton;
    public Button _quitButton;
    public Animation _menuAnimation;

    bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        Button _playBtn = _playButton.GetComponent<Button>();
        _playBtn.onClick.AddListener(PlayGame);

        Button _optionsBtn = _optionsButton.GetComponent<Button>();
        _optionsBtn.onClick.AddListener(Options);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayGame()
    {
        SceneManager.LoadScene("FirstScene");
    }

    void Options()
    {
        print(isOpen.ToString());
        if (isOpen == false)
        {
            
            Animation _optionsAnimOpen = _menuAnimation.GetComponent<Animation>();
            _optionsAnimOpen.Play("ANM_MainMenu_Options_Open");
            isOpen = true;
         
        }

       else 
       {
            Animation _optionsAnimClose = _menuAnimation.GetComponent<Animation>();
            _optionsAnimClose.Play("ANM_MainMenu_Options_Close");
            isOpen = false;
       }
        
         



    }
  
    
}
