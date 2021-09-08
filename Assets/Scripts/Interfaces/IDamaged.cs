public interface IDamaged
{
    bool TakeDamage(float phyDamage, float magDamage);
    bool TakeDamage(float phyDamage, float magDamage, bool crit);

}
