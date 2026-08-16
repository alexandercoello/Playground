using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Scripts.PlayerUI;

namespace Scripts
{
    public class Weapon : MonoBehaviour
    {
        [Header("Bullet")]
        public Bullet bulletPrefab;
        public Transform bulletSpawn;
        public float bulletVelocity = 30;
        public float bulletPrefabLifeTime = 3f;
        public int damage = 1;

        //Player UI Reticle
        public PlayerUIReticle reticle;

        [Header("Player")]
        public Camera playerCamera;
        public PlayerUIText ammunitionUIText;

        [Header("Shooting")]
        bool isShooting;
        bool canShoot;
        bool isReloading;
        bool allowReset = true;
        public float shootingDelay = 1f;
        public ShootingMode shootingMode;
        public KeyCode FireWeaponKey = KeyCode.Mouse0;
        public KeyCode AimDownSightsKey = KeyCode.Mouse1;
        //Burst
        public int BulletsPerBurst = 3;
        int burstBulletsLeft;
        //Base spread intensity when hipfiring the weapon (Should be 0 <> 1)
        public float HipfireSpreadIntensity;
        //Current spread intentsity incluing weapon heat and movement penalties
        public float currentSpreadIntensity;


        [Header("Ammunition")]
        public int AmmunitionCapacity = 0;
        public int AmmunitionRemaining = 0;
        public float ReloadSpeed;
        public KeyCode ReloadKey = KeyCode.R;
        public ReloadType reloadType;

        
        [Header("Heat")]
        //Tracks how long weapons has consistently been firing
        float CurrentHeat = 0;
        //Should be less than 1000
        public float MaxHeat;
        public float HeatIncrement;
        public float HeatDecrement;

        public enum ShootingMode
        {
            Single,
            Burst,
            Auto
        }

        public enum ReloadType
        {
            //Works single shot bolt and chamber fed weapons like some rifles/shotguns
            Single,
            //Works for magazine fed weapons that set AmmunitionRemaining == AmmunitionCapacity
            Magazine
        }

        void Start()
        {
            canShoot = true;
            AmmunitionRemaining = AmmunitionCapacity;
            burstBulletsLeft = BulletsPerBurst;
            ammunitionUIText.ShowPlayerUI();
            CheckAndUpdateAmmunitionUI();
        }

        // Update is called once per frame
        void Update()
        {
            bool weaponWasFired = false;

            if(shootingMode == ShootingMode.Auto)
            {
                //Holding down fire button
                isShooting = Input.GetKey(FireWeaponKey);
            }
            else //Must be Burst or Single 
            {
                isShooting = Input.GetKeyDown(FireWeaponKey);
            }

            if(canShoot && isShooting && !isReloading)
            {
                burstBulletsLeft = BulletsPerBurst;
                canShoot = false;
                FireWeapon();
                weaponWasFired = true;
            }

            UpdateWeaponHeat(weaponWasFired);

            if(Input.GetKeyDown(ReloadKey))
            {
                StartReload();
            }
        }

        private void FireWeapon()
        {
            if(AmmunitionRemaining <= 0)
                return;
            

            Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

            Bullet bullet = Instantiate(bulletPrefab, bulletSpawn.position, quaternion.identity);
            
            bullet.Damage = damage;
            bullet.transform.forward = shootingDirection;
            bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

            StartCoroutine(DestoryBulletAfterTime(bullet, bulletPrefabLifeTime));

            DecrementAmmunitionRemaining();

            if(allowReset)
            {
                Invoke("ResetShot", shootingDelay);
                allowReset = false;
            }

            //BurstMode
            if(shootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
            {
                burstBulletsLeft--;
                Invoke("FireWeapon", shootingDelay);
            }
        }

        private void DecrementAmmunitionRemaining()
        {
            if (AmmunitionRemaining > 0)
            {
                AmmunitionRemaining -= 1;
            }
            CheckAndUpdateAmmunitionUI();
        }

        private void StartReload()
        {
            if(AmmunitionRemaining < AmmunitionCapacity)
            {
                isReloading = true;
                Invoke("ReloadWeapon", ReloadSpeed);
            }
        }

        private void ReloadWeapon()
        {
            switch (reloadType)
            {
                case ReloadType.Single:
                    AmmunitionRemaining += 1;
                    break;
                case ReloadType.Magazine:
                    AmmunitionRemaining = AmmunitionCapacity;
                    break;
            }
            isReloading = false;
            canShoot = true;
            CheckAndUpdateAmmunitionUI();
        }

        private Vector3 CalculateDirectionAndSpread()
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;

            if(Physics.Raycast(ray, out hit))
            {
                //Hitting Something
                targetPoint = hit.point;
            }
            else
            {
                //Shot hits nothing
                targetPoint = ray.GetPoint(100);
            }

            Vector3 direction = targetPoint - bulletSpawn.position;
            float spreadX = UnityEngine.Random.Range(-currentSpreadIntensity, currentSpreadIntensity);
            float spreadY = UnityEngine.Random.Range(-currentSpreadIntensity, currentSpreadIntensity);

            
            return direction + new Vector3(spreadX, spreadY, 0);
        }

        //Set a Less than one value to set aim penalties (while not ADS)
        private void CalculateSpreadIntensity()
        {
            //Calculate Heat penalty
            float heatAimPenatly = CurrentHeat * 0.001f;
            
            //TODO: Calculate Movement penalty
            float movementAimPenalty = 0;

            float totalAimPenalty = heatAimPenatly + movementAimPenalty + 1;

            currentSpreadIntensity = HipfireSpreadIntensity * totalAimPenalty;
            
            //reticle.UpdateReticleAccuracy();
        }

        //Manage Weapon Heat system
        //TODO: May be able to improve performance by skipping things when weapon not fired and heat == 0
        private void UpdateWeaponHeat(bool weaponWasFired)
        {
            float newCurrentHeat = CurrentHeat;
            if(weaponWasFired && CurrentHeat < MaxHeat)
            {
                newCurrentHeat += HeatIncrement;
                if(newCurrentHeat > MaxHeat)
                {
                    CurrentHeat = MaxHeat;
                    return;
                }

                CurrentHeat = newCurrentHeat;
            }
            else if(!weaponWasFired && CurrentHeat > 0)
            {
                newCurrentHeat -= HeatDecrement;
                if(newCurrentHeat < 0)
                {
                    CurrentHeat = 0;
                    return;
                }

                CurrentHeat = newCurrentHeat;
            }

            CalculateSpreadIntensity();

        }

        private void ResetShot()
        {
            canShoot = true;
            allowReset = true;
        }


        private IEnumerator DestoryBulletAfterTime(Bullet bullet, float bulletPrefabLifetime)
        {
            yield return new WaitForSeconds(bulletPrefabLifetime);

            if(bullet != null)
            {
                Destroy(bullet.gameObject);
            }
        }

        protected void CheckAndUpdateAmmunitionUI()
        {
            if(ammunitionUIText != null)
            {
                ammunitionUIText.UpdatePlayerUI($"{AmmunitionRemaining}/{AmmunitionCapacity}");                
            }
        }

    }
}