
public interface IHealth
{
    public int MaxHealth { get;}

    public int CurrentHealth { get;}

    public void RestoreHealth(int heal);

    public void TakeDamage(int damage);

    public void Die();

}
