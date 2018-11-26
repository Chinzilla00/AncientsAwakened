using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AncientsAwakened.Items.Boss.Yamata
{
    public class EternalTwilight : ModItem 
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eternal Twilight");
            Tooltip.SetDefault(""); //TODO: Come up with a good tooltip
        }

        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 76;
            item.damage = 180;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 15f;
            item.knockBack = 5;
            item.ranged = true;
            item.noMelee = true;
        }
    }
}
