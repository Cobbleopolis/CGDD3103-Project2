using System.Collections;
using Cobble.Entity;
using Cobble.Projectile;
using UnityEngine;
using UnityEngine.AI;

namespace Cobble.AI {
    [RequireComponent(typeof(NavMeshAgent))]
    public class AttackAi : AiAction {
        public Transform TargetTransform;

        [SerializeField] private Transform _headTransform;

        public float MinTargetDist = 1.5f;

        public float FoV = 60f;

        public float AttackDistance = 5f;

        public float MeleeDistance = 2f;

        public float MeleeDamage = 20f;

        public float AttackInterval = 1f;

        private GunProjectileSpawn _gunProjectileSpawn;

        private LivingEntity _livingEntity;

        protected override void Start() {
            base.Start();
            _gunProjectileSpawn = GetComponentInChildren<GunProjectileSpawn>();
            _livingEntity = TargetTransform.gameObject.GetComponent<LivingEntity>();
        }

        protected override void OnActivate() {
            if (!NavMeshAgent) return;
            NavMeshAgent.stoppingDistance = MinTargetDist;
            StartCoroutine("RepeatShoot");
        }

        protected override void OnDeactivate() {
//            if (!NavMeshAgent) return;
            StopCoroutine("RepeatShoot");
        }

        public override void Call() {
            if (!TargetTransform) return;
            NavMeshAgent.SetDestination(TargetTransform.position);
        }

        private void Shoot() {
            RaycastHit hit;
            if (_livingEntity &&
                (!NavMeshAgent.hasPath &&
                 Vector3.Distance(transform.position, TargetTransform.position) <= MeleeDamage ||
                 NavMeshAgent.remainingDistance <= MeleeDistance)) {
                _livingEntity.Damage(MeleeDamage);
            } else if (_gunProjectileSpawn &&
                       NavMeshAgent.remainingDistance <= AttackDistance &&
                       Vector3.Angle(TargetTransform.position - _headTransform.position, _headTransform.forward) <=
                       FoV &&
                       Physics.Linecast(_headTransform.position, TargetTransform.position, out hit) &&
                       hit.transform == TargetTransform) {
                _gunProjectileSpawn.Fire();
            }
        }

        private IEnumerator RepeatShoot() {
            while (IsAiActive) {
                yield return new WaitForSeconds(AttackInterval);
                Shoot();
            }
        }
    }
}