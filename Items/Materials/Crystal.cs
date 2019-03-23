using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Crystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Biome Prism");
            Tooltip.SetDefault("A magical prism that can be enhanced with the power of a biome.");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Crimson;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Crimson.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Prism", 5);
            recipe.AddTile(null, "TerraPrism");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class CrimsonCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Prism");
            Tooltip.SetDefault("Imbued with the carnal energy of the flesh-ridden wasteland");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Crimson;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Crimson.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.FleshGrass);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class CorruptionCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Prism");
            Tooltip.SetDefault("Imbued with the shadowy essence of the decaying woodlands");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Corruption;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Corruption.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.CorruptGrass);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class DungeonCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Prism");
            Tooltip.SetDefault("Imbued with the ghastly spirits of the ancient crypt");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Dungeon;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Dungeon.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.AddTile(TileID.BlueDungeonBrick);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.AddTile(TileID.PinkDungeonBrick);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.AddTile(TileID.GreenDungeonBrick);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class HallowCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallow Prism");
            Tooltip.SetDefault("Imbued with the holy light of the blessed plains");
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Hallow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Hallow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.HallowedGrass);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class HellCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Prism");
            Tooltip.SetDefault("Imbued with the sinful influence of the unholy caverns");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Hell;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Hell.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.Ash);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    /*public class SkyCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Prism");
            Tooltip.SetDefault("Imbued with the celestial wonder of the expansive ozone");
            
           
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("AAMod/Items/Materials/Crystal");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            spriteBatch.Draw(texture, position, null, AAColor.Sky, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("AAMod/Items/Materials/Crystal");
            spriteBatch.Draw(texture, position, null, AAColor.Sky, 0, origin, scale, SpriteEffects.None, 0f);
            return false;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Sky.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.Cloud);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }*/

    public class OceanCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Prism");
            Tooltip.SetDefault("Imbued with the calming sounds of the seven seas");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Ocean;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Ocean.ToVector3() * 0.55f * Main.essScale);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.Sand);
            recipe.needWater = true;
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class IceCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Prism");
            Tooltip.SetDefault("Imbued with the chilling winds of the frozen mountains");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Snow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Snow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.SnowBlock);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class DesertCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Prism");
            Tooltip.SetDefault("Imbued with the heated rays of the sandy wastes");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Desert;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Desert.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.Sand);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class JungleCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Prism");
            Tooltip.SetDefault("Imbued with the rythmic beat of the tribal rainforest");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Jungle;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Jungle.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.JungleGrass);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class InfernoCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Prism");
            Tooltip.SetDefault("Imbued with the blazing fury of the fire-ravaged mountains");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Inferno;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Inferno.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(null, "InfernoGrass");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class MireCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Prism");
            Tooltip.SetDefault("Imbued with the abyssal wrath of the dark bogs");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Mire;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Mire.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(null, "MireGrass");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class TerraCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Prism");
            Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.TerraGlow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.TerraGlow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(TileID.Grass);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class ChaosCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Prism");
            Tooltip.SetDefault("Imbued with the discordian flames of chaos");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
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
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MireCrystal");
            recipe.AddIngredient(null, "InfernoCrystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class VoidCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Prism");
            Tooltip.SetDefault("Imbued with the echoes of unyielding malice");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Oblivion.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.AddTile(null, "Doomstone");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}