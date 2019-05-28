using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.DevTools
{
    public class Anticheat : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("[DEV] Anticheat Disabler");
            BaseMod.BaseUtility.AddTooltips(item, new string[] { "For testers or pussies." });
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.value = BaseUtility.CalcValue(0, 0, 0, 0);

            item.useStyle = 1;
            item.useAnimation = 45;
            item.useTime = 45;
            item.autoReuse = false;
            item.consumable = false;
        }

        public int runOnce = 0;

        public override bool UseItem(Player player)
        {
            if (AAWorld.Anticheat == false && runOnce == 0)
            {
                Main.NewText("Anticheat Protocol enabled", Color.Cyan);
                AAWorld.Anticheat = true;
                runOnce += 1;
            }
            if (AAWorld.Anticheat == true && runOnce == 0)
            {
                runOnce += 1;
                Main.NewText("Anticheat Protocol disabled", Color.Red);
                AAWorld.Anticheat = false;
            }

            runOnce = 0;

            return true;
        }
    }
}