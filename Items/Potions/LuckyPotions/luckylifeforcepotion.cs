using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Potions.LuckyPotions
{
    public class luckylifeforcepotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lucky Lifeforce Potion");
			Tooltip.SetDefault("Increases max life by 21%");
		}
		
		public override void SetDefaults()
		{
            item.UseSound = SoundID.Item3;
            item.useStyle = ItemUseStyleID.EatingUsing;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.maxStack = 30;
			item.consumable = true;
			item.width = 16;
			item.height = 16;
			item.value = Item.sellPrice(0, 10, 0, 0);
			item.rare = ItemRarityID.Lime;
			item.buffType = mod.BuffType("luckylifeforce");
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

	public class luckylifeforce : ModBuff
	{
		public override bool Autoload(ref string name, ref string texture)
		{
			texture = "Terraria/Buff_113";
			return mod.Properties.Autoload;
		}

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Lifeforce");
			Description.SetDefault("21% increased max life");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[113] = true;
			player.lifeForce = true;
			player.statLifeMax2 += (int)(player.statLifeMax * .21f);
		}
	}
}
