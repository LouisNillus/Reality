using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using DG.Tweening;
using System;

public class VideoManager : MonoBehaviour
{
    public static VideoManager instance;

    VideoPlayer player;
    [SerializeField] RenderTexture videoTexture;
    [SerializeField] CanvasGroup thumbnailGroup;
    [SerializeField] CanvasGroup videoGroup;

    public UnityEvent OnVideoEnded;

    private void Awake()
    {
        instance = this;
        player = GetComponent<VideoPlayer>();

        //player.time = player.clip.length - 6;
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.loopPointReached += VideoEnd;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.loopPointReached -= VideoEnd;
        }
    }

    public void VideoEnd(VideoPlayer player)
    {
        if (OnVideoEnded != null) FadeVideo(0f, null, () => OnVideoEnded.Invoke());
    }

    public void FadeVideo(float value, Action onStart = null, Action onComplete = null)
    {
        videoGroup.DOFade(value, 1f).OnStart(() => onStart?.Invoke()).OnComplete(() => onComplete?.Invoke());
    }

    public void ThumbnailToVideo()
    {
        thumbnailGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            thumbnailGroup.gameObject.SetActive(false);
            FadeVideo(1f, () => player.Play());
        });
    }
    public void VideoToThumbnail()
    {
        videoGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            player.Stop();
            player.time = 0f;
            Clear();
            thumbnailGroup.gameObject.SetActive(true);
            thumbnailGroup.DOFade(1f, 1f);
        });
    }

    public void Play()
    {
        player.Play();
    }

    public void Pause()
    {
        player.Pause();
    }

    public void Clear()
    {
        videoTexture.Release();
    }

    private void OnApplicationQuit()
    {
        Clear();
    }
}
