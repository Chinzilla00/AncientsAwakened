using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class GrassSpear : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;
            item.melee = true;
            item.width = 132;
            item.height = 132;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 4.7f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 2, 40, 0);
            item.rare = 3;
            item.shoot = mod.ProjectileType("GSP");  //put your Spear projectile name
            item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Grass Spear");
      Tooltip.SetDefault("");
    }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Everleaf", 10);   //you need 1 DirtBlock
            recipe.AddIngredient(ItemID.Stinger, 4);
            recipe.AddIngredient(ItemID.JungleSpores, 4);
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
