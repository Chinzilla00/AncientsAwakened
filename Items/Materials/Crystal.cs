using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class CrimsonCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Prism");
            Tooltip.SetDefault("Imbued with the carnal energy of the flesh-ridden wasteland");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class CorruptionCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corruption Prism");
            Tooltip.SetDefault("Imbued with the shadowy essence of the decaying woodlands");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class DungeonCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dungeon Prism");
            Tooltip.SetDefault("Imbued with the ghastly spirits of the ancient crypt");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class HallowCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallow Prism");
            Tooltip.SetDefault("Imbued with the holy light of the blessed plains");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
            return AAColor.Hallow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Hallow.ToVector3() * 0.55f * Main.essScale);
        }
    }

    public class HellCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Prism");
            Tooltip.SetDefault("Imbued with the sinful influence of the unholy caverns");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    /*public class SkyCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sky Prism");
            Tooltip.SetDefault("Imbued with the celestial wonder of the expansive ozone");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }*/

    public class OceanCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Prism");
            Tooltip.SetDefault("Imbued with the calming sounds of the seven seas");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class IceCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Prism");
            Tooltip.SetDefault("Imbued with the chilling winds of the frozen mountains");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class DesertCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert Prism");
            Tooltip.SetDefault("Imbued with the heated rays of the sandy wastes");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class JungleCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Prism");
            Tooltip.SetDefault("Imbued with the rythmic beat of the tribal rainforest");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class InfernoCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Prism");
            Tooltip.SetDefault("Imbued with the blazing fury of the fire-ravaged mountains");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 6));
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
    }

    public class MireCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Prism");
            Tooltip.SetDefault("Imbued with the abyssal wrath of the dark bogs");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class TerraCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Prism");
            Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class ChaosCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Prism");
            Tooltip.SetDefault("Imbued with the discordian flames of chaos");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class VoidCrystal : ModItem
    {
        public override string Texture { get { return "AAMod/Items/Materials/Crystal"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Prism");
            Tooltip.SetDefault("Imbued with the echoes of unyielding malice");
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6,6));
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
    }

    public class CrystalDrop : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (NPC.downedPlantBoss)
            {
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<MireCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneInferno)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<InfernoCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneVoid)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<VoidCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        //Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<SnowCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDesert || Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUndergroundDesert)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<DesertCrystal>(), 1);
                    }
                }
                if (BaseExtensions.InZone(Main.player[Player.FindClosest(npc.position, npc.width, npc.height)], "Ocean", null))
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<OceanCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneHoly)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<HallowCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<DungeonCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneJungle)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<JungleCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<HellCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).Terrarium)
                {
                    if (Main.rand.Next(0, 100) >= 99)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<TerraCrystal>(), 1);
                    }
                }
                if (Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneInferno || Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneMire)
                {
                    if (Main.rand.Next(0, 100) >= 99.5f)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType<ChaosCrystal>(), 1);
                    }
                }
            }
        }
    }
}