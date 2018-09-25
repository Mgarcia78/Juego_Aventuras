using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {
    public class StoryNode
    {
        public string history;
        public string[] answers;
        public StoryNode[] nextNode;
        public bool isFinal = false;
        public delegate void NodeVisited();
        public NodeVisited nodeVisited;



        public bool Item1Activo = false;
        public bool Item2Activo = false;
        public bool Item3Activo = false;
    }

    public Text historyText;
    public Transform answerParent;
    public GameObject buttonAnswer;
    private StoryNode current;
    public GameObject pauseMenu;
    public GameObject tituloPausa;
    public GameObject tituloGameOver;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    void Start()
    {
        current = StoryFiller.FillStory();

        //Aparece un error si se vacia el texto por eso se ha vaciado el componente de texto por defecto de la scene
        //historyText.text = "";
        FillUI();
    }

    void AnswerSelected(int index)
    {
        print(index);



        //Borra el contenido de texto Historia
        historyText.text = "";

        //Imprime el nuevo texto de la historia, pone el texto del botón como cabecera y la letra en negrita y cursiva
        historyText.text += "\n" + "<b>" + "<i>" + current.answers[index] + "</i>" + "</b>";

      

        if (!current.isFinal)
        {
            current = current.nextNode[index];
            FillUI();
        }
        else
        {
            //Final de partida
            FinishGameMenu();
        }



        //Activa los iconos de los diferentes items
        if (current.Item1Activo)
        {
            ActivaItem1();
        }

        if (current.Item2Activo)
        {
            ActivaItem2();
        }

        if (current.Item3Activo)
        {
            ActivaItem3();
        }
    }

    void FillUI()
    {
        historyText.text += "\n" + "\n" + current.history;



        //Comprueba los nodos por los que se ha pasado para completar la historia

        if (current.nodeVisited != null)
        {
            current.nodeVisited();
        }



        foreach (Transform child in answerParent.transform)
        {
            Destroy(child.gameObject);
        }

        bool isLeft = true;
        float height = 50;
        int index = 0;

        foreach(string answer in current.answers)
        {
            GameObject buttonAnswerCopy = Instantiate(buttonAnswer);
            buttonAnswerCopy.transform.parent = answerParent;
            float x = buttonAnswerCopy.GetComponent <RectTransform>().rect.x * 1.1f;

            buttonAnswerCopy.GetComponent <RectTransform>().localPosition = new Vector3(isLeft ? x : -x, height, 0);

            if (!isLeft) height += buttonAnswerCopy.GetComponent <RectTransform>().rect.y * 2.5f;

            isLeft = !isLeft;
            FillListener(buttonAnswerCopy.GetComponent<Button>(), index);
            buttonAnswerCopy.GetComponentInChildren<Text>().text = answer;

            index++;
        }
    }

    void FillListener(Button button, int index)
    {
        button.onClick.AddListener(() =>
        {
            AnswerSelected(index);
        });
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            PauseGameMenu();
    }

    void PauseGameMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        tituloPausa.SetActive(!tituloPausa.activeSelf);
    }

    void FinishGameMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        tituloGameOver.SetActive(!tituloGameOver.activeSelf);
    }

    public void RestartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PantallaInicial");
    }



    //Activa los iconos de los diferentes items
    public void ActivaItem1()
    {
        item1.SetActive(!item1.activeSelf);
    }

    void ActivaItem2()
    {
        item2.SetActive(!item2.activeSelf);
    }

    void ActivaItem3()
    {
        item3.SetActive(!item3.activeSelf);
    }
}
