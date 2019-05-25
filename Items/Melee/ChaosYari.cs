using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria;

namespace AAMod.Items.Melee
{
    public class ChaosYari : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Yari");		
		}

        public override void SetDefaults()
        {
            item.damage = 130;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.maxStack = 1;
            item.useTime = 18;
            item.useAnimation = 18;
            item.knockBack = 4f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.shootSpeed = 11f;
            item.shoot = mod.ProjectileType("ChaosYari");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "AsgardianLance", 1);
            recipe.AddIngredient(ItemID.Gungnir, 1);
            recipe.AddIngredient(mod, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}