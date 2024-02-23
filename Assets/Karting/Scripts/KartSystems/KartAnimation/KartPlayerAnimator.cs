using UnityEngine;
using UnityEngine.Assertions;
using Photon.Pun;

namespace KartGame.KartSystems 
{
    public class KartPlayerAnimator : MonoBehaviour
    {
        public Animator PlayerAnimator;
        public ArcadeKart Kart;

        public string SteeringParam = "Steering";
        public string GroundedParam = "Grounded";

        int m_SteerHash, m_GroundHash;

        float steeringSmoother;

        PhotonView _photonView;

        void Awake()
        {
            _photonView = this.GetComponent<PhotonView>();

            if(!_photonView.IsMine)
            {
                return;
            }

            Assert.IsNotNull(Kart, "No ArcadeKart found!");
            Assert.IsNotNull(PlayerAnimator, "No PlayerAnimator found!");
            m_SteerHash  = Animator.StringToHash(SteeringParam);
            m_GroundHash = Animator.StringToHash(GroundedParam);
        }

        void Update()
        {
            if (!_photonView.IsMine)
            {
                return;
            }

            steeringSmoother = Mathf.Lerp(steeringSmoother, Kart.Input.TurnInput, Time.deltaTime * 5f);
            PlayerAnimator.SetFloat(m_SteerHash, steeringSmoother);

            // If more than 2 wheels are above the ground then we consider that the kart is airbourne.
            PlayerAnimator.SetBool(m_GroundHash, Kart.GroundPercent >= 0.5f);
        }
    }
}
