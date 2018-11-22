using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Depthwalker : ModItem
    {
        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.CorruptYoyo);

            item.damage = 14;                            
            item.value = 1000000;
            item.rare = 2;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shoot = mod.ProjectileType("Depthwalker");  
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 200);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depthwalker");
            Tooltip.SetDefault("Walk the Hydra");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
