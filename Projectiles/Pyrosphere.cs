using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles
{
	public class Pyrosphere : ModProjectile
	{
		public static Texture2D chainTex;

		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Pyrosphere");
		}		
		
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.timeLeft = 3600;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.penetrate = -1;
            projectile.knockBack = 1;
            projectile.melee = true;
        }

		public override void AI()
		{
			BaseAI.AIFlail(projectile, ref projectile.ai, false, 160f);
			projectile.direction = projectile.spriteDirection = Main.player[projectile.owner].direction;
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.OnFire, 120);
        }

        public override bool OnTileCollide(Vector2 value2)
		{
			BaseAI.TileCollideFlail(projectile, ref value2);
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color dColor)
		{
			if (chainTex == null) chainTex = mod.GetTexture("Projectiles/Pyrosphere_Chain");
			BaseMod.BaseDrawing.DrawChain(spriteBatch, chainTex, 0, projectile.Center, Main.player[projectile.owner].Center);
			return true;
		}
	}
}