using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Melee
{
    public class VikingPolearm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Polearm");		
		}
		
        public override void SetDefaults()
        {
            item.width = 35;
            item.height = 35;
            item.maxStack = 1;
            item.rare = 5;
            item.value = BaseUtility.CalcValue(0, 0, 20, 0);

            item.useStyle = 5;
            item.useAnimation = 55;
            item.useTime = 55;
            item.UseSound = SoundID.Item1;
            item.damage = 29;
            item.knockBack = 5;
            item.melee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = mod.ProjType("VikingPolearm");
            item.shootSpeed = 4;			
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "SnowMana", 8);
            recipe.AddIngredient(ItemID.IceBlock, 40);
            recipe.AddIngredient(ItemID.BorealWood, 12);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}