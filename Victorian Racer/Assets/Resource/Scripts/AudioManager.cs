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

        _chantVolume = _mainMenuChant.volume;
        _driftVolume = _carControls._carDriveSFX.volume;
        _driftVolume = _carControls._driftSFX.volume;
        _BGMVolume = _carControls._BGM.volume;
        _bellVolume = _respawnCheckpoints._bellSFX.volume;
        _graveyardLeftVolume = _respawnCheckpoints._graveyardBGMLeft.volume;
        _graveyardRightVolume = _respawnCheckpoints._graveyardBGMRight.volume;
        _clangVolume = _pitchforkTrap._clangSFX.volume;
        _wooshVolume = _fireBallAbility._fireWooshSFX.volume;
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

        if (_mute == true)
        {
            if (SceneManager.sceneCount == 0)
            {
                _mainMenuChant.volume = 0.0f;
            }
            
            if (SceneManager.sceneCount == 1)
            {
                _carControls._carDriveSFX.volume = 0.0f;
                _carControls._driftSFX.volume = 0.0f;
                _carControls._BGM.volume = 0.0f;
                _respawnCheckpoints._bellSFX.volume = 0.0f;
                _respawnCheckpoints._graveyardBGMLeft.volume = 0.0f;
                _respawnCheckpoints._graveyardBGMRight.volume = 0.0f;
                _raceCountdown._carStartSFX.volume = 0.0f;
                _pitchforkTrap._clangSFX.volume = 0.0f;
                _fireBallAbility._fireWooshSFX.volume = 0.0f;
            }
        }

        if (_mute == true)
        {
            if (SceneManager.sceneCount == 0)
            {
                _mainMenuChant.volume = _chantVolume;
            }
            
            if (SceneManager.sceneCount == 1)
            {
                _carControls._carDriveSFX.volume = _driveVolume;
                _carControls._driftSFX.volume = _driftVolume;
                _carControls._BGM.volume = _BGMVolume;
                _respawnCheckpoints._bellSFX.volume = _bellVolume;
                _respawnCheckpoints._graveyardBGMLeft.volume = _graveyardLeftVolume;
                _respawnCheckpoints._graveyardBGMRight.volume = _graveyardRightVolume;
                _raceCountdown._carStartSFX.volume = _startVolume;
                _pitchforkTrap._clangSFX.volume = _clangVolume;
                _fireBallAbility._fireWooshSFX.volume = _wooshVolume;
            }
        }
    }
}
