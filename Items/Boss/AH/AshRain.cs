using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Boss.AH
{
    public class AshRain : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ash Rain");
			Tooltip.SetDefault(@"Shoots fireball which explodes on hit or after some time
Right click to detonate fireballs");
        }

        public override void ModifyTooltips(List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override void SetDefaults()
        {
            item.damage = 315;                        
            item.magic = true;            
            item.width = 24;
            item.height = 28;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;    
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            item.mana = 5;
            item.autoReuse = true;
            item.shootSpeed = 11f;
        }

        
        private readonly List<int> AshRainFire = new List<int>();
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse != 2)
            {
                item.shoot = mod.ProjectileType("FireMagic");
                item.UseSound = SoundID.Item20;
            }
            if (player.altFunctionUse == 2)
            {
                foreach(int P in AshRainFire)
                {
                    if(Main.projectile[P].type == mod.ProjectileType("FireMagic")) Main.projectile[P].Kill();
                }
                item.UseSound = null;
                AshRainFire.Clear();
            }
            return true;
        }
        
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            if (player.altFunctionUse != 2)
			{
				int P = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("FireMagic"), damage, knockBack, player.whoAmI, 0f, 0f);
                AshRainFire.Add(P);
			}
			return false;
		}
    }
}
