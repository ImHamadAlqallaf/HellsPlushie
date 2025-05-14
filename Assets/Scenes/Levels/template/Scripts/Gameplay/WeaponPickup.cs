using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class WeaponPickup : Pickup
    {
        [Tooltip("The prefab for the weapon that will be added to the player on pickup")]
        public WeaponController WeaponPrefab;

        protected override void Start()
        {
            base.Start();

            // Set all children layers to default (to prevent seeing weapons through meshes)
            foreach (Transform t in GetComponentsInChildren<Transform>())
            {
                if (t != transform)
                    t.gameObject.layer = 0;
            }

            if (WeaponPrefab == null)
                Debug.LogError($"[{name}] WeaponPrefab is not assigned!", this);
        }

        protected override void OnPicked(PlayerCharacterController byPlayer)
        {
            if (WeaponPrefab == null)
            {
                Debug.LogWarning($"[{name}] Pickup has no WeaponPrefab, skipping add.", this);
                return;
            }

            var pwm = byPlayer.GetComponent<PlayerWeaponsManager>();
            if (pwm == null)
            {
                Debug.LogError($"[{name}] PlayerCharacterController '{byPlayer.name}' has no PlayerWeaponsManager!", byPlayer);
                return;
            }

            bool added = pwm.AddWeapon(WeaponPrefab);
            if (!added)
            {
                Debug.LogWarning($"[{name}] PlayerWeaponsManager failed to AddWeapon({WeaponPrefab.name}).", pwm);
                return;
            }

            // if this was the first weapon, auto-switch to it
            if (pwm.GetActiveWeapon() == null)
                pwm.SwitchWeapon(true);

            PlayPickupFeedback();
            Destroy(gameObject);
        }
    }
}
