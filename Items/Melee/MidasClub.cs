using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class MidasClub : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Midas Club");
			Tooltip.SetDefault("Hit stuff for more cash");
		}
        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 34;
            item.height = 52;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 10000;
            item.rare = 2;
            item.autoReuse = false;
        }
        public override void AddRecipes()
		{
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldBar, 15);
            recipe.AddIngredient(ItemID.FlaskofGold, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumBar, 15);
            recipe.AddIngredient(ItemID.FlaskofGold, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {

            target.AddBuff(BuffID.Midas, 120);
        }
    }
}
