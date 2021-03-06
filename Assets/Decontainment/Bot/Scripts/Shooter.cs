using System;
using UnityEngine;

namespace Bot
{
    public class Shooter : MonoBehaviour
    {
        public Trigger shotRequested;
        public bool async;

        public Hardpoint hardpoint;
        public WeaponData weaponData;

        private float cooldownTimer;

        public bool Running { get { return !async && cooldownTimer > 0; } }

        void FixedUpdate()
        {
            cooldownTimer -= Time.fixedDeltaTime;
            if (shotRequested.Value && cooldownTimer <= 0) {
                cooldownTimer = weaponData.cooldown;
                async = true;

                Projectile.CreateProjectile(this, weaponData.projectilePrefab, hardpoint.transform.position, hardpoint.transform.right);
            }
        }

        public void Init(Hardpoint hardpoint, WeaponData weaponData)
        {
            this.hardpoint = hardpoint;
            this.weaponData = weaponData;

            this.hardpoint.Init(weaponData.hardpointColor);
        }
    }
}