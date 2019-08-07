using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class TechneciumRepeater : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Technecium Repeater");
			Tooltip.SetDefault("33% chance not to consume arrows");
        }

		public override void SetDefaults()
        {
            item.damage = 110;
            item.crit += 25;
            item.ranged = true;
            item.width = 50;
            item.height = 34;
            item.useTime = 3;
            item.reuseDelay = 15;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = 350000;
            item.rare = 9;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = 40;
        }
		
		public override bool ConsumeAmmo(Player player)
		{
		    return Main.rand.NextFloat() >= .33;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 0);
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "TechneciumBar", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
