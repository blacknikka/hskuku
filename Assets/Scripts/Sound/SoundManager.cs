using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    // -----------------------
    //BGM用、SE用に分けてオーディオソースを持つ
    [SerializeField]
    private AudioSource AttachBGMSource;

    [SerializeField]
    private AudioSource AttachSESource;

    // -----------------------
    //全Audioを保持
    private Dictionary<string, AudioClip> _bgmDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> _seDic = new Dictionary<string, AudioClip>();

    // -----------------------
    //ボリューム保存用のkeyとデフォルト値
    private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
    private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
    private const float BGM_VOLUME_DEFULT = 0.3f;
    private const float SE_VOLUME_DEFULT = 0.3f;

	private void Awake()
	{
        if (this != Inst)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        Resources.LoadAll("Audio/BGM").ToList().ForEach((x) =>
        {
            _bgmDic[x.name] = x as AudioClip;
        });

        Resources.LoadAll("Audio/SE").ToList().ForEach((x) =>
        {
            _seDic[x.name] = x as AudioClip;
        });
    }

	// Use this for initialization
	void Start () {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
        AttachSESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //=================================================================================
    //SE
    //=================================================================================
    public void PlaySE(string seName)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "という名前のSEがありません");
            return;
        }

        AttachSESource.PlayOneShot(_seDic[seName]);
    }

    //=================================================================================
    // BGM
    //=================================================================================
    public void PlayBGM(string bgmName)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "という名前のBGMがありません");
            return;
        }

        AttachBGMSource.clip = _bgmDic[bgmName];
        AttachBGMSource.Play();
    }

    //=================================================================================
    //音量変更
    //=================================================================================
    /// <summary>
    /// BGMとSEのボリュームを別々に変更&保存
    /// </summary>
    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
    }
}
