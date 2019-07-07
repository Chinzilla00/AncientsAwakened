namespace AAMod.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class Rift : VoidStarPF
    {
        public override void SetDefaults()
		{
            base.SetDefaults();
            projectile.magic = false;
            projectile.melee = true;
        }
    }
}
