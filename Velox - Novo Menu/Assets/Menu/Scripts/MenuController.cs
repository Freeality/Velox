using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Contém a lista de pistas e faz a exibição conforme a tecla pressionada
/// A tecla seta-direita exibe para a próxima imagem da lista.
/// A tecla seta-esquerda exibe a imagem anterior.
/// A tecla enter inicia a pista referente a imagem exibida.
/// </summary>
public class MenuController : MonoBehaviour {

    public GameObject startPanel;
    public GameObject mensagemNasPistasPanel;
    public GameObject fimPanel;
    public GameObject[] paineisDasPistas;
    public string[] nomesDasPistas;

    public RectTransform[] imagensNavegacao;
    public RectTransform painelNavegacao;
    public GameObject[] pistasAnim;

    public Text mensagemText;
    public Text paginaText;

    public AudioClip audioBotaoVerde; // return
    public AudioClip audioBotaoAzul; // right
    public AudioClip audioBotaoVermelho; // left

    public AudioClip audioSejaBemVindo;

    private int idPistaAtual;
    private int totalImagens;

    // Use this for initialization
    void Start () {
        gerenciaStart();
    }
	
	// Update is called once per frame
	void Update () {
        gerenciaInput();
	}

    /// <summary>
    /// Verifica qual tecla foi pressionada e executa o método adequado
    /// </summary>
    void gerenciaInput() {

        char command = SerialReader.GetComando();

        if (GameController.instance.mostraMenu) {
            if (Input.GetKeyDown("right") || command == 'b') {
                mostraProximaPista();
                return;
            }

            if (Input.GetKeyDown("left") || command == 'r') {
                mostraPistaAnterior();
                return;
            }
        }

        if (Input.GetKeyDown("return") || command == 'g') {
            gerenciaReturn();
        }
    }

    /// <summary>
    /// Se o StartPanel estiver ligado, inicia o menu principal
    /// caso contrario, o menu das pistas está ligado. Então, inicia
    /// a pista selecionada.
    /// </summary>
    void gerenciaReturn() {
        // o estado do startPanel
        bool startPanelIsOn = GameController.instance.mostraStart;
        bool fimPanelIsOn = GameController.instance.mostraFim;

        // faz o teste e inicia menu se ok
        if (startPanelIsOn || fimPanelIsOn) {
            iniciarMenu();
            return;
        }

        // se não, inicia pista atual
        iniciarPistaAtual();
    }

    /// <summary>
    /// Verifica qual a imagem atual e caso não seja a última
    /// desliga a atual, liga a próxima e atualiza o id atual.
    /// </summary>
    void mostraProximaPista() {
        SoundManager.instance.PlaySingle(audioBotaoAzul);
        desativaPista(idPistaAtual);
        ativaPista(proximaPista());
        atualizarPagina();
    }

    /// <summary>
    /// Desativa pista atual, ativa pista anterior e atualiza a página
    /// </summary>
    void mostraPistaAnterior() {
        SoundManager.instance.PlaySingle(audioBotaoVermelho);
        desativaPista(idPistaAtual);
        ativaPista(pistaAnterior());
        atualizarPagina();
    }

    /// <summary>
    /// Verifica qual próxima pista
    /// </summary>
    /// <returns>O id correto da próxima pista</returns>
    int proximaPista() {
        if(totalImagens > 0 && (idPistaAtual + 1) < totalImagens) {
            idPistaAtual++;
        }
        return idPistaAtual;
    }

    /// <summary>
    /// Verifica qual a pista anterior
    /// </summary>
    /// <returns>O id correto da pista anterior</returns>
    int pistaAnterior() {
        if(idPistaAtual > 0) {
            idPistaAtual--;
        }
        return idPistaAtual;
    }

    /// <summary>
    /// Liga a pista com o id fornecido.
    /// </summary>
    /// <param name="id">Deve estar no intervalo de paineisDasPistas</param>
    void ativaPista(int id) {
        if (idEstaEmPaineisDasPistas(id)) {
            GameObject pista = paineisDasPistas[id];
            pista.gameObject.SetActive(true);

            RectTransform imagemNavegacao = imagensNavegacao[id];
            EasyTween anim = pistasAnim[id].GetComponent<EasyTween> ();
            selecionaNavegacao(imagemNavegacao, anim);
        }
    }

    void deselecionaNavegacao(RectTransform rect) {
        Image imagem = rect.GetComponent<Image>();
        imagem.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    void selecionaNavegacao(RectTransform rect, EasyTween anim) {
        Image imagem = rect.GetComponent<Image>();
        imagem.color = Color.white;
        anim.OpenCloseObjectAnimation();
    }

    /// <summary>
    /// Testa se o id está em paineisDasPistas
    /// </summary>
    /// <param name="id">um inteiro</param>
    /// <returns>true se estiver, false se não estiver</returns>
    bool idEstaEmPaineisDasPistas(int id) {
        if (id >= 0 && id < paineisDasPistas.Length) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Desativa uma pista com um id válido
    /// </summary>
    /// <param name="id">um inteiro dentro de paineisDasPistas</param>
    void desativaPista(int id) {
        if (idEstaEmPaineisDasPistas(id)) {
            GameObject pista = paineisDasPistas[id];
            pista.gameObject.SetActive(false);

            RectTransform imagemNavegacao = imagensNavegacao[id];
            deselecionaNavegacao(imagemNavegacao);
        }
    }

    /// <summary>
    /// Testa se a pista atual está em nomesDasPistas.
    /// Se verdadeiro atualiza a pista atual em game controler;
    /// Obtém o path da pista atual;
    /// Desativa o painel da pista atual;
    /// Atualiza mensagem e carrega a pista.
    /// </summary>
    void iniciarPistaAtual() {
        if (idPistaAtual <= (nomesDasPistas.Length - 1)) {

            SoundManager.instance.musicSource.Stop();
            SoundManager.instance.PlaySingle(audioBotaoVerde);

            GameController.instance.pistaAtual = idPistaAtual;
            string cenaPath = "Scenes/" + nomesDasPistas[idPistaAtual];
            Debug.Log("iniciando scena...." + cenaPath);
            desativaPista(idPistaAtual);

            SoundManager.instance.PlaySingle(audioSejaBemVindo);
            mensagemText.text = "Vamos lá! Só um momento, por favor...";

            GameController.instance.MostraFim();
            SceneManager.LoadScene(cenaPath);
            return;
        }

        Debug.Log("A pista " + idPistaAtual.ToString() + " ainda está em desenvolvimento");
    }

    /// <summary>
    /// desliga startPanel;
    /// liga MensagemNasPistasPanel;
    /// liga PistaPanel_01;
    /// </summary>
    void iniciarMenu() {
        SoundManager.instance.PlaySingle(audioBotaoVerde);

        if (!SoundManager.instance.musicSource.isPlaying) {
            SoundManager.instance.musicSource.Play();
        }

        GameController.instance.MostraMenu();
        idPistaAtual = 0;
        totalImagens = paineisDasPistas.Length;
        mensagemText.text = "ESCOLHA SEU PASSEIO";
        atualizarPagina();

        startPanel.gameObject.SetActive(false);
        mensagemNasPistasPanel.gameObject.SetActive(true);
        fimPanel.gameObject.SetActive(false);
        ativaPista(0);

        painelNavegacao.gameObject.SetActive(true);
    }

    /// <summary>
    /// Desativar StartPanel
    /// Ativa Fim
    /// </summary>
    void iniciarFim() {
        startPanel.gameObject.SetActive(false);
        fimPanel.gameObject.SetActive(true);
        GameController.instance.MostraFim();
    }

    void iniciarStart() {
        if (!startPanel.gameObject.activeSelf) {
            startPanel.gameObject.SetActive(true);
            GameController.instance.MostraStart();
        }
    }

    /// <summary>
    /// Atualiza a mensagem paginaText com a pista atual.
    /// </summary>
    void atualizarPagina() {
        paginaText.text = "Passeio " + (idPistaAtual + 1).ToString() + "/" + totalImagens.ToString();
    }

    void gerenciaStart() {

        if(GameController.instance.mostraStart) {
            iniciarStart();
            return;
        }

        if (GameController.instance.mostraMenu) {
            iniciarMenu();
            return;
        }

        if (GameController.instance.mostraFim) {
            iniciarFim();
            return;
        }
    }
}
