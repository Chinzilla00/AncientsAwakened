using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Materials
{
    public class Crystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Biome Prism");
            Tooltip.SetDefault("A magical prism that can be enhanced with the power of a biome.");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.COLOR_WHITEFADE1;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.COLOR_WHITEFADE1.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Prism", 5);
            recipe.AddTile(null, "HallowedForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class TerraCrystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Prism");
            Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = ItemRarityID.Yellow;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.TerraGlow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.TerraGlow.ToVector3() * 0.55f * Main.essScale);
        }
    }

    public class ChaosCrystal : BaseAAItem
    {
        public override string Texture => "AAMod/Items/Materials/Crystal";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Prism");
            Tooltip.SetDefault("Imbued with the discordian flames of chaos");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Shen3;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Shen3.ToVector3() * 0.55f * Main.essScale);
        }
    }
}