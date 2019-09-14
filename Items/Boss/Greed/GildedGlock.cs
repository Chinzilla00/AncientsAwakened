using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss.Greed
{
    public class GildedGlock : BaseAAItem
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Gilded Glock");
            Tooltip.SetDefault("Uses Coins as Ammo");
            item.width = 44;
            item.height = 30;
            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 28;
            item.useTime = 28;
            item.UseSound = SoundID.Item41;
            item.damage = 70;
            item.knockBack = 7;
            item.ranged = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = 158;
            item.shootSpeed = 12;		
        }

        public override bool CanUseItem(Player player)
        {
            int itemIndex = -1;
            if (HasCoin(player, 71, ref itemIndex)) //copper coins
            {
                if (ConsumeAmmo(player)) { BasePlayer.ReduceSlot(player, itemIndex, 1); }
                return true;
            }
            if (HasCoin(player, 72, ref itemIndex)) //silver coins
            {
                if (!BasePlayer.HasEmptySlots(player, 1, true, true, false)) { return false; }
                if (BasePlayer.DowngradeMoney(player, itemIndex, ref itemIndex))
                {
                    if (ConsumeAmmo(player)) { BasePlayer.ReduceSlot(player, itemIndex, 1); }
                    return true;
                }
                return true;
            }
            if (HasCoin(player, 73, ref itemIndex)) //gold coins
            {
                if (!BasePlayer.HasEmptySlots(player, 1, true, true, false)) { return false; }
                if (BasePlayer.DowngradeMoney(player, itemIndex, ref itemIndex))
                {
                    if (ConsumeAmmo(player)) { BasePlayer.ReduceSlot(player, itemIndex, 1); }
                    return true;
                }
                return true;
            }
            else
            if (HasCoin(player, 74, ref itemIndex)) //plat coins
            {
                if (!BasePlayer.HasEmptySlots(player, 1, true, true, false)) { return false; }
                if (BasePlayer.DowngradeMoney(player, itemIndex, ref itemIndex))
                {
                    if (ConsumeAmmo(player)) { BasePlayer.ReduceSlot(player, itemIndex, 1); }
                    return true;
                }
            }
            return false;
        }

        public bool HasCoin(Player player, int type, ref int itemIndex)
		{
			return BasePlayer.HasItem(player, type, ref itemIndex, 1, true, true);
		}
	}
}