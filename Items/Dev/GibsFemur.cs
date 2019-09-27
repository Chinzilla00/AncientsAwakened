using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class GibsFemur : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angry Femur");
            Tooltip.SetDefault("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        public override void SetDefaults()
        {
			item.useTime = 25;
            item.CloneDefaults(ItemID.LightDisc);
            item.melee = true;
            item.maxStack = 3;
            item.damage = 120;                            
            item.value = 6;
            item.rare = 11;
            item.knockBack = 5;
            item.useStyle = 1;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shoot = mod.ProjectileType("GibsFemur");
			item.width = 32;
            item.height = 32;
            item.noMelee = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(255, 128, 0);
                }
            }
        }

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == mod.ProjectileType("GibsFemur"))
                {
                    num++;
                }
            }
            if (num >= item.stack)
            {
                return false;
            }
            return true;
        }
    }
}
