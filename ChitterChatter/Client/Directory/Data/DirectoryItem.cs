using Client.Directory;

namespace Client.Directory.Data
{
    /// <summary>
    /// Information about a directory item such as a drive/file/folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get
            {
                if (this.Type == DirectoryItemType.Drive)
                {
                    return this.FullPath;
                }
                else
                {
                    return DirectoryStructure.GetFileFolderName(this.FullPath);
                }
            }
        }
    }
}
