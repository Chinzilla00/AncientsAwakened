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
            bossBagNPC = mod.NPCType("SerpentHead");
            
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
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.PHMDevArmor();
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
			player.QuickSpawnItem(mod.ItemType("ArcticMedallion"));			
        }
    }
}