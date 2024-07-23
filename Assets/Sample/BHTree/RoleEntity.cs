using UnityEngine;
namespace ElinBinary.BehaviorTree {
    public class RoleEntity : MonoBehaviour {
        public int hp;
        public int hpMax;
        public float moveSpeed;
        public bool hasTarget;
        public bool arrivedTarget;
        public float cd1;
        public float cdMax1;
        public float cd2;
        public float cdMax2;
        public RoleAIComponent aiCom;

        public RoleEntity() {
            aiCom = new RoleAIComponent();
        }

        public void Ctor(Vector2 pos, int hpMax, float moveSpeed, float cdMax1, float cdMax2) {
            transform.position = pos;
            this.hp = hpMax;
            this.hpMax = hpMax;
            this.moveSpeed = moveSpeed;
            this.cd1 = cdMax1;
            this.cdMax1 = cdMax1;
            this.cd2 = cdMax2;
            this.cdMax2 = cdMax2;
        }

        public void AICom_AddRoot(BHTree root) {
            aiCom.tree = root;
        }

        public Vector2 Pos() {
            return transform.position;
        }

        public void Move(Vector2 dir, float dt) {
            transform.position += (Vector3)dir.normalized * moveSpeed * dt;
        }
    }
}
