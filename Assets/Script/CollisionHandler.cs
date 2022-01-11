using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip crashNoise;
    [SerializeField] AudioClip landNoise;

    AudioSource eventnoise;
    bool isTransitioning=false;

    void Start(){
        eventnoise=GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) {

        if(isTransitioning){
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

        isTransitioning=true;
        eventnoise.Stop();
        eventnoise.PlayOneShot(crashNoise);
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel",delayTime);
    }

    void finishing(){

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
}
