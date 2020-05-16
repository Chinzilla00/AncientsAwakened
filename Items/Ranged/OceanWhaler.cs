using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class OceanWhaler : BaseAAItem
    {

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 25;
            item.useTime = 25;
            item.knockBack = 6f;
            item.width = 30;
            item.height = 10;
            item.damage = 34;
            item.shoot = ModContent.ProjectileType<Projectiles.OceanWhaler>();
            item.shootSpeed = 11f;
            item.UseSound = SoundID.Item10;
            item.rare = ItemRarityID.Green;
            item.value = 27000;
            item.ranged = true;
        }

		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int num17 = 0; num17 < 1000; num17++)
            {
                if (Main.projectile[num17].active && Main.projectile[num17].owner == Main.myPlayer && Main.projectile[num17].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.Harpoon);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
