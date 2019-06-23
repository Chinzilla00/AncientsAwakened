using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class Crystal : BaseAAItem
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
            
            
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 10000;
            item.rare = 8;
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
            recipe.AddTile(null, "TerraPrism");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class CrimsonCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Vertebrae, 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Ichor, 3);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SoulofNight, 2);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class CorruptionCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.RottenChunk, 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CursedFlame, 3);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SoulofNight, 2);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class DungeonCrystal : BaseAAItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Prism");
            Tooltip.SetDefault("Imbued with the ghastly spirits of the ancient crypt");
        }

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
                recipe.AddIngredient(ItemID.Bone, 15);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Ectoplasm, 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class HallowCrystal : BaseAAItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallow Prism");
            Tooltip.SetDefault("Imbued with the holy light of the blessed plains");
        }

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
            return AAColor.Hallow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Hallow.ToVector3() * 0.55f * Main.essScale);
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SoulofLight, 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.CrystalShard, 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class HellCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            recipe.AddIngredient(null, "PureEvil", 3);
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

    public class OceanCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.Coral, 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.SharkFin, 4);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class IceCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "SnowMana", 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.FrostCore, 1);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class DesertCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 1);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DesertMana", 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class JungleCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "PlanteraPetal", 3);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class InfernoCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "SoulOfSmite", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DragonFire", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DragonScale", 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class MireCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "SoulOfSpite", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "HydraToxin", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "MirePod", 10);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class TerraCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TerraShard", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DragonSpirit", 5);
                recipe.AddIngredient(null, "Crystal");
                recipe.AddTile(null, "TerraPrism");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }

    public class ChaosCrystal : BaseAAItem
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

    public class VoidCrystal : BaseAAItem
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
            item.width = 16;
            item.height = 16;
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
            recipe.AddIngredient(null, "VoidEnergy");
            recipe.AddIngredient(null, "Crystal");
            recipe.AddTile(null, "TerraPrism");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}