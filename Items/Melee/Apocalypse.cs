using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Apocalypse : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Apocalypse");
            Tooltip.SetDefault(@"The Flaming Jacks travel towards the sunset, where
souls travel to reach the afterlife.
Horseman's Blade EX");
        }

		public override void SetDefaults()
		{
            item.melee = true;
            item.damage = 1000;
            item.useStyle = 1;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.shootSpeed = 20f;
            item.width = 54;
			item.height = 54;    
            item.knockBack = 6.5f;
            item.useTime = 12;
			item.useAnimation = 12;
			item.value = 1000000;
            item.expert = true; item.expertOnly = true;

			glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
			glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
			glowmaskDrawColor = Color.White;  //glowmask draw color
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TheHorsemansBlade);
			recipe.AddIngredient(mod, "EXSoul", 1);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            float screenX = Main.screenPosition.X;
            if (player.direction < 0)
            {
                screenX += Main.screenWidth;
            }

            //change to make more/less projectiles
            float screenY = Main.screenPosition.Y;
            screenY += Main.rand.Next(Main.screenHeight);
            Vector2 vector = new Vector2(screenX, screenY);
            float velocityX = target.Center.X - vector.X;
            float velocityY = target.Center.Y - vector.Y;
            velocityX += Main.rand.Next(-50, 51) * 0.1f;
            velocityY += Main.rand.Next(-50, 51) * 0.1f;
            int num5 = 24;
            float num6 = (float)Math.Sqrt(velocityX * velocityX + velocityY * velocityY);
            num6 = num5 / num6;
            velocityX *= num6;
            velocityY *= num6;
            Projectile p = Projectile.NewProjectileDirect(new Vector2(screenX, screenY), new Vector2(velocityX, velocityY), ModContent.ProjectileType<Projectiles.Apocalypse>(), damage, 0f, player.whoAmI);
            p.tileCollide = false;
            target.AddBuff(BuffID.OnFire, 400);
        }
	}
}
