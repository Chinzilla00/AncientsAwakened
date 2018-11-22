using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Discord : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Discord");
			Tooltip.SetDefault("A slightly chaotic sword");
		}
		public override void SetDefaults()
		{
			item.damage = 40;
			item.melee = true;
            item.width = 48;
			item.height = 48;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 20000;
			item.rare = 3;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shootSpeed = 15f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod, "IncineriteBar", 20);
			recipe.AddIngredient(mod, "AbyssiumBar", 20);
            recipe.AddIngredient(mod, "FlamingFury", 1);
            recipe.AddIngredient(mod, "ExilesKatana", 1);
            recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		 public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
			target.AddBuff(BuffID.Poisoned, 200);
        }
	}
}
