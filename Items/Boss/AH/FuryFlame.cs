using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Boss.AH
{
    public class FuryFlame : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fury Flame");
            Tooltip.SetDefault("Allows you to blast explosive flames at your foes");
        }

        public override void SetDefaults()
        {
            item.damage = 140;
            item.noMelee = true;
            item.magic = true;
            item.width = 64;
            item.height = 46;
            item.useTime = 2;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = mod.ProjectileType("FuryFlame");
            item.mana = 4;
            item.knockBack = 0;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shootSpeed = 7f;
            item.noUseGraphic = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, 7f);
            return false;
        }
    }
}
