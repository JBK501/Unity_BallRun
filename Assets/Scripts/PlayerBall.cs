using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBall : MonoBehaviour
{
    public float jumpPower;
    public int itemCount;
    public GameManagerLogic manager;

    bool isJump;

    Rigidbody rigid;
    AudioSource audioSource;

    private void Awake()
    {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 점프
        if(Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0),ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 수평이동
        float v = Input.GetAxisRaw("Vertical"); // 수직이동

        rigid.AddForce(new Vector3 (h,0,v),ForceMode.Impulse);
    }

    // 바닥 충돌 시 점프 플래그 변경
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
            isJump = false;
    }


    // 아이템 먹을 시 개수 추가
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            audioSource.Play();
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if(other.tag == "Point")
        {
            if(itemCount == manager.totalItemCount)
            {
                // Game Clear
                SceneManager.LoadScene(manager.stage+1);
            }
            else
            {
                // Restart
                SceneManager.LoadScene(manager.stage);
            }
        }
    }
}
