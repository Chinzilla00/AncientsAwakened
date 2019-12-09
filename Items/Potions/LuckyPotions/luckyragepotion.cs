using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Potions.LuckyPotions
{
    public class luckyragepotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lucky Rage Potion");
			Tooltip.SetDefault("Increases critical chance by 11%");
		}
		
		public override void SetDefaults()
		{
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.maxStack = 30;
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = 7;
			item.buffType = mod.BuffType("luckyrage");
			item.buffTime = 18000;
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
	}

	public class luckyrage : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "Terraria/Buff_115";
			return mod.Properties.Autoload;
		}

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Rage");
			Description.SetDefault("11% increased critical chance");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[115] = true;
			player.meleeCrit += 11;
			player.rangedCrit += 11;
			player.magicCrit += 11;
			player.thrownCrit += 11;
		}
	}
}
