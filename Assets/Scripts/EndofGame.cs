using UnityEngine;
using UnityEngine.SceneManagement;

public class EndofGame : MonoBehaviour
{
    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
            SceneManager.LoadScene(3);
    }
}
