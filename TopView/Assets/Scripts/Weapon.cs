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
        yield return new WaitForSeconds(0.4f); // 0.1�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        // 2
        yield return new WaitForSeconds(0.3f); // 1������ ���
        meleeArea.enabled = false;

        // 3
        yield return new WaitForSeconds(0.3f); // 1������ ���
        trailEffect.enabled = false;

        //yield break; // yield Ż��
    }

    // Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���η�ƾ
    // Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)

    IEnumerator Shot()
    {
        // #1. �Ѿ� �߻�
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        yield return null;
        
        // #2. ź�� ����
        /*
        GameObject intantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caseRigid = intantBullet.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caseRigid.AddForce(caseVec, ForceMode.Impulse);
        caseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
        */
    }
}
