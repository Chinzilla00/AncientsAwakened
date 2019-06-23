using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Hooks
{
	class DragonsGrip : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon's Grip");
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.IlluminantHook);
			item.shootSpeed = 18f;
			item.shoot = mod.ProjectileType("DragonsGripP");
		}
	}
	class DragonsGripP : ModProjectile
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Grip");
        }

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.IlluminantHook);
		}
        
		public override bool? CanUseGrapple(Player player)
		{
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++)
			{
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == projectile.type)
				{
					hooksOut++;
				}
			}
			if (hooksOut > 3)
			{
				return false;
			}
			return true;
		}

		public override float GrappleRange()
		{
			return 500f;
		}

		public override void NumGrappleHooks(Player player, ref int numHooks)
		{
			numHooks = 2;
		}
        
		public override void GrappleRetreatSpeed(Player player, ref float speed)
		{
			speed = 20f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed)
		{
			speed = 8;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize(); 
				distToProj *= 24f; 
				center += distToProj; 
				distToProj = playerCenter - center;    
				distance = distToProj.Length();
				Color drawColor = lightColor;
                
				spriteBatch.Draw(mod.GetTexture("Items/Hooks/DragonsGrip_Chain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height), drawColor, projRotation,
					new Vector2(Main.chain30Texture.Width * 0.5f, Main.chain30Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
	}

}
