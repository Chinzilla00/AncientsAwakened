using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class Olympia : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 150;
			item.melee = true;
			item.width = 52;
			item.height = 52;
            item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<Projectiles.Athena.Skyrazor>();
            item.shootSpeed = 10;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "SkycutterKopis", 1);
            recipe.AddIngredient(null, "StormSphere", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
