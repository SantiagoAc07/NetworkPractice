using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;
using Systems.Player;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    public TMP_Text countdownText;
    public GameObject resultPanel;
    public TMP_Text resultText;
    public GameObject restartButton, menuButton;

    private bool _raceStarted = false;
    private int _playersFinished = 0;
    private string _winner = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        photonView.RPC("StartCountdown", RpcTarget.All);
    }

    [PunRPC]
    private void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        int countdown = 3;
        while (countdown >= 0)
        {
            countdownText.text = countdown > 0 ? countdown.ToString() : "GO!";
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        countdownText.gameObject.SetActive(false);
        _raceStarted = true;

        EnablePlayerMovement(); // 🚀 Aquí es donde finalmente se activa el movimiento
    }


    private void EnablePlayerMovement()
    {
        Movement[] players = FindObjectsOfType<Movement>();
        foreach (var player in players)
        {
            player.EnableMovement(); // 🔥 Activa el movimiento de todos los jugadores
        }
    }


    public void PlayerFinished(string playerName)
    {
        if (!_raceStarted) return;
        _playersFinished++;

        if (_playersFinished == 1)
        {
            _winner = playerName;
        }
        else if (_playersFinished == 2)
        {
            DetermineWinner();
        }
    }

    private void DetermineWinner()
    {
        string localPlayerName = PhotonNetwork.NickName;
        if (_winner == localPlayerName)
            ShowResult("¡Ganaste!");
        else
            ShowResult("Perdiste");
    }

    private void ShowResult(string message)
    {
        resultPanel.SetActive(true);
        resultText.text = message;
        restartButton.SetActive(true);
        menuButton.SetActive(true);
    }

    public void RestartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void GoToMenu()
    {
        PhotonNetwork.LoadLevel("Menu");
    }
}