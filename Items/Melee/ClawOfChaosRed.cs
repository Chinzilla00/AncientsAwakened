using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfChaosRed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infernal Baghnakhs");
            Tooltip.SetDefault("Strike your foes with the power of an infernal strike");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 28;
            item.height = 22;
            item.useTime = 7;
            item.useAnimation = 7;
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
            recipe.AddIngredient(mod, "RadiantIncinerite", 20);
            recipe.AddIngredient(mod, "DragonGlove", 1);
            recipe.AddIngredient(ItemID.FetidBaghnakhs, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}