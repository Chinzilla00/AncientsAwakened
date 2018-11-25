using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Toxithrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Toxithrower");
			Tooltip.SetDefault("Uses gel for ammo");
		}

        public override void SetDefaults()
        {
            item.damage = 70;
            item.ranged = true;
            item.width = 68;
            item.height = 22;
            item.useTime = 3;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 3.25f;
            item.UseSound = SoundID.Item34;
            item.value = 1000000;
            item.rare = 4;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Toxifire"); //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 7.5f;
            item.useAmmo = 23;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HydraToxin", 5);
            recipe.AddIngredient(null, "Abyssium", 10);
            recipe.AddIngredient(null, "SoulOfSpite", 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}