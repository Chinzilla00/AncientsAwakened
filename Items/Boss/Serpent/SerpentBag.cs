using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class SerpentBag : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            bossBagNPC = mod.NPCType("Serpent");
            
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                //player.QuickSpawnItem(mod.ItemType("ZeroMask"));
            }
            if (Main.rand.NextFloat() < 0.01f)
            {
                int choice = Main.rand.Next(17);
                {
                    if (choice == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("HalHat"));
                        player.QuickSpawnItem(mod.ItemType("HalTux"));
                        player.QuickSpawnItem(mod.ItemType("HalTrousers"));
                    }
                    else if (choice == 1)
                    {
                        player.QuickSpawnItem(mod.ItemType("FishDiverMask"));
                        player.QuickSpawnItem(mod.ItemType("FishDiverJacket"));
                        player.QuickSpawnItem(mod.ItemType("FishDiverBoots"));
                    }
                    else if (choice == 2)
                    {
                        player.QuickSpawnItem(mod.ItemType("N1"));
                    }
                    if (choice == 3)
                    {
                        player.QuickSpawnItem(mod.ItemType("GlitchesHat"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesBreastplate"));
                        player.QuickSpawnItem(mod.ItemType("GlitchesGreaves"));
                    }
                    if (choice == 4)
                    {
                        player.QuickSpawnItem(mod.ItemType("GavransGoggles"));
                        player.QuickSpawnItem(mod.ItemType("GavransChest"));
                        player.QuickSpawnItem(mod.ItemType("GavransChest"));
                    }
                    if (choice == 5)
                    {
                        player.QuickSpawnItem(mod.ItemType("ChinMask"));
                        player.QuickSpawnItem(mod.ItemType("ChinSuit"));
                        player.QuickSpawnItem(mod.ItemType("ChinPants"));
                    }
                    if (choice == 6)
                    {
                        player.QuickSpawnItem(mod.ItemType("SkrallStaff"));
                    }
                    if (choice == 7)
                    {
                        player.QuickSpawnItem(mod.ItemType("CharlieShell"));
                    }
                    if (choice == 8)
                    {
                        player.QuickSpawnItem(mod.ItemType("TimeTeller"));
                    }
                    if (choice == 9)
                    {
                        player.QuickSpawnItem(mod.ItemType("TitanAxe"));
                    }
                    if (choice == 10)
                    {
                        player.QuickSpawnItem(mod.ItemType("EnderStaff"));
                    }
                    if (choice == 11)
                    {
                        player.QuickSpawnItem(mod.ItemType("CatsEyeRifle"));
                    }
                    if (choice == 12)
                    {
                        player.QuickSpawnItem(mod.ItemType("DuckstepGun"));
                    }
                    if (choice == 13)
                    {
                        player.QuickSpawnItem(mod.ItemType("TiedHat"));
                        player.QuickSpawnItem(mod.ItemType("TiedHalTux"));
                        player.QuickSpawnItem(mod.ItemType("TiedTrousers"));
                    }
                    if (choice == 14)
                    {
                        player.QuickSpawnItem(mod.ItemType("MoonHood"));
                        player.QuickSpawnItem(mod.ItemType("MoonRobe"));
                        player.QuickSpawnItem(mod.ItemType("MoonBoots"));
                    }
                }
            }
            player.QuickSpawnItem(mod.ItemType("SnowMana"), Main.rand.Next(15, 20));
            string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("SnowflakeShuriken"), Main.rand.Next(100, 130));
            }
            else
            {
                player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            }
        }
    }
}