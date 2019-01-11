using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.GripsShen
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class DiscordianShredder : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = -12;
            item.expert = true;
            item.accessory = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discordian Shredder");
            Tooltip.SetDefault(@"For every hit you land on an enemy, 30 true damage (unassigned to any class) is dealt
Attacks inflict Discordian Inferno");
        }
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<AAPlayer>().DiscordShredder = true;
        }
    }
}