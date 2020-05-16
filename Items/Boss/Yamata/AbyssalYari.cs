using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Yamata
{
    public class AbyssalYari : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Yari");
            Tooltip.SetDefault(@"One of two legendary spears used to divide time into day and night");
        }

        public override void SetDefaults()
        {
            item.damage = 350;
            item.melee = true;
            item.width = 132;
            item.height = 132;
            item.scale = 1.1f;
            item.maxStack = 1;
            item.useTime = 25;
            item.useAnimation = 25;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("AbyssalYariP");  
            item.autoReuse = true;
            item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }
    }
}
