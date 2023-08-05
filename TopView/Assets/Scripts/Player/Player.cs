using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;

public class Player : MonoBehaviour
{
    public bool IsDied { get; private set; } = false;
    public static Player Instance { get { return instance; } }
    public PlayerController Controller { get; private set; }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }
    public SkinnedMeshRenderer skinnedMeshRenderer { get; private set; }
    public Color originalMaterialColor { get; private set; }
    public StateMachine stateMachine { get; private set; }
    //public AnimationEventHandler _AnimationEventHandler { get; private set; }
    //public AudioSource audioSource { get; private set; }

    [SerializeField]
    public Transform shotGenerator;

    public GameObject hitBox;

    [SerializeField]
    private Transform rightHand;
    private static Player instance;

    float ACTIVE_TIME = 3.0f;

    #region Ω∫≈»
    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    public float Speed { get; set; } = 3.0f;

    public float MaxHP { get { return maxHP; } }
    public float CurrentHP { get { return currentHP; } }
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            capsuleCollider = GetComponent<CapsuleCollider>();
            Controller = GetComponent<PlayerController>();

            InitStateMachine();
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            DestroyImmediate(gameObject);
    }

    private void Update()
    {
        stateMachine?.UpdateState();
    }

    private void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }

    public void OnUpdateStats(float maxHP, float currentHP, float Speed)
    {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
        this.Speed = Speed;
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine(StateName.Move, new MoveState());
    }
}
