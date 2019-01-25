using Terraria;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
    public class SoCCache : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Treasure Cache");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 36;
			item.height = 32;
			item.expert = true;
			bossBagNPC = mod.NPCType("SoC");
		}
        
        public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
            if (Main.rand.NextFloat() < 0.01f)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(mod.ItemType("EXSoul"));
            string[] lootTable = 
            {
                "CthulhuCannon"
            };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
	}
}