using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ScourgeOfShadows : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scourge of the Shadows");
            Tooltip.SetDefault("Bounce off tiles up to 3 times\nBreaks after hitting an enemy\nSprays little eaters while travelling and on enemy hit\nScourge of the Corruptor EX");
        }

        public override void SetDefaults()
		{
            item.autoReuse = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.shootSpeed = 14f;
			item.shoot = mod.ProjectileType("ScourgeOfShadowsP");
			item.damage = 130;
			item.width = 18;
			item.height = 20;
			item.UseSound = SoundID.Item39;
			item.useAnimation = 10;
			item.useTime = 10;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 50, 0, 0);
			item.knockBack = 5f;
			item.melee = true;
			item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ScourgeoftheCorruptor);
            recipe.AddIngredient(null, "EXSoul");
		    recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
    }
}
