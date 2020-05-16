using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Dev.Invoker
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class InvokerBook : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Aleister Book");
            Tooltip.SetDefault(@"A Legendary Book of the Mega Therion.
10% increased minion damage
+2 minion slots
Maybe you could make it stronger..?
There's a note written on the cover: 
I need more powerful souls, *****,*********,**********");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.rare = ItemRarityID.Purple;
            item.accessory = true;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 1;
            item.expertOnly = true;
            item.useTime = 30;
            item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
		{
            return false;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += .1f;
            player.maxMinions += 2;

            InvokerPlayer InvokerPlayer = InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
        }
    }
}