using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace AAMod.Misc
{
    public static class ModPatcher
    {
        private static MethodInfo calamityAddBosses;

        public static void Patch()
        {
            Mod calamity = ModLoader.GetMod("CalamityMod");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (calamity != null && bossChecklist != null)
            {
                PreparePatchScalBossDifficulty(calamity, out calamityAddBosses);
                if (calamityAddBosses != null)
                {
                    ModifyAddBosses += PatchScalBossDifficulty;
                }
            }
        }

        private static void PreparePatchScalBossDifficulty(Mod calamity, out MethodInfo addBosses)
        {
            addBosses = GetMethodFromMod(calamity, "WeakReferenceSupport", "AddCalamityBosses", BindingFlags.NonPublic | BindingFlags.Static);
        }

        private static MethodInfo GetMethodFromMod(Mod mod, string typeName, string methodName, BindingFlags bindingFlags)
        {
            Assembly assembly = mod.GetType().Assembly;
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Name == typeName)
                {
                    return type.GetMethod(methodName, bindingFlags);
                }
            }
            return null;
        }

        private static event ILContext.Manipulator ModifyAddBosses
        {
            add => HookEndpointManager.Modify(calamityAddBosses, value);
            remove => HookEndpointManager.Unmodify(calamityAddBosses, value);
        }

        private static void PatchScalBossDifficulty(ILContext il)
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchLdstr("Supreme Calamitas"))) throw new ILException(); // Find where the name of scal is pushed onto the stack.

            c.Index += 2; // Advance cursor index by 2 to bring it in line with the load local instruction.
            c.Emit(OpCodes.Pop); // Pop the loaded difficulty value off the stack (19.0f).
            c.Emit(OpCodes.Ldc_R4, 21.0f); // Replace it with 21.0f.
        }
    }
}
