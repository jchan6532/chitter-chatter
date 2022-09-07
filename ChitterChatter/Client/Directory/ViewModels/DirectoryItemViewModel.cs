using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Client.Directory.Data;

namespace Client.Directory.ViewModels
{
    /// <summary>
    /// View model for directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DirectoryItem mItem;

        public DirectoryItem Item { 
            get 
            { 
                return this.mItem;
            }
            set 
            {
                this.mItem = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Item)));
            }
        }
    }
}
