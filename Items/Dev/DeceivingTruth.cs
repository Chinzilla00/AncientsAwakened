using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class DeceivingTruth : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Deceiving Truth");
            Tooltip.SetDefault(@"Summons an infinitely piercing vine");
            Item.staff[item.type] = true;
        }

		public override void SetDefaults()
		{
            item.mana = 10;
            item.damage = 80;
            item.useStyle = 5;
            item.shootSpeed = 32f;
            item.shoot = mod.ProjectileType<Projectiles.Darkpuppey.DeceivingTruth>();
            item.width = 26;
            item.height = 28;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Vine");
            item.useAnimation = 23;
            item.useTime = 23;
            item.autoReuse = true;
            item.rare = 11;
            item.noMelee = true;
            item.knockBack = 1f;
            item.value = 2000000;
            item.magic = true;
        }
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 246, 124);
                }
            }
        }
    }
}