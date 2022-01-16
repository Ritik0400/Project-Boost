using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crashNoise;
    [SerializeField] AudioClip landNoise;
    [SerializeField] ParticleSystem crasheffect;
    [SerializeField] ParticleSystem successeffect;

    AudioSource eventnoise;
    bool isTransitioning=false;
    bool collisionDisabled=false;


    void Start(){
        eventnoise=GetComponent<AudioSource>();
    }
    void Update()
    {
        CheatKeys();
    }

    void OnCollisionEnter(Collision other) {

        if(isTransitioning || collisionDisabled){
            return;
        }

        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("You have returned to the launch pad!");
                break;
            case "Finish":
                finishing();
                break;
            default:
                crashing();
                break;
        }
    }

    void crashing(){
        crasheffect.Play();
        isTransitioning=true;
        eventnoise.Stop();
        eventnoise.PlayOneShot(crashNoise);
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",delayTime);
    }

    void finishing(){
        successeffect.Play();
        isTransitioning=true;
        eventnoise.Stop();
        eventnoise.PlayOneShot(landNoise);
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel",delayTime);
    }

    void LoadNextLevel(){
        int currentSceneIndex =SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex== SceneManager.sceneCountInBuildSettings){
            nextSceneIndex=0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel() 
    {
        int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void CheatKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled=!collisionDisabled;   //toggle collision
        }
        
    }

}
