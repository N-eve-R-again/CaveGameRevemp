using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    public AudioSource[] audioSourcesOS;
    public AudioSource[] audioSourcesLP;
    public AudioSource displayAudio;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            if (FindObjectOfType<GameManager>())
            {
                FindObjectOfType<GameManager>().LateStart();
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    public void PlayOS(int index)
    {
        if(index != -1)
        {
            if (!audioSourcesOS[index].isPlaying)
            {
                audioSourcesOS[index].pitch = Random.Range(0.8f, 1.2f);
                audioSourcesOS[index].Play();
                //audioSourcesOS[index].volume = 1f;
            }
        }
        else
        {
            displayAudio.Play();
        }


    }
    public void PlayLP(int index)
    {

        if (!audioSourcesLP[index].isPlaying)
        {

            audioSourcesLP[index].Play();
            audioSourcesLP[index].volume = 0f;
            StartCoroutine(PlayCoroutine(index, 1f, audioSourcesLP[index]));
        }

    }
    public void FadeOS(int index)
    {

        audioSourcesOS[index].Stop();
        //StartCoroutine(FadeCoroutine(index, 5f, audioSourcesOS[index]));

    }
    public void FadeLP(int index)
    {


        StartCoroutine(FadeCoroutine(index, 1f, audioSourcesLP[index]));

    }

    IEnumerator FadeCoroutine(int index, float fadeSpeed, AudioSource audio)
    {
        while (audio.isPlaying)
        {

            audio.volume = Mathf.Lerp(audio.volume, 0, Time.deltaTime * fadeSpeed);

            if (audio.volume < 0.01f)
            {
                audio.Stop();
            }
            else
            {
                yield return null;
            }
        }

        //yield return null;
    }

    IEnumerator PlayCoroutine(int index, float fadeSpeed, AudioSource audio)
    {
        while (audio.volume < 0.98f)
        {

            audio.volume = Mathf.Lerp(audio.volume, 1f, Time.deltaTime * fadeSpeed);

            yield return null;


        }

        //yield return null;
    }
}
