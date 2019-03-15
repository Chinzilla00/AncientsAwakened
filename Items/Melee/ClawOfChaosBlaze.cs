using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfChaosBlaze : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Baghnakhs");
            Tooltip.SetDefault("Strike your foes with the force of a draconian strike");
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.melee = true;
            item.width = 28;
            item.height = 22;
            item.useTime = 7;
            item.useAnimation = 6;
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
            recipe.AddIngredient(mod, "ClawOfChaosRed", 1);
            recipe.AddIngredient(mod, "DaybreakIncinerite", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}