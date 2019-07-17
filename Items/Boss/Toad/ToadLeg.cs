using Terraria.ModLoader;
using Terraria;


namespace AAMod.Items.Boss.Toad
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ToadLeg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Legs");
            Tooltip.SetDefault(@"Increased jump speed and allows auto-jump
You are immune to fall damage
Increased jump height");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 5;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            Player.jumpHeight = 25;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
        }
    }
}