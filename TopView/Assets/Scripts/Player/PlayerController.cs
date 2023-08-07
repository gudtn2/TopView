using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using CharacterController;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    Animator anim;
    public Vector3 MouseDir { get; private set; }
    public Player player { get; private set; }
    public Vector3 inputDir { get; private set; }
    public Vector3 moveDir { get; private set; }

    MoveState moveState;
    AttackState attackState;

    float fireDelay;
    bool isFireReady;
    bool isReload;
    bool rDown;

    #region ¹Ù´ÚÃ¼Å©
    private int groundLayer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

        groundLayer = 1 << LayerMask.NameToLayer("Ground");

        moveState = player.stateMachine.GetState(StateName.Move) as MoveState;
        attackState = player.stateMachine.GetState(StateName.ATTACK) as AttackState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (player.IsDied)
        {
            inputDir = Vector3.zero;
            return;
        }

        Vector2 input = context.ReadValue<Vector2>();
        inputDir = new Vector3(input.x, 0f, input.y);
        if (attackState.IsAttack == false)
            LookAt(inputDir);
    }

    public void OnClickLeftMouse(InputAction.CallbackContext context)
    {
        fireDelay += Time.deltaTime;
        isFireReady = player.equipWeapon.rate < fireDelay;
               
        player.equipWeapon.Use();
        anim.SetTrigger(player.equipWeapon.type == Weapon.Type.HANDGUN ? "HandGun" : "Shoot");
        fireDelay = 0;        
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (player.ammo == 0)
            return;

        if (player.equipWeapon.curAmmo == player.equipWeapon.maxAmmo)
            return;

        rDown = true;
        if (rDown || player.equipWeapon.curAmmo == 0 && isFireReady && !isReload)
        {
            anim.SetLayerWeight(1, 1f);
            isReload = true;

            anim.SetBool("Reloading", true);

            Invoke("ReloadOut", 1.5f);
        }

    }
    void ReloadOut()
    {
        player.ammo += player.equipWeapon.curAmmo;
        int reAmmo = player.ammo < player.equipWeapon.maxAmmo ? player.ammo : player.equipWeapon.maxAmmo;
        player.ammo -= reAmmo;
        player.equipWeapon.curAmmo = reAmmo;
        Debug.Log(reAmmo);
        isReload = false;
        anim.SetBool("Reloading", false);
    }

    protected Vector3 GetMouseWorldPosition()
    {
        //if (player.IsDied || player.stateMachine.CurrentState is HitState)
        //    return Vector3.zero;

        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.red, 5f);

        if (Physics.Raycast(ray, out RaycastHit HitInfo, Mathf.Infinity, groundLayer))
        {
            Vector3 target = HitInfo.point;
            Vector3 myPosition = new Vector3(transform.position.x, 0f, transform.position.z);
            target.Set(target.x, 0f, target.z);
            return (target - myPosition).normalized;
        }
        return Vector3.zero;
    }


    public void LookAt(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            transform.rotation = targetAngle;
        }
    }
}
