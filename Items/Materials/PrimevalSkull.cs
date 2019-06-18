using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class PrimevalSkull : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Primeval Skull");
            Tooltip.SetDefault("Energy from an age since passed radiates from this ancient fossil");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 8));
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = 7;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Desert.ToVector3() * 0.55f * Main.essScale);
        }
    }
}