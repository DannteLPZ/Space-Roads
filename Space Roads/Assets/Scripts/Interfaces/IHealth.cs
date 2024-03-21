
public interface IHealth
{
    int maxHealth { get; set; }

    int currentHealth { get; set; }

    public void RestoreHealth();

    public void GetDamage();

}
