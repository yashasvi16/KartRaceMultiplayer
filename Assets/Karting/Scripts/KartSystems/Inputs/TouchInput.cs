using System.Collections;
using System.Collections.Generic;
using KartGame.KartSystems;
using UnityEngine;
using SimpleInputNamespace;
using Photon.Pun;

namespace KartGame.KartSystems
{
    public class TouchInput : BaseInput
    {
        public string TurnInputName = "Horizontal";
        public bool _Accelerate;
        public bool _Brake;

        PhotonView _photonView;

        private void Awake()
        {
            _photonView = this.GetComponent<PhotonView>();
        }
        public void Update()
        {
            if (!_photonView.IsMine)
            {
                return;
            }

            float v = SimpleInput.GetAxis("Vertical");

            if (v > 0.1)
            {
                _Accelerate = true;
                _Brake = false;
            }
            else if(v < -0.1f)
            {
                _Accelerate = false;
                _Brake = true;
            }
            else
            {
                _Accelerate = false;
                _Brake = false;
            }

        }
        public override InputData GenerateInput()
        {
            if (!_photonView.IsMine)
            {
                return new InputData();
            }

            return new InputData
            {
                Accelerate = _Accelerate,
                Brake = _Brake,
                TurnInput = SimpleInput.GetAxis(TurnInputName),
            };
        }
    }
}

