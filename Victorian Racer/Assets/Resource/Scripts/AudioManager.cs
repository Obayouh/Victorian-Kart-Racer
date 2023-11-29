using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject _uncheckedAudioBox;
    [SerializeField] private GameObject _checkedAudioBox;
    [SerializeField] private AudioSource _mainMenuChant;

    private CarControls _carControls;
    private RespawnCheckpoints _respawnCheckpoints;
    private RaceCountdown _raceCountdown;
    private PitchforkTrap _pitchforkTrap;
    private FireBallAbility _fireBallAbility;

    private float _chantVolume;
    private float _driveVolume;
    private float _driftVolume;
    private float _BGMVolume;
    private float _bellVolume;
    private float _graveyardLeftVolume;
    private float _graveyardRightVolume;
    private float _startVolume;
    private float _clangVolume;
    private float _wooshVolume;

    [SerializeField] private bool _mute = false;

    private void Awake()
    {
        _carControls = FindObjectOfType<CarControls>();
        _respawnCheckpoints = FindObjectOfType<RespawnCheckpoints>();
        _raceCountdown = FindObjectOfType<RaceCountdown>();
        _pitchforkTrap = FindObjectOfType<PitchforkTrap>();
        _fireBallAbility = FindObjectOfType<FireBallAbility>();


    }

    private void Start()
    {
        //MuteSound();

    }

    private void Update()
    {
        //if (_checkedAudioBox.activeInHierarchy == true)
        //{
        //    _mute = true;
        //}

        //if (_uncheckedAudioBox.activeInHierarchy == true)
        //{
        //    _mute = false;
        //}
    }

    public void MuteSound()
    {
        _mute = true;

        if (_mute == true)
        {
            if (SceneManager.sceneCount == 0)
            {
                //_mainMenuChant.volume = 0.0f;
                _mainMenuChant.mute = true;
            }
            if (SceneManager.sceneCount == 1)
            {
                //_carControls._carDriveSFX.volume = 0.0f;
                //_carControls._driftSFX.volume = 0.0f;
                //_carControls._BGM.volume = 0.0f;
                //_respawnCheckpoints._bellSFX.volume = 0.0f;
                //_respawnCheckpoints._graveyardBGMLeft.volume = 0.0f;
                //_respawnCheckpoints._graveyardBGMRight.volume = 0.0f;
                //_raceCountdown._carStartSFX.volume = 0.0f;
                //_pitchforkTrap._clangSFX.volume = 0.0f;
                //_fireBallAbility._fireWooshSFX.volume = 0.0f;
                _carControls._carDriveSFX.mute = true;
                _carControls._driftSFX.mute = true;
                _carControls._BGM.mute = true;
                _respawnCheckpoints._bellSFX.mute = true;
                _respawnCheckpoints._graveyardBGMLeft.mute = true;
                _respawnCheckpoints._graveyardBGMRight.mute = true;
                _raceCountdown._carStartSFX.mute = true;
                _pitchforkTrap._clangSFX.mute = true;
                _fireBallAbility._fireWooshSFX.mute = true;
            }
            else
            {
                ;
            }
        }
    }

    public void UnMuteSound()
    {
        _mute = false;

        if (_mute == false)
        {
            if (SceneManager.sceneCount == 0)
            {
                _mainMenuChant.mute = false;
            }

            if (SceneManager.sceneCount == 1)
            {
                //_carControls._carDriveSFX.volume = _driveVolume;
                //_carControls._driftSFX.volume = _driftVolume;
                //_carControls._BGM.volume = _BGMVolume;
                //_respawnCheckpoints._bellSFX.volume = _bellVolume;
                //_respawnCheckpoints._graveyardBGMLeft.volume = _graveyardLeftVolume;
                //_respawnCheckpoints._graveyardBGMRight.volume = _graveyardRightVolume;
                //_raceCountdown._carStartSFX.volume = _startVolume;
                //_pitchforkTrap._clangSFX.volume = _clangVolume;
                //_fireBallAbility._fireWooshSFX.volume = _wooshVolume;
                _carControls._carDriveSFX.mute = false;
                _carControls._driftSFX.mute = false;
                _carControls._BGM.mute = false;
                _respawnCheckpoints._bellSFX.mute = false;
                _respawnCheckpoints._graveyardBGMLeft.mute = false;
                _respawnCheckpoints._graveyardBGMRight.mute = false;
                _raceCountdown._carStartSFX.mute = false;
                _pitchforkTrap._clangSFX.mute = false;
                _fireBallAbility._fireWooshSFX.mute = false;
            }
        }
    }
}
