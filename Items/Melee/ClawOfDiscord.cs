using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfDiscord : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Discordian Baghnakhs");
            Tooltip.SetDefault("Strike your foes with the unyielding force of chaos itself");
        }

		public override void SetDefaults()
		{
			item.damage = 110;
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
            recipe.AddIngredient(mod, "ClawOfChaosBlaze", 1);
            recipe.AddIngredient(mod, "ClawOfChaosAbyss", 1);
            recipe.AddIngredient(mod, "CrucibleScale", 3);
            recipe.AddIngredient(mod, "DreadScale", 3);
            recipe.AddIngredient(mod, "Discordium", 5);
            recipe.AddTile(null, "AncientForge");
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.DiscordInferno>(), 600);
        }
	}
}
