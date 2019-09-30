namespace AAMod.Projectiles
{
    public class YamataTooth : ShenTooth
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            projectile.CloneDefaults(mod.ProjectileType<ShenTooth>());
            type = 2;
        }
    }
}
