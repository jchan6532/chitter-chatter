using System.Windows;
using System;

using TCPHelpers.BaseClasses.Client;
using Client.AppModel;

namespace Client.ViewModels
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The client inside the window view model
        /// </summary>
        private ClientAPPMODEL mClient = null;

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow = null;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        private int mResizeBorder = 6;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        #endregion


        #region Public Properties

        #region Resize Border and thickness properties
        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder
        {
            get
            {
                return this.mResizeBorder;
            }
            set
            {
                this.mResizeBorder = value;
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get
            {
                return new Thickness(this.ResizeBorder);
            }
        }
        #endregion

        #region Resize Border and thickness properties
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize 
        {
            get
            {
                if (this.mWindow.WindowState == WindowState.Maximized)
                {
                    return 0;
                }
                else
                {
                    return this.mOuterMarginSize;
                }
            }
            set
            {
                this.mOuterMarginSize = value;
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginThickness 
        {
            get
            {
                return new Thickness(this.OuterMarginSize);
            } 
        }
        #endregion

        #region Window and corner radius
        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                if (this.mWindow.WindowState == WindowState.Maximized)
                {
                    return 0;
                }
                else
                {
                    return this.mWindowRadius;
                }
            }
            set
            {
                this.mWindowRadius = value;
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius 
        { 
            get 
            { 
                return new CornerRadius(this.WindowRadius);
            }
        }
        #endregion

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this.mWindow = window;

            // Listen out for the window resizing
            this.mWindow.StateChanged += (sender, e) =>
            {

            };

            this.mClient = new ClientAPPMODEL();
        }

        #endregion
    }
}
