using Microsoft.Phone.Shell;
using System.Linq;

namespace HomeWork3
{
    public sealed class TileManager
    {
        public bool SetApplicationTileData<T>(T newData) where T : ShellTileData
        {
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault();
            if (tile != null)
            {
                tile.Update(newData);
                return true;
            }
            return false;
        }
        
        private void InitializeTileManager()
        {
        }

        #region Singleton Pattern w/ Constructor
        private TileManager()
            : base()
        {
            InitializeTileManager();
        }
        public static TileManager Instance
        {
            get
            {
                return SingletonTileManagerCreator._Instance;
            }
        }
        private class SingletonTileManagerCreator
        {
            private SingletonTileManagerCreator() { }
            public static TileManager _Instance = new TileManager();
        }
        #endregion

    }
}
