namespace ItemRandomizerPlugin {

    using Exiled.API.Features;
    using System;
    using Player = Exiled.Events.Handlers.Player;

    public class ItemRandomizer : Plugin<Config> {
        public PlayerHandler _playerHandler;

        public override string Name => "ItemRandomizerPlugin";
        public override string Prefix => "IRndPl";
        public override string Author => "Megador";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 9, 11);

        public override void OnEnabled() {
            Log.Info("ItemRandomizerPlugin loaded successfully");
            _playerHandler = new PlayerHandler();
            Player.DroppingItem += _playerHandler.OnItemDropped;
            base.OnEnabled();
        }

        public override void OnDisabled() {
            Player.DroppingItem -= _playerHandler.OnItemDropped;
            _playerHandler = null;
            base.OnDisabled();
        }

    }
}