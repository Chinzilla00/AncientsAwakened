using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.AH
{
    public class HarukaBox : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 16;
            item.height = 16;
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka's Lockbox");
            Tooltip.SetDefault(@"Right click to open
Contains a set of Midnight Assassin clothes");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            item.TurnToAir();
            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AssassinHood"), 1, false, 0, false, false);
            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AssassinShirt"), 1, false, 0, false, false);
            Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("AssassinBoots"), 1, false, 0, false, false);
        }
    }
}
