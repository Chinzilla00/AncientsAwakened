using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss
{
    public class MadnessTruffle : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Truffle");
            Tooltip.SetDefault(@"Increased jump speed and allows auto-jump
You are immune to fall damage
Increased jump height
+50 Max Mana
+50 Max Life
You know what? Just don't put it anywhere near your mouth.");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 8;
            item.accessory = true;
            item.expert = true;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            Player.jumpHeight = 25;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.statManaMax2 += 50;
            player.statLifeMax2 += 50;
        }
    }
}