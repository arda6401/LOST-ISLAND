using System.Collections.Generic;
using LostIsland.Combat;
using LostIsland.Core;
using Mirror;
using Unity.Mathematics;
using UnityEngine;


namespace LostIsland.Weapons
{

    [RequireComponent(typeof(PlayerInputSender), typeof(Health))]
    public class PlayerWeapon : NetworkBehaviour
    {
        [Header("Data")]
        [SerializeField] private WeaponDefinition definition;
        [SerializeField] private float inputTimeout = 0.25f;


        [Header("muzzle")]
        [SerializeField] private float muzzleHeight = 1f;
        [SerializeField] private float muzzleOffset = 0.7f;

        [Header("Server")]
        [SerializeField] private LayerMask hitMask = ~0;

        [Header("Virtual (client)")]
        [SerializeField] private GameObject projectileVisualPrefab;
        [SerializeField] private GameObject impactEffectPrefab;

        private PlayerInputSender input;
        private Health ownHealth;
        private WeaponStats stats;


        private void Awake()
        {
            input = GetComponent<PlayerInputSender>();
            ownHealth = GetComponent<Health>();


            if (definition == null)
            {
                Debug.LogError("[PlayerWeapon] Definition not added.", this);
                enabled = false;
                return;
            }

            stats = WeaponStats.From(definition);

        }

        #region  Server

        private struct Projectile
        {
            public ushort Id;
            public Vector3 position;
            public Vector3 Direction;
            public float Remaining;
        }


        private readonly List<Projectile> projectiles = new List<Projectile>();
        private float nextFireTime;
        private ushort NextProjectileId = 1;


        [ServerCallback]
        private void FixedUpdate()
        {
            TryFire();
            StepProjectiles(Time.fixedDeltaTime);
        }


        [Server]
        private void TryFire()
        {
            if (ownHealth.IsDead) return;
            if (!input.GetFiring(inputTimeout)) return;
            if (Time.time < nextFireTime) return;

            nextFireTime = Time.time + stats.FireInterval;

            Vector3 dir = Quaternion.Euler(0f, input.ServerAimAngle, 0f) * Vector3.forward;
            Vector3 origin = transform.position + Vector3.up * muzzleHeight + dir * muzzleOffset;

            Debug.Log("[Server] Fire!");


            ushort id = NextProjectileId++;

            projectiles.Add(new Projectile
            {
                Id = id,
                Direction = dir,
                position = origin,
                Remaining = stats.Range
            });


            RpcFired(id, origin, AimAngleCode.Encode(input.ServerAimAngle));

        }

        [Server]
        private void StepProjectiles(float dt)
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                Projectile p = projectiles[i];
                float distance = Mathf.Min(stats.ProjectileSpeed * dt, p.Remaining);


                if (Physics.SphereCast(p.position, stats.ProjectileRadius, p.Direction,
                out RaycastHit hit, distance, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Debug.DrawLine(p.position, hit.point, Color.red, 0.5f);
                    OnProjectileHit(hit);
                    Vector3 hitCenter = p.position + p.Direction * hit.distance;
                    RpcProjectileHit(p.Id, hitCenter);
                    projectiles.RemoveAt(i);
                    continue;
                }


                Vector3 next = p.position + p.Direction * distance;
                Debug.DrawLine(p.position, next, Color.yellow, 0.5f);
                p.position = next;
                p.Remaining -= distance;


                if (p.Remaining <= 0.001f) projectiles.RemoveAt(i);
                else projectiles[i] = p;

            }
        }



        [Server]
        private void OnProjectileHit(RaycastHit hit)
        {
            IDamageable target = hit.collider.GetComponentInParent<IDamageable>();
            if (target == null || ReferenceEquals(target, ownHealth)) return;

            target.TakeDamage(stats.Damage);
            Debug.Log($"[Server] Hit {target} ");

        }

        #endregion



        #region  Client


        private struct VisualProjectile
        {
            public ushort Id;
            public Transform Transform;
            public Vector3 Direction;
            public float Remaining;

        }

        private readonly List<VisualProjectile> visuals = new List<VisualProjectile>();


        [ClientRpc(channel = Channels.Unreliable)]
        private void RpcFired(ushort id, Vector3 origin, ushort aimAngle)
        {
            if (projectileVisualPrefab == null) return;


            Vector3 dir = Quaternion.Euler(0f, AimAngleCode.Decode(aimAngle), 0f) * Vector3.forward;
            GameObject go = Instantiate(projectileVisualPrefab, origin, Quaternion.LookRotation(dir));

            visuals.Add(new VisualProjectile
            {
                Id = id,
                Transform = go.transform,
                Direction = dir,
                Remaining = stats.Range
            });
        }


        [ClientRpc]
        private void RpcProjectileHit(ushort id, Vector3 position)
        {
            for (int i = 0; i < visuals.Count; i++)
            {
                if (visuals[i].Id != id) continue;


                Destroy(visuals[i].Transform.gameObject);
                visuals.RemoveAt(i);
                break;

            }

            if(impactEffectPrefab == null) return;

            GameObject fx = Instantiate(impactEffectPrefab,position, quaternion.identity);
            Destroy(fx,2f);
        }



        [ClientCallback]
        private void Update()
        {
            for (int i = visuals.Count - 1; i >= 0; i--)
            {
                VisualProjectile v = visuals[i];
                float distance = Mathf.Min(stats.ProjectileSpeed * Time.deltaTime, v.Remaining);

                v.Transform.position += v.Direction * distance;
                v.Remaining -= distance;

                if (v.Remaining <= 0.001f)
                {
                    Destroy(v.Transform.gameObject);
                    visuals.RemoveAt(i);
                }
                else
                {
                    visuals[i] = v;
                }
            }
        }


        private void OnDestroy()
        {
            foreach (VisualProjectile v in visuals)
                if (v.Transform != null) Destroy(v.Transform.gameObject);
            visuals.Clear();
        }



        #endregion

    }
}

