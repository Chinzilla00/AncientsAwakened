using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DMC : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DMC");
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Electrified"), 500);
        }

        public override void SetDefaults()
        {
			projectile.aiStyle = -1;
            projectile.width = 26;
            projectile.height = 26;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			BaseAI.TileCollideBoomerang(projectile, ref projectile.velocity, true);
			return false;
		}

        public override void AI()
        {
            Player p = Main.player[projectile.owner];
            BaseAI.AIBoomerang(projectile, ref projectile.ai, p.position, p.width, p.height, true, 24f, 45, 1.2f, .5f, false);
        }
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(mod.BuffType<Buffs.Electrified>(), 90);
        }
    }
}
