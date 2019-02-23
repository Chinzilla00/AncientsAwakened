using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfChaosAbyss : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dread Baghnakhs");
            Tooltip.SetDefault("Strike your foes with the speed of an abyssal slash");
        }

		public override void SetDefaults()
		{
			item.damage = 90;
			item.melee = true;
			item.width = 28;
			item.height = 22;
			item.useTime = 5;
			item.useAnimation = 5;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = 200000;
			item.rare = 11;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "ClawOfChaosBlue", 1);
            recipe.AddIngredient(mod, "EventideAbyssium", 10);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.Moonraze>(), 600);
        }
	}
}
