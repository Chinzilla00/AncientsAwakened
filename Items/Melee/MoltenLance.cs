using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class MoltenLance : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 30;
            item.melee = true;
            item.width = 112;
            item.height = 112;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 21;
            item.useAnimation = 21;
            item.knockBack = 4.4f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useTurn = true;
			item.autoReuse = false;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 2, 40, 0);
            item.rare = 3;
            item.shoot = mod.ProjectileType("MLP");  //put your Spear projectile name
            item.shootSpeed = 5f;
        }
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Lance");
            Tooltip.SetDefault("Makes instant barbeque shishkebabs!");
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);   //you need 1 DirtBlock
            recipe.AddTile(TileID.Anvils);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
