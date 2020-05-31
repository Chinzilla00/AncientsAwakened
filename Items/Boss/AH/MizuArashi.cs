using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Boss.AH
{
    public class MizuArashi : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.damage = 110;
            item.noMelee = true;
            item.ranged = true;
            item.width = 52;
            item.height = 20;
            item.useTime = 2;
            item.reuseDelay = 12;
            item.useAnimation = 12;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 4;
            item.value = Item.sellPrice(0, 25, 0, 0);
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 8f;
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

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mizu Arashi");
            Tooltip.SetDefault(@"Has a 1/15 chance to shoot a Mizu spirit
Spirits deal 2x damage, pierce up to 10 enemies and go through tiles
77% not to consume arrows");
        }
		
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .77f;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (Main.rand.NextBool(15))
			{
                Projectile.NewProjectile(position.X, position.Y, speedX , speedY, mod.ProjectileType("Mizu"), damage * 2, knockBack, player.whoAmI);
            }
			else
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
            return false;
        }
    }
}
