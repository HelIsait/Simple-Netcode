using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Stanislav.Network.From.Nick
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null!;
        [SerializeField] private Button restart = null!;


        private void Awake()
        {
            text.EnsureNotNull();
            restart.EnsureNotNull()!.onClick!.AddListener(() => SceneManager.LoadScene(0));
            gameObject.SetActive(false);
        }


        public void Show(string winnerName)
        {
            gameObject.SetActive(true);
            text!.SetText($"Winner: {winnerName}");
        }
    }
}