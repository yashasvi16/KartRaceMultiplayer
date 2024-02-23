using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TMP_Text _playerName;
    PlayerManager _target;
    Transform _targetTransform;
    Renderer _targetRenderer;
    CanvasGroup _targetCanvasGroup;
    Vector3 _targetPosition;

    public Vector3 screenOffset = new Vector3(0, 30, 0);

    // Start is called before the first frame update
    void Awake()
    {
        this.transform.SetParent(GameObject.Find("WorldCanvas").GetComponent<Transform>(), false);
        _targetCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_target == null)
        {
            Destroy(this.gameObject);
            return;
        }

    }

    private void LateUpdate()
    {
        if(_targetRenderer != null)
        {
            this._targetCanvasGroup.alpha = _targetRenderer.isVisible ? 1f : 0f;
        }
        if(_targetTransform != null)
        {
            _targetPosition = _targetTransform.position;
            this.transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + screenOffset;

        }
    }

    public void SetTarget(PlayerManager target)
    {
        if(target == null)
        {
            return;
        }

        _target = target;

        _targetTransform = this._target.GetComponent<Transform>();
        _targetRenderer = this._target.GetComponent<Renderer>();

        if (_playerName != null)
        {
            _playerName.text = _target.photonView.Owner.NickName;
        }
    }
}
