using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MusicController : MonoBehaviour
{
    [SerializeField] public static string DefaultKey = "TwinTown";
    [SerializeField] float crossFade = 3f;

    readonly AudioSource[] sources = new AudioSource[2];
    int active;             // index of the currently audible source
    AsyncOperationHandle<AudioClip> lastHandle;

    private string lastUsedKey = "TwinTown";

    async void Start()
    {
        // create pooled sources
        sources[0] = gameObject.AddComponent<AudioSource>();
        sources[1] = gameObject.AddComponent<AudioSource>();
        sources[0].loop = sources[1].loop = true;

        await PlayAsync(DefaultKey, immediate: true);
    }

    public async void CrossFadeTo(string originalKey)
    {
        string key = originalKey;

        if (lastUsedKey == originalKey)
        {
            return;
        }

        if (originalKey == "MichaelJarman")
        {
            switch (Random.Range(0, 3))
            {
                case 1:
                    key = "MichaelJarman1";
                    break;
                case 2:
                    key = "MichaelJarman2";
                    break;
                default:
                    break;
            }
        }

        await PlayAsync(key, immediate: false);
        lastUsedKey = originalKey;
    }

    public void CrossFadeIntoBattle(string addressableLabel)       // called when combat ends
    {
        CrossFadeTo(addressableLabel);             // fade back to the main/ambient theme
        lastUsedKey = addressableLabel;
    }

    public void CrossFadeOutOfBattle()       // called when combat ends
    {
        CrossFadeTo(DefaultKey);             // fade back to the main/ambient theme
        lastUsedKey = DefaultKey;
    }

    async Task PlayAsync(string key, bool immediate)
    {
        // Asynchronously fetch clip
        var handle = Addressables.LoadAssetAsync<AudioClip>(key);
        await handle.Task;                       // await without freezing the main thread

        var next = 1 - active;                   // pick the silent source
        sources[next].clip = handle.Result;
        sources[next].Play();

        if (immediate)           // startup: no fade, just swap
        {
            sources[next].volume = 0.2f;
            sources[active].Stop();
            active = next;
        }
        else                     // runtime: smooth cross-fade
        {
            StartCoroutine(Fade(sources[active], sources[next], crossFade));
            active = next;
        }

        // release previously loaded clip once the fade is done
        if (lastHandle.IsValid())
            Addressables.Release(lastHandle);
        lastHandle = handle;
    }

    IEnumerator Fade(AudioSource from, AudioSource to, float time)
    {
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            float k = t / time;
            from.volume = Mathf.Lerp(0.2f, 0f, k);
            to.volume = Mathf.Lerp(0f, 0.2f, k);
            yield return null;
        }
        from.Stop();
    }
}
