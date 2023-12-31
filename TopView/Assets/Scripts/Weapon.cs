using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { HANDGUN, RIFLE };
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    [SerializeField] private Transform bulletPos;
    public GameObject bullet;
    //public Transform bulletCasePos;
    //public GameObject bulletCase;

    public void Use()
    {
        if(type == Type.HANDGUN)
        {
            StopCoroutine("Swing");

            StartCoroutine("Swing");
        }
        else if (type == Type.RIFLE && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.4f); // 0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        // 2
        yield return new WaitForSeconds(0.3f); // 1프레임 대기
        meleeArea.enabled = false;

        // 3
        yield return new WaitForSeconds(0.3f); // 1프레임 대기
        trailEffect.enabled = false;

        //yield break; // yield 탈출
    }

    // Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴
    // Use() 메인루틴 + Swing() 코루틴 (Co-Op)

    IEnumerator Shot()
    {
        // #1. 총알 발사
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        
        // #2. 탄피 배출
        /*
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantBullet.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        */
    }
}
