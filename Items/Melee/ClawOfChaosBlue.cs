using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfChaosBlue : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Baghnakhs");
            Tooltip.SetDefault("Strike your foes with the speed of a shadowy slash");
        }

		public override void SetDefaults()
		{
			item.damage = 65;
			item.melee = true;
			item.width = 28;
			item.height = 22;
			item.useTime = 7;
			item.useAnimation = 5;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 80000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "DeepAbyssium", 20);
            recipe.AddIngredient(mod, "HydraGlove", 1);
            recipe.AddIngredient(ItemID.FetidBaghnakhs, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 600);
        }
	}
}
