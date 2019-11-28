using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Events;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class OldOneCharm : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.value = Item.sellPrice(1, 0, 0, 0);
			item.rare = 8;
			item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.minionDamage += .12f;
            player.maxTurrets ++;
            if(DD2Event.Ongoing) player.minionDamage += .1f;
            player.GetModPlayer<AAPlayer>().OldOneCharm = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Old One Charm");
			Tooltip.SetDefault(@"Pressing the accessory ability hotkey helps player skip the time between old one army two waves.
Increase 12% minion damage
Increases your max number of sentries
While Old One's Army is on, increase 22% minion damage.");
			
		}
	}
}
