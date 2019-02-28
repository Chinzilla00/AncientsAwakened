using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning
{
    public class HydraStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Staff");
            Tooltip.SetDefault(@"Summons a stone hydra to fight for you");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("StoneHydra");
            item.damage = 15;
            item.width = 50;
            item.height = 50;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 0, 27, 0);
            item.knockBack = 7.5f;
            item.rare = 1;
            item.summon = true;
            item.mana = 15;
            item.sentry = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            int num74 = item.shoot;
            int num76 = item.damage;
            float num77 = item.knockBack;
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
			if (player.ownedProjectileCounts[num74] < player.maxTurrets)
			{
				Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, num74, num76, num77, i, 0f, 0f);
			}
			if (player.ownedProjectileCounts[num74] == player.maxTurrets)
			{
				for(int g = 0; g < 1000; ++g)
				{
					if(Main.projectile[g].active && Main.projectile[g].type == num74)
					{
						Main.projectile[g].Kill();
						break;
					}
				}
				 Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, num74, num76, num77, i, 0f, 0f);
			}

            return false;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}