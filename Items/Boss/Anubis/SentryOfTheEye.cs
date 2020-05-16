using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Anubis
{
    public class SentryOfTheEye : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sentry of the Eye");
            Tooltip.SetDefault(@"Summons an eye sentry");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("TheEye");
            item.damage = 65;
            item.width = 50;
            item.height = 50;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 0, 27, 0);
            item.knockBack = 7.5f;
            item.rare = ItemRarityID.LightPurple;
            item.summon = true;
            item.mana = 30;
            item.sentry = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            int num74 = item.shoot;
            int num76 = item.damage;
            float num77 = item.knockBack;
            int num155 = (int)(Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }
			if (player.ownedProjectileCounts[num74] < player.maxTurrets)
			{
				Projectile.NewProjectile(Main.mouseX + Main.screenPosition.X, Main.mouseY + Main.screenPosition.Y, 0f, 0f, num74, num76, num77, i, 0f, 0f);
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
				 Projectile.NewProjectile(Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 0f, num74, num76, num77, i, 0f, 0f);
			}

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<Djinn.SandScepter>(), 1);
            recipe.AddIngredient(null, "ForsakenFragment", 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}