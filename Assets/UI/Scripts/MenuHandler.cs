using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _panel;

    public void Awake()
    {
        GameMaster.OnPause += OnPause;
        GameMaster.OnUnPause += OnUnPause;
    }

    private void OnDisable()
    {
        GameMaster.OnPause -= OnPause;
        GameMaster.OnUnPause -= OnUnPause;
    }
    public void StartGameButton()
    {
        GameMaster.Instance.StartGame();
        gameObject.SetActive(false);
    }

    public void ContinueGameButton()
    {
        GameMaster.Instance.UnPauseGame();
    }

    public void QuitGameButton()
    {
        GameMaster.Instance.QuitGame();
    }

    public void OnPause()
    {
        _panel.SetActive(true);
    }
    public void OnUnPause()
    {
        _panel.SetActive(false);
    }

}
