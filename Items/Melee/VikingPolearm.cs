using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria;

namespace AAMod.Items.Melee
{
    public class VikingPolearm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Polearm");		
		}

        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.maxStack = 1;
            item.useTime = 24;
            item.useAnimation = 24;
            item.knockBack = 2.3f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.useStyle = 5;
            item.value = Item.sellPrice(0, 0, 40, 0);
            item.rare = 2;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("VikingPolearm");  //put your Spear projectile name
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
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