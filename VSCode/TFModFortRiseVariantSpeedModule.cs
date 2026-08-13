using System;
using System.Diagnostics;
using FortRise;
using Microsoft.Extensions.Logging;
using Monocle;
using TowerFall;

namespace TFModFortRiseVariantSpeed
{
  public class TFModFortRiseVariantSpeedModule : Mod
  {
    public static TFModFortRiseVariantSpeedModule Instance;

    private static Type[] Registerables = [
        typeof(TextureRegistry),
        typeof(Variants)

     ];
    internal Type[] Hookables = [
        typeof(MyPlayer),
        typeof(MyVariantToggle),
    ];
    //public static Atlas SpeedAtlas;

    public TFModFortRiseVariantSpeedModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      TFModFortRiseVariantSpeed.Logger.Init(logger);

      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }

      foreach (var registerable in Registerables)
      {
        registerable.GetMethod(nameof(IRegisterable.Register))!.Invoke(null, [content, context.Registry]);
      }
    }

    public static TFModFortRiseVariantSpeedSettings Settings
        => Instance.GetSettings<TFModFortRiseVariantSpeedSettings>()!;

    public override ModuleSettings CreateSettings()
    {
      return new TFModFortRiseVariantSpeedSettings();
    }

    /// <summary>
    /// Ecrit les reglages sur le disque tout de suite.
    ///
    /// FortRise ne les enregistre qu'en quittant SES options (MainMenu.DestroyOptions)
    /// ou lors d'une sauvegarde de partie. Une vitesse changee depuis la fenetre de
    /// l'ecran des variantes resterait donc en memoire et serait perdue en quittant
    /// le jeu. SaveSettings est internal cote FortRise, d'ou la reflexion.
    /// </summary>
    public static void SaveSettingsNow()
    {
      if (Instance == null)
      {
        return;
      }

      try
      {
        var method = typeof(Mod).GetMethod("SaveSettings",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method?.Invoke(Instance, null);
      }
      catch (Exception ex)
      {
        TFModFortRiseVariantSpeed.Logger.Info($"[Settings] sauvegarde immediate impossible : {ex.Message}");
      }
    }
  }
}
