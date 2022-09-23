using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using TCPHelpers.BaseClasses.Client;

namespace Client.ViewModels
{
    /// <summary>
    /// View model for the login screen
    /// </summary>
    /// <seealso cref="TCPHelpers.BaseClasses.Client.BaseViewModel" />
    public class LoginViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The page this view model controls
        /// </summary>
        private Page mPage = null;

        #endregion

        #region Public Properties



        #endregion


        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion


        #region Private methods

        private bool Login(Page page)
        {
            this.mPage = page;

            return false;
        }

        #endregion

    }
}
