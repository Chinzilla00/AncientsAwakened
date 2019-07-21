using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace AAMod.Items.Melee
{
    public class ChaosYariEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Perfect Chaos Yari");		
		}

        public override void SetDefaults()
        {
            item.damage = 180;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.useTime = 20;
            item.useAnimation = 20;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(5, 0, 0, 0);
            item.rare = 11;
            item.expert = true; item.expertOnly = true;
            item.shootSpeed = 12f;
            item.shoot = mod.ProjectileType("ChaosYariEX");
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ChaosYari", 1);
            recipe.AddIngredient(mod, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}