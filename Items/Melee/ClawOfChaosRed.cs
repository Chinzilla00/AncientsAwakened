using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class ClawOfChaosRed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconic Baghnakhs");
        }

        public override void SetDefaults()
        {
            item.damage = 95;
            item.melee = true;
            item.width = 28;
            item.height = 22;
            item.useTime = 6;
            item.useAnimation = 6;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = 80000;
            item.rare = 7;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RadiantIncinerite", 20);
            recipe.AddIngredient(mod, "DragonClaw", 5);
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