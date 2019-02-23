using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class IchorSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Spear");
            Tooltip.SetDefault("Inflicts Ichor to enemy on hit");
        }

        public override void SetDefaults()
        {
            item.damage = 48;
            item.melee = true;
            item.width = 80;
            item.height = 80;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 24;
            item.useAnimation = 18;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
			item.autoReuse = true;
            item.rare = 3;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("IchorSpear");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 12);
			recipe.AddIngredient(ItemID.Ichor, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
