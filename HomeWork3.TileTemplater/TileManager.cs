using Microsoft.Phone.Shell;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork3
{
    public sealed class TileManager
    {
        private IsolatedStorageFile _isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

        public async Task SaveToSharedShellDirectory(string filename, Stream inputFile)
        {
            var filelePath = Path.Combine("shared/shellcontent/", filename);
            using (var isoStoreFile = _isolatedStorageFile.OpenFile(filelePath, FileMode.Create, FileAccess.ReadWrite))
            {
                await inputFile.CopyToAsync(isoStoreFile);
            }
        }

        public string GetShellDirectoryFilePath(string filename)
        {
            return string.Concat("isostore:/Shared/ShellContent/", filename);
        }

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
