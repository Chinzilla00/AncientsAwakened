using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class HeatCleaver : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heat Cleaver");
            Tooltip.SetDefault("Burning with the heat of the Inferno.");
        }
        public override void SetDefaults()
        {
            item.damage = 44;
            item.melee = true;
            item.width = 50;
            item.height = 70;
            item.useTime = 40;
            item.useAnimation = 38;
            item.useStyle = 1;
            item.knockBack = 40;
            item.value = 1000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "FlamingFury", 1);
            recipe.AddIngredient(null, "DragonClaw", 10);
            recipe.AddIngredient(null, "BroodScale", 5);
            recipe.AddTile(null, "HellstoneAnvil");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            int rate = Main.rand.Next(1, 3);
            if (rate == 1)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
    }
}
