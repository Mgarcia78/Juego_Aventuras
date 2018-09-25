using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartida : MonoBehaviour {
    public void CargarPartidaNueva()
    {
         SceneManager.LoadScene("PartidaNueva");
    }
}
