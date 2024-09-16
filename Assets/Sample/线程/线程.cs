using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

public class 线程 : MonoBehaviour {
    [SerializeField] Button btnTask;
    [SerializeField] Button btnThread;
    [SerializeField] Button btnSingle;
    [SerializeField] Button btnTogether;
    [SerializeField] Button btnAwait;
    [SerializeField] Button btnWhenAll;
    [SerializeField] Transform panel;
    bool isPanelMove;

    void Awake() {

        btnSingle.onClick.AddListener(() => {
            Thread.Sleep(1000);
            Debug.Log("做菜");
            Thread.Sleep(3000);
            Debug.Log("做饭");
        });

        btnThread.onClick.AddListener(() => {
            Thread t = new Thread(() => {
                Thread.Sleep(1000);
                Debug.Log("做菜");
                Thread.Sleep(3000);
                Debug.Log("做饭");
            });
            t.Start();
        });

        btnTask.onClick.AddListener(() => {
            // 性能更好、接口更多 推荐用这个
            Task.Run(() => {
                Thread.Sleep(1000);
                Debug.Log("做菜");
                Thread.Sleep(3000);
                Debug.Log("做饭");
            });
        });

        btnTogether.onClick.AddListener(() => {
            Task.Run(() => {
                Thread.Sleep(1000);
                Debug.Log("做菜");
            });
            Task.Run(() => {
                Thread.Sleep(3000);
                Debug.Log("做饭");
            });
        });

        btnAwait.onClick.AddListener(async () => {
            // 异步
            await Task.Run(() => {
                Thread.Sleep(3000);
                Debug.Log("做菜");
                Thread.Sleep(3000);
                Debug.Log("做饭");
            });
            Debug.Log("饭菜都做好了！");
        });

        btnWhenAll.onClick.AddListener(() => {
            // 等待
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => {
                Thread.Sleep(3000);
                Debug.Log("做菜");
            }));
            tasks.Add(Task.Run(() => {
                Thread.Sleep(3000);
                Debug.Log("做饭");
            }));
            Task.WhenAll(tasks).ContinueWith(t => {
                Debug.Log("饭菜都做好了！");
            });
        });
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            isPanelMove = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            isPanelMove = false;
        }

        if (isPanelMove) {
            panel.position += Input.mousePositionDelta;
        }
    }

}