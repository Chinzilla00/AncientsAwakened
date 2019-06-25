using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Ryusei : BaseAAItem
    {
        public override void SetDefaults()
        {
			item.CloneDefaults(ItemID.Sunfury);

            item.damage = 48; 
            item.melee = true; 
            item.width = 46; 
            item.height = 66;    
            item.knockBack = 7;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
            item.autoReuse = true;
            item.useTurn = false;
            item.shoot = mod.ProjectileType("Ryusei");
			item.UseSound = SoundID.Item18;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yogan");
			Tooltip.SetDefault(@"Ignites enemies on hit with flames and Dragonfire");
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Yogan"));
            recipe.AddIngredient(mod.ItemType("InfernoCrystal"));
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
