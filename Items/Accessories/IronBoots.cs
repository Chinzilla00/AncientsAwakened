using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class IronBoots : BaseAAItem
    {
            
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Boots");
            Tooltip.SetDefault(@"Allows flight
The wearer can run incredibly fast
You are immune to fall damage");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 32;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 2;
            item.accessory = true;
            item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.moveSpeed += 5f;
            player.noFallDmg = true;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                for (int i = 0; i < 16; i++)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Vector2.UnitY.RotatedBy(i * .392699093f) * new Vector2(1f, 4f);
                    vector = vector.RotatedBy(player.velocity.ToRotation());
                    int dust = Dust.NewDust(player.Center, 0, 0, DustID.Electric, 0f, 0f, 0);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].position = new Vector2(player.position.X + 10, player.position.Y + 3f + 30);
                    Main.dust[dust].velocity = player.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                }
            }
            base.WingUpdate(player, inUse);
            return false;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].shoeSlot > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}