namespace CharacterController
{
    public enum StateName
    {
        Move = 0,
        DODGE,
        ATTACK,
        SKILL,
        HIT,
        DIE,

        ENEMY_MOVE = 10000,
        ENEMY_ATTACK,
        ENEMY_HIT,
        ENEMY_DIE,
        ENEMY_STAY,
        ENEMY_CHARGE,
        ENEMY_CHARGE_HIT,
        ENEMY_CLOSE_SKILL,
        ENEMY_FAR_SKILL
    }
}
