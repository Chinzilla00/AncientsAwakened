using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Sagittarius
{
    public class ZeroStar : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Stalion's Star");
            Tooltip.SetDefault("A spinning blade of doom");
        }
		public override void SetDefaults()
		{
	        item.damage = 25;
	        item.width = 46;
	        item.height = 46;
	        item.useTime = 30;
	        item.useAnimation = 30;
	        item.useStyle = ItemUseStyleID.SwingThrow;
	        item.knockBack = 6;
	        item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = SoundID.Item1;
	        item.autoReuse = true;
            item.melee = true;
            item.shoot = mod.ProjectileType("ZeroStarP");
            item.shootSpeed = 10f;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.rare = ItemRarityID.LightRed;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Doomite", 25);
                recipe.AddIngredient(null, "DoomiteScrap", 15);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}