using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Unity.FPS.Game;  // for WeaponController, CrosshairData

namespace Unity.FPS.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class MeleeWeaponController : WeaponController
    {
        [Header("Internal References")]
        [Tooltip("Tip of the sword, where we sphere-cast from")]
        public Transform WeaponTip;

        [Header("Attack Parameters")]
        [Tooltip("Damage per successful hit")]
        public float Damage = 50f;
        [Tooltip("How far (in world units) the swing reaches")]
        public float AttackRange = 2f;
        [Tooltip("Radius of the swing arc")]
        public float AttackRadius = 0.5f;
        [Tooltip("Minimum time (s) between swings")]
        public float DelayBetweenAttacks = 0.7f;

        [Header("Audio & Animations")]
        const string k_AnimAttackParam = "Attack";

        [Tooltip("One-shot swing SFX")]
        public AudioClip SwingSfx;

        [Header("Events")]
        public UnityEvent OnAttack;            // before hit-detection
        public event Action OnAttackProcessed; // after hit detection

        AudioSource _audioSource;
        float _lastAttackTime = Mathf.NegativeInfinity;
        bool _isAttacking = false;

        void Awake()
        {
            // cache AudioSource
            _audioSource = GetComponent<AudioSource>();

            // sanity checks
            Debug.Assert(WeaponTip != null, "Assign WeaponTip in Inspector!");
            Debug.Assert(WeaponAnimator != null, "Assign Animator (inherited from WeaponController)!");
            Debug.Assert(WeaponRoot != null, "Assign WeaponRoot (inherited from WeaponController)!");
        }

        void Update()
        {
        // only run your melee?specific input
        if (!_isAttacking
            && Input.GetMouseButtonDown(0)
            && Time.time >= _lastAttackTime + DelayBetweenAttacks)
        {
            StartCoroutine(PerformAttackRoutine());
        }

        // note: we do NOT call base.Update() here,
        // so the WeaponController auto?shoot logic never runs.
    }

        IEnumerator PerformAttackRoutine()
        {
            _isAttacking = true;
            _lastAttackTime = Time.time;

            // 1) Animation & SFX
            WeaponAnimator.SetTrigger(k_AnimAttackParam);
            if (SwingSfx != null)
                _audioSource.PlayOneShot(SwingSfx);

            OnAttack?.Invoke();

            // 2) Wait for your animation to reach the hit frame
            //    Adjust the 0.1f to match your swing timing or use AnimationEvent instead.
            yield return new WaitForSeconds(0.1f);

            // 3) Sphere-cast attack
            Vector3 origin = WeaponTip.position;
            Vector3 direction = WeaponTip.forward;
            var hits = Physics.SphereCastAll(origin, AttackRadius, direction, AttackRange);

            foreach (var hit in hits)
            {
                var d = hit.collider.GetComponent<IDamageable>();
                if (d != null)
                    d.TakeDamage(Damage);
            }

            OnAttackProcessed?.Invoke();

            // 4) Swing complete
            _isAttacking = false;
        }

        // In MeleeWeaponController.cs

        /// <summary>
        /// Called by your weapon?switcher when toggling this weapon on/off.
        /// </summary>
        public new void ShowWeapon(bool show)
        {
            WeaponRoot.SetActive(show);
        }

    }
}
