using FortRise;
using HarmonyLib;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  public class MyLevel : IHookable
  {

    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(Level), nameof(Level.Update)),
          postfix: new HarmonyMethod(Update_patch)
      );
    }

    public static void Update_patch(Level __instance) {

      if (Variants.Speedx1.IsActive())
      {
        Engine.TimeRate = 1.1f;
      }
      else if (Variants.Speedx2.IsActive())
      {
        Engine.TimeRate = 1.2f;
      }
      else if (Variants.Speedx3.IsActive())
      {
        Engine.TimeRate = 1.3f;
      }
      else if (Variants.Speedx4.IsActive())
      {
        Engine.TimeRate = 1.4f;
      }
      else if (Variants.Speedx5.IsActive())
      {
        Engine.TimeRate = 1.5f;
      }
    }
  }
}
