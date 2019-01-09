using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class Icepick : ModItem
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
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
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
