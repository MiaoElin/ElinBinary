using UnityEngine;
using ElinBinary.BehaviorTree;

namespace ElinBinary {
    public class Sample_BHTree : MonoBehaviour {
        [SerializeField] GameObject rolePrefab;
        Vector2 moveAxis;
        RoleEntity owner;
        RoleEntity monster;
        RoleEntity curer;
        void Start() {
            owner = GameObject.Instantiate(rolePrefab).GetComponent<RoleEntity>();
            Debug.Assert(owner != null);
            owner.Ctor(Vector2.zero, 100, 10, 1, 2);
            owner.GetComponent<SpriteRenderer>().color = Color.blue;

            monster = GameObject.Instantiate(rolePrefab).GetComponent<RoleEntity>();
            monster.GetComponent<SpriteRenderer>().color = Color.red;
            monster.Ctor(Vector2.zero + Vector2.right * 10, 50, 0, 1, 5);

            curer = GameObject.Instantiate(rolePrefab).GetComponent<RoleEntity>();
            curer.GetComponent<SpriteRenderer>().color = Color.green;
            curer.Ctor(new Vector2(2, 5), 100, 0, 1, 0);

            #region Monster
            // === SearchAction ===
            BHTreeNode searchAction = new BHTreeNode();
            searchAction.InitAction();
            // - PreconditionHandle
            searchAction.PreconditionHandle = () => {
                bool allow = false;
                if (Vector2.SqrMagnitude(owner.Pos() - monster.Pos()) <= 8 * 8) {
                    allow = true;
                }
                return allow;
            };
            // - ActHandle
            searchAction.ActNotEnterHandle = (dt) => {
                Debug.Log("Monster Has not Target,back to normal");
                return BHTreeNodeStatus.Done;
            };

            searchAction.ActEnterHandle = (dt) => {
                Debug.Log("Monster Has Target");
                monster.hasTarget = true;
                return BHTreeNodeStatus.Done;
            };

            // === AttackAction_1 ===
            BHTreeNode attackAction1 = new BHTreeNode();
            attackAction1.InitAction();
            // - PreconditionHandle
            attackAction1.PreconditionHandle = () => {
                if (!monster.hasTarget) {
                    return false;
                } else {
                    if (Vector2.SqrMagnitude(owner.Pos() - monster.Pos()) < 5 * 5) {
                        Debug.Log("Monster Enter Attack1");
                        return true;
                    } else {
                        return false;
                    }
                }
            };
            // - ActRunningHandle
            attackAction1.ActRunningHandle = (dt) => {
                if (monster.cd1 <= 0) {
                    Debug.Log("Skill1");
                    owner.hp -= 10;
                    if (owner.hp <= 0) {
                        owner.hp = 0;
                    }
                    monster.cd1 = monster.cdMax1;
                }
                return BHTreeNodeStatus.Done;
            };


            // === AttackAction_2 ===
            BHTreeNode attackAction2 = new BHTreeNode();
            attackAction2.InitAction();
            // - PreconditionHandle
            attackAction2.PreconditionHandle = () => {
                if (!monster.hasTarget) {
                    return false;
                } else {
                    if (Vector2.SqrMagnitude(owner.Pos() - monster.Pos()) < 5 * 5) {
                        Debug.Log("Monster Enter Attack2");
                        return true;
                    } else {
                        return false;
                    }
                }
            };
            // - ActRunningHandle
            attackAction2.ActRunningHandle = (dt) => {
                if (monster.cd2 <= 0) {
                    monster.cd2 = monster.cdMax2;

                    Debug.Log("Skill2");
                    owner.hp -= 10;
                    if (owner.hp <= 0) {
                        owner.hp = 0;
                    }
                }
                return BHTreeNodeStatus.Done;
            };

            // - BHTreeNode Root
            BHTreeNode monsterRoot = new BHTreeNode();
            monsterRoot.InitContainer(BHTreeNodeType.Sequence);
            monsterRoot.childrens.Add(searchAction);
            monsterRoot.childrens.Add(attackAction1);
            monsterRoot.childrens.Add(attackAction2);

            // - BHTree Tree
            BHTree monsterTree = new BHTree();
            monsterTree.InitRoot(monsterRoot);
            monster.aiCom.tree = monsterTree;
            #endregion

            #region Curer
            BHTreeNode cureAction = new BHTreeNode();
            cureAction.InitAction();
            cureAction.PreconditionHandle = () => {
                if (Vector2.SqrMagnitude(owner.Pos() - curer.Pos()) <= 3 * 3) {
                    Debug.Log("Curer Enter Working");
                    return true;
                } else {
                    return false;
                }
            };

            cureAction.ActRunningHandle = (dt) => {
                if (curer.cd1 <= 0) {
                    curer.cd1 = curer.cdMax1;

                    owner.hp += 5;
                    if (owner.hp >= owner.hpMax) {
                        owner.hp = owner.hpMax;
                        Debug.Log("Owner is full of flood");
                    } else {
                        Debug.Log("Curer is Working");
                    }
                }
                return BHTreeNodeStatus.Done;
            };

            BHTreeNode cureRoot = new BHTreeNode();
            cureRoot.InitContainer(BHTreeNodeType.Sequence);
            cureRoot.childrens.Add(cureAction);

            BHTree cureTree = new BHTree();
            cureTree.InitRoot(cureRoot);
            curer.aiCom.tree = cureTree;

            #endregion

        }

        void Update() {
            float dt = Time.deltaTime;
            // Owner
            owner.Move(MoveAxisTick(), dt);

            // Monster
            monster.cd1 -= dt;
            if (monster.cd1 <= 0) {
                monster.cd1 = 0;
            }
            monster.cd2 -= dt;
            if (monster.cd2 <= 0) {
                monster.cd2 = 0;
            }
            monster.aiCom.tree.Execute(dt);

            // Curer
            curer.cd1 -= dt;
            if (curer.cd1 <= 0) {
                curer.cd1 = 0;
            }
            curer.aiCom.tree.Execute(dt);

        }

        Vector2 MoveAxisTick() {
            moveAxis = Vector2.zero;
            if (Input.GetKey(KeyCode.A)) {
                moveAxis.x = -1;
            } else if (Input.GetKey(KeyCode.D)) {
                moveAxis.x = 1;
            }

            if (Input.GetKey(KeyCode.W)) {
                moveAxis.y = 1;
            } else if (Input.GetKey(KeyCode.S)) {
                moveAxis.y = -1;
            }

            return moveAxis;
        }
    }
}
