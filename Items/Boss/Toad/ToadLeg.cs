using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

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
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true; item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoJump = true;
            Player.jumpHeight = 20;
            player.jumpSpeedBoost += 1.5f;
            player.noFallDmg = true;
        }
    }
}