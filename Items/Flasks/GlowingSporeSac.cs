using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Flasks
{
    public class GlowingSporeSac : ModItem
	{
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.maxStack = 999;
            item.consumable = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.shoot = ProjectileID.MushroomSpray;
            item.shootSpeed = 1f;
            item.useStyle = 1;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glowing Spore Sack");
            Tooltip.SetDefault(@"Spreads Glowing Mushrooms");
        }
    }
}
