using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Mossket : ModItem
    {

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.width = 24;
            item.height = 28;
            item.shoot = 10;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item11;
            item.damage = 15;
            item.shootSpeed = 12f;
            item.noMelee = true;
            item.knockBack = .5f;
            item.value = 50000;
            item.scale = 1f;
            item.rare = 1;
            item.ranged = true;
        }
    }
}
