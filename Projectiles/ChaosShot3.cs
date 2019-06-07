namespace AAMod.Projectiles
{
    public class ChaosShot3 : ChaosShot1
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Shot");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            proType = 2;
            projectile.friendly = true;
            projectile.penetrate = -1;
            offsetLeft = true;
        }
    }
}