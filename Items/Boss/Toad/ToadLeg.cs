using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;


namespace AAMod.Items.Boss.Toad
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ToadLeg : ModItem
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
            item.value = Item.sellPrice(0, 12, 0, 0);
            item.rare = 5;
            item.accessory = true;
            item.expertOnly = true;
            item.expert = true;
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