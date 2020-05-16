using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class Icepick : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icepick");
        }

        public override void SetDefaults()
        {

            item.damage = 10;
            item.melee = true;
            item.width = 46;
            item.height = 42;
            item.useTime = 13;
            item.useAnimation = 20;
            item.pick = 105;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 120);
        }
    }
}
