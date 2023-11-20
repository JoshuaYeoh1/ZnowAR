using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Invoke("unlockFPS",.1f);
        LoadVolume();
        //awakeAudio();
        awakeTransition();

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartCoroutine(spawningSnowballs());
        //StartCoroutine(spawningEnemies());
    }

    void Update()
    {
        updateReloadButton();
        // updateShuffleMusic();
    }

    void unlockFPS()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    [Header("Audio Manager")]
    public AudioSource SFXObject;
    public AudioSource musicSource, ambSource;
    //public AudioClip[] musMainMenu, musLevel1, musLevel2, musWin, amb1, amb2;
    //Coroutine randAmbRt;

    // void awakeAudio()
    // {
    //     changeMusic(.1f);
    // }    

    // public void changeMusic(float changeFadeTime=2)
    // {
    //     StartCoroutine(changingMusic(changeFadeTime));
    // }
    
    // IEnumerator changingMusic(float changeFadeTime)
    // {
    //     fadeAudio(musicSource, true, changeFadeTime, 0);

    //     yield return new WaitForSecondsRealtime(changeFadeTime);

    //     playMusic();
    // }

    // void playMusic()
    // {
    //     musicSource.Stop();

    //     fadeAudio(musicSource, true, .1f, 1);

    //     if(SceneManager.GetActiveScene().buildIndex==0)
    //     musicSource.clip = musMainMenu[Random.Range(0, musMainMenu.Length)];
    //     else if(LevelCompleted)
    //     musicSource.clip = musWin[Random.Range(0, musWin.Length)];
    //     else if(SceneManager.GetActiveScene().buildIndex==1)
    //     musicSource.clip = musLevel1[Random.Range(0, musLevel1.Length)];
    //     else
    //     musicSource.clip = musLevel2[Random.Range(0, musLevel2.Length)];

    //     musicSource.Play();
    // }

    // void updateShuffleMusic()
    // {
    //     if(!musicSource.isPlaying)
    //     {
    //         playMusic();
    //     }
    // }

    // public void fadeAudio(AudioSource source, bool fadeIn, float fadeTime, float toVolume)
    // {
    //     StartCoroutine(fadeAudioEnum(source, fadeIn, fadeTime, toVolume));
    // }

    // IEnumerator fadeAudioEnum(AudioSource source, bool fadeIn, float fadeTime, float toVolume)
    // {
    //     if(!fadeIn)
    //     {
    //         float lengthOfSource = source.clip.samples/source.clip.frequency;
    //         yield return new WaitForSecondsRealtime(lengthOfSource-fadeTime);
    //     }

    //     float time=0, startVolume=source.volume;
    //     while(time<fadeTime)
    //     {
    //         time+=Time.deltaTime;
    //         source.volume = Mathf.Lerp(startVolume, toVolume, time/fadeTime);
    //         yield return null;
    //     }
    //     yield break;
    // }

    // void toggleAmb(bool toggle=true)
    // {
    //     if(toggle)
    //     {
    //         ambSource.clip = amb1[Random.Range(0, amb1.Length)];
    //         ambSource.Play();
    //         randAmbRt=StartCoroutine(randAmb());
    //     }
    //     else
    //     {
    //         ambSource.Stop();
    //         if(randAmbRt!=null) StopCoroutine(randAmbRt);
    //     }
    // }

    // IEnumerator randAmb()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(Random.Range(2f,10f));

    //         for(int i=0;i<Random.Range(1,4);i++)
    //         {   
    //             playSFX(amb2, transform, false, true, Random.Range(.1f,1f), true);
    //         }
    //     }
    // }

    public void playSFX(AudioClip[] clip, Transform spawnTransform, bool dynamics=true, bool randPitch=true, float volume=1, bool randPan=false)
    {   
        AudioSource source = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);
        //source.transform.parent = transform;
        
        source.clip = clip[Random.Range(0,clip.Length)];
        source.volume = volume;
        SFXObject sfxobj = source.GetComponent<SFXObject>();
        sfxobj.randPitch = randPitch;
        sfxobj.dynamics = dynamics;
        if(randPan) source.panStereo = Random.Range(-1f,1f);

        source.Play();

        Destroy(source.gameObject, source.clip.length);
    }

    public AudioMixer mixer;
    public const string MASTER_KEY = "masterVolume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    void LoadVolume()
    {   
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume)*20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume)*20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume)*20);
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    [Header("Game")]
    public Camera cam;
    public bool controlsEnabled=true;
    public int snowballAmmo, maxSnowballs=20, snowballCount;
    public List<GameObject> snowballSpawnerList = new List<GameObject>();
    public GameObject enemyPrefab;
    public List<GameObject> enemyList = new List<GameObject>();
    public float enemySpawnTimeMin=5, enemySpawnTimeMax=10;
    public float enemySpawnRangeMin=5, enemySpawnRangeMax=10;
    public int maxEnemies=3;

    IEnumerator spawningSnowballs()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(.1f,3f));

            if(snowballCount<maxSnowballs && snowballSpawnerList.Count>0)
            snowballSpawnerList[Random.Range(0,snowballSpawnerList.Count)].GetComponent<SnowballSpawner>().spawnSnowball();
        }
    }

    IEnumerator spawningEnemies()
    {
        float offsetX, offsetY, offsetZ;

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(enemySpawnTimeMin,enemySpawnTimeMax));

            if(enemyList.Count<maxEnemies)
            {                
                if(Random.Range(1,3)==1) offsetX=Random.Range(enemySpawnRangeMin,enemySpawnRangeMax);
                else offsetX=Random.Range(-enemySpawnRangeMin,-enemySpawnRangeMax);

                offsetY=Random.Range(enemySpawnRangeMin,enemySpawnRangeMax);

                if(Random.Range(1,3)==1) offsetZ=Random.Range(enemySpawnRangeMin,enemySpawnRangeMax);
                else offsetZ=Random.Range(-enemySpawnRangeMin,-enemySpawnRangeMax);

                Vector3 spawnPos = new Vector3(cam.transform.position.x+offsetX, cam.transform.position.y+offsetY, cam.transform.position.z+offsetZ);
                
                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                enemyList.Add(enemy);
            }
        }
    }

    // public void camShake()
    // {
    //     GameObject.FindGameObjectWithTag("camshaker").GetComponent<WiggleRotate>().shake(.2f);
    // }

    [Header("Scene Manager")]
    public Animator transitionAnimator;
    GameObject transitionCanvas;
    CanvasGroup transitionCanvasGroup;
    public bool transitioning;
    int transitionTypes=1;
    Coroutine transitionRt;

    public void awakeTransition()
    {
        transitionCanvas=transitionAnimator.transform.parent.gameObject;
        transitionCanvasGroup=transitionCanvas.GetComponent<CanvasGroup>();
        transitionCanvas.SetActive(true);
        transitionIn(Random.Range(0,transitionTypes));
    }

    public void transitionIn(int type)
    {
        cancelTransition();
        transitionRt = StartCoroutine(transitionInEnum(type));
    }
    IEnumerator transitionInEnum(int type)
    {
        enableTransition();
        transitionAnimator.SetInteger("randin", type);

        yield return new WaitForSecondsRealtime(.1f);
        yield return new WaitForSecondsRealtime(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

        disableTransition();
    }

    public void transitionOut(int type, bool quit=false)
    {
        cancelTransition();
        transitionRt = StartCoroutine(transitionOutEnum(type, quit));
    }
    IEnumerator transitionOutEnum(int type, bool quit=false)
    {
        enableTransition();
        transitionAnimator.SetInteger("randout", type);

        if(quit)
        {
            yield return new WaitForSecondsRealtime(.1f);

            // fadeAudio(musicSource, true, transitionAnimator.GetCurrentAnimatorStateInfo(0).length, 0);
            // fadeAudio(ambSource, true, transitionAnimator.GetCurrentAnimatorStateInfo(0).length, 0);

            yield return new WaitForSecondsRealtime(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

            Debug.Log("Quit");
            Application.Quit();
        }
    }

    public void transitionTo(int sceneNumber)
    {
        cancelTransition();
        StartCoroutine(transitionToEnum(sceneNumber));
    }
    IEnumerator transitionToEnum(int sceneNumber, bool anim=true)
    {
        if(anim)
        {
            transitionOut(Random.Range(0,transitionTypes));
            yield return new WaitForSecondsRealtime(.1f);
            yield return new WaitForSecondsRealtime(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        SceneManager.LoadScene(sceneNumber);

        if(anim) transitionIn(Random.Range(0,transitionTypes));

        yield return new WaitForSecondsRealtime(.1f);

        // changeMusic();

        // toggleAmb(false);
        // if(SceneManager.GetActiveScene().buildIndex!=0) toggleAmb(true);
    }

    void enableTransition()
    {
        transitioning=true;
        controlsEnabled=false;
        transitionCanvas.SetActive(true);
        transitionCanvasGroup.interactable=true;
        transitionCanvasGroup.blocksRaycasts=true;
    }

    void disableTransition()
    {
        transitionCanvasGroup.interactable=false;
        transitionCanvasGroup.blocksRaycasts=false;
        controlsEnabled=true;
        transitioning=false;
    }

    void cancelTransition()
    {
        transitionCanvasGroup.interactable=false;
        transitionCanvasGroup.blocksRaycasts=false;
        if(transitionRt!=null) StopCoroutine(transitionRt);
        transitionAnimator.SetInteger("randin", -1);
        transitionAnimator.SetInteger("randout", -1);
        transitionCanvas.SetActive(false);
        transitioning=false;
    }

    void updateReloadButton()
    {
        if(Input.GetKeyDown(KeyCode.R)) ReloadScene();
    }

    public void ReloadScene()
    {
        //if(!transitioning && SceneManager.GetActiveScene().buildIndex!=0)
        transitionTo(SceneManager.GetActiveScene().buildIndex);
    }

    public void RandomScene()
    {
        if(!transitioning)
        transitionTo(Random.Range(0,6));
    }

    // [Header("SFX")]
    // public AudioClip[] sfxBtnClick;
    // public AudioClip[] sfxBtnHover, sfxTween;
}