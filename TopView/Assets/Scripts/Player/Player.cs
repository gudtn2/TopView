using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;

public class Player : MonoBehaviour
{
    public bool IsDied { get; private set; } = false;
    public static Player Instance { get { return Instance; } }
    public PlayerController Controller { get; private set; }
    public Rigidbody rigidBody { get; private set; }
    public Animator animator { get; private set; }
    public CapsuleCollider capsuleCollider { get; private set; }
    public SkinnedMeshRenderer skinnedMeshRenderer { get; private set; }
    public Color originalMaterialColor { get; private set; }
    //public AnimationEventHandler _AnimationEventHandler { get; private set; }
    public AudioSource audioSource { get; private set; }

    public Transform shotGenerator;

    public GameObject hitBox;

    [SerializeField]
    private Transform rightHand;
    private static Player instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
