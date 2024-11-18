public class BulletPool : Pool<Bullet>
{
    public Bullet Get()
    {
        var bullet = GetFromPool();
        bullet.Removed += OnReleaseObject;

        return bullet;
    }

    protected override void OnReleaseObject(Bullet poolObject)
    {
        poolObject.Removed -= OnReleaseObject;
        base.OnReleaseObject(poolObject);
    }
}
